using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents an array of carriages linked to a vehicle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct CarriagesArray
{
    /// <summary>
    /// Gets the array of vehicles representing the carriages.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = OpenMpConstants.MAX_VEHICLE_CARRIAGES)]
    public readonly IVehicle[] Values;
}