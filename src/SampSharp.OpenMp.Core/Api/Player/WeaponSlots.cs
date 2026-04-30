using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the weapon slots of a player, containing data for each weapon slot.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct WeaponSlots
{
    /// <summary>
    /// The maximum number of weapon slots available.
    /// </summary>
    public const int MAX_WEAPON_SLOTS = 13;

    /// <summary>
    /// The array of weapon slot data, where each index corresponds to a specific weapon slot.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WEAPON_SLOTS)]
    public readonly WeaponSlotData[] Data;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeaponSlots"/> struct.
    /// </summary>
    /// <param name="data">The array of weapon slot data.</param>
    /// <exception cref="ArgumentException">Thrown if the length of <paramref name="data"/> is not equal to <see cref="MAX_WEAPON_SLOTS"/>.</exception>
    public WeaponSlots(WeaponSlotData[] data)
    {
        if (data.Length != MAX_WEAPON_SLOTS)
        {
            throw new ArgumentException("Slot count should be MAX_WEAPON_SLOTS", nameof(data));
        }

        Data = data;
    }
}