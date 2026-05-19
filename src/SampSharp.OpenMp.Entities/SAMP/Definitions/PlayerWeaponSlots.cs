using System.Collections;
using System.Collections.Generic;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a collection of weapon slots for a player spawn class.
/// </summary>
public class PlayerWeaponSlots : IEnumerable<PlayerWeaponSlot>
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
        ArgumentNullException.ThrowIfNull(data);

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
    /// Adds a weapon slot to this collection.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void Add(PlayerWeaponSlot item)
    {
        _data[GetSlotIndex(item.Weapon)] = new WeaponSlotData((byte)(int)item.Weapon, item.Ammo);
    }

    /// <summary>
    /// Resets the specified weapon slot.
    /// </summary>
    /// <param name="weaponSlot">The weapon slot to reset.</param>
    public void Reset(WeaponSlot weaponSlot)
    {
        var slot = (int)weaponSlot;
        ArgumentOutOfRangeException.ThrowIfLessThan(slot, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(slot, WeaponSlots.MAX_WEAPON_SLOTS);

        _data[slot] = default;
    }

    /// <summary>
    /// Removes the weapon from its associated slot.
    /// </summary>
    /// <param name="weapon">The weapon to remove.</param>
    /// <returns><see langword="true" /> if the associated slot was non-empty; otherwise, <see langword="false" />.</returns>
    public bool Remove(Weapon weapon)
    {
        var slot = GetSlotIndex(weapon);
        var hadValue = _data[slot].Id != 0 || _data[slot].Ammo != 0;
        _data[slot] = default;

        return hadValue;
    }

    /// <summary>
    /// Removes the weapon from its associated slot.
    /// </summary>
    /// <param name="item">The item whose weapon slot should be removed.</param>
    /// <returns><see langword="true" /> if the associated slot was non-empty; otherwise, <see langword="false" />.</returns>
    public bool Remove(PlayerWeaponSlot item)
    {
        return Remove(item.Weapon);
    }

    /// <summary>
    /// Gets the weapon slot at the specified index.
    /// Use <see cref="Add" />, <see cref="Reset" />, or <see cref="Remove(Weapon)" /> to modify slots.
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
    }

    /// <inheritdoc />
    public IEnumerator<PlayerWeaponSlot> GetEnumerator()
    {
        foreach (var data in _data)
        {
            if (data.Id == 0 && data.Ammo == 0)
            {
                continue;
            }

            yield return new PlayerWeaponSlot((Weapon)data.Id, data.Ammo);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static int GetSlotIndex(Weapon weapon)
    {
        var slot = WeaponInfo.Get((byte)(int)weapon).Slot;

        if (slot < 0 || slot >= WeaponSlots.MAX_WEAPON_SLOTS)
        {
            throw new ArgumentException("Weapon does not map to a valid weapon slot.", nameof(weapon));
        }

        return slot;
    }
}
