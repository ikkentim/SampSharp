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
namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all player states.
    /// </summary>
    public enum PlayerState
    {
        /// <summary>
        ///     No state.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Player is on foot.
        /// </summary>
        OnFoot = 1,

        /// <summary>
        ///     Player is driving a vehicle.
        /// </summary>
        Driving = 2,

        /// <summary>
        ///     Player is in a vehicle as passenger.
        /// </summary>
        Passenger = 3,

        /// <summary>
        ///     Player is exiting a vehicle.
        /// </summary>
        ExitVehicle = 4,

        /// <summary>
        ///     Player is entering a vehicle as driver.
        /// </summary>
        EnterVehicleDriver = 5,

        /// <summary>
        ///     Player is entering a vehicle as passenger.
        /// </summary>
        EnterVehiclePassenger = 6,

        /// <summary>
        ///     Player is dead.
        /// </summary>
        Wasted = 7,

        /// <summary>
        ///     Player has spawned.
        /// </summary>
        Spawned = 8,

        /// <summary>
        ///     Player is spectating.
        /// </summary>
        Spectating = 9
    }
}