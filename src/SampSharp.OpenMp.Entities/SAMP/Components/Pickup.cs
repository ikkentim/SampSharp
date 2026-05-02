using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a global pickup. For pickups that logically belong to a single player, use
/// <see cref="PlayerPickup" />.
/// </summary>
public class Pickup : BasePickup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pickup" /> class.
    /// </summary>
    protected Pickup(IPickupsComponent pickups, IPickup pickup) : base(pickups, pickup)
    {
    }
}
