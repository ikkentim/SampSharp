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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.VehicleMod" /> or <see cref="BaseVehicle.Mod" /> event.
    /// </summary>
    public class VehicleModEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleModEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="componentId">The component identifier.</param>
        public VehicleModEventArgs(BasePlayer player, int componentId)
            : base(player)
        {
            ComponentId = componentId;
        }

        /// <summary>
        ///     Gets or sets the component identifier.
        /// </summary>
        /// <value>
        ///     The component identifier.
        /// </value>
        public int ComponentId { get; set; }

        /// <summary>
        ///     Gets or sets whether to desync the mod (or an invalid mod) from propagating and / or crashing players.
        /// </summary>
        public bool PreventPropagation { get; set; }
    }
}