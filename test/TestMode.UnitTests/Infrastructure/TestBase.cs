using SampSharp.Entities.SAMP;

namespace TestMode.UnitTests;

public class TestBase : IDisposable
{
    public Player Player => XunitSystem.Player;
    public IServiceProvider Services => XunitSystem.ServiceProvider;

    public void Dispose()
    {
        Cleanup();
        GC.SuppressFinalize(this);
    }

    protected virtual void Cleanup()
    {
    }
}