namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="INetworkComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IComponent))]
public readonly partial struct INetworkComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0xea9799fd79cf8442); // ID for RakNetLegacyNetworkComponent

    /// <summary>
    /// Gets the network provided by this component.
    /// </summary>
    /// <returns>The network.</returns>
    public partial INetwork GetNetwork();
}