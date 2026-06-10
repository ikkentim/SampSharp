using SampSharp.Entities;

namespace TestMode.OpenMp.Entities.Systems;

public class TestGameModeSystem : ISystem
{
    [Event]
    public void OnGameModeInit()
    {
        Console.WriteLine("OnGameModeInit");
    }

    [Event]
    public void OnGameModeExit()
    {
        Console.WriteLine("GameModeExit");
    }
}