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

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>An attribute which indicates the method is invokable as a player command.</summary>
[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
public class PlayerCommandAttribute : Attribute, ICommandMethodInfo
{
    /// <summary>Initializes a new instance of the <see cref="PlayerCommandAttribute" /> class.</summary>
    public PlayerCommandAttribute()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="PlayerCommandAttribute" /> class.</summary>
    /// <param name="name">The overridden name of the command.</param>
    public PlayerCommandAttribute(string name)
    {
        Name = name;
    }

    /// <inheritdoc />
    public string Name { get; set; }
}