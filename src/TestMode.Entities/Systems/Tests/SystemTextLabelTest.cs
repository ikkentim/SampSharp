using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemTextLabelTest : ISystem
    {
        [Event]
        public void OnGameModeInit(IWorldService worldService)
        {
            var green = Color.Green;
            green.A = 128;
            worldService.CreateTextLabel("text", green, new Vector3(10, 10, 10), 1000);
        }
    }
}