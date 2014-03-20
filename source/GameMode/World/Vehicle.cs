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

        /// <summary>
        /// Gets the model ID of this Vehicle.
        /// </summary>
        public int Model
        {
            get { return Native.GetVehicleModel(VehicleId); }
        }

        #endregion

        #region Events



        #endregion

        /// <summary>
        /// Registers all events the Vehicle class listens to.
        /// </summary>
        /// <param name="gameMode">An instance of BaseMode to which to listen.</param>
        /// <param name="cast">A function to get a <see cref="Vehicle"/> object from a vehicleid.</param>
        public static void RegisterEvents(BaseMode gameMode, Func<int, Vehicle> cast)
        {
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

        /// <summary>
        /// Destroys this Vehicle.
        /// </summary>
        public void Destroy()
        {
            Native.DestroyVehicle(VehicleId);
            Dispose();
        }

        //public void OnDeath(PlayerVehicleEventArgs e)
        //{
            
        //}

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
    }
}
