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
using System.Reflection;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Contains methods for a command manager service.
    /// </summary>
    public interface ICommandsManager : IService
    {
        /// <summary>
        ///     Gets a read-only collection of all registered commands.
        /// </summary>
        IReadOnlyCollection<ICommand> Commands { get; }

        /// <summary>
        ///     Loads all tagged commands from the assembly containing the specified type.
        /// </summary>
        /// <typeparam name="T">A type inside the assembly to load the commands form.</typeparam>
        void RegisterCommands<T>() where T : class;

        /// <summary>
        ///     Loads all tagged commands from the assembly containing the specified type.
        /// </summary>
        /// <param name="typeInAssembly">A type inside the assembly to load the commands form.</param>
        void RegisterCommands(Type typeInAssembly);

        /// <summary>
        ///     Loads all tagged commands from the specified <paramref name="assembly" />.
        /// </summary>
        /// <param name="assembly">The assembly to load the commands from.</param>
        void RegisterCommands(Assembly assembly);

        /// <summary>
        ///     Registers the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        void Register(ICommand command);

        /// <summary>
        ///     Processes the specified player.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="player">The player.</param>
        /// <returns>true if processed; false otherwise.</returns>
        bool Process(string commandText, BasePlayer player);
    }
}