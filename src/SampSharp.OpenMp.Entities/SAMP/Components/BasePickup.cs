using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Provides the shared data and functionality of a pickup, regardless of whether it is global
/// (<see cref="Pickup" />) or scoped to a single player (<see cref="PlayerPickup" />).
/// </summary>
public abstract class BasePickup : WorldEntity
{
    private readonly IPickup _pickup;
    private readonly IPickupsComponent _pickups;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePickup" /> class.
    /// </summary>
    protected BasePickup(IPickupsComponent pickups, IPickup pickup) : base((IEntity)pickup)
    {
        _pickups = pickups;
        _pickup = pickup;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _pickup.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets the model of this pickup.
    /// </summary>
    public virtual int Model => _pickup.GetModel();

    /// <summary>
    /// Gets the type of this pickup.
    /// </summary>
    public virtual PickupType SpawnType => (PickupType)_pickup.GetPickupType();

    /// <summary>Changes the type of this pickup.</summary>
    /// <param name="type">The new <see cref="PickupType" />.</param>
    /// <param name="update">Whether to update the pickup visually for streamed-in players.</param>
    public virtual void SetType(PickupType type, bool update = true)
    {
        _pickup.SetType((byte)type, update);
    }

    /// <summary>Changes the model of this pickup.</summary>
    /// <param name="model">The new model ID.</param>
    /// <param name="update">Whether to update the pickup visually for streamed-in players.</param>
    public virtual void SetModel(int model, bool update = true)
    {
        _pickup.SetModel(model, update);
    }

    /// <summary>Sets the position of this pickup without sending a visual update.</summary>
    /// <param name="position">The new position.</param>
    public virtual void SetPositionNoUpdate(Vector3 position)
    {
        _pickup.SetPositionNoUpdate(position);
    }

    /// <summary>Checks whether this pickup is streamed in for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    /// <returns><see langword="true" /> if streamed in; otherwise <see langword="false" />.</returns>
    public virtual bool IsStreamedInForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return _pickup.IsStreamedInForPlayer(player);
    }

    /// <summary>Streams this pickup in for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    public virtual void StreamInForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        _pickup.StreamInForPlayer(player);
    }

    /// <summary>Streams this pickup out for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    public virtual void StreamOutForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        _pickup.StreamOutForPlayer(player);
    }

    /// <summary>Hides or shows this pickup for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    /// <param name="hidden"><see langword="true" /> to hide; <see langword="false" /> to show.</param>
    public virtual void SetHiddenForPlayer(Player player, bool hidden)
    {
        ArgumentNullException.ThrowIfNull(player);
        _pickup.SetPickupHiddenForPlayer(player, hidden);
    }

    /// <summary>Checks whether this pickup is hidden for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    /// <returns><see langword="true" /> if hidden; otherwise <see langword="false" />.</returns>
    public virtual bool IsHiddenForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return _pickup.IsPickupHiddenForPlayer(player);
    }

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

    /// <summary>
    /// Performs an implicit conversion from <see cref="BasePickup" /> to <see cref="IPickup" />.
    /// </summary>
    public static implicit operator IPickup(BasePickup pickup)
    {
        return pickup._pickup;
    }
}