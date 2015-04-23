// SampSharp
// Copyright 2015 Tim Potze
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

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Indicates that a method is a player-command and specifies information about the command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        public CommandAttribute(string name)
        {
            Name = name;
            IgnoreCase = true;
        }

        /// <summary>
        ///     Gets or sets the name of this Command.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets whether this Command is case-sensitive.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        ///     Gets or sets an alias of this Command.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     Gets or sets a shortcut of this Command.
        /// </summary>
        /// <remarks>
        ///     A shortcut is the same as an alias, but without the commandgroup in front of it.
        /// </remarks>
        public string Shortcut { get; set; }

        /// <summary>
        ///     Gets or sets the method run to check whether a player has the permissions the run the command.
        /// </summary>
        public string PermissionCheckMethod { get; set; }
    }
}
