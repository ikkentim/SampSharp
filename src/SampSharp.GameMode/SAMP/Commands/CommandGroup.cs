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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents a group of commands.
    /// </summary>
    public sealed class CommandGroup : Pool<CommandGroup>
    {
        /// <summary>
        ///     Initializes a new instance of the CommandGroup class.
        /// </summary>
        /// <param name="name">The name of the command group.</param>
        /// <param name="alias">An alias for the command group.</param>
        /// <param name="parentGroup">The parent command group of the command group.</param>
        public CommandGroup(string name, string alias = null, CommandGroup parentGroup = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            Alias = alias;
            ParentGroup = parentGroup;

            foreach (var cmd in Command.GetAll<DetectedCommand>().Where(c => c.Group == null))
            {
                var groupAttribute = cmd.Command.GetCustomAttribute<CommandGroupAttribute>();

                if (groupAttribute != null && groupAttribute.Group == CommandPath)
                {
                    cmd.Group = this;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the name of this CommandGroup.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the alias of this CommandGroup.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     Gets or sets the parent CommandGroup of this CommandGroup.
        /// </summary>
        public CommandGroup ParentGroup { get; set; }

        /// <summary>
        ///     Gets the paths to this command.
        /// </summary>
        public IEnumerable<string> CommandPaths
        {
            get
            {
                if (ParentGroup == null)
                {
                    yield return Name;

                    if (Alias != null)
                    {
                        yield return Alias;
                    }
                }
                else
                {
                    foreach (var str in ParentGroup.CommandPaths)
                    {
                        yield return string.Format("{0} {1}", str, Name);

                        if (Alias != null)
                        {
                            yield return string.Format("{0} {1}", str, Alias);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Gets the main path to this CommandGroup.
        /// </summary>
        public string CommandPath
        {
            get { return ParentGroup == null ? Name : string.Format("{0} {1}", ParentGroup.CommandPath, Name); }
        }

        /// <summary>
        ///     Initializes a new instance of the CommandGroup class.
        /// </summary>
        /// <param name="name">The name of the command group.</param>
        /// <param name="alias">An alias for the command group.</param>
        /// <param name="parentGroup">The parent command group of the command group.</param>
        /// <returns>The new CommandGroup instance.</returns>
        public static CommandGroup Register(string name, string alias = null, CommandGroup parentGroup = null)
        {
            var group =
                All.FirstOrDefault(
                    g =>
                        g.Name == name &&
                        (g.ParentGroup == parentGroup || (g.ParentGroup == null && parentGroup == null) ||
                         g.ParentGroup.CommandPath == parentGroup.CommandPath));

            if (group == null)
                return new CommandGroup(name, alias, parentGroup);

            group.Alias = alias ?? group.Alias;
            group.ParentGroup = parentGroup ?? group.ParentGroup;

            return group;
        }
    }
}