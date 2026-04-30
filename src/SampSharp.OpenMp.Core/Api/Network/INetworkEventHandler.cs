namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="INetwork.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface INetworkEventHandler
{
    /// <summary>
    /// Called when a peer connects to the network.
    /// </summary>
    /// <param name="peer">The player representing the connected peer.</param>
    void OnPeerConnect(IPlayer peer);

    /// <summary>
    /// Called when a peer disconnects from the network.
    /// </summary>
    /// <param name="peer">The player representing the disconnected peer.</param>
    /// <param name="reason">The reason for the disconnection.</param>
    void OnPeerDisconnect(IPlayer peer, PeerDisconnectReason reason);
}
