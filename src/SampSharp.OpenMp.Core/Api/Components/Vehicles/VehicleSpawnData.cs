using System.Numerics;
using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the data required to spawn a vehicle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct VehicleSpawnData
{
    /// <summary>
    /// Gets the respawn delay for the vehicle.
    /// </summary>
    public readonly Seconds respawnDelay;

    /// <summary>
    /// Gets the model ID of the vehicle.
    /// </summary>
    public readonly int modelID;

    /// <summary>
    /// Gets the position where the vehicle will spawn.
    /// </summary>
    public readonly Vector3 position;

    /// <summary>
    /// Gets the Z rotation of the vehicle.
    /// </summary>
    public readonly float zRotation;

    /// <summary>
    /// Gets the primary color of the vehicle.
    /// </summary>
    public readonly int colour1;

    /// <summary>
    /// Gets the secondary color of the vehicle.
    /// </summary>
    public readonly int colour2;

    /// <summary>
    /// Gets a value indicating whether the vehicle has a siren.
    /// </summary>
    public readonly BlittableBoolean siren;

    /// <summary>
    /// Gets the interior ID of the vehicle.
    /// </summary>
    public readonly int interior;
}