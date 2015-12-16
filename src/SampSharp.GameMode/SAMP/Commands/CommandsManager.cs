using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    public class CommandsManager : ICommandsManager
    {
        private static readonly Type[] SupportedReturnTypes = {typeof (bool), typeof (void)};
        private readonly List<ICommand> _commands = new List<ICommand>(); 

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
            RegisterCommands(typeof(T));
        }

        /// <summary>
        ///     Loads all tagged commands from the assembly containing the specified type.
        /// </summary>
        /// <param name="typeInAssembly">A type inside the assembly to load the commands form.</param>
        public virtual void RegisterCommands(Type typeInAssembly)
        {
            if (typeInAssembly == null) throw new ArgumentNullException(nameof(typeInAssembly));
            RegisterCommands(typeInAssembly.Assembly);
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
                        .Where(type => !type.IsInterface && type.IsClass && !type.IsAbstract)
                        // Select the methods in the type.
                        .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
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

                if(commandPaths.Count == 0)
                    commandPaths.AddRange(attribute.Names.Select(n => new CommandPath(n)));
                
                if (!string.IsNullOrWhiteSpace(attribute.Shortcut))
                    commandPaths.Add(new CommandPath(attribute.Shortcut));

                Register(commandPaths.ToArray(), attribute.DisplayName, attribute.IgnoreCase, GetCommandPermissionCheckers(method).ToArray(), method, attribute.UsageMessage);
            }
        }

        public virtual void Register(ICommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            _commands.Add(command);
        }

        public virtual bool Process(string commandText, BasePlayer player)
        {
            var command = GetCommandForText(player, commandText);

            return command != null && command.Invoke(player, commandText);
        }

        #endregion

        public virtual void Register(CommandPath[] commandPaths, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
        {
            Register(CreateDefaultCommand(commandPaths, displayName, ignoreCase, permissionCheckers, method, usageMessage));
        }

        protected virtual ICommand CreateDefaultCommand(CommandPath[] commandPaths, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
        {
            return new DefaultCommand(commandPaths, displayName, ignoreCase, permissionCheckers, method, usageMessage);
        }

        private static IEnumerable<IPermissionChecker> GetCommandPermissionCheckers(Type type)
        {
            if (type == null || type == typeof(object))
                yield break;

            foreach (var permissionChecker in GetCommandPermissionCheckers(type.DeclaringType))
                yield return permissionChecker;

            foreach (
                var permissionChecker in
                    type.GetCustomAttributes<CommandGroupAttribute>().Select(a => a.PermissionChecker))
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
                        .Concat(method.GetCustomAttributes<CommandAttribute>().Select(a => a.PermissionChecker)))
                yield return permissionChecker;
        }

        private static IEnumerable<string> GetCommandGroupPaths(Type type)
        {
            if (type == null || type == typeof(object))
                yield break;

            var count = 0;
            var groups =
                type.GetCustomAttributes<CommandGroupAttribute>()
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
    }
}