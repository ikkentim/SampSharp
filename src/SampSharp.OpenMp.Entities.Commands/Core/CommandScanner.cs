using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Logging;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Scans ISystem types for command methods marked with [PlayerCommand] or [ConsoleCommand].
/// Builds CommandDefinition objects and registers them in a registry.
/// </summary>
internal partial class CommandScanner
{
    private static readonly MethodInfo _getComponentInfo = typeof(IEntityManager).GetMethod(nameof(IEntityManager.GetComponent),
        BindingFlags.Public | BindingFlags.Instance, null, [typeof(EntityId)], null)!;
    private readonly ISystemRegistry _systemRegistry;
    private readonly IUnhandledExceptionHandler _unhandledExceptionHandler;
    private readonly ILogger _logger;

    public CommandScanner(ISystemRegistry systemRegistry, IUnhandledExceptionHandler unhandledExceptionHandler, ILogger logger)
    {
        _systemRegistry = systemRegistry;
        _unhandledExceptionHandler = unhandledExceptionHandler;
        _logger = logger;
    }

    public void ScanPlayerCommands(CommandRegistry registry, ICommandParameterParserFactory parserFactory)
    {
        var scanner = ClassScanner.Create().IncludeTypes(_systemRegistry.GetSystemTypes().Span).IncludeNonPublicMembers();

        var methods = scanner.ScanMethods<PlayerCommandAttribute>();

        foreach (var (systemType, method, attribute) in methods)
        {
            var classGroups = systemType.GetCustomAttributes<CommandGroupAttribute>();
            var methodGroups = method.GetCustomAttributes<CommandGroupAttribute>();
            var commandGroup = BuildCommandGroup(classGroups, methodGroups);
            var commandName = attribute.Name ?? GetCommandName(method);

            if (string.IsNullOrWhiteSpace(commandName) || commandName.Contains(' '))
            {
                LogRejectedCommand("player", systemType, method,
                    $"invalid command name '{commandName ?? "<null>"}'. Command names must not be empty or contain spaces.");
                continue;
            }

            var parameters = method.GetParameters();
            if (parameters.Length == 0 || (!parameters[0].ParameterType.IsAssignableTo(typeof(Component)) && parameters[0].ParameterType != typeof(EntityId)))
            {
                LogRejectedCommand("player", systemType, method, "the first parameter must be a Component or EntityId.");
                continue;
            }

            var aliases = method.GetCustomAttributes<AliasAttribute>().SelectMany(a => a.Aliases).Select(a => new CommandAlias(a)).ToArray();
            var tags = method.GetCustomAttributes<CommandTagAttribute>().Select(t => new CommandTag(t.Key, t.Value)).ToArray();

            if (!TryBuildOverload(commandName, commandGroup, method, systemType, parserFactory, 1, aliases, tags, out var overload, out var rejectionReason))
            {
                LogRejectedCommand("player", systemType, method, rejectionReason!);
                continue;
            }

            registry.Register(overload);
        }
    }

    public void ScanConsoleCommands(CommandRegistry registry, ICommandParameterParserFactory parserFactory)
    {
        var scanner = ClassScanner.Create().IncludeTypes(_systemRegistry.GetSystemTypes().Span).IncludeNonPublicMembers();

        var methods = scanner.ScanMethods<ConsoleCommandAttribute>();

        foreach (var (systemType, method, attribute) in methods)
        {
            var classGroups = systemType.GetCustomAttributes<CommandGroupAttribute>();
            var methodGroups = method.GetCustomAttributes<CommandGroupAttribute>();
            var commandGroup = BuildCommandGroup(classGroups, methodGroups);
            var commandName = attribute.Name ?? GetCommandName(method);

            if (string.IsNullOrWhiteSpace(commandName) || commandName.Contains(' '))
            {
                LogRejectedCommand("console", systemType, method,
                    $"invalid command name '{commandName ?? "<null>"}'. Command names must not be empty or contain spaces.");
                continue;
            }

            var aliases = method.GetCustomAttributes<AliasAttribute>().SelectMany(a => a.Aliases).Select(a => new CommandAlias(a)).ToArray();
            var tags = method.GetCustomAttributes<CommandTagAttribute>().Select(t => new CommandTag(t.Key, t.Value)).ToArray();

            var prefixParams = 0;
            var parameters = method.GetParameters();
            if (parameters.Length > 0 && parameters[0].ParameterType == typeof(ConsoleCommandDispatchContext))
            {
                prefixParams = 1;
            }

            if (!TryBuildOverload(commandName, commandGroup, method, systemType, parserFactory, prefixParams, aliases, tags, out var overload, out var rejectionReason))
            {
                LogRejectedCommand("console", systemType, method, rejectionReason!);
                continue;
            }

            registry.Register(overload);
        }
    }

    private CommandGroup? BuildCommandGroup(IEnumerable<CommandGroupAttribute> classGroups, IEnumerable<CommandGroupAttribute> methodGroups)
    {
        var allParts = classGroups.SelectMany(g => g.Parts).Concat(methodGroups.SelectMany(g => g.Parts)).ToList();

        return allParts.Count > 0 ? new CommandGroup(allParts) : null;
    }

    private bool TryBuildOverload(string commandName, CommandGroup? commandGroup, MethodInfo method, Type systemType, ICommandParameterParserFactory parserFactory,
        int prefixParameters, CommandAlias[] aliases, CommandTag[] tags, [NotNullWhen(true)] out CommandDefinition? overload, [NotNullWhen(false)] out string? rejectionReason)
    {
        overload = null;
        rejectionReason = null;

        var parameters = method.GetParameters();
        if (parameters.Length < prefixParameters)
        {
            rejectionReason = "the command signature is missing required dispatch context parameters.";
            return false;
        }

        // Validate return type: bool, int, void, Task, Task<T>
        if (!IsValidReturnType(method.ReturnType))
        {
            rejectionReason = $"invalid return type '{method.ReturnType}'. Supported return types are void, bool, Task, and Task<bool>.";
            return false;
        }

        // Collect parsed parameters (skip prefix, handle DI)
        if (!TryCollectParameters(parameters, prefixParameters, parserFactory, out var parsedParams, out rejectionReason))
        {
            return false;
        }

        var parameterSources = BuildMethodParameterSources(parameters, prefixParameters, parsedParams!);
        var invoker = CompileCommandInvoker(method, parameterSources);
        var componentMatcher = CompileComponentMatcher(parameterSources);

        overload = new CommandDefinition(commandName, commandGroup, method, parameters, systemType, parsedParams!, invoker, prefixParameters, aliases, tags, componentMatcher);

        return true;
    }

    private static MethodParameterSource[] BuildMethodParameterSources(ParameterInfo[] parameters, int prefixParameterCount, CommandParameterInfo[] parsedParameters)
    {
        var sources = new MethodParameterSource[parameters.Length];
        var parsedParamsByIndex = parsedParameters.ToDictionary(p => p.ParameterIndex);

        var j = 0; // Counter for args array index

        for (var i = 0; i < parameters.Length; i++)
        {
            var paramInfo = parameters[i];
            var source = new MethodParameterSource(paramInfo);

            // Check if this is a prefix parameter (Player component or ConsoleCommandDispatchContext)
            if (i < prefixParameterCount)
            {
                source.ParameterIndex = j++;
            }
            // Check if this is a parsed parameter
            else if (parsedParamsByIndex.ContainsKey(i))
            {
                source.ParameterIndex = j++;
            }
            else
            {
                // This is a DI service parameter
                source.IsService = true;
            }

            // Mark as component if applicable
            if (paramInfo.ParameterType.IsAssignableTo(typeof(Component)))
            {
                source.IsComponent = true;
            }

            sources[i] = source;
        }

        return sources;
    }

    private CommandInvoker CompileCommandInvoker(MethodInfo method, MethodParameterSource[] sources)
    {
        var methodInvoker = MethodInvokerFactory.Compile(method, sources, MethodResult.False);

        return ToCommandInvoker(methodInvoker, method);
    }

    private static Func<object?[], IEntityManager, bool>? CompileComponentMatcher(MethodParameterSource[] sources)
    {
        var componentSources = sources.Where(s => s.IsComponent && s.ParameterIndex >= 0).ToArray();
        if (componentSources.Length == 0)
        {
            return null;
        }

        var args = Expression.Parameter(typeof(object[]), "args");
        var entityManager = Expression.Parameter(typeof(IEntityManager), "entityManager");
        var entityEmpty = Expression.Constant(EntityId.Empty, typeof(EntityId));

        Expression? body = null;
        foreach (var source in componentSources)
        {
            var entity = Expression.Convert(Expression.ArrayIndex(args, Expression.Constant(source.ParameterIndex)), typeof(EntityId));
            var componentType = source.Info.ParameterType;
            var component = Expression.Condition(
                Expression.Equal(entity, entityEmpty),
                Expression.Constant(null, componentType),
                Expression.Call(entityManager, _getComponentInfo.MakeGenericMethod(componentType), entity));

            var check = Expression.OrElse(
                Expression.Equal(entity, entityEmpty),
                Expression.NotEqual(component, Expression.Constant(null, componentType)));

            body = body == null ? check : Expression.AndAlso(body, check);
        }

        return Expression.Lambda<Func<object?[], IEntityManager, bool>>(body!, args, entityManager).Compile();
    }

    private CommandInvoker ToCommandInvoker(MethodInvoker methodInvoker, MethodInfo method)
    {
        if (method.ReturnType == typeof(void) )
        {
            return [StackTraceHidden](target, args, services, manager) =>
            {
                var result = (MethodResult?)methodInvoker(target, args, services, manager);
                return result?.Value ?? true;
            };
        }

        if (method.ReturnType == typeof(bool))
        {
            return [StackTraceHidden] (target, args, services, manager) => ((MethodResult)methodInvoker(target, args, services, manager)!).Value;
        }

        if (method.ReturnType == typeof(Task))
        {
            return [StackTraceHidden](target, args, services, manager) =>
            {
                var result = methodInvoker(target, args, services, manager)!;

                if (result is Task task)
                {
                    HandleTask(task);
                    return true;
                }

                if (result is MethodResult methodResult)
                {
                    return methodResult.Value;
                }

                return true;
            };
        }

        if (method.ReturnType == typeof(Task<bool>))
        {
            return [StackTraceHidden](target, args, services, manager) =>
            {
                var result = methodInvoker(target, args, services, manager)!;

                if (result is Task<bool> task)
                {
                    if (task.IsCompleted)
                    {
                        return task.Result;
                    }

                    HandleTask(task);
                    return true;
                }

                if (result is MethodResult methodResult)
                {
                    return methodResult.Value;
                }

                return true;
            };
        }

        throw new InvalidOperationException();
    }

    private void HandleTask(Task task)
    {
        if (task.IsCompleted)
        {
            HandleTaskException(task);
        }
        else
        {
            task.ContinueWith(HandleTaskException);
        }
    }

    private void HandleTaskException(Task task)
    {
        if (task is { IsFaulted: true, Exception: not null })
        {
            // Exception from async task - would typically be logged
            if (task.Exception.InnerExceptions.Count == 1)
            {
                _unhandledExceptionHandler.Handle("async-command", task.Exception.InnerExceptions[0]);
            }
            else
            {
                _unhandledExceptionHandler.Handle("async-command", task.Exception);
            }
        }
    }

    private static bool IsValidReturnType(Type returnType)
    {
        return returnType == typeof(void) || 
               returnType == typeof(bool) ||
               returnType == typeof(Task) || 
               returnType == typeof(Task<bool>);
    }

    private static bool TryCollectParameters(ParameterInfo[] parameters, int prefixParameters, ICommandParameterParserFactory parserFactory,
        out CommandParameterInfo[]? result, [NotNullWhen(false)] out string? rejectionReason)
    {
        result = null;
        rejectionReason = null;

        if (parameters.Length < prefixParameters)
        {
            rejectionReason = "the command signature is missing required dispatch context parameters.";
            return false;
        }

        var list = new List<CommandParameterInfo>();
        var parameterIndex = prefixParameters;
        var optionalSeen = false;

        for (var i = prefixParameters; i < parameters.Length; i++)
        {
            var param = parameters[i];

            var paramName = param.Name ?? $"param{i}";

            // Try to get a parser for this parameter
            var parser = parserFactory.CreateParser(parameters, i);

            if (parser == null)
            {
                // No parser = this is a DI parameter, not parsed from input
                parameterIndex++;
                continue;
            }

            // This parameter will be parsed from input
            var isRequired = !param.HasDefaultValue;
            if (!isRequired && !optionalSeen)
            {
                optionalSeen = true;
            }
            else if (isRequired && optionalSeen)
            {
                // Required parameter after optional - invalid
                rejectionReason = "required parameters cannot follow optional parameters.";
                return false;
            }

            var cmdParamInfo = new CommandParameterInfo(paramName, parser, isRequired, param.DefaultValue, parameterIndex++);

            list.Add(cmdParamInfo);
        }

        result = list.ToArray();
        return true;
    }

    private static string GetCommandName(MethodInfo method)
    {
        var name = method.Name.ToLowerInvariant();
        if (name.EndsWith("command", StringComparison.Ordinal))
        {
            name = name[..^7];
        }

        return name;
    }

    private void LogRejectedCommand(string commandKind, Type systemType, MethodInfo method, string reason)
    {
        LogRejectedCommand(commandKind, systemType.FullName ?? systemType.Name, method.Name, reason);
    }

    [LoggerMessage(LogLevel.Warning, "Rejected {CommandKind} command {SystemType}.{Method}: {Reason}")]
    private partial void LogRejectedCommand(string commandKind, string systemType, string method, string reason);
}
