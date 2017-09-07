// SampSharp
// Copyright 2017 Tim Potze
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

namespace SampSharp.GameMode.World
{
    public partial class BasePlayer
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class PlayerInternal : NativeObjectSingleton<PlayerInternal>
        {
            [NativeMethod]
            public virtual bool SetSpawnInfo(int playerid, int team, int skin, float x, float y, float z, float rotation,
                int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SpawnPlayer(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerPos(int playerid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerPosFindZ(int playerid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerPos(int playerid, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerFacingAngle(int playerid, float angle)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerFacingAngle(int playerid, out float angle)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerInRangeOfPoint(int playerid, float range, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual float GetPlayerDistanceFromPoint(int playerid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerStreamedIn(int playerid, int forplayerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerInterior(int playerid, int interiorid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerInterior(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerHealth(int playerid, float health)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerHealth(int playerid, out float health)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerArmour(int playerid, float armour)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerArmour(int playerid, out float armour)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerAmmo(int playerid, int weaponid, int ammo)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerAmmo(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerWeaponState(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerTargetPlayer(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerTargetActor(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerTeam(int playerid, int teamid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerTeam(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerScore(int playerid, int score)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerScore(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerDrunkLevel(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerDrunkLevel(int playerid, int level)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerColor(int playerid, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerColor(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerSkin(int playerid, int skinid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerSkin(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GivePlayerWeapon(int playerid, int weaponid, int ammo)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ResetPlayerWeapons(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerArmedWeapon(int playerid, int weaponid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerWeaponData(int playerid, int slot, out int weapon, out int ammo)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GivePlayerMoney(int playerid, int money)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ResetPlayerMoney(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int SetPlayerName(int playerid, string name)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerMoney(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerState(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerIp(int playerid, out string ip, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerPing(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerWeapon(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerKeys(int playerid, out int keys, out int updown, out int leftright)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerName(int playerid, out string name, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerTime(int playerid, int hour, int minute)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerTime(int playerid, out int hour, out int minute)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TogglePlayerClock(int playerid, bool toggle)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerWeather(int playerid, int weather)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ForceClassSelection(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerWantedLevel(int playerid, int level)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerWantedLevel(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerFightingStyle(int playerid, int style)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerFightingStyle(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerVelocity(int playerid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerVelocity(int playerid, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayCrimeReportForPlayer(int playerid, int suspectid, int crime)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayAudioStreamForPlayer(int playerid, string url, float posX, float posY, float posZ,
                float distance, bool usepos)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool StopAudioStreamForPlayer(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerShopName(int playerid, string shopname)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerSkillLevel(int playerid, int skill, int level)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerSurfingVehicleID(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerSurfingObjectID(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerAttachedObject(int playerid, int index, int modelid, int bone, float offsetX,
                float offsetY, float offsetZ, float rotX, float rotY, float rotZ, float scaleX, float scaleY,
                float scaleZ, int materialcolor1, int materialcolor2)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool RemovePlayerAttachedObject(int playerid, int index)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerAttachedObjectSlotUsed(int playerid, int index)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool EditAttachedObject(int playerid, int index)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerChatBubble(int playerid, string text, int color, float drawdistance,
                int expiretime)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PutPlayerInVehicle(int playerid, int vehicleid, int seatid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerVehicleID(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerVehicleSeat(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool RemovePlayerFromVehicle(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TogglePlayerControllable(int playerid, bool toggle)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerPlaySound(int playerid, int soundid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ApplyAnimation(int playerid, string animlib, string animname, float fDelta, bool loop,
                bool lockx, bool locky, bool freeze, int time, bool forcesync)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ClearAnimations(int playerid, bool forcesync)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerAnimationIndex(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetAnimationName(int index, out string animlib, int animlibSize, out string animname,
                int animnameSize)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerSpecialAction(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerSpecialAction(int playerid, int actionid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DisableRemoteVehicleCollisions(int playerid, bool disable)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerCheckpoint(int playerid, float x, float y, float z, float size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DisablePlayerCheckpoint(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerRaceCheckpoint(int playerid, int type, float x, float y, float z, float nextx,
                float nexty, float nextz, float size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DisablePlayerRaceCheckpoint(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerWorldBounds(int playerid, float xMax, float xMin, float yMax, float yMin)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerMarkerForPlayer(int playerid, int showplayerid, int color)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ShowPlayerNameTagForPlayer(int playerid, int showplayerid, bool show)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerMapIcon(int playerid, int iconid, float x, float y, float z, int markertype,
                int color, int style)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool RemovePlayerMapIcon(int playerid, int iconid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerCameraPos(int playerid, float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerCameraLookAt(int playerid, float x, float y, float z, int cut)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetCameraBehindPlayer(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerCameraPos(int playerid, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerCameraFrontVector(int playerid, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerCameraMode(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool EnablePlayerCameraTarget(int playerid, bool enable)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerCameraTargetObject(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerCameraTargetVehicle(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerCameraTargetPlayer(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerCameraTargetActor(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool InterpolateCameraPos(int playerid, float fromX, float fromY, float fromZ, float toX,
                float toY, float toZ, int time, int cut)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool InterpolateCameraLookAt(int playerid, float fromX, float fromY, float fromZ, float toX,
                float toY, float toZ, int time, int cut)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerConnected(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerInVehicle(int playerid, int vehicleid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerInAnyVehicle(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerInCheckpoint(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerInRaceCheckpoint(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetPlayerVirtualWorld(int playerid, int worldid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerVirtualWorld(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool EnableStuntBonusForPlayer(int playerid, bool enable)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool TogglePlayerSpectating(int playerid, bool toggle)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerSpectatePlayer(int playerid, int targetplayerid, int mode)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool PlayerSpectateVehicle(int playerid, int targetvehicleid, int mode)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool StartRecordingPlayerData(int playerid, int recordtype, string recordname)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool StopRecordingPlayerData(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool CreateExplosionForPlayer(int playerid, float x, float y, float z, int type, float radius)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SelectTextDraw(int playerid, int hovercolor)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool CancelSelectTextDraw(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SendClientMessage(int playerid, int color, string message)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SendClientMessageToAll(int color, string message)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SendPlayerMessageToPlayer(int playerid, int senderid, string message)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SendPlayerMessageToAll(int senderid, string message)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SendDeathMessage(int killer, int killee, int weapon)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SendDeathMessageToPlayer(int playerid, int killer, int killee, int weapon)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GameTextForAll(string text, int time, int style)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GameTextForPlayer(int playerid, string text, int time, int style)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool Kick(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool Ban(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool BanEx(int playerid, string reason)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool CreateExplosion(float x, float y, float z, int type, float radius)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerNPC(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool IsPlayerAdmin(int playerid)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int GetPlayerPoolSize()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerVersion(int playerid, out string version, int len)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerNetworkStats(int playerid, out string retstr, int size)
            {
                throw new NativeNotImplementedException();
            }


            [NativeMethod(Function = "gpci")]
            public virtual bool GPCI(int playerid, out string buffer, int size)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GetPlayerLastShotVectors(int playerid, out float fOriginX, out float fOriginY,
                out float fOriginZ, out float fHitPosX, out float fHitPosY, out float fHitPosZ)
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}