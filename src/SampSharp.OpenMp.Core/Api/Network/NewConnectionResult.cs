namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the result of a new connection attempt in the open.mp server.
/// </summary>
public enum NewConnectionResult
{
    /// <summary>
    /// The connection attempt should be ignored.
    /// </summary>
    Ignore,

    /// <summary>
    /// The connection attempt failed due to a version mismatch.
    /// </summary>
    VersionMismatch,

    /// <summary>
    /// The connection attempt failed due to an invalid or bad name.
    /// </summary>
    BadName,

    /// <summary>
    /// The connection attempt failed due to an unsupported or bad modification.
    /// </summary>
    BadMod,

    /// <summary>
    /// The connection attempt failed because no player slot was available.
    /// </summary>
    NoPlayerSlot,

    /// <summary>
    /// The connection attempt was successful.
    /// </summary>
    Success
};
