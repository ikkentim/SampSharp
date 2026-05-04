using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

public class ActorTests : TestBase
{
    private readonly Actor _actor;

    public ActorTests()
    {
        _actor = Services.GetRequiredService<IWorldService>().CreateActor(46, new Vector3(4, 5, 6), 45);
    }

    protected override void Cleanup()
    {
        _actor.DestroyEntity();
    }

    [Fact]
    public void CreateActor_should_set_properties()
    {
        _actor.Skin.ShouldBe(46);
        _actor.Position.ShouldBe(new Vector3(4, 5, 6));
        _actor.Angle.ShouldBe(45);
    }

    [Fact]
    public void Angle_should_roundtrip()
    {
        _actor.Angle = 88;
        _actor.Angle.ShouldBe(88);
    }

    [Fact]
    public void Position_should_roundtrip()
    {
        _actor.Position = new Vector3(1, 2, 3);
        _actor.Position.ShouldBe(new Vector3(1, 2, 3));
    }

    [Fact]
    public void Health_should_roundtrip()
    {
        _actor.Health = 94;
        _actor.Health.ShouldBe(94);
    }

    [Fact]
    public void IsInvulnerable_should_roundtrip_true()
    {
        _actor.IsInvulnerable = true;
        _actor.IsInvulnerable.ShouldBeTrue();
    }

    [Fact]
    public void IsInvulnerable_should_roundtrip_false()
    {
        _actor.IsInvulnerable = false;
        _actor.IsInvulnerable.ShouldBeFalse();
    }

    [Fact]
    public void Skin_should_roundtrip()
    {
        _actor.Skin = 99;
        _actor.Skin.ShouldBe(99);
    }

    [Fact]
    public void ApplyAnim_should_succeed()
    {
        _actor.ApplyAnimation("BASEBALL", "Bat_Hit_1", 1, false, false, false, false, TimeSpan.FromMilliseconds(830));
    }

    [Fact]
    public void IsStreamedIn_should_succeed()
    {
        _actor.IsStreamedIn(Player);
    }

    [Fact]
    public void ClearAnimations_should_succeed()
    {
        _actor.ApplyAnimation("BASEBALL", "Bat_Hit_1", 1, false, false, false, false, TimeSpan.FromMilliseconds(830));
        _actor.ClearAnimations();
    }
}