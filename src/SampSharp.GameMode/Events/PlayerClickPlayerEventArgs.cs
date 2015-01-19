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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerClickPlayer" /> event.
    /// </summary>
    public class PlayerClickPlayerEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Intializes a new instance of the PlayerClickPlayerEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player.</param>
        /// <param name="clickedplayerid">Id of the clicked player.</param>
        /// <param name="source">PlayerClickSource of the event.</param>
        public PlayerClickPlayerEventArgs(int playerid, int clickedplayerid, PlayerClickSource source) : base(playerid)
        {
            ClickedPlayerId = clickedplayerid;
            PlayerClickSource = source;
        }

        /// <summary>
        ///     Gets the id of the clicked player.
        /// </summary>
        public int ClickedPlayerId { get; private set; }

        /// <summary>
        ///     Gets the clicked player.
        /// </summary>
        public GtaPlayer ClickedPlayer
        {
            get { return ClickedPlayerId == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(ClickedPlayerId); }
        }

        /// <summary>
        ///     Gets the PlayerClickSource of this event.
        /// </summary>
        public PlayerClickSource PlayerClickSource { get; private set; }
    }
}