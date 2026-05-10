using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents the spawn configuration data for a player class.
/// </summary>
public record PlayerSpawnData
{
    /// <summary>
    /// Gets or sets the team ID for the player spawn.
    /// </summary>
    public int Team { get; set; }

    /// <summary>
    /// Gets or sets the skin model ID for the player spawn.
    /// </summary>
    public int Skin { get; set; }

    /// <summary>
    /// Gets or sets the spawn location coordinates.
    /// </summary>
    public Vector3 Location { get; set; }

    /// <summary>
    /// Gets or sets the spawn angle in degrees.
    /// </summary>
    public float Angle { get; set; }

    /// <summary>
    /// Gets or sets the weapon slots assigned at spawn.
    /// </summary>
    public PlayerWeaponSlots Weapons { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpawnData"/> class with default values.
    /// </summary>
    public PlayerSpawnData()
    {
        Weapons = new PlayerWeaponSlots();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpawnData"/> class with the specified spawn configuration.
    /// </summary>
    /// <param name="team">The team ID.</param>
    /// <param name="skin">The skin model ID.</param>
    /// <param name="location">The spawn location coordinates.</param>
    /// <param name="angle">The spawn angle in degrees.</param>
    /// <param name="weapons">The weapon slots assigned at spawn.</param>
    public PlayerSpawnData(int team, int skin, Vector3 location, float angle, PlayerWeaponSlots weapons)
    {
        Team = team;
        Skin = skin;
        Location = location;
        Angle = angle;
        Weapons = weapons;
    }

    /// <summary>
    /// Creates a <see cref="PlayerSpawnData"/> instance from open.mp player class data.
    /// </summary>
    /// <param name="playerClass">The open.mp player class data.</param>
    /// <returns>A <see cref="PlayerSpawnData"/> instance containing the spawn configuration.</returns>
    public static PlayerSpawnData FromOmpData(ref PlayerClass playerClass)
    {
        return new PlayerSpawnData(playerClass.Team, playerClass.Skin, playerClass.Spawn, playerClass.Angle, new PlayerWeaponSlots(playerClass.Weapons.Data));
    }

    /// <summary>
    /// Converts this spawn data to open.mp player class data.
    /// </summary>
    /// <returns>A <see cref="PlayerClass"/> instance containing the open.mp representation of the spawn data.</returns>
    public PlayerClass ToOmpData()
    {
        return new PlayerClass(Team, Skin, Location, Angle, Weapons.ToOmpData());
    }
}