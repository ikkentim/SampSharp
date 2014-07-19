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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    public sealed class DetectedCommand : Command
    {
        private static Func<string, ParameterAttribute[], string> _usageFormat = (name, parameters) =>
            string.Format("Usage: /{0}{1}{2}", name, parameters.Any() ? ": " : string.Empty,
                string.Join(" ", parameters.Select(
                    p => p.Optional
                        ? string.Format("({0})", p.DisplayName)
                        : string.Format("[{0}]", p.DisplayName)
                    ))
                );


        public DetectedCommand(MethodInfo command)
        {
            if (command == null)
            {
                throw new NullReferenceException("command cannot be null");
            }

            var commandAttribute = command.GetCustomAttribute<CommandAttribute>();
            var groupAttribute = command.GetCustomAttribute<CommandGroupAttribute>();
            if (commandAttribute == null)
            {
                throw new ArgumentException("method does not have CommandAttribute attached");
            }

            if (groupAttribute != null)
            {
                Group = CommandGroup.All.FirstOrDefault(g => g.CommandPath == groupAttribute.Group);
            }

            Name = commandAttribute.Name;
            Alias = commandAttribute.Alias;
            Shortcut = commandAttribute.Shortcut;
            Command = command;
            PermissionCheck = commandAttribute.PermissionCheckMethod == null
                ? null
                : command.DeclaringType.GetMethods()
                    .FirstOrDefault(m => m.IsStatic && m.Name == commandAttribute.PermissionCheckMethod);

            if (PermissionCheck != null)
            {
                var parms = PermissionCheck.GetParameters();
                if (parms.Length != 1 || parms[0].ParameterType != typeof (Player))
                {
                    throw new ArgumentException("PermissionCheckMethod of " + Name +
                                                " does not take a Player as parameter");
                }

                if (PermissionCheck.ReturnType != typeof (bool))
                {
                    throw new ArgumentException("PermissionCheckMethod of " + Name + " does not return a boolean");
                }
            }

            if (Command.ReturnType != typeof (bool))
            {
                throw new ArgumentException("command " + Name + " does not return a boolean");
            }
        }

        public override string Name { get; protected set; }
        public string Alias { get; private set; }
        public string Shortcut { get; set; }
        public CommandGroup Group { get; set; }
        public MethodInfo Command { get; private set; }
        public MethodInfo PermissionCheck { get; private set; }

        public IEnumerable<string> CommandPaths
        {
            get
            {
                if (Shortcut != null)
                {
                    yield return Shortcut;
                }

                if (Group == null)
                {
                    yield return Name;

                    if (Alias != null)
                    {
                        yield return Alias;
                    }
                }
                else
                {
                    foreach (var str in Group.CommandPaths)
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
            get { return Group == null ? Name : string.Format("{0} {1}", Group.CommandPath, Name); }
        }

        /// <summary>
        ///     Gets or sets the usage message send when a wrongly formatted command is being processed.
        /// </summary>
        public static Func<string, ParameterAttribute[], string> UsageFormat
        {
            get { return _usageFormat; }
            set { _usageFormat = value; }
        }

        public override bool CommandTextMatchesCommand(ref string commandText)
        {
            commandText = commandText.Trim(' ');

            foreach (var str in CommandPaths)
            {
                if (commandText == str)
                {
                    commandText = string.Empty;
                    return true;
                }

                if (commandText.StartsWith(str + " "))
                {
                    commandText = commandText.Substring(str.Length);
                    return true;
                }
            }

            return false;
        }

        public override bool HasPlayerPermissionForCommand(Player player)
        {
            return PermissionCheck == null || (bool) PermissionCheck.Invoke(null, new object[] {player});
        }

        public override bool RunCommand(Player player, string args)
        {
            var arguments = new List<object>();

            /*
             * Custom attributes on parameters are on the time of writing this not
             * available in mono. When this is available, AttributeTargets of ParameterAttribute
             * should be changed from Method to Parameter.
             */

            //TODO: Check for newer version of the mono runtime with implementation of AttributeTargets.Parameter

            foreach (ParameterInfo parameter in Command.GetParameters().Skip(1))
            {
                args = args.Trim();
                object argument;

                var attr =
                    Command.GetCustomAttributes<ParameterAttribute>().FirstOrDefault(a => a.Name == parameter.Name);

                if (attr == null)
                {
                    /*
                     * Skip command when a parameter is missing an attribute.
                     */
                    return false;
                }
                /*
                 * Check for missing optional parameters. This is obviously allowed.
                 */
                if (args.Length == 0 && attr.Optional)
                {
                    arguments.Add(parameter.DefaultValue);
                    continue;
                }

                if (args.Length == 0 || !attr.Check(ref args, out argument))
                {
                    if (UsageFormat != null)
                    {
                        player.SendClientMessage(Color.White,
                            UsageFormat(Name,
                                Command.GetParameters()
                                    .Skip(1) // Skip 'sender' parameter
                                    .Select(p => p.GetCustomAttribute<ParameterAttribute>())
                                    .ToArray()));
                        return true;
                    }

                    return false;
                }

                arguments.Add(argument);
            }


            arguments.Insert(0, player);

            return (bool) Command.Invoke(null, arguments.ToArray());
        }
    }
}