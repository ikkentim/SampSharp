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

namespace SampSharp.Entities.SAMP.Commands
{
    /// <summary>
    /// An attribute which provides additional information a command parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CommandParameterAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets an override value for the name of the parameter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an override value for the parser of the parameter.
        /// </summary>
        public Type Parser { get; set; }
    }
}