using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TestMode.Entities.Services;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemObjectTest : ISystem
    {
        [Event]
        public void OnGameModeInit(IWorldService worldService)
        {
            var obj = worldService.CreateObject(16638, new Vector3(10, 10, 40), Vector3.Zero, 1000);
            obj.DisableCameraCollisions();
        }

        [Event]
        public void OnPlayerConnect(Player player, IVehicleRepository vehiclesRepository, IWorldService worldService)
        {
            var obj = worldService.CreatePlayerObject(player, 16638, new Vector3(50, 10, 40), Vector3.Zero, 1000);
            obj.DisableCameraCollisions();
        }
    }
}