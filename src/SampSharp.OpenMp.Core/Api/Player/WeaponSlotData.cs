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

    // TODO: WeaponInfo related extensions from open.mp-sdk.
}