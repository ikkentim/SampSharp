using System.Net;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the network data of a peer.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PeerNetworkData
{
    /// <summary>
    /// Represents the network ID of a peer, including its address and port.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct NetworkID
    {
        /// <summary>
        /// Gets the address of the peer.
        /// </summary>
        public readonly PeerAddress address;

        /// <summary>
        /// Gets the port of the peer.
        /// </summary>
        public readonly ushort port;

        /// <summary>
        /// Converts the network ID to an <see cref="IPEndPoint"/> instance.
        /// </summary>
        /// <returns>An <see cref="IPEndPoint"/> representing the peer's address and port.</returns>
        public IPEndPoint ToEndpoint()
        {
            return new IPEndPoint(address.ToAddress(), port);
        }
    }

    /// <summary>
    /// Gets the network interface associated with the peer.
    /// </summary>
    public readonly INetwork network;

    /// <summary>
    /// Gets the network ID of the peer.
    /// </summary>
    public readonly NetworkID networkID;
}
