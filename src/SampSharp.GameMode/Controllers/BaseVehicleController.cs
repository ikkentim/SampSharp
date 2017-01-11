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
using SampSharp.GameMode.Factories;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all vehicle actions.
    /// </summary>
    [Controller]
    public class BaseVehicleController : Disposable, IEventListener, ITypeProvider, IGameServiceProvider
    {
        /// <summary>
        ///     Registers the events this VehicleController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            //Register all vehicle events
            gameMode.VehicleSpawned += (sender, args) => (sender as BaseVehicle)?.OnSpawn(args);
            gameMode.VehicleDied += (sender, args) => (sender as BaseVehicle)?.OnDeath(args);
            gameMode.PlayerEnterVehicle += (sender, args) => args.Vehicle?.OnPlayerEnter(args);
            gameMode.PlayerExitVehicle += (sender, args) => args.Vehicle?.OnPlayerExit(args);
            gameMode.VehicleMod += (sender, args) => (sender as BaseVehicle)?.OnMod(args);
            gameMode.VehiclePaintjobApplied += (sender, args) => (sender as BaseVehicle)?.OnPaintjobApplied(args);
            gameMode.VehicleResprayed += (sender, args) => (sender as BaseVehicle)?.OnResprayed(args);
            gameMode.VehicleDamageStatusUpdated +=
                (sender, args) => (sender as BaseVehicle)?.OnDamageStatusUpdated(args);
            gameMode.UnoccupiedVehicleUpdated += (sender, args) => (sender as BaseVehicle)?.OnUnoccupiedUpdate(args);
            gameMode.VehicleStreamIn += (sender, args) => (sender as BaseVehicle)?.OnStreamIn(args);
            gameMode.VehicleStreamOut += (sender, args) => (sender as BaseVehicle)?.OnStreamOut(args);
            gameMode.TrailerUpdate += (sender, args) => (sender as BaseVehicle)?.OnTrailerUpdate(args);
            gameMode.VehicleSirenStateChange += (sender, args) => (sender as BaseVehicle)?.OnSirenStateChanged(args);
        }

        /// <summary>
        ///     Registers the services this controller provides.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        /// <param name="serviceContainer">The service container.</param>
        public virtual void RegisterServices(BaseMode gameMode, GameModeServiceContainer serviceContainer)
        {
            serviceContainer.AddService<IVehicleFactory>(new BaseVehicleFactory(gameMode));
        }

        /// <summary>
        ///     Registers types this VehicleController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            BaseVehicle.Register<BaseVehicle>();
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var vehicle in BaseVehicle.All)
                {
                    vehicle.Dispose();
                }
            }
        }
    }
}