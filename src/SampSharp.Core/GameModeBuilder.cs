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
using System.IO;
using SampSharp.Core.Logging;

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
        private GameModeStartBehaviour _startBehaviour = GameModeStartBehaviour.Gmx;
        //private GameModeExitBehaviour _exitBehaviour = GameModeExitBehaviour.ShutDown;

        /// <summary>
        ///     Use the specified <paramref name="pipeName" /> to communicate with the SampSharp server.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UsePipe(string pipeName)
        {
            _pipeName = pipeName;
            return this;
        }

        /// <summary>
        ///     Redirect the console output to the server.
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
        ///     Use the gamemode of the specified <typeparamref name="TGameMode" /> type.
        /// </summary>
        /// <typeparam name="TGameMode">The type of the game mode to use.</typeparam>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder Use<TGameMode>() where TGameMode : IGameModeProvider
        {
            return Use(Activator.CreateInstance<TGameMode>());
        }

        /// <summary>
        ///     Uses the specified log level as the maximum level which is written to the log by SampSharp.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseLogLevel(CoreLogLevel logLevel)
        {
            CoreLog.LogLevel = logLevel;
            return this;
        }

        /// <summary>
        ///     Uses the specified stream to log SampSharp log messages to.
        /// </summary>
        /// <param name="stream">The stream to log SampSharp log messages to.</param>
        /// <remarks>If a null value is specified as stream, no log messages will appear.</remarks>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseLogStream(Stream stream)
        {
            CoreLog.Stream = stream;
            return this;
        }

        /// <summary>
        ///     Sets the behaviour used once a OnGameModeExit call has been received.
        /// </summary>
        /// <param name="exitBehaviour">The exit behaviour.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseExitBehaviour(GameModeExitBehaviour exitBehaviour)
        {
            throw new NotImplementedException();
            //_exitBehaviour = exitBehaviour;
            //return this;
        }


        /// <summary>
        ///     Use the specified start method when attaching to the server.
        /// </summary>
        /// <param name="startBehaviour">The start behaviour.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseStartBehaviour(GameModeStartBehaviour startBehaviour)
        {
            _startBehaviour = startBehaviour;
            return this;
        }

        /// <summary>
        ///     Run the game mode using the build configuration stored in this instance.
        /// </summary>
        public void Run()
        {
            // TODO: To use the restart exit behaviour, the same client should still be used (because of the internal loaded resources, used by singletons)
            //do
            //{
                BuildAndRun();
            //} while (_exitBehaviour == GameModeExitBehaviour.Restart);
        }

        private void BuildAndRun()
        {
            var client = new GameModeClient(_pipeName, _startBehaviour, _gameModeProvider);

            var redirect = _redirectConsoleOutput;

            if (redirect)
                Console.SetOut(new ServerLogWriter(client));

            client.Run();

            if (redirect)
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        }
    }
}