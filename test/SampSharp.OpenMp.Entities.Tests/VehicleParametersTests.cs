using System.Runtime.CompilerServices;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class VehicleParametersTests
{
    [Fact]
    public void Default_struct_should_have_Off_values_via_zero_init()
    {
        var p = default(VehicleParameters);
        p.Engine.ShouldBe(VehicleParameterValue.Off);
    }

    [Fact]
    public void With_expression_should_modify_only_specified_fields()
    {
        var original = new VehicleParameters(
            VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off,
            VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off,
            VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off,
            VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off);
        var modified = original with { Engine = VehicleParameterValue.On, Siren = VehicleParameterValue.On };
        modified.Engine.ShouldBe(VehicleParameterValue.On);
        modified.Siren.ShouldBe(VehicleParameterValue.On);
        modified.Lights.ShouldBe(VehicleParameterValue.Off);
        original.Engine.ShouldBe(VehicleParameterValue.Off);
    }

    [Fact]
    public void Equals_should_be_true_for_record_struct_with_identical_values()
    {
        var a = new VehicleParameters(
            VehicleParameterValue.On, VehicleParameterValue.Off, VehicleParameterValue.Unset, VehicleParameterValue.Off,
            VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off,
            VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off,
            VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off, VehicleParameterValue.Off);
        var b = a with { };
        a.ShouldBe(b);
    }

    [Fact]
    public void Layout_should_match_VehicleParams_size()
    {
        Unsafe.SizeOf<VehicleParameters>().ShouldBe(Unsafe.SizeOf<VehicleParams>());
    }

    [Fact]
    public void Roundtrip_through_VehicleParams_should_preserve_all_fields()
    {
        var native = new VehicleParams(
            engine: 1, lights: 0, alarm: -1, doors: 1,
            bonnet: 0, boot: 1, objective: 0, siren: 1,
            doorDriver: 0, doorPassenger: 1, doorBackLeft: 0, doorBackRight: 1,
            windowDriver: 0, windowPassenger: 1, windowBackLeft: 0, windowBackRight: 1);

        var managed = ReinterpretFromParams(ref native);

        managed.Engine.ShouldBe(VehicleParameterValue.On);
        managed.Lights.ShouldBe(VehicleParameterValue.Off);
        managed.Alarm.ShouldBe(VehicleParameterValue.Unset);
        managed.Doors.ShouldBe(VehicleParameterValue.On);
        managed.Bonnet.ShouldBe(VehicleParameterValue.Off);
        managed.Boot.ShouldBe(VehicleParameterValue.On);
        managed.Objective.ShouldBe(VehicleParameterValue.Off);
        managed.Siren.ShouldBe(VehicleParameterValue.On);
        managed.DoorDriver.ShouldBe(VehicleParameterValue.Off);
        managed.DoorPassenger.ShouldBe(VehicleParameterValue.On);
        managed.DoorBackLeft.ShouldBe(VehicleParameterValue.Off);
        managed.DoorBackRight.ShouldBe(VehicleParameterValue.On);
        managed.WindowDriver.ShouldBe(VehicleParameterValue.Off);
        managed.WindowPassenger.ShouldBe(VehicleParameterValue.On);
        managed.WindowBackLeft.ShouldBe(VehicleParameterValue.Off);
        managed.WindowBackRight.ShouldBe(VehicleParameterValue.On);

        var native2 = ReinterpretToParams(ref managed);
        native2.ShouldBe(native);
    }

    // FromParams/ToParams are internal in VehicleParameters; exercise the same Unsafe.As reinterpret here.
    private static VehicleParameters ReinterpretFromParams(ref VehicleParams value)
    {
        return Unsafe.As<VehicleParams, VehicleParameters>(ref value);
    }

    private static VehicleParams ReinterpretToParams(ref VehicleParameters value)
    {
        return Unsafe.As<VehicleParameters, VehicleParams>(ref value);
    }
}
