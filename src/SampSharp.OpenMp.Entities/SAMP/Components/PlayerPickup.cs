using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a pickup that is logically owned by a single player. Created via
/// <see cref="IWorldService.CreatePlayerPickup" />, which binds the pickup's legacy ID
/// to the owning player.
/// </summary>
/// <remarks>
/// open.mp does not have a dedicated per-player pickup creation API; under the hood this is
/// a regular pickup with <c>SetLegacyPlayer</c> set to the owner. Per-player visibility is
/// controlled through <see cref="Pickup.SetHiddenForPlayer" /> /
/// <see cref="Pickup.IsHiddenForPlayer" />.
/// </remarks>
public class PlayerPickup : Pickup
{
    /// <summary>Constructs an instance of PlayerPickup, should be used internally.</summary>
    protected PlayerPickup(IPickupsComponent pickups, IPickup pickup) : base(pickups, pickup)
    {
    }
}
