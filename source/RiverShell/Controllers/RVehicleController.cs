using RiverShell.World;
using SampSharp.GameMode.Controllers;

namespace RiverShell.Controllers
{
    public class RVehicleController : VehicleController
    {
        public override void RegisterTypes()
        {
            RVehicle.Register<RVehicle>();
        }
    }
}
