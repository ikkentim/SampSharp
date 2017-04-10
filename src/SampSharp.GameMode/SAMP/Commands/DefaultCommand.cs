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
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.SAMP.Commands.Parameters;
using SampSharp.GameMode.SAMP.Commands.ParameterTypes;
using SampSharp.GameMode.SAMP.Commands.PermissionCheckers;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents the default command based on a method with command attributes.
    /// </summary>
    public class DefaultCommand : ICommand
    {
        private readonly string _displayName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultCommand" /> class.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case of the command.</param>
        /// <param name="permissionCheckers">The permission checkers.</param>
        /// <param name="method">The method.</param>
        /// <param name="usageMessage">The usage message.</param>
        public DefaultCommand(CommandPath[] names, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers,
            MethodInfo method, string usageMessage)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (names.Length == 0) throw new ArgumentException("Must contain at least 1 name", nameof(names));

            if (!IsValidCommandMethod(method))
                throw new ArgumentException("Method unsuitable as command", nameof(method));

            IsMethodMemberOfPlayer = typeof (BasePlayer).GetTypeInfo().IsAssignableFrom(method.DeclaringType);

            var skipCount = IsMethodMemberOfPlayer ? 0 : 1;
            var index = 0;
            var count = method.GetParameters().Length - skipCount;

            Names = names;
            _displayName = displayName;
            IsCaseIgnored = ignoreCase;
            Method = method;
            UsageMessage = string.IsNullOrWhiteSpace(usageMessage) ? null : usageMessage;
            Parameters = Method.GetParameters()
                .Skip(skipCount)
                .Select(
                    p =>
                    {
                        var type = GetParameterType(p, index++, count);
                        return type == null
                            ? null
                            : new CommandParameterInfo(p.Name, type, p.HasDefaultValue, p.DefaultValue);
                    })
                .ToArray();

            if (Parameters.Any(v => v == null))
            {
                throw new ArgumentException("Method has parameter of unknown type", nameof(method));
            }

            PermissionCheckers = (permissionCheckers?.Where(p => p != null).ToArray() ?? new IPermissionChecker[0]);
        }

        /// <summary>
        ///     Gets the names.
        /// </summary>
        public virtual CommandPath[] Names { get; }

        /// <summary>
        ///     Gets the display name.
        /// </summary>
        public virtual string DisplayName
        {
            get { return _displayName ?? Names.OrderByDescending(n => n.Length).First().FullName; }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance ignores the case of the command.
        /// </summary>
        public virtual bool IsCaseIgnored { get; }

        /// <summary>
        ///     Gets the method.
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is method member of player.
        /// </summary>
        public bool IsMethodMemberOfPlayer { get; }

        /// <summary>
        ///     Gets the usage message.
        /// </summary>
        public virtual string UsageMessage { get; }

        /// <summary>
        ///     Gets the parameters.
        /// </summary>
        public CommandParameterInfo[] Parameters { get; }

        /// <summary>
        ///     Gets the permission checkers.
        /// </summary>
        public IPermissionChecker[] PermissionCheckers { get; }

        /// <summary>
        ///     Determines whether the specified method is a valid command method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>true if valid; false otherwise.</returns>
        public static bool IsValidCommandMethod(MethodInfo method)
        {
            return (method.IsStatic && method.GetParameters().Length >= 1 &&
                    typeof (BasePlayer).GetTypeInfo().IsAssignableFrom(method.GetParameters().First().ParameterType)) ||
                   (!method.IsStatic && typeof (BasePlayer).GetTypeInfo().IsAssignableFrom(method.DeclaringType));
        }

        private bool GetArguments(string commandText, out object[] arguments)
        {
            arguments = new object[Parameters.Length];
            var index = 0;
            foreach (var parameter in Parameters)
            {
                object arg;
                if (!parameter.CommandParameterType.Parse(ref commandText, out arg))
                {
                    if (!parameter.IsOptional)
                        return false;

                    arguments[index] = parameter.DefaultValue;
                }
                else
                {
                    arguments[index] = arg;
                }

                index++;
            }

            return true;
        }

        /// <summary>
        ///     Gets the type of the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>The type of the parameter.</returns>
        protected virtual ICommandParameterType GetParameterType(ParameterInfo parameter, int index, int count)
        {
            var attribute = parameter.GetCustomAttribute<ParameterAttribute>();

            if (attribute != null && typeof (ICommandParameterType).GetTypeInfo().IsAssignableFrom(attribute.Type))
                return Activator.CreateInstance(attribute.Type) as ICommandParameterType;

            if (parameter.ParameterType == typeof (string))
                return index == count - 1
                    ? (ICommandParameterType) new TextType()
                    : new WordType();

            if (parameter.ParameterType == typeof (int))
                return new IntegerType();

            if (parameter.ParameterType == typeof (float))
                return new FloatType();

            if (typeof (BasePlayer).GetTypeInfo().IsAssignableFrom(parameter.ParameterType))
                return new PlayerType();

            if (parameter.ParameterType.GetTypeInfo().IsEnum)
                return
                    Activator.CreateInstance(typeof (EnumType<>).MakeGenericType(parameter.ParameterType))
                        as ICommandParameterType;

            return null;
        }

        /// <summary>
        /// Sends the usage message to the specified <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>True on success, false otherwise.</returns>
        protected virtual bool SendUsageMessage(BasePlayer player)
        {
            if (UsageMessage == null)
            {
                player.SendClientMessage($"Usage: {this}");
                return true;
            }

            player.SendClientMessage(UsageMessage);
            return true;
        }

        /// <summary>
        ///     Sends the permission denied message for the specified permission checker.
        /// </summary>
        /// <param name="permissionChecker">The permission checker.</param>
        /// <param name="player">The player.</param>
        /// <returns>true on success; false otherwise.</returns>
        protected virtual bool SendPermissionDeniedMessage(IPermissionChecker permissionChecker, BasePlayer player)
        {
            if (permissionChecker == null) throw new ArgumentNullException(nameof(permissionChecker));
            if (player == null) throw new ArgumentNullException(nameof(player));

            if (permissionChecker.Message == null)
                return false;

            player.SendClientMessage(permissionChecker.Message);
            return true;
        }

        #region Overrides of Object

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            var name = "/" + DisplayName;

            if (Parameters.Any())
                return name + " " +
                       string.Join(" ", Parameters.Select(p => p.IsOptional ? $"<{p.Name}>" : $"[{p.Name}]"));
            return name;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        ///     Determines whether this instance can be invoked by the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns>A value indicating whether this instance can be invoked.</returns>
        public virtual CommandCallableResponse CanInvoke(BasePlayer player, string commandText)
        {
            if (PermissionCheckers.Where(p => p.Message == null).Any(p => !p.Check(player)))
                return CommandCallableResponse.False;

            commandText = commandText.TrimStart('/');

            foreach (var name in Names)
            {
                if (!name.Matches(commandText, IsCaseIgnored)) continue;

                commandText = commandText.Substring(name.Length);
                object[] tmp;
                return GetArguments(commandText, out tmp)
                    ? CommandCallableResponse.True
                    : CommandCallableResponse.Optional;
            }

            return CommandCallableResponse.False;
        }

        /// <summary>
        ///     Invokes this command.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns>true on success; false otherwise.</returns>
        public virtual bool Invoke(BasePlayer player, string commandText)
        {
            foreach (var p in PermissionCheckers)
            {
                if (!p.Check(player))
                    return SendPermissionDeniedMessage(p, player);
            }

            commandText = commandText.TrimStart('/');

            object[] arguments;
            var name = Names.Cast<CommandPath?>().FirstOrDefault(n => n != null && n.Value.Matches(commandText));

            if (name == null)
                return false;

            commandText = commandText.Substring(name.Value.Length).Trim();

            if (!GetArguments(commandText, out arguments))
                return SendUsageMessage(player);

            var result = Method.Invoke(IsMethodMemberOfPlayer ? player : null,
                IsMethodMemberOfPlayer ? arguments : new object[] {player}.Concat(arguments).ToArray());

            return result as bool? ?? true;
        }

        #endregion
    }
}