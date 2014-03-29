// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Collections.Generic;
using System.Linq;
using GameMode.Definitions;
using GameMode.Events;

namespace GameMode.World
{
    public class Vehicle : IWorldObject, IDisposable
    {
        #region Fields

        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no vehicle matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.InvalidVehicleId;

        /// <summary>
        ///     Contains all instances of Vehicles.
        /// </summary>
        protected static List<Vehicle> Instances = new List<Vehicle>();

        #endregion

        #region Factories

        /// <summary>
        ///     Returns an instance of <see cref="Vehicle" /> that deals with <paramref name="vehicleId" />.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle we are dealing with.</param>
        /// <returns>An instance of <see cref="Vehicle" />.</returns>
        public static Vehicle Find(int vehicleId)
        {
            //Find player in memory or initialize new player
            return Instances.FirstOrDefault(p => p.VehicleId == vehicleId) ?? new Vehicle(vehicleId);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initalizes a new instance of the Vehicle class.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to initialize.</param>
        protected Vehicle(int vehicleId)
        {
            VehicleId = vehicleId;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the ID of this Vehicle.
        /// </summary>
        public int VehicleId { get; private set; }

        /// <summary>
        ///     Gets a readonly set of all <see cref="Vehicle" /> instances.
        /// </summary>
        public static IReadOnlyCollection<Vehicle> All
        {
            get { return Instances.AsReadOnly(); }
        }

        #endregion

        #region Vehicles natives

        /// <summary>
        ///     Gets whether this Vehicle has been created and still is alive.
        /// </summary>
        public virtual bool IsValid
        {
            get { return Native.IsValidVehicle(VehicleId); }
        }

        /// <summary>
        ///     Gets or sets the position of this Vehicle.
        /// </summary>
        public virtual Vector Position
        {
            get { return Native.GetVehiclePos(VehicleId); }
            set { Native.SetVehiclePos(VehicleId, value); }
        }

        /// <summary>
        ///     Gets or sets the rotation of this Vehicle.
        /// </summary>
        /// <remarks>
        ///     Only the Z angle can be set!
        /// </remarks>
        public virtual Vector Rotation
        {
            get { return new Vector(0, 0, Angle); }
            set { Native.SetVehicleZAngle(VehicleId, value.Z); }
        }

        /// <summary>
        ///     Gets or sets the Z angle of this Vehicle.
        /// </summary>
        public virtual float Angle
        {
            get { return Native.GetVehicleZAngle(VehicleId); }
            set { Native.SetVehicleZAngle(VehicleId, value); }
        }

        /// <summary>
        ///     Gets the model ID of this Vehicle.
        /// </summary>
        public virtual int Model
        {
            get { return Native.GetVehicleModel(VehicleId); }
        }

        /// <summary>
        ///     Gets whether this Vehicle has a trailer attached to it.
        /// </summary>
        public virtual bool HasTrailer
        {
            get { return Native.IsTrailerAttachedToVehicle(VehicleId); }
        }

        /// <summary>
        ///     Gets or sets the the trailer attached to this Vehicle.
        /// </summary>
        /// <returns>The trailer attached.</returns>
        public virtual Vehicle Trailer
        {
            get
            {
                int id = Native.GetVehicleTrailer(VehicleId);
                return id == 0 ? null : Find(id);
            }
            set
            {
                if (value == null)
                    Native.DetachTrailerFromVehicle(VehicleId);
                else
                    Native.AttachTrailerToVehicle(value.VehicleId, VehicleId);
            }
        }

        /// <summary>
        ///     Gets or sets the velocity at which this Vehicle is moving.
        /// </summary>
        public virtual Vector Velocity
        {
            get { return Native.GetVehicleVelocity(VehicleId); }
            set { Native.SetVehicleVelocity(VehicleId, value); }
        }

        /// <summary>
        ///     Gets or sets the virtual world of this Vehicle.
        /// </summary>
        public virtual int VirtualWorld
        {
            get { return Native.GetVehicleVirtualWorld(VehicleId); }
            set { Native.SetVehicleVirtualWorld(VehicleId, value); }
        }

        /// <summary>
        ///     Gets this Vehicle's engine status. If True, the engine is running.
        /// </summary>
        public virtual bool Engine
        {
            get
            {
                bool value, misc;
                GetParams(out value, out misc, out misc, out misc, out misc, out misc, out misc);
                return value;
            }
        }

        /// <summary>
        ///     Gets this Vehicle's lights' state. If True the lights are on.
        /// </summary>
        public virtual bool Lights
        {
            get
            {
                bool value, misc;
                GetParams(out misc, out value, out misc, out misc, out misc, out misc, out misc);
                return value;
            }
        }

        /// <summary>
        ///     Gets this Vehicle's alarm state. If True the alarm is (or was) sounding.
        /// </summary>
        public virtual bool Alarm
        {
            get
            {
                bool value, misc;
                GetParams(out misc, out misc, out value, out misc, out misc, out misc, out misc);
                return value;
            }
        }

        /// <summary>
        ///     Gets the lock status of the doors of this Vehicle. If True the doors are locked.
        /// </summary>
        public virtual bool Doors
        {
            get
            {
                bool value, misc;
                GetParams(out misc, out misc, out misc, out value, out misc, out misc, out misc);
                return value;
            }
        }

        /// <summary>
        ///     Gets the bonnet/hood status of this Vehicle. If True, it's open.
        /// </summary>
        public virtual bool Bonnet
        {
            get
            {
                bool value, misc;
                GetParams(out misc, out value, out misc, out misc, out value, out misc, out misc);
                return value;
            }
        }

        /// <summary>
        ///     Gets the boot/trunk status of this Vehicle. True means it is open.
        /// </summary>
        public virtual bool Boot
        {
            get
            {
                bool value, misc;
                GetParams(out misc, out value, out misc, out misc, out misc, out value, out misc);
                return value;
            }
        }

        /// <summary>
        ///     Gets the objective status of this Vehicle. True means the objective is on.
        /// </summary>
        public virtual bool Objective
        {
            get
            {
                bool value, misc;
                GetParams(out misc, out value, out misc, out misc, out misc, out misc, out value);
                return value;
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleSpawn" /> is being called.
        ///     This callback is called when a vehicle spawns.
        /// </summary>
        public event VehicleHandler Spawn;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleDeath" /> is being called.
        ///     This callback is called when a vehicle is destroyed - either by exploding or becoming submerged in water.
        /// </summary>
        public event PlayerVehicleHandler Died;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerEnterVehicle" /> is being called.
        ///     This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the
        ///     time this callback is called.
        /// </summary>
        public event PlayerEnterVehicleHandler PlayerEnter;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerExitVehicle" /> is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        public event PlayerVehicleHandler PlayerExit;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleMod" /> is being called.
        ///     This callback is called when a vehicle is modded.
        /// </summary>
        public event VehicleModHandler Mod;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehiclePaintjob" /> is being called.
        ///     Called when a player changes the paintjob of their vehicle (in a modshop).
        /// </summary>
        public event VehiclePaintjobHandler PaintjobApplied;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleRespray" /> is being called.
        ///     The callback name is deceptive, this callback is called when a player exits a mod shop, regardless of whether the
        ///     vehicle's colors were changed, and is NEVER called for pay 'n' spray garages.
        /// </summary>
        public event VehicleResprayedHandler Resprayed;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleDamageStatusUpdate" /> is being called.
        ///     This callback is called when a vehicle element such as doors, tires, panels, or lights get damaged.
        /// </summary>
        public event PlayerVehicleHandler DamageStatusUpdated;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnUnoccupiedVehicleUpdate" /> is being called.
        ///     This callback is called everytime an unoccupied vehicle updates the server with their status.
        /// </summary>
        public event UnoccupiedVehicleUpdatedHandler UnoccupiedUpdate;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleStreamIn" /> is being called.
        ///     Called when a vehicle is streamed to a player's client.
        /// </summary>
        public event PlayerVehicleHandler StreamIn;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleStreamOut" /> is being called.
        ///     This callback is called when a vehicle is streamed out from some player's client.
        /// </summary>
        public event PlayerVehicleHandler StreamOut;

        #endregion

        #region Vehicles natives

        /// <summary>
        ///     This function can be used to calculate the distance (as a float) between this Vehicle and another map coordinate.
        ///     This can be useful to detect how far a vehicle away is from a location.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>A float containing the distance from the point specified in the coordinates.</returns>
        public virtual float GetDistanceFromPoint(Vector point)
        {
            return Native.GetVehicleDistanceFromPoint(VehicleId, point.X, point.Y, point.Z);
        }

        /// <summary>
        ///     Creates a vehicle in the world.
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
        /// <returns> The vehicle created.</returns>
        public static Vehicle Create(int vehicletype, Vector position, float rotation, int color1, int color2,
            int respawnDelay)
        {
            int id = new[] {449, 537, 538, 569, 570, 590}.Contains(vehicletype)
                ? Native.AddStaticVehicleEx(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                    respawnDelay)
                : Native.CreateVehicle(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                    respawnDelay);

            return id == InvalidId ? null : Find(id);
        }

        /// <summary>
        ///     Creates a static vehicle in the world.
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
        /// <returns> The vehicle created.</returns>
        public static Vehicle CreateStatic(int vehicletype, Vector position, float rotation, int color1, int color2,
            int respawnDelay)
        {
            int id = Native.AddStaticVehicleEx(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                respawnDelay);

            return id == InvalidId ? null : Find(id);
        }

        /// <summary>
        ///     Creates a static vehicle in the world.
        /// </summary>
        /// <param name="vehicletype">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <returns> The vehicle created.</returns>
        public static Vehicle CreateStatic(int vehicletype, Vector position, float rotation, int color1, int color2)
        {
            int id = Native.AddStaticVehicle(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2);

            return id == InvalidId ? null : Find(id);
        }

        /// <summary>
        ///     Checks if this Vehicle is streamed in for a Player.
        /// </summary>
        /// <param name="forplayer">The Player to check.</param>
        /// <returns>False: Vehicle is not streamed in for the Player. False: Vehicle is streamed in for the Player.</returns>
        public virtual bool IsStreamedIn(Player forplayer)
        {
            return Native.IsVehicleStreamedIn(VehicleId, forplayer.PlayerId);
        }

        /// <summary>
        ///     Returns this Vehicle's rotation on all axis as a quaternion.
        /// </summary>
        /// <param name="w">A float variable in which to store the first quaternion angle, passed by reference.</param>
        /// <param name="x">A float variable in which to store the second quaternion angle, passed by reference.</param>
        /// <param name="y">A float variable in which to store the third quaternion angle, passed by reference.</param>
        /// <param name="z">A float variable in which to store the fourth quaternion angle, passed by reference.</param>
        public virtual void GetRotationQuat(out float w, out float x, out float y,
            out float z)
        {
            Native.GetVehicleRotationQuat(VehicleId, out w, out x, out y, out z);
        }

        /// <summary>
        ///     Set the parameters of this Vehicle for a Player.
        /// </summary>
        /// <param name="player">The Player to set this Vehicle's parameters for.</param>
        /// <param name="objective">False to disable the objective or True to show it.</param>
        /// <param name="doorslocked">False to unlock the doors or True to lock them.</param>
        public virtual void SetParamsForPlayer(Player player, bool objective,
            bool doorslocked)
        {
            Native.SetVehicleParamsForPlayer(VehicleId, player.PlayerId, objective, doorslocked);
        }

        /// <summary>
        ///     Use this function before any player connects (<see cref="BaseMode.OnGameModeInit" />) to tell all clients that the
        ///     script will control vehicle engines and lights. This prevents the game automatically turning the engine on/off when
        ///     players enter/exit vehicles and headlights automatically coming on when it is dark.
        /// </summary>
        public static void ManualEngineAndLights()
        {
            Native.ManualVehicleEngineAndLights();
        }

        /// <summary>
        ///     Sets this Vehicle's parameters for all players.
        /// </summary>
        /// <param name="engine">Toggle the engine status on or off.</param>
        /// <param name="lights">Toggle the lights on or off.</param>
        /// <param name="alarm">Toggle the vehicle alarm on or off.</param>
        /// <param name="doors">Toggle the lock status of the doors.</param>
        /// <param name="bonnet">Toggle the bonnet to be open or closed.</param>
        /// <param name="boot">Toggle the boot to be open or closed.</param>
        /// <param name="objective">Toggle the objective status for the vehicle on or off.</param>
        public virtual void SetParams(bool engine, bool lights, bool alarm, bool doors, bool bonnet, bool boot,
            bool objective)
        {
            Native.SetVehicleParamsEx(VehicleId, engine, lights, alarm, doors, bonnet, boot, objective);
        }


        /// <summary>
        ///     Gets this Vehicle's parameters.
        /// </summary>
        /// <param name="engine">Get the engine status. If True, the engine is running.</param>
        /// <param name="lights">Get the vehicle's lights' state. If True the lights are on.</param>
        /// <param name="alarm">Get the vehicle's alarm state. If True the alarm is (or was) sounding.</param>
        /// <param name="doors">Get the lock status of the doors. If True the doors are locked.</param>
        /// <param name="bonnet">Get the bonnet/hood status. If True, it's open.</param>
        /// <param name="boot">Get the boot/trunk status. True means it is open.</param>
        /// <param name="objective">Get the objective status. True means the objective is on.</param>
        public virtual void GetParams(out bool engine, out bool lights, out bool alarm,
            out bool doors, out bool bonnet, out bool boot, out bool objective)
        {
            Native.GetVehicleParamsEx(VehicleId, out engine, out lights, out alarm, out doors, out bonnet, out boot,
                out objective);
        }

        /// <summary>
        ///     Sets this Vehicle back to the position at where it was created.
        /// </summary>
        public virtual void Respawn()
        {
            Native.SetVehicleToRespawn(VehicleId);
        }

        /// <summary>
        ///     Links this Vehicle to the interior. This can be used for example for an arena/stadium.
        /// </summary>
        /// <param name="interiorid">Interior ID.</param>
        public virtual void LinkToInterior(int interiorid)
        {
            Native.LinkVehicleToInterior(VehicleId, interiorid);
        }

        /// <summary>
        ///     Adds a 'component' (often referred to as a 'mod' (modification)) to this Vehicle.
        /// </summary>
        /// <param name="componentid">The ID of the component to add to the vehicle.</param>
        public virtual void AddComponent(int componentid)
        {
            Native.AddVehicleComponent(VehicleId, componentid);
        }

        /// <summary>
        ///     Remove a component from the Vehicle.
        /// </summary>
        /// <param name="componentid">ID of the component to remove.</param>
        public virtual void RemoveComponent(int componentid)
        {
            Native.RemoveVehicleComponent(VehicleId, componentid);
        }

        /// <summary>
        ///     Change this Vehicle's primary and secondary colors.
        /// </summary>
        /// <param name="color1">The new vehicle's primary Color ID.</param>
        /// <param name="color2">The new vehicle's secondary Color ID.</param>
        public virtual void ChangeColor(int color1, int color2)
        {
            Native.ChangeVehicleColor(VehicleId, color1, color2);
        }

        /// <summary>
        ///     Change this Vehicle's paintjob (for plain colors see <see cref="ChangeColor" />).
        /// </summary>
        /// <param name="paintjobid">The ID of the Paintjob to apply. Use 3 to remove a paintjob.</param>
        public virtual void ChangePaintjob(int paintjobid)
        {
            Native.ChangeVehiclePaintjob(VehicleId, paintjobid);
        }

        /// <summary>
        ///     Set this Vehicle's numberplate, which supports olor embedding.
        /// </summary>
        /// <param name="numberplate">The text that should be displayed on the numberplate. Color Embedding> is supported.</param>
        public virtual void SetNumberPlate(string numberplate)
        {
            Native.SetVehicleNumberPlate(VehicleId, numberplate);
        }

        /// <summary>
        ///     Retreives the installed component ID from this Vehicle in a specific slot.
        /// </summary>
        /// <param name="slot">The component slot to check for components.</param>
        /// <returns>The ID of the component installed in the specified slot.</returns>
        public virtual int GetComponentInSlot(int slot)
        {
            return Native.GetVehicleComponentInSlot(VehicleId, slot);
        }

        /// <summary>
        ///     Find out what type of component a certain ID is.
        /// </summary>
        /// <param name="componentid">The component ID to check.</param>
        /// <returns>The component slot ID of the specified component.</returns>
        public static int GetComponentType(int componentid)
        {
            return Native.GetVehicleComponentType(componentid);
        }

        /// <summary>
        ///     Fully repairs this Vehicle, including visual damage (bumps, dents, scratches, popped tires etc.).
        /// </summary>
        public virtual void Repair()
        {
            Native.RepairVehicle(VehicleId);
        }

        /// <summary>
        ///     Sets the angular velocity of this Vehicle.
        /// </summary>
        /// <param name="velocity">The amount of velocity in the angular directions.</param>
        public virtual void SetVehicleAngularVelocity(Vector velocity)
        {
            Native.SetVehicleAngularVelocity(VehicleId, velocity);
        }

        /// <summary>
        ///     Retrieve the damage statuses of this Vehicle.
        /// </summary>
        /// <param name="panels">A variable to store the panel damage data in, passed by reference.</param>
        /// <param name="doors">A variable to store the door damage data in, passed by reference.</param>
        /// <param name="lights">A variable to store the light damage data in, passed by reference.</param>
        /// <param name="tires">A variable to store the tire damage data in, passed by reference.</param>
        public virtual void GetVehicleDamageStatus(out int panels, out int doors, out int lights, out int tires)
        {
            Native.GetVehicleDamageStatus(VehicleId, out panels, out doors, out lights, out tires);
        }

        /// <summary>
        ///     Sets the various visual damage statuses of this Vehicle, such as popped tires, broken lights and damaged panels.
        /// </summary>
        /// <param name="panels">A set of bits containing the panel damage status.</param>
        /// <param name="doors">A set of bits containing the door damage status.</param>
        /// <param name="lights">A set of bits containing the light damage status.</param>
        /// <param name="tires">A set of bits containing the tire damage status.</param>
        public virtual void UpdateVehicleDamageStatus(int panels, int doors, int lights, int tires)
        {
            Native.UpdateVehicleDamageStatus(VehicleId, panels, doors, lights, tires);
        }

        /// <summary>
        ///     Retrieve information about a specific vehicle model such as the size or position of seats.
        /// </summary>
        /// <param name="model">The vehicle model to get info of.</param>
        /// <param name="infotype">The type of information to retrieve.</param>
        /// <returns>The offset vector.</returns>
        public static Vector GetVehicleModelInfo(int model, VehicleModelInfo infotype)
        {
            return Native.GetVehicleModelInfo(model, infotype);
        }

        /// <summary>
        ///     Retrieve information about this Vehicle's model such as the size or position of seats.
        /// </summary>
        /// <param name="infotype">The type of information to retrieve.</param>
        /// <returns>The offset vector.</returns>
        public virtual Vector GetVehicleModelInfo(VehicleModelInfo infotype)
        {
            return Native.GetVehicleModelInfo(Model, infotype);
        }

        /// <summary>
        ///     Destroys this Vehicle.
        /// </summary>
        public virtual void Destroy()
        {
            Native.DestroyVehicle(VehicleId);
            Dispose();
        }

        #endregion

        #region Event raisers

        /// <summary>
        ///     Raises the <see cref="Spawn" /> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnSpawn(VehicleEventArgs e)
        {
            if (Spawn != null)
                Spawn(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Died" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnDeath(PlayerVehicleEventArgs e)
        {
            if (Died != null)
                Died(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnter" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEnterVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnPlayerEnter(PlayerEnterVehicleEventArgs e)
        {
            if (PlayerEnter != null)
                PlayerEnter(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerExit" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnPlayerExit(PlayerVehicleEventArgs e)
        {
            if (PlayerExit != null)
                PlayerExit(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Mod" /> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleModEventArgs" /> that contains the event data. </param>
        public virtual void OnMod(VehicleModEventArgs e)
        {
            if (Mod != null)
                Mod(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PaintjobApplied" /> event.
        /// </summary>
        /// <param name="e">An <see cref="VehiclePaintjobEventArgs" /> that contains the event data. </param>
        public virtual void OnPaintjobApplied(VehiclePaintjobEventArgs e)
        {
            if (PaintjobApplied != null)
                PaintjobApplied(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Resprayed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnResprayed(VehicleResprayedEventArgs e)
        {
            if (Resprayed != null)
                Resprayed(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="DamageStatusUpdated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnDamageStatusUpdated(PlayerVehicleEventArgs e)
        {
            if (DamageStatusUpdated != null)
                DamageStatusUpdated(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="UnoccupiedUpdate" /> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnUnoccupiedUpdate(UnoccupiedVehicleEventArgs e)
        {
            if (UnoccupiedUpdate != null)
                UnoccupiedUpdate(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StreamIn" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnStreamIn(PlayerVehicleEventArgs e)
        {
            if (StreamIn != null)
                StreamIn(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StreamOut" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnStreamOut(PlayerVehicleEventArgs e)
        {
            if (StreamOut != null)
                StreamOut(this, e);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Removes this Vehicle from memory. It is best to dispose the object when the vehicle is destroyed.
        /// </summary>
        public virtual void Dispose()
        {
            Instances.Remove(this);
        }

        public override int GetHashCode()
        {
            return VehicleId;
        }

        public override string ToString()
        {
            return string.Format("Vehicle(Id:{0}, Model: {1})", VehicleId, Model);
        }

        #endregion
    }
}