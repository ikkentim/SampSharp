using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemZoneTest : ISystem
    {
        private GangZone _zone;

        [Event]
        public void OnGameModeInit(IWorldService worldService)
        {
            var blue = Color.Blue;
            blue.A = 128;
            _zone = worldService.CreateGangZone(0, 0, 100, 100);
            _zone.Color = blue;
            _zone.Show();
        }

        [Event]
        public void OnPlayerConnect(Player player)
        {
            _zone.Show(player.Entity);
        }
    }
}