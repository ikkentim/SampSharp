using System.Numerics;
using SampSharp.OpenMp.Core.RobinHood;
using SampSharp.OpenMp.Core.Std.Chrono;

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

    /// <summary>Tells the NPC to continuously follow <paramref name="targetPlayer" /> using the specified movement type.</summary>
    /// <param name="targetPlayer">The player to follow.</param>
    /// <param name="moveType">The movement type (walk, jog, sprint, etc.).</param>
    /// <param name="moveSpeed">The movement speed (-1 for automatic).</param>
    /// <param name="stopRange">The radius within which the NPC stops following.</param>
    /// <param name="posCheckUpdateDelay">How often (in milliseconds) the NPC recalculates the target's position.</param>
    /// <param name="autoRestart">If <see langword="true" />, the NPC will restart following after reaching the player.</param>
    public partial bool MoveToPlayer(IPlayer targetPlayer, NPCMoveType moveType, float moveSpeed, float stopRange, Milliseconds posCheckUpdateDelay, bool autoRestart);

    /// <summary>Stops any active movement.</summary>
    public partial void StopMove();

    /// <summary>Gets the set of players this NPC is currently streamed in for.</summary>
    public partial FlatPtrHashSet<IPlayer> StreamedForPlayers();

    /// <summary>Clears any applied animation.</summary>
    public partial void ClearAnimations();

    /// <summary>Applies an animation. <paramref name="animationData" /> bundles lib + name + flags + duration.</summary>
    public partial void ApplyAnimation(AnimationData animationData);

    /// <summary>Checks whether this NPC is streamed in for the given player.</summary>
    public partial bool IsStreamedInForPlayer(IPlayer other);

    /// <summary>Sets the weapon skill level for the specified weapon skill.</summary>
    public partial void SetWeaponSkillLevel(PlayerWeaponSkill weaponSkill, int level);

    /// <summary>Gets the weapon skill level for the specified weapon skill.</summary>
    public partial int GetWeaponSkillLevel(PlayerWeaponSkill weaponSkill);

    /// <summary>Sets the NPC's movement keys.</summary>
    public partial void SetKeys(ushort upAndDown, ushort leftAndRight, ushort keys);

    /// <summary>Gets the NPC's current movement keys.</summary>
    public partial void GetKeys(out ushort upAndDown, out ushort leftAndRight, out ushort keys);

    /// <summary>Starts a melee attack for the specified duration.</summary>
    /// <param name="time">Duration of the attack in milliseconds.</param>
    /// <param name="secondaryMeleeAttack">If <see langword="true" />, performs the secondary melee attack.</param>
    public partial void MeleeAttack(int time, bool secondaryMeleeAttack);

    /// <summary>Stops the current melee attack.</summary>
    public partial void StopMeleeAttack();

    /// <summary>Gets a value indicating whether the NPC is currently performing a melee attack.</summary>
    public partial bool IsMeleeAttacking();

    /// <summary>Sets the NPC's fighting style.</summary>
    public partial void SetFightingStyle(PlayerFightingStyle style);

    /// <summary>Gets the NPC's current fighting style.</summary>
    public partial PlayerFightingStyle GetFightingStyle();

    /// <summary>Enables or disables weapon reloading for the NPC.</summary>
    public partial void EnableReloading(bool toggle);

    /// <summary>Gets a value indicating whether weapon reloading is enabled for the NPC.</summary>
    public partial bool IsReloadEnabled();

    /// <summary>Gets a value indicating whether the NPC is currently reloading its weapon.</summary>
    public partial bool IsReloading();

    /// <summary>Enables or disables infinite ammo for the NPC.</summary>
    public partial void EnableInfiniteAmmo(bool enable);

    /// <summary>Gets a value indicating whether infinite ammo is enabled for the NPC.</summary>
    public partial bool IsInfiniteAmmoEnabled();

    /// <summary>Gets the NPC's current weapon state.</summary>
    public partial PlayerWeaponState GetWeaponState();

    /// <summary>Sets the NPC's weapon state.</summary>
    public partial void SetWeaponState(PlayerWeaponState state);

    /// <summary>Sets the ammo count in the current weapon's clip.</summary>
    public partial void SetAmmoInClip(int ammo);

    /// <summary>Gets the ammo count in the current weapon's clip.</summary>
    public partial int GetAmmoInClip();

    /// <summary>Triggers a weapon shot from the NPC.</summary>
    public partial void Shoot(int hitId, PlayerBulletHitType hitType, byte weapon, Vector3 endPoint, Vector3 offset, bool isHit, EntityCheckType betweenCheckFlags);

    /// <summary>Gets a value indicating whether the NPC is currently shooting.</summary>
    public partial bool IsShooting();

    /// <summary>Instructs the NPC to aim at a world position.</summary>
    public partial void AimAt(Vector3 point, bool shoot, int shootDelay, bool setAngle, Vector3 offsetFrom, EntityCheckType betweenCheckFlags);

    /// <summary>Instructs the NPC to aim at a specific player.</summary>
    public partial void AimAtPlayer(IPlayer atPlayer, bool shoot, int shootDelay, bool setAngle, Vector3 offset, Vector3 offsetFrom, EntityCheckType betweenCheckFlags);

    /// <summary>Stops the NPC from aiming.</summary>
    public partial void StopAim();

    /// <summary>Gets a value indicating whether the NPC is currently aiming.</summary>
    public partial bool IsAiming();

    /// <summary>Gets a value indicating whether the NPC is currently aiming at the specified player.</summary>
    public partial bool IsAimingAtPlayer(IPlayer player);

    /// <summary>Sets the accuracy of the NPC for the specified weapon.</summary>
    public partial void SetWeaponAccuracy(byte weapon, float accuracy);

    /// <summary>Gets the accuracy of the NPC for the specified weapon.</summary>
    public partial float GetWeaponAccuracy(byte weapon);

    /// <summary>Sets the reload time (in milliseconds) for the specified weapon.</summary>
    public partial void SetWeaponReloadTime(byte weapon, int time);

    /// <summary>Gets the reload time (in milliseconds) for the specified weapon.</summary>
    public partial int GetWeaponReloadTime(byte weapon);

    /// <summary>Gets the actual reload time accounting for skill level and dual wield.</summary>
    public partial int GetWeaponActualReloadTime(byte weapon);

    /// <summary>Sets the shoot time (in milliseconds) for the specified weapon.</summary>
    public partial void SetWeaponShootTime(byte weapon, int time);

    /// <summary>Gets the shoot time (in milliseconds) for the specified weapon.</summary>
    public partial int GetWeaponShootTime(byte weapon);

    /// <summary>Sets the clip size for the specified weapon.</summary>
    public partial void SetWeaponClipSize(byte weapon, int size);

    /// <summary>Gets the clip size for the specified weapon.</summary>
    public partial int GetWeaponClipSize(byte weapon);

    /// <summary>Gets the actual clip size accounting for skill level and infinite ammo.</summary>
    public partial int GetWeaponActualClipSize(byte weapon);

    /// <summary>Instructs the NPC to enter a vehicle.</summary>
    public partial void EnterVehicle(IVehicle vehicle, byte seatId, NPCMoveType moveType);

    /// <summary>Instructs the NPC to exit the current vehicle.</summary>
    public partial void ExitVehicle();

    /// <summary>Teleports the NPC directly into a vehicle seat.</summary>
    public partial bool PutInVehicle(IVehicle vehicle, byte seat);

    /// <summary>Removes the NPC from its current vehicle.</summary>
    public partial bool RemoveFromVehicle();

    /// <summary>Instructs the NPC to move along a previously created path.</summary>
    public partial bool MoveByPath(int pathId, NPCMoveType moveType, float moveSpeed, bool reverse);

    /// <summary>Pauses path-based movement temporarily.</summary>
    public partial void PausePath();

    /// <summary>Resumes previously paused path-based movement.</summary>
    public partial void ResumePath();

    /// <summary>Stops path-based movement entirely.</summary>
    public partial void StopPath();

    /// <summary>Gets a value indicating whether the NPC is currently moving along a path.</summary>
    public partial bool IsMovingByPath();

    /// <summary>Gets a value indicating whether the NPC's path movement is currently paused.</summary>
    public partial bool IsPathPaused();

    /// <summary>Gets the ID of the path the NPC is currently following, or -1 if none.</summary>
    public partial int GetCurrentPathId();

    /// <summary>Gets the index of the current waypoint within the active path.</summary>
    public partial int GetCurrentPathPointIndex();

    /// <summary>Gets the vehicle this NPC is currently in, or a handle with no value if not in a vehicle.</summary>
    public partial IVehicle GetVehicle();

    /// <summary>Gets the seat index this NPC occupies in the current vehicle.</summary>
    public partial int GetVehicleSeat();

    /// <summary>Gets the vehicle the NPC is in the process of entering, or a handle with no value if not entering one.</summary>
    public partial IVehicle GetEnteringVehicle();

    /// <summary>Gets the seat index the NPC is targeting while entering a vehicle.</summary>
    public partial int GetEnteringVehicleSeat();

    /// <summary>Enables or disables the siren on the NPC's vehicle.</summary>
    public partial void UseVehicleSiren(bool use);

    /// <summary>Gets a value indicating whether the NPC's vehicle siren is currently active.</summary>
    public partial bool IsVehicleSirenUsed();

    /// <summary>Sets the health of the NPC's current vehicle.</summary>
    public partial void SetVehicleHealth(float health);

    /// <summary>Gets the health of the NPC's current vehicle.</summary>
    public partial float GetVehicleHealth();

    /// <summary>Sets the hydra thruster direction for the NPC's vehicle.</summary>
    public partial void SetVehicleHydraThrusters(int direction);

    /// <summary>Gets the hydra thruster direction for the NPC's vehicle.</summary>
    public partial int GetVehicleHydraThrusters();

    /// <summary>Sets the gear state for the NPC's vehicle.</summary>
    public partial void SetVehicleGearState(int gear);

    /// <summary>Gets the gear state for the NPC's vehicle.</summary>
    public partial int GetVehicleGearState();

    /// <summary>Sets the train speed for the NPC's vehicle.</summary>
    public partial void SetVehicleTrainSpeed(float speed);

    /// <summary>Gets the train speed for the NPC's vehicle.</summary>
    public partial float GetVehicleTrainSpeed();

    /// <summary>Resets the foot-sync animation for the NPC.</summary>
    public partial void ResetAnimation();

    /// <summary>Sets the foot-sync animation for the NPC.</summary>
    public partial void SetAnimation(int animationId, float delta, bool loop, bool lockX, bool lockY, bool freeze, int time);

    /// <summary>Gets the current foot-sync animation parameters for the NPC.</summary>
    public partial void GetAnimation(out int animationId, out float delta, out bool loop, out bool lockX, out bool lockY, out bool freeze, out int time);

    /// <summary>Sets the special action for the NPC.</summary>
    public partial void SetSpecialAction(PlayerSpecialAction action);

    /// <summary>Gets the current special action for the NPC.</summary>
    public partial PlayerSpecialAction GetSpecialAction();

    /// <summary>Starts playing a pre-recorded movement by file name.</summary>
    /// <param name="recordName">The path to the record file (relative to scriptfiles).</param>
    /// <param name="autoUnload">If <see langword="true" />, the record is unloaded when playback ends.</param>
    /// <param name="point">The starting position for playback (zero vector for current position).</param>
    /// <param name="rotation">The starting rotation for playback.</param>
    public partial bool StartPlaybackByName(string recordName, bool autoUnload, Vector3 point, GTAQuat rotation);

    /// <summary>Starts playing a pre-loaded recording by its record ID.</summary>
    /// <param name="recordId">The record ID returned by <see cref="INPCComponent.LoadRecord" />.</param>
    /// <param name="autoUnload">If <see langword="true" />, the record is unloaded when playback ends.</param>
    /// <param name="point">The starting position for playback (zero vector for current position).</param>
    /// <param name="rotation">The starting rotation for playback.</param>
    public partial bool StartPlaybackById(int recordId, bool autoUnload, Vector3 point, GTAQuat rotation);

    /// <summary>Stops the current playback.</summary>
    public partial void StopPlayback();

    /// <summary>Pauses or resumes the current playback.</summary>
    public partial void PausePlayback(bool paused);

    /// <summary>Gets a value indicating whether the NPC is currently playing a recording.</summary>
    public partial bool IsPlayingPlayback();

    /// <summary>Gets a value indicating whether playback is currently paused.</summary>
    public partial bool IsPlaybackPaused();

    /// <summary>Starts node-based movement for the NPC.</summary>
    public partial bool PlayNode(int nodeId, NPCMoveType moveType, float moveSpeed, float radius, bool setAngle);

    /// <summary>Stops node-based movement.</summary>
    public partial void StopPlayingNode();

    /// <summary>Pauses node-based movement.</summary>
    public partial void PausePlayingNode();

    /// <summary>Resumes previously paused node-based movement.</summary>
    public partial void ResumePlayingNode();

    /// <summary>Gets a value indicating whether node-based movement is currently paused.</summary>
    public partial bool IsPlayingNodePaused();

    /// <summary>Gets a value indicating whether the NPC is currently following a node path.</summary>
    public partial bool IsPlayingNode();

    /// <summary>Changes the active node and seeks to a specific target point.</summary>
    public partial ushort ChangeNode(int nodeId, ushort targetPointId);

    /// <summary>Updates the current node point to the specified point ID.</summary>
    public partial bool UpdateNodePoint(ushort pointId);

    /// <summary>Sets the surfing data for the NPC.</summary>
    public partial void SetSurfingData(PlayerSurfingData data);

    /// <summary>Gets the current surfing data for the NPC.</summary>
    public partial PlayerSurfingData GetSurfingData();

    /// <summary>Resets the NPC's surfing data.</summary>
    public partial void ResetSurfingData();

    /// <summary>Gets a value indicating whether the NPC is currently moving towards the specified player.</summary>
    public partial bool IsMovingToPlayer(IPlayer player);

    /// <summary>Simulates NPC death, optionally with a killer and weapon.</summary>
    public partial void Kill(IPlayer killer, byte weapon);

    /// <summary>Gets the player the NPC is currently aiming at, or a handle with no value if not aiming at any player.</summary>
    public partial IPlayer GetPlayerAimingAt();

    /// <summary>Gets the player the NPC is currently moving towards, or a handle with no value if not following a player.</summary>
    public partial IPlayer GetPlayerMovingTo();

    /// <summary>Gets the world position the NPC is currently moving to.</summary>
    public partial Vector3 GetPositionMovingTo();
}