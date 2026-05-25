using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class VehicleInfoServiceTests : TestBase
{
    private IVehicleInfoService Sut => Services.GetRequiredService<IVehicleInfoService>();

    [Fact]
    public void GetComponentType_should_return_valid_type()
    {
        var type = Sut.GetComponentType(1025);
        type.ShouldBe(CarModType.Hood);
    }

    [Fact]
    public void GetModelInfo_should_return_non_zero_size()
    {
        var size = Sut.GetModelInfo(VehicleModelType.Landstalker, VehicleModelInfoType.Size);
        (size.X > 0 || size.Y > 0 || size.Z > 0).ShouldBeTrue();
    }

    [Fact]
    public void IsValidComponentForVehicle_should_return_true_for_valid_component()
    {
        Sut.IsValidComponentForVehicle(VehicleModelType.Landstalker, 1025).ShouldBeTrue();
    }

    [Fact]
    public void IsValidComponentForVehicle_should_return_false_for_invalid_component()
    {
        Sut.IsValidComponentForVehicle(VehicleModelType.BMX, 1025).ShouldBeFalse();
    }

    [Fact]
    public void GetRandomVehicleColor_should_succeed()
    {
        _ = Sut.GetRandomVehicleColor(VehicleModelType.Landstalker);
    }

    [Fact]
    public void GetColorFromVehicleColor_should_succeed()
    {
        var color = Sut.GetColorFromVehicleColor(3);
        color.ShouldNotBe(default(Color));
    }

    [Fact]
    public void GetPassengerSeatCount_should_be_positive_for_multi_seat_vehicle()
    {
        Sut.GetPassengerSeatCount(VehicleModelType.Landstalker).ShouldBeGreaterThan(0);
    }

    [Fact]
    public void GetPassengerSeatCount_should_be_zero_for_bicycle()
    {
        Sut.GetPassengerSeatCount(VehicleModelType.BMX).ShouldBe(0);
    }
}
