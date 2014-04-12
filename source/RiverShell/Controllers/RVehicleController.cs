using GameMode.Controllers;
using RiverShell.World;

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
