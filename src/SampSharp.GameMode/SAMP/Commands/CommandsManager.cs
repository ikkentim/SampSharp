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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SampSharp.Core.Logging;
using SampSharp.GameMode.SAMP.Commands.PermissionCheckers;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents the default commands manager.
    /// </summary>
    public class CommandsManager : ICommandsManager
    {
        private static readonly Type[] SupportedReturnTypes = {typeof (bool), typeof (void)};
        private readonly List<ICommand> _commands = new List<ICommand>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsManager"/> class.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CommandsManager(BaseMode gameMode)
        {
            if (gameMode == null) throw new ArgumentNullException(nameof(gameMode));
            GameMode = gameMode;
        }

        #region Implementation of IService

        /// <summary>
        ///     Gets the game mode.
        /// </summary>
        public BaseMode GameMode { get; }

        #endregion

        /// <summary>
        ///     Registers the specified command.
        /// </summary>
        /// <param name="commandPaths">The command paths.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case of the command.</param>
        /// <param name="permissionCheckers">The permission checkers.</param>
        /// <param name="method">The method.</param>
        /// <param name="usageMessage">The usage message.</param>
        public virtual void Register(CommandPath[] commandPaths, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
        {
            Register(CreateCommand(commandPaths, displayName, ignoreCase, permissionCheckers, method,
                usageMessage));
        }

        /// <summary>
        ///     Creates a command.
        /// </summary>
        /// <param name="commandPaths">The command paths.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case the command.</param>
        /// <param name="permissionCheckers">The permission checkers.</param>
        /// <param name="method">The method.</param>
        /// <param name="usageMessage">The usage message.</param>
        /// <returns>The created command</returns>
        protected virtual ICommand CreateCommand(CommandPath[] commandPaths, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
        {
            return new DefaultCommand(commandPaths, displayName, ignoreCase, permissionCheckers, method, usageMessage);
        }

        private static IPermissionChecker CreatePermissionChecker(Type type)
        {
            if (type == null || !typeof(IPermissionChecker).GetTypeInfo().IsAssignableFrom(type))
                return null;

            return Activator.CreateInstance(type) as IPermissionChecker;
        }
        private static IEnumerable<IPermissionChecker> GetCommandPermissionCheckers(Type type)
        {
            if (type == null || type == typeof(object))
                yield break;

            foreach (var permissionChecker in GetCommandPermissionCheckers(type.DeclaringType))
                yield return permissionChecker;

            foreach (
                var permissionChecker in
                type.GetTypeInfo()
                    .GetCustomAttributes<CommandGroupAttribute>()
                    .Select(a => a.PermissionChecker)
                    .Distinct()
                    .Select(CreatePermissionChecker)
                    .Where(c => c != null))
                yield return permissionChecker;
        }

        private static IEnumerable<IPermissionChecker> GetCommandPermissionCheckers(MethodInfo method)
        {
            foreach (var permissionChecker in GetCommandPermissionCheckers(method.DeclaringType))
                yield return permissionChecker;

            foreach (
                var permissionChecker in
                method.GetCustomAttributes<CommandGroupAttribute>()
                    .Select(a => a.PermissionChecker)
                    .Concat(method.GetCustomAttributes<CommandAttribute>()
                        .Select(a => a.PermissionChecker))
                    .Distinct()
                    .Select(CreatePermissionChecker)
                    .Where(c => c != null))
                yield return permissionChecker;
        }

        private static IEnumerable<string> GetCommandGroupPaths(Type type)
        {
            if (type == null || type == typeof (object))
                yield break;

            var count = 0;
            var groups =
                type.GetTypeInfo()
                    .GetCustomAttributes<CommandGroupAttribute>()
                    .SelectMany(a => a.Paths)
                    .Select(g => g.Trim())
                    .Where(g => !string.IsNullOrEmpty(g)).ToArray();

            foreach (var group in GetCommandGroupPaths(type.DeclaringType))
            {
                if (groups.Length == 0)
                    yield return group;
                else
                    foreach (var g in groups)
                        yield return $"{group} {g}";

                count++;
            }

            if (count == 0)
                foreach (var g in groups)
                    yield return g;
        }

        private static IEnumerable<string> GetCommandGroupPaths(MethodInfo method)
        {
            var count = 0;
            var groups =
                method.GetCustomAttributes<CommandGroupAttribute>()
                    .SelectMany(a => a.Paths)
                    .Select(g => g.Trim())
                    .Where(g => !string.IsNullOrEmpty(g)).ToArray();

            foreach (var path in GetCommandGroupPaths(method.DeclaringType))
            {
                if (groups.Length == 0)
                    yield return path;
                else
                    foreach (var g in groups)
                        yield return $"{path} {g}";

                count++;
            }

            if (count == 0)
                foreach (var g in groups)
                    yield return g;
        }

        /// <summary>
        ///     Gets the command for the specified command text.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns>The found command.</returns>
        public ICommand GetCommandForText(BasePlayer player, string commandText)
        {
            ICommand candidate = null;

            foreach (var command in _commands)
            {
                switch (command.CanInvoke(player, commandText))
                {
                    case CommandCallableResponse.True:
                        return command;
                    case CommandCallableResponse.Optional:
                        candidate = command;
                        break;
                }
            }

            return candidate;
        }

        #region Implementation of ICommandsManager

        /// <summary>
        ///     Gets a read-only collection of all registered commands.
        /// </summary>
        public virtual IReadOnlyCollection<ICommand> Commands => _commands.AsReadOnly();

        /// <summary>
        ///     Loads all tagged commands from the assembly containing the specified type.
        /// </summary>
        /// <typeparam name="T">A type inside the assembly to load the commands form.</typeparam>
        public virtual void RegisterCommands<T>() where T : class
        {
            RegisterCommands(typeof (T));
        }

        /// <summary>
        ///     Loads all tagged commands from the assembly containing the specified type.
        /// </summary>
        /// <param name="typeInAssembly">A type inside the assembly to load the commands form.</param>
        public virtual void RegisterCommands(Type typeInAssembly)
        {
            if (typeInAssembly == null) throw new ArgumentNullException(nameof(typeInAssembly));
            RegisterCommands(typeInAssembly.GetTypeInfo().Assembly);
        }

        /// <summary>
        ///     Loads all tagged commands from the specified <paramref name="assembly" />.
        /// </summary>
        /// <param name="assembly">The assembly to load the commands from.</param>
        public virtual void RegisterCommands(Assembly assembly)
        {
            foreach (
                var method in
                    assembly.GetTypes()
                        // Get all classes in the specified assembly.
                        .Where(type => !type.GetTypeInfo().IsInterface && type.GetTypeInfo().IsClass && !type.GetTypeInfo().IsAbstract)
                        // Select the methods in the type.
                        .SelectMany(
                            type =>
                                type.GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                                BindingFlags.Instance))
                        // Exclude abstract methods. (none should be since abstract types are excluded)
                        .Where(method => !method.IsAbstract)
                        // Only include methods with a return type of bool or void.
                        .Where(method => SupportedReturnTypes.Contains(method.ReturnType))
                        // Only include methods which have a command attribute.
                        .Where(method => method.GetCustomAttribute<CommandAttribute>() != null)
                        // Only include methods which are static and have a player as first argument -or- are a non-static member of a player derived class.
                        .Where(DefaultCommand.IsValidCommandMethod)
                )
            {
                var attribute = method.GetCustomAttribute<CommandAttribute>();

                var commandPaths =
                    GetCommandGroupPaths(method)
                        .SelectMany(g => attribute.Names.Select(n => new CommandPath(g, n)))
                        .ToList();

                if (commandPaths.Count == 0)
                    commandPaths.AddRange(attribute.Names.Select(n => new CommandPath(n)));

                if (!string.IsNullOrWhiteSpace(attribute.Shortcut))
                    commandPaths.Add(new CommandPath(attribute.Shortcut));

                Register(commandPaths.ToArray(), attribute.DisplayName, attribute.IgnoreCase,
                    GetCommandPermissionCheckers(method).ToArray(), method, attribute.UsageMessage);
            }
        }

        /// <summary>
        ///     Registers the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public virtual void Register(ICommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            CoreLog.Log(CoreLogLevel.Debug, $"Registering command {command}");
            _commands.Add(command);
        }

        /// <summary>
        ///     Processes the specified player.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="player">The player.</param>
        /// <returns>true if processed; false otherwise.</returns>
        public virtual bool Process(string commandText, BasePlayer player)
        {
            var command = GetCommandForText(player, commandText);

            return command != null && command.Invoke(player, commandText);
        }

        #endregion
    }
}