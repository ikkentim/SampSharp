using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents an update for an unoccupied vehicle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct UnoccupiedVehicleUpdate
{
    /// <summary>
    /// Gets the seat ID in the vehicle.
    /// </summary>
    public readonly byte seat;

    /// <summary>
    /// Gets the position of the vehicle.
    /// </summary>
    public readonly Vector3 position;

    /// <summary>
    /// Gets the velocity of the vehicle.
    /// </summary>
    public readonly Vector3 velocity;
}