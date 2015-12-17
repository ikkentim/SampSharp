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

using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.SAMP.Commands;

namespace TestMode.Controllers
{
    public class MyCommandController : CommandController
    {
        #region Overrides of CommandController

        /// <summary>
        ///     Registers the services this controller provides.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        /// <param name="serviceContainer">The service container.</param>
        public override void RegisterServices(BaseMode gameMode, GameModeServiceContainer serviceContainer)
        {
            CommandsManager = new CommandsManager(gameMode);
            serviceContainer.AddService(CommandsManager);

            // Register commands in game mode.
            CommandsManager.RegisterCommands(gameMode.GetType());
        }

        #endregion
    }
}