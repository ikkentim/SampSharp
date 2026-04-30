namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the network type used by the application.
/// </summary>
public enum ENetworkType
{
    /// <summary>
    /// The legacy RakNet network type.
    /// </summary>
    RakNetLegacy,

    /// <summary>
    /// The ENet network type.
    /// </summary>
    ENet,

    /// <summary>
    /// Marks the end of the network type enumeration.
    /// </summary>
    End
}
