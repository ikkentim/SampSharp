using System;
using System.Collections.Generic;
using System.Linq;
using GameMode.Definitions;
using GameMode.Events;

namespace GameMode.World
{
    public class Vehicle : IDisposable
    {
        #region Fields

        /// <summary>
        /// Contains all instances of Vehicles.
        /// </summary>
        protected static List<Vehicle> Instances = new List<Vehicle>();

        /// <summary>
        /// Gets an ID commonly returned by methods to point out that no vehicle matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.InvalidVehicleId;

        #endregion

        #region Factories

        /// <summary>
        /// Returns an instance of <see cref="Vehicle"/> that deals with <paramref name="vehicleId"/>.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle we are dealing with.</param>
        /// <returns>An instance of <see cref="Vehicle"/>.</returns>
        public static Vehicle Find(int vehicleId)
        {
            //Find player in memory or initialize new player
            return Instances.FirstOrDefault(p => p.VehicleId == vehicleId) ?? new Vehicle(vehicleId);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initalizes a new instance of the Vehicle class.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to initialize.</param>
        protected Vehicle(int vehicleId)
        {
            VehicleId = vehicleId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ID of this Vehicle.
        /// </summary>
        public int VehicleId { get; private set; }

        #endregion

        #region Vehicles natives

        /// <summary>
        /// Gets whether this Vehicle has been created and still is alive.
        /// </summary>
        public virtual bool IsValid
        {
            get { return Native.IsValidVehicle(VehicleId); }
        }
        /// <summary>
        /// Gets the model ID of this Vehicle.
        /// </summary>
        public virtual int Model
        {
            get { return Native.GetVehicleModel(VehicleId); }
        }

        #endregion

        #region Events



        #endregion

        #region Vehicles natives

        /// <summary>
        /// This function can be used to calculate the distance (as a float) between this Vehicle and another map coordinate. This can be useful to detect how far a vehicle away is from a location.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>A float containing the distance from the point specified in the coordinates.</returns>
        public virtual float GetDistanceFromPoint(Vector point)
        {
            return Native.GetVehicleDistanceFromPoint(VehicleId, point.X, point.Y, point.Z);
        }

        /// <summary>
        /// Creates a vehicle in the world.
        /// </summary>
        /// <param name="vehicletype">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <param name="respawnDelay">The delay until the car is respawned without a driver in seconds. Using -1 will prevent the vehicle from respawning.</param>
        /// <returns> The vehicle created.</returns>

        public static Vehicle Create(int vehicletype, Vector position, float rotation, int color1, int color2, int respawnDelay)
        {
            var id = new[] {449, 537, 538, 569, 570, 590}.Contains(vehicletype)
                ? Native.AddStaticVehicleEx(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                    respawnDelay)
                : Native.CreateVehicle(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                    respawnDelay);

            return id == InvalidId ? null : Find(id);
        }

        /// <summary>
        /// Creates a static vehicle in the world.
        /// </summary>
        /// <param name="vehicletype">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <param name="respawnDelay">The delay until the car is respawned without a driver in seconds. Using -1 will prevent the vehicle from respawning.</param>
        /// <returns> The vehicle created.</returns>

        public static Vehicle CreateStatic(int vehicletype, Vector position, float rotation, int color1, int color2, int respawnDelay)
        {
            var id = Native.AddStaticVehicleEx(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2,
                    respawnDelay);

            return id == InvalidId ? null : Find(id);
        }

        /// <summary>
        /// Creates a static vehicle in the world.
        /// </summary>
        /// <param name="vehicletype">The model for the vehicle.</param>
        /// <param name="position">The coordinates for the vehicle.</param>
        /// <param name="rotation">The facing angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <returns> The vehicle created.</returns>

        public static Vehicle CreateStatic(int vehicletype, Vector position, float rotation, int color1, int color2)
        {
            var id = Native.AddStaticVehicle(vehicletype, position.X, position.Y, position.Z, rotation, color1, color2);

            return id == InvalidId ? null : Find(id);
        }

        /// <summary>
        /// Destroys this Vehicle.
        /// </summary>
        public virtual void Destroy()
        {
            Native.DestroyVehicle(VehicleId);
            Dispose();
        }

        #endregion

        #region Event raisers

        public void OnDeath(PlayerVehicleEventArgs e)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers all events the Vehicle class listens to.
        /// </summary>
        /// <param name="gameMode">An instance of BaseMode to which to listen.</param>
        /// <param name="cast">A function to get a <see cref="Vehicle"/> object from a vehicleid.</param>
        protected static void RegisterEvents(BaseMode gameMode, Func<int, Vehicle> cast)
        {
            gameMode.VehicleSpawned += (sender, args) => { };
            gameMode.VehicleDied += (sender, args) => { };
            gameMode.PlayerEnterVehicle += (sender, args) => { };
            gameMode.PlayerExitVehicle += (sender, args) => { };
            gameMode.VehicleMod += (sender, args) => { };
            gameMode.VehiclePaintjobApplied += (sender, args) => { };
            gameMode.VehicleResprayed += (sender, args) => { };
            gameMode.VehicleDamageStatusUpdated += (sender, args) => { };
            gameMode.UnoccupiedVehicleUpdated += (sender, args) => { };
            gameMode.VehicleStreamIn += (sender, args) => { };
            gameMode.VehicleStreamOut += (sender, args) => { };

            //baseMode.VehicleDied += (sender, args) => cast(args.VehicleId).OnDeath(args);
        }

        /// <summary>
        /// Registers all events the Vehicle class listens to.
        /// </summary>
        /// <param name="gameMode">An instance of BaseMode to which to listen.</param>
        public static void RegisterEvents(BaseMode gameMode)
        {
            RegisterEvents(gameMode, Find);
        }

        public override int GetHashCode()
        {
            return VehicleId;
        }

        public override string ToString()
        {
            return string.Format("Vehicle(Id:{0}, Model: )", VehicleId);
        }

        /// <summary>
        /// Removes this Vehicle from memory. It is best to dispose the object when the vehicle is destroyed.
        /// </summary>
        public virtual void Dispose()
        {
            Instances.Remove(this);
        }

        #endregion
    }
}
