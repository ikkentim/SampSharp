using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using SampSharp.Entities.Utilities;
using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

#pragma warning disable CS0618 // Type or member is obsolete
internal class EventDispatcher : IEventDispatcher, IEventService
#pragma warning restore CS0618 // Type or member is obsolete
{
    private static readonly Type[] _defaultParameterTypes =
    [
        typeof(string)
    ];

    private readonly IEntityManager _entityManager;

    private readonly Dictionary<string, Event> _events = new();
    private readonly ILogger<EventDispatcher> _logger;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventDispatcher" /> class.
    /// </summary>
    public EventDispatcher(IServiceProvider serviceProvider, IEntityManager entityManager, ILogger<EventDispatcher> logger, ISystemRegistry systemRegistry)
    {
        _serviceProvider = serviceProvider;
        _entityManager = entityManager;
        _logger = logger;

        systemRegistry.Register(() => LoadTargetSites(systemRegistry));
    }

    private Event GetOrCreateEvent(string name)
    {
        if (!_events.TryGetValue(name, out var @event))
        {
            _events[name] = @event = new Event(name);
        }

        return @event;
    }

    private object? InnerInvoke(EventContext context, Event @event, object? defaultResult)
    {
        object? result = null;
        
        foreach (var targetSite in @event.TargetSites)
        {
            targetSite.Target ??= _serviceProvider.GetService(targetSite.TargetType);

            // System is not loaded. Skip target site
            if (targetSite.Target == null)
            {
                continue;
            }

            var targetResult = targetSite.Invoke(targetSite.Target, context);

            // Do not consider default result as invocation result
            if (targetResult is null || targetResult == defaultResult)
            {
                continue;
            }

            result = targetResult;
        }

        return result ?? defaultResult;
    }

    private void LoadTargetSites(ISystemRegistry systemRegistry)
    {
        // Find methods with EventAttribute in any loaded system.
        var events = ClassScanner.Create()
            .IncludeTypes(systemRegistry.GetSystemTypes().Span)
            .IncludeNonPublicMembers()
            .ScanMethods<EventAttribute>();

        // Gather event data, compile invoker and add the data to the events collection.
        foreach (var (target, method, attribute) in events)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Adding event listener on {type}.{method}.", method.DeclaringType, method.Name);
            }

            var name = attribute.Name ?? method.Name;
            var @event = GetOrCreateEvent(name);

            var (paramCount, paramSources) = GetParameterSources(method);

            var targetSite = CreateTargetSite(target, method, paramSources, paramCount);
            @event.TargetSites.Add(targetSite);
        }
    }

    private static (int paramCount, MethodParameterSource[] paramSources) GetParameterSources(MethodInfo method)
    {
        var paramIndex = 0; // The current pointer in the event arguments array.
        var parameterSources = method.GetParameters()
            .Select(info => new MethodParameterSource(info))
            .ToArray();

        // Determine the source of each parameter.
        foreach (var source in parameterSources)
        {
            var type = source.Info.ParameterType;

            if (typeof(Component).IsAssignableFrom(type))
            {
                // Components are provided by the entity in the arguments array of the event.
                source.ParameterIndex = paramIndex++;
                source.IsComponent = true;
            }
            else if (type.IsValueType || type.IsArray || _defaultParameterTypes.Contains(type) || type.GetCustomAttribute<EventParameterAttribute>() != null)
            {
                // Default types are passed straight through.
                source.ParameterIndex = paramIndex++;
            }
            else
            {
                // Other types are provided trough Dependency Injection.
                source.IsService = true;
            }
        }

        return (paramIndex, parameterSources);
    }

    private TargetSiteData CreateTargetSite(Type target, MethodInfo method, MethodParameterSource[] parameterInfos, int sourceParamCount)
    {
        var compiled = MethodInvokerFactory.Compile(method, parameterInfos);
        var targetSiteName = $"{method.DeclaringType?.FullName}.{method.Name}";

        return new TargetSiteData(target, (instance, eventContext) =>
        {
            try
            {
                var args = eventContext.Arguments;
                if (sourceParamCount == args.Length)
                {
                    return compiled.Invoke(instance, args, eventContext.EventServices, _entityManager);
                }

                _logger.LogError(
                    "Event \"{eventName}\" argument count mismatch: dispatcher passed {argsLength} arg(s), handler {targetSite}({handlerParams}) expects {sourceParamCount}",
                    eventContext.Name,
                    args.Length,
                    targetSiteName,
                    string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}")),
                    sourceParamCount);
            }
            catch(Exception ex)
            {
                SampSharpExceptionHandler.HandleException(targetSiteName, ex);
            }

            return null;
        });
    }

    /// <summary>
    /// Creates an invoker for an event which creates and manages the context for the event.
    /// </summary>
    private EventInvokeDelegate CreateEventInvoke(Event @event, object? defaultResult)
    {
        var context = new EventContextImpl(@event.Name, _serviceProvider);

        // In order to chain the middleware from first to last, the middleware must be nested from last to first
        EventDelegate invoke = ctx => InnerInvoke(ctx, @event, defaultResult);
        for (var i = @event.Middleware.Count - 1; i >= 0; i--)
        {
            invoke = @event.Middleware[i](invoke);
        }

        return args =>
        {
            try
            {
                context.SetArguments(args);

                var result = invoke(context);
                return result switch
                {
                    Task<bool> task => !task.IsCompleted ? null : (task.Result ? MethodResult.True : MethodResult.False),
                    Task<int> task => !task.IsCompleted ? null : task.Result,
                    Task => null,
                    _ => result
                };
            }
            catch(Exception ex)
            {
                SampSharpExceptionHandler.HandleException(@event.Name, ex);
                return null;
            }
        };
    }

    public void UseMiddleware(string name, Func<EventDelegate, EventDelegate> middleware)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(middleware);

        var @event = GetOrCreateEvent(name);
        @event.ClearCache();
        @event.Middleware.Add(middleware);
    }

    public object? Invoke(string name, params ReadOnlySpan<object> arguments)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (!_events.TryGetValue(name, out var @event))
        {
            return null;
        }

        if(!@event.Cache.TryGetValue(NullValue.Instance, out var invoke))
        {
            invoke = CreateEventInvoke(@event, null);
            @event.Cache.TryAdd(NullValue.Instance, invoke);
        }

        return invoke(arguments);
    }

    [return: NotNullIfNotNull(nameof(defaultValue))]
    public T? InvokeAs<T>(string name, T defaultValue, params ReadOnlySpan<object> arguments)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (!_events.TryGetValue(name, out var @event))
        {
            return defaultValue;
        }

        var defaultKey = defaultValue ?? (object)NullValue.Instance;
        if(!@event.Cache.TryGetValue(defaultKey, out var invoke))
        {
            invoke = CreateEventInvoke(@event, defaultValue);
            @event.Cache.TryAdd(defaultKey, invoke);
        }

        var result = invoke(arguments);

        if(result is Task<T> task)
        {
            return task.IsCompleted ? task.Result : defaultValue;
        }

        if (typeof(T) == typeof(bool) && result is MethodResult m)
        {
            var value = m.Value;
            return Unsafe.As<bool, T>(ref value);
        }

        return result is T resultAsT 
            ? resultAsT 
            : defaultValue;
    }


    private delegate object? EventInvokeDelegate(ReadOnlySpan<object> args);

    private sealed record Event(string Name)
    {
        public List<TargetSiteData> TargetSites { get; } = [];
        public List<Func<EventDelegate, EventDelegate>> Middleware { get; } = [];
        public ConcurrentDictionary<object, EventInvokeDelegate> Cache { get; } = new();

        public void ClearCache()
        {
            Cache.Clear();
        }
    }

    private sealed record NullValue
    {
        public static NullValue Instance { get; } = new();
    }

    private sealed record TargetSiteData(Type TargetType, Func<object, EventContext, object?> Invoke)
    {
        public object? Target { get; set; }
    }
}