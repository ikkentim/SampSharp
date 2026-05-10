using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a collection of weapon slots for a player spawn class.
/// </summary>
public class PlayerWeaponSlots
{
    private readonly WeaponSlotData[] _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerWeaponSlots"/> class with empty weapon slots.
    /// </summary>
    public PlayerWeaponSlots()
    {
        _data = new WeaponSlotData[WeaponSlots.MAX_WEAPON_SLOTS];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerWeaponSlots"/> class with the specified weapon slot data.
    /// </summary>
    /// <param name="data">The weapon slot data array.</param>
    /// <exception cref="ArgumentException">Thrown when the data array length does not match the maximum weapon slot count.</exception>
    public PlayerWeaponSlots(WeaponSlotData[] data)
    {
        if (data.Length != WeaponSlots.MAX_WEAPON_SLOTS)
        {
            throw new ArgumentException("Invalid weapon slot count", nameof(data));
        }

        _data = data;
    }

    /// <summary>
    /// Converts this weapon slots collection to open.mp weapon slots data.
    /// </summary>
    /// <returns>A <see cref="WeaponSlots"/> instance containing the open.mp representation of the weapon slots.</returns>
    public WeaponSlots ToOmpData()
    {
        return new WeaponSlots(_data);
    }

    /// <summary>
    /// Gets or sets the weapon slot at the specified index.
    /// </summary>
    /// <param name="slot">The zero-based index of the weapon slot.</param>
    /// <returns>A <see cref="PlayerWeaponSlot"/> containing the weapon and ammo information.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the slot index is less than 0 or greater than or equal to the maximum weapon slot count.</exception>
    public PlayerWeaponSlot this[int slot]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(slot, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(slot, WeaponSlots.MAX_WEAPON_SLOTS);
            var data = _data[slot];

            return new PlayerWeaponSlot((Weapon)data.Id, data.Ammo);
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(slot, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(slot, WeaponSlots.MAX_WEAPON_SLOTS);

            _data[slot] = new WeaponSlotData((byte)(int)value.Weapon, value.Ammo);
        }
    }
}