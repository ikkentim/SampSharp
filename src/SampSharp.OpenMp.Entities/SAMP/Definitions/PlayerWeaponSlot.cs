namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a weapon slot containing a weapon and its ammunition count.
/// </summary>
/// <param name="Weapon">The weapon type assigned to this slot.</param>
/// <param name="Ammo">The amount of ammunition for the weapon.</param>
public record struct PlayerWeaponSlot(Weapon Weapon, int Ammo);