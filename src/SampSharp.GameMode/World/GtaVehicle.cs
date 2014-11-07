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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.World
{
    public class GtaVehicle : IdentifiedPool<GtaVehicle>, IIDentifiable, IWorldObject
    {
        #region Fields

        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no vehicle matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.InvalidVehicleId;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initalizes a new instance of the Vehicle class.
        /// </summary>
        /// <param name="id">The ID of the vehicle to initialize.</param>
        public GtaVehicle(int id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets an instance of VehicleModelInfo about this Vehicle.
        /// </summary>
        public VehicleModelInfo Info
        {
            get { return VehicleModelInfo.ForVehicle(this); }
        }

        /// <summary>
        ///     Gets the driver of this Vehicle.
        /// </summary>
        public GtaPlayer Driver
        {
            get { return GtaPlayer.All.FirstOrDefault(p => p.Vehicle == this && p.VehicleSeat == 0); }
        }

        /// <summary>
        ///     Gets the passengers of this Vehicle. (not the driver)
        /// </summary>
        public IEnumerable<GtaPlayer> Passengers
        {
            get { return GtaPlayer.All.Where(p => p.Vehicle == this).Where(player => player.VehicleSeat > 0); }
        }

        /// <summary>
        ///     Gets the ID of this Vehicle.
        /// </summary>
        public int Id { get; private set; }

        #endregion

        #region Vehicles native properties

        /// <summary>
        ///     Gets whether this Vehicle has been created and still is alive.
        /// </summary>
        public virtual bool IsValid
        {
            get { return Native.IsValidVehicle(Id); }
        }

        /// <summary>
        ///     Gets or sets the Z angle of this Vehicle.
        /// </summary>
        public virtual float Angle
        {
            get { return Native.GetVehicleZAngle(Id); }
            set { Native.SetVehicleZAngle(Id, value); }
        }

        /// <summary>
        ///     Gets the model ID of this Vehicle.
        /// </summary>
        public virtual int Model
        {
            get { return Native.GetVehicleModel(Id); }
        }

        /// <summary>
        ///     Gets whether this Vehicle has a trailer attached to it.
        /// </summary>
        public virtual bool HasTrailer
        {
            get { return Native.IsTrailerAttachedToVehicle(Id); }
        }

        /// <summary>
        ///     Gets or sets the the trailer attached to this Vehicle.
        /// </summary>
        /// <returns>The trailer attached.</returns>
        public virtual GtaVehicle Trailer
        {
            get
            {
                int id = Native.GetVehicleTrailer(Id);
                return id == 0 ? null : Find(id);
            }
            set
            {
                if (value == null)
                    Native.DetachTrailerFromVehicle(Id);
                else
                    Native.AttachTrailerToVehicle(value.Id, Id);
            }
        }

        /// <summary>
        ///     Gets or sets the velocity at which this Vehicle is moving.
        /// </summary>
        public virtual Vector Velocity
        {
            get { return Native.GetVehicleVelocity(Id); }
            set { Native.SetVehicleVelocity(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the virtual world of this Vehicle.
        /// </summary>
        public virtual int VirtualWorld
        {
            get { return Native.GetVehicleVirtualWorld(Id); }
            set { Native.SetVehicleVirtualWorld(Id, value); }
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

        /// <summary>
        ///     Gets or sets the rotation of this Vehicle.
        /// </summary>
        /// <remarks>
        ///     Only the Z angle can be set!
        /// </remarks>
        public virtual Vector Rotation
        {
            get { return new Vector(0, 0, Angle); }
            set { Native.SetVehicleZAngle(Id, value.Z); }
        }

        /// <summary>
        ///     Gets or sets the position of this Vehicle.
        /// </summary>
        public virtual Vector Position
        {
            get { return Native.GetVehiclePos(Id); }
            set { Native.SetVehiclePos(Id, value); }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleSpawn" /> is being called.
        ///     This callback is called when a vehicle spawns.
        /// </summary>
        public event EventHandler<VehicleEventArgs> Spawn;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleDeath" /> is being called.
        ///     This callback is called when a vehicle is destroyed - either by exploding or becoming submerged in water.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> Died;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerEnterVehicle" /> is being called.
        ///     This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the
        ///     time this callback is called.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> PlayerEnter;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerExitVehicle" /> is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> PlayerExit;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleMod" /> is being called.
        ///     This callback is called when a vehicle is modded.
        /// </summary>
        public event EventHandler<VehicleModEventArgs> Mod;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehiclePaintjob" /> is being called.
        ///     Called when a player changes the paintjob of their vehicle (in a modshop).
        /// </summary>
        public event EventHandler<VehiclePaintjobEventArgs> PaintjobApplied;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleRespray" /> is being called.
        ///     The callback name is deceptive, this callback is called when a player exits a mod shop, regardless of whether the
        ///     vehicle's colors were changed, and is NEVER called for pay 'n' spray garages.
        /// </summary>
        public event EventHandler<VehicleResprayedEventArgs> Resprayed;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleDamageStatusUpdate" /> is being called.
        ///     This callback is called when a vehicle element such as doors, tires, panels, or lights get damaged.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> DamageStatusUpdated;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnUnoccupiedVehicleUpdate" /> is being called.
        ///     This callback is called everytime an unoccupied vehicle updates the server with their status.
        /// </summary>
        public event EventHandler<UnoccupiedVehicleEventArgs> UnoccupiedUpdate;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleStreamIn" /> is being called.
        ///     Called when a vehicle is streamed to a player's client.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> StreamIn;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnVehicleStreamOut" /> is being called.
        ///     This callback is called when a vehicle is streamed out from some player's client.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> StreamOut;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnTrailerUpdate" /> is being called.
        ///     This callback is called when a player sent a trailer update.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> TrailerUpdate;

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
            CheckDisposure();

            return Native.GetVehicleDistanceFromPoint(Id, point.X, point.Y, point.Z);
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
        public static GtaVehicle Create(int vehicletype, Vector position, float rotation, int color1, int color2,
            int respawnDelay = -1)
        {
            int id = new[] {449, 537, 538, 569, 570, 590}.Contains(vehicletype)
                ? Native.AddStaticVehicleEx(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                    respawnDelay)
                : Native.CreateVehicle(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                    respawnDelay);

            return id == InvalidId ? null : FindOrCreate(id);
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
        public static GtaVehicle Create(VehicleModelType vehicletype, Vector position, float rotation, int color1,
            int color2,
            int respawnDelay = -1)
        {
            return Create((int) vehicletype, position, rotation, color1, color2, respawnDelay);
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
        public static GtaVehicle CreateStatic(int vehicletype, Vector position, float rotation, int color1, int color2,
            int respawnDelay)
        {
            int id = Native.AddStaticVehicleEx(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                respawnDelay);

            return id == InvalidId ? null : FindOrCreate(id);
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
        public static GtaVehicle CreateStatic(int vehicletype, Vector position, float rotation, int color1, int color2)
        {
            int id = Native.AddStaticVehicle(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2);

            return id == InvalidId ? null : FindOrCreate(id);
        }

        /// <summary>
        ///     Checks if this Vehicle is streamed in for a Player.
        /// </summary>
        /// <param name="forplayer">The Player to check.</param>
        /// <returns>False: Vehicle is not streamed in for the Player. False: Vehicle is streamed in for the Player.</returns>
        public virtual bool IsStreamedIn(GtaPlayer forplayer)
        {
            CheckDisposure();

            return Native.IsVehicleStreamedIn(Id, forplayer.Id);
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
            CheckDisposure();

            Native.GetVehicleRotationQuat(Id, out w, out x, out y, out z);
        }

        /// <summary>
        ///     Set the parameters of this Vehicle for a Player.
        /// </summary>
        /// <param name="player">The Player to set this Vehicle's parameters for.</param>
        /// <param name="objective">False to disable the objective or True to show it.</param>
        /// <param name="doorslocked">False to unlock the doors or True to lock them.</param>
        public virtual void SetParamsForPlayer(GtaPlayer player, bool objective,
            bool doorslocked)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            Native.SetVehicleParamsForPlayer(Id, player.Id, objective, doorslocked);
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
            CheckDisposure();

            Native.SetVehicleParamsEx(Id, engine, lights, alarm, doors, bonnet, boot, objective);
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
            CheckDisposure();

            Native.GetVehicleParamsEx(Id, out engine, out lights, out alarm, out doors, out bonnet, out boot,
                out objective);
        }

        /// <summary>
        ///     Sets this Vehicle back to the position at where it was created.
        /// </summary>
        public virtual void Respawn()
        {
            CheckDisposure();

            Native.SetVehicleToRespawn(Id);
        }

        /// <summary>
        ///     Links this Vehicle to the interior. This can be used for example for an arena/stadium.
        /// </summary>
        /// <param name="interiorid">Interior ID.</param>
        public virtual void LinkToInterior(int interiorid)
        {
            CheckDisposure();

            Native.LinkVehicleToInterior(Id, interiorid);
        }

        /// <summary>
        ///     Adds a 'component' (often referred to as a 'mod' (modification)) to this Vehicle.
        /// </summary>
        /// <param name="componentid">The ID of the component to add to the vehicle.</param>
        public virtual void AddComponent(int componentid)
        {
            CheckDisposure();

            Native.AddVehicleComponent(Id, componentid);
        }

        /// <summary>
        ///     Remove a component from the Vehicle.
        /// </summary>
        /// <param name="componentid">ID of the component to remove.</param>
        public virtual void RemoveComponent(int componentid)
        {
            CheckDisposure();

            Native.RemoveVehicleComponent(Id, componentid);
        }

        /// <summary>
        ///     Change this Vehicle's primary and secondary colors.
        /// </summary>
        /// <param name="color1">The new vehicle's primary Color ID.</param>
        /// <param name="color2">The new vehicle's secondary Color ID.</param>
        public virtual void ChangeColor(int color1, int color2)
        {
            CheckDisposure();

            Native.ChangeVehicleColor(Id, color1, color2);
        }

        /// <summary>
        ///     Change this Vehicle's paintjob (for plain colors see <see cref="ChangeColor" />).
        /// </summary>
        /// <param name="paintjobid">The ID of the Paintjob to apply. Use 3 to remove a paintjob.</param>
        public virtual void ChangePaintjob(int paintjobid)
        {
            CheckDisposure();

            Native.ChangeVehiclePaintjob(Id, paintjobid);
        }

        /// <summary>
        ///     Set this Vehicle's numberplate, which supports olor embedding.
        /// </summary>
        /// <param name="numberplate">The text that should be displayed on the numberplate. Color Embedding> is supported.</param>
        public virtual void SetNumberPlate(string numberplate)
        {
            CheckDisposure();

            Native.SetVehicleNumberPlate(Id, numberplate);
        }

        /// <summary>
        ///     Retreives the installed component ID from this Vehicle in a specific slot.
        /// </summary>
        /// <param name="slot">The component slot to check for components.</param>
        /// <returns>The ID of the component installed in the specified slot.</returns>
        public virtual int GetComponentInSlot(int slot)
        {
            CheckDisposure();

            return Native.GetVehicleComponentInSlot(Id, slot);
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
            CheckDisposure();

            Native.RepairVehicle(Id);
        }

        /// <summary>
        ///     Sets the angular velocity of this Vehicle.
        /// </summary>
        /// <param name="velocity">The amount of velocity in the angular directions.</param>
        public virtual void SetVehicleAngularVelocity(Vector velocity)
        {
            CheckDisposure();

            Native.SetVehicleAngularVelocity(Id, velocity);
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
            CheckDisposure();

            Native.GetVehicleDamageStatus(Id, out panels, out doors, out lights, out tires);
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
            CheckDisposure();

            Native.UpdateVehicleDamageStatus(Id, panels, doors, lights, tires);
        }

        /// <summary>
        ///     Retrieve information about a specific vehicle model such as the size or position of seats.
        /// </summary>
        /// <param name="model">The vehicle model to get info of.</param>
        /// <param name="infotype">The type of information to retrieve.</param>
        /// <returns>The offset vector.</returns>
        public static Vector GetVehicleModelInfo(int model, VehicleModelInfoType infotype)
        {
            return Native.GetVehicleModelInfo(model, infotype);
        }

        /// <summary>
        ///     Retrieve information about this Vehicle's model such as the size or position of seats.
        /// </summary>
        /// <param name="infotype">The type of information to retrieve.</param>
        /// <returns>The offset vector.</returns>
        public virtual Vector GetVehicleModelInfo(VehicleModelInfoType infotype)
        {
            CheckDisposure();

            return Native.GetVehicleModelInfo(Model, infotype);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.DestroyVehicle(Id);
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

        /// <summary>
        ///     Raises the <see cref="TrailerUpdate" /> event.
        /// </summary>
        /// <param name="args">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnTrailerUpdate(PlayerVehicleEventArgs args)
        {
            if (TrailerUpdate != null)
            {
                TrailerUpdate(this, args);
            }
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return string.Format("Vehicle(Id:{0}, Model: {1})", Id, Model);
        }

        #endregion
    }
}