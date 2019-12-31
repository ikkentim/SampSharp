// SampSharp
// Copyright 2019 Tim Potze
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

using SampSharp.Entities.SAMP.Components;
using SampSharp.Entities.SAMP.Definitions;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Provides functionality for adding entities to the SA:MP world.
    /// </summary>
    public interface IWorldService
    {
        /// <summary>
        /// Creates a new actor in the world.
        /// </summary>
        /// <param name="modelId">The model identifier.</param>
        /// <param name="position">The position of the actor.</param>
        /// <param name="rotation">The rotation of the actor.</param>
        /// <returns>The actor component of the newly created entity.</returns>
        Actor CreateActor(int modelId, Vector3 position, float rotation);

        /// <summary>
        /// Creates a vehicle in the world.
        /// </summary>
        /// <param name="type">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <param name="respawnDelay">
        /// The delay until the car is respawned without a driver in seconds. Using -1 will prevent the
        /// vehicle from respawning.
        /// </param>
        /// <param name="addSiren">If true, enables the vehicle to have a siren, providing the vehicle has a horn.</param>
        /// <returns> The created vehicle.</returns>
        Vehicle CreateVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2,
            int respawnDelay = -1, bool addSiren = false);

        /// <summary>
        /// Creates a static vehicle in the world.
        /// </summary>
        /// <param name="type">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <param name="respawnDelay">
        /// The delay until the car is respawned without a driver in seconds. Using -1 will prevent the
        /// vehicle from respawning.
        /// </param>
        /// <param name="addSiren">If true, enables the vehicle to have a siren, providing the vehicle has a horn.</param>
        /// <returns> The created vehicle.</returns>
        Vehicle CreateStaticVehicle(VehicleModelType type, Vector3 position, float rotation, int color1, int color2,
            int respawnDelay = -1, bool addSiren = false);

        /// <summary>
        /// Creates a gang zone in the world.
        /// </summary>
        /// <param name="minX">The minimum x.</param>
        /// <param name="minY">The minimum y.</param>
        /// <param name="maxX">The maximum x.</param>
        /// <param name="maxY">The maximum y.</param>
        /// <returns>The created gang zone.</returns>
        GangZone CreateGangZone(float minX, float minY, float maxX, float maxY);

        /// <summary>
        /// Creates a pickup in the world.
        /// </summary>
        /// <param name="model">The model of the pickup.</param>
        /// <param name="type">The pickup spawn type.</param>
        /// <param name="position">The position where the pickup should be spawned.</param>
        /// <param name="virtualWorld">The virtual world ID of the pickup. Use -1 for all worlds.</param>
        /// <returns>The created pickup.</returns>
        Pickup CreatePickup(int model, int type, Vector3 position, int virtualWorld = -1);

        /// <summary>
        /// Creates an object in the world.
        /// </summary>
        /// <param name="modelId">The model ID.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <returns>The created object.</returns>
        GlobalObject CreateObject(int modelId, Vector3 position, Vector3 rotation, float drawDistance);

        /// <summary>
        /// Creates a player object in the world.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="modelId">The model ID.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <returns>The created player object.</returns>
        PlayerObject CreatePlayerObject(Entity player, int modelId, Vector3 position, Vector3 rotation,
            float drawDistance);

        /// <summary>
        /// Creates a text label in the world.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="testLos">if set to <c>true</c> the line of sight is tested to decide whether the label is drawn.</param>
        /// <returns>The created text label.</returns>
        TextLabel CreateTextLabel(string text, Color color, Vector3 position, float drawDistance,
            int virtualWorld = 0, bool testLos = true);

        /// <summary>
        /// Creates a player text label in the world.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="testLos">if set to <c>true</c> the line of sight is tested to decide whether the label is drawn.</param>
        /// <param name="attachedTo">A player or vehicle to attach the text label to.</param>
        /// <returns>The created text label.</returns>
        PlayerTextLabel CreatePlayerTextLabel(Entity player, string text, Color color, Vector3 position,
            float drawDistance, bool testLos = true, Entity attachedTo = null);
    }
}