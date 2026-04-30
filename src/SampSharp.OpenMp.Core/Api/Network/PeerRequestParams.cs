using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the parameters of a peer request in the network API.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct PeerRequestParams
{
    /// <summary>
    /// Gets the version of the client making the request.
    /// </summary>
    public readonly ClientVersion Version;

    /// <summary>
    /// Gets the name of the client version as a string view.
    /// </summary>
    public readonly StringView VersionName;

    /// <summary>
    /// Gets a value indicating whether the client is a bot.
    /// </summary>
    public readonly bool Bot;

    /// <summary>
    /// Gets the name of the client making the request.
    /// </summary>
    public readonly StringView Name;

    /// <summary>
    /// Gets the serial number of the client making the request.
    /// </summary>
    public readonly StringView Serial;

    /// <summary>
    /// Gets a value indicating whether the client is using the official client.
    /// </summary>
    public readonly bool IsUsingOfficialClient;
};
