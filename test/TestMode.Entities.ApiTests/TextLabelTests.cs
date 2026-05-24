using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class TextLabelTests : TestBase
{
    private readonly TextLabel _textLabel;

    public TextLabelTests()
    {
        _textLabel = Services.GetRequiredService<IWorldService>().CreateTextLabel("text", Color.Red, new Vector3(10, 20, 30), 40);
    }

    protected override void Cleanup()
    {
        _textLabel.DestroyEntity();
    }

    [Fact]
    public void CreatePlayerTextLabel_should_set_properties()
    {
        _textLabel.Text.ShouldBe("text");
        _textLabel.Color.ShouldBe(Color.Red);
        _textLabel.DrawDistance.ShouldBe(40);
        _textLabel.TestLos.ShouldBeTrue();
    }

    [Fact]
    public void Text_should_roundtrip()
    {
        _textLabel.Text = "new text";
        _textLabel.Text.ShouldBe("new text");
    }

    [Fact]
    public void Color_should_roundtrip()
    {
        _textLabel.Color = Color.Blue;
        _textLabel.Color.ShouldBe(Color.Blue);
    }

    [Fact]
    public void AttachedEntity_should_be_null()
    {
        _textLabel.AttachedEntity.ShouldBeNull();
    }

    [Fact]
    public void Attach_to_player_should_succeed()
    {
        _textLabel.Attach(Player);
        _textLabel.AttachedEntity.ShouldBe(Player);
    }

    [Fact]
    public void Attach_to_vehicle_should_succeed()
    {
        var vehicle = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.Alpha, Vector3.Zero, 0, 0, 0);

        try
        {
            _textLabel.Attach(vehicle);
            _textLabel.AttachedEntity.ShouldBe(vehicle);
        }
        finally
        {
            vehicle.DestroyEntity();
        }
    }

    [Fact]
    public void AttachedPlayer_should_be_null_initially()
    {
        _textLabel.AttachedPlayer.ShouldBeNull();
    }

    [Fact]
    public void AttachedVehicle_should_be_null_initially()
    {
        _textLabel.AttachedVehicle.ShouldBeNull();
    }

    [Fact]
    public void AttachedPlayer_should_be_set_after_attach()
    {
        _textLabel.Attach(Player);
        _textLabel.AttachedPlayer.ShouldBe(Player);
    }

    [Fact]
    public void AttachedVehicle_should_be_set_after_attach()
    {
        var vehicle = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.Alpha, Vector3.Zero, 0, 0, 0);

        try
        {
            _textLabel.Attach(vehicle);
            _textLabel.AttachedVehicle.ShouldBe(vehicle);
        }
        finally
        {
            vehicle.DestroyEntity();
        }
    }

    [Fact]
    public void DetachFromPlayer_should_succeed()
    {
        _textLabel.Attach(Player);
        _textLabel.DetachFromPlayer(new Vector3(10, 20, 30));
        _textLabel.AttachedPlayer.ShouldBeNull();
    }

    [Fact]
    public void DetachFromVehicle_should_succeed()
    {
        var vehicle = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.Alpha, Vector3.Zero, 0, 0, 0);

        try
        {
            _textLabel.Attach(vehicle);
            _textLabel.DetachFromVehicle(new Vector3(10, 20, 30));
            _textLabel.AttachedVehicle.ShouldBeNull();
        }
        finally
        {
            vehicle.DestroyEntity();
        }
    }

    [Fact]
    public void SetColorAndText_should_update_text_and_color()
    {
        _textLabel.SetColorAndText(Color.Blue, "updated text");
        _textLabel.Text.ShouldBe("updated text");
        _textLabel.Color.ShouldBe(Color.Blue);
    }

    [Fact]
    public void IsStreamedInForPlayer_should_succeed()
    {
        _ = _textLabel.IsStreamedInForPlayer(Player);
    }

    [Fact]
    public void StreamInForPlayer_should_succeed()
    {
        _textLabel.StreamInForPlayer(Player);
    }

    [Fact]
    public void StreamOutForPlayer_should_succeed()
    {
        _textLabel.StreamInForPlayer(Player);
        _textLabel.StreamOutForPlayer(Player);
    }
}