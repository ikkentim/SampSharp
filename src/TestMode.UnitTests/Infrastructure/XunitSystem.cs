using System.Numerics;
using System.Reflection;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit.Runner.Common;
using Xunit.Runner.InProc.SystemConsole;
using Xunit.Sdk;
using Xunit.v3;

namespace TestMode.UnitTests;

public class XunitSystem : ISystem
{
    public static Player Player { get; private set; } = null!;
    public static IServiceProvider ServiceProvider { get; private set; } = null!;
    private bool _started;

    public XunitSystem(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    [Event]
    public void OnGameModeInit(IServerService serverService)
    {
        serverService.AddPlayerClass(1, new Vector3(0, 0, 10), 0);

        serverService.ConnectNpc("tester", "npcidle");
    }

    [Event]
    public void OnPlayerConnect(Player player, ITimerService timerService)
    {
        if (!_started && player.IsNpc)
        {
            _started = true;
            Player = player;

            timerService.Delay(sp =>
            {
                _ = RunXUnit();
            }, TimeSpan.FromSeconds(0.5f));
        }
    }

    public async Task RunXUnit()
    {
        var runner = new ConsoleRunner(["-parallel", "none"], Assembly.GetExecutingAssembly());
        var exitCode = await runner.EntryPoint();

        Environment.Exit(exitCode);
    }
}