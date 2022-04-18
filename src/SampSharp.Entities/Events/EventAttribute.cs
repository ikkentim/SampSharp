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

using System;
using SampSharp.Entities.Annotations;

namespace SampSharp.Entities;

/// <summary>
/// Indicates a method is to be invoked when an event occurs.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
public class EventAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the name of the event which should invoke the method. If this value is null, the method name is used as
    /// the event name.
    /// </summary>
    public string Name { get; set; }
}