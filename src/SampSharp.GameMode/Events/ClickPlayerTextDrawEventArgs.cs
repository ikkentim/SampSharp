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
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerClickPlayerTextDraw" />,
    ///     <see cref="Display.PlayerTextDraw.Click" /> or
    ///     <see cref="BasePlayer.ClickPlayerTextDraw" /> event.
    /// </summary>
    public class ClickPlayerTextDrawEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the ClickPlayerTextDrawEventArgs class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="playerTextDraw">The player text draw.</param>
        public ClickPlayerTextDrawEventArgs(BasePlayer player, PlayerTextDraw playerTextDraw)
        {
            Player = player;
            PlayerTextDraw = playerTextDraw;
        }

        /// <summary>
        ///     Gets the player.
        /// </summary>
        public BasePlayer Player { get; private set; }

        /// <summary>
        ///     Gets the text draw.
        /// </summary>
        public PlayerTextDraw PlayerTextDraw { get; private set; }
    }
}