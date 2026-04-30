namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="INetwork.GetPerPacketOutEventDispatcher"/> and <see cref="INetwork.GetPerRPCOutEventDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface ISingleNetworkOutEventHandler
{
    /// <summary>
    /// Called when a packet is sent to a player.
    /// </summary>
    /// <param name="peer">The player to whom the packet is being sent.</param>
    /// <param name="bs">The network bit stream containing the packet data.</param>
    /// <returns><see langword="true" /> if the packet should be sent; otherwise, <see langword="false" />.</returns>
    bool OnSend(IPlayer peer, NetworkBitStream bs);
}
