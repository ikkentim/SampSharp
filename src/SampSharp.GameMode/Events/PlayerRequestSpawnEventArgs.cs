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

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerRequestSpawn" /> event.
    /// </summary>
    public class PlayerRequestSpawnEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerRequestSpawnEventArgs class.
        /// </summary>
        /// <param name="playerid">The id of the player.</param>
        public PlayerRequestSpawnEventArgs(int playerid)
        {
            PlayerId = playerid;
        }

        /// <summary>
        ///     Gets the id of the player involved in the event.
        /// </summary>
        public int PlayerId { get; private set; }

        /// <summary>
        ///     Gets the player involved in the event.
        /// </summary>
        public GtaPlayer Player
        {
            get { return PlayerId == GtaPlayer.InvalidId ? null : GtaPlayer.Find(PlayerId); }
        }

        /// <summary>
        ///     Gets or sets whether the player is prevented from spawning.
        /// </summary>
        public bool PreventSpawning { get; set; }
    }
}