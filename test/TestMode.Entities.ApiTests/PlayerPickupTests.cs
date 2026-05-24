using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class PlayerPickupTests : TestBase
{
    private readonly PlayerPickup _pickup;

    public PlayerPickupTests()
    {
        _pickup = Services.GetRequiredService<IWorldService>()
            .CreatePlayerPickup(Player, 1234, PickupType.ScriptedActionsOnlyEveryFewSeconds, new Vector3(10, 20, 30));
    }

    protected override void Cleanup()
    {
        _pickup?.DestroyEntity();
    }

    [Fact]
    public void CreatePlayerPickup_should_set_properties()
    {
        _pickup.ShouldNotBeNull();
        _pickup.Model.ShouldBe(1234);
        _pickup.SpawnType.ShouldBe(PickupType.ScriptedActionsOnlyEveryFewSeconds);
        _pickup.Position.ShouldBe(new Vector3(10, 20, 30));
    }

    [Fact]
    public void IsStreamedIn_should_succeed()
    {
        _ = _pickup.IsStreamedIn();
    }

    [Fact]
    public void StreamIn_should_succeed()
    {
        _pickup.StreamIn();
    }

    [Fact]
    public void StreamOut_should_succeed()
    {
        _pickup.StreamIn();
        _pickup.StreamOut();
    }
}
