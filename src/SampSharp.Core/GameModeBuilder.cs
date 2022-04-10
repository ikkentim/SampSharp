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
using System.Text;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a configuration build for running a SampSharp game mode.
    /// </summary>
    public sealed class GameModeBuilder
    {
        private IGameModeProvider _gameModeProvider;
        private Encoding _encoding;
        
        private GameModeRunnerFactory _create;
        private GameModeRunnerRun _run;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModeBuilder" /> class.
        /// </summary>
        public GameModeBuilder()
        {
            _create = Build;
            _run = InnerRun;
        }
        
        /// <summary>
        ///     Use the specified <paramref name="encoding" /> when en/decoding text messages sent to/from the server.
        /// </summary>
        /// <param name="encoding">The encoding to use when en/decoding text messages send to/from the server.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder UseEncoding(Encoding encoding)
        {
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
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
        /// Adds a build action to the game mode builder.
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder AddBuildAction(Func<GameModeRunnerFactory, IGameModeRunner> action)
        {
            var next = _create;
            _create = () => action(next);

            return this;
        }
      
        /// <summary>
        /// Adds a run action to the game mode builder.
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <returns>The updated game mode configuration builder.</returns>
        public GameModeBuilder AddRunAction(Action<IGameModeRunner, GameModeRunnerRun> action)
        {
            var next = _run;
            _run = runner => action(runner, next);

            return this;
        }

        /// <summary>
        ///     Run the game mode using the build configuration stored in this instance.
        /// </summary>
        public void Run()
        {
            var runner = _create() ??
                         throw new GameModeBuilderException("No game mode runner was created by the builder.");

            _run(runner);
        }
        
        private void InnerRun(IGameModeRunner runner)
        {
            runner.Run();
        }

        private IGameModeRunner Build()
        {
            if (_gameModeProvider == null)
                throw new GameModeBuilderException("No game mode provider has been specified.");

            return new HostedGameModeClient(_gameModeProvider, _encoding);
        }
    }
}
