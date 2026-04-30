namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerClassData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerClassData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0x185655ded843788b);

    /// <summary>
    /// Gets the player's current class information.
    /// </summary>
    /// <returns>A reference to the player's class data.</returns>
    public partial ref PlayerClass GetClass();

    /// <summary>
    /// Sets the spawn information for the player's class.
    /// </summary>
    /// <param name="info">The class spawn information to set.</param>
    public partial void SetSpawnInfo(ref PlayerClass info);

    /// <summary>
    /// Spawns the player with their current class information.
    /// </summary>
    public partial void SpawnPlayer();
}