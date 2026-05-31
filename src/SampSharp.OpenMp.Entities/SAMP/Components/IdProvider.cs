using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides an identifier.
/// </summary>
public abstract class IdProvider : Component
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IdProvider" /> class.
    /// </summary>
    /// <param name="idProvider">The open.mp id provider this component represents.</param>
    protected IdProvider(IIDProvider idProvider)
    {
        Resource = idProvider;
    }

    private IIDProvider Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(IdProvider));
            return field;
        }
    }

    /// <summary>
    /// Gets the identifier of this component.
    /// </summary>
    public virtual int Id => Resource.GetID();
}