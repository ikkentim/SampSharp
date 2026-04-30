using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents an array of vehicle models.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct VehicleModelsArray
{
    /// <summary>
    /// Gets the array of vehicle model values.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = OpenMpConstants.MAX_VEHICLE_MODELS)]
    public readonly byte[] Values;
}