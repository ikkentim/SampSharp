// SampSharp
// Copyright 2015 Tim Potze
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

using System.Runtime.CompilerServices;

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetSpawnInfo(int playerid, int team, int skin, float x, float y, float z,
            float rotation, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SpawnPlayer(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerPos(int playerid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerPosFindZ(int playerid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerPos(int playerid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerFacingAngle(int playerid, float angle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerFacingAngle(int playerid, out float angle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInRangeOfPoint(int playerid, float range, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float GetPlayerDistanceFromPoint(int playerid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerStreamedIn(int playerid, int forplayerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerInterior(int playerid, int interiorid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerInterior(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerHealth(int playerid, float health);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerHealth(int playerid, out float health);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerArmour(int playerid, float armour);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerArmour(int playerid, out float armour);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerAmmo(int playerid, int weaponid, int ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerAmmo(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerWeaponState(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerTargetPlayer(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerTargetActor(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerTeam(int playerid, int teamid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerTeam(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerScore(int playerid, int score);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerScore(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerDrunkLevel(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerDrunkLevel(int playerid, int level);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerColor(int playerid, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerColor(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerSkin(int playerid, int skinid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSkin(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GivePlayerWeapon(int playerid, int weaponid, int ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ResetPlayerWeapons(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerArmedWeapon(int playerid, int weaponid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerWeaponData(int playerid, int slot, out int weapon, out int ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GivePlayerMoney(int playerid, int money);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ResetPlayerMoney(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int SetPlayerName(int playerid, string name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerMoney(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerState(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerIp(int playerid, out string ip, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerPing(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerWeapon(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerKeys(int playerid, out int keys, out int updown, out int leftright);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerName(int playerid, out string name, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerTime(int playerid, int hour, int minute);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerTime(int playerid, out int hour, out int minute);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TogglePlayerClock(int playerid, bool toggle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerWeather(int playerid, int weather);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ForceClassSelection(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerWantedLevel(int playerid, int level);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerWantedLevel(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerFightingStyle(int playerid, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerFightingStyle(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerVelocity(int playerid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerVelocity(int playerid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayCrimeReportForPlayer(int playerid, int suspectid, int crime);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayAudioStreamForPlayer(int playerid, string url, float posX, float posY, float posZ,
            float distance, bool usepos);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopAudioStreamForPlayer(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerShopName(int playerid, string shopname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerSkillLevel(int playerid, int skill, int level);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSurfingVehicleID(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSurfingObjectID(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemoveBuildingForPlayer(int playerid, int modelid, float x, float y, float z,
            float radius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerAttachedObject(int playerid, int index, int modelid, int bone, float offsetX,
            float offsetY, float offsetZ, float rotX, float rotY, float rotZ, float scaleX, float scaleY,
            float scaleZ, int materialcolor1, int materialcolor2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemovePlayerAttachedObject(int playerid, int index);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerAttachedObjectSlotUsed(int playerid, int index);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EditAttachedObject(int playerid, int index);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePlayerTextDraw(int playerid, float x, float y, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawDestroy(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawLetterSize(int playerid, int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawTextSize(int playerid, int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawAlignment(int playerid, int text, int alignment);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawColor(int playerid, int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawUseBox(int playerid, int text, bool use);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawBoxColor(int playerid, int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetShadow(int playerid, int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetOutline(int playerid, int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawBackgroundColor(int playerid, int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawFont(int playerid, int text, int font);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetProportional(int playerid, int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetSelectable(int playerid, int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawShow(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawHide(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetString(int playerid, int text, string str);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetPreviewModel(int playerid, int text, int modelindex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetPreviewRot(int playerid, int text, float rotX, float rotY,
            float rotZ, float zoom);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetPreviewVehCol(int playerid, int text, int color1, int color2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPVarInt(int playerid, string varname, int value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPVarInt(int playerid, string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPVarString(int playerid, string varname, string value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPVarString(int playerid, string varname, out string value, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPVarFloat(int playerid, string varname, float value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float GetPVarFloat(int playerid, string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DeletePVar(int playerid, string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPVarsUpperIndex(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPVarNameAtIndex(int playerid, int index, out string varname, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPVarType(int playerid, string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerChatBubble(int playerid, string text, int color, float drawdistance,
            int expiretime);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PutPlayerInVehicle(int playerid, int vehicleid, int seatid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerVehicleID(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerVehicleSeat(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemovePlayerFromVehicle(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TogglePlayerControllable(int playerid, bool toggle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerPlaySound(int playerid, int soundid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ApplyAnimation(int playerid, string animlib, string animname, float fDelta, bool loop,
            bool lockx, bool locky, bool freeze, int time, bool forcesync);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ClearAnimations(int playerid, bool forcesync);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerAnimationIndex(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetAnimationName(int index, out string animlib, int animlibSize, out string animname,
            int animnameSize);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSpecialAction(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerSpecialAction(int playerid, int actionid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableRemoteVehicleCollisions(int playerid, bool disable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerCheckpoint(int playerid, float x, float y, float z, float size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisablePlayerCheckpoint(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerRaceCheckpoint(int playerid, int type, float x, float y, float z, float nextx,
            float nexty, float nextz, float size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisablePlayerRaceCheckpoint(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerWorldBounds(int playerid, float xMax, float xMin, float yMax, float yMin);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerMarkerForPlayer(int playerid, int showplayerid, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerNameTagForPlayer(int playerid, int showplayerid, bool show);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerMapIcon(int playerid, int iconid, float x, float y, float z, int markertype,
            int color, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemovePlayerMapIcon(int playerid, int iconid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerCameraPos(int playerid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerCameraLookAt(int playerid, float x, float y, float z, int cut);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetCameraBehindPlayer(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerCameraPos(int playerid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerCameraFrontVector(int playerid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerCameraMode(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnablePlayerCameraTarget(int playerid, bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerCameraTargetObject(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerCameraTargetVehicle(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerCameraTargetPlayer(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerCameraTargetActor(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachCameraToObject(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachCameraToPlayerObject(int playerid, int playerobjectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool InterpolateCameraPos(int playerid, float fromX, float fromY, float fromZ, float toX,
            float toY, float toZ, int time, int cut);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool InterpolateCameraLookAt(int playerid, float fromX, float fromY, float fromZ, float toX,
            float toY, float toZ, int time, int cut);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerConnected(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInVehicle(int playerid, int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInAnyVehicle(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInCheckpoint(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInRaceCheckpoint(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerVirtualWorld(int playerid, int worldid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerVirtualWorld(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableStuntBonusForPlayer(int playerid, bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableStuntBonusForAll(bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TogglePlayerSpectating(int playerid, bool toggle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerSpectatePlayer(int playerid, int targetplayerid, int mode);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerSpectateVehicle(int playerid, int targetvehicleid, int mode);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StartRecordingPlayerData(int playerid, int recordtype, string recordname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopRecordingPlayerData(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CreateExplosionForPlayer(int playerid, float x, float y, float z, int type,
            float radius);
    }
}