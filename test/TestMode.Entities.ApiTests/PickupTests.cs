using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class PickupTests : TestBase
{
    private readonly Pickup _pickup;

    public PickupTests()
    {
        _pickup = Services.GetRequiredService<IWorldService>().CreatePickup(1234, PickupType.ScriptedActionsOnlyEveryFewSeconds, new Vector3(10, 20, 30), 10);
    }

    protected override void Cleanup()
    {
        _pickup.DestroyEntity();
    }

    [Fact]
    public void CreatePickup_should_set_properties()
    {
        _pickup.Model.ShouldBe(1234);
        _pickup.SpawnType.ShouldBe(PickupType.ScriptedActionsOnlyEveryFewSeconds);
        _pickup.Position.ShouldBe(new Vector3(10, 20, 30));
        _pickup.VirtualWorld.ShouldBe(10);
    }

    [Fact]
    public void SetType_should_change_spawn_type()
    {
        _pickup.SetType(PickupType.ShowButNotPickupable);
        _pickup.SpawnType.ShouldBe(PickupType.ShowButNotPickupable);
    }

    [Fact]
    public void SetModel_should_change_model()
    {
        _pickup.SetModel(1238);
        _pickup.Model.ShouldBe(1238);
    }

    [Fact]
    public void SetPositionNoUpdate_should_succeed()
    {
        Should.NotThrow(() => _pickup.SetPositionNoUpdate(new Vector3(50, 60, 70)));
    }
}