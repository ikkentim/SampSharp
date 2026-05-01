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
    /// Constructs an instance of Npc, should be used internally.
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
    /// The underlying <see cref="IPlayer" /> handle this NPC drives.
    /// </summary>
    public virtual IPlayer Player => _npc.GetPlayer();

    /// <summary>
    /// Gets or sets the NPC's world position. Use <see cref="SetPosition" /> for the immediate-update overload.
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
    /// Sets the NPC's skin model id.
    /// </summary>
    public virtual int Skin
    {
        set => _npc.SetSkin(value);
    }

    /// <summary>
    /// Gets or sets the current weapon id.
    /// </summary>
    public virtual byte Weapon
    {
        get => _npc.GetWeapon();
        set => _npc.SetWeapon(value);
    }

    /// <summary>
    /// Gets or sets ammo for the current weapon.
    /// </summary>
    public virtual int Ammo
    {
        get => _npc.GetAmmo();
        set => _npc.SetAmmo(value);
    }

    /// <summary>
    /// Gets or sets the NPC's health.
    /// </summary>
    public virtual float Health
    {
        get => _npc.GetHealth();
        set => _npc.SetHealth(value);
    }

    /// <summary>
    /// Gets or sets the NPC's armour.
    /// </summary>
    public virtual float Armour
    {
        get => _npc.GetArmour();
        set => _npc.SetArmour(value);
    }

    /// <summary>
    /// Gets or sets the NPC's invulnerability flag.
    /// </summary>
    public virtual bool IsInvulnerable
    {
        get => _npc.IsInvulnerable();
        set => _npc.SetInvulnerable(value);
    }

    /// <summary>
    /// Gets or sets the interior id this NPC is bookkept under (server-side only).
    /// </summary>
    public virtual int Interior
    {
        get => (int)_npc.GetInterior();
        set => _npc.SetInterior((uint)value);
    }

    /// <summary><see langword="true" /> if the NPC has been killed and not yet respawned.</summary>
    public virtual bool IsDead => _npc.IsDead();

    /// <summary><see langword="true" /> if the NPC is currently following any movement command.</summary>
    public virtual bool IsMoving => _npc.IsMoving();

    /// <summary>
    /// Gets the NPC's current velocity.
    /// </summary>
    public virtual Vector3 Velocity => _npc.GetVelocity();

    /// <summary>
    /// Sets the NPC's position. <paramref name="immediateUpdate" />=true broadcasts a sync
    /// to streamed-in players right away instead of waiting for the next tick.
    /// </summary>
    public virtual void SetPosition(Vector3 position, bool immediateUpdate)
    {
        _npc.SetPosition(position, immediateUpdate);
    }

    /// <summary>
    /// Sets the NPC's rotation. See <see cref="SetPosition" /> for the immediate-update flag.
    /// </summary>
    public virtual void SetRotation(GTAQuat rotation, bool immediateUpdate)
    {
        _npc.SetRotation(rotation, immediateUpdate);
    }

    /// <summary>
    /// Spawns the NPC at its currently configured position/rotation.
    /// </summary>
    public virtual void Spawn()
    {
        _npc.Spawn();
    }

    /// <summary>
    /// Respawns the NPC keeping its current state.
    /// </summary>
    public virtual void Respawn()
    {
        _npc.Respawn();
    }

    /// <summary>
    /// Tells the NPC to walk/jog/sprint/drive to the target position.
    /// </summary>
    public virtual bool MoveTo(Vector3 position, NPCMoveType moveType, float moveSpeed = -1f, float stopRange = 1.0f)
    {
        return _npc.Move(position, moveType, moveSpeed, stopRange);
    }

    /// <summary>
    /// Stops any active movement.
    /// </summary>
    public virtual void StopMoving()
    {
        _npc.StopMove();
    }

    /// <summary>
    /// Applies an animation to this NPC.
    /// </summary>
    public virtual void ApplyAnimation(string library, string name, float fDelta, bool loop, bool lockX, bool lockY,
        bool freeze, TimeSpan time)
    {
        ArgumentNullException.ThrowIfNull(library);
        ArgumentNullException.ThrowIfNull(name);
        _npc.ApplyAnimation(new AnimationData(fDelta, loop, lockX, lockY, freeze,
            (uint)time.TotalMilliseconds, library, name));
    }

    /// <summary>
    /// Clears any applied animation.
    /// </summary>
    public virtual void ClearAnimations()
    {
        _npc.ClearAnimations();
    }

    /// <summary>
    /// Sets the NPC's velocity.
    /// </summary>
    public virtual void SetVelocity(Vector3 velocity, bool update = false)
    {
        _npc.SetVelocity(velocity, update);
    }

    /// <summary><see langword="true" /> if this NPC is streamed in for the given player.</summary>
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