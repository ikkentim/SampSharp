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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Factories
{
    /// <summary>
    ///     Contains the definition of a vehicle factory.
    /// </summary>
    public interface IVehicleFactory : IService
    {
        /// <summary>
        ///     Creates a <see cref="BaseVehicle" /> in the world.
        /// </summary>
        /// <param name="vehicletype">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <param name="respawnDelay">
        ///     The delay until the car is respawned without a driver in seconds. Using -1 will prevent the
        ///     vehicle from respawning.
        /// </param>
        /// <param name="addAlarm">If true, enables the vehicle to have a siren, providing the vehicle has a horn.</param>
        /// <returns> The <see cref="BaseVehicle" /> created.</returns>
        BaseVehicle Create(VehicleModelType vehicletype, Vector3 position, float rotation, int color1,
            int color2,
            int respawnDelay = -1, bool addAlarm = false);

        /// <summary>
        ///     Creates a static <see cref="BaseVehicle" /> in the world.
        /// </summary>
        /// <param name="vehicletype">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <param name="respawnDelay">
        ///     The delay until the car is respawned without a driver in seconds. Using -1 will prevent the
        ///     vehicle from respawning.
        /// </param>
        /// <param name="addAlarm">If true, enables the vehicle to have a siren, providing the vehicle has a horn.</param>
        /// <returns> The <see cref="BaseVehicle" /> created.</returns>
        BaseVehicle CreateStatic(VehicleModelType vehicletype, Vector3 position, float rotation, int color1,
            int color2,
            int respawnDelay, bool addAlarm = false);

        /// <summary>
        ///     Creates a static <see cref="BaseVehicle" /> in the world.
        /// </summary>
        /// <param name="vehicletype">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <returns> The <see cref="BaseVehicle" /> created.</returns>
        BaseVehicle CreateStatic(VehicleModelType vehicletype, Vector3 position, float rotation, int color1,
            int color2);
    }
}