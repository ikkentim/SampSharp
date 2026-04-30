namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the status of a player's name during validation or update operations.
/// </summary>
public enum EPlayerNameStatus
{
    /// <summary>
    /// The player's name was successfully updated.
    /// </summary>
    Updated,

    /// <summary>
    /// The player's name is already taken by another player.
    /// </summary>
    Taken,

    /// <summary>
    /// The player's name is invalid and does not meet the required criteria.
    /// </summary>
    Invalid
}