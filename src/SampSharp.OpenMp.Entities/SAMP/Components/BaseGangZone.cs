using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Provides the shared data and functionality of a gang zone, regardless of whether it is global
/// (<see cref="GangZone" />) or scoped to a single player (<see cref="PlayerGangZone" />).
/// </summary>
public abstract class BaseGangZone : IdProvider
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IGangZonesComponent _gangZones;
    private readonly IGangZone _gangZone;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseGangZone" /> class.
    /// </summary>
    protected BaseGangZone(IOmpEntityProvider entityProvider, IGangZonesComponent gangZones, IGangZone gangZone) : base((IIDProvider)gangZone)
    {
        _entityProvider = entityProvider;
        _gangZone = gangZone;
        _gangZones = gangZones;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _gangZone.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets the minimum position of this gang zone as a <see cref="Vector2" />.
    /// </summary>
    public virtual Vector2 Min => _gangZone.GetPosition().Min;

    /// <summary>
    /// Gets the maximum position of this gang zone as a <see cref="Vector2" />.
    /// </summary>
    public virtual Vector2 Max => _gangZone.GetPosition().Max;

    /// <summary>
    /// Gets the minimum x coordinate of this gang zone.
    /// </summary>
    public virtual float MinX => Min.X;

    /// <summary>
    /// Gets the minimum y coordinate of this gang zone.
    /// </summary>
    public virtual float MinY => Min.Y;

    /// <summary>
    /// Gets the maximum x coordinate of this gang zone.
    /// </summary>
    public virtual float MaxX => Max.X;

    /// <summary>
    /// Gets the maximum y coordinate of this gang zone.
    /// </summary>
    public virtual float MaxY => Max.Y;

    /// <summary>
    /// Gets or sets the <see cref="Color" /> of this gang zone.
    /// </summary>
    public virtual Color Color { get; set; }

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
    public virtual void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        
        Colour clr = Color;
        _gangZone.ShowForPlayer(player, ref clr);
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
        
        _gangZone.HideForPlayer(player);
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
        _gangZone.FlashForPlayer(player, ref clr);
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
        
        _gangZone.StopFlashForPlayer(player);
    }

    /// <summary>
    /// Checks whether this gang zone is currently shown for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns><see langword="true" /> if shown; otherwise <see langword="false" />.</returns>
    public virtual bool IsShownForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return _gangZone.IsShownForPlayer(player);
    }

    /// <summary>
    /// Checks whether this gang zone is currently flashing for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns><see langword="true" /> if flashing; otherwise <see langword="false" />.</returns>
    public virtual bool IsFlashingForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return _gangZone.IsFlashingForPlayer(player);
    }

    /// <summary>
    /// Gets the color with which this gang zone is shown for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>The per-player gang zone color.</returns>
    public virtual Color GetColorForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return _gangZone.GetColourForPlayer(player);
    }

    /// <summary>
    /// Gets the flashing color for this gang zone as seen by the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>The per-player flashing color.</returns>
    public virtual Color GetFlashingColorForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return _gangZone.GetFlashingColourForPlayer(player);
    }

    /// <summary>
    /// Enumerates the players for whom this gang zone is currently shown.
    /// </summary>
    /// <returns>A lazy sequence of <see cref="Player" /> components.</returns>
    public virtual IEnumerable<Player> GetShownFor()
    {
        foreach (var raw in _gangZone.GetShownFor())
        {
            var component = _entityProvider.GetComponent(raw);
            if (component != null)
            {
                yield return component;
            }
        }
    }

    /// <summary>
    /// Updates the boundary of this gang zone.
    /// </summary>
    /// <param name="min">The minimum corner.</param>
    /// <param name="max">The maximum corner.</param>
    public virtual void SetPosition(Vector2 min, Vector2 max)
    {
        var pos = new GangZonePos(min, max);
        _gangZone.SetPosition(ref pos);
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
        return _gangZone.IsPlayerInside(player);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _gangZones.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Color: {Color})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="BaseGangZone" /> to <see cref="IGangZone" />.
    /// </summary>
    public static implicit operator IGangZone(BaseGangZone gangZone)
    {
        return gangZone._gangZone;
    }
}