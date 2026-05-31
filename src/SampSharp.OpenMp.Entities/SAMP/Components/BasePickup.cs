using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Provides the shared data and functionality of a pickup, regardless of whether it is global
/// (<see cref="Pickup" />) or scoped to a single player (<see cref="PlayerPickup" />).
/// </summary>
public abstract class BasePickup : WorldEntity
{
    private readonly IPickupsComponent _pickups;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePickup" /> class.
    /// </summary>
    protected BasePickup(IPickupsComponent pickups, IPickup pickup) : base(pickup.HasValue ? (IEntity)pickup : default)
    {
        _pickups = pickups;
        Resource = pickup;
    }

    private IPickup Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(BasePickup));
            return field;
        }
    }

    /// <summary>
    /// Gets the model of this pickup.
    /// </summary>
    public virtual int Model => Resource.GetModel();

    /// <summary>
    /// Gets the type of this pickup.
    /// </summary>
    public virtual PickupType SpawnType => (PickupType)Resource.GetPickupType();

    /// <summary>Changes the type of this pickup.</summary>
    /// <param name="type">The new <see cref="PickupType" />.</param>
    /// <param name="update">Whether to update the pickup visually for streamed-in players.</param>
    public virtual void SetType(PickupType type, bool update = true)
    {
        Resource.SetType((byte)type, update);
    }

    /// <summary>Changes the model of this pickup.</summary>
    /// <param name="model">The new model ID.</param>
    /// <param name="update">Whether to update the pickup visually for streamed-in players.</param>
    public virtual void SetModel(int model, bool update = true)
    {
        Resource.SetModel(model, update);
    }

    /// <summary>Sets the position of this pickup without sending a visual update.</summary>
    /// <param name="position">The new position.</param>
    public virtual void SetPositionNoUpdate(Vector3 position)
    {
        Resource.SetPositionNoUpdate(position);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!Resource.GetExtension<ComponentExtension>().IsOmpEntityDestroyed)
        {
            _pickups.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (!IsComponentAlive)
        {
            return "(Destroyed)";
        }
        return $"(Id: {Id}, Model: {Model})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="BasePickup" /> to <see cref="IPickup" />.
    /// </summary>
    public static implicit operator IPickup(BasePickup? pickup)
    {
        return pickup?.Resource ?? default;
    }
}