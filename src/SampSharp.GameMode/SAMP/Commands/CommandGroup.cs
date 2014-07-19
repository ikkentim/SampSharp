// SampSharp
// Copyright (C) 2014 Tim Potze
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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.SAMP.Commands
{
    public sealed class CommandGroup : Pool<CommandGroup>
    {
        public CommandGroup(string name)
        {
            Name = name;

            foreach (var cmd in Command.GetAll<DetectedCommand>().Where(c => c.Group == null))
            {
                var groupAttribute = cmd.Command.GetCustomAttribute<CommandGroupAttribute>();

                if (groupAttribute != null && groupAttribute.Group == CommandPath)
                {
                    cmd.Group = this;
                }
            }
        }

        public CommandGroup(string name, string alias) : this(name)
        {
            Alias = alias;
        }

        public CommandGroup(string name, string alias, CommandGroup commandGroup)
            : this(name, alias)
        {
            ParentGroup = commandGroup;
        }

        public string Name { get; set; }

        public string Alias { get; set; }

        public CommandGroup ParentGroup { get; set; }

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

        public string CommandPath
        {
            get { return ParentGroup == null ? Name : string.Format("{0} {1}", ParentGroup.CommandPath, Name); }
        }

        public static CommandGroup Register(string name)
        {
            return new CommandGroup(name);
        }

        public static CommandGroup Register(string name, string alias)
        {
            return new CommandGroup(name, alias);
        }


        public static CommandGroup Register(string name, CommandGroup parentGroup)
        {
            return new CommandGroup(name, null, parentGroup);
        }

        public static CommandGroup Register(string name, string alias, CommandGroup commandGroup)
        {
            return new CommandGroup(name, alias, commandGroup);
        }
    }
}