// SampSharp
// Copyright 2017 Tim Potze
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
using SampSharp.GameMode.SAMP.Commands.PermissionCheckers;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Indicates commands within this class or method are part of a command group.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CommandGroupAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandGroupAttribute" /> class.
        /// </summary>
        /// <param name="paths">The relative paths of the command group.</param>
        public CommandGroupAttribute(params string[] paths)
        {
            Paths = paths;
        }

        /// <summary>
        /// Gets the relative paths of the command group.
        /// </summary>
        public string[] Paths { get; }

        /// <summary>
        ///     Gets or sets the permission checker type.
        /// </summary>
        public Type PermissionChecker { get; set; }
    }
}