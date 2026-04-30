using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents synchronization data for a vehicle passenger.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct VehiclePassengerSyncPacket
{
    /// <summary>
    /// Gets the ID of the player.
    /// </summary>
    public readonly int PlayerID;

    /// <summary>
    /// Gets the ID of the vehicle.
    /// </summary>
    public readonly int VehicleID;

    /// <summary>
    /// Gets the combined data for seat, drive-by, cuffed state, weapon, and additional key.
    /// </summary>
    public readonly ushort DriveBySeatAdditionalKeyWeapon;

    /// <summary>
    /// Gets the seat ID of the passenger.
    /// </summary>
    public byte SeatId => (byte)(DriveBySeatAdditionalKeyWeapon & 0xb111111);

    /// <summary>
    /// Gets a value indicating whether the passenger is in drive-by mode.
    /// </summary>
    public bool DriveBy => (DriveBySeatAdditionalKeyWeapon & 0b1000000) != 0;

    /// <summary>
    /// Gets a value indicating whether the passenger is cuffed.
    /// </summary>
    public bool Cuffed => (DriveBySeatAdditionalKeyWeapon & 0b10000000) != 0;

    /// <summary>
    /// Gets the weapon ID of the passenger.
    /// </summary>
    public byte WeaponId => (byte)((DriveBySeatAdditionalKeyWeapon >> 8) & 0b111111);

    /// <summary>
    /// Gets the additional key pressed by the passenger.
    /// </summary>
    public byte AdditionalKey => (byte)(DriveBySeatAdditionalKeyWeapon >> 14);

    /// <summary>
    /// Gets the keys pressed by the passenger.
    /// </summary>
    public readonly ushort Keys;

    /// <summary>
    /// Gets the health and armor of the passenger.
    /// </summary>
    public readonly Vector2 HealthArmour;

    /// <summary>
    /// Gets the left-right movement of the passenger.
    /// </summary>
    public readonly ushort LeftRight;

    /// <summary>
    /// Gets the up-down movement of the passenger.
    /// </summary>
    public readonly ushort UpDown;

    /// <summary>
    /// Gets the position of the passenger.
    /// </summary>
    public readonly Vector3 Position;
};