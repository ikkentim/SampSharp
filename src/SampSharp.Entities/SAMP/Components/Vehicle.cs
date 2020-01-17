// SampSharp
// Copyright 2020 Tim Potze
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
using SampSharp.Entities.SAMP.Definitions;
using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of a vehicle.
    /// </summary>
    public sealed class Vehicle : Component
    {
        private Vehicle()
        {
        }

        /// <summary>
        /// Gets or sets the Z angle of this vehicle.
        /// </summary>
        public float Angle
        {
            get
            {
                GetComponent<NativeVehicle>().GetVehicleZAngle(out var angle);
                return angle;
            }
            set => GetComponent<NativeVehicle>().SetVehicleZAngle(value);
        }

        /// <summary>
        /// Gets the model ID of this vehicle.
        /// </summary>
        public VehicleModelType Model => (VehicleModelType) GetComponent<NativeVehicle>().GetVehicleModel();

        /// <summary>
        /// Gets whether this vehicle has a trailer attached to it.
        /// </summary>
        public bool HasTrailer => GetComponent<NativeVehicle>().IsTrailerAttachedToVehicle();

        /// <summary>
        /// Gets or sets the the trailer attached to this vehicle.
        /// </summary>
        /// <returns>The trailer attached.</returns>
        public Entity Trailer
        {
            get
            {
                var id = GetComponent<NativeVehicle>().GetVehicleTrailer();
                return id == 0 ? null : Entity.Manager.Get(SampEntities.GetVehicleId(id));
            }
            set
            {
                if (value == null)
                    GetComponent<NativeVehicle>().DetachTrailerFromVehicle();
                else
                    value.GetComponent<NativeVehicle>().AttachTrailerToVehicle(Entity.Id);
            }
        }

        /// <summary>
        /// Gets or sets the velocity at which this vehicle is moving.
        /// </summary>
        public Vector3 Velocity
        {
            get
            {
                GetComponent<NativeVehicle>().GetVehicleVelocity(out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => GetComponent<NativeVehicle>().SetVehicleVelocity(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Gets or sets the virtual world of this vehicle.
        /// </summary>
        public int VirtualWorld
        {
            get => GetComponent<NativeVehicle>().GetVehicleVirtualWorld();
            set => GetComponent<NativeVehicle>().SetVehicleVirtualWorld(value);
        }

        /// <summary>
        /// Gets or sets this vehicle's engine status. If True, the engine is running.
        /// </summary>
        public bool Engine
        {
            get
            {
                GetParameters(out bool value, out _, out _, out _, out _, out _, out _);
                return value;
            }
            set
            {
                GetParameters(out _, out var b, out var c, out var d, out var e, out var f,
                    out VehicleParameterValue g);
                SetParameters(value ? VehicleParameterValue.On : VehicleParameterValue.Off, b, c, d, e, f, g);
            }
        }

        /// <summary>
        /// Gets or sets this vehicle's lights' state. If True the lights are on.
        /// </summary>
        public bool Lights
        {
            get
            {
                GetParameters(out _, out bool value, out _, out _, out _, out _, out _);
                return value;
            }
            set
            {
                GetParameters(out var a, out _, out var c, out var d, out var e, out var f,
                    out VehicleParameterValue g);
                SetParameters(a, value ? VehicleParameterValue.On : VehicleParameterValue.Off, c, d, e, f, g);
            }
        }

        /// <summary>
        /// Gets or sets this vehicle's alarm state. If True the alarm is (or was) sounding.
        /// </summary>
        public bool Alarm
        {
            get
            {
                GetParameters(out _, out _, out bool value, out _, out _, out _, out _);
                return value;
            }
            set
            {
                GetParameters(out var a, out var b, out _, out var d, out var e, out var f,
                    out VehicleParameterValue g);
                SetParameters(a, b, value ? VehicleParameterValue.On : VehicleParameterValue.Off, d, e, f, g);
            }
        }

        /// <summary>
        /// Gets or sets the lock status of the doors of this vehicle. If True the doors are locked.
        /// </summary>
        public bool Doors
        {
            get
            {
                GetParameters(out _, out _, out _, out bool value, out _, out _, out _);
                return value;
            }
            set
            {
                GetParameters(out var a, out var b, out var c, out _, out var e, out var f,
                    out VehicleParameterValue g);
                SetParameters(a, b, c, value ? VehicleParameterValue.On : VehicleParameterValue.Off, e, f, g);
            }
        }

        /// <summary>
        /// Gets or sets the bonnet/hood status of this vehicle. If True, it's open.
        /// </summary>
        public bool Bonnet
        {
            get
            {
                GetParameters(out _, out bool value, out _, out _, out value, out _, out _);
                return value;
            }
            set
            {
                GetParameters(out var a, out var b, out var c, out var d, out _, out var f,
                    out VehicleParameterValue g);
                SetParameters(a, b, c, d, value ? VehicleParameterValue.On : VehicleParameterValue.Off, f, g);
            }
        }

        /// <summary>
        /// Gets or sets the boot/trunk status of this vehicle. True means it is open.
        /// </summary>
        public bool Boot
        {
            get
            {
                GetParameters(out _, out bool value, out _, out _, out _, out value, out _);
                return value;
            }
            set
            {
                GetParameters(out var a, out var b, out var c, out var d, out var e, out _,
                    out VehicleParameterValue g);
                SetParameters(a, b, c, d, e, value ? VehicleParameterValue.On : VehicleParameterValue.Off, g);
            }
        }

        /// <summary>
        /// Gets or sets the objective status of this vehicle. True means the objective is on.
        /// </summary>
        public bool Objective
        {
            get
            {
                GetParameters(out _, out bool value, out _, out _, out _, out _, out value);
                return value;
            }
            set
            {
                GetParameters(out VehicleParameterValue a, out var b, out var c, out var d, out var e, out var f,
                    out _);
                SetParameters(a, b, c, d, e, f, value ? VehicleParameterValue.On : VehicleParameterValue.Off);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the driver door is open.
        /// </summary>
        public bool IsDriverDoorOpen
        {
            get
            {
                GetDoorsParameters(out bool value, out _, out _, out _);
                return value;
            }
            set
            {
                GetDoorsParameters(out _, out var b, out var c, out VehicleParameterValue d);
                SetDoorsParameters(value ? VehicleParameterValue.On : VehicleParameterValue.Off, b, c, d);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the passenger door is open.
        /// </summary>
        public bool IsPassengerDoorOpen
        {
            get
            {
                GetDoorsParameters(out _, out bool value, out _, out _);
                return value;
            }
            set
            {
                GetDoorsParameters(out var a, out _, out var c, out VehicleParameterValue d);
                SetDoorsParameters(a, value ? VehicleParameterValue.On : VehicleParameterValue.Off, c, d);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the driver door is open.
        /// </summary>
        public bool IsBackLeftDoorOpen
        {
            get
            {
                GetDoorsParameters(out _, out _, out bool value, out _);
                return value;
            }
            set
            {
                GetDoorsParameters(out var a, out var b, out _, out VehicleParameterValue d);
                SetDoorsParameters(a, b, value ? VehicleParameterValue.On : VehicleParameterValue.Off, d);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the driver door is open.
        /// </summary>
        public bool IsBackRightDoorOpen
        {
            get
            {
                GetDoorsParameters(out _, out _, out _, out bool value);
                return value;
            }
            set
            {
                GetDoorsParameters(out VehicleParameterValue a, out var b, out var c, out _);
                SetDoorsParameters(a, b, c, value ? VehicleParameterValue.On : VehicleParameterValue.Off);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the driver window is closed.
        /// </summary>
        public bool IsDriverWindowClosed
        {
            get
            {
                GetWindowsParameters(out bool value, out _, out _, out _);
                return value;
            }
            set
            {
                GetWindowsParameters(out _, out var b, out var c, out VehicleParameterValue d);
                SetWindowsParameters(value ? VehicleParameterValue.On : VehicleParameterValue.Off, b, c, d);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the passenger window is closed.
        /// </summary>
        public bool IsPassengerWindowClosed
        {
            get
            {
                GetWindowsParameters(out _, out bool value, out _, out _);
                return value;
            }
            set
            {
                GetWindowsParameters(out var a, out _, out var c, out VehicleParameterValue d);
                SetWindowsParameters(a, value ? VehicleParameterValue.On : VehicleParameterValue.Off, c, d);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the driver window is closed.
        /// </summary>
        public bool IsBackLeftWindowClosed
        {
            get
            {
                GetWindowsParameters(out _, out _, out bool value, out _);
                return value;
            }
            set
            {
                GetWindowsParameters(out var a, out var b, out _, out VehicleParameterValue d);
                SetWindowsParameters(a, b, value ? VehicleParameterValue.On : VehicleParameterValue.Off, d);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the driver window is closed.
        /// </summary>
        public bool IsBackRightWindowClosed
        {
            get
            {
                GetWindowsParameters(out _, out _, out _, out bool value);
                return value;
            }
            set
            {
                GetWindowsParameters(out VehicleParameterValue a, out var b, out var c, out _);
                SetWindowsParameters(a, b, c, value ? VehicleParameterValue.On : VehicleParameterValue.Off);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this Vehicle's siren is on.
        /// </summary>
        public bool IsSirenOn => GetComponent<NativeVehicle>().GetVehicleParamsSirenState() == 1;

        /// <summary>
        /// Gets or sets the rotation of this vehicle.
        /// </summary>
        /// <remarks>
        /// Only the Z angle can be set!
        /// </remarks>
        public Vector3 Rotation
        {
            get => new Vector3(0, 0, Angle);
            set => GetComponent<NativeVehicle>().SetVehicleZAngle(value.Z);
        }

        /// <summary>
        /// Gets or sets the health of this vehicle.
        /// </summary>
        public float Health
        {
            get
            {
                GetComponent<NativeVehicle>().GetVehicleHealth(out var value);
                return value;
            }
            set => GetComponent<NativeVehicle>().SetVehicleHealth(value);
        }

        /// <summary>
        /// Gets or sets the position of this vehicle.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                GetComponent<NativeVehicle>().GetVehiclePos(out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => GetComponent<NativeVehicle>().SetVehiclePos(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// This function can be used to calculate the distance (as a float) between this vehicle and
        /// another
        /// map coordinate.
        /// This can be useful to detect how far a vehicle away is from a location.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>A float containing the distance from the point specified in the coordinates.</returns>
        public float GetDistanceFromPoint(Vector3 point)
        {
            return GetComponent<NativeVehicle>().GetVehicleDistanceFromPoint(point.X, point.Y, point.Z);
        }

        /// <summary>
        /// Checks if this vehicle is streamed in for the specified <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns><c>true</c> if this vehicle is streamed in for the specified vehicle; <c>false</c> otherwise.</returns>
        public bool IsStreamedIn(Entity player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            
            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            return GetComponent<NativeVehicle>().IsVehicleStreamedIn(player.Id);
        }

        /// <summary>
        /// Returns this vehicle's rotation on all axis as a quaternion.
        /// </summary>
        /// <param name="w">A float variable in which to store the first quaternion angle, passed by reference.</param>
        /// <param name="x">A float variable in which to store the second quaternion angle, passed by reference.</param>
        /// <param name="y">A float variable in which to store the third quaternion angle, passed by reference.</param>
        /// <param name="z">A float variable in which to store the fourth quaternion angle, passed by reference.</param>
        public void GetRotationQuaternion(out float w, out float x, out float y,
            out float z)
        {
            GetComponent<NativeVehicle>().GetVehicleRotationQuat(out w, out x, out y, out z);
        }

        /// <summary>
        /// Returns this vehicle's rotation on all axis as a quaternion.
        /// </summary>
        /// <returns>The rotation in a <see cref="Quaternion" /> structure.</returns>
        public Quaternion GetRotationQuaternion()
        {
            // TODO: To property
            GetRotationQuaternion(out var x, out var y, out var z, out var w);
            return new Quaternion(x, y, z, w);
        }

        /// <summary>
        /// Set the parameters of this vehicle for a player.
        /// </summary>
        /// <param name="player">The player to set this vehicle's parameters for.</param>
        /// <param name="objective">False to disable the objective or True to show it.</param>
        /// <param name="doorsLocked">False to unlock the doors or True to lock them.</param>
        public void SetParametersForPlayer(Entity player, bool objective,
            bool doorsLocked)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            
            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            GetComponent<NativeVehicle>().SetVehicleParamsForPlayer(player.Id, objective, doorsLocked);
        }

        /// <summary>
        /// Sets this vehicle's parameters for all players.
        /// </summary>
        /// <param name="engine">Toggle the engine status on or off.</param>
        /// <param name="lights">Toggle the lights on or off.</param>
        /// <param name="alarm">Toggle the vehicle alarm on or off.</param>
        /// <param name="doors">Toggle the lock status of the doors.</param>
        /// <param name="bonnet">Toggle the bonnet to be open or closed.</param>
        /// <param name="boot">Toggle the boot to be open or closed.</param>
        /// <param name="objective">Toggle the objective status for the vehicle on or off.</param>
        public void SetParameters(bool engine, bool lights, bool alarm, bool doors, bool bonnet, bool boot,
            bool objective)
        {
            GetComponent<NativeVehicle>().SetVehicleParamsEx(engine ? 1 : 0, lights ? 1 : 0, alarm ? 1 : 0,
                doors ? 1 : 0, bonnet ? 1 : 0,
                boot ? 1 : 0, objective ? 1 : 0);
        }

        /// <summary>
        /// Sets this vehicle's parameters for all players.
        /// </summary>
        /// <param name="engine">Toggle the engine status on or off.</param>
        /// <param name="lights">Toggle the lights on or off.</param>
        /// <param name="alarm">Toggle the vehicle alarm on or off.</param>
        /// <param name="doors">Toggle the lock status of the doors.</param>
        /// <param name="bonnet">Toggle the bonnet to be open or closed.</param>
        /// <param name="boot">Toggle the boot to be open or closed.</param>
        /// <param name="objective">Toggle the objective status for the vehicle on or off.</param>
        public void SetParameters(VehicleParameterValue engine, VehicleParameterValue lights,
            VehicleParameterValue alarm, VehicleParameterValue doors, VehicleParameterValue bonnet,
            VehicleParameterValue boot,
            VehicleParameterValue objective)
        {
            GetComponent<NativeVehicle>().SetVehicleParamsEx((int) engine, (int) lights, (int) alarm, (int) doors,
                (int) bonnet,
                (int) boot,
                (int) objective);
        }

        /// <summary>
        /// Gets this vehicle's parameters.
        /// </summary>
        /// <param name="engine">Get the engine status. If on the engine is running.</param>
        /// <param name="lights">Get the vehicle's lights' state. If on the lights are on.</param>
        /// <param name="alarm">Get the vehicle's alarm state. If on the alarm is (or was) sounding.</param>
        /// <param name="doors">Get the lock status of the doors. If on the doors are locked.</param>
        /// <param name="bonnet">Get the bonnet/hood status. If on it is open.</param>
        /// <param name="boot">Get the boot/trunk status. If on it is open.</param>
        /// <param name="objective">Get the objective status. If on the objective is on.</param>
        public void GetParameters(out VehicleParameterValue engine, out VehicleParameterValue lights,
            out VehicleParameterValue alarm, out VehicleParameterValue doors, out VehicleParameterValue bonnet,
            out VehicleParameterValue boot, out VehicleParameterValue objective)
        {
            GetComponent<NativeVehicle>().GetVehicleParamsEx(out var tmpEngine, out var tmpLights, out var tmpAlarm,
                out var tmpDoors, out var tmpBonnet,
                out var tmpBoot, out var tmpObjective);

            engine = (VehicleParameterValue) tmpEngine;
            lights = (VehicleParameterValue) tmpLights;
            alarm = (VehicleParameterValue) tmpAlarm;
            doors = (VehicleParameterValue) tmpDoors;
            bonnet = (VehicleParameterValue) tmpBonnet;
            boot = (VehicleParameterValue) tmpBoot;
            objective = (VehicleParameterValue) tmpObjective;
        }

        private static bool ToBool(VehicleParameterValue value, bool defaultValue = false)
        {
            return value == VehicleParameterValue.Unset ? defaultValue : value == VehicleParameterValue.On;
        }

        /// <summary>
        /// Gets this vehicle's parameters.
        /// </summary>
        /// <param name="engine">Get the engine status. If true the engine is running.</param>
        /// <param name="lights">Get the vehicle's lights' state. If true the lights are on.</param>
        /// <param name="alarm">Get the vehicle's alarm state. If true the alarm is (or was) sounding.</param>
        /// <param name="doors">Get the lock status of the doors. If true the doors are locked.</param>
        /// <param name="bonnet">Get the bonnet/hood status. If true it is open.</param>
        /// <param name="boot">Get the boot/trunk status. If true it is open.</param>
        /// <param name="objective">Get the objective status. If true the objective is on.</param>
        public void GetParameters(out bool engine, out bool lights, out bool alarm,
            out bool doors, out bool bonnet, out bool boot, out bool objective)
        {
            GetParameters(out var tmpEngine, out var tmpLights, out var tmpAlarm, out var tmpDoors, out var tmpBonnet,
                out var tmpBoot,
                out VehicleParameterValue tmpObjective);

            engine = ToBool(tmpEngine);
            lights = ToBool(tmpLights);
            alarm = ToBool(tmpAlarm);
            doors = ToBool(tmpDoors);
            bonnet = ToBool(tmpBonnet);
            boot = ToBool(tmpBoot);
            objective = ToBool(tmpObjective);
        }

        /// <summary>
        /// Sets the doors parameters.
        /// </summary>
        /// <param name="driver">if set to <c>true</c> the driver side door is open.</param>
        /// <param name="passenger">if set to <c>true</c> the passenger side door is open.</param>
        /// <param name="backLeft">if set to <c>true</c> the back-left door is open.</param>
        /// <param name="backRight">if set to <c>true</c> the back-right door is open.</param>
        public void SetDoorsParameters(bool driver, bool passenger, bool backLeft, bool backRight)
        {
            GetComponent<NativeVehicle>()
                .SetVehicleParamsCarDoors(driver ? 1 : 0, passenger ? 1 : 0, backLeft ? 1 : 0, backRight ? 1 : 0);
        }

        /// <summary>
        /// Sets the doors parameters.
        /// </summary>
        /// <param name="driver">if on the driver side door is open.</param>
        /// <param name="passenger">if on the passenger side door is open.</param>
        /// <param name="backLeft">if on the back-left door is open.</param>
        /// <param name="backRight">if on the back-right door is open.</param>
        public void SetDoorsParameters(VehicleParameterValue driver, VehicleParameterValue passenger,
            VehicleParameterValue backLeft, VehicleParameterValue backRight)
        {
            GetComponent<NativeVehicle>()
                .SetVehicleParamsCarDoors((int) driver, (int) passenger, (int) backLeft, (int) backRight);
        }

        /// <summary>
        /// Gets the doors parameters.
        /// </summary>
        /// <param name="driver">if on the driver side door is open.</param>
        /// <param name="passenger">if on the passenger side door is open.</param>
        /// <param name="backLeft">if on the back-left door is open.</param>
        /// <param name="backRight">if on the back-right door is open.</param>
        public void GetDoorsParameters(out VehicleParameterValue driver, out VehicleParameterValue passenger,
            out VehicleParameterValue backLeft, out VehicleParameterValue backRight)
        {
            GetComponent<NativeVehicle>().GetVehicleParamsCarDoors(out var tmpDriver, out var tmpPassenger,
                out var tmpBackLeft, out var tmpBackRight);

            driver = (VehicleParameterValue) tmpDriver;
            passenger = (VehicleParameterValue) tmpPassenger;
            backLeft = (VehicleParameterValue) tmpBackLeft;
            backRight = (VehicleParameterValue) tmpBackRight;
        }

        /// <summary>
        /// Gets the doors parameters.
        /// </summary>
        /// <param name="driver">if true the driver side door is open.</param>
        /// <param name="passenger">if true the passenger side door is open.</param>
        /// <param name="backLeft">if true the back-left door is open.</param>
        /// <param name="backRight">if true the back-right door is open.</param>
        public void GetDoorsParameters(out bool driver, out bool passenger, out bool backLeft,
            out bool backRight)
        {
            GetDoorsParameters(out var tmpDriver, out var tmpPassenger, out var tmpBackLeft,
                out VehicleParameterValue tmpBackRight);

            driver = ToBool(tmpDriver);
            passenger = ToBool(tmpPassenger);
            backLeft = ToBool(tmpBackLeft);
            backRight = ToBool(tmpBackRight);
        }

        /// <summary>
        /// Sets the windows parameters.
        /// </summary>
        /// <param name="driver">if set to <c>true</c> the driver side window is closed.</param>
        /// <param name="passenger">if set to <c>true</c> the passenger side window is closed.</param>
        /// <param name="backLeft">if set to <c>true</c> the back-left window is closed.</param>
        /// <param name="backRight">if set to <c>true</c> the back-right window is closed.</param>
        public void SetWindowsParameters(bool driver, bool passenger, bool backLeft, bool backRight)
        {
            GetComponent<NativeVehicle>().SetVehicleParamsCarWindows(driver ? 1 : 0, passenger ? 1 : 0,
                backLeft ? 1 : 0,
                backRight ? 1 : 0);
        }

        /// <summary>
        /// Sets the windows parameters.
        /// </summary>
        /// <param name="driver">if on the driver side window is closed.</param>
        /// <param name="passenger">if on the passenger side window is closed.</param>
        /// <param name="backLeft">if on the back-left window is closed.</param>
        /// <param name="backRight">if on the back-right window is closed.</param>
        public void SetWindowsParameters(VehicleParameterValue driver, VehicleParameterValue passenger,
            VehicleParameterValue backLeft, VehicleParameterValue backRight)
        {
            GetComponent<NativeVehicle>()
                .SetVehicleParamsCarWindows((int) driver, (int) passenger, (int) backLeft, (int) backRight);
        }

        /// <summary>
        /// Gets the windows parameters.
        /// </summary>
        /// <param name="driver">if on the driver side window is closed.</param>
        /// <param name="passenger">if on the passenger side window is closed.</param>
        /// <param name="backLeft">if on the back-left window is closed.</param>
        /// <param name="backRight">if on the back-right window is closed.</param>
        public void GetWindowsParameters(out VehicleParameterValue driver, out VehicleParameterValue passenger,
            out VehicleParameterValue backLeft, out VehicleParameterValue backRight)
        {
            GetComponent<NativeVehicle>().GetVehicleParamsCarWindows(out var tmpDriver, out var tmpPassenger,
                out var tmpBackLeft, out var tmpBackRight);

            driver = (VehicleParameterValue) tmpDriver;
            passenger = (VehicleParameterValue) tmpPassenger;
            backLeft = (VehicleParameterValue) tmpBackLeft;
            backRight = (VehicleParameterValue) tmpBackRight;
        }

        /// <summary>
        /// Gets the windows parameters.
        /// </summary>
        /// <param name="driver">if true the driver side window is closed.</param>
        /// <param name="passenger">if true the passenger side window is closed.</param>
        /// <param name="backLeft">if true the back-left window is closed.</param>
        /// <param name="backRight">if true the back-right window is closed.</param>
        public void GetWindowsParameters(out bool driver, out bool passenger, out bool backLeft,
            out bool backRight)
        {
            GetWindowsParameters(out var tmpDriver, out var tmpPassenger, out var tmpBackLeft,
                out VehicleParameterValue tmpBackRight);

            driver = ToBool(tmpDriver, true); // unset is most commonly also closed
            passenger = ToBool(tmpPassenger, true);
            backLeft = ToBool(tmpBackLeft, true);
            backRight = ToBool(tmpBackRight, true);
        }

        /// <summary>
        /// Sets this vehicle back to the position at where it was created.
        /// </summary>
        public void Respawn()
        {
            GetComponent<NativeVehicle>().SetVehicleToRespawn();
        }

        /// <summary>
        /// Links this vehicle to the interior. This can be used for example for an arena/stadium.
        /// </summary>
        /// <param name="interiorId">Interior ID.</param>
        public void LinkToInterior(int interiorId)
        {
            GetComponent<NativeVehicle>().LinkVehicleToInterior(interiorId);
        }

        /// <summary>
        /// Adds a 'component' (often referred to as a 'mod' (modification)) to this Vehicle.
        /// </summary>
        /// <param name="componentId">The ID of the component to add to the vehicle.</param>
        public void AddComponent(int componentId)
        {
            GetComponent<NativeVehicle>().AddVehicleComponent(componentId);
        }

        /// <summary>
        /// Remove a component from the vehicle.
        /// </summary>
        /// <param name="componentId">ID of the component to remove.</param>
        public void RemoveComponent(int componentId)
        {
            GetComponent<NativeVehicle>().RemoveVehicleComponent(componentId);
        }

        /// <summary>
        /// Change this vehicle's primary and secondary colors.
        /// </summary>
        /// <param name="color1">The new vehicle's primary Color ID.</param>
        /// <param name="color2">The new vehicle's secondary Color ID.</param>
        public void ChangeColor(int color1, int color2)
        {
            GetComponent<NativeVehicle>().ChangeVehicleColor(color1, color2);
        }

        /// <summary>
        /// Change this vehicle's paintjob (for plain colors see <see cref="ChangeColor" />).
        /// </summary>
        /// <param name="paintjobId">The ID of the paintjob to apply. Use 3 to remove a paintjob.</param>
        public void ChangePaintjob(int paintjobId)
        {
            GetComponent<NativeVehicle>().ChangeVehiclePaintjob(paintjobId);
        }

        /// <summary>
        /// Set this vehicle's numberplate, which supports color embedding.
        /// </summary>
        /// <param name="numberplate">The text that should be displayed on the numberplate. Color Embedding> is supported.</param>
        public void SetNumberPlate(string numberplate)
        {
            if (numberplate == null) throw new ArgumentNullException(nameof(numberplate));

            GetComponent<NativeVehicle>().SetVehicleNumberPlate(numberplate);
        }

        /// <summary>
        /// Retrieves the installed component ID from this vehicle in a specific slot.
        /// </summary>
        /// <param name="slot">The component slot to check for components.</param>
        /// <returns>The ID of the component installed in the specified slot.</returns>
        public int GetComponentInSlot(CarModType slot)
        {
            return GetComponent<NativeVehicle>().GetVehicleComponentInSlot((int) slot);
        }

        /// <summary>
        /// Fully repairs this vehicle, including visual damage (bumps, dents, scratches, popped tires
        /// etc.).
        /// </summary>
        public void Repair()
        {
            GetComponent<NativeVehicle>().RepairVehicle();
        }

        /// <summary>
        /// Sets the angular velocity of this vehicle.
        /// </summary>
        /// <param name="velocity">The amount of velocity in the angular directions.</param>
        public void SetAngularVelocity(Vector3 velocity)
        {
            GetComponent<NativeVehicle>().SetVehicleAngularVelocity(velocity.X, velocity.Y, velocity.Z);
        }

        /// <summary>
        /// Retrieve the damage statuses of this vehicle.
        /// </summary>
        /// <param name="panels">A variable to store the panel damage data in, passed by reference.</param>
        /// <param name="doors">A variable to store the door damage data in, passed by reference.</param>
        /// <param name="lights">A variable to store the light damage data in, passed by reference.</param>
        /// <param name="tires">A variable to store the tire damage data in, passed by reference.</param>
        public void GetDamageStatus(out int panels, out int doors, out int lights, out int tires)
        {
            GetComponent<NativeVehicle>().GetVehicleDamageStatus(out panels, out doors, out lights, out tires);
        }

        /// <summary>
        /// Sets the various visual damage statuses of this vehicle, such as popped tires, broken lights and
        /// damaged panels.
        /// </summary>
        /// <param name="panels">A set of bits containing the panel damage status.</param>
        /// <param name="doors">A set of bits containing the door damage status.</param>
        /// <param name="lights">A set of bits containing the light damage status.</param>
        /// <param name="tires">A set of bits containing the tire damage status.</param>
        public void UpdateDamageStatus(int panels, int doors, int lights, int tires)
        {
            GetComponent<NativeVehicle>().UpdateVehicleDamageStatus(panels, doors, lights, tires);
        }


        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            GetComponent<NativeVehicle>().DestroyVehicle();
        }
    }
}