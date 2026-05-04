using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

public class PlayerTextLabelTests : TestBase
{
    private readonly PlayerTextLabel _textLabel;

    public PlayerTextLabelTests()
    {
        _textLabel = Services.GetRequiredService<IWorldService>().CreatePlayerTextLabel(Player, "text", Color.Red, new Vector3(10, 20, 30), 40);
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
}