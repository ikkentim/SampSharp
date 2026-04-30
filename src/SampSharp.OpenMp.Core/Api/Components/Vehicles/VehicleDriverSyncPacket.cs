using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents synchronization data for a vehicle driver.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct VehicleDriverSyncPacket
{
    /// <summary>
    /// Gets the ID of the player driving the vehicle.
    /// </summary>
    public readonly int PlayerID;

    /// <summary>
    /// Gets the ID of the vehicle being driven.
    /// </summary>
    public readonly ushort VehicleID;

    /// <summary>
    /// Gets the left-right movement of the vehicle.
    /// </summary>
    public readonly ushort LeftRight;

    /// <summary>
    /// Gets the up-down movement of the vehicle.
    /// </summary>
    public readonly ushort UpDown;

    /// <summary>
    /// Gets the keys pressed by the driver.
    /// </summary>
    public readonly ushort Keys;

    /// <summary>
    /// Gets the rotation quaternion of the vehicle.
    /// </summary>
    public readonly GTAQuat Rotation;

    /// <summary>
    /// Gets the position of the vehicle.
    /// </summary>
    public readonly Vector3 Position;

    /// <summary>
    /// Gets the velocity of the vehicle.
    /// </summary>
    public readonly Vector3 Velocity;

    /// <summary>
    /// Gets the health of the vehicle.
    /// </summary>
    public readonly float Health;

    /// <summary>
    /// Gets the health and armor of the driver.
    /// </summary>
    public readonly Vector2 PlayerHealthArmour;

    /// <summary>
    /// Gets the siren state of the vehicle.
    /// </summary>
    public readonly byte Siren;

    /// <summary>
    /// Gets the landing gear state of the vehicle.
    /// </summary>
    public readonly byte LandingGear;

    /// <summary>
    /// Gets the ID of the trailer attached to the vehicle.
    /// </summary>
    public readonly ushort TrailerID;

    /// <summary>
    /// Gets a value indicating whether the vehicle has a trailer attached.
    /// </summary>
    public readonly BlittableBoolean HasTrailer;

    /// <summary>
    /// Gets the combined data for additional key and weapon.
    /// </summary>
    public readonly byte AdditionalKeyWeapon;

    /// <summary>
    /// Gets the thrust angle for Hydra vehicles.
    /// </summary>
    public readonly uint HydraThrustAngle;

    /// <summary>
    /// Gets the weapon ID of the driver.
    /// </summary>
    public byte WeaponID => (byte)(AdditionalKeyWeapon & 0b111111);

    /// <summary>
    /// Gets the additional key pressed by the driver.
    /// </summary>
    public byte AdditionalKey => (byte)(AdditionalKeyWeapon >> 6);

    /// <summary>
    /// Gets the speed of the train if the vehicle is a train.
    /// </summary>
    public float TrainSpeed => GetTrainSpeed();

    private unsafe float GetTrainSpeed()
    {
        var value = HydraThrustAngle;
        return *(float*)&value;
    }
}