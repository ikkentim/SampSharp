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
    ///     Provides data for the <see cref="BaseMode.UnoccupiedVehicleUpdated" /> or
    ///     <see cref="BaseVehicle.UnoccupiedUpdate" /> event.
    /// </summary>
    public class UnoccupiedVehicleEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnoccupiedVehicleEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="passengerSeat">The passenger seat.</param>
        /// <param name="newPosition">The new position.</param>
        /// <param name="newVelocity">The new velocity.</param>
        public UnoccupiedVehicleEventArgs(BasePlayer player, int passengerSeat, Vector3 newPosition,
            Vector3 newVelocity)
            : base(player)
        {
            PassengerSeat = passengerSeat;
            NewPosition = newPosition;
            NewVelocity = newVelocity;
        }

        /// <summary>
        ///     Gets the passenger seat.
        /// </summary>
        public int PassengerSeat { get; private set; }

        /// <summary>
        ///     Gets the new position.
        /// </summary>
        public Vector3 NewPosition { get; private set; }

        /// <summary>
        ///     Gets the new velocity.
        /// </summary>
        public Vector3 NewVelocity { get; private set; }

        /// <summary>
        ///     Gets or sets whether to stop the vehicle syncing its position to other players.
        /// </summary>
        public bool PreventPropagation { get; set; }
    }
}