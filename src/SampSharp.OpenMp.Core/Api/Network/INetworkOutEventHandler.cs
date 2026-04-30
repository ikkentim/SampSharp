namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="INetwork.GetPerPacketOutEventDispatcher" /> and <see cref="INetwork.GetPerRPCOutEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface INetworkOutEventHandler
{
    /// <summary>
    /// Called when a packet is sent to a player.
    /// </summary>
    /// <param name="peer">The player receiving the packet.</param>
    /// <param name="id">The packet ID.</param>
    /// <param name="bs">The bit stream containing the packet data.</param>
    /// <returns><see langword="true" /> if the packet should be sent; otherwise, <see langword="false" />.</returns>
    bool OnSendPacket(IPlayer peer, int id, NetworkBitStream bs);

    /// <summary>
    /// Called when an RPC is sent to a player.
    /// </summary>
    /// <param name="peer">The player receiving the RPC.</param>
    /// <param name="id">The RPC ID.</param>
    /// <param name="bs">The bit stream containing the RPC data.</param>
    /// <returns><see langword="true" /> if the RPC should be sent; otherwise, <see langword="false" />.</returns>
    bool OnSendRPC(IPlayer peer, int id, NetworkBitStream bs);
}
