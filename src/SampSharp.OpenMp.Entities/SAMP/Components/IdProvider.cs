using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides an identifier.
/// </summary>
/// <param name="idProvider">The open.mp id provider this component represents.</param>
public abstract class IdProvider(IIDProvider idProvider) : Component
{
    /// <summary>
    /// Gets the identifier of this component.
    /// </summary>
    public virtual int Id => idProvider.GetID();
}