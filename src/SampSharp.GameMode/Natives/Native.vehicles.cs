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

using System.Runtime.CompilerServices;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Natives
{
    public static partial class Native
    {
        /// <summary>
        ///     Check if a vehicle is created.
        /// </summary>
        /// <param name="vehicleid">The vehicle to check for existance.</param>
        /// <returns>true if the vehicle exists, otherwise false.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidVehicle(int vehicleid);

        /// <summary>
        ///     This function can be used to calculate the distance (as a float) between a vehicle and another map coordinate. This
        ///     can be useful to detect how far a vehicle away is from a location.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to calculate the distance for.</param>
        /// <param name="x">The X map coordinate.</param>
        /// <param name="y">The Y map coordinate.</param>
        /// <param name="z">The Z map coordinate.</param>
        /// <returns>A float containing the distance from the point specified in the coordinates.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float GetVehicleDistanceFromPoint(int vehicleid, float x, float y, float z);

        /// <summary>
        ///     Creates a vehicle in the world. Can be used in place of <see cref="AddStaticVehicleEx" /> at any time in the
        ///     script.
        /// </summary>
        /// <param name="vehicletype">The model for the vehicle.</param>
        /// <param name="x">The X coordinate for the vehicle.</param>
        /// <param name="y">The Y coordinate for the vehicle.</param>
        /// <param name="z">The Z coordinate for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <param name="respawnDelay">
        ///     The delay until the car is respawned without a driver in seconds. Using -1 will prevent the
        ///     vehicle from respawning.
        /// </param>
        /// <returns>
        ///     The vehicle ID of the vehicle created (1 - <see cref="Limits.MaxVehicles" />).
        ///     <see cref="Misc.InvalidVehicleId" /> (65535) if vehicle was not created (vehicle limit reached or invalid vehicle
        ///     model ID passed).
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreateVehicle(int vehicletype, float x, float y, float z, float rotation, int color1,
            int color2, int respawnDelay);

        /// <summary>
        ///     Destroys a vehicle which was previously created.
        /// </summary>
        /// <param name="vehicleid">The vehicleid of the vehicle which shall be destroyed.</param>
        /// <returns> False: Vehicle does not exist. True: Vehicle was successfully destroyed.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyVehicle(int vehicleid);

        /// <summary>
        ///     Checks if a vehicle is streamed in for a player.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to check.</param>
        /// <param name="forplayerid">The ID of the player to check.</param>
        /// <returns>False: Vehicle is not streamed in for the player. False: Vehicle is streamed in for the player.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsVehicleStreamedIn(int vehicleid, int forplayerid);

        /// <summary>
        ///     Get the X Y Z coordinates of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the position of.</param>
        /// <param name="x">A float to store the X coordinate in, passed by reference.</param>
        /// <param name="y">A float to store the Y coordinate in, passed by reference.</param>
        /// <param name="z">A float to store the Z coordinate in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehiclePos(int vehicleid, out float x, out float y, out float z);

        /// <summary>
        ///     Set a vehicle's position.
        /// </summary>
        /// <remarks>
        ///     An empty vehicle will not fall after being teleported into the air.
        /// </remarks>
        /// <param name="vehicleid">Vehicle ID that you want set new position.</param>
        /// <param name="x">The X coordinate to position the vehicle at.</param>
        /// <param name="y">The Y coordinate to position the vehicle at.</param>
        /// <param name="z">The Z coordinate to position the vehicle at.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehiclePos(int vehicleid, float x, float y, float z);

        /// <summary>
        ///     Store the z rotation of a vehicle in a float variable.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the angle of.</param>
        /// <param name="zAngle">The variable (FLOAT) in which to store the rotation, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleZAngle(int vehicleid, out float zAngle);

        /// <summary>
        ///     Returns a vehicle's rotation on all axis as a quaternion.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the rotation of.</param>
        /// <param name="w">A float variable in which to store the first quaternion angle, passed by reference.</param>
        /// <param name="x">A float variable in which to store the second quaternion angle, passed by reference.</param>
        /// <param name="y">A float variable in which to store the third quaternion angle, passed by reference.</param>
        /// <param name="z">A float variable in which to store the fourth quaternion angle, passed by reference.</param>
        /// <returns>True on succes, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleRotationQuat(int vehicleid, out float w, out float x, out float y,
            out float z);

        /// <summary>
        ///     Set the Z rotation of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to set the rotation of.</param>
        /// <param name="zAngle">The angle to set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleZAngle(int vehicleid, float zAngle);

        /// <summary>
        ///     Set the parameters of a vehicle for a player.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to set the parameters of.</param>
        /// <param name="playerid">The ID of the player to set the vehicle's parameters for.</param>
        /// <param name="objective">False to disable the objective or True to show it.</param>
        /// <param name="doorslocked">False to unlock the doors or True to lock them.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleParamsForPlayer(int vehicleid, int playerid, bool objective,
            bool doorslocked);

        /// <summary>
        ///     Use this function before any player connects (<see cref="BaseMode.OnGameModeInit" />) to tell all clients that the
        ///     script will control vehicle engines and lights. This prevents the game automatically turning the engine on/off when
        ///     players enter/exit vehicles and headlights automatically coming on when it is dark.
        /// </summary>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ManualVehicleEngineAndLights();

        /// <summary>
        ///     Sets a vehicle's parameters for all players.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to set the parameters of.</param>
        /// <param name="engine">Toggle the engine status on or off.</param>
        /// <param name="lights">Toggle the lights on or off.</param>
        /// <param name="alarm">Toggle the vehicle alarm on or off.</param>
        /// <param name="doors">Toggle the lock status of the doors.</param>
        /// <param name="bonnet">Toggle the bonnet to be open or closed.</param>
        /// <param name="boot">Toggle the boot to be open or closed.</param>
        /// <param name="objective">Toggle the objective status for the vehicle on or off.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleParamsEx(int vehicleid, bool engine, bool lights, bool alarm, bool doors,
            bool bonnet, bool boot, bool objective);


        /// <summary>
        ///     Gets a vehicle's parameters.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the parameters from.</param>
        /// <param name="engine">Get the engine status. If True, the engine is running..</param>
        /// <param name="lights">Get the vehicle's lights' state. If True the lights are on.</param>
        /// <param name="alarm">Get the vehicle's alarm state. If True the alarm is (or was) sounding.</param>
        /// <param name="doors">Get the lock status of the doors. If True the doors are locked.</param>
        /// <param name="bonnet">Get the bonnet/hood status. If True, it's open.</param>
        /// <param name="boot">Get the boot/trunk status. True means it is open.</param>
        /// <param name="objective">Get the objective status. True means the objective is on.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleParamsEx(int vehicleid, out bool engine, out bool lights, out bool alarm,
            out bool doors, out bool bonnet, out bool boot, out bool objective);

        /// <summary>
        ///     Sets a vehicle back to the position at where it was created.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to respawn.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleToRespawn(int vehicleid);

        /// <summary>
        ///     Links the vehicle to the interior. This can be used for example for an arena/stadium.
        /// </summary>
        /// <param name="vehicleid">Vehicle ID (Not model).</param>
        /// <param name="interiorid">Interior ID.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LinkVehicleToInterior(int vehicleid, int interiorid);

        /// <summary>
        ///     Adds a 'component' (often referred to as a 'mod' (modification)) to a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to add the component to. Not to be confused with modelid.</param>
        /// <param name="componentid">The ID of the component to add to the vehicle.</param>
        /// <returns>
        ///     False: The component was not added because the vehicle does not exist. True: The component was successfully
        ///     added to the vehicle.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AddVehicleComponent(int vehicleid, int componentid);

        /// <summary>
        ///     Remove a component from a vehicle.
        /// </summary>
        /// <param name="vehicleid">ID of the vehicle.</param>
        /// <param name="componentid">ID of the component to remove.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemoveVehicleComponent(int vehicleid, int componentid);

        /// <summary>
        ///     Change a vehicle's primary and secondary colors.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to change the colors of.</param>
        /// <param name="color1">The new vehicle's primary Color ID.</param>
        /// <param name="color2">The new vehicle's secondary Color ID.</param>
        /// <returns> False: The vehicle does not exist. True: The vehicle's color was successfully changed.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ChangeVehicleColor(int vehicleid, int color1, int color2);

        /// <summary>
        ///     Change a vehicle's paintjob (for plain colors see <see cref="ChangeVehicleColor" />).
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to change the paintjob of.</param>
        /// <param name="paintjobid">The ID of the Paintjob to apply. Use 3 to remove a paintjob.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ChangeVehiclePaintjob(int vehicleid, int paintjobid);

        /// <summary>
        ///     Sets a vehicle's health to a specific value.
        /// </summary>
        /// <param name="vehicleid">ID of the vehicle to set the health of.</param>
        /// <param name="health">Vehicle heath given as a float value.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleHealth(int vehicleid, float health);

        /// <summary>
        ///     Get the health of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the health of.</param>
        /// <param name="health">A float varaible in which to store the vehicle's health, passed by reference.</param>
        /// <returns> True: success False: failure (invalid vehicle ID).</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleHealth(int vehicleid, out float health);

        /// <summary>
        ///     Attach a vehicle to another vehicle as a trailer.
        /// </summary>
        /// <param name="trailerid">The ID of the vehicle that will be pulled.</param>
        /// <param name="vehicleid">The ID of the vehicle that will pull the trailer.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachTrailerToVehicle(int trailerid, int vehicleid);

        /// <summary>
        ///     Detach the connection between a vehicle and its trailer, if any.
        /// </summary>
        /// <param name="vehicleid">ID of the pulling vehicle.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DetachTrailerFromVehicle(int vehicleid);

        /// <summary>
        ///     Checks if a vehicle has a trailer attached to it.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to check for trailers.</param>
        /// <returns>True if the vehicle has a trailer attached, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsTrailerAttachedToVehicle(int vehicleid);

        /// <summary>
        ///     Get the ID of the trailer attached to a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the trailer of.</param>
        /// <returns>The vehicle ID of the trailer or 0 if no trailer is attached.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleTrailer(int vehicleid);

        /// <summary>
        ///     Set a vehicle's numberplate, which supports olor embedding.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to set the numberplate of.</param>
        /// <param name="numberplate">The text that should be displayed on the numberplate. Color Embedding> is supported.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleNumberPlate(int vehicleid, string numberplate);

        /// <summary>
        ///     Gets the model ID of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the model of.</param>
        /// <returns>The vehicle model ID, or 0 if the vehicle doesn't exist.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleModel(int vehicleid);

        /// <summary>
        ///     Retreives the installed component ID from a vehicle in a specific slot.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to check for the component.</param>
        /// <param name="slot">The component slot to check for components.</param>
        /// <returns>The ID of the component installed in the specified slot.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleComponentInSlot(int vehicleid, int slot);

        /// <summary>
        ///     Find out what type of component a certain ID is.
        /// </summary>
        /// <param name="component">The component ID to check.</param>
        /// <returns>The component slot ID of the specified component.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleComponentType(int component);

        /// <summary>
        ///     Fully repairs a vehicle, including visual damage (bumps, dents, scratches, popped tires etc.).
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to repair.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RepairVehicle(int vehicleid);

        /// <summary>
        ///     Gets the velocity at which the vehicle is moving in the three directions, X, Y and Z.
        /// </summary>
        /// <param name="vehicleid">The vehicle to get the velocity of.</param>
        /// <param name="x">The Float variable to save the velocity in the X direction to.</param>
        /// <param name="y">The Float variable to save the velocity in the Y direction to.</param>
        /// <param name="z">The Float variable to save the velocity in the Z direction to.</param>
        /// <returns>
        ///     The function itself doesn't return a specific value. The X, Y and Z velocities are stored in the referenced
        ///     variables.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleVelocity(int vehicleid, out float x, out float y, out float z);

        /// <summary>
        ///     Sets the X, Y and Z velocity of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to set the velocity of.</param>
        /// <param name="x">The velocity in the X direction.</param>
        /// <param name="y">The velocity in the Y direction .</param>
        /// <param name="z">The velocity in the Z direction.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleVelocity(int vehicleid, float x, float y, float z);

        /// <summary>
        ///     Sets the angular X, Y and Z velocity of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to set the velocity of.</param>
        /// <param name="x">The amount of velocity in the angular X direction.</param>
        /// <param name="y">The amount of velocity in the angular Y direction .</param>
        /// <param name="z">The amount of velocity in the angular Z direction.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleAngularVelocity(int vehicleid, float x, float y, float z);

        /// <summary>
        ///     Retrieve the damage statuses of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the damage statuses of.</param>
        /// <param name="panels">A variable to store the panel damage data in, passed by reference.</param>
        /// <param name="doors">A variable to store the door damage data in, passed by reference.</param>
        /// <param name="lights">A variable to store the light damage data in, passed by reference.</param>
        /// <param name="tires">A variable to store the tire damage data in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleDamageStatus(int vehicleid, out int panels, out int doors, out int lights,
            out int tires);

        /// <summary>
        ///     Sets the various visual damage statuses of a vehicle, such as popped tires, broken lights and damaged panels.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to set the damage of.</param>
        /// <param name="panels">A set of bits containing the panel damage status.</param>
        /// <param name="doors">A set of bits containing the door damage status.</param>
        /// <param name="lights">A set of bits containing the light damage status.</param>
        /// <param name="tires">A set of bits containing the tire damage status.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UpdateVehicleDamageStatus(int vehicleid, int panels, int doors, int lights, int tires);

        /// <summary>
        ///     Sets the 'virtual world' of a vehicle. Players will only be able to see vehicles in their own virtual world.
        /// </summary>
        /// <param name="vehicleid">The ID of vehicle to set the virtual world of.</param>
        /// <param name="worldid">The ID of the virtual world to put the vehicle in.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetVehicleVirtualWorld(int vehicleid, int worldid);

        /// <summary>
        ///     Get the virtual world of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the virtual world of.</param>
        /// <returns>The virtual world that the vehicle is in.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehicleVirtualWorld(int vehicleid);

        /// <summary>
        ///     Retrieve information about a specific vehicle model such as the size or position of seats.
        /// </summary>
        /// <param name="model">The vehicle model to get info of.</param>
        /// <param name="infotype">The type of information to retrieve.</param>
        /// <param name="x">A float to store the X value.</param>
        /// <param name="y">A float to store the Y value.</param>
        /// <param name="z">A float to store the Z value.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetVehicleModelInfo(int model, int infotype, out float x, out float y, out float z);

        /// <summary>
        ///     Get the X Y Z coordinates of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the position of.</param>
        /// <returns>The position of the vehicle.</returns>
        public static Vector GetVehiclePos(int vehicleid)
        {
            float x, y, z;
            GetVehiclePos(vehicleid, out x, out y, out z);
            return new Vector(x, y, z);
        }

        /// <summary>
        ///     Set a vehicle's position.
        /// </summary>
        /// An empty vehicle will not fall after being teleported into the air.
        /// <remarks>
        /// </remarks>
        /// <param name="vehicleid">The ID of the vehicle.</param>
        /// <param name="position">The coordinates to position the vehicle at.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetVehiclePos(int vehicleid, Vector position)
        {
            return SetVehiclePos(vehicleid, position.X, position.Y, position.Z);
        }

        /// <summary>
        ///     Get the z rotation of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the angle of.</param>
        /// <returns>The rotation of the vehicle.</returns>
        public static float GetVehicleZAngle(int vehicleid)
        {
            float angle;
            GetVehicleZAngle(vehicleid, out angle);
            return angle;
        }

        /// <summary>
        ///     Get the health of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to get the health of.</param>
        /// <returns>The vehicle's health.</returns>
        public static float GetVehicleHealth(int vehicleid)
        {
            float health;
            GetVehicleHealth(vehicleid, out health);
            return health;
        }

        /// <summary>
        ///     Gets the velocity at which the vehicle is moving in the three directions.
        /// </summary>
        /// <param name="vehicleid">The vehicle to get the velocity of.</param>
        /// <returns>The velocity of the vehicle.</returns>
        public static Vector GetVehicleVelocity(int vehicleid)
        {
            float x, y, z;
            GetVehicleVelocity(vehicleid, out x, out y, out z);
            return new Vector(x, y, z);
        }

        /// <summary>
        ///     Sets the velocity of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to set the velocity of.</param>
        /// <param name="velocity">The velocity.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetVehicleVelocity(int vehicleid, Vector velocity)
        {
            return SetVehicleVelocity(vehicleid, velocity.X, velocity.Y, velocity.Z);
        }

        /// <summary>
        ///     Sets the angular X, Y and Z velocity of a vehicle.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle to set the velocity of.</param>
        /// <param name="velocity">The angular velocity.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetVehicleAngularVelocity(int vehicleid, Vector velocity)
        {
            return SetVehicleAngularVelocity(vehicleid, velocity.X, velocity.Y, velocity.Z);
        }

        /// <summary>
        ///     Retrieve information about a specific vehicle model such as the size or position of seats.
        /// </summary>
        /// <param name="model">The vehicle model to get info of.</param>
        /// <param name="infotype">The type of information to retrieve.</param>
        /// <returns>The model information.</returns>
        public static Vector GetVehicleModelInfo(VehicleModelType model, VehicleModelInfoType infotype)
        {
            float x, y, z;
            GetVehicleModelInfo((int) model, (int) infotype, out x, out y, out z);
            return new Vector(x, y, z);
        }
    }
}