using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class WorldServiceTests : TestBase
{
    private IWorldService Sut => Services.GetRequiredService<IWorldService>();

    [Fact]
    public void Gravity_set_should_succeed()
    {
        Sut.Gravity = 0.008f;
    }

    [Fact]
    public void Gravity_roundtrip_should_succeed()
    {
        Sut.Gravity = 0.008f;
        Sut.Gravity.ShouldBe(0.008f);
    }

    [Fact]
    public void Gravity_below_minus_50_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Sut.Gravity = -50.1f);
    }

    [Fact]
    public void Gravity_above_50_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Sut.Gravity = 50.1f);
    }

    [Fact]
    public void CreateStaticVehicle_should_succeed()
    {
        var vehicle = Sut.CreateStaticVehicle(VehicleModelType.BMX, new Vector3(1, 2, 3), 0, 0, 0);

        try
        {
            vehicle.ShouldNotBeNull();
            vehicle.Model.ShouldBe(VehicleModelType.BMX);
        }
        finally
        {
            vehicle.Destroy();
        }
    }

    [Fact]
    public void SetObjectsDefaultCameraCollision_should_succeed()
    {
        Sut.SetObjectsDefaultCameraCollision(true);
        Sut.SetObjectsDefaultCameraCollision(false);
    }

    [Fact]
    public void SendClientMessage_with_color_should_succeed()
    {
        Sut.SendClientMessage(new Color(255, 0, 0), "Test message");
    }

    [Fact]
    public void SendClientMessage_with_color_and_format_should_succeed()
    {
        Sut.SendClientMessage(new Color(255, 0, 0), "Test {0}", "message");
    }

    [Fact]
    public void SendClientMessage_without_color_should_succeed()
    {
        Sut.SendClientMessage("Test message");
    }

    [Fact]
    public void SendClientMessage_without_color_with_format_should_succeed()
    {
        Sut.SendClientMessage("Test {0}", "message");
    }

    [Fact]
    public void SendPlayerMessageToPlayer_should_succeed()
    {
        Sut.SendPlayerMessageToPlayer(Player, "Test message");
    }

    [Fact]
    public void SendDeathMessage_should_succeed()
    {
        Sut.SendDeathMessage(Player, Player, Weapon.Colt45);
    }

    [Fact]
    public void GameText_should_succeed()
    {
        Sut.GameText("Test", TimeSpan.FromSeconds(5), GameTextStyle.Style1);
    }

    [Fact]
    public void HideGameText_should_succeed()
    {
        Sut.GameText("Test", TimeSpan.FromSeconds(5), GameTextStyle.Style1);
        Sut.HideGameText(GameTextStyle.Style1);
    }

    [Fact]
    public void CreateExplosion_should_succeed()
    {
        Sut.CreateExplosion(new Vector3(1, 2, 3), ExplosionType.LargeInvisible, 10.0f);
    }

    [Fact]
    public void SetWeather_should_succeed()
    {
        Sut.SetWeather(1);
    }
}
