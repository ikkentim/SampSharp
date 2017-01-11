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
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all dialog actions.
    /// </summary>
    [Controller]
    public class DialogController : IEventListener
    {
        /// <summary>
        ///     Registers the events this DialogController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            gameMode.DialogResponse += (sender, args) =>
            {
                var player = sender as BasePlayer;

                if (player == null)
                    return;

                Dialog.GetOpenDialog(player)?.OnResponse(args);
            };

            gameMode.PlayerDisconnected += (sender, args) =>
            {
                var player = sender as BasePlayer;

                if (player == null)
                    return;

                Dialog.Hide(player);
            };
        }
    }
}