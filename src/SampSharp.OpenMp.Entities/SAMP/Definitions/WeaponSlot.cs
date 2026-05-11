namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents an open.mp weapon slot.
/// </summary>
public enum WeaponSlot
{
    /// <summary>
    /// The weapon does not map to a valid slot.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// Unarmed weapons.
    /// </summary>
    Unarmed = 0,

    /// <summary>
    /// Melee weapons.
    /// </summary>
    Melee = 1,

    /// <summary>
    /// Pistols.
    /// </summary>
    Pistol = 2,

    /// <summary>
    /// Shotguns.
    /// </summary>
    Shotgun = 3,

    /// <summary>
    /// Machine guns.
    /// </summary>
    MachineGun = 4,

    /// <summary>
    /// Assault rifles.
    /// </summary>
    AssaultRifle = 5,

    /// <summary>
    /// Long rifles.
    /// </summary>
    LongRifle = 6,

    /// <summary>
    /// Artillery weapons.
    /// </summary>
    Artillery = 7,

    /// <summary>
    /// Explosives.
    /// </summary>
    Explosives = 8,

    /// <summary>
    /// Equipment.
    /// </summary>
    Equipment = 9,

    /// <summary>
    /// Gifts.
    /// </summary>
    Gift = 10,

    /// <summary>
    /// Gadgets.
    /// </summary>
    Gadget = 11,

    /// <summary>
    /// Detonators.
    /// </summary>
    Detonator = 12
}
