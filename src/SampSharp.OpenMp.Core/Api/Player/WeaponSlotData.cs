using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents data for a weapon slot, including the weapon ID and the amount of ammunition.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct WeaponSlotData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WeaponSlotData" /> struct.
    /// </summary>
    /// <param name="id">The ID of the weapon in the slot.</param>
    /// <param name="ammo">The amount of ammunition for the weapon.</param>
    public WeaponSlotData(byte id, int ammo)
    {
        Id = id;
        Ammo = ammo;
    }

    /// <summary>
    /// Gets the ID of the weapon in the slot.
    /// </summary>
    public readonly byte Id;

    /// <summary>
    /// Gets the amount of ammunition for the weapon in the slot.
    /// </summary>
    public readonly int Ammo;

    /// <summary>
    /// The static <see cref="WeaponInfo" /> entry for this weapon ID.
    /// Returns a sentinel (<see cref="PlayerWeaponType.None" />, slot <c>-1</c>) for invalid IDs.
    /// </summary>
    public WeaponInfo Info => WeaponInfo.Get(Id);

    /// <summary>
    /// The slot this weapon occupies (0-12), or <c>-1</c> if the weapon ID
    /// doesn't map to a real weapon. Mirrors <c>WeaponSlotData::slot</c> in the open.mp SDK.
    /// </summary>
    public int Slot => WeaponInfo.Get(Id).Slot;

    /// <summary>
    /// True if this weapon shoots bullets (and so consumes ammo, can be reloaded, etc.).
    /// Mirrors <c>WeaponSlotData::shootable</c> in the open.mp SDK.
    /// </summary>
    public bool Shootable => WeaponInfo.Get(Id).Type == PlayerWeaponType.Bullet;
}