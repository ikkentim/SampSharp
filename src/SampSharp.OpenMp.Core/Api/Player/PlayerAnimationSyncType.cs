namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the synchronization type for a player's animation.
/// </summary>
public enum PlayerAnimationSyncType
{
    /// <summary>
    /// The animation is not synchronized with other players.
    /// </summary>
    NoSync,

    /// <summary>
    /// The animation is synchronized with other players.
    /// </summary>
    Sync,

    /// <summary>
    /// The animation is synchronized with other players, but only affects others.
    /// </summary>
    SyncOthers
}