using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemVehicleTest : ISystem
    {
        [Event]
        public void OnGameModeInit(IWorldService worldService)
        {
            worldService.CreateVehicle(VehicleModelType.Alpha, new Vector3(40, 40, 10), 0, 0, 0);
        }
    }
}