namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IExtension" /> interface.
/// </summary>
[OpenMpApi]
[OpenMpApiPartial]
public readonly partial struct IExtension
{
    public partial interface IManagedInterface
    {
        /// <summary>
        /// Gets the identifier of the extension type. <see cref="IPool{T}"/>
        /// </summary>
        static abstract UID ExtensionId { get; }
    }
}