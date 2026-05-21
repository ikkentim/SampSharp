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
    private readonly IGangZone _gangZone;
    private readonly IGangZonesComponent _gangZones;

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
    public static implicit operator IGangZone(BaseGangZone? gangZone)
    {
        return gangZone?._gangZone ?? default;
    }
}