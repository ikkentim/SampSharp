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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents a command detected within an assembly.
    /// </summary>
    public sealed class DetectedCommand : Command
    {
        private readonly ParameterInfo[] _parameterInfos;

        static DetectedCommand()
        {
            UsageFormat = (name, parameters) =>
                string.Format("Usage: /{0}{1}{2}", name, parameters.Any() ? ": " : string.Empty,
                    string.Join(" ", parameters.Select(
                        p => p.Optional
                            ? string.Format("({0})", p.DisplayName)
                            : string.Format("[{0}]", p.DisplayName)
                        ))
                    );

            ResolveParameterType = (type, name) =>
            {
                if (type == typeof (int)) return new IntegerAttribute(name);
                if (type == typeof (string)) return new WordAttribute(name);
                if (type == typeof (float)) return new FloatAttribute(name);
                if (typeof (GtaPlayer).IsAssignableFrom(type)) return new PlayerAttribute(name);

                return type.IsEnum ? new EnumAttribute(name, type) : null;
            };
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DetectedCommand" /> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if command is null.</exception>
        /// <exception cref="System.ArgumentException">The given <paramref name="command" /> is not valid.</exception>
        public DetectedCommand(MethodInfo command)
        {
            if (command == null) throw new ArgumentNullException("command");

            if (!command.IsStatic && !typeof (GtaPlayer).IsAssignableFrom(command.DeclaringType))
                throw new ArgumentException("command must be static or member of GtaPlayer");

            _parameterInfos = command.GetParameters();

            var commandAttribute = command.GetCustomAttribute<CommandAttribute>();
            var groupAttribute = command.GetCustomAttribute<CommandGroupAttribute>();

            if (commandAttribute == null) throw new ArgumentException("method does not have CommandAttribute attached");

            if (groupAttribute != null)
                Group = CommandGroup.All.FirstOrDefault(g => g.CommandPath == groupAttribute.Group);


            Name = commandAttribute.Name;
            IgnoreCase = commandAttribute.IgnoreCase;
            Alias = commandAttribute.Alias;
            Shortcut = commandAttribute.Shortcut;
            Command = command;

            if (commandAttribute.PermissionChecker != null)
            {
                if (!typeof(IPermissionChecker).IsAssignableFrom(commandAttribute.PermissionChecker))
                {
                    throw new ArgumentException(commandAttribute.PermissionChecker + " should implement IPermissionChecker interface");
                }

                PermissionCheck = (IPermissionChecker) Activator.CreateInstance(commandAttribute.PermissionChecker);
            }

            ParameterInfo[] cmdParams = Command.GetParameters();

            if (Command.IsStatic &&
                (cmdParams.Length == 0 || !typeof (GtaPlayer).IsAssignableFrom(cmdParams[0].ParameterType)))
            {
                throw new ArgumentException("command " + Name + " does not accept a player as first parameter");
            }

            Parameters =
                Command.GetParameters()
                    .Skip(Command.IsStatic ? 1 : 0)
                    .Select(
                        parameter =>
                        {
                            /*
                             * Custom attributes on parameters are on the time of writing this not
                             * available in mono. When this is available, AttributeTargets of ParameterAttribute
                             * should be changed from Method to Parameter.
                             * 
                             * At the moment these attributes are attached to the method instead of the parameter.
                             */

                            ParameterAttribute attribute = Command.GetCustomAttributes<ParameterAttribute>()
                                .FirstOrDefault(a => a.Name == parameter.Name) ??
                                                           ResolveParameterType(parameter.ParameterType, parameter.Name);

                            attribute.Optional = attribute.Optional || parameter.HasDefaultValue;

                            return attribute;
                        }).
                    ToArray();


            if (Parameters.Contains(null))
            {
                throw new ArgumentException("command " + Name +
                                            " has a parameter of a unknown type without an attached attrubute");
            }
        }

        /// <summary>
        ///     Checks whether the provided <paramref name="commandText" /> starts with the right characters to run this command.
        /// </summary>
        /// <param name="commandText">
        ///     The command the player entered. When the command returns True, the referenced string will
        ///     only contain the command arguments.
        /// </param>
        /// <returns>
        ///     True when successful, False otherwise.
        /// </returns>
        public override int CommandTextMatchesCommand(ref string commandText)
        {
            commandText = commandText.Trim(' ');

            foreach (string str in CommandPaths.OrderByDescending(c => c.Count(h => h == ' ')))
            {
                if ((IgnoreCase && (commandText.ToLower() == str.ToLower() ||
                                    commandText.ToLower().StartsWith(str.ToLower() + " "))) ||
                    (commandText == str || commandText.StartsWith(str + " ")))
                {
                    commandText = commandText.Substring(str.Length);
                    return str.Split(' ').Length;
                }
            }

            return 0;
        }

        /// <summary>
        ///     Checks whether the <paramref name="commandText" /> contains all required arguments.
        /// </summary>
        /// <param name="commandText">The text to check.</param>
        /// <returns>True if all required arguments are present; False otherwise.</returns>
        public override bool AreArgumentsValid(string commandText)
        {
            for (int paramIndex = 0; paramIndex < Parameters.Length; paramIndex++)
            {
                ParameterAttribute parameterAttribute = Parameters[paramIndex];

                commandText = commandText.Trim();

                /*
                 * Check for missing optional parameters. This is obviously allowed.
                 */
                if (commandText.Length == 0 && parameterAttribute.Optional)
                {
                    continue;
                }

                object argument;
                if (commandText.Length == 0 || !parameterAttribute.Check(ref commandText, out argument))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Checks whether the given player has the permission to run this command.
        /// </summary>
        /// <param name="player">The player that attempts to run this command.</param>
        /// <returns>
        ///     True when allowed, False otherwise.
        /// </returns>
        public override bool HasPlayerPermissionForCommand(GtaPlayer player)
        {
            return PermissionCheck == null || PermissionCheck.Check(player);
        }

        /// <summary>
        ///     Runs the command.
        /// </summary>
        /// <param name="player">The player running the command.</param>
        /// <param name="args">The arguments the player entered.</param>
        /// <returns>
        ///     True when the command has been executed, False otherwise.
        /// </returns>
        public override bool RunCommand(GtaPlayer player, string args)
        {
            if (!HasPlayerPermissionForCommand(player))
            {
                if (PermissionCheck.Message == null)
                {
                    // If the message is null, we return false so samp will think that this method doesn't exists
                    // and will print the default message
                    return false;
                }

                player.SendClientMessage(PermissionCheck.Message);
                return true;
            }

            var arguments = new List<object>();

            if (Command.IsStatic) arguments.Add(player);

            for (int paramIndex = 0; paramIndex < Parameters.Length; paramIndex++)
            {
                ParameterInfo parameterInfo = _parameterInfos[paramIndex + (Command.IsStatic ? 1 : 0)];
                ParameterAttribute parameterAttribute = Parameters[paramIndex];

                args = args.Trim();

                /*
                 * Check for missing optional parameters. This is obviously allowed.
                 */
                if (args.Length == 0 && parameterAttribute.Optional)
                {
                    arguments.Add(parameterInfo.DefaultValue);
                    continue;
                }

                object argument;
                if (args.Length == 0 || !parameterAttribute.Check(ref args, out argument))
                {
                    if (UsageFormat != null)
                    {
                        player.SendClientMessage(UsageFormat(CommandPath, Parameters));
                        return true;
                    }

                    return false;
                }

                arguments.Add(argument);
            }

            object result = Command.Invoke(Command.IsStatic ? null : player, arguments.ToArray());

            return Command.ReturnType == typeof (void) || (bool) result;
        }

        #region Properties

        /// <summary>
        ///     Gets the alias.
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        ///     Gets or sets the shortcut.
        /// </summary>
        public string Shortcut { get; set; }

        /// <summary>
        ///     Gets or sets the command group.
        /// </summary>
        public CommandGroup Group { get; set; }

        /// <summary>
        ///     Gets the command method.
        /// </summary>
        public MethodInfo Command { get; private set; }

        /// <summary>
        ///     Gets the permission check method.
        /// </summary>
        public IPermissionChecker PermissionCheck { get; private set; }

        /// <summary>
        ///     Gets the parameters.
        /// </summary>
        public ParameterAttribute[] Parameters { get; private set; }

        /// <summary>
        ///     Gets the command paths.
        /// </summary>
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
                    foreach (string str in Group.CommandPaths)
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
        ///     Gets the command path.
        /// </summary>
        public string CommandPath
        {
            get { return Group == null ? Name : string.Format("{0} {1}", Group.CommandPath, Name); }
        }

        /// <summary>
        ///     Gets or sets the usage message send when a wrongly formatted command is being processed.
        /// </summary>
        public static Func<string, ParameterAttribute[], string> UsageFormat { get; set; }

        /// <summary>
        ///     Gets or sets the method the find the parameter type of a parameter when no attribute was
        ///     attached to the parameter.
        /// </summary>
        public static Func<Type, string, ParameterAttribute> ResolveParameterType { get; set; }

        #endregion
    }
}