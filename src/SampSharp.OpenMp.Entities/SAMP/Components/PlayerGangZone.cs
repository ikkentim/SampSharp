using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a gang zone that is logically owned by a single player. Created via
/// <see cref="IWorldService.CreatePlayerGangZone" />, which binds the gang zone's legacy ID
/// to the owning player.
/// </summary>
/// <remarks>
/// open.mp does not have a dedicated per-player gang zone creation API; under the
/// hood this is a regular gang zone with <c>SetLegacyPlayer</c> set to the owner.
/// </remarks>
public class PlayerGangZone : BaseGangZone
{
    private readonly Player _player;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerGangZone" /> class.
    /// </summary>
    protected PlayerGangZone(IOmpEntityProvider entityProvider, IGangZonesComponent gangZones, IGangZone gangZone, Player player)
        : base(entityProvider, gangZones, gangZone)
    {
        Resource = gangZone;
        _player = player;
    }

    private IGangZone Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(PlayerGangZone));
            return field;
        }
    }

    /// <summary>
    /// Shows this gang zone to the player.
    /// </summary>
    public virtual void Show()
    {
        Colour clr = Color;
        Resource.ShowForPlayer(_player, ref clr);
    }


    /// <summary>
    /// Hides this gang zone for the player.
    /// </summary>
    public virtual void Hide()
    {
        Resource.HideForPlayer(_player);
    }

    /// <summary>
    /// Flashes this gang zone for the player.
    /// </summary>
    /// <param name="color">The <see cref="Color" /> to flash.</param>
    public virtual void Flash(Color color)
    {
        Colour clr = color;
        Resource.FlashForPlayer(_player, ref clr);
    }

    /// <summary>
    /// Stops this gang zone from flashing for the player.
    /// </summary>
    public virtual void StopFlash()
    {
        Resource.StopFlashForPlayer(_player);
    }

    /// <summary>
    /// Checks whether this gang zone is currently shown for the player.
    /// </summary>
    /// <returns><see langword="true" /> if shown; otherwise <see langword="false" />.</returns>
    public virtual bool IsShown()
    {
        return Resource.IsShownForPlayer(_player);
    }

    /// <summary>
    /// Checks whether this gang zone is currently flashing for the player.
    /// </summary>
    /// <returns><see langword="true" /> if flashing; otherwise <see langword="false" />.</returns>
    public virtual bool IsFlashing()
    {
        return Resource.IsFlashingForPlayer(_player);
    }

    /// <summary>
    /// Gets the flashing color for this gang zone as seen by the player.
    /// </summary>
    /// <returns>The flashing color.</returns>
    public virtual Color GetFlashingColor()
    {
        return Resource.GetFlashingColourForPlayer(_player);
    }

    /// <summary>
    /// Checks whether the player is inside this gang zone.
    /// </summary>
    /// <remarks>
    /// Requires that this gang zone has been registered for enter/leave checking via
    /// <see cref="IWorldService.UseGangZoneCheck" />, otherwise the result is always <see langword="false" />.
    /// </remarks>
    /// <returns><see langword="true" /> if the player is inside; otherwise <see langword="false" />.</returns>
    public virtual bool IsPlayerInside()
    {
        return Resource.IsPlayerInside(_player);
    }

}
