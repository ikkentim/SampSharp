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

using SampSharp.GameMode.Factories;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all vehicle actions.
    /// </summary>
    public class GtaVehicleController : Disposable, IEventListener, ITypeProvider, IGameServiceProvider
    {
        /// <summary>
        ///     Registers the events this VehicleController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            //Register all vehicle events
            gameMode.VehicleSpawned += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnSpawn(args);
            };
            gameMode.VehicleDied += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnDeath(args);
            };
            gameMode.PlayerEnterVehicle += (sender, args) =>
            {
                if (args.Vehicle != null)
                    args.Vehicle.OnPlayerEnter(args);
            };
            gameMode.PlayerExitVehicle += (sender, args) =>
            {
                if (args.Vehicle != null)
                    args.Vehicle.OnPlayerExit(args);
            };
            gameMode.VehicleMod += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnMod(args);
            };
            gameMode.VehiclePaintjobApplied += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnPaintjobApplied(args);
            };
            gameMode.VehicleResprayed += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnResprayed(args);
            };
            gameMode.VehicleDamageStatusUpdated += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnDamageStatusUpdated(args);
            };
            gameMode.UnoccupiedVehicleUpdated += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnUnoccupiedUpdate(args);
            };
            gameMode.VehicleStreamIn += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnStreamIn(args);
            };
            gameMode.VehicleStreamOut += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnStreamOut(args);
            };
            gameMode.TrailerUpdate += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnTrailerUpdate(args);
            };
            gameMode.VehicleSirenStateChange += (sender, args) =>
            {
                var vehicle = sender as GtaVehicle;
                if (vehicle != null)
                    vehicle.OnSirenStateChanged(args);
            };
        }

        /// <summary>
        ///     Registers the services this controller provides.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        /// <param name="serviceContainer">The service container.</param>
        public virtual void RegisterServices(BaseMode gameMode, GameModeServiceContainer serviceContainer)
        {
            serviceContainer.AddService<IVehicleFactory>(new GtaVehicleFactory(gameMode));
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
                foreach (var vehicle in GtaVehicle.All)
                {
                    vehicle.Dispose();
                }
            }
        }
    }
}