using Microsoft.Extensions.Logging;
using SampSharp.Entities;

namespace TestMode.OpenMp.Entities;

public class TestTicker : ITickingSystem
{
    public void Tick()
    {
        
    }

    [Event]
    public void OnInitialized(ILogger<TestTicker> logger)
    {
        logger.LogInformation("On initialized");
    }
}