namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the type of recording for a player.
/// </summary>
public enum PlayerRecordingType
{
    /// <summary>
    /// No recording is active.
    /// </summary>
    None,

    /// <summary>
    /// Recording player movement while in a vehicle (driver recording).
    /// </summary>
    Driver,

    /// <summary>
    /// Recording player movement while on foot.
    /// </summary>
    OnFoot
}