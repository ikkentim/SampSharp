// SampSharp
// Copyright 2020 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SampSharp.Core;
using SampSharp.Core.Logging;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents the event service.
    /// </summary>
    /// <seealso cref="IEventService" />
    public class EventService : IEventService
    {
        private static readonly Type[] DefaultParameterTypes =
        {
            typeof(string)
            // TODO: Callbacks with parameter length are not yet supported
            //typeof(int[]),
            //typeof(bool[]),
            //typeof(float[]),
        };

        private readonly Dictionary<string, Event> _events = new Dictionary<string, Event>();
        private readonly IGameModeClient _gameModeClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEntityManager _entityManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventService" /> class.
        /// </summary>
        public EventService(IGameModeClient gameModeClient, IServiceProvider serviceProvider, IEntityManager entityManager)
        {
            _gameModeClient = gameModeClient;
            _serviceProvider = serviceProvider;
            _entityManager = entityManager;

            CreateEventsFromAssemblies();
        }

        /// <inheritdoc />
        public void EnableEvent(string name, Type[] parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var handler = BuildInvoke(name);
            _gameModeClient.RegisterCallback(name, handler.Target, handler.Method, parameters, null);
        }

        /// <inheritdoc />
        public void UseMiddleware(string name, Func<EventDelegate, EventDelegate> middleware)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (middleware == null) throw new ArgumentNullException(nameof(middleware));

            if (!_events.TryGetValue(name, out var @event))
                _events[name] = @event = new Event(Invoke);

            @event.Middleware.Add(middleware);

            // In order to chain the middleware from first to last, the middleware must be nested from last to first
            EventDelegate invoke = Invoke;
            for (var i = @event.Middleware.Count - 1; i >= 0; i--) invoke = @event.Middleware[i](invoke);

            @event.Invoke = invoke;
        }

        /// <inheritdoc />
        public object Invoke(string name, params object[] args)
        {
            // TODO Could cache built invokers into a dictionary
            return BuildInvoke(name)(args);
        }

        private object Invoke(EventContext context)
        {
            object result = null;

            if (context.Name == null || !_events.TryGetValue(context.Name, out var evt)) return null;

            foreach (var sysEvt in evt.Invokers)
            {
                var system = _serviceProvider.GetService(sysEvt.TargetType);

                // System is not loaded. Skip invoking target.
                if (system == null)
                    continue;

                result = sysEvt.Invoke(system, context) ?? result;
            }

            return result;
        }

        private void CreateEventsFromAssemblies()
        {
            // Find methods with EventAttribute in any ISystem in any assembly.
            var events = new AssemblyScanner()
                .IncludeAllAssemblies()
                .IncludeNonPublicMembers()
                .Implements<ISystem>()
                .ScanMethods<EventAttribute>();

            // Gather event data, compile invoker and add the data to the events collection.
            foreach (var (method, attribute) in events)
            {
                CoreLog.LogDebug("Adding event listener on {0}.{1}.", method.DeclaringType, method.Name);

                var name = attribute.Name ?? method.Name;

                if (!_events.TryGetValue(name, out var @event))
                    _events[name] = @event = new Event(Invoke);

                var argsPtr = 0; // The current pointer in the event arguments array.
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
                        source.ParameterIndex = argsPtr++;
                        source.IsComponent = true;
                    }
                    else if (type.IsValueType || DefaultParameterTypes.Contains(type))
                    {
                        // Default types are passed straight trough.
                        source.ParameterIndex = argsPtr++;
                    }
                    else
                    {
                        // Other types are provided trough Dependency Injection.
                        source.IsService = true;
                    }
                }

                var invoker = CreateInvoker(method, parameterSources, argsPtr);
                @event.Invokers.Add(invoker);
            }
        }

        private InvokerInfo CreateInvoker(MethodInfo method, MethodParameterSource[] parameterInfos,
            int callbackParamCount)
        {
            var compiled = MethodInvokerFactory.Compile(method, parameterInfos);

            return new InvokerInfo
            {
                TargetType = method.DeclaringType,
                Invoke = (instance, eventContext) =>
                {
                    var args = eventContext.Arguments;
                    if (callbackParamCount == args.Length)
                        return compiled(instance, args, eventContext.EventServices, _entityManager);

                    CoreLog.Log(CoreLogLevel.Error,
                        $"Callback parameter count mismatch {callbackParamCount} != {args.Length}");
                    return null;
                }
            };
        }

        private Func<object[], object> BuildInvoke(string name)
        {
            var context = new EventContextImpl();
            context.SetName(name);
            context.SetEventServices(_serviceProvider);

            return args =>
            {
                context.SetArguments(args);

                if (!_events.TryGetValue(name, out var @event))
                    return null;

                var result = @event.Invoke(context);

                switch (result)
                {
                    case Task<bool> task:
                        return !task.IsCompleted ? null : (object) task.Result;
                    case Task<int> task:
                        return !task.IsCompleted ? null : (object) task.Result;
                    case Task _:
                        return null;
                    default:
                        return result;
                }
            };
        }

        private class Event
        {
            public readonly List<InvokerInfo> Invokers = new List<InvokerInfo>();

            public readonly List<Func<EventDelegate, EventDelegate>> Middleware =
                new List<Func<EventDelegate, EventDelegate>>();

            public EventDelegate Invoke;

            public Event(EventDelegate invoke)
            {
                Invoke = invoke;
            }
        }

        private class InvokerInfo
        {
            public Func<object, EventContext, object> Invoke;
            public Type TargetType;
        }
    }
}