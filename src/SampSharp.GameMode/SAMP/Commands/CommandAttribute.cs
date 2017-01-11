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
    ///     Indicates a method is a player command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        public CommandAttribute(string name) : this(new[] {name})
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandAttribute" /> class.
        /// </summary>
        /// <param name="names">The names of the command.</param>
        public CommandAttribute(params string[] names)
        {
            Names = names;
            IgnoreCase = true;
        }

        /// <summary>
        ///     Gets the names.
        /// </summary>
        public string[] Names { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether to ignore the case of the command.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        ///     Gets or sets the shortcut.
        /// </summary>
        public string Shortcut { get; set; }

        /// <summary>
        ///     Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the usage message.
        /// </summary>
        public string UsageMessage { get; set; }

        /// <summary>
        ///     Gets or sets the permission checker type.
        /// </summary>
        public Type PermissionChecker { get; set; }
    }
}
