using System.Net;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a network peer address, supporting both IPv4 and IPv6.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PeerAddress
{
    /// <summary>
    /// Indicates whether the address is an IPv6 address.
    /// </summary>
    public readonly bool Ipv6;

    /// <summary>
    /// The IPv4 address represented as a 32-bit unsigned integer.
    /// This field is only used if <see cref="Ipv6"/> is <c>false</c>.
    /// </summary>
    public readonly uint V4;

    /// <summary>
    /// The raw bytes of the address.
    /// For IPv6, this contains the full 16-byte address.
    /// For IPv4, this may be unused.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public readonly byte[] Bytes;

    /// <summary>
    /// Converts the peer address to an <see cref="IPAddress"/> instance.
    /// </summary>
    /// <returns>An <see cref="IPAddress"/> representing the peer address.</returns>
    public IPAddress ToAddress()
    {
        if (Ipv6)
        {
            Span<byte> buf = stackalloc byte[46]; // INET6_ADDRSTRLEN
            Bytes.CopyTo(buf);
            return new IPAddress(buf);
        }
        else
        {
            return new IPAddress(V4);
        }
    }
}
