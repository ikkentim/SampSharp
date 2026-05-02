namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all reasons for a player to disconnect.
/// </summary>
public enum DisconnectReason
{
    /// <summary>
    /// The Player timed out.
    /// </summary>
    TimedOut = 0,

    /// <summary>
    /// The Player left. (/q(uit) or through the menu)
    /// </summary>
    Left = 1,

    /// <summary>
    /// The Player was kicked or banned.
    /// </summary>
    Kicked = 2
}