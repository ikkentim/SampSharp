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

using System.Reflection;

namespace SampSharp.Entities;

/// <summary>Provides extended methods for <see cref="ITimerService" />.</summary>
public static class TimerServiceExtensions
{
    /// <summary>
    /// Starts a timer with the specified <paramref name="interval" />. The specified <paramref name="method" /> will be invoked on the specified
    /// <paramref name="target" /> each timer tick.
    /// </summary>
    /// <param name="timerService">The timer service.</param>
    /// <param name="target">The target on which to tick.</param>
    /// <param name="method">The method to invoke each timer tick.</param>
    /// <param name="interval">The interval at which to tick.</param>
    /// <returns>A reference to the started timer.</returns>
    public static TimerReference Start(this ITimerService timerService, object target, MethodInfo method, TimeSpan interval)
    {
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(method);

        if (!TimerSystem.IsValidInterval(interval))
        {
            throw new ArgumentOutOfRangeException(nameof(interval), interval, "The interval should be a nonzero positive value.");
        }

        if (!method.DeclaringType!.IsInstanceOfType(target))
        {
            throw new ArgumentException("The specified method is not a member of the specified target", nameof(method));
        }

        var parameterInfos = method.GetParameters()
            .Select(info => new MethodParameterSource(info) { IsService = true })
            .ToArray();

        var compiled = MethodInvokerFactory.Compile(method, parameterInfos);

        return timerService.Start(serviceProvider => compiled(target, null, serviceProvider, null), interval);
    }
}