// SampSharp
// Copyright 2022 Tim Potze
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

using System.Diagnostics;
using Microsoft.Extensions.Logging;
using SampSharp.Entities.Utilities;
using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

internal class TimerSystem : ITickingSystem, ITimerService
{
    private static readonly TimeSpan _lowIntervalThreshold = TimeSpan.FromSeconds(1.0 / 50); // 50Hz

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TimerSystem> _logger;
    private readonly ISystemRegistry _systemRegistry;

    private readonly List<TimerInfo> _timers = [];
    private long _lastTick;

    public TimerSystem(IServiceProvider serviceProvider, ILogger<TimerSystem> logger, ISystemRegistry systemRegistry)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _systemRegistry = systemRegistry;
        
        _lastTick = Stopwatch.GetTimestamp();
        
        systemRegistry.Register(CreateTimersFromAssemblies);
    }

    public TimerReference Delay(Action<IServiceProvider> action, TimeSpan delay)
    {
        return Start((sp, reference) =>
        {
            Stop(reference);
            action(sp);
        }, delay);
    }

    public void Stop(TimerReference timer)
    {
        ArgumentNullException.ThrowIfNull(timer);
        timer.Info.IsActive = false;
        _timers.Remove(timer.Info);
    }

    public TimerReference Start(Action<IServiceProvider> action, TimeSpan interval)
    {
        return Start((sp, _) => action(sp), interval);
    }

    public TimerReference Start(Action<IServiceProvider, TimerReference> action, TimeSpan interval)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (!IsValidInterval(interval))
        {
            throw new ArgumentOutOfRangeException(nameof(interval), interval, "The interval should be a nonzero positive value.");
        }

        var invoker = new TimerInfo(intervalTicks: interval.Ticks, nextTick: Stopwatch.GetTimestamp() + interval.Ticks, invoke: null!, true);

        var reference = new TimerReference(invoker, action.Target, action.Method);

        invoker.Invoke = () => action(_serviceProvider, reference);
        invoker.Reference = reference;

        _timers.Add(invoker);

        return reference;
    }

    public void Tick()
    {
        if (_timers.Count == 0)
        {
            return;
        }

        var timestamp = Stopwatch.GetTimestamp();

        // Don't user foreach for performance reasons
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < _timers.Count; i++)
        {
            var timer = _timers[i];

            while (timer.NextTick <= timestamp)
            {
                try
                {
                    timer.Invoke();
                }
                catch (Exception ex)
                {
                    var context = "timer";

                    if (timer.Reference?.Method != null)
                    {
                        context = $"timer {timer.Reference.Method.DeclaringType}.{timer.Reference.Method.Name}";
                    }

                    SampSharpExceptionHandler.HandleException(context, ex);
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

    private void CreateTimersFromAssemblies()
    {
        var tick = _lastTick;
        // Find methods with TimerAttribute in any loaded system.
        var events = ClassScanner.Create()
            .IncludeTypes(_systemRegistry.GetSystemTypes().Span)
            .IncludeNonPublicMembers()
            .Implements<ISystem>()
            .ScanMethods<TimerAttribute>();

        // Create timer invokers and store timer info in registry.
        foreach (var (target, method, attribute) in events)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Adding timer on {type}.{method}.", target, method.Name);
            }
         
            if (!IsValidInterval(attribute.IntervalTimeSpan))
            {
                _logger.LogError("Timer {method} could not be registered the interval {interval} is invalid.", method, attribute.IntervalTimeSpan);
                continue;
            }

            var service = _serviceProvider.GetService(target);

            if (service == null)
            {
                _logger.LogDebug("Skipping timer registration because the service could not be loaded.");
                continue;
            }

            var parameterInfos = method.GetParameters()
                .Select(info => new MethodParameterSource(info) { IsService = true })
                .ToArray();

            var compiled = MethodInvokerFactory.Compile(method, parameterInfos);

            if (attribute.IntervalTimeSpan < _lowIntervalThreshold)
            {
                _logger.LogWarning("Timer {type}.{method} has a low interval of {interval}.", target, method.Name, attribute.IntervalTimeSpan);
            }

            var timer = new TimerInfo(
                intervalTicks: attribute.IntervalTimeSpan.Ticks, 
                nextTick: tick + attribute.IntervalTimeSpan.Ticks, 
                invoke: () => compiled(service, null, _serviceProvider, null), 
                isActive: true);

            timer.Reference = new TimerReference(timer, service, method);

            _timers.Add(timer);
        }
    }
}