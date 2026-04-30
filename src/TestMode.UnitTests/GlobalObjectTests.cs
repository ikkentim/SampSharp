using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;
using ObjectMaterialSize = SampSharp.Entities.SAMP.ObjectMaterialSize;
using ObjectMaterialTextAlign = SampSharp.Entities.SAMP.ObjectMaterialTextAlign;

namespace TestMode.UnitTests;

public class GlobalObjectTests : TestBase
{
    private readonly IWorldService _worldService;
    private readonly GlobalObject _object;

    public GlobalObjectTests()
    {
        _worldService = Services.GetRequiredService<IWorldService>();
        _object = _worldService.CreateObject(100, new Vector3(10, 20, 30), new Vector3(20, 10, 0), 30);
    }

    protected override void Cleanup()
    {
        _object?.Destroy();
    }

    [Fact]
    public void CreateObject_should_set_properties()
    {
       _object.ModelId.ShouldBe(100);
       _object.Position.ShouldBe(new Vector3(10, 20, 30));
       _object.RotationEuler.ShouldBe(new Vector3(20, 10, 0), 0.8f);
       _object.DrawDistance.ShouldBe(30);
    }

    [Fact]
    public void Position_should_roundtrip()
    {
        _object.Position = new Vector3(20, 30, 40);
        _object.Position.ShouldBe(new Vector3(20, 30, 40));
    }
    
    [Fact]
    public void IsMoving_should_return_correct_value()
    {
        _object.Move(new Vector3(100, 200, 300), 10, Vector3.Zero);
        _object.IsMoving.ShouldBeTrue();
        _object.Stop();
        _object.IsMoving.ShouldBeFalse();
    }

    [Fact]
    public void RotationEuler_should_roundtrip()
    {
        _object.RotationEuler = new Vector3(20, 30, 40);
        _object.RotationEuler.ShouldBe(new Vector3(20, 30, 40), 0.8f);
    }
    
    [Fact]
    public void Move_and_Stop_should_succeed()
    {
        _object.Move(new Vector3(100, 200, 300), 10, Vector3.Zero);
        _object.Stop();
    }

    [Fact]
    public void Move_time_should_be_correct()
    {
        _object.Position = new Vector3(100, 0, 0);
        var result = _object.Move(new Vector3(200, 0, 0), 10, Vector3.Zero);
        _object.Stop();

        result.ShouldBe(10000);
    }

    [Fact]
    public void DisableCameraCollisions_should_succeed()
    {
        _object.DisableCameraCollisions();
    }
    
    [Fact]
    public void SetMaterial_should_succeed()
    {
        _object.SetMaterial(0, 0, "none", "none", Color.White);
    }
     
    [Fact]
    public void SetMaterialText_should_succeed()
    {
        _object.SetMaterialText(0, "test", ObjectMaterialSize.X128X128, "Arial", 12, true, Color.White, Color.White, ObjectMaterialTextAlign.Center);
    }

    [Fact]
    public void Core_GetMaterialData_should_succeed()
    {
        _object.SetMaterial(0, 123, "none", "none", Color.White);

        IObject obj = _object;

        obj.GetMaterialData(0, out var data).ShouldBeTrue();
        data.ShouldNotBeNull();
        data.Model.ShouldBe(123);
        data.Txd.ShouldBe("none");
        data.Texture.ShouldBe("none");
        data.MaterialColour.ShouldBe((Colour)Color.White);
    }

    [Fact]
    public void AttachToPlayer_should_succeed()
    {
        _object.AttachTo(Player, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }

    [Fact]
    public void AttachToVehicle_should_succeed()
    {
        var vehicle = _worldService.CreateVehicle(VehicleModelType.Landstalker, new Vector3(0, 0, 0), 0, 0, 0);

        try
        {
            _object.AttachTo(vehicle, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        }
        finally
        {
            vehicle.Destroy();
        }
    }

    [Fact]
    public void AttachToObject_should_succeed()
    {
        var obj = _worldService.CreateObject(100, new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        try
        {
            _object.AttachTo(obj, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        }
        finally
        {
            obj.Destroy();
        }
    }
}