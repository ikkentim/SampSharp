namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerCheckpointData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerCheckpointData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0xbc07576aa3591a66);

    /// <summary>
    /// Gets the player's current race checkpoint data.
    /// </summary>
    /// <returns>A reference to the race checkpoint data.</returns>
    public partial IRaceCheckpointData GetRaceCheckpoint();

    /// <summary>
    /// Gets the player's current checkpoint data.
    /// </summary>
    /// <returns>A reference to the checkpoint data.</returns>
    public partial ICheckpointData GetCheckpoint();
}