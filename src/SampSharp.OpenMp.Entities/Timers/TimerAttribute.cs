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

using JetBrains.Annotations;

namespace SampSharp.Entities;

/// <summary>An attribute which indicates the method should be invoked at a specified interval.</summary>
[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
public class TimerAttribute : Attribute
{
    /// <summary>Initializes a new instance of the <see cref="TimerAttribute" /> class.</summary>
    /// <param name="interval">The interval of the timer.</param>
    public TimerAttribute(double interval)
    {
        Interval = interval;
    }

    /// <summary>Gets or sets the interval of the timer.</summary>
    public double Interval { get; set; }

    internal TimeSpan IntervalTimeSpan => TimeSpan.FromMilliseconds(Interval);
}