using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SampSharp.Core;
using SampSharp.Core.Logging;
using SampSharp.EntityComponentSystem.Components;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Systems;

namespace SampSharp.EntityComponentSystem.Events
{
    public class EventService : IEventService
    {
        private static readonly Type[] DefaultParameterTypes =
        {
            typeof(int),
            typeof(bool),
            typeof(float),
            typeof(int[]),
            typeof(bool[]),
            typeof(float[]),
            typeof(string),
            typeof(Entity)
        };

        private static readonly MethodInfo GetComponentInfo =
            typeof(Entity).GetMethod(nameof(Entity.GetComponent), BindingFlags.Public | BindingFlags.Instance);

        private static readonly MethodInfo GetServiceInfo =
            typeof(EventService).GetMethod(nameof(GetService), BindingFlags.NonPublic | BindingFlags.Static);

        private readonly Dictionary<string, Event> _events = new Dictionary<string, Event>();
        private readonly IGameModeClient _gameModeClient;
        private readonly IServiceProvider _serviceProvider;

        public EventService(IGameModeClient gameModeClient, IServiceProvider serviceProvider)
        {
            _gameModeClient = gameModeClient;
            _serviceProvider = serviceProvider;

            Scanner();
        }

        public void Load(string name, Type[] parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            // TODO: Callbacks with parameter length are not yet supported

            var handler = GetInvoke(name);
            _gameModeClient.RegisterCallback(name, handler.Target, handler.Method, parameters);

            if (!_events.ContainsKey(name)) _events[name] = new Event();
        }

        public void Use(string name, Func<EventDelegate, EventDelegate> middleware)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (middleware == null) throw new ArgumentNullException(nameof(middleware));

            if (!_events.TryGetValue(name, out var evt))
                _events[name] = evt = new Event();

            evt.Middleware.Add(middleware);
        }

        private object InvokeEventFuncs(EventContext context)
        {
            CoreLog.LogDebug($"Event: {context.Name}({string.Join(", ", context.Arguments)})");

            if (_events.TryGetValue(context.Name, out var evt))
                foreach (var sysEvt in evt.SystemEvents)
                {
                    var system = _serviceProvider.GetService(sysEvt.Method.DeclaringType);

                    if (system == null)
                        continue;

                    sysEvt.Call(system, context.Arguments, context.EventServices);

                    // TODO: Handle return value
//                    if (sysEvt.ParametersArray)
//                    {
//                        sysEvt.Method.Invoke(system, new object[] {context.Arguments});
//                    }
//                    else
//                    {
//                        // Match signature of method
//                        if (context.Arguments.Length != sysEvt.ParameterTypes.Length)
//                            continue;
//
//                        if (context.Arguments.Length != 0 &&
//                            context.Arguments.Where((a, i) =>
//                                !sysEvt.ParameterTypes[i].IsInstanceOfType(a) &&
//                                !(a == null && !sysEvt.ParameterTypes[i].IsValueType)).Any())
//                            continue;
//
//                        sysEvt.Method.Invoke(system, context.Arguments);
//                    }
                }

            return null;
        }

        private void Scanner()
        {
            var assembly = Assembly.GetEntryAssembly();

            var assemblies = new List<Assembly>();

            void AddToScan(Assembly asm)
            {
                if (assemblies.Contains(asm))
                    return;

                assemblies.Add(asm);

                foreach (var assemblyRef in asm.GetReferencedAssemblies())
                {
                    if (assemblyRef.Name.StartsWith("System") || assemblyRef.Name.StartsWith("Microsoft") ||
                        assemblyRef.Name.StartsWith("netstandard"))
                        continue;

                    AddToScan(Assembly.Load(assemblyRef));
                }
            }

            AddToScan(assembly);

            CoreLog.LogDebug("Scan {0} assemblies for events", assemblies.Count);

            // Find eligible methods in system implementations
            var events = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && typeof(ISystem).IsAssignableFrom(t))
                .SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                .Select(m => (method: m, attribute: m.GetCustomAttribute<EventAttribute>()))
                .Where(x => x.attribute != null);

            // Add event methods to the event data
            foreach (var (method, attribute) in events)
            {
                CoreLog.LogDebug("Adding event listener on " + method.DeclaringType + "." + method.Name);

                var name = attribute.Name ?? method.Name;

                if (!_events.TryGetValue(name, out var evt))
                    _events[name] = evt = new Event();

                var parameterIndex = 0;
                var parameters = method.GetParameters();
                var parameterInfos = new SystemEventParameter[parameters.Length];
                var parametersArrayIndex = -1;
                var parameterTypes = new List<Type>();

                void AssertNoParamsArray()
                {
                    // Assert no object[] parameter has been detected already
                    if (parametersArrayIndex != -1)
                        throw new EventSignatureException(
                            $"Event method {method.DeclaringType.Name}.{method.Name} has invalid parameters.");
                }

                // Scan and arrange parameters.
                for (var i = 0; i < parameters.Length; i++)
                {
                    var type = parameters[i].ParameterType;

                    parameterInfos[i] = new SystemEventParameter();

                    // object[] array can appear once without any other non-DI parameters.
                    if (type == typeof(object[]))
                    {
                        AssertNoParamsArray();
                        parametersArrayIndex = i;
                        continue;
                    }

                    if (typeof(Component).IsAssignableFrom(type))
                    {
                        // Get component of entity
                        AssertNoParamsArray();
                        parameterInfos[i].ParameterIndex = parameterIndex++;
                        parameterInfos[i].ComponentType = type;
                        parameterTypes.Add(typeof(Entity));
                    }
                    else if (DefaultParameterTypes.Contains(type))
                    {
                        // Default pass-through types
                        AssertNoParamsArray();
                        parameterInfos[i].ParameterIndex = parameterIndex++;
                        parameterTypes.Add(type);
                    }
                    else
                    {
                        // dependency injection
                        parameterInfos[i].ServiceType = type;
                    }
                }

                var caller = Compile(method, parameters, parameterInfos, parametersArrayIndex);

                var entry = new SystemEvent
                {
                    Method = method,
                    ParameterTypes = parameterTypes.ToArray(),
                    Call = (instance, args, serviceProvider) =>
                    {
                        // TODO: Check parameters match types?
                        return caller(instance, args, serviceProvider);
                    }
                };

                evt.SystemEvents.Add(entry);
            }
        }

        private object Invoke(EventContext context, int index, Event evt)
        {
            // TODO: Construct the middleware just once
            return index >= evt.Middleware.Count
                ? InvokeEventFuncs(context)
                : evt.Middleware[index](eventContext => Invoke(eventContext, index + 1, evt))(context);
        }

        private Func<object[], object> GetInvoke(string name)
        {
            var context = new EventContextImpl();
            context.SetName(name);

            return args =>
            {
                context.SetArguments(args);
                context.SetEventServices(_serviceProvider); // TODO: Should I scope it?

                return _events.TryGetValue(name, out var evt)
                    ? Invoke(context, 0, evt)
                    : null;
            };
        }

        private static Func<object, object[], IServiceProvider, object> Compile(MethodInfo methodInfo,
            ParameterInfo[] parameters, SystemEventParameter[] parameterInfos, int parametersArrayIndex)
        {
            if (methodInfo.DeclaringType == null)
                throw new ArgumentException("Method must have declaring type", nameof(methodInfo));

            // Input arguments
            var instanceArg = Expression.Parameter(typeof(object), "instance");
            var argsArg = Expression.Parameter(typeof(object[]), "args");
            var providerArg = Expression.Parameter(typeof(IServiceProvider), "serviceProvider");

            var methodArguments = new Expression[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                if (parameterType.IsByRef) throw new NotSupportedException();

                if (i == parametersArrayIndex)
                {
                    // Args array
                    methodArguments[i] = argsArg;
                }
                else if (parameterInfos[i].ComponentType != null)
                {
                    // Get component from entity
                    Expression getValue =
                        Expression.ArrayIndex(argsArg, Expression.Constant(parameterInfos[i].ParameterIndex));
                    getValue = Expression.Convert(getValue, typeof(Entity));
                    methodArguments[i] = Expression.Call(getValue,
                        GetComponentInfo.MakeGenericMethod(parameterInfos[i].ComponentType));
                }
                else if (parameterInfos[i].ServiceType != null)
                {
                    // Get service
                    var getServiceCall = Expression.Call(GetServiceInfo, providerArg,
                        Expression.Constant(parameterType, typeof(Type)));
                    methodArguments[i] = Expression.Convert(getServiceCall, parameterType);
                }
                else if (parameterInfos[i].ParameterIndex >= 0)
                {
                    // Pass through
                    var getValue =
                        Expression.ArrayIndex(argsArg, Expression.Constant(parameterInfos[i].ParameterIndex));
                    methodArguments[i] = Expression.Convert(getValue, parameterType);
                }
            }

            var service = Expression.Convert(instanceArg, methodInfo.DeclaringType);
            Expression body = Expression.Call(service, methodInfo, methodArguments);

            if (methodInfo.ReturnType == typeof(void))
                body = Expression.Block(body, Expression.Constant(null));
            else if (methodInfo.ReturnType != typeof(object)) body = Expression.Convert(body, typeof(object));

            var lambda =
                Expression.Lambda<Func<object, object[], IServiceProvider, object>>(body, instanceArg, argsArg,
                    providerArg);

            return lambda.Compile();
        }

        private static object GetService(IServiceProvider sp, Type type)
        {
            var service = sp.GetService(type);
            return service ?? throw new InvalidOperationException();
        }

        private class Event
        {
            public readonly List<Func<EventDelegate, EventDelegate>> Middleware =
                new List<Func<EventDelegate, EventDelegate>>();

            public readonly List<SystemEvent> SystemEvents = new List<SystemEvent>();
        }

        private class SystemEvent
        {
            public Func<object, object[], IServiceProvider, object> Call;
            public MethodInfo Method;
            public Type[] ParameterTypes;
        }

        private class SystemEventParameter
        {
            public Type ComponentType;
            public int ParameterIndex = -1;
            public Type ServiceType;
        }
    }
}