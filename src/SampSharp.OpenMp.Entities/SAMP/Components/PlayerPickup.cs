using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a pickup that is logically owned by a single player. Created via
/// <see cref="IWorldService.CreatePlayerPickup" />, which binds the pickup's legacy ID
/// to the owning player.
/// </summary>
/// <remarks>
/// open.mp does not have a dedicated per-player pickup creation API; under the hood this is
/// a regular pickup with <c>SetLegacyPlayer</c> set to the owner.
/// </remarks>
public class PlayerPickup : BasePickup
{
    private readonly IPickup _pickup;
    private readonly Player _player;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickup" /> class.
    /// </summary>
    protected PlayerPickup(IPickupsComponent pickups, IPickup pickup, Player player) : base(pickups, pickup)
    {
        _pickup = pickup;
        _player = player;
    }

    /// <summary>Checks whether this pickup is streamed in for the player.</summary>
    /// <returns><see langword="true" /> if streamed in; otherwise <see langword="false" />.</returns>
    public virtual bool IsStreamedIn()
    {
        return _pickup.IsStreamedInForPlayer(_player);
    }

    /// <summary>Streams this pickup in for the player.</summary>
    public virtual void StreamIn()
    {
        _pickup.StreamInForPlayer(_player);
    }

    /// <summary>Streams this pickup out for the player.</summary>
    public virtual void StreamOut()
    {
        _pickup.StreamOutForPlayer(_player);
    }
}
