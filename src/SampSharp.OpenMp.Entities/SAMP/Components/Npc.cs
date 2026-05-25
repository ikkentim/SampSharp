using System.Numerics;
using SampSharp.OpenMp.Core.Api;
using SampSharp.OpenMp.Core.Std;
using SampSharp.OpenMp.Core.Std.Chrono;
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
    private const int DefaultPositionCheckUpdateDelayMs = 500;

    private readonly INPCComponent _npcs;

    /// <summary>
    /// Initializes a new instance of the <see cref="Npc" /> class.
    /// </summary>
    protected Npc(INPCComponent npcs, INPC npc) : base((IIDProvider)npc)
    {
        _npcs = npcs;
        Resource = npc;
    }

    private INPC Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(Npc));
            return field;
        }
    }

    /// <summary>
    /// Gets the underlying <see cref="IPlayer" /> handle that this NPC drives.
    /// </summary>
    public virtual IPlayer Player => Resource.GetPlayer();

    /// <summary>
    /// Gets or sets the NPC's position in the world as a <see cref="Vector3" />. Use <see cref="SetPosition" /> for the immediate-update overload.
    /// </summary>
    public virtual Vector3 Position
    {
        get => Resource.GetPosition();
        set => Resource.SetPosition(value, false);
    }

    /// <summary>
    /// Gets or sets the NPC's rotation as a quaternion.
    /// </summary>
    public virtual Quaternion Rotation
    {
        get => Resource.GetRotation();
        set => Resource.SetRotation(value, false);
    }

    /// <summary>
    /// Gets or sets the virtual world this NPC is in.
    /// </summary>
    public virtual int VirtualWorld
    {
        get => Resource.GetVirtualWorld();
        set => Resource.SetVirtualWorld(value);
    }

    /// <summary>
    /// Sets the NPC's skin model ID.
    /// </summary>
    public virtual int Skin
    {
        set => Resource.SetSkin(value);
    }

    /// <summary>
    /// Gets or sets the current weapon ID.
    /// </summary>
    public virtual byte Weapon
    {
        get => Resource.GetWeapon();
        set => Resource.SetWeapon(value);
    }

    /// <summary>
    /// Gets or sets the ammunition for the current weapon.
    /// </summary>
    public virtual int Ammo
    {
        get => Resource.GetAmmo();
        set => Resource.SetAmmo(value);
    }

    /// <summary>
    /// Gets or sets the health of this NPC.
    /// </summary>
    public virtual float Health
    {
        get => Resource.GetHealth();
        set => Resource.SetHealth(value);
    }

    /// <summary>
    /// Gets or sets the armor of this NPC.
    /// </summary>
    public virtual float Armour
    {
        get => Resource.GetArmour();
        set => Resource.SetArmour(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this NPC is invulnerable.
    /// </summary>
    public virtual bool IsInvulnerable
    {
        get => Resource.IsInvulnerable();
        set => Resource.SetInvulnerable(value);
    }

    /// <summary>
    /// Gets or sets the interior ID this NPC is in (server-side only).
    /// </summary>
    public virtual int Interior
    {
        get => (int)Resource.GetInterior();
        set => Resource.SetInterior((uint)value);
    }

    /// <summary>
    /// Gets a value indicating whether this NPC has been killed and not yet respawned.
    /// </summary>
    public virtual bool IsDead => Resource.IsDead();

    /// <summary>
    /// Gets a value indicating whether this NPC is currently executing any movement command.
    /// </summary>
    public virtual bool IsMoving => Resource.IsMoving();

    /// <summary>
    /// Gets the velocity of this NPC as a <see cref="Vector3" />.
    /// </summary>
    public virtual Vector3 Velocity => Resource.GetVelocity();

    /// <summary>
    /// Gets or sets the NPC's current weapon state.
    /// </summary>
    public virtual PlayerWeaponState WeaponState
    {
        get => Resource.GetWeaponState();
        set => Resource.SetWeaponState(value);
    }

    /// <summary>
    /// Gets or sets the ammo count in the current weapon's clip.
    /// </summary>
    public virtual int AmmoInClip
    {
        get => Resource.GetAmmoInClip();
        set => Resource.SetAmmoInClip(value);
    }

    /// <summary>
    /// Gets or sets the NPC's fighting style.
    /// </summary>
    public virtual PlayerFightingStyle FightingStyle
    {
        get => Resource.GetFightingStyle();
        set => Resource.SetFightingStyle(value);
    }

    /// <summary>
    /// Gets or sets the NPC's special action.
    /// </summary>
    public virtual PlayerSpecialAction SpecialAction
    {
        get => Resource.GetSpecialAction();
        set => Resource.SetSpecialAction(value);
    }

    /// <summary>
    /// Gets a value indicating whether this NPC is currently shooting.
    /// </summary>
    public virtual bool IsShooting => Resource.IsShooting();

    /// <summary>
    /// Gets a value indicating whether this NPC is currently aiming.
    /// </summary>
    public virtual bool IsAiming => Resource.IsAiming();

    /// <summary>
    /// Gets a value indicating whether this NPC is currently performing a melee attack.
    /// </summary>
    public virtual bool IsMeleeAttacking => Resource.IsMeleeAttacking();

    /// <summary>
    /// Gets a value indicating whether weapon reloading is enabled for this NPC.
    /// </summary>
    public virtual bool IsReloadEnabled => Resource.IsReloadEnabled();

    /// <summary>
    /// Gets a value indicating whether this NPC is currently reloading.
    /// </summary>
    public virtual bool IsReloading => Resource.IsReloading();

    /// <summary>
    /// Gets a value indicating whether infinite ammo is enabled for this NPC.
    /// </summary>
    public virtual bool IsInfiniteAmmoEnabled => Resource.IsInfiniteAmmoEnabled();

    /// <summary>
    /// Gets a value indicating whether this NPC is currently moving along a path.
    /// </summary>
    public virtual bool IsMovingByPath => Resource.IsMovingByPath();

    /// <summary>
    /// Gets a value indicating whether path-based movement is currently paused.
    /// </summary>
    public virtual bool IsPathPaused => Resource.IsPathPaused();

    /// <summary>
    /// Gets the ID of the path this NPC is currently following, or -1 if none.
    /// </summary>
    public virtual int CurrentPathId => Resource.GetCurrentPathId();

    /// <summary>
    /// Gets the index of the current waypoint within the active path.
    /// </summary>
    public virtual int CurrentPathPointIndex => Resource.GetCurrentPathPointIndex();

    /// <summary>
    /// Gets the vehicle this NPC is currently in, or a handle with no value if not in a vehicle.
    /// </summary>
    public virtual IVehicle Vehicle => Resource.GetVehicle();

    /// <summary>
    /// Gets the seat index this NPC occupies in the current vehicle.
    /// </summary>
    public virtual int VehicleSeat => Resource.GetVehicleSeat();

    /// <summary>
    /// Gets the vehicle the NPC is in the process of entering, or a handle with no value if not entering one.
    /// </summary>
    public virtual IVehicle EnteringVehicle => Resource.GetEnteringVehicle();

    /// <summary>
    /// Gets the seat index the NPC is targeting while entering a vehicle.
    /// </summary>
    public virtual int EnteringVehicleSeat => Resource.GetEnteringVehicleSeat();

    /// <summary>
    /// Gets or sets a value indicating whether the siren on this NPC's vehicle is active.
    /// </summary>
    public virtual bool IsVehicleSirenUsed
    {
        get => Resource.IsVehicleSirenUsed();
        set => Resource.UseVehicleSiren(value);
    }

    /// <summary>
    /// Gets or sets the health of this NPC's current vehicle.
    /// </summary>
    public virtual float VehicleHealth
    {
        get => Resource.GetVehicleHealth();
        set => Resource.SetVehicleHealth(value);
    }

    /// <summary>
    /// Gets or sets the hydra thruster direction for this NPC's vehicle.
    /// </summary>
    public virtual int VehicleHydraThrusters
    {
        get => Resource.GetVehicleHydraThrusters();
        set => Resource.SetVehicleHydraThrusters(value);
    }

    /// <summary>
    /// Gets or sets the gear state for this NPC's vehicle.
    /// </summary>
    public virtual int VehicleGearState
    {
        get => Resource.GetVehicleGearState();
        set => Resource.SetVehicleGearState(value);
    }

    /// <summary>
    /// Gets or sets the train speed for this NPC's vehicle.
    /// </summary>
    public virtual float VehicleTrainSpeed
    {
        get => Resource.GetVehicleTrainSpeed();
        set => Resource.SetVehicleTrainSpeed(value);
    }

    /// <summary>
    /// Gets a value indicating whether this NPC is currently playing a recording.
    /// </summary>
    public virtual bool IsPlayingPlayback => Resource.IsPlayingPlayback();

    /// <summary>
    /// Gets a value indicating whether playback is currently paused.
    /// </summary>
    public virtual bool IsPlaybackPaused => Resource.IsPlaybackPaused();

    /// <summary>
    /// Gets a value indicating whether this NPC is currently following a node path.
    /// </summary>
    public virtual bool IsPlayingNode => Resource.IsPlayingNode();

    /// <summary>
    /// Gets a value indicating whether node-based movement is currently paused.
    /// </summary>
    public virtual bool IsPlayingNodePaused => Resource.IsPlayingNodePaused();

    /// <summary>
    /// Gets the world position this NPC is currently moving to.
    /// </summary>
    public virtual Vector3 PositionMovingTo => Resource.GetPositionMovingTo();

    /// <summary>
    /// Gets the player this NPC is currently aiming at, or a handle with no value if not aiming at any player.
    /// </summary>
    public virtual IPlayer PlayerAimingAt => Resource.GetPlayerAimingAt();

    /// <summary>
    /// Gets the player this NPC is currently moving towards, or a handle with no value if not following a player.
    /// </summary>
    public virtual IPlayer PlayerMovingTo => Resource.GetPlayerMovingTo();

    /// <summary>
    /// Gets or sets the surfing data for this NPC.
    /// </summary>
    public virtual PlayerSurfingData SurfingData
    {
        get => Resource.GetSurfingData();
        set => Resource.SetSurfingData(value);
    }

    /// <summary>
    /// Sets the position of this NPC.
    /// </summary>
    /// <param name="position">The <see cref="Vector3" /> position to set.</param>
    /// <param name="immediateUpdate">A value indicating whether to broadcast a sync to streamed-in players immediately instead of waiting for the next tick.</param>
    public virtual void SetPosition(Vector3 position, bool immediateUpdate)
    {
        Resource.SetPosition(position, immediateUpdate);
    }

    /// <summary>
    /// Sets the rotation of this NPC. See <see cref="SetPosition" /> for the immediate-update flag.
    /// </summary>
    /// <param name="rotation">The rotation to set.</param>
    /// <param name="immediateUpdate">A value indicating whether to broadcast a sync to streamed-in players immediately instead of waiting for the next tick.</param>
    public virtual void SetRotation(Quaternion rotation, bool immediateUpdate)
    {
        Resource.SetRotation(rotation, immediateUpdate);
    }

    /// <summary>
    /// Spawns this NPC at its currently configured position and rotation.
    /// </summary>
    public virtual void Spawn()
    {
        Resource.Spawn();
    }

    /// <summary>
    /// Respawns this NPC, keeping its current state.
    /// </summary>
    public virtual void Respawn()
    {
        Resource.Respawn();
    }

    /// <summary>
    /// Instructs this NPC to move to the specified <paramref name="position" />.
    /// </summary>
    /// <param name="position">The position to move to.</param>
    /// <param name="moveType">The movement type (walk, jog, sprint, or drive).</param>
    /// <param name="moveSpeed">The speed to move at (-1 for default speed).</param>
    /// <param name="stopRange">The distance within which the NPC stops moving.</param>
    /// <returns><see langword="true" /> if the movement command was successful; <see langword="false" /> otherwise.</returns>
    public virtual bool MoveTo(Vector3 position, NPCMoveType moveType, float moveSpeed = -1f, float stopRange = 1.0f)
    {
        return Resource.Move(position, moveType, moveSpeed, stopRange);
    }

    /// <summary>
    /// Instructs this NPC to continuously follow <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player to follow.</param>
    /// <param name="moveType">The movement type.</param>
    /// <param name="moveSpeed">The speed to move at (-1 for default speed).</param>
    /// <param name="stopRange">The distance within which the NPC stops following.</param>
    /// <param name="posCheckUpdateDelay">How often the NPC recalculates the target's position.</param>
    /// <param name="autoRestart">If <see langword="true" />, the NPC restarts following after reaching the player.</param>
    /// <returns><see langword="true" /> if the command was successful; otherwise <see langword="false" />.</returns>
    public virtual bool MoveToPlayer(Player player, NPCMoveType moveType, float moveSpeed = -1f, float stopRange = 0.2f,
        TimeSpan posCheckUpdateDelay = default, bool autoRestart = false)
    {
        ArgumentNullException.ThrowIfNull(player);
        return Resource.MoveToPlayer(player, moveType, moveSpeed, stopRange,
            posCheckUpdateDelay == default ? new Milliseconds(DefaultPositionCheckUpdateDelayMs) : (Milliseconds)posCheckUpdateDelay,
            autoRestart);
    }

    /// <summary>
    /// Stops any active movement command for this NPC.
    /// </summary>
    public virtual void StopMoving()
    {
        Resource.StopMove();
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
        Resource.ApplyAnimation(new AnimationData(fDelta, loop, lockX, lockY, freeze,
            (uint)time.TotalMilliseconds, library, name));
    }

    /// <summary>
    /// Clears all animations applied to this NPC.
    /// </summary>
    public virtual void ClearAnimations()
    {
        Resource.ClearAnimations();
    }

    /// <summary>
    /// Resets the foot-sync animation for this NPC.
    /// </summary>
    public virtual void ResetAnimation()
    {
        Resource.ResetAnimation();
    }

    /// <summary>
    /// Sets the foot-sync animation parameters for this NPC.
    /// </summary>
    public virtual void SetAnimation(int animationId, float delta, bool loop, bool lockX, bool lockY, bool freeze, int time)
    {
        Resource.SetAnimation(animationId, delta, loop, lockX, lockY, freeze, time);
    }

    /// <summary>
    /// Gets the current foot-sync animation parameters for this NPC.
    /// </summary>
    public virtual void GetAnimation(out int animationId, out float delta, out bool loop, out bool lockX, out bool lockY, out bool freeze, out int time)
    {
        Resource.GetAnimation(out animationId, out delta, out loop, out lockX, out lockY, out freeze, out time);
    }

    /// <summary>
    /// Sets the velocity of this NPC.
    /// </summary>
    /// <param name="velocity">The velocity to set.</param>
    /// <param name="update">A value indicating whether to update immediately.</param>
    public virtual void SetVelocity(Vector3 velocity, bool update = false)
    {
        Resource.SetVelocity(velocity, update);
    }

    /// <summary>
    /// Determines whether this NPC is streamed in for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns><see langword="true" /> if this NPC is streamed in for the player; <see langword="false" /> otherwise.</returns>
    public virtual bool IsStreamedIn(Player player)
    {
        return player != null && Resource.IsStreamedInForPlayer(player);
    }

    /// <summary>
    /// Sets the weapon skill level for the specified weapon skill.
    /// </summary>
    public virtual void SetWeaponSkillLevel(PlayerWeaponSkill weaponSkill, int level)
    {
        Resource.SetWeaponSkillLevel(weaponSkill, level);
    }

    /// <summary>
    /// Gets the weapon skill level for the specified weapon skill.
    /// </summary>
    public virtual int GetWeaponSkillLevel(PlayerWeaponSkill weaponSkill)
    {
        return Resource.GetWeaponSkillLevel(weaponSkill);
    }

    /// <summary>
    /// Sets the NPC's movement keys.
    /// </summary>
    public virtual void SetKeys(ushort upAndDown, ushort leftAndRight, ushort keys)
    {
        Resource.SetKeys(upAndDown, leftAndRight, keys);
    }

    /// <summary>
    /// Gets the NPC's current movement keys.
    /// </summary>
    public virtual void GetKeys(out ushort upAndDown, out ushort leftAndRight, out ushort keys)
    {
        Resource.GetKeys(out upAndDown, out leftAndRight, out keys);
    }

    /// <summary>
    /// Starts a melee attack for the specified duration.
    /// </summary>
    /// <param name="time">Duration of the attack in milliseconds.</param>
    /// <param name="secondaryMeleeAttack">If <see langword="true" />, performs the secondary melee attack.</param>
    public virtual void MeleeAttack(int time, bool secondaryMeleeAttack = false)
    {
        Resource.MeleeAttack(time, secondaryMeleeAttack);
    }

    /// <summary>
    /// Stops the current melee attack.
    /// </summary>
    public virtual void StopMeleeAttack()
    {
        Resource.StopMeleeAttack();
    }

    /// <summary>
    /// Enables or disables weapon reloading for this NPC.
    /// </summary>
    public virtual void EnableReloading(bool toggle)
    {
        Resource.EnableReloading(toggle);
    }

    /// <summary>
    /// Enables or disables infinite ammo for this NPC.
    /// </summary>
    public virtual void EnableInfiniteAmmo(bool enable)
    {
        Resource.EnableInfiniteAmmo(enable);
    }

    /// <summary>
    /// Triggers a weapon shot from this NPC.
    /// </summary>
    public virtual void Shoot(int hitId, PlayerBulletHitType hitType, byte weapon, Vector3 endPoint, Vector3 offset, bool isHit, EntityCheckType betweenCheckFlags)
    {
        Resource.Shoot(hitId, hitType, weapon, endPoint, offset, isHit, betweenCheckFlags);
    }

    /// <summary>
    /// Instructs this NPC to aim at a world position.
    /// </summary>
    public virtual void AimAt(Vector3 point, bool shoot, int shootDelay, bool setAngle, Vector3 offsetFrom, EntityCheckType betweenCheckFlags)
    {
        Resource.AimAt(point, shoot, shootDelay, setAngle, offsetFrom, betweenCheckFlags);
    }

    /// <summary>
    /// Instructs this NPC to aim at a specific player.
    /// </summary>
    public virtual void AimAtPlayer(Player player, bool shoot, int shootDelay, bool setAngle, Vector3 offset, Vector3 offsetFrom, EntityCheckType betweenCheckFlags)
    {
        ArgumentNullException.ThrowIfNull(player);
        Resource.AimAtPlayer(player, shoot, shootDelay, setAngle, offset, offsetFrom, betweenCheckFlags);
    }

    /// <summary>
    /// Stops this NPC from aiming.
    /// </summary>
    public virtual void StopAim()
    {
        Resource.StopAim();
    }

    /// <summary>
    /// Gets a value indicating whether this NPC is currently aiming at the specified player.
    /// </summary>
    public virtual bool IsAimingAtPlayer(Player player)
    {
        return player != null && Resource.IsAimingAtPlayer(player);
    }

    /// <summary>
    /// Sets the accuracy of this NPC for the specified weapon.
    /// </summary>
    public virtual void SetWeaponAccuracy(byte weapon, float accuracy)
    {
        Resource.SetWeaponAccuracy(weapon, accuracy);
    }

    /// <summary>
    /// Gets the accuracy of this NPC for the specified weapon.
    /// </summary>
    public virtual float GetWeaponAccuracy(byte weapon)
    {
        return Resource.GetWeaponAccuracy(weapon);
    }

    /// <summary>
    /// Sets the reload time (in milliseconds) for the specified weapon.
    /// </summary>
    public virtual void SetWeaponReloadTime(byte weapon, int time)
    {
        Resource.SetWeaponReloadTime(weapon, time);
    }

    /// <summary>
    /// Gets the reload time (in milliseconds) for the specified weapon.
    /// </summary>
    public virtual int GetWeaponReloadTime(byte weapon)
    {
        return Resource.GetWeaponReloadTime(weapon);
    }

    /// <summary>
    /// Gets the actual reload time accounting for skill level and dual wield.
    /// </summary>
    public virtual int GetWeaponActualReloadTime(byte weapon)
    {
        return Resource.GetWeaponActualReloadTime(weapon);
    }

    /// <summary>
    /// Sets the shoot time (in milliseconds) for the specified weapon.
    /// </summary>
    public virtual void SetWeaponShootTime(byte weapon, int time)
    {
        Resource.SetWeaponShootTime(weapon, time);
    }

    /// <summary>
    /// Gets the shoot time (in milliseconds) for the specified weapon.
    /// </summary>
    public virtual int GetWeaponShootTime(byte weapon)
    {
        return Resource.GetWeaponShootTime(weapon);
    }

    /// <summary>
    /// Sets the clip size for the specified weapon.
    /// </summary>
    public virtual void SetWeaponClipSize(byte weapon, int size)
    {
        Resource.SetWeaponClipSize(weapon, size);
    }

    /// <summary>
    /// Gets the clip size for the specified weapon.
    /// </summary>
    public virtual int GetWeaponClipSize(byte weapon)
    {
        return Resource.GetWeaponClipSize(weapon);
    }

    /// <summary>
    /// Gets the actual clip size accounting for skill level and infinite ammo.
    /// </summary>
    public virtual int GetWeaponActualClipSize(byte weapon)
    {
        return Resource.GetWeaponActualClipSize(weapon);
    }

    /// <summary>
    /// Instructs this NPC to enter a vehicle.
    /// </summary>
    public virtual void EnterVehicle(IVehicle vehicle, byte seatId, NPCMoveType moveType)
    {
        Resource.EnterVehicle(vehicle, seatId, moveType);
    }

    /// <summary>
    /// Instructs this NPC to exit the current vehicle.
    /// </summary>
    public virtual void ExitVehicle()
    {
        Resource.ExitVehicle();
    }

    /// <summary>
    /// Teleports this NPC directly into a vehicle seat.
    /// </summary>
    public virtual bool PutInVehicle(IVehicle vehicle, byte seat)
    {
        return Resource.PutInVehicle(vehicle, seat);
    }

    /// <summary>
    /// Removes this NPC from its current vehicle.
    /// </summary>
    public virtual bool RemoveFromVehicle()
    {
        return Resource.RemoveFromVehicle();
    }

    /// <summary>
    /// Instructs this NPC to move along a previously created path.
    /// </summary>
    public virtual bool MoveByPath(int pathId, NPCMoveType moveType = NPCMoveType.Auto, float moveSpeed = -1f, bool reverse = false)
    {
        return Resource.MoveByPath(pathId, moveType, moveSpeed, reverse);
    }

    /// <summary>
    /// Pauses path-based movement temporarily.
    /// </summary>
    public virtual void PausePath()
    {
        Resource.PausePath();
    }

    /// <summary>
    /// Resumes previously paused path-based movement.
    /// </summary>
    public virtual void ResumePath()
    {
        Resource.ResumePath();
    }

    /// <summary>
    /// Stops path-based movement entirely.
    /// </summary>
    public virtual void StopPath()
    {
        Resource.StopPath();
    }

    /// <summary>
    /// Starts playing a pre-recorded movement by file name.
    /// </summary>
    /// <param name="recordName">The path to the record file (relative to scriptfiles).</param>
    /// <param name="autoUnload">If <see langword="true" />, the record is unloaded when playback ends.</param>
    /// <param name="point">The starting position for playback (zero vector for current position).</param>
    /// <param name="rotation">The starting rotation for playback.</param>
    public virtual bool StartPlayback(string recordName, bool autoUnload = true, Vector3 point = default, Quaternion rotation = default)
    {
        ArgumentNullException.ThrowIfNull(recordName);
        return Resource.StartPlaybackByName(recordName, autoUnload, point, rotation);
    }

    /// <summary>
    /// Starts playing a pre-loaded recording by its record ID.
    /// </summary>
    /// <param name="recordId">The record ID returned by <see cref="INpcService.LoadRecord" />.</param>
    /// <param name="autoUnload">If <see langword="true" />, the record is unloaded when playback ends.</param>
    /// <param name="point">The starting position for playback (zero vector for current position).</param>
    /// <param name="rotation">The starting rotation for playback.</param>
    public virtual bool StartPlayback(int recordId, bool autoUnload = true, Vector3 point = default, Quaternion rotation = default)
    {
        return Resource.StartPlaybackById(recordId, autoUnload, point, rotation);
    }

    /// <summary>
    /// Stops the current playback.
    /// </summary>
    public virtual void StopPlayback()
    {
        Resource.StopPlayback();
    }

    /// <summary>
    /// Pauses or resumes the current playback.
    /// </summary>
    public virtual void PausePlayback(bool paused = true)
    {
        Resource.PausePlayback(paused);
    }

    /// <summary>
    /// Starts node-based movement for this NPC.
    /// </summary>
    public virtual bool PlayNode(int nodeId, NPCMoveType moveType = NPCMoveType.Auto, float moveSpeed = -1f, float radius = 0f, bool setAngle = true)
    {
        return Resource.PlayNode(nodeId, moveType, moveSpeed, radius, setAngle);
    }

    /// <summary>
    /// Stops node-based movement.
    /// </summary>
    public virtual void StopPlayingNode()
    {
        Resource.StopPlayingNode();
    }

    /// <summary>
    /// Pauses node-based movement.
    /// </summary>
    public virtual void PausePlayingNode()
    {
        Resource.PausePlayingNode();
    }

    /// <summary>
    /// Resumes previously paused node-based movement.
    /// </summary>
    public virtual void ResumePlayingNode()
    {
        Resource.ResumePlayingNode();
    }

    /// <summary>
    /// Changes the active node and seeks to a specific target point.
    /// </summary>
    public virtual ushort ChangeNode(int nodeId, ushort targetPointId)
    {
        return Resource.ChangeNode(nodeId, targetPointId);
    }

    /// <summary>
    /// Updates the current node point to the specified point ID.
    /// </summary>
    public virtual bool UpdateNodePoint(ushort pointId)
    {
        return Resource.UpdateNodePoint(pointId);
    }

    /// <summary>
    /// Resets the surfing data for this NPC.
    /// </summary>
    public virtual void ResetSurfingData()
    {
        Resource.ResetSurfingData();
    }

    /// <summary>
    /// Gets a value indicating whether this NPC is currently moving towards the specified player.
    /// </summary>
    public virtual bool IsMovingToPlayer(Player player)
    {
        return player != null && Resource.IsMovingToPlayer(player);
    }

    /// <summary>
    /// Simulates this NPC's death.
    /// </summary>
    /// <param name="killer">The player who killed the NPC, or <see langword="null" /> for no killer.</param>
    /// <param name="weapon">The weapon used to kill the NPC.</param>
    public virtual void Kill(Player? killer, byte weapon)
    {
        Resource.Kill(killer != null ? (IPlayer)killer : default, weapon);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!Resource.GetExtension<ComponentExtension>().IsOmpEntityDestroyed)
        {
            _npcs.Destroy(Resource);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (!IsComponentAlive)
        {
            return "(Destroyed)";
        }
        return $"(Id: {Id})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Npc" /> to <see cref="INPC" />.
    /// </summary>
    public static implicit operator INPC(Npc? npc)
    {
        return npc?.Resource ?? default;
    }
}
