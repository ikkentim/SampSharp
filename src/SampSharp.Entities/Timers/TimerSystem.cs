using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SampSharp.Core.Logging;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents a system which invokes timers on every tick.
    /// </summary>
    public class TimerSystem : ITickingSystem, ITimerService
    {
        private static readonly TimeSpan LowInterval = TimeSpan.FromSeconds(1.0 / 50); // 10 server ticks
        private readonly IServiceProvider _serviceProvider;
        
        private readonly List<TimerInfo> _timers = new List<TimerInfo>();
        private long _lastTick;
        private bool _didInitialize;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerSystem" /> class.
        /// </summary>
        public TimerSystem(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public void Stop(TimerReference timer)
        {
            if (timer == null) throw new ArgumentNullException(nameof(timer));
            timer.Info.IsActive = false;
            _timers.Remove(timer.Info);
        }
        
        /// <inheritdoc />
        public TimerReference Start(Action<IServiceProvider> action, TimeSpan interval)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            if(!IsValidInterval(interval))
                throw new ArgumentOutOfRangeException(nameof(interval), interval, "The interval should be a nonzero positive value.");

            var invoker = new TimerInfo
            {
                Invoke = () => action(_serviceProvider),
                IsActive = true,
                IntervalTicks = interval.Ticks,
                NextTick = Stopwatch.GetTimestamp() + interval.Ticks
            };

            _timers.Add(invoker);

            return new TimerReference(invoker, null, null);
        }

        [Event]
        internal void OnGameModeInit()
        {
            _lastTick = Stopwatch.GetTimestamp();

            CreateTimersFromAssemblies(_lastTick);

            _didInitialize = true;
        }

        /// <inheritdoc />
        public void Tick()
        {
            if (!_didInitialize || _timers.Count == 0)
                return;

            var timestamp = Stopwatch.GetTimestamp();
            
            // Don't user foreach for performance reasons
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < _timers.Count; i++)
            {
                var timer = _timers[i];

                while ((timer.NextTick > _lastTick || timestamp < _lastTick) && timer.NextTick <= timestamp)
                {
                    try
                    {
                        timer.Invoke();
                    }
                    catch (Exception e)
                    {
                        CoreLog.Log(CoreLogLevel.Error, $"Timer threw an exception: {e}");
                    }

                    timer.NextTick += timer.IntervalTicks;
                }
            }

            _lastTick = timestamp;
        }
        
        internal static bool IsValidInterval(TimeSpan interval)
        {
            return interval >= TimeSpan.FromTicks(1);
        }

        private void CreateTimersFromAssemblies(long tick)
        {
            // Find methods with TimerAttribute in any ISystem in any assembly.
            var events = new AssemblyScanner()
                .IncludeAllAssemblies()
                .IncludeNonPublicMembers()
                .Implements<ISystem>()
                .ScanMethods<TimerAttribute>();

            // Create timer invokers and store timer info in registry.
            foreach (var (method, attribute) in events)
            {
                CoreLog.LogDebug("Adding timer on {0}.{1}.", method.DeclaringType, method.Name);

                if (!IsValidInterval(attribute.IntervalTimeSpan))
                {
                    CoreLog.Log(CoreLogLevel.Error, $"Timer {method} could not be registered the interval {attribute.IntervalTimeSpan} is invalid.");
                    continue;
                }

                var service = _serviceProvider.GetService(method.DeclaringType);

                if (service == null)
                {
                    CoreLog.Log(CoreLogLevel.Debug, "Skipping timer registration because service could not be loaded.");
                    continue;
                }

                var parameterInfos = method.GetParameters()
                    .Select(info => new MethodParameterSource(info){IsService = true})
                    .ToArray();

                var compiled = MethodInvokerFactory.Compile(method, parameterInfos);

                if (attribute.IntervalTimeSpan < LowInterval)
                {
                    CoreLog.Log(CoreLogLevel.Warning, $"Timer {method.DeclaringType}.{method.Name} has a low interval of {attribute.IntervalTimeSpan}.");
                }

                var timer = new TimerInfo
                {
                    IsActive = true,
                    Invoke = () => compiled(service, null, _serviceProvider, null),
                    IntervalTicks = attribute.IntervalTimeSpan.Ticks,
                    NextTick = tick + attribute.IntervalTimeSpan.Ticks
                };

                _timers.Add(timer);
            }
        }
    }
}
