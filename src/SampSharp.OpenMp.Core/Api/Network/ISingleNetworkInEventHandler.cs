namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="INetwork.GetPerPacketInEventDispatcher"/> and <see cref="INetwork.GetPerRPCInEventDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface ISingleNetworkInEventHandler
{
    /// <summary>
    /// Called when a network packet is received from a player.
    /// </summary>
    /// <param name="peer">The player who sent the packet.</param>
    /// <param name="bs">The bit stream containing the packet data.</param>
    /// <returns><see langword="true" /> if the packet should be handled; otherwise, <see langword="false" />.</returns>
    bool OnReceive(IPlayer peer, NetworkBitStream bs);
}
