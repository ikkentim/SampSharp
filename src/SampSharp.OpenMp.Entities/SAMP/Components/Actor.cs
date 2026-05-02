using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of an actor.
/// </summary>
public class Actor : WorldEntity
{
    private readonly IActor _actor;
    private readonly IActorsComponent _actors;

    /// <summary>
    /// Initializes a new instance of the <see cref="Actor" /> class.
    /// </summary>
    protected Actor(IActorsComponent actors, IActor actor) : base((IEntity)actor)
    {
        _actors = actors;
        _actor = actor;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _actor.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets or sets the facing angle of this actor.
    /// </summary>
    public virtual float Angle
    {
        get => float.RadiansToDegrees(MathHelper.GetZAngleFromRotationMatrix(Matrix4x4.CreateFromQuaternion(_actor.GetRotation())));
        set => Rotation = Quaternion.CreateFromAxisAngle(GtaVector.Up, float.DegreesToRadians(value));
    }

    /// <summary>
    /// Gets or sets the skin of this actor.
    /// </summary>
    public virtual int Skin
    {
        get => _actor.GetSkin();
        set => _actor.SetSkin(value);
    }

    /// <summary>
    /// Gets or sets the health of this actor.
    /// </summary>
    public virtual float Health
    {
        get => _actor.GetHealth();
        set => _actor.SetHealth(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this actor is invulnerable.
    /// </summary>
    public virtual bool IsInvulnerable
    {
        get => _actor.IsInvulnerable();
        set => _actor.SetInvulnerable(value);
    }

    /// <summary>
    /// Determines whether this actor is streamed in for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to check.</param>
    /// <returns><see langword="true" /> if the actor is streamed in for the player; <see langword="false" /> otherwise.</returns>
    public virtual bool IsStreamedIn(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        
        return _actor.IsStreamedInForPlayer(player);
    }

    /// <summary>
    /// Applies the specified animation to this actor.
    /// </summary>
    /// <param name="library">The animation library from which to apply an animation.</param>
    /// <param name="name">The name of the animation to apply within the specified <paramref name="library" />.</param>
    /// <param name="fDelta">The speed at which to play the animation.</param>
    /// <param name="loop">A value indicating whether the animation should loop.</param>
    /// <param name="lockX">A value indicating whether to allow this actor to move along its x-coordinate.</param>
    /// <param name="lockY">A value indicating whether to allow this actor to move along its y-coordinate.</param>
    /// <param name="freeze">A value indicating whether to freeze this actor at the end of the animation.</param>
    /// <param name="time">The duration for which to play the animation.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="library" /> or <paramref name="name" /> is <see langword="null" />.</exception>
    public virtual void ApplyAnimation(string library, string name, float fDelta, bool loop, bool lockX, bool lockY, bool freeze, TimeSpan time)
    {
        ArgumentNullException.ThrowIfNull(library);
        ArgumentNullException.ThrowIfNull(name);
        _actor.ApplyAnimation(new AnimationData(fDelta, loop, lockX, lockY, freeze, (uint)time.TotalMilliseconds, library, name));
    }

    /// <inheritdoc cref="ApplyAnimation(string, string, float, bool, bool, bool, bool, TimeSpan)" />
    [Obsolete("Use the TimeSpan overload. This int-milliseconds variant is kept for source compatibility and will be removed.")]
    public virtual void ApplyAnimation(string library, string name, float fDelta, bool loop, bool lockX, bool lockY, bool freeze, int time)
        => ApplyAnimation(library, name, fDelta, loop, lockX, lockY, freeze, TimeSpan.FromMilliseconds(time));

    /// <summary>
    /// Clears all animations applied to this actor.
    /// </summary>
    public virtual void ClearAnimations()
    {
        _actor.ClearAnimations();
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _actors.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Actor" /> to <see cref="IActor" />.
    /// </summary>
    public static implicit operator IActor(Actor actor)
    {
        return actor._actor;
    }
}