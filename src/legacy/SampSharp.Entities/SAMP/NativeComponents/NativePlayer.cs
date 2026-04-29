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

using System.Diagnostics.CodeAnalysis;
using SampSharp.Core.Natives.NativeObjects;

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class NativePlayer : BaseNativeComponent
{
    /// <summary>Identifier indicating the handle is invalid.</summary>
    public const int InvalidId = 0xFFFF;

    [NativeMethod]
    public virtual bool SetSpawnInfo(int team, int skin, float x, float y, float z, float rotation, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo,
        int weapon3, int weapon3Ammo)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SpawnPlayer()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerPos(float x, float y, float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerPosFindZ(float x, float y, float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerPos(out float x, out float y, out float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerFacingAngle(float angle)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerFacingAngle(out float angle)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsPlayerInRangeOfPoint(float range, float x, float y, float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual float GetPlayerDistanceFromPoint(float x, float y, float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(IdentifiersIndex = 1)]
    public virtual bool IsPlayerStreamedIn(int forplayerid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerInterior(int interiorid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerInterior()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerHealth(float health)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerHealth(out float health)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerArmour(float armour)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerArmour(out float armour)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerAmmo(int weaponid, int ammo)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerAmmo()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerWeaponState()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerTargetPlayer()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerTargetActor()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerTeam(int teamid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerTeam()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerScore(int score)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerScore()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerDrunkLevel()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerDrunkLevel(int level)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerColor(int color)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerColor()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerSkin(int skinid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerSkin()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GivePlayerWeapon(int weaponid, int ammo)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool ResetPlayerWeapons()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerArmedWeapon(int weaponid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerWeaponData(int slot, out int weapon, out int ammo)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GivePlayerMoney(int money)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool ResetPlayerMoney()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int SetPlayerName(string name)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerMoney()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerState()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerIp(out string ip, int size)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerPing()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerWeapon()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerKeys(out int keys, out int updown, out int leftright)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerName(out string name, int size)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerTime(int hour, int minute)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerTime(out int hour, out int minute)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool TogglePlayerClock(bool toggle)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerWeather(int weather)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool ForceClassSelection()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerWantedLevel(int level)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerWantedLevel()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerFightingStyle(int style)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerFightingStyle()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerVelocity(float x, float y, float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerVelocity(out float x, out float y, out float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool PlayCrimeReportForPlayer(int suspectid, int crime)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool PlayAudioStreamForPlayer(string url, float posX, float posY, float posZ, float distance, bool usepos)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool StopAudioStreamForPlayer()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerShopName(string shopname)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerSkillLevel(int skill, int level)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerSurfingVehicleID()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerSurfingObjectID()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerAttachedObject(int index, int modelid, int bone, float offsetX, float offsetY, float offsetZ, float rotX, float rotY,
        float rotZ, float scaleX, float scaleY, float scaleZ, int materialcolor1, int materialcolor2)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool RemovePlayerAttachedObject(int index)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsPlayerAttachedObjectSlotUsed(int index)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool EditAttachedObject(int index)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerChatBubble(string text, int color, float drawdistance, int expiretime)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool PutPlayerInVehicle(int vehicleid, int seatid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerVehicleID()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerVehicleSeat()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool RemovePlayerFromVehicle()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool TogglePlayerControllable(bool toggle)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool PlayerPlaySound(int soundid, float x, float y, float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool ApplyAnimation(string animlib, string animname, float fDelta, bool loop, bool lockx, bool locky, bool freeze, int time, bool forcesync)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool ClearAnimations(bool forcesync)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerAnimationIndex()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerSpecialAction()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerSpecialAction(int actionid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool DisableRemoteVehicleCollisions(bool disable)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerCheckpoint(float x, float y, float z, float size)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool DisablePlayerCheckpoint()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerRaceCheckpoint(int type, float x, float y, float z, float nextx, float nexty, float nextz, float size)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool DisablePlayerRaceCheckpoint()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerWorldBounds(float xMax, float xMin, float yMax, float yMin)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerMarkerForPlayer(int showplayerid, int color)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool ShowPlayerNameTagForPlayer(int showplayerid, bool show)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerCameraPos(float x, float y, float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerCameraLookAt(float x, float y, float z, int cut)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetCameraBehindPlayer()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerCameraPos(out float x, out float y, out float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerCameraFrontVector(out float x, out float y, out float z)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerCameraMode()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool EnablePlayerCameraTarget(bool enable)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerCameraTargetObject()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerCameraTargetVehicle()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerCameraTargetPlayer()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerCameraTargetActor()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool InterpolateCameraPos(float fromX, float fromY, float fromZ, float toX, float toY, float toZ, int time, int cut)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool InterpolateCameraLookAt(float fromX, float fromY, float fromZ, float toX, float toY, float toZ, int time, int cut)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsPlayerConnected()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsPlayerInVehicle(int vehicleid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsPlayerInAnyVehicle()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsPlayerInCheckpoint()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsPlayerInRaceCheckpoint()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerVirtualWorld(int worldid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerVirtualWorld()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool EnableStuntBonusForPlayer(bool enable)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool TogglePlayerSpectating(bool toggle)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool PlayerSpectatePlayer(int targetplayerid, int mode)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool PlayerSpectateVehicle(int targetvehicleid, int mode)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool StartRecordingPlayerData(int recordtype, string recordname)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool StopRecordingPlayerData()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool CreateExplosionForPlayer(float x, float y, float z, int type, float radius)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SelectTextDraw(int hovercolor)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool CancelSelectTextDraw()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool CancelEdit()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SendClientMessage(int color, string message)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SendDeathMessageToPlayer(int killer, int killee, int weapon)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GameTextForPlayer(string text, int time, int style)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool Kick()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool Ban()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool BanEx(string reason)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsPlayerNPC()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool IsPlayerAdmin()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerVersion(out string version, int len)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerNetworkStats(out string retstr, int size)
    {
        throw new NativeNotImplementedException();
    }


    [NativeMethod(Function = "gpci")]
    public virtual bool GPCI(out string buffer, int size)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetPlayerLastShotVectors(out float fOriginX, out float fOriginY, out float fOriginZ, out float fHitPosX, out float fHitPosY,
        out float fHitPosZ)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(Function = "NetStats_GetConnectedTime")]
    public virtual int GetConnectedTime()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(Function = "NetStats_MessagesReceived")]
    public virtual int GetMessagesReceived()
    {
        throw new NativeNotImplementedException();
    }

    // Another name, because on behalf of the player
    [NativeMethod(Function = "NetStats_BytesReceived")]
    public virtual int GetBytesSent()
    {
        throw new NativeNotImplementedException();
    }

    // Another name, because on behalf of the player
    [NativeMethod(Function = "NetStats_BytesSent")]
    public virtual int GetBytesReceived()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(Function = "NetStats_MessagesSent")]
    public virtual int GetMessagesSent()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(Function = "NetStats_MessagesRecvPerSecond")]
    public virtual int GetMessagesReceivedPerSecond()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(Function = "NetStats_PacketLossPercent")]
    public virtual float GetPacketLossPercent()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(Function = "NetStats_ConnectionStatus")]
    public virtual int GetConnectionStatus()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(Function = "NetStats_GetIpPort")]
    public virtual int GetIpPort(out string ipPort, int ipPortLen)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool EditObject(int objectid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool EditPlayerObject(int objectid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SelectObject()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool RemoveBuildingForPlayer(int modelid, float x, float y, float z, float radius)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool AttachCameraToObject(int objectid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool AttachCameraToPlayerObject(int playerobjectid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SendPlayerMessageToPlayer(int senderid, string message)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod(true)]
    public virtual bool GetAnimationName(int index, out string animlib, int animlibSize, out string animname, int animnameSize)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual void ShowPlayerDialog(int dialogid, int style, string caption, string info, string button1, string button2)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetPlayerMapIcon(int iconid, float x, float y, float z, int markertype, int color, int style)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool RemovePlayerMapIcon(int iconid)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual float GetPlayerCameraAspectRatio()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual float GetPlayerCameraZoom()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerMenu()
    {
        throw new NativeNotImplementedException();
    }
}