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
    ///     Provides data for the <see cref="BaseMode.VehicleResprayed" /> or <see cref="BaseVehicle.Resprayed" /> event.
    /// </summary>
    public class VehicleResprayedEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleResprayedEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="color1">The color1.</param>
        /// <param name="color2">The color2.</param>
        public VehicleResprayedEventArgs(BasePlayer player, int color1, int color2)
            : base(player)
        {
            Color1 = color1;
            Color2 = color2;
        }

        /// <summary>
        ///     Gets the color1.
        /// </summary>
        public int Color1 { get; private set; }

        /// <summary>
        ///     Gets the color2.
        /// </summary>
        public int Color2 { get; private set; }
    }
}