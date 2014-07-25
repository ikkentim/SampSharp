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

        private static Func<Type, string, ParameterAttribute> _resolveParameterType = (type, name) =>
        {
            if (type == typeof (int)) return new IntegerAttribute(name);
            if (type == typeof (string)) return new WordAttribute(name);
            if (type == typeof(float)) return null; //new FloatAttribute(name);
            if (typeof(Player).IsAssignableFrom(type)) return new PlayerAttribute(name);
            
            return type.IsEnum ? new EnumAttribute(name, type) : null;
        }; 

        public DetectedCommand(MethodInfo command, bool ignoreCase)
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
            IgnoreCase = ignoreCase;
            Alias = commandAttribute.Alias;
            Shortcut = commandAttribute.Shortcut;
            Command = command;
            PermissionCheck = commandAttribute.PermissionCheckMethod == null
                ? null
                : command.DeclaringType.GetMethods()
                    .FirstOrDefault(m => m.IsStatic && m.Name == commandAttribute.PermissionCheckMethod);

            if (PermissionCheck != null)
            {
                var permParams = PermissionCheck.GetParameters();
                if (permParams.Length != 1 || !typeof(Player).IsAssignableFrom(permParams[0].ParameterType))
                {
                    throw new ArgumentException("PermissionCheckMethod of " + Name +
                                                " does not take a Player as parameter");
                }

                if (PermissionCheck.ReturnType != typeof (bool))
                {
                    throw new ArgumentException("PermissionCheckMethod of " + Name + " does not return a boolean");
                }
            }

            var cmdParams = Command.GetParameters();

            if (cmdParams.Length == 0 || !typeof(Player).IsAssignableFrom(cmdParams[0].ParameterType))
            {
                throw new ArgumentException("command "+ Name + " does not accept a player as first parameter");
            }

            if (Command.ReturnType != typeof (bool))
            {
                throw new ArgumentException("command " + Name + " does not return a boolean");
            }

            Parameters =
                Command.GetParameters()
                    .Skip(1)
                    .Select(
                        parameter =>
                            /*
                             * Custom attributes on parameters are on the time of writing this not
                             * available in mono. When this is available, AttributeTargets of ParameterAttribute
                             * should be changed from Method to Parameter.
                             * 
                             * At the moment these attributes are attached to the method instead of the parameter.
                             */
                            Command.GetCustomAttributes<ParameterAttribute>()
                                .FirstOrDefault(a => a.Name == parameter.Name) ??
                            ResolveParameterType(parameter.ParameterType, parameter.Name)).ToArray();

            if (Parameters.Contains(null))
            {
                throw new ArgumentException("command " + Name + " has a parameter of a unknown type without an attached attrubute");
            }
        }

        #region Properties

        public string Alias { get; private set; }
        public string Shortcut { get; set; }
        public CommandGroup Group { get; set; }
        public MethodInfo Command { get; private set; }
        public MethodInfo PermissionCheck { get; private set; }
        public ParameterAttribute[] Parameters { get; private set; }

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

        /// <summary>
        ///     Gets or sets the metod the find the parameter type of a parameter when no attribute was
        ///     attached to the parameter.
        /// </summary>
        public static Func<Type, string, ParameterAttribute> ResolveParameterType
        {
            get { return _resolveParameterType; }
            set { _resolveParameterType = value; }
        }

        #endregion

        public override bool CommandTextMatchesCommand(ref string commandText)
        {
            commandText = commandText.Trim(' ');

            foreach (var str in CommandPaths)
            {
                if (commandText == str || (IgnoreCase && commandText.ToLower() == str.ToLower()))
                {
                    commandText = string.Empty;
                    return true;
                }

                if (commandText.StartsWith(str + " ") ||
                    (IgnoreCase && commandText.ToLower().StartsWith(str.ToLower() + " ")))
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
            var arguments = new List<object>
            {
                player
            };
            var idx = 0;

            foreach (ParameterInfo parameter in Command.GetParameters().Skip(1))
            {
                args = args.Trim();
                object argument;

                var attr = Parameters[idx++];

                /*
                 * Check for missing optional parameters. This is obviously allowed.
                 */
                if (args.Length == 0 && attr.Optional)
                {
                    arguments.Add(attr.DefaultValue);
                    continue;
                }

                if (args.Length == 0 || !attr.Check(ref args, out argument))
                {
                    if (UsageFormat != null)
                    {
                        player.SendClientMessage(Color.White, UsageFormat(Name, Parameters));
                        return true;
                    }

                    return false;
                }

                arguments.Add(argument);
            }

            return (bool) Command.Invoke(null, arguments.ToArray());
        }
    }
}