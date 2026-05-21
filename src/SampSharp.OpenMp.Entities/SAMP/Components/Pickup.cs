using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a global pickup. For pickups that logically belong to a single player, use
/// <see cref="PlayerPickup" />.
/// </summary>
public class Pickup : BasePickup
{
    private readonly IPickup _pickup;

    /// <summary>
    /// Initializes a new instance of the <see cref="Pickup" /> class.
    /// </summary>
    protected Pickup(IPickupsComponent pickups, IPickup pickup) : base(pickups, pickup)
    {
        _pickup = pickup;
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
}
