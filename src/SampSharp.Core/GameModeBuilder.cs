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
using System.Threading;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a configuration build for running a SampSharp game mode.
    /// </summary>
    public sealed class GameModeBuilder
    {
        private IGameModeProvider _gameModeProvider;
        private string _pipeName = "SampSharp";
        private bool _redirectConsoleOutput;
        /// <summary>
        ///     Use the specified <see cref="pipeName" /> to communicate with the SampSharp server.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UsePipe(string pipeName)
        {
            _pipeName = pipeName;
            return this;
        }

        /// <summary>
        /// Redirect the console output to the server.
        /// </summary>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder RedirectConsoleOutput()
        {
            _redirectConsoleOutput = true;
            return this;
        }

        /// <summary>
        ///     Use the specified game mode.
        /// </summary>
        /// <param name="gameMode">The game mode to use.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder Use(IGameModeProvider gameMode)
        {
            _gameModeProvider = gameMode ?? throw new ArgumentNullException(nameof(gameMode));
            return this;
        }

        /// <summary>
        ///     Use the gamemode of the specified <see cref="TGameMode" /> type.
        /// </summary>
        /// <typeparam name="TGameMode">The type of the game mode to use.</typeparam>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder Use<TGameMode>() where TGameMode : IGameModeProvider
        {
            return Use(Activator.CreateInstance<TGameMode>());
        }

        /// <summary>
        ///     Run the game mode using the build configuration stored in this instance.
        /// </summary>
        public void Run()
        {
            var client = new GameModeClient(_pipeName, _gameModeProvider);

            if (_redirectConsoleOutput)
            {
                Console.SetOut(new ServerLogWriter(client));
            }

            client.Run();
        }
    }
}