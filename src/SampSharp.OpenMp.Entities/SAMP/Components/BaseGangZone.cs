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

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseGangZone" /> class.
    /// </summary>
    protected BaseGangZone(IOmpEntityProvider entityProvider, IGangZonesComponent gangZones, IGangZone gangZone) : base((IIDProvider)gangZone)
    {
        _entityProvider = entityProvider;
        Resource = gangZone;
        _gangZones = gangZones;
    }

    private IGangZone Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(BaseGangZone));
            return field;
        }
    }

    /// <summary>
    /// Gets the minimum position of this gang zone as a <see cref="Vector2" />.
    /// </summary>
    public virtual Vector2 Min => Resource.GetPosition().Min;

    /// <summary>
    /// Gets the maximum position of this gang zone as a <see cref="Vector2" />.
    /// </summary>
    public virtual Vector2 Max => Resource.GetPosition().Max;

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
        foreach (var raw in Resource.GetShownFor())
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
        Resource.SetPosition(ref pos);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!Resource.GetExtension<ComponentExtension>().IsOmpEntityDestroyed)
        {
            _gangZones.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (!IsComponentAlive)
        {
            return "(Destroyed)";
        }
        return $"(Id: {Id}, Color: {Color}, Min: {Min}, Max: {Max})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="BaseGangZone" /> to <see cref="IGangZone" />.
    /// </summary>
    public static implicit operator IGangZone(BaseGangZone? gangZone)
    {
        return gangZone?.Resource ?? default;
    }
}