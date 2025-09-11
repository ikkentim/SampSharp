// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SampSharp.Core.Logging;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.World;

/// <summary>Represents a Open.MP NPC component.</summary>
public partial class Npc : IdentifiedPool<Npc>, IWorldObject
{

    public const int InvalidId = -1;

    /// <summary>Maximum number of NPC which can exist.</summary>
    public const int Max = 1000;

    public const int MaxNodes = 64;

    public const float MoveSpeedAuto = -1.0f;
    public const float MoveSpeedWalk = 0.1552086f;
    public const float MoveSpeedJog = 0.56444f;
    public const float MoveSpeedSprint = 0.926784f;

    public Npc()
    {
    }

    /// <summary>Gets or sets the name of this NPC.</summary>
    public virtual string Name
    {
        get;
        private set;
    }

    /// <summary>Gets or sets the facing angle of this NPC.</summary>
    public virtual float Angle
    {
        get
        {
            NpcInternal.Instance.NPC_GetFacingAngle(Id, out var angle);
            return angle;
        }
        set => NpcInternal.Instance.NPC_SetFacingAngle(Id, value);
    }

    /// <summary>Gets or sets the interior of this NPC.</summary>
    public virtual int Interior
    {
        get => NpcInternal.Instance.NPC_GetInterior(Id);
        set => NpcInternal.Instance.NPC_SetInterior(Id, value);
    }

    /// <summary>Gets or sets the virtual world of this NPC.</summary>
    public virtual int VirtualWorld
    {
        get => NpcInternal.Instance.NPC_GetVirtualWorld(Id);
        set => NpcInternal.Instance.NPC_SetVirtualWorld(Id, value);
    }

    /// <summary>Gets or sets the health of this NPC.</summary>
    public virtual float Health
    {
        get => NpcInternal.Instance.GetHealth(Id);
        set => NpcInternal.Instance.SetHealth(Id, value);
    }

    /// <summary>Gets or sets the armor of this NPC.</summary>
    public virtual float Armour
    {
        get => NpcInternal.Instance.GetArmour(Id);
        set => NpcInternal.Instance.SetArmour(Id, value);
    }

    /// <summary>Gets or sets the invulnerability of this NPC.</summary>
    public virtual bool Invulnerable
    {
        get => NpcInternal.Instance.GetInvulnerable(Id);
        set => NpcInternal.Instance.SetInvulnerable(Id, value);
    }



    /// <summary>Occurs when the <see cref="OnFinishMove" /> is being called. This callback is triggered when the npc reached it's destination.</summary>
    public event EventHandler<EventArgs> FinishedMove;

    /// <summary>Occurs when the <see cref="OnCreate" /> is being called. This callback is called when a npc connects to the server.</summary>
    public event EventHandler<EventArgs> Created;

    /// <summary>Occurs when the <see cref="OnDestroy" /> is being called. This callback is called when a npc disconnects from the server.</summary>
    public event EventHandler<EventArgs> Destroyed;

    /// <summary>Occurs when the <see cref="OnSpawn" /> is being called. This callback is called when a npc spawns.</summary>
    public event EventHandler<SpawnEventArgs> Spawned;

    /// <summary>Occurs when the <see cref="OnRespawn" /> is being called. This callback is called when a npc respawns.</summary>
    public event EventHandler<SpawnEventArgs> Respawned;

    /// <summary>Occurs when the <see cref="OnWeaponStateChange" /> is being called. This callback is called when the weapon state changes.</summary>
    public event EventHandler<WeaponStateChangeEventArgs> WeaponStateChanged;

    /// <summary>Occurs when the <see cref="OnTakeDamage" /> is being called. This callback is called when a npc takes damages.</summary>
    public event EventHandler<DamageEventArgs> TakeDamage;

    /// <summary>Occurs when the <see cref="OnGiveDamage" /> is being called. This callback is called when a npc gives damage.</summary>
    public event EventHandler<DamageEventArgs> GiveDamage;

    /// <summary>Occurs when the <see cref="OnDeath" /> is being called. This callback is triggered when the npc dies.</summary>
    public event EventHandler<DeathEventArgs> Died;

    /// <summary>Occurs when the <see cref="OnPlaybackStart" /> is being called. This callback is triggered when the playback starts.</summary>
    public event EventHandler<NpcPlaybackEventArgs> PlaybackStarted;

    /// <summary>Occurs when the <see cref="OnPlaybackEnd" /> is being called. This callback is triggered when the playback ends.</summary>
    public event EventHandler<NpcPlaybackEventArgs> PlaybackEnded;

    /// <summary>Occurs when the <see cref="OnWeaponShot" /> is being called. This callback is triggered when the npc shots with a weapon.</summary>
    public event EventHandler<WeaponShotEventArgs> WeaponShot;

    /// <summary>Occurs when the <see cref="OnFinishNodePoint" /> is being called.</summary>
    public event EventHandler<NpcFinishNodePointEventArgs> FinishNodePoint;

    /// <summary>Occurs when the <see cref="OnFinishNode" /> is being called.</summary>
    public event EventHandler<NpcFinishNodeEventArgs> FinishNode;

    /// <summary>Occurs when the <see cref="OnChangeNode" /> is being called.</summary>
    public event EventHandler<NpcChangeNodeEventArgs> ChangeNode;

    /// <summary>Occurs when the <see cref="OnFinishMovePath" /> is being called.</summary>
    public event EventHandler<NpcFinishMovePathEventArgs> FinishMovePath;

    /// <summary>Occurs when the <see cref="OnFinishMovePathPoint" /> is being called.</summary>
    public event EventHandler<NpcFinishMovePathPointEventArgs> FinishMovePathPoint;


    /// <summary>Creates the NPC with the given name</summary>
    /// <param name="name">The name of the NPC</param>
    public static Npc Create(string name)
    {
        var r = Npc.Create(-1); // Get an instance of Npc class by using IdentifiedPool's create method
        var id = NpcInternal.Instance.NPC_Create(name); // Create the Npc on the server and get it's id
        r.Id = id;
        r.Name = name;
        return r;
    }

    /// <summary>Spawns a NPC.</summary>
    public virtual void Spawn()
    {
        AssertNotDisposed();

        NpcInternal.Instance.NPC_Spawn(Id);
    }

    /// <summary>Respawns a NPC.</summary>
    public virtual void Respawn()
    {
        AssertNotDisposed();

        NpcInternal.Instance.NPC_Respawn(Id);
    }


    /// <summary>Gets the WeaponState of the Weapon this NPC is currently holding.</summary>
    public virtual WeaponState WeaponState => (WeaponState)NpcInternal.Instance.NPC_GetWeaponState(Id);

    /// <summary>Gets or sets the Weapon this NPC is currently holding.</summary>
    public virtual Weapon Weapon
    {
        get => (Weapon)NpcInternal.Instance.NPC_GetWeapon(Id);
        set => NpcInternal.Instance.NPC_SetWeapon(Id, (int)value);
    }

    /// <summary>Gets or sets the ammo of the Weapon this NPC is currently holding.</summary>
    /// <remarks>This method does not need parameter "weapon" unlike BasePlayer, I assume it sets the ammo of the current hold weapon</remarks>
    public virtual int WeaponAmmo
    {
        get => NpcInternal.Instance.NPC_GetAmmo(Id);
        set => NpcInternal.Instance.NPC_SetAmmo(Id, value);
    }


    /// <summary>Gets or sets the FightStyle of this NPC.</summary>
    public virtual FightStyle FightStyle
    {
        get => (FightStyle)NpcInternal.Instance.NPC_GetFightingStyle(Id);
        set => NpcInternal.Instance.NPC_SetFightingStyle(Id, (int)value);
    }


    /// <summary>Gets the vehicle seat this NPC sits on.</summary>
    public virtual int VehicleSeat => NpcInternal.Instance.NPC_GetVehicleSeat(Id);

    /// <summary>Gets whether this NPC is currently in any vehicle.</summary>
    public virtual bool InAnyVehicle => NpcInternal.Instance.NPC_GetVehicle(Id) != -1;


    /// <summary>Gets the Vehicle this NPC is currently in.</summary>
    public virtual BaseVehicle Vehicle
    {
        get
        {
            var vehicleid = NpcInternal.Instance.NPC_GetVehicleId(Id);
            return vehicleid == 0
                ? null
                : BaseVehicle.Find(vehicleid);
        }
    }

    /// <summary>Gets or sets the rotation of this NPC.</summary>
    /// <remarks>Only the Z angle can be set!</remarks>
    public virtual Vector3 Rotation
    {
        get => new(0, 0, Angle);
        set => Angle = value.Z;
    }

    /// <summary>Gets or sets the position of this NPC.</summary>
    public virtual Vector3 Position
    {
        get
        {
            NpcInternal.Instance.NPC_GetPos(Id, out var x, out var y, out var z);
            return new Vector3(x, y, z);
        }
        set => NpcInternal.Instance.NPC_SetPos(Id, value.X, value.Y, value.Z);
    }

    /// <summary>Gets a value indicating whether this Player is alive.</summary>
    public virtual bool IsAlive => !NpcInternal.Instance.NPC_IsDead(Id);


    /// <summary>Checks if a <see cref="Npc" /> is streamed in this <see cref="BasePlayer" />'s client.</summary>
    /// <param name="other">The NPC to check is streamed in.</param>
    /// <returns>True if the other NPC is streamed in for this Player, False if not.</returns>
    public virtual bool IsPlayerStreamedIn(BasePlayer other)
    {
        AssertNotDisposed();

        ArgumentNullException.ThrowIfNull(other);

        return NpcInternal.Instance.NPC_IsStreamedIn(other.Id, Id);
    }

    /// <summary>Check which keys this <see cref="Npc" /> is pressing.</summary>
    /// <remarks>
    /// Only the FUNCTION of keys can be detected; not actual keys. You can not detect if the player presses space, but you can detect if they press sprint
    /// (which can be mapped (assigned) to ANY key, but is space by default)).
    /// </remarks>
    /// <param name="updown">Up or Down value, passed by reference.</param>
    /// <param name="leftright">Left or Right value, passed by reference.</param>
    /// <param name="keys">A set of bits containing this NPC's key states</param>
    public virtual void GetKeys(out int updown, out int leftright, out Keys keys)
    {
        AssertNotDisposed();

        NpcInternal.Instance.NPC_GetKeys(Id, out updown, out leftright, out var keysDown);
        keys = (Keys)keysDown;
    }

    /// <summary>Sets which keys this <see cref="Npc" /> is pressing.</summary>
    /// <remarks>
    /// Only the FUNCTION of keys can be detected; not actual keys. You can not detect if the player presses space, but you can detect if they press sprint
    /// (which can be mapped (assigned) to ANY key, but is space by default)).
    /// </remarks>
    /// <param name="updown">Up or Down value, passed by reference.</param>
    /// <param name="leftright">Left or Right value, passed by reference.</param>
    /// <param name="keys">A set of bits containing this NPC's key states</param>
    public virtual void SetKeys(int updown, int leftright, Keys keys)
    {
        AssertNotDisposed();

        NpcInternal.Instance.NPC_SetKeys(Id, updown, leftright, (int)keys);
    }


    /// <summary>Puts this <see cref="Npc" /> in a <see cref="BaseVehicle" />.</summary>
    /// <param name="vehicle">The vehicle for the NPC to be put in.</param>
    /// <param name="seatid">The ID of the seat to put the NPC in (default = 0).</param>
    public virtual void PutInVehicle(BaseVehicle vehicle, int seatid = 0)
    {
        AssertNotDisposed();

        ArgumentNullException.ThrowIfNull(vehicle);

        NpcInternal.Instance.NPC_PutInVehicle(Id, vehicle.Id, seatid);
    }

    /// <summary>Removes/ejects this <see cref="Npc" /> from his vehicle.</summary>
    /// <remarks>
    /// The exiting animation is not synced for other players. This function will not work when used in <see cref="OnEnterVehicle" />, because the player
    /// isn't in the vehicle when the callback is called. Use <see cref="OnStateChanged" /> instead.
    /// </remarks>
    public virtual void RemoveFromVehicle()
    {
        AssertNotDisposed();

        NpcInternal.Instance.NPC_RemoveFromVehicle(Id);
    }


    /// <summary>
    /// Moves the NPC to a specific position
    /// </summary>
    /// <param name="target">The position to move to</param>
    /// <param name="moveType">The <see cref="NPCMoveType" /> (jog, sprint, walk, ...)</param>
    /// <param name="moveSpeed">The move speed</param>
    /// <param name="stopRange">The stop range (default = 0.2f)</param>
    public virtual void Move(Vector3 target, NPCMoveType moveType = NPCMoveType.Jog, float moveSpeed = MoveSpeedAuto, float stopRange = 0.2f)
    {
        AssertNotDisposed();

        NpcInternal.Instance.NPC_Move(Id, target.X, target.Y, target.Z, (int)moveType, moveSpeed, stopRange);
    }

    /// <summary>
    /// Moves the NPC to a specific player
    /// </summary>
    /// <param name="target">The <see cref="BasePlayer" /> to move to</param>
    /// <param name="moveType">The move type (jog, sprint, walk, ...)</param>
    /// <param name="moveSpeed">The move speed</param>
    /// <param name="stopRange">The stop range (default = 0.2f)</param>
    /// <param name="updateDelayMS">The delay in MS between 2 updates</param>
    /// <param name="autoRestart">If True, the NPC will keep following the <see cref="BasePlayer"/></param>
    public virtual void MoveToPlayer(BasePlayer target, NPCMoveType moveType = NPCMoveType.Jog, float moveSpeed = MoveSpeedAuto, float stopRange = 0.2f,
        int updateDelayMS = 500, bool autoRestart = false)
    {
        AssertNotDisposed();

        ArgumentNullException.ThrowIfNull(target);

        NpcInternal.Instance.NPC_MoveToPlayer(Id, target.Id, (int)moveType, moveSpeed, stopRange, updateDelayMS, autoRestart);
    }

    /// <summary>
    /// Stops the NPC
    /// </summary>
    /// <returns>True if the NPC has been stopped</returns>
    public virtual bool StopMove()
    {
        AssertNotDisposed();

        return NpcInternal.Instance.NPC_StopMove(Id);
    }

    /// <summary>
    /// Returns true if the NPC is moving
    /// </summary>
    /// <returns>True if the NPC is moving</returns>
    public virtual bool IsMoving()
    {
        AssertNotDisposed();

        return NpcInternal.Instance.NPC_IsMoving(Id);
    }



    /// <summary>Raises the <see cref="Died" /> event.</summary>
    /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
    public virtual void OnFinishMove(EventArgs e)
    {
        CoreLog.Log(CoreLogLevel.Info, $"Method NPC.OnNPCFinishMove called, triggering FinishedMove event ...");
        FinishedMove?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="Created" /> event.</summary>
    /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
    public virtual void OnCreate(EventArgs e)
    {
        Created?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="Destroyed" /> event.</summary>
    /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
    public virtual void OnDestroy(EventArgs e)
    {
        Destroyed?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="Spawned" /> event.</summary>
    /// <param name="e">An <see cref="SpawnEventArgs" /> that contains the event data. </param>
    public virtual void OnSpawn(SpawnEventArgs e)
    {
        Spawned?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="Respawned" /> event.</summary>
    /// <param name="e">An <see cref="SpawnEventArgs" /> that contains the event data. </param>
    public virtual void OnRespawn(SpawnEventArgs e)
    {
        Respawned?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="WeaponStateChanged" /> event.</summary>
    /// <param name="e">An <see cref="SpawnEventArgs" /> that contains the event data. </param>
    public virtual void OnWeaponStateChange(WeaponStateChangeEventArgs e)
    {
        WeaponStateChanged?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="TakeDamage" /> event.</summary>
    /// <param name="e">An <see cref="DamageEventArgs" /> that contains the event data. </param>
    public virtual void OnTakeDamage(DamageEventArgs e)
    {
        TakeDamage?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="GiveDamage" /> event.</summary>
    /// <param name="e">An <see cref="DamageEventArgs" /> that contains the event data. </param>
    public virtual void OnGiveDamage(DamageEventArgs e)
    {
        GiveDamage?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="Died" /> event.</summary>
    /// <param name="e">An <see cref="DeathEventArgs" /> that contains the event data. </param>
    public virtual void OnDeath(DeathEventArgs e)
    {
        Died?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="PlaybackStarted" /> event.</summary>
    /// <param name="e">An <see cref="NpcPlaybackEventArgs" /> that contains the event data. </param>
    public virtual void OnPlaybackStart(NpcPlaybackEventArgs e)
    {
        PlaybackStarted?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="PlaybackEnded" /> event.</summary>
    /// <param name="e">An <see cref="NpcPlaybackEventArgs" /> that contains the event data. </param>
    public virtual void OnPlaybackEnd(NpcPlaybackEventArgs e)
    {
        PlaybackEnded?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="Weaponshot" /> event.</summary>
    /// <param name="e">An <see cref="WeaponShotEventArgs" /> that contains the event data. </param>
    public virtual void OnWeaponShot(WeaponShotEventArgs e)
    {
        WeaponShot?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="FinishNodePoint" /> event.</summary>
    /// <param name="e">An <see cref="NpcFinishNodePointEventArgs" /> that contains the event data. </param>
    public virtual void OnFinishNodePoint(NpcFinishNodePointEventArgs e)
    {
        FinishNodePoint?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="FinishNodePoint" /> event.</summary>
    /// <param name="e">An <see cref="NpcFinishNodeEventArgs" /> that contains the event data. </param>
    public virtual void OnFinishNode(NpcFinishNodeEventArgs e)
    {
        FinishNode?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="ChangeNodePoint" /> event.</summary>
    /// <param name="e">An <see cref="NpcChangeNodeEventArgs" /> that contains the event data. </param>
    public virtual void OnChangeNode(NpcChangeNodeEventArgs e)
    {
        ChangeNode?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="FinishMovePath" /> event.</summary>
    /// <param name="e">An <see cref="NpcFinishMovePathEventArgs" /> that contains the event data. </param>
    public virtual void OnFinishMovePath(NpcFinishMovePathEventArgs e)
    {
        FinishMovePath?.Invoke(this, e);
    }

    /// <summary>Raises the <see cref="FinishMovePathPoint" /> event.</summary>
    /// <param name="e">An <see cref="NpcFinishMovePathPointEventArgs" /> that contains the event data. </param>
    public virtual void OnFinishMovePathPoint(NpcFinishMovePathPointEventArgs e)
    {
        FinishMovePathPoint?.Invoke(this, e);
    }
}
