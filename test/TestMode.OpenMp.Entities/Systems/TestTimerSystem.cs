using SampSharp.Entities;

namespace TestMode.OpenMp.Entities.Systems;

public class TestTimerSystem : ISystem
{
    private int _ticks;

    [Timer(1000)]
    public void OnTimer()
    {
        if (_ticks++ == 3)
        {
            //throw new Exception("Test exception");
        }
    }
}