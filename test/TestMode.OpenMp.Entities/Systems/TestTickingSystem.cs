using Microsoft.Extensions.Logging;
using SampSharp.Entities;

namespace TestMode.OpenMp.Entities.Systems;

public class TestTickingSystem : ITickingSystem
{
    public void Tick()
    {
        
    }

    [Event]
    public void OnInitialized(ILogger<TestTickingSystem> logger)
    {
        logger.LogInformation("On initialized");
    }
}