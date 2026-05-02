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
/// controlled through <see cref="BasePickup.SetHiddenForPlayer" /> /
/// <see cref="BasePickup.IsHiddenForPlayer" />.
/// </remarks>
public class PlayerPickup : BasePickup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickup" /> class.
    /// </summary>
    protected PlayerPickup(IPickupsComponent pickups, IPickup pickup) : base(pickups, pickup)
    {
    }
}
