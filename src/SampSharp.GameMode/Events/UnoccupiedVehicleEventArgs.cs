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
    ///     Provides data for the <see cref="BaseMode.UnoccupiedVehicleUpdated" /> event.
    /// </summary>
    public class UnoccupiedVehicleEventArgs : PlayerVehicleEventArgs //@todo: change to inherit VehicleEventArgs only.
    {
        public UnoccupiedVehicleEventArgs(int playerid, int vehicleid, int passengerSeat, Vector newPosition,
            Vector newVelocity)
            : base(playerid, vehicleid)
        {
            PassengerSeat = passengerSeat;
            NewPosition = newPosition;
            NewVelocity = newVelocity;
        }

        public int PassengerSeat { get; private set; }

        public Vector NewPosition { get; private set; }

        public Vector NewVelocity { get; private set; }

        /// <summary>
        ///     Gets or sets whether to stop the vehicle syncing its position to other players.
        /// </summary>
        public bool PreventPropagation { get; set; }

        //@todo Implement IPropagationPreventable interface where possible
    }
}