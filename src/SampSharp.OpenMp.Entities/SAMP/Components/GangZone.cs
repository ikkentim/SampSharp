using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a global gang zone visible to any subset of players via
/// <see cref="BaseGangZone.Show()" /> / <see cref="BaseGangZone.Show(Player)" />.
/// For zones that logically belong to a single player, use <see cref="PlayerGangZone" />.
/// </summary>
public class GangZone : BaseGangZone
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GangZone" /> class.
    /// </summary>
    protected GangZone(IOmpEntityProvider entityProvider, IGangZonesComponent gangZones, IGangZone gangZone)
        : base(entityProvider, gangZones, gangZone)
    {
    }
}
