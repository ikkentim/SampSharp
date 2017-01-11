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
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all commands.
    /// </summary>
    [Controller]
    public class CommandController : IEventListener, IGameServiceProvider
    {
        /// <summary>
        ///     Gets or sets the commands manager.
        /// </summary>
        protected virtual ICommandsManager CommandsManager { get; set; }

        /// <summary>
        ///     Registers the events this <see cref="GlobalObjectController" /> wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerCommandText += gameMode_PlayerCommandText;
        }

        #region Implementation of IGameServiceProvider

        /// <summary>
        ///     Registers the services this controller provides.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        /// <param name="serviceContainer">The service container.</param>
        public virtual void RegisterServices(BaseMode gameMode, GameModeServiceContainer serviceContainer)
        {
            CommandsManager = new CommandsManager(gameMode);
            serviceContainer.AddService(CommandsManager);

            // Register commands in game mode.
            CommandsManager.RegisterCommands(gameMode.GetType());
        }

        #endregion

        private void gameMode_PlayerCommandText(object sender, CommandTextEventArgs e)
        {
            if (CommandsManager == null)
                return;

            var player = sender as BasePlayer;
            if (player == null) return;

            e.Success = CommandsManager.Process(e.Text, player);
        }
    }
}