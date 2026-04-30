namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the reasons for a peer disconnecting from the network.
/// </summary>
public enum PeerDisconnectReason
{
    /// <summary>
    /// The peer was disconnected due to a timeout.
    /// </summary>
    Timeout,

    /// <summary>
    /// The peer voluntarily quit the session.
    /// </summary>
    Quit,

    /// <summary>
    /// The peer was kicked from the session.
    /// </summary>
    Kicked,

    /// <summary>
    /// The peer was disconnected for a custom reason.
    /// </summary>
    Custom,

    /// <summary>
    /// The peer was disconnected because the mode ended.
    /// </summary>
    ModeEnd
}
