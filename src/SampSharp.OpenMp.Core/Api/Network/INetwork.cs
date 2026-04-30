using System.Runtime.InteropServices.Marshalling;
using SampSharp.OpenMp.Core.Std;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="INetwork" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible))]
public readonly partial struct INetwork
{
    /// <summary>
    /// Gets the network type of the network.
    /// </summary>
    /// <returns>The network type of the network.</returns>
    public partial ENetworkType GetNetworkType();

    /// <summary>
    /// Gets the dispatcher which dispatches network events.
    /// </summary>
    /// <returns>An event dispatcher for network events.</returns>
    public partial IEventDispatcher<INetworkEventHandler> GetEventDispatcher();

    /// <summary>
    /// Gets the dispatcher which dispatches incoming network events.
    /// </summary>
    /// <returns>An event dispatcher for incoming network events.</returns>
    public partial IEventDispatcher<INetworkInEventHandler> GetInEventDispatcher();

    /// <summary>
    /// Gets the dispatcher which dispatches outgoing network events.
    /// </summary>
    /// <returns>An event dispatcher for outgoing network events.</returns>
    public partial IEventDispatcher<INetworkOutEventHandler> GetOutEventDispatcher();

    /// <summary>
    /// Sends a packet to a network peer.
    /// </summary>
    /// <param name="peer">The network peer to send the packet to.</param>
    /// <param name="data">The data span with the length in bits.</param>
    /// <param name="channel">The channel to use for sending the packet.</param>
    /// <param name="dispatchEvents">Whether to dispatch packet-related events.</param>
    /// <returns><c>true</c> if the packet was sent successfully; otherwise, <c>false</c>.</returns>
    public partial bool SendPacket(IPlayer peer, SpanLite<byte> data, int channel, bool dispatchEvents = true);

    /// <summary>
    /// Broadcasts a packet to all peers on this network.
    /// </summary>
    /// <param name="data">The data span with the length in bits.</param>
    /// <param name="channel">The channel to use for broadcasting the packet.</param>
    /// <param name="exceptPeer">The peer to exclude from the broadcast.</param>
    /// <param name="dispatchEvents">Whether to dispatch packet-related events.</param>
    /// <returns><c>true</c> if the packet was broadcast successfully; otherwise, <c>false</c>.</returns>
    public partial bool BroadcastPacket(SpanLite<byte> data, int channel, IPlayer exceptPeer = default, bool dispatchEvents = true);

    /// <summary>
    /// Sends an RPC to a network peer.
    /// </summary>
    /// <param name="peer">The network peer to send the RPC to.</param>
    /// <param name="id">The RPC ID for the current network.</param>
    /// <param name="data">The data span with the length in bits.</param>
    /// <param name="channel">The channel to use for sending the RPC.</param>
    /// <param name="dispatchEvents">Whether to dispatch RPC-related events.</param>
    /// <returns><c>true</c> if the RPC was sent successfully; otherwise, <c>false</c>.</returns>
    public partial bool SendRPC(IPlayer peer, int id, SpanLite<byte> data, int channel, bool dispatchEvents = true);

    /// <summary>
    /// Broadcasts an RPC to all peers on this network.
    /// </summary>
    /// <param name="id">The RPC ID for the current network.</param>
    /// <param name="data">The data span with the length in bits.</param>
    /// <param name="channel">The channel to use for broadcasting the RPC.</param>
    /// <param name="exceptPeer">The peer to exclude from the broadcast.</param>
    /// <param name="dispatchEvents">Whether to dispatch RPC-related events.</param>
    /// <returns><c>true</c> if the RPC was broadcast successfully; otherwise, <c>false</c>.</returns>
    public partial bool BroadcastRPC(int id, SpanLite<byte> data, int channel, IPlayer exceptPeer = default, bool dispatchEvents = true);

    /// <summary>
    /// Gets network statistics for a specific player or the entire network.
    /// </summary>
    /// <param name="player">The player to get statistics for, or <c>default</c> for the entire network.</param>
    /// <returns>The network statistics.</returns>
    public partial NetworkStats GetStatistics(IPlayer player = default);

    /// <summary>
    /// Gets the last ping for a peer on this network.
    /// </summary>
    /// <param name="peer">The network peer to get the ping for.</param>
    /// <returns>The last ping value, or 0 if the peer is not on this network.</returns>
    public partial uint GetPing(IPlayer peer);

    /// <summary>
    /// Disconnects a peer from the network.
    /// </summary>
    /// <param name="peer">The network peer to disconnect.</param>
    public partial void Disconnect(IPlayer peer);

    /// <summary>
    /// Bans a peer from the network.
    /// </summary>
    /// <param name="entry">The ban entry containing details about the ban.</param>
    /// <param name="expire">The duration of the ban before it expires.</param>
    public partial void Ban(BanEntry entry, [MarshalUsing(typeof(MillisecondsMarshaller))] TimeSpan expire);

    /// <summary>
    /// Unbans a peer from the network.
    /// </summary>
    /// <param name="entry">The ban entry to remove.</param>
    public partial void Unban(BanEntry entry);

    /// <summary>
    /// Updates server parameters.
    /// </summary>
    public partial void Update();

    /// <summary>
    /// Gets the dispatcher which dispatches incoming network events bound to a specific RPC ID.
    /// </summary>
    /// <returns>An indexed event dispatcher for incoming RPC events.</returns>
    public partial IIndexedEventDispatcher<ISingleNetworkInEventHandler> GetPerRPCInEventDispatcher();

    /// <summary>
    /// Gets the dispatcher which dispatches incoming network events bound to a specific packet ID.
    /// </summary>
    /// <returns>An indexed event dispatcher for incoming packet events.</returns>
    public partial IIndexedEventDispatcher<ISingleNetworkInEventHandler> GetPerPacketInEventDispatcher();

    /// <summary>
    /// Gets the dispatcher which dispatches outgoing network events bound to a specific RPC ID.
    /// </summary>
    /// <returns>An indexed event dispatcher for outgoing RPC events.</returns>
    public partial IIndexedEventDispatcher<ISingleNetworkOutEventHandler> GetPerRPCOutEventDispatcher();

    /// <summary>
    /// Gets the dispatcher which dispatches outgoing network events bound to a specific packet ID.
    /// </summary>
    /// <returns>An indexed event dispatcher for outgoing packet events.</returns>
    public partial IIndexedEventDispatcher<ISingleNetworkOutEventHandler> GetPerPacketOutEventDispatcher();
}