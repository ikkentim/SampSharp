﻿// SampSharp
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

using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all vehicle actions.
    /// </summary>
    public class GtaVehicleController : Disposable, IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this VehicleController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            //Register all vehicle events
            gameMode.VehicleSpawned += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnSpawn(args);
            gameMode.VehicleDied += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnDeath(args);
            };
            gameMode.PlayerEnterVehicle += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnPlayerEnter(args);
            };
            gameMode.PlayerExitVehicle += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnPlayerExit(args);
            };
            gameMode.VehicleMod += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnMod(args);
            };
            gameMode.VehiclePaintjobApplied += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnPaintjobApplied(args);
            };
            gameMode.VehicleResprayed += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnResprayed(args);
            };
            gameMode.VehicleDamageStatusUpdated += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnDamageStatusUpdated(args);
            };
            gameMode.UnoccupiedVehicleUpdated += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnUnoccupiedUpdate(args);
            };
            gameMode.VehicleStreamIn += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnStreamIn(args);
            };
            gameMode.VehicleStreamOut += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnStreamOut(args);
            };
            gameMode.TrailerUpdate += (sender, args) =>
            {
                GtaVehicle vehicle = GtaVehicle.Find(args.VehicleId);

                if (vehicle != null)
                    vehicle.OnTrailerUpdate(args);
            };
        }

        /// <summary>
        ///     Registers types this VehicleController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            GtaVehicle.Register<GtaVehicle>();
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (GtaVehicle vehicle in GtaVehicle.All)
                {
                    vehicle.Dispose();
                }
            }
        }
    }
}