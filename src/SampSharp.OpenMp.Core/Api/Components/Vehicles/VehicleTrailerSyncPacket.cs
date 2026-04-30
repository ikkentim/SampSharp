using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents synchronization data for a vehicle trailer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct VehicleTrailerSyncPacket
{
    /// <summary>
    /// Gets the ID of the vehicle.
    /// </summary>
    public readonly int VehicleID;

    /// <summary>
    /// Gets the ID of the player associated with the vehicle.
    /// </summary>
    public readonly int PlayerID;

    /// <summary>
    /// Gets the position of the trailer.
    /// </summary>
    public readonly Vector3 Position;

    /// <summary>
    /// Gets the quaternion representing the trailer's rotation.
    /// </summary>
    public readonly Vector4 Quat;

    /// <summary>
    /// Gets the velocity of the trailer.
    /// </summary>
    public readonly Vector3 Velocity;

    /// <summary>
    /// Gets the turn velocity of the trailer.
    /// </summary>
    public readonly Vector3 TurnVelocity;
};