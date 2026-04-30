using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a set of vehicle parameters.
/// </summary>
/// <param name="Engine">A value indicating whether the vehicle engine is on.</param>
/// <param name="Lights">A value indicating whether the vehicle lights are on.</param>
/// <param name="Alarm">A value indicating whether the vehicle alarm is on.</param>
/// <param name="Doors">A value indicating whether the vehicle doors are locked.</param>
/// <param name="Bonnet">A value indicating whether the vehicle bonnet is open.</param>
/// <param name="Boot">A value indicating whether the vehicle boot is open.</param>
/// <param name="Objective">A value indicating whether the vehicle has an objective indicator.</param>
/// <param name="Siren">A value indicating whether the vehicle siren is on.</param>
/// <param name="DoorDriver">A value indicating whether the driver's door is open.</param>
/// <param name="DoorPassenger">A value indicating whether the passenger's door is open.</param>
/// <param name="DoorBackLeft">A value indicating whether the back left door is open.</param>
/// <param name="DoorBackRight">A value indicating whether the back right door is open.</param>
/// <param name="WindowDriver">A value indicating whether the driver's window is open.</param>
/// <param name="WindowPassenger">A value indicating whether the passenger's window is open.</param>
/// <param name="WindowBackLeft">A value indicating whether the back left window is open.</param>
/// <param name="WindowBackRight">A value indicating whether the back right window is open.</param>
[StructLayout(LayoutKind.Sequential)]
public record struct VehicleParameters(
    VehicleParameterValue Engine,
    VehicleParameterValue Lights,
    VehicleParameterValue Alarm,
    VehicleParameterValue Doors,
    VehicleParameterValue Bonnet,
    VehicleParameterValue Boot,
    VehicleParameterValue Objective,
    VehicleParameterValue Siren,
    VehicleParameterValue DoorDriver,
    VehicleParameterValue DoorPassenger,
    VehicleParameterValue DoorBackLeft,
    VehicleParameterValue DoorBackRight,
    VehicleParameterValue WindowDriver,
    VehicleParameterValue WindowPassenger,
    VehicleParameterValue WindowBackLeft,
    VehicleParameterValue WindowBackRight)
{
    internal static VehicleParameters FromParams(ref VehicleParams value)
    {
        return Unsafe.As<VehicleParams, VehicleParameters>(ref value);
    }

    [Pure]
    internal VehicleParams ToParams()
    {
        return Unsafe.As<VehicleParameters, VehicleParams>(ref this);
    }
}