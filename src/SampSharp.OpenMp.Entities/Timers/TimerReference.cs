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
using System.Reflection;

namespace SampSharp.Entities;

/// <summary>Represents a reference to an interval or timeout.</summary>
public class TimerReference
{
    internal TimerReference(TimerInfo info, object? target, MethodInfo? method)
    {
        Info = info;
        Target = target;
        Method = method;
    }

    /// <summary>Gets the time span until the next tick of this timer.</summary>
    public TimeSpan NextTick => new(Info.NextTick - Stopwatch.GetTimestamp());

    internal TimerInfo Info { get; set; }

    /// <summary>Gets a value indicating whether the timer is active.</summary>
    public bool IsActive => Info.IsActive;

    /// <summary>Gets the target on which the timer is invoked.</summary>
    public object? Target { get; }

    /// <summary>Gets the method to be invoked with this timer.</summary>
    public MethodInfo? Method { get; }
}