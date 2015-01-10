// SampSharp
// Copyright (C) 2015 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

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

            foreach (DetectedCommand cmd in Command.GetAll<DetectedCommand>().Where(c => c.Group == null))
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
                    foreach (string str in ParentGroup.CommandPaths)
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
            return new CommandGroup(name, alias, parentGroup);
        }
    }
}