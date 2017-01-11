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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.TrailerUpdate" /> <see cref="BaseVehicle.TrailerUpdate" /> event.
    /// </summary>
    public class TrailerEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TrailerEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player sending the update.</param>
        public TrailerEventArgs(BasePlayer player)
        {
            Player = player;
        }

        /// <summary>
        ///     Gets the player sending the update.
        /// </summary>
        public BasePlayer Player { get; private set; }

        /// <summary>
        ///     Gets or sets whether to stop the vehicle syncing its position to other players.
        /// </summary>
        public bool PreventPropagation { get; set; }
    }
}