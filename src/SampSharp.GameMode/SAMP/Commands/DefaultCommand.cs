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
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.SAMP.Commands.Arguments;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    public class DefaultCommand : ICommand
    {
        private readonly string _displayName;

        public DefaultCommand(CommandPath[] names, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers,
            MethodInfo method, string usageMessage)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (names.Length == 0) throw new ArgumentException("Must contain at least 1 name", nameof(names));

            if (!IsValidCommandMethod(method))
                throw new ArgumentException("Method unsuitable as command", nameof(method));

            IsMethodMemberOfPlayer = typeof (BasePlayer).IsAssignableFrom(method.DeclaringType);

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
                        new CommandParameterInfo(p.Name, GetParameterType(p, index++, count), p.HasDefaultValue,
                            p.DefaultValue))
                .ToArray();
            PermissionCheckers = (permissionCheckers?.Where(p => p != null).ToArray() ?? new IPermissionChecker[0]);
        }

        public virtual CommandPath[] Names { get; }

        public virtual string DisplayName
        {
            get { return _displayName ?? Names.OrderByDescending(n => n.Length).First().FullName; }
        }

        public virtual bool IsCaseIgnored { get; }
        public MethodInfo Method { get; }
        public bool IsMethodMemberOfPlayer { get; }
        public virtual string UsageMessage { get; }
        public CommandParameterInfo[] Parameters { get; }
        public IPermissionChecker[] PermissionCheckers { get; }

        public static bool IsValidCommandMethod(MethodInfo method)
        {
            return (method.IsStatic && method.GetParameters().Length >= 1 &&
                    typeof (BasePlayer).IsAssignableFrom(method.GetParameters().First().ParameterType)) ||
                   (!method.IsStatic && typeof (BasePlayer).IsAssignableFrom(method.DeclaringType));
        }

        private bool GetArguments(string commandText, out object[] arguments)
        {
            arguments = new object[Parameters.Length];
            var index = 0;
            foreach (var parameter in Parameters)
            {
                object arg;
                if (!parameter.CommandParameterType.GetValue(ref commandText, out arg))
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

        protected virtual ICommandParameterType GetParameterType(ParameterInfo parameter, int index, int count)
        {
            var attribute = parameter.GetCustomAttribute<ArgumentTypeAttribute>();

            if (attribute != null && typeof (ICommandParameterType).IsAssignableFrom(attribute.Type))
                return Activator.CreateInstance(attribute.Type) as ICommandParameterType;

            if (parameter.ParameterType == typeof (string))
                return index == count - 1
                    ? (ICommandParameterType) new TextCommandParameterType()
                    : new WordCommandParameterType();

            if (parameter.ParameterType == typeof (int))
                return new IntegerCommandParameterType();

            if (parameter.ParameterType == typeof (float))
                return new FloatCommandParameterType();

            if (typeof (BasePlayer).IsAssignableFrom(parameter.ParameterType))
                return new PlayerCommandParameterType();

            if (parameter.ParameterType.IsEnum)
                return
                    Activator.CreateInstance(typeof (EnumCommandParameterType<>).MakeGenericType(parameter.ParameterType))
                        as ICommandParameterType;

            throw new Exception($"Parameter {parameter} has no type");
        }

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