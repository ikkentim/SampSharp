namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IIDProvider" /> interface.
/// </summary>
[OpenMpApi]
public readonly partial struct IIDProvider
{
    /// <summary>
    /// Gets the identifier of this unit.
    /// </summary>
    /// <returns>The identifier of the unit.</returns>
    public partial int GetID();
}