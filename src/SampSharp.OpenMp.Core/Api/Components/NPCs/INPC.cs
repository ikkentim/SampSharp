using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="INPC" /> interface
/// (a server-controlled bot built on top of an <see cref="IPlayer" />).
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IIDProvider))]
public readonly partial struct INPC
{
    /// <summary>Gets the underlying <see cref="IPlayer" /> instance for this NPC.</summary>
    public partial IPlayer GetPlayer();

    /// <summary>Gets the NPC's world position.</summary>
    public partial Vector3 GetPosition();

    /// <summary>
    /// Sets the NPC's position. <paramref name="immediateUpdate" />=true broadcasts a sync
    /// to streamed-in players right away instead of waiting for the next tick.
    /// </summary>
    public partial void SetPosition(Vector3 position, bool immediateUpdate);

    /// <summary>Gets the NPC's rotation as a quaternion.</summary>
    public partial GTAQuat GetRotation();

    /// <summary>Sets the NPC's rotation. See <see cref="SetPosition" /> for the immediate-update flag.</summary>
    public partial void SetRotation(GTAQuat rotation, bool immediateUpdate);

    /// <summary>Gets the virtual world this NPC is in.</summary>
    public partial int GetVirtualWorld();

    /// <summary>Sets the virtual world this NPC is in.</summary>
    public partial void SetVirtualWorld(int vw);

    /// <summary>Gets the interior id this NPC is recorded in (server-side bookkeeping only).</summary>
    public partial uint GetInterior();

    /// <summary>Sets the interior id (server-side bookkeeping only — does not relocate the NPC).</summary>
    public partial void SetInterior(uint interior);

    /// <summary>Gets the current velocity vector.</summary>
    public partial Vector3 GetVelocity();

    /// <summary>Sets the velocity vector. <paramref name="update" />=true forces a sync this tick.</summary>
    public partial void SetVelocity(Vector3 velocity, bool update);

    /// <summary>Spawns the NPC at its currently configured position/rotation.</summary>
    public partial void Spawn();

    /// <summary>Respawns the NPC keeping its current state.</summary>
    public partial void Respawn();

    /// <summary>True if the NPC has been killed and not yet respawned.</summary>
    public partial bool IsDead();

    /// <summary>Sets the NPC's skin model id.</summary>
    public partial void SetSkin(int model);

    /// <summary>Sets the current weapon id.</summary>
    public partial void SetWeapon(byte weapon);

    /// <summary>Gets the current weapon id.</summary>
    public partial byte GetWeapon();

    /// <summary>Sets ammo for the current weapon.</summary>
    public partial void SetAmmo(int ammo);

    /// <summary>Gets ammo for the current weapon.</summary>
    public partial int GetAmmo();

    /// <summary>Gets the NPC's health.</summary>
    public partial float GetHealth();

    /// <summary>Sets the NPC's health.</summary>
    public partial void SetHealth(float health);

    /// <summary>Gets the NPC's armour.</summary>
    public partial float GetArmour();

    /// <summary>Sets the NPC's armour.</summary>
    public partial void SetArmour(float armour);

    /// <summary>True iff the NPC is invulnerable.</summary>
    public partial bool IsInvulnerable();

    /// <summary>Sets invulnerability.</summary>
    public partial void SetInvulnerable(bool toggle);

    /// <summary>True if the NPC is currently moving (path or direct).</summary>
    public partial bool IsMoving();

    /// <summary>Tells the NPC to walk/jog/sprint/drive to <paramref name="position" />.</summary>
    public partial bool Move(Vector3 position, NPCMoveType moveType, float moveSpeed, float stopRange);

    /// <summary>Stops any active movement.</summary>
    public partial void StopMove();

    /// <summary>Clears any applied animation.</summary>
    public partial void ClearAnimations();

    /// <summary>Applies an animation. <paramref name="animationData" /> bundles lib + name + flags + duration.</summary>
    public partial void ApplyAnimation(AnimationData animationData);

    /// <summary>Checks whether this NPC is streamed in for the given player.</summary>
    public partial bool IsStreamedInForPlayer(IPlayer other);
}