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

using SampSharp.Core.Natives.NativeObjects;
using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.World;

public partial class Npc
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CA1707 // Character _ present in the method's names
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell",
        "S3218:Inner class members should not shadow outer class \"static\" or type members", Justification = "Native function declarations")]
    public class NpcInternal : NativeObjectSingleton<NpcInternal>
    {
        #region Core
        [NativeMethod]
        public virtual int NPC_Create(string name)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_Destroy(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsValid(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsDead(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_Spawn(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_Respawn(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_GetAll(int npcid) // TODO
        {
            throw new NativeNotImplementedException();
        }
        #endregion
        #region Position and movement
        [NativeMethod]
        public virtual void NPC_SetPos(int npcid, float x, float y, float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_GetPos(int npcid, out float x, out float y, out float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetRot(int npcid, float x, float y, float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_GetRot(int npcid, out float x, out float y, out float z)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetFacingAngle(int npcid, float angle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_GetFacingAngle(int npcid, out float angle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetVirtualWorld(int npcid, int vw)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetVirtualWorld(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_Move(int npcid, float x, float y, float z, int moveType, float moveSpeed, float stopRange = 0.2f)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_MoveToPlayer(int npcid, int playerid, int moveType, float moveSpeed, float stopRange = 0.2f, 
            int updateDelayMS = 500, bool autoRestart = false)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_StopMove(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsMoving(int npcid)
        {
            throw new NativeNotImplementedException();
        }
        #endregion
        #region Appearance

        [NativeMethod]
        public virtual void NPC_SetSkin(int npcid, int model)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsStreamedIn(int npcid, int playerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsAnyStreamedIn(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetInterior(int npcid, int interiorid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetInterior(int npcid)
        {
            throw new NativeNotImplementedException();
        }
        #endregion
        #region Health & Combat
        [NativeMethod]
        public virtual void SetHealth(int npcid, float health)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual float GetHealth(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void SetArmour(int npcid, float armour)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual float GetArmour(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void SetInvulnerable(int npcid, bool toggle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool GetInvulnerable(int npcid)
        {
            throw new NativeNotImplementedException();
        }
        #endregion
        #region Weapons & Combat

        [NativeMethod]
        public virtual void NPC_SetWeapon(int npcid, int weaponid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetWeapon(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetAmmo(int npcid, int ammo)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetAmmo(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetAmmoInClip(int npcid, int ammo)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetAmmoInClip(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetKeys(int npcid, int updown, int leftright, int keys)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetKeys(int npcid, out int updown, out int leftright, out int keys)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_MeleeAttack(int npcid, int time, bool secondaryAttack = false)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_StopMeleeAttack(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsMeleeAttacking(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetFightingStyle(int npcid, int style)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetFightingStyle(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_EnableReloading(int npcid, bool enabe)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsReloadEnabled(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsReloading(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_EnableInfiniteAmmo(int npcid, bool enabe)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsInfiniteAmmoEnabled(int npcid, bool enabe)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetWeaponState(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_Shoot(int npcid, int weapon, int target, float endPointX, float endPointY, float endPointZ, 
            float offsetX, float offSetY, float offSetZ, bool isHit, int checkInBetweenFlags = (int)NPCEntityCheck.All)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsShooting(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_AimAt(int npcid, float pointX, float pointY, float pointZ, bool shoot, int shootDelay, bool updateAngle,
            float offsetFromX, float offsetFromY, float offsetFromZ, int checkInBetweenFlags = (int)NPCEntityCheck.All)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_AimAtPlaer(int npcid, int playerid, bool shoot, int shootDelay, bool updateAngle,
            float offsetX, float offSetY, float offSetZ, float offsetFromX, float offsetFromY, float offsetFromZ, 
            int checkInBetweenFlags = (int)NPCEntityCheck.All)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_StopAim(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsAiming(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsAimingAtPlayer(int npcid, int playerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetWeaponAccuracy(int npcid, int weaponid, float accuracy)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual float NPC_GetWeaponAccuracy(int npcid, int weaponid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetWeaponReloadTime(int npcid, int weaponid, int time)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetWeaponReloadTime(int npcid, int weaponid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetWeaponActualReloadTime(int npcid, int weaponid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetWeaponShootTime(int npcid, int weaponid, int time)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetWeaponShootTime(int npcid, int weaponid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetWeaponClipSize(int npcid, int weaponid, int size)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetWeaponClipSize(int npcid, int weaponid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetWeaponActualClipSize(int npcid, int weaponid)
        {
            throw new NativeNotImplementedException();
        }

        #endregion
        #region Vehicles

        [NativeMethod]
        public virtual bool NPC_EnterVehicle(int npcid, int vehicleid, int seatid, int moveType)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_ExitVehicle(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_PutInVehicle(int npcid, int vehicleid, int seatid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_RemoveFromVehicle(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetVehicle(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetVehicleId(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetVehicleSeat(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetEnteringVehicle(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetEnteringVehicleId(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetEnteringVehicleSeat(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsEnteringVehicle(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_UseVehicleSiren(int npcid, bool use = true)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool NPC_IsVehicleSirenUsed(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetVehicleHealth(int npcid, float health)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual float NPC_GetVehicleHealth(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetVehicleHydraThrusters(int npcid, int angle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetVehicleHydraThrusters(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetVehicleGearState(int npcid, int state)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int NPC_GetVehicleGearState(int npcid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void NPC_SetVehicleTrainSpeed(int npcid, float speed)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual float NPC_GetVehicleTrainSpeed(int npcid)
        {
            throw new NativeNotImplementedException();
        }
        #endregion
    }
}
