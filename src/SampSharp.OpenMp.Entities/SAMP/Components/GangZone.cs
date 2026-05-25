using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a global gang zone visible to any subset of players via
/// <see cref="Show()" /> / <see cref="Show(Player)" />.
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
        Resource = gangZone;
    }

    private IGangZone Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(GangZone));
            return field;
        }
    }

    /// <summary>
    /// Shows this gang zone to all players.
    /// </summary>
    public virtual void Show()
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            Show(player);
        }
    }

    /// <summary>
    /// Shows this gang zone to the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to show this gang zone to.</param>
    /// <param name="color">The per-player gang zone color.</param>
    public virtual void Show(Player player, Color color)
    {
        ArgumentNullException.ThrowIfNull(player);

        Colour clr = color;
        Resource.ShowForPlayer(player, ref clr);
    }

    /// <summary>
    /// Shows this gang zone to the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to show this gang zone to.</param>
    public virtual void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        Colour clr = Color;
        Resource.ShowForPlayer(player, ref clr);
    }

    /// <summary>
    /// Hides this gang zone for all players.
    /// </summary>
    public virtual void Hide()
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            Hide(player);
        }
    }

    /// <summary>
    /// Hides this gang zone for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to hide this gang zone from.</param>
    public virtual void Hide(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        Resource.HideForPlayer(player);
    }

    /// <summary>
    /// Flashes this gang zone to all players.
    /// </summary>
    /// <param name="color">The <see cref="Color" /> to flash.</param>
    public virtual void Flash(Color color)
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            Flash(player, color);
        }
    }

    /// <summary>
    /// Flashes this gang zone for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to flash this gang zone to.</param>
    /// <param name="color">The <see cref="Color" /> to flash.</param>
    public virtual void Flash(Player player, Color color)
    {
        ArgumentNullException.ThrowIfNull(player);

        Colour clr = color;
        Resource.FlashForPlayer(player, ref clr);
    }

    /// <summary>
    /// Stops this gang zone from flashing for all players.
    /// </summary>
    public virtual void StopFlash()
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            StopFlash(player);
        }
    }

    /// <summary>
    /// Stops this gang zone from flashing for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to stop the gang zone flash for.</param>
    public virtual void StopFlash(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        Resource.StopFlashForPlayer(player);
    }

    /// <summary>
    /// Checks whether this gang zone is currently shown for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns><see langword="true" /> if shown; otherwise <see langword="false" />.</returns>
    public virtual bool IsShownForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return Resource.IsShownForPlayer(player);
    }

    /// <summary>
    /// Checks whether this gang zone is currently flashing for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns><see langword="true" /> if flashing; otherwise <see langword="false" />.</returns>
    public virtual bool IsFlashingForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return Resource.IsFlashingForPlayer(player);
    }

    /// <summary>
    /// Gets the color with which this gang zone is shown for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>The per-player gang zone color.</returns>
    public virtual Color GetColorForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return Resource.GetColourForPlayer(player);
    }

    /// <summary>
    /// Gets the flashing color for this gang zone as seen by the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>The per-player flashing color.</returns>
    public virtual Color GetFlashingColorForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return Resource.GetFlashingColourForPlayer(player);
    }

    /// <summary>
    /// Checks whether the specified <paramref name="player" /> is inside this gang zone.
    /// </summary>
    /// <remarks>
    /// Requires that this gang zone has been registered for enter/leave checking via
    /// <see cref="IWorldService.UseGangZoneCheck" />, otherwise the result is always <see langword="false" />.
    /// </remarks>
    /// <param name="player">The player.</param>
    /// <returns><see langword="true" /> if the player is inside; otherwise <see langword="false" />.</returns>
    public virtual bool IsPlayerInside(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return Resource.IsPlayerInside(player);
    }
}
