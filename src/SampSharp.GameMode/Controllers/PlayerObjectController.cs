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
using System.Linq;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all player-object actions.
    /// </summary>
    [Controller]
    public class PlayerObjectController : Disposable, IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this PlayerObjectController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerObjectMoved += (sender, args) => (sender as PlayerObject)?.OnMoved(args);
            gameMode.PlayerEditPlayerObject += (sender, args) => (sender as PlayerObject)?.OnEdited(args);
            gameMode.PlayerSelectPlayerObject += (sender, args) => (sender as PlayerObject)?.OnSelected(args);
            gameMode.PlayerCleanup += (sender, args) =>
            {
                var player = sender as BasePlayer;

                if (player == null)
                    return;

                foreach (var obj in PlayerObject.Of(player).ToArray())
                    obj.Dispose();
            };
        }

        /// <summary>
        ///     Registers types this PlayerObjectController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            PlayerObject.Register<PlayerObject>();
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var o in PlayerObject.All)
                {
                    o.Dispose();
                }
            }
        }
    }
}