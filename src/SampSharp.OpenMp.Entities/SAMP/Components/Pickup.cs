using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a component which provides the data and functionality of a pickup.</summary>
public class Pickup : WorldEntity
{
    private readonly IPickupsComponent _pickups;
    private readonly IPickup _pickup;

    /// <summary>Constructs an instance of Pickup, should be used internally.</summary>
    protected Pickup(IPickupsComponent pickups, IPickup pickup) : base((IEntity)pickup)
    {
        _pickups = pickups;
        _pickup = pickup;
    }
    
    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _pickup.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>Gets the model of this <see cref="Pickup" />.</summary>
    public virtual int Model => _pickup.GetModel();

    /// <summary>Gets the type of this <see cref="Pickup" />.</summary>
    public virtual PickupType SpawnType => (PickupType)_pickup.GetPickupType();
    
    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _pickups.AsPool().Release(Id);
        }
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Model: {Model})";
    }
    
    /// <summary>Performs an implicit conversion from <see cref="Pickup" /> to <see cref="IPickup" />.</summary>
    public static implicit operator IPickup(Pickup pickup)
    {
        return pickup._pickup;
    }
}