using System.Numerics;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Describes the data used to spawn (and respawn) a vehicle.
/// </summary>
/// <param name="ModelId">The vehicle model ID.</param>
/// <param name="Position">The world position where the vehicle should spawn.</param>
/// <param name="ZRotation">The Z-axis rotation in degrees.</param>
/// <param name="PrimaryColor">The primary color ID.</param>
/// <param name="SecondaryColor">The secondary color ID.</param>
/// <param name="HasSiren">A value indicating whether the vehicle has a siren.</param>
/// <param name="Interior">The interior ID.</param>
/// <param name="RespawnDelay">The respawn delay.</param>
public readonly record struct VehicleSpawnInfo(
    int ModelId,
    Vector3 Position,
    float ZRotation,
    int PrimaryColor,
    int SecondaryColor,
    bool HasSiren,
    int Interior,
    TimeSpan RespawnDelay);
