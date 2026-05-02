using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a gang zone that is logically owned by a single player. Created via
/// <see cref="IWorldService.CreatePlayerGangZone" />, which binds the gang zone's legacy ID
/// to the owning player.
/// </summary>
/// <remarks>
/// open.mp does not have a dedicated per-player gang zone creation API; under the hood this is
/// a regular gang zone with <c>SetLegacyPlayer</c> set to the owner. Use <see cref="BaseGangZone.Show(Player)" />
/// / <see cref="BaseGangZone.Hide(Player)" /> for explicit per-player visibility.
/// </remarks>
public class PlayerGangZone : BaseGangZone
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerGangZone" /> class.
    /// </summary>
    protected PlayerGangZone(IOmpEntityProvider entityProvider, IGangZonesComponent gangZones, IGangZone gangZone)
        : base(entityProvider, gangZones, gangZone)
    {
    }
}
