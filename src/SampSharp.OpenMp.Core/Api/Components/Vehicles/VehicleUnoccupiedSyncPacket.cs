using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents synchronization data for an unoccupied vehicle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct VehicleUnoccupiedSyncPacket
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
    /// Gets the seat ID in the vehicle.
    /// </summary>
    public readonly byte SeatID;

    /// <summary>
    /// Gets the roll vector of the vehicle.
    /// </summary>
    public readonly Vector3 Roll;

    /// <summary>
    /// Gets the rotation vector of the vehicle.
    /// </summary>
    public readonly Vector3 Rotation;

    /// <summary>
    /// Gets the position of the vehicle.
    /// </summary>
    public readonly Vector3 Position;

    /// <summary>
    /// Gets the velocity of the vehicle.
    /// </summary>
    public readonly Vector3 Velocity;

    /// <summary>
    /// Gets the angular velocity of the vehicle.
    /// </summary>
    public readonly Vector3 AngularVelocity;

    /// <summary>
    /// Gets the health of the vehicle.
    /// </summary>
    public readonly float Health;
};