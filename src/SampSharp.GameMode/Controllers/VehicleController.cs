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

using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all vehicle actions.
    /// </summary>
    public class VehicleController : Disposable, IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this VehicleController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            //Register all vehicle events
            gameMode.VehicleSpawned += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnSpawn(args);
            gameMode.VehicleDied += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnDeath(args);
            gameMode.PlayerEnterVehicle += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnPlayerEnter(args);
            gameMode.PlayerExitVehicle += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnPlayerExit(args);
            gameMode.VehicleMod += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnMod(args);
            gameMode.VehiclePaintjobApplied +=
                (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnPaintjobApplied(args);
            gameMode.VehicleResprayed += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnResprayed(args);
            gameMode.VehicleDamageStatusUpdated +=
                (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnDamageStatusUpdated(args);
            gameMode.UnoccupiedVehicleUpdated +=
                (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnUnoccupiedUpdate(args);
            gameMode.VehicleStreamIn += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnStreamIn(args);
            gameMode.VehicleStreamOut += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnStreamOut(args);
            gameMode.TrailerUpdate += (sender, args) => GtaVehicle.FindOrCreate(args.VehicleId).OnTrailerUpdate(args);
        }

        /// <summary>
        ///     Registers types this VehicleController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            GtaVehicle.Register<GtaVehicle>();
        }

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