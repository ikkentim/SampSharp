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
using System;
using System.Collections.Generic;
using System.Linq;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Factories;
using SampSharp.GameMode.Helpers;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a SA-MP vehicle.
    /// </summary>
    public partial class BaseVehicle : IdentifiedPool<BaseVehicle>, IWorldObject
    {
        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        /// <summary>
        ///     Maximum number of vehicles which can exist.
        /// </summary>
        public const int Max = 2000;

        #region Overrides of Object

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Vehicle(Id:{Id}, Model: {Model})";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets an instance of <see cref="VehicleModelInfo" /> about this <see cref="BaseVehicle" />.
        /// </summary>
        public VehicleModelInfo ModelInfo => VehicleModelInfo.ForVehicle(this);

        /// <summary>
        ///     Gets the driver of this <see cref="BaseVehicle" />.
        /// </summary>
        public BasePlayer Driver
        {
            get { return BasePlayer.All.FirstOrDefault(p => p.Vehicle == this && p.VehicleSeat == 0); }
        }

        /// <summary>
        ///     Gets the passengers of this <see cref="BaseVehicle" />. (not the driver)
        /// </summary>
        public IEnumerable<BasePlayer> Passengers
        {
            get { return BasePlayer.All.Where(p => p.Vehicle == this).Where(player => player.VehicleSeat > 0); }
        }

        /// <summary>
        ///     Gets the size of the vehicles pool.
        /// </summary>
        public static int PoolSize => VehicleInternal.Instance.GetVehiclePoolSize();

        #endregion

        #region Vehicles native properties

        /// <summary>
        ///     Gets whether this <see cref="BaseVehicle" /> has been created and still is alive.
        /// </summary>
        public virtual bool IsValid => VehicleInternal.Instance.IsValidVehicle(Id);

        /// <summary>
        ///     Gets or sets the Z angle of this <see cref="BaseVehicle" />.
        /// </summary>
        public virtual float Angle
        {
            get
            {
                float angle;
                VehicleInternal.Instance.GetVehicleZAngle(Id, out angle);
                return angle;
            }
            set { VehicleInternal.Instance.SetVehicleZAngle(Id, value); }
        }

        /// <summary>
        ///     Gets the model ID of this <see cref="BaseVehicle" />.
        /// </summary>
        public virtual VehicleModelType Model => (VehicleModelType) VehicleInternal.Instance.GetVehicleModel(Id);

        /// <summary>
        ///     Gets whether this <see cref="BaseVehicle" /> has a trailer attached to it.
        /// </summary>
        public virtual bool HasTrailer => VehicleInternal.Instance.IsTrailerAttachedToVehicle(Id);

        /// <summary>
        ///     Gets or sets the the trailer attached to this <see cref="BaseVehicle" />.
        /// </summary>
        /// <returns>The trailer attached.</returns>
        public virtual BaseVehicle Trailer
        {
            get
            {
                var id = VehicleInternal.Instance.GetVehicleTrailer(Id);
                return id == 0 ? null : Find(id);
            }
            set
            {
                if (value == null)
                    VehicleInternal.Instance.DetachTrailerFromVehicle(Id);
                else
                    VehicleInternal.Instance.AttachTrailerToVehicle(value.Id, Id);
            }
        }

        /// <summary>
        ///     Gets or sets the velocity at which this <see cref="BaseVehicle" /> is moving.
        /// </summary>
        public virtual Vector3 Velocity
        {
            get
            {
                float x, y, z;
                VehicleInternal.Instance.GetVehicleVelocity(Id, out x, out y, out z);
                return new Vector3(x, y, z);
            }
            set { VehicleInternal.Instance.SetVehicleVelocity(Id, value.X, value.Y, value.Z); }
        }

        /// <summary>
        ///     Gets or sets the virtual world of this <see cref="BaseVehicle" />.
        /// </summary>
        public virtual int VirtualWorld
        {
            get { return VehicleInternal.Instance.GetVehicleVirtualWorld(Id); }
            set { VehicleInternal.Instance.SetVehicleVirtualWorld(Id, value); }
        }

        /// <summary>
        ///     Gets or sets this <see cref="BaseVehicle" />'s engine status. If True, the engine is running.
        /// </summary>
        public virtual bool Engine
        {
            get
            {
                bool value, misc;
                GetParameters(out value, out misc, out misc, out misc, out misc, out misc, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d, e, f, g;
                GetParameters(out a, out b, out c, out d, out e, out f, out g);
                SetParameters(value ? VehicleParameterValue.On : VehicleParameterValue.Off, b, c, d, e, f, g);
            }
        }

        /// <summary>
        ///     Gets or sets this <see cref="BaseVehicle" />'s lights' state. If True the lights are on.
        /// </summary>
        public virtual bool Lights
        {
            get
            {
                bool value, misc;
                GetParameters(out misc, out value, out misc, out misc, out misc, out misc, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d, e, f, g;
                GetParameters(out a, out b, out c, out d, out e, out f, out g);
                SetParameters(a, value ? VehicleParameterValue.On : VehicleParameterValue.Off, c, d, e, f, g);
            }
        }

        /// <summary>
        ///     Gets or sets this <see cref="BaseVehicle" />'s alarm state. If True the alarm is (or was) sounding.
        /// </summary>
        public virtual bool Alarm
        {
            get
            {
                bool value, misc;
                GetParameters(out misc, out misc, out value, out misc, out misc, out misc, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d, e, f, g;
                GetParameters(out a, out b, out c, out d, out e, out f, out g);
                SetParameters(a, b, value ? VehicleParameterValue.On : VehicleParameterValue.Off, d, e, f, g);
            }
        }

        /// <summary>
        ///     Gets or sets the lock status of the doors of this <see cref="BaseVehicle" />. If True the doors are locked.
        /// </summary>
        public virtual bool Doors
        {
            get
            {
                bool value, misc;
                GetParameters(out misc, out misc, out misc, out value, out misc, out misc, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d, e, f, g;
                GetParameters(out a, out b, out c, out d, out e, out f, out g);
                SetParameters(a, b, c, value ? VehicleParameterValue.On : VehicleParameterValue.Off, e, f, g);
            }
        }

        /// <summary>
        ///     Gets or sets the bonnet/hood status of this <see cref="BaseVehicle" />. If True, it's open.
        /// </summary>
        public virtual bool Bonnet
        {
            get
            {
                bool value, misc;
                GetParameters(out misc, out value, out misc, out misc, out value, out misc, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d, e, f, g;
                GetParameters(out a, out b, out c, out d, out e, out f, out g);
                SetParameters(a, b, c, d, value ? VehicleParameterValue.On : VehicleParameterValue.Off, f, g);
            }
        }

        /// <summary>
        ///     Gets or sets the boot/trunk status of this <see cref="BaseVehicle" />. True means it is open.
        /// </summary>
        public virtual bool Boot
        {
            get
            {
                bool value, misc;
                GetParameters(out misc, out value, out misc, out misc, out misc, out value, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d, e, f, g;
                GetParameters(out a, out b, out c, out d, out e, out f, out g);
                SetParameters(a, b, c, d, e, value ? VehicleParameterValue.On : VehicleParameterValue.Off, g);
            }
        }

        /// <summary>
        ///     Gets or sets the objective status of this <see cref="BaseVehicle" />. True means the objective is on.
        /// </summary>
        public virtual bool Objective
        {
            get
            {
                bool value, misc;
                GetParameters(out misc, out value, out misc, out misc, out misc, out misc, out value);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d, e, f, g;
                GetParameters(out a, out b, out c, out d, out e, out f, out g);
                SetParameters(a, b, c, d, e, f, value ? VehicleParameterValue.On : VehicleParameterValue.Off);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the driver door is open.
        /// </summary>
        public virtual bool IsDriverDoorOpen
        {
            get
            {
                bool value, misc;
                GetDoorsParameters(out value, out misc, out misc, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d;
                GetDoorsParameters(out a, out b, out c, out d);
                SetDoorsParameters(value ? VehicleParameterValue.On : VehicleParameterValue.Off, b, c, d);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the passenger door is open.
        /// </summary>
        public virtual bool IsPassengerDoorOpen
        {
            get
            {
                bool value, misc;
                GetDoorsParameters(out misc, out value, out misc, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d;
                GetDoorsParameters(out a, out b, out c, out d);
                SetDoorsParameters(a, value ? VehicleParameterValue.On : VehicleParameterValue.Off, c, d);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the driver door is open.
        /// </summary>
        public virtual bool IsBackLeftDoorOpen
        {
            get
            {
                bool value, misc;
                GetDoorsParameters(out misc, out misc, out value, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d;
                GetDoorsParameters(out a, out b, out c, out d);
                SetDoorsParameters(a, b, value ? VehicleParameterValue.On : VehicleParameterValue.Off, d);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the driver door is open.
        /// </summary>
        public virtual bool IsBackRightDoorOpen
        {
            get
            {
                bool value, misc;
                GetDoorsParameters(out misc, out misc, out misc, out value);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d;
                GetDoorsParameters(out a, out b, out c, out d);
                SetDoorsParameters(a, b, c, value ? VehicleParameterValue.On : VehicleParameterValue.Off);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the driver window is closed.
        /// </summary>
        public virtual bool IsDriverWindowClosed
        {
            get
            {
                bool value, misc;
                GetWindowsParameters(out value, out misc, out misc, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d;
                GetWindowsParameters(out a, out b, out c, out d);
                SetWindowsParameters(value ? VehicleParameterValue.On : VehicleParameterValue.Off, b, c, d);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the passenger window is closed.
        /// </summary>
        public virtual bool IsPassengerWindowClosed
        {
            get
            {
                bool value, misc;
                GetWindowsParameters(out misc, out value, out misc, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d;
                GetWindowsParameters(out a, out b, out c, out d);
                SetWindowsParameters(a, value ? VehicleParameterValue.On : VehicleParameterValue.Off, c, d);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the driver window is closed.
        /// </summary>
        public virtual bool IsBackLeftWindowClosed
        {
            get
            {
                bool value, misc;
                GetWindowsParameters(out misc, out misc, out value, out misc);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d;
                GetWindowsParameters(out a, out b, out c, out d);
                SetWindowsParameters(a, b, value ? VehicleParameterValue.On : VehicleParameterValue.Off, d);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the driver window is closed.
        /// </summary>
        public virtual bool IsBackRightWindowClosed
        {
            get
            {
                bool value, misc;
                GetWindowsParameters(out misc, out misc, out misc, out value);
                return value;
            }
            set
            {
                VehicleParameterValue a, b, c, d;
                GetWindowsParameters(out a, out b, out c, out d);
                SetWindowsParameters(a, b, c, value ? VehicleParameterValue.On : VehicleParameterValue.Off);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this Vehicle's siren is on.
        /// </summary>
        public virtual bool IsSirenOn
        {
            get
            {
                AssertNotDisposed();
                return VehicleInternal.Instance.GetVehicleParamsSirenState(Id) == 1;
            }
        }

        /// <summary>
        ///     Gets or sets the rotation of this <see cref="BaseVehicle" />.
        /// </summary>
        /// <remarks>
        ///     Only the Z angle can be set!
        /// </remarks>
        public virtual Vector3 Rotation
        {
            get { return new Vector3(0, 0, Angle); }
            set { VehicleInternal.Instance.SetVehicleZAngle(Id, value.Z); }
        }

        /// <summary>
        ///     Gets or sets the health of this <see cref="BaseVehicle" />.
        /// </summary>
        public virtual float Health
        {
            get
            {
                float value;
                VehicleInternal.Instance.GetVehicleHealth(Id, out value);
                return value;
            }
            set { VehicleInternal.Instance.SetVehicleHealth(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the position of this <see cref="BaseVehicle" />.
        /// </summary>
        public virtual Vector3 Position
        {
            get
            {
                float x, y, z;
                VehicleInternal.Instance.GetVehiclePos(Id, out x, out y, out z);
                return new Vector3(x, y, z);
            }
            set { VehicleInternal.Instance.SetVehiclePos(Id, value.X, value.Y, value.Z); }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="OnSpawn" /> is being called.
        ///     This callback is called when <see cref="BaseVehicle" /> spawns.
        /// </summary>
        public event EventHandler<EventArgs> Spawn;

        /// <summary>
        ///     Occurs when the <see cref="OnDeath" /> is being called.
        ///     This callback is called when this <see cref="BaseVehicle" /> is destroyed - either by exploding or becoming
        ///     submerged in water.
        /// </summary>
        public event EventHandler<PlayerEventArgs> Died;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnter" /> is being called.
        ///     This callback is called when a <see cref="BasePlayer" /> starts to enter this <see cref="BaseVehicle" />,
        ///     meaning the player is not in vehicle yet at the time this callback is called.
        /// </summary>
        public event EventHandler<EnterVehicleEventArgs> PlayerEnter;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerExit" /> is being called.
        ///     This callback is called when a <see cref="BasePlayer" /> exits a <see cref="BaseVehicle" />.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> PlayerExit;

        /// <summary>
        ///     Occurs when the <see cref="OnMod" /> is being called.
        ///     This callback is called when this <see cref="BaseVehicle" /> is modded.
        /// </summary>
        public event EventHandler<VehicleModEventArgs> Mod;

        /// <summary>
        ///     Occurs when the <see cref="OnPaintjobApplied" /> is being called.
        ///     Called when a <see cref="BasePlayer" /> changes the paintjob of this <see cref="BaseVehicle" /> (in a modshop).
        /// </summary>
        public event EventHandler<VehiclePaintjobEventArgs> PaintjobApplied;

        /// <summary>
        ///     Occurs when the <see cref="OnResprayed" /> is being called.
        ///     The callback name is deceptive, this callback is called when a <see cref="BasePlayer" /> exits a mod shop with this
        ///     <see cref="BaseVehicle" />,
        ///     regardless of whether the vehicle's colors were changed, and is NEVER called for pay 'n' spray garages.
        /// </summary>
        public event EventHandler<VehicleResprayedEventArgs> Resprayed;

        /// <summary>
        ///     Occurs when the <see cref="OnDamageStatusUpdated" /> is being called.
        ///     This callback is called when a element of this <see cref="BaseVehicle" /> such as doors, tires, panels, or lights
        ///     get damaged.
        /// </summary>
        public event EventHandler<PlayerEventArgs> DamageStatusUpdated;

        /// <summary>
        ///     Occurs when the <see cref="OnUnoccupiedUpdate" /> is being called.
        ///     This callback is called everytime this <see cref="BaseVehicle" /> updates the server with their status while it is
        ///     unoccupied.
        /// </summary>
        public event EventHandler<UnoccupiedVehicleEventArgs> UnoccupiedUpdate;

        /// <summary>
        ///     Occurs when the <see cref="OnStreamIn" /> is being called.
        ///     Called when a <see cref="BaseVehicle" /> is streamed to a <see cref="BasePlayer" />'s client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> StreamIn;

        /// <summary>
        ///     Occurs when the <see cref="OnStreamOut" /> is being called.
        ///     This callback is called when a <see cref="BaseVehicle" /> is streamed out from some <see cref="BasePlayer" />'s
        ///     client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> StreamOut;

        /// <summary>
        ///     Occurs when the <see cref="OnTrailerUpdate" /> is being called.
        ///     This callback is called when a <see cref="BasePlayer" /> sent a trailer update about this
        ///     <see cref="BaseVehicle" />.
        /// </summary>
        public event EventHandler<TrailerEventArgs> TrailerUpdate;

        /// <summary>
        ///     Occurs when the <see cref="OnSirenStateChanged" /> is being called.
        ///     This callback is called when this <see cref="BaseVehicle" />'s siren is toggled.
        /// </summary>
        public event EventHandler<SirenStateEventArgs> SirenStateChanged;

        #endregion

        #region Vehicles natives

        /// <summary>
        ///     This function can be used to calculate the distance (as a float) between this <see cref="BaseVehicle" /> and
        ///     another
        ///     map coordinate.
        ///     This can be useful to detect how far a vehicle away is from a location.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>A float containing the distance from the point specified in the coordinates.</returns>
        public virtual float GetDistanceFromPoint(Vector3 point)
        {
            AssertNotDisposed();

            return VehicleInternal.Instance.GetVehicleDistanceFromPoint(Id, point.X, point.Y, point.Z);
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
        public static BaseVehicle Create(VehicleModelType vehicletype, Vector3 position, float rotation, int color1,
            int color2,
            int respawnDelay = -1, bool addAlarm = false)
        {
            var service = BaseMode.Instance.Services.GetService<IVehicleFactory>();

            return service?.Create(vehicletype, position, rotation, color1, color2, respawnDelay, addAlarm);
        }

        /// <summary>
        ///     Creates a static <see cref="BaseVehicle" /> in the world.
        /// </summary>
        /// <param name="vehicleType">The model for the vehicle.</param>
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
        public static BaseVehicle CreateStatic(VehicleModelType vehicleType, Vector3 position, float rotation,
            int color1, int color2,
            int respawnDelay, bool addAlarm = false)
        {
            var service = BaseMode.Instance.Services.GetService<IVehicleFactory>();

            return service?.CreateStatic(vehicleType, position, rotation, color1, color2, respawnDelay, addAlarm);
        }

        /// <summary>
        ///     Creates a static <see cref="BaseVehicle" /> in the world.
        /// </summary>
        /// <param name="vehicleType">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <returns> The <see cref="BaseVehicle" /> created.</returns>
        public static BaseVehicle CreateStatic(VehicleModelType vehicleType, Vector3 position, float rotation,
            int color1, int color2)
        {
            var service = BaseMode.Instance.Services.GetService<IVehicleFactory>();

            return service?.Create(vehicleType, position, rotation, color1, color2);
        }

        /// <summary>
        ///     Checks if this <see cref="BaseVehicle" /> is streamed in for a <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="forPlayer">The Player to check.</param>
        /// <returns>True if this vehicle is streamed in for the specified vehicle; False otherwise.</returns>
        public virtual bool IsStreamedIn(BasePlayer forPlayer)
        {
            AssertNotDisposed();

            return VehicleInternal.Instance.IsVehicleStreamedIn(Id, forPlayer.Id);
        }

        /// <summary>
        ///     Returns this <see cref="BaseVehicle" />'s rotation on all axis as a quaternion.
        /// </summary>
        /// <param name="w">A float variable in which to store the first quaternion angle, passed by reference.</param>
        /// <param name="x">A float variable in which to store the second quaternion angle, passed by reference.</param>
        /// <param name="y">A float variable in which to store the third quaternion angle, passed by reference.</param>
        /// <param name="z">A float variable in which to store the fourth quaternion angle, passed by reference.</param>
        public virtual void GetRotationQuat(out float w, out float x, out float y,
            out float z)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.GetVehicleRotationQuat(Id, out w, out x, out y, out z);
        }

        /// <summary>
        ///     Returns this <see cref="BaseVehicle" />'s rotation on all axis as a quaternion.
        /// </summary>
        /// <returns>The rotation in a <see cref="Quaternion"/> structure.</returns>
        public virtual Quaternion GetRotationQuat()
        {
            AssertNotDisposed();

            float x, y, z, w;
            GetRotationQuat(out x, out y, out z, out w);
            return new Quaternion(x, y, y, w);
        }

        /// <summary>
        ///     Set the parameters of this <see cref="BaseVehicle" /> for a <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="player">The <see cref="BasePlayer" /> to set this vehicles's parameters for.</param>
        /// <param name="objective">False to disable the objective or True to show it.</param>
        /// <param name="doorslocked">False to unlock the doors or True to lock them.</param>
        public virtual void SetParametersForPlayer(BasePlayer player, bool objective,
            bool doorslocked)
        {
            AssertNotDisposed();

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            VehicleInternal.Instance.SetVehicleParamsForPlayer(Id, player.Id, objective, doorslocked);
        }

        /// <summary>
        ///     Sets this <see cref="BaseVehicle" />'s parameters for all players.
        /// </summary>
        /// <param name="engine">Toggle the engine status on or off.</param>
        /// <param name="lights">Toggle the lights on or off.</param>
        /// <param name="alarm">Toggle the vehicle alarm on or off.</param>
        /// <param name="doors">Toggle the lock status of the doors.</param>
        /// <param name="bonnet">Toggle the bonnet to be open or closed.</param>
        /// <param name="boot">Toggle the boot to be open or closed.</param>
        /// <param name="objective">Toggle the objective status for the vehicle on or off.</param>
        public virtual void SetParameters(bool engine, bool lights, bool alarm, bool doors, bool bonnet, bool boot,
            bool objective)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.SetVehicleParamsEx(Id, engine ? 1 : 0, lights ? 1 : 0, alarm ? 1 : 0, doors ? 1 : 0, bonnet ? 1 : 0,
                boot ? 1 : 0, objective ? 1 : 0);
        }

        /// <summary>
        ///     Sets this <see cref="BaseVehicle" />'s parameters for all players.
        /// </summary>
        /// <param name="engine">Toggle the engine status on or off.</param>
        /// <param name="lights">Toggle the lights on or off.</param>
        /// <param name="alarm">Toggle the vehicle alarm on or off.</param>
        /// <param name="doors">Toggle the lock status of the doors.</param>
        /// <param name="bonnet">Toggle the bonnet to be open or closed.</param>
        /// <param name="boot">Toggle the boot to be open or closed.</param>
        /// <param name="objective">Toggle the objective status for the vehicle on or off.</param>
        public virtual void SetParameters(VehicleParameterValue engine, VehicleParameterValue lights,
            VehicleParameterValue alarm, VehicleParameterValue doors, VehicleParameterValue bonnet,
            VehicleParameterValue boot,
            VehicleParameterValue objective)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.SetVehicleParamsEx(Id, (int) engine, (int) lights, (int) alarm, (int) doors, (int) bonnet,
                (int) boot,
                (int) objective);
        }

        /// <summary>
        ///     Gets this <see cref="BaseVehicle" />'s parameters.
        /// </summary>
        /// <param name="engine">Get the engine status. If on the engine is running.</param>
        /// <param name="lights">Get the vehicle's lights' state. If on the lights are on.</param>
        /// <param name="alarm">Get the vehicle's alarm state. If on the alarm is (or was) sounding.</param>
        /// <param name="doors">Get the lock status of the doors. If on the doors are locked.</param>
        /// <param name="bonnet">Get the bonnet/hood status. If on it is open.</param>
        /// <param name="boot">Get the boot/trunk status. If on it is open.</param>
        /// <param name="objective">Get the objective status. If on the objective is on.</param>
        public virtual void GetParameters(out VehicleParameterValue engine, out VehicleParameterValue lights,
            out VehicleParameterValue alarm, out VehicleParameterValue doors, out VehicleParameterValue bonnet,
            out VehicleParameterValue boot, out VehicleParameterValue objective)
        {
            AssertNotDisposed();

            int tmpEngine, tmpLights, tmpAlarm, tmpDoors, tmpBonnet, tmpBoot, tmpObjective;
            VehicleInternal.Instance.GetVehicleParamsEx(Id, out tmpEngine, out tmpLights, out tmpAlarm, out tmpDoors, out tmpBonnet,
                out tmpBoot, out tmpObjective);

            engine = (VehicleParameterValue) tmpEngine;
            lights = (VehicleParameterValue) tmpLights;
            alarm = (VehicleParameterValue) tmpAlarm;
            doors = (VehicleParameterValue) tmpDoors;
            bonnet = (VehicleParameterValue) tmpBonnet;
            boot = (VehicleParameterValue) tmpBoot;
            objective = (VehicleParameterValue) tmpObjective;
        }

        /// <summary>
        ///     Gets this <see cref="BaseVehicle" />'s parameters.
        /// </summary>
        /// <param name="engine">Get the engine status. If true the engine is running.</param>
        /// <param name="lights">Get the vehicle's lights' state. If true the lights are on.</param>
        /// <param name="alarm">Get the vehicle's alarm state. If true the alarm is (or was) sounding.</param>
        /// <param name="doors">Get the lock status of the doors. If true the doors are locked.</param>
        /// <param name="bonnet">Get the bonnet/hood status. If true it is open.</param>
        /// <param name="boot">Get the boot/trunk status. If true it is open.</param>
        /// <param name="objective">Get the objective status. If true the objective is on.</param>
        public virtual void GetParameters(out bool engine, out bool lights, out bool alarm,
            out bool doors, out bool bonnet, out bool boot, out bool objective)
        {
            VehicleParameterValue tmpEngine, tmpLights, tmpAlarm, tmpDoors, tmpBonnet, tmpBoot, tmpObjective;
            GetParameters(out tmpEngine, out tmpLights, out tmpAlarm, out tmpDoors, out tmpBonnet, out tmpBoot,
                out tmpObjective);

            engine = tmpEngine.ToBool();
            lights = tmpLights.ToBool();
            alarm = tmpAlarm.ToBool();
            doors = tmpDoors.ToBool();
            bonnet = tmpBonnet.ToBool();
            boot = tmpBoot.ToBool();
            objective = tmpObjective.ToBool();
        }

        /// <summary>
        ///     Sets the doors parameters.
        /// </summary>
        /// <param name="driver">if set to <c>true</c> the driver side door is open.</param>
        /// <param name="passenger">if set to <c>true</c> the passenger side door is open.</param>
        /// <param name="backleft">if set to <c>true</c> the backleft door is open.</param>
        /// <param name="backright">if set to <c>true</c> the backright door is open.</param>
        public virtual void SetDoorsParameters(bool driver, bool passenger, bool backleft, bool backright)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.SetVehicleParamsCarDoors(Id, driver ? 1 : 0, passenger ? 1 : 0, backleft ? 1 : 0, backright ? 1 : 0);
        }

        /// <summary>
        ///     Sets the doors parameters.
        /// </summary>
        /// <param name="driver">if on the driver side door is open.</param>
        /// <param name="passenger">if on the passenger side door is open.</param>
        /// <param name="backleft">if on the backleft door is open.</param>
        /// <param name="backright">if on the backright door is open.</param>
        public virtual void SetDoorsParameters(VehicleParameterValue driver, VehicleParameterValue passenger,
            VehicleParameterValue backleft, VehicleParameterValue backright)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.SetVehicleParamsCarDoors(Id, (int) driver, (int) passenger, (int) backleft, (int) backright);
        }

        /// <summary>
        ///     Gets the doors parameters.
        /// </summary>
        /// <param name="driver">if on the driver side door is open.</param>
        /// <param name="passenger">if on the passenger side door is open.</param>
        /// <param name="backleft">if on the backleft door is open.</param>
        /// <param name="backright">if on the backright door is open.</param>
        public virtual void GetDoorsParameters(out VehicleParameterValue driver, out VehicleParameterValue passenger,
            out VehicleParameterValue backleft, out VehicleParameterValue backright)
        {
            AssertNotDisposed();

            int tmpDriver, tmpPassenger, tmpBackleft, tmpBackright;
            VehicleInternal.Instance.GetVehicleParamsCarDoors(Id, out tmpDriver, out tmpPassenger, out tmpBackleft, out tmpBackright);

            driver = (VehicleParameterValue) tmpDriver;
            passenger = (VehicleParameterValue) tmpPassenger;
            backleft = (VehicleParameterValue) tmpBackleft;
            backright = (VehicleParameterValue) tmpBackright;
        }

        /// <summary>
        ///     Gets the doors parameters.
        /// </summary>
        /// <param name="driver">if true the driver side door is open.</param>
        /// <param name="passenger">if true the passenger side door is open.</param>
        /// <param name="backleft">if true the backleft door is open.</param>
        /// <param name="backright">if true the backright door is open.</param>
        public virtual void GetDoorsParameters(out bool driver, out bool passenger, out bool backleft,
            out bool backright)
        {
            AssertNotDisposed();

            VehicleParameterValue tmpDriver, tmpPassenger, tmpBackleft, tmpBackright;
            GetDoorsParameters(out tmpDriver, out tmpPassenger, out tmpBackleft, out tmpBackright);

            driver = tmpDriver.ToBool();
            passenger = tmpPassenger.ToBool();
            backleft = tmpBackleft.ToBool();
            backright = tmpBackright.ToBool();
        }

        /// <summary>
        ///     Sets the windows parameters.
        /// </summary>
        /// <param name="driver">if set to <c>true</c> the driver side window is closed.</param>
        /// <param name="passenger">if set to <c>true</c> the passenger side window is closed.</param>
        /// <param name="backleft">if set to <c>true</c> the backleft window is closed.</param>
        /// <param name="backright">if set to <c>true</c> the backright window is closed.</param>
        public virtual void SetWindowsParameters(bool driver, bool passenger, bool backleft, bool backright)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.SetVehicleParamsCarWindows(Id, driver ? 1 : 0, passenger ? 1 : 0, backleft ? 1 : 0,
                backright ? 1 : 0);
        }

        /// <summary>
        ///     Sets the windows parameters.
        /// </summary>
        /// <param name="driver">if on the driver side window is closed.</param>
        /// <param name="passenger">if on the passenger side window is closed.</param>
        /// <param name="backleft">if on the backleft window is closed.</param>
        /// <param name="backright">if on the backright window is closed.</param>
        public virtual void SetWindowsParameters(VehicleParameterValue driver, VehicleParameterValue passenger,
            VehicleParameterValue backleft, VehicleParameterValue backright)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.SetVehicleParamsCarWindows(Id, (int) driver, (int) passenger, (int) backleft, (int) backright);
        }

        /// <summary>
        ///     Gets the windows parameters.
        /// </summary>
        /// <param name="driver">if on the driver side window is closed.</param>
        /// <param name="passenger">if on the passenger side window is closed.</param>
        /// <param name="backleft">if on the backleft window is closed.</param>
        /// <param name="backright">if on the backright window is closed.</param>
        public virtual void GetWindowsParameters(out VehicleParameterValue driver, out VehicleParameterValue passenger,
            out VehicleParameterValue backleft, out VehicleParameterValue backright)
        {
            AssertNotDisposed();

            int tmpDriver, tmpPassenger, tmpBackleft, tmpBackright;
            VehicleInternal.Instance.GetVehicleParamsCarWindows(Id, out tmpDriver, out tmpPassenger, out tmpBackleft, out tmpBackright);

            driver = (VehicleParameterValue) tmpDriver;
            passenger = (VehicleParameterValue) tmpPassenger;
            backleft = (VehicleParameterValue) tmpBackleft;
            backright = (VehicleParameterValue) tmpBackright;
        }

        /// <summary>
        ///     Gets the windows parameters.
        /// </summary>
        /// <param name="driver">if true the driver side window is closed.</param>
        /// <param name="passenger">if true the passenger side window is closed.</param>
        /// <param name="backleft">if true the backleft window is closed.</param>
        /// <param name="backright">if true the backright window is closed.</param>
        public virtual void GetWindowsParameters(out bool driver, out bool passenger, out bool backleft,
            out bool backright)
        {
            AssertNotDisposed();

            VehicleParameterValue tmpDriver, tmpPassenger, tmpBackleft, tmpBackright;
            GetWindowsParameters(out tmpDriver, out tmpPassenger, out tmpBackleft, out tmpBackright);

            driver = tmpDriver.ToBool(true); // unset is most commonly also closed
            passenger = tmpPassenger.ToBool(true);
            backleft = tmpBackleft.ToBool(true);
            backright = tmpBackright.ToBool(true);
        }

        /// <summary>
        ///     Sets this <see cref="BaseVehicle" /> back to the position at where it was created.
        /// </summary>
        public virtual void Respawn()
        {
            AssertNotDisposed();

            VehicleInternal.Instance.SetVehicleToRespawn(Id);
        }

        /// <summary>
        ///     Links this <see cref="BaseVehicle" /> to the interior. This can be used for example for an arena/stadium.
        /// </summary>
        /// <param name="interiorid">Interior ID.</param>
        public virtual void LinkToInterior(int interiorid)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.LinkVehicleToInterior(Id, interiorid);
        }

        /// <summary>
        ///     Adds a 'component' (often referred to as a 'mod' (modification)) to this Vehicle.
        /// </summary>
        /// <param name="componentid">The ID of the component to add to the vehicle.</param>
        public virtual void AddComponent(int componentid)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.AddVehicleComponent(Id, componentid);
        }

        /// <summary>
        ///     Remove a component from the <see cref="BaseVehicle" />.
        /// </summary>
        /// <param name="componentid">ID of the component to remove.</param>
        public virtual void RemoveComponent(int componentid)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.RemoveVehicleComponent(Id, componentid);
        }

        /// <summary>
        ///     Change this <see cref="BaseVehicle" />'s primary and secondary colors.
        /// </summary>
        /// <param name="color1">The new vehicle's primary Color ID.</param>
        /// <param name="color2">The new vehicle's secondary Color ID.</param>
        public virtual void ChangeColor(int color1, int color2)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.ChangeVehicleColor(Id, color1, color2);
        }

        /// <summary>
        ///     Change this <see cref="BaseVehicle" />'s paintjob (for plain colors see <see cref="ChangeColor" />).
        /// </summary>
        /// <param name="paintjobid">The ID of the Paintjob to apply. Use 3 to remove a paintjob.</param>
        public virtual void ChangePaintjob(int paintjobid)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.ChangeVehiclePaintjob(Id, paintjobid);
        }

        /// <summary>
        ///     Set this <see cref="BaseVehicle" />'s numberplate, which supports olor embedding.
        /// </summary>
        /// <param name="numberplate">The text that should be displayed on the numberplate. Color Embedding> is supported.</param>
        public virtual void SetNumberPlate(string numberplate)
        {
            if (numberplate == null) throw new ArgumentNullException(nameof(numberplate));

            AssertNotDisposed();

            VehicleInternal.Instance.SetVehicleNumberPlate(Id, numberplate);
        }

        /// <summary>
        ///     Retrieves the installed component ID from this <see cref="BaseVehicle" /> in a specific slot.
        /// </summary>
        /// <param name="slot">The component slot to check for components.</param>
        /// <returns>The ID of the component installed in the specified slot.</returns>
        public virtual int GetComponentInSlot(CarModType slot)
        {
            AssertNotDisposed();

            return VehicleInternal.Instance.GetVehicleComponentInSlot(Id, (int) slot);
        }

        /// <summary>
        ///     Find out what type of component a certain ID is.
        /// </summary>
        /// <param name="componentid">The component ID to check.</param>
        /// <returns>The component slot ID of the specified component.</returns>
        public static int GetComponentType(int componentid)
        {
            return VehicleInternal.Instance.GetVehicleComponentType(componentid);
        }

        /// <summary>
        ///     Fully repairs this <see cref="BaseVehicle" />, including visual damage (bumps, dents, scratches, popped tires
        ///     etc.).
        /// </summary>
        public virtual void Repair()
        {
            AssertNotDisposed();

            VehicleInternal.Instance.RepairVehicle(Id);
        }

        /// <summary>
        ///     Sets the angular velocity of this <see cref="BaseVehicle" />.
        /// </summary>
        /// <param name="velocity">The amount of velocity in the angular directions.</param>
        public virtual void SetAngularVelocity(Vector3 velocity)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.SetVehicleAngularVelocity(Id, velocity.X, velocity.Y, velocity.Z);
        }

        /// <summary>
        ///     Retrieve the damage statuses of this <see cref="BaseVehicle" />.
        /// </summary>
        /// <param name="panels">A variable to store the panel damage data in, passed by reference.</param>
        /// <param name="doors">A variable to store the door damage data in, passed by reference.</param>
        /// <param name="lights">A variable to store the light damage data in, passed by reference.</param>
        /// <param name="tires">A variable to store the tire damage data in, passed by reference.</param>
        public virtual void GetDamageStatus(out int panels, out int doors, out int lights, out int tires)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.GetVehicleDamageStatus(Id, out panels, out doors, out lights, out tires);
        }

        /// <summary>
        ///     Sets the various visual damage statuses of this <see cref="BaseVehicle" />, such as popped tires, broken lights and
        ///     damaged panels.
        /// </summary>
        /// <param name="panels">A set of bits containing the panel damage status.</param>
        /// <param name="doors">A set of bits containing the door damage status.</param>
        /// <param name="lights">A set of bits containing the light damage status.</param>
        /// <param name="tires">A set of bits containing the tire damage status.</param>
        public virtual void UpdateDamageStatus(int panels, int doors, int lights, int tires)
        {
            AssertNotDisposed();

            VehicleInternal.Instance.UpdateVehicleDamageStatus(Id, panels, doors, lights, tires);
        }

        /// <summary>
        ///     Retrieve information about a specific vehicle model such as the size or position of seats.
        /// </summary>
        /// <param name="model">The vehicle model to get info of.</param>
        /// <param name="infotype">The type of information to retrieve.</param>
        /// <returns>The offset vector.</returns>
        public static Vector3 GetModelInfo(VehicleModelType model, VehicleModelInfoType infotype)
        {
            float x, y, z;
            VehicleInternal.Instance.GetVehicleModelInfo((int) model, (int) infotype, out x, out y, out z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Removes this instance from the pool.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (IsValid)
                VehicleInternal.Instance.DestroyVehicle(Id);
        }

        #endregion

        #region Event raisers

        /// <summary>
        ///     Raises the <see cref="Spawn" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        public virtual void OnSpawn(EventArgs e)
        {
            Spawn?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Died" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnDeath(PlayerEventArgs e)
        {
            Died?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnter" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EnterVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnPlayerEnter(EnterVehicleEventArgs e)
        {
            PlayerEnter?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerExit" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnPlayerExit(PlayerVehicleEventArgs e)
        {
            PlayerExit?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Mod" /> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleModEventArgs" /> that contains the event data. </param>
        public virtual void OnMod(VehicleModEventArgs e)
        {
            Mod?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PaintjobApplied" /> event.
        /// </summary>
        /// <param name="e">An <see cref="VehiclePaintjobEventArgs" /> that contains the event data. </param>
        public virtual void OnPaintjobApplied(VehiclePaintjobEventArgs e)
        {
            PaintjobApplied?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Resprayed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleResprayedEventArgs" /> that contains the event data. </param>
        public virtual void OnResprayed(VehicleResprayedEventArgs e)
        {
            Resprayed?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="DamageStatusUpdated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnDamageStatusUpdated(PlayerEventArgs e)
        {
            DamageStatusUpdated?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="UnoccupiedUpdate" /> event.
        /// </summary>
        /// <param name="e">An <see cref="UnoccupiedVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnUnoccupiedUpdate(UnoccupiedVehicleEventArgs e)
        {
            UnoccupiedUpdate?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StreamIn" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnStreamIn(PlayerEventArgs e)
        {
            StreamIn?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StreamOut" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnStreamOut(PlayerEventArgs e)
        {
            StreamOut?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="TrailerUpdate" /> event.
        /// </summary>
        /// <param name="args">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnTrailerUpdate(TrailerEventArgs args)
        {
            TrailerUpdate?.Invoke(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="SirenStateChanged" /> event.
        /// </summary>
        /// <param name="args">The <see cref="SirenStateEventArgs" /> instance containing the event data.</param>
        public virtual void OnSirenStateChanged(SirenStateEventArgs args)
        {
            SirenStateChanged?.Invoke(this, args);
        }

        #endregion
    }
}