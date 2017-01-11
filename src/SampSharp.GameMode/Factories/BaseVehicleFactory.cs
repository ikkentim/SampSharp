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
using System.Linq;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Factories
{
    /// <summary>
    ///     Represents the default vehicle factory.
    /// </summary>
    public partial class BaseVehicleFactory : IVehicleFactory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseVehicleFactory" /> class.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        public BaseVehicleFactory(BaseMode gameMode)
        {
            GameMode = gameMode;
        }

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
        public virtual BaseVehicle Create(VehicleModelType vehicletype, Vector3 position, float rotation, int color1,
            int color2,
            int respawnDelay = -1, bool addAlarm = false)
        {
            var id = new[] {449, 537, 538, 569, 570, 590}.Contains((int) vehicletype)
                ? BaseVehicleFactoryInternal.Instance.AddStaticVehicleEx((int) vehicletype, position.X, position.Y, position.Z, rotation, color1,
                    color2,
                    respawnDelay, addAlarm)
                : BaseVehicleFactoryInternal.Instance.CreateVehicle((int) vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                    respawnDelay, addAlarm);

            return id == BaseVehicle.InvalidId ? null : BaseVehicle.FindOrCreate(id);
        }

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
        public virtual BaseVehicle CreateStatic(VehicleModelType vehicletype, Vector3 position, float rotation,
            int color1,
            int color2,
            int respawnDelay, bool addAlarm = false)
        {
            var id = BaseVehicleFactoryInternal.Instance.AddStaticVehicleEx((int) vehicletype, position.X, position.Y, position.Z, rotation, color1,
                color2,
                respawnDelay, addAlarm);

            return id == BaseVehicle.InvalidId ? null : BaseVehicle.FindOrCreate(id);
        }

        /// <summary>
        ///     Creates a static <see cref="BaseVehicle" /> in the world.
        /// </summary>
        /// <param name="vehicletype">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <returns> The <see cref="BaseVehicle" /> created.</returns>
        public virtual BaseVehicle CreateStatic(VehicleModelType vehicletype, Vector3 position, float rotation,
            int color1,
            int color2)
        {
            var id = BaseVehicleFactoryInternal.Instance.AddStaticVehicle((int) vehicletype, position.X, position.Y, position.Z, rotation, color1,
                color2);

            return id == BaseVehicle.InvalidId ? null : BaseVehicle.FindOrCreate(id);
        }

        /// <summary>
        ///     Gets the game mode.
        /// </summary>
        public BaseMode GameMode { get; }
    }
}