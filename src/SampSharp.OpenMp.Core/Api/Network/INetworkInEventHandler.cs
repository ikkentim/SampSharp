namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="INetwork.GetPerPacketInEventDispatcher" /> and <see cref="INetwork.GetPerRPCInEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface INetworkInEventHandler
{
    /// <summary>
    /// Called when a packet is received from a player.
    /// </summary>
    /// <param name="peer">The player who sent the packet.</param>
    /// <param name="id">The ID of the packet.</param>
    /// <param name="bs">The bit stream containing the packet data.</param>
    /// <returns><see langword="true" /> if the packet should be handled; otherwise, <see langword="false" />.</returns>
    bool OnReceivePacket(IPlayer peer, int id, NetworkBitStream bs);

    /// <summary>
    /// Called when an RPC is received from a player.
    /// </summary>
    /// <param name="peer">The player who sent the RPC.</param>
    /// <param name="id">The ID of the RPC.</param>
    /// <param name="bs">The bit stream containing the RPC data.</param>
    /// <returns><see langword="true" /> if the RPC should be handled; otherwise, <see langword="false" />.</returns>
    bool OnReceiveRPC(IPlayer peer, int id, NetworkBitStream bs);
}
