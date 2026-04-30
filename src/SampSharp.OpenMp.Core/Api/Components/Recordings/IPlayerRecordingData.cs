namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerRecordingData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerRecordingData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0x34DB532857286482);

    /// <summary>
    /// Starts recording the player's movements and actions.
    /// </summary>
    /// <param name="type">The type of recording to start (e.g., driver, on foot).</param>
    /// <param name="file">The file path where the recording will be saved.</param>
    public partial void Start(PlayerRecordingType type, string file);

    /// <summary>
    /// Stops the current player recording.
    /// </summary>
    public partial void Stop();
}