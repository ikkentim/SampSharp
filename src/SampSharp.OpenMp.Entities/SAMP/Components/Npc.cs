using System.Numerics;
using SampSharp.OpenMp.Core.Api;
using INPC = SampSharp.OpenMp.Core.Api.INPC;
using INPCComponent = SampSharp.OpenMp.Core.Api.INPCComponent;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// ECS-side wrapper around an open.mp <see cref="INPC" /> (a server-controlled bot
/// built on top of an <see cref="IPlayer" />).
/// </summary>
/// <remarks>
/// Unlike <see cref="WorldEntity" />-based components, <see cref="Npc" /> does NOT
/// derive from <see cref="IEntity" /> — open.mp's <see cref="INPC" /> only implements
/// <see cref="IIDProvider" />, and its position/rotation setters take an extra
/// "immediate update" boolean. Position / Rotation / VirtualWorld are exposed
/// directly here.
/// </remarks>
public class Npc : IdProvider
{
    private readonly INPC _npc;
    private readonly INPCComponent _npcs;

    /// <summary>
    /// Initializes a new instance of the <see cref="Npc" /> class.
    /// </summary>
    protected Npc(INPCComponent npcs, INPC npc) : base((IIDProvider)npc)
    {
        _npcs = npcs;
        _npc = npc;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _npc.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets the underlying player handle that this NPC drives.
    /// </summary>
    public virtual IPlayer Player => _npc.GetPlayer();

    /// <summary>
    /// Gets or sets the NPC's position in the world. Use <see cref="SetPosition" /> for the immediate-update overload.
    /// </summary>
    public virtual Vector3 Position
    {
        get => _npc.GetPosition();
        set => _npc.SetPosition(value, false);
    }

    /// <summary>
    /// Gets or sets the NPC's rotation as a quaternion.
    /// </summary>
    public virtual GTAQuat Rotation
    {
        get => _npc.GetRotation();
        set => _npc.SetRotation(value, false);
    }

    /// <summary>
    /// Gets or sets the virtual world this NPC is in.
    /// </summary>
    public virtual int VirtualWorld
    {
        get => _npc.GetVirtualWorld();
        set => _npc.SetVirtualWorld(value);
    }

    /// <summary>
    /// Sets the NPC's skin model ID.
    /// </summary>
    public virtual int Skin
    {
        set => _npc.SetSkin(value);
    }

    /// <summary>
    /// Gets or sets the current weapon ID.
    /// </summary>
    public virtual byte Weapon
    {
        get => _npc.GetWeapon();
        set => _npc.SetWeapon(value);
    }

    /// <summary>
    /// Gets or sets the ammunition for the current weapon.
    /// </summary>
    public virtual int Ammo
    {
        get => _npc.GetAmmo();
        set => _npc.SetAmmo(value);
    }

    /// <summary>
    /// Gets or sets the health of this NPC.
    /// </summary>
    public virtual float Health
    {
        get => _npc.GetHealth();
        set => _npc.SetHealth(value);
    }

    /// <summary>
    /// Gets or sets the armor of this NPC.
    /// </summary>
    public virtual float Armour
    {
        get => _npc.GetArmour();
        set => _npc.SetArmour(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this NPC is invulnerable.
    /// </summary>
    public virtual bool IsInvulnerable
    {
        get => _npc.IsInvulnerable();
        set => _npc.SetInvulnerable(value);
    }

    /// <summary>
    /// Gets or sets the interior ID this NPC is in (server-side only).
    /// </summary>
    public virtual int Interior
    {
        get => (int)_npc.GetInterior();
        set => _npc.SetInterior((uint)value);
    }

    /// <summary>
    /// Gets a value indicating whether this NPC has been killed and not yet respawned.
    /// </summary>
    public virtual bool IsDead => _npc.IsDead();

    /// <summary>
    /// Gets a value indicating whether this NPC is currently executing any movement command.
    /// </summary>
    public virtual bool IsMoving => _npc.IsMoving();

    /// <summary>
    /// Gets the velocity of this NPC.
    /// </summary>
    public virtual Vector3 Velocity => _npc.GetVelocity();

    /// <summary>
    /// Sets the position of this NPC.
    /// </summary>
    /// <param name="position">The position to set.</param>
    /// <param name="immediateUpdate">A value indicating whether to broadcast a sync to streamed-in players immediately instead of waiting for the next tick.</param>
    public virtual void SetPosition(Vector3 position, bool immediateUpdate)
    {
        _npc.SetPosition(position, immediateUpdate);
    }

    /// <summary>
    /// Sets the rotation of this NPC. See <see cref="SetPosition" /> for the immediate-update flag.
    /// </summary>
    /// <param name="rotation">The rotation to set.</param>
    /// <param name="immediateUpdate">A value indicating whether to broadcast a sync to streamed-in players immediately instead of waiting for the next tick.</param>
    public virtual void SetRotation(GTAQuat rotation, bool immediateUpdate)
    {
        _npc.SetRotation(rotation, immediateUpdate);
    }

    /// <summary>
    /// Spawns this NPC at its currently configured position and rotation.
    /// </summary>
    public virtual void Spawn()
    {
        _npc.Spawn();
    }

    /// <summary>
    /// Respawns this NPC, keeping its current state.
    /// </summary>
    public virtual void Respawn()
    {
        _npc.Respawn();
    }

    /// <summary>
    /// Instructs this NPC to move to the specified position.
    /// </summary>
    /// <param name="position">The position to move to.</param>
    /// <param name="moveType">The movement type (walk, jog, sprint, or drive).</param>
    /// <param name="moveSpeed">The speed to move at (-1 for default speed).</param>
    /// <param name="stopRange">The distance within which the NPC stops moving.</param>
    /// <returns><see langword="true" /> if the movement command was successful; <see langword="false" /> otherwise.</returns>
    public virtual bool MoveTo(Vector3 position, NPCMoveType moveType, float moveSpeed = -1f, float stopRange = 1.0f)
    {
        return _npc.Move(position, moveType, moveSpeed, stopRange);
    }

    /// <summary>
    /// Stops any active movement command for this NPC.
    /// </summary>
    public virtual void StopMoving()
    {
        _npc.StopMove();
    }

    /// <summary>
    /// Applies an animation to this NPC.
    /// </summary>
    /// <param name="library">The animation library.</param>
    /// <param name="name">The animation name.</param>
    /// <param name="fDelta">The speed to play the animation.</param>
    /// <param name="loop">A value indicating whether the animation should loop.</param>
    /// <param name="lockX">A value indicating whether to lock the NPC's x-coordinate during the animation.</param>
    /// <param name="lockY">A value indicating whether to lock the NPC's y-coordinate during the animation.</param>
    /// <param name="freeze">A value indicating whether to freeze the NPC at the end of the animation.</param>
    /// <param name="time">The duration for which to play the animation.</param>
    public virtual void ApplyAnimation(string library, string name, float fDelta, bool loop, bool lockX, bool lockY,
        bool freeze, TimeSpan time)
    {
        ArgumentNullException.ThrowIfNull(library);
        ArgumentNullException.ThrowIfNull(name);
        _npc.ApplyAnimation(new AnimationData(fDelta, loop, lockX, lockY, freeze,
            (uint)time.TotalMilliseconds, library, name));
    }

    /// <summary>
    /// Clears all animations applied to this NPC.
    /// </summary>
    public virtual void ClearAnimations()
    {
        _npc.ClearAnimations();
    }

    /// <summary>
    /// Sets the velocity of this NPC.
    /// </summary>
    /// <param name="velocity">The velocity to set.</param>
    /// <param name="update">A value indicating whether to update immediately.</param>
    public virtual void SetVelocity(Vector3 velocity, bool update = false)
    {
        _npc.SetVelocity(velocity, update);
    }

    /// <summary>
    /// Determines whether this NPC is streamed in for the specified player.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns><see langword="true" /> if this NPC is streamed in for the player; <see langword="false" /> otherwise.</returns>
    public virtual bool IsStreamedIn(Player player)
    {
        return player != null && _npc.IsStreamedInForPlayer(player);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed && _npcs.HasValue)
        {
            _npcs.Destroy(_npc);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Npc" /> to <see cref="INPC" />.
    /// </summary>
    public static implicit operator INPC(Npc npc)
    {
        return npc._npc;
    }
}