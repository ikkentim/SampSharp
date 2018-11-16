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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerClickPlayer" /> or <see cref="BasePlayer.ClickPlayer" /> event.
    /// </summary>
    public class ClickPlayerEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the ClickPlayerEventArgs class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="clickedPlayer">Id of the clicked player.</param>
        /// <param name="source">PlayerClickSource of the event.</param>
        public ClickPlayerEventArgs(BasePlayer player, BasePlayer clickedPlayer, PlayerClickSource source) : base(player)
        {
            ClickedPlayer = Optional<BasePlayer>.From(clickedPlayer);
            PlayerClickSource = source;
        }

        /// <summary>
        ///     Gets the clicked player.
        /// </summary>
        public Optional<BasePlayer> ClickedPlayer { get; private set; }

        /// <summary>
        ///     Gets the PlayerClickSource of this event.
        /// </summary>
        public PlayerClickSource PlayerClickSource { get; private set; }
    }
}