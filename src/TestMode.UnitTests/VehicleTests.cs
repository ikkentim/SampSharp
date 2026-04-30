using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

public class VehicleTests : TestBase
{
    private readonly Vehicle _vehicle;

    public VehicleTests()
    {
        _vehicle = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.Landstalker, new Vector3(10, 0, 5), 30, 5, 8);
    }

    protected override void Cleanup()
    {
        _vehicle?.Destroy();
    }

    [Fact]
    public void CreateVehicle_should_set_properties()
    {
        _vehicle.Model.ShouldBe(VehicleModelType.Landstalker);
        _vehicle.Position.ShouldBe(new Vector3(10, 0, 5));
        _vehicle.Angle.ShouldBe(30);
        _vehicle.Color1.ShouldBe(5);
        _vehicle.Color2.ShouldBe(8);
    }

    [Fact]
    public void Position_should_roundtrip()
    {
        _vehicle.Position = new Vector3(1, 2, 3);
        _vehicle.Position.ShouldBe(new Vector3(1, 2, 3));
    }
    
    [Fact]
    public void Alarm_should_roundtrip_true()
    {
        _vehicle.Alarm = true;
        _vehicle.Alarm.ShouldBeTrue();
    }

    [Fact]
    public void Alarm_should_roundtrip_false()
    {
        _vehicle.Alarm = false;
        _vehicle.Alarm.ShouldBeFalse();
    }

    [Fact]
    public void Bonnet_should_roundtrip_true()
    {
        _vehicle.Bonnet = true;
        _vehicle.Bonnet.ShouldBeTrue();
    }
    [Fact]
    public void Bonnet_should_roundtrip_false()
    {
        _vehicle.Bonnet = false;
        _vehicle.Bonnet.ShouldBeFalse();
    }
    [Fact]
    public void Boot_should_roundtrip_true()
    {
        _vehicle.Boot = true;
        _vehicle.Boot.ShouldBeTrue();
    }
    [Fact]
    public void Boot_should_roundtrip_false()
    {
        _vehicle.Boot = false;
        _vehicle.Boot.ShouldBeFalse();
    }
    [Fact]
    public void Doors_should_roundtrip_true()
    {
        _vehicle.Doors = true;
        _vehicle.Doors.ShouldBeTrue();
    }
    [Fact]
    public void Doors_should_roundtrip_false()
    {
        _vehicle.Doors = false;
        _vehicle.Doors.ShouldBeFalse();
    }
    [Fact]
    public void Engine_should_roundtrip_true()
    {
        _vehicle.Engine = true;
        _vehicle.Engine.ShouldBeTrue();
    }
    [Fact]
    public void Engine_should_roundtrip_false()
    {
        _vehicle.Engine = false;
        _vehicle.Engine.ShouldBeFalse();
    }
    [Fact]
    public void Objective_should_roundtrip_true()
    {
        _vehicle.Objective = true;
        _vehicle.Objective.ShouldBeTrue();
    }
    [Fact]
    public void Objective_should_roundtrip_false()
    {
        _vehicle.Objective = false;
        _vehicle.Objective.ShouldBeFalse();
    }
    [Fact]
    public void Lights_should_roundtrip_true()
    {
        _vehicle.Lights = true;
        _vehicle.Lights.ShouldBeTrue();
    }
    [Fact]
    public void Lights_should_roundtrip_false()
    {
        _vehicle.Lights = false;
        _vehicle.Lights.ShouldBeFalse();
    }
    [Fact]
    public void IsBackLeftDoorOpen_should_roundtrip_true()
    {
        _vehicle.IsBackLeftDoorOpen = true;
        _vehicle.IsBackLeftDoorOpen.ShouldBeTrue();
    }
    [Fact]
    public void IsBackLeftDoorOpen_should_roundtrip_false()
    {
        _vehicle.IsBackLeftDoorOpen = false;
        _vehicle.IsBackLeftDoorOpen.ShouldBeFalse();
    }
    [Fact]
    public void IsBackLeftWindowClosed_should_roundtrip_true()
    {
        _vehicle.IsBackLeftWindowClosed = true;
        _vehicle.IsBackLeftWindowClosed.ShouldBeTrue();
    }
    [Fact]
    public void IsBackLeftWindowClosed_should_roundtrip_false()
    {
        _vehicle.IsBackLeftWindowClosed = false;
        _vehicle.IsBackLeftWindowClosed.ShouldBeFalse();
    }
    [Fact]
    public void IsBackRightDoorOpen_should_roundtrip_true()
    {
        _vehicle.IsBackRightDoorOpen = true;
        _vehicle.IsBackRightDoorOpen.ShouldBeTrue();
    }
    [Fact]
    public void IsBackRightDoorOpen_should_roundtrip_false()
    {
        _vehicle.IsBackRightDoorOpen = false;
        _vehicle.IsBackRightDoorOpen.ShouldBeFalse();
    }
    [Fact]
    public void IsBackRightWindowClosed_should_roundtrip_true()
    {
        _vehicle.IsBackRightWindowClosed = true;
        _vehicle.IsBackRightWindowClosed.ShouldBeTrue();
    }
    [Fact]
    public void IsBackRightWindowClosed_should_roundtrip_false()
    {
        _vehicle.IsBackRightWindowClosed = false;
        _vehicle.IsBackRightWindowClosed.ShouldBeFalse();
    }
    [Fact]
    public void IsDriverDoorOpen_should_roundtrip_true()
    {
        _vehicle.IsDriverDoorOpen = true;
        _vehicle.IsDriverDoorOpen.ShouldBeTrue();
    }
    [Fact]
    public void IsDriverDoorOpen_should_roundtrip_false()
    {
        _vehicle.IsDriverDoorOpen = false;
        _vehicle.IsDriverDoorOpen.ShouldBeFalse();
    }
    [Fact]
    public void IsDriverWindowClosed_should_roundtrip_true()
    {
        _vehicle.IsDriverWindowClosed = true;
        _vehicle.IsDriverWindowClosed.ShouldBeTrue();
    }
    [Fact]
    public void IsDriverWindowClosed_should_roundtrip_false()
    {
        _vehicle.IsDriverWindowClosed = false;
        _vehicle.IsDriverWindowClosed.ShouldBeFalse();
    }
    [Fact]
    public void IsPassengerDoorOpen_should_roundtrip_true()
    {
        _vehicle.IsPassengerDoorOpen = true;
        _vehicle.IsPassengerDoorOpen.ShouldBeTrue();
    }
    [Fact]
    public void IsPassengerDoorOpen_should_roundtrip_false()
    {
        _vehicle.IsPassengerDoorOpen = false;
        _vehicle.IsPassengerDoorOpen.ShouldBeFalse();
    }
    [Fact]
    public void IsPassengerWindowClosed_should_roundtrip_true()
    {
        _vehicle.IsPassengerWindowClosed = true;
        _vehicle.IsPassengerWindowClosed.ShouldBeTrue();
    }
    [Fact]
    public void IsPassengerWindowClosed_should_roundtrip_false()
    {
        _vehicle.IsPassengerWindowClosed = false;
        _vehicle.IsPassengerWindowClosed.ShouldBeFalse();
    }

    [Fact]
    public void ChangeColor_should_succeed()
    {
        _vehicle.ChangeColor(5, 12);
    }

    [Fact]
    public void SetNumberPlate_should_succeed()
    {
        _vehicle.SetNumberPlate("SampSharp");
    }

    [Fact]
    public void AddComponent_should_roundtirp()
    {
        _vehicle.AddComponent(1025);
        _vehicle.GetComponentInSlot(CarModType.Wheels).ShouldBe(1025);
    }
    [Fact]
    public void ChangePaintjob_should_succeed()
    {
        _vehicle.ChangePaintjob(54);
    }

    [Fact]
    public void GetDistanceFromPoint_should_succeed()
    {
        _vehicle.Position = new Vector3(10, 10, 0);
        (_vehicle.GetDistanceFromPoint(new Vector3(20, 20, 0))).ShouldBe(MathF.Sqrt(10 * 10 * 2));
    }

    [Fact]
    public void IsStreamedIn_should_succeed()
    {
        _vehicle.IsStreamedIn(Player);
    }

    [Fact]
    public void LinkToInterior_should_succeed()
    {
        _vehicle.LinkToInterior(4);
        ((IVehicle)_vehicle).GetInterior().ShouldBe(4);// TODO add to api
        _vehicle.LinkToInterior(0);
    }

    [Fact]
    public void VirtualWorld_should_roundtrip()
    {
        _vehicle.VirtualWorld = 23;
        _vehicle.VirtualWorld.ShouldBe(23);
        _vehicle.VirtualWorld = 0;
    }

    [Fact]
    public void Health_should_roundtrip()
    {
        _vehicle.Health = 876;
        _vehicle.Health.ShouldBe(876);
    }

    [Fact]
    public void Repair_should_succeed()
    {
        _vehicle.Health = 500;
        _vehicle.UpdateDamageStatus(13, 14, 15, 16);
        _vehicle.Repair();

        _vehicle.GetDamageStatus(out var panels, out var doors, out var lights, out var tires);
        panels.ShouldBe(0);
        doors.ShouldBe(0);
        lights.ShouldBe(0);
        tires.ShouldBe(0);

        _vehicle.Health.ShouldBe(1000);
    }

    [Fact]
    public void UpdateDamageStatus_should_roundtrip()
    {
        _vehicle.UpdateDamageStatus(13, 14, 15, 16);
        _vehicle.GetDamageStatus(out var panels, out var doors, out var lights, out var tires);

        panels.ShouldBe(13);
        doors.ShouldBe(14);
        lights.ShouldBe(15);
        tires.ShouldBe(16);
    }
    
    [Fact]
    public void Respawn_should_succeed()
    {
        _vehicle.Respawn();
    }
    
    [Fact]
    public void SetAngularVelocity_should_succeed()
    {
        // doesn't roundtrip; the setter send a packet to the driver.
        _vehicle.SetAngularVelocity(new Vector3(5, 6, 7));
        
    }

    [Fact]
    public void Angle_setter_should_roundtrip()
    {
        _vehicle.Angle = 45.0f;
        _vehicle.Angle.ShouldBe(45);
    }

    [Fact]
    public void Model_should_be_correct()
    {
        _vehicle.Model.ShouldBe(VehicleModelType.Landstalker);
    }

    [Fact]
    public void HasTrailer_should_be_false_initially()
    {
        _vehicle.HasTrailer.ShouldBeFalse();
    }
    
    [Fact]
    public void Velocity_setter_should_succeed()
    {
        // doesn't roundtrip; the setter send a packet to the driver.
        Player.PutInVehicle(_vehicle);

        _vehicle.Velocity = new Vector3(1, 1, 1);

        Player.RemoveFromVehicle(true);
    }

    [Fact]
    public void Velocity_getter_should_succeed()
    {
        // doesn't roundtrip; the setter send a packet to the driver.
        Player.PutInVehicle(_vehicle);

        var result = _vehicle.Velocity;

        result.ShouldBe(Vector3.Zero);

        Player.RemoveFromVehicle(true);
    }

    [Fact]
    public void IsSirenOn_should_be_false()
    {
        // state is only set by driver's packets - setter only sets steaming data and does not affect getter
        // setter not available in sampsharp
        _vehicle.IsSirenOn.ShouldBeFalse();
    }

    [Fact]
    public void Rotation_should_roundtrip()
    {
        _vehicle.RotationEuler = new Vector3(0, 0, 90);
        _vehicle.RotationEuler.ShouldBe(new Vector3(0, 0, 90));
    }
    [Fact]
    public void RemoveComponent_should_succeed()
    {

        _vehicle.AddComponent(1025);
        _vehicle.RemoveComponent(1025);
        _vehicle.GetComponentInSlot(CarModType.Wheels).ShouldBe(0);
    }

    [Fact]
    public void Trailer_should_roundtrip()
    {
        var trailer = Services.GetRequiredService<IWorldService>().CreateVehicle(VehicleModelType.ArticleTrailer, new Vector3(0, 0, 0), 0, 0, 0);

        try
        {
            _vehicle.Trailer = trailer;
            _vehicle.Trailer.ShouldBe(trailer);

            _vehicle.Trailer = null;
            _vehicle.Trailer.ShouldBeNull();
        }
        finally
        {
            trailer.Destroy();
        }
    }

    [Fact]
    public void SetParametersForPlayer_should_succeed()
    {
        var parameters = new VehicleParameters(
            VehicleParameterValue.On, VehicleParameterValue.Off, VehicleParameterValue.On, VehicleParameterValue.Off,
            VehicleParameterValue.On, VehicleParameterValue.Off, VehicleParameterValue.On, VehicleParameterValue.Off,
            VehicleParameterValue.On, VehicleParameterValue.Off, VehicleParameterValue.On, VehicleParameterValue.Off,
            VehicleParameterValue.On, VehicleParameterValue.Off, VehicleParameterValue.On, VehicleParameterValue.Off);

        _vehicle.SetParametersForPlayer(Player, parameters);
    }

}