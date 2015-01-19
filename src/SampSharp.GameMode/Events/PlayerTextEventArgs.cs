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

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerText" /> event.
    /// </summary>
    public class PlayerTextEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerTextEventArgs class.
        /// </summary>
        /// <param name="playerid">The id of the player.</param>
        /// <param name="text">The text sent by the player.</param>
        public PlayerTextEventArgs(int playerid, string text) : base(playerid)
        {
            Text = text;
        }

        /// <summary>
        ///     Gets the text sent by the player.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        ///     Gets or sets whether this message should be sent to all players.
        ///     Returning 0 in this callback will stop the text from being sent to all players
        /// </summary>
        public bool SendToPlayers { get; set; }
    }
}