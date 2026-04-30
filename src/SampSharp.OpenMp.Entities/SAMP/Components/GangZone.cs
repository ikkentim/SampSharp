using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a component which provides the data and functionality of a gang zone.</summary>
public class GangZone : IdProvider
{
    private readonly IGangZonesComponent _gangZones;
    private readonly IGangZone _gangZone;

    /// <summary>Constructs an instance of GangZone, should be used internally.</summary>
    protected GangZone(IGangZonesComponent gangZones, IGangZone gangZone) : base((IIDProvider)gangZone)
    {
        _gangZone = gangZone;
        _gangZones = gangZones;
        
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _gangZone.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>Gets the minimum position of this <see cref="GangZone" />.</summary>
    public virtual Vector2 Min => _gangZone.GetPosition().Min;

    /// <summary>Gets the maximum position of this <see cref="GangZone" />.</summary>
    public virtual Vector2 Max => _gangZone.GetPosition().Max;

    /// <summary>Gets the minimum x value for this <see cref="GangZone" />.</summary>
    public virtual float MinX => Min.X;

    /// <summary>Gets the minimum y value for this <see cref="GangZone" />.</summary>
    public virtual float MinY => Min.Y;

    /// <summary>Gets the maximum x value for this <see cref="GangZone" />.</summary>
    public virtual float MaxX => Max.X;

    /// <summary>Gets the maximum y value for this <see cref="GangZone" />.</summary>
    public virtual float MaxY => Max.Y;

    /// <summary>Gets or sets the color of this <see cref="GangZone" />.</summary>
    public virtual Color Color { get; set; }

    /// <summary>Shows this <see cref="GangZone" />.</summary>
    public virtual void Show()
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            Show(player);
        }
    }

    /// <summary>Shows this <see cref="GangZone" /> to the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    public virtual void Show(Player player)
    {
        Colour clr = Color;
        _gangZone.ShowForPlayer(player, ref clr);
    }

    /// <summary>Hides this <see cref="GangZone" />.</summary>
    public virtual void Hide()
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            Hide(player);
        }
    }

    /// <summary>Hides this <see cref="GangZone" /> for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    public virtual void Hide(Player player)
    {
        _gangZone.HideForPlayer(player);
    }

    /// <summary>Flashes this <see cref="GangZone" />.</summary>
    /// <param name="color">The color.</param>
    public virtual void Flash(Color color)
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            Flash(player, color);
        }
    }
    
    /// <summary>Flashes this <see cref="GangZone" /> for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    /// <param name="color">The color.</param>
    public virtual void Flash(Player player, Color color)
    {
        Colour clr = color;
        _gangZone.FlashForPlayer(player, ref clr);
    }

    /// <summary>Stops this <see cref="GangZone" /> from flash.</summary>
    public virtual void StopFlash()
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            StopFlash(player);
        }
    }

    /// <summary>Stops this <see cref="GangZone" /> from flash for the specified player.</summary>
    /// <param name="player">The player.</param>
    public virtual void StopFlash(Player player)
    {
        _gangZone.StopFlashForPlayer(player);
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
    
    /// <summary>Performs an implicit conversion from <see cref="GangZone" /> to <see cref="IGangZone" />.</summary>
    public static implicit operator IGangZone(GangZone gangZone)
    {
        return gangZone._gangZone;
    }
}