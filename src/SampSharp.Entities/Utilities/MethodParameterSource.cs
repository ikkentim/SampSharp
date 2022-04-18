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

namespace SampSharp.Entities.Utilities;

/// <summary>
/// Provides information about the origin of a parameter of a method.
/// </summary>
public class MethodParameterSource
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MethodParameterSource" /> class.
    /// </summary>
    /// <param name="info">The parameter information.</param>
    public MethodParameterSource(ParameterInfo info)
    {
        Info = info;
    }

    /// <summary>
    /// Gets the parameter information.
    /// </summary>
    public ParameterInfo Info { get; }

    /// <summary>
    /// The index in the arguments array which contains the value for this parameter. A value of -1 indicates this parameter is
    /// not supplied by the arguments array.
    /// </summary>
    public int ParameterIndex { get; set; } = -1;

    /// <summary>
    /// Gets or sets a value indicating whether the value of this parameter is a service which should be retrieved from the
    /// service provider.
    /// </summary>
    public bool IsService { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this value of this parameter is a component which should be retrieved of the
    /// entity provided in the arguments array.
    /// </summary>
    public bool IsComponent { get; set; }
}