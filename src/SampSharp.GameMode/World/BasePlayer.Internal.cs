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

using SampSharp.GameMode.API;

namespace SampSharp.GameMode.World
{
    public partial class BasePlayer
    {
        private static class Internal
        {
            public delegate bool ApplyAnimationImpl(
                int playerid, string animlib, string animname, float fDelta, bool loop,
                bool lockx, bool locky, bool freeze, int time, bool forcesync);

            public delegate bool BanExImpl(int playerid, string reason);

            public delegate bool BanImpl(int playerid);

            public delegate bool CancelSelectTextDrawImpl(int playerid);

            public delegate bool ClearAnimationsImpl(int playerid, bool forcesync);

            public delegate bool CreateExplosionForPlayerImpl(int playerid, float x, float y, float z, int type,
                float radius);

            public delegate bool CreateExplosionImpl(float x, float y, float z, int type, float radius);

            public delegate bool DisablePlayerCheckpointImpl(int playerid);

            public delegate bool DisablePlayerRaceCheckpointImpl(int playerid);

            public delegate bool DisableRemoteVehicleCollisionsImpl(int playerid, bool disable);

            public delegate bool EditAttachedObjectImpl(int playerid, int index);

            public delegate bool EnablePlayerCameraTargetImpl(int playerid, bool enable);

            public delegate bool EnableStuntBonusForPlayerImpl(int playerid, bool enable);

            public delegate bool ForceClassSelectionImpl(int playerid);

            public delegate bool GameTextForAllImpl(string text, int time, int style);

            public delegate bool GameTextForPlayerImpl(int playerid, string text, int time, int style);

            public delegate bool GetAnimationNameImpl(
                int index, out string animlib, int animlibSize, out string animname,
                int animnameSize);

            public delegate int GetPlayerAmmoImpl(int playerid);

            public delegate int GetPlayerAnimationIndexImpl(int playerid);

            public delegate bool GetPlayerArmourImpl(int playerid, out float armour);

            public delegate bool GetPlayerCameraFrontVectorImpl(int playerid, out float x, out float y, out float z);

            public delegate int GetPlayerCameraModeImpl(int playerid);

            public delegate bool GetPlayerCameraPosImpl(int playerid, out float x, out float y, out float z);

            public delegate int GetPlayerCameraTargetActorImpl(int playerid);

            public delegate int GetPlayerCameraTargetObjectImpl(int playerid);

            public delegate int GetPlayerCameraTargetPlayerImpl(int playerid);

            public delegate int GetPlayerCameraTargetVehicleImpl(int playerid);

            public delegate int GetPlayerColorImpl(int playerid);

            public delegate float GetPlayerDistanceFromPointImpl(int playerid, float x, float y, float z);

            public delegate int GetPlayerDrunkLevelImpl(int playerid);

            public delegate bool GetPlayerFacingAngleImpl(int playerid, out float angle);

            public delegate int GetPlayerFightingStyleImpl(int playerid);

            public delegate bool GetPlayerHealthImpl(int playerid, out float health);

            public delegate int GetPlayerInteriorImpl(int playerid);

            public delegate bool GetPlayerIpImpl(int playerid, out string ip, int size);

            public delegate bool GetPlayerKeysImpl(int playerid, out int keys, out int updown, out int leftright);

            public delegate int GetPlayerMoneyImpl(int playerid);

            public delegate int GetPlayerNameImpl(int playerid, out string name, int size);

            public delegate bool GetPlayerNetworkStatsImpl(int playerid, out string retstr, int size);

            public delegate int GetPlayerPingImpl(int playerid);

            public delegate int GetPlayerPoolSizeImpl();

            public delegate bool GetPlayerPosImpl(int playerid, out float x, out float y, out float z);

            public delegate int GetPlayerScoreImpl(int playerid);

            public delegate int GetPlayerSkinImpl(int playerid);

            public delegate int GetPlayerSpecialActionImpl(int playerid);

            public delegate int GetPlayerStateImpl(int playerid);

            public delegate int GetPlayerSurfingObjectIDImpl(int playerid);

            public delegate int GetPlayerSurfingVehicleIDImpl(int playerid);

            public delegate int GetPlayerTargetActorImpl(int playerid);

            public delegate int GetPlayerTargetPlayerImpl(int playerid);

            public delegate int GetPlayerTeamImpl(int playerid);

            public delegate bool GetPlayerTimeImpl(int playerid, out int hour, out int minute);

            public delegate int GetPlayerVehicleIDImpl(int playerid);

            public delegate int GetPlayerVehicleSeatImpl(int playerid);

            public delegate bool GetPlayerVelocityImpl(int playerid, out float x, out float y, out float z);

            public delegate bool GetPlayerVersionImpl(int playerid, out string version, int len);

            public delegate int GetPlayerVirtualWorldImpl(int playerid);

            public delegate int GetPlayerWantedLevelImpl(int playerid);

            public delegate bool GetPlayerWeaponDataImpl(int playerid, int slot, out int weapon, out int ammo);

            public delegate int GetPlayerWeaponImpl(int playerid);

            public delegate int GetPlayerWeaponStateImpl(int playerid);

            public delegate bool GivePlayerMoneyImpl(int playerid, int money);

            public delegate bool GivePlayerWeaponImpl(int playerid, int weaponid, int ammo);

            public delegate bool InterpolateCameraLookAtImpl(
                int playerid, float fromX, float fromY, float fromZ, float toX,
                float toY, float toZ, int time, int cut);

            public delegate bool InterpolateCameraPosImpl(
                int playerid, float fromX, float fromY, float fromZ, float toX,
                float toY, float toZ, int time, int cut);

            public delegate bool IsPlayerAdminImpl(int playerid);

            public delegate bool IsPlayerAttachedObjectSlotUsedImpl(int playerid, int index);

            public delegate bool IsPlayerConnectedImpl(int playerid);

            public delegate bool IsPlayerInAnyVehicleImpl(int playerid);

            public delegate bool IsPlayerInCheckpointImpl(int playerid);

            public delegate bool IsPlayerInRaceCheckpointImpl(int playerid);

            public delegate bool IsPlayerInRangeOfPointImpl(int playerid, float range, float x, float y, float z);

            public delegate bool IsPlayerInVehicleImpl(int playerid, int vehicleid);

            public delegate bool IsPlayerNPCImpl(int playerid);

            public delegate bool IsPlayerStreamedInImpl(int playerid, int forplayerid);

            public delegate bool KickImpl(int playerid);

            public delegate bool PlayAudioStreamForPlayerImpl(
                int playerid, string url, float posX, float posY, float posZ,
                float distance, bool usepos);

            public delegate bool PlayCrimeReportForPlayerImpl(int playerid, int suspectid, int crime);

            public delegate bool PlayerPlaySoundImpl(int playerid, int soundid, float x, float y, float z);

            public delegate bool PlayerSpectatePlayerImpl(int playerid, int targetplayerid, int mode);

            public delegate bool PlayerSpectateVehicleImpl(int playerid, int targetvehicleid, int mode);

            public delegate bool PutPlayerInVehicleImpl(int playerid, int vehicleid, int seatid);

            public delegate bool RemovePlayerAttachedObjectImpl(int playerid, int index);

            public delegate bool RemovePlayerFromVehicleImpl(int playerid);

            public delegate bool RemovePlayerMapIconImpl(int playerid, int iconid);

            public delegate bool ResetPlayerMoneyImpl(int playerid);

            public delegate bool ResetPlayerWeaponsImpl(int playerid);

            public delegate bool SelectTextDrawImpl(int playerid, int hovercolor);

            public delegate bool SendClientMessageImpl(int playerid, int color, string message);

            public delegate bool SendClientMessageToAllImpl(int color, string message);

            public delegate bool SendDeathMessageImpl(int killer, int killee, int weapon);

            public delegate bool SendDeathMessageToPlayerImpl(int playerid, int killer, int killee, int weapon);

            public delegate bool SendPlayerMessageToAllImpl(int senderid, string message);

            public delegate bool SendPlayerMessageToPlayerImpl(int playerid, int senderid, string message);

            public delegate bool SetCameraBehindPlayerImpl(int playerid);

            public delegate bool SetPlayerAmmoImpl(int playerid, int weaponid, int ammo);

            public delegate bool SetPlayerArmedWeaponImpl(int playerid, int weaponid);

            public delegate bool SetPlayerArmourImpl(int playerid, float armour);

            public delegate bool SetPlayerAttachedObjectImpl(
                int playerid, int index, int modelid, int bone, float offsetX,
                float offsetY, float offsetZ, float rotX, float rotY, float rotZ, float scaleX, float scaleY,
                float scaleZ, int materialcolor1, int materialcolor2);

            public delegate bool SetPlayerCameraLookAtImpl(int playerid, float x, float y, float z, int cut);

            public delegate bool SetPlayerCameraPosImpl(int playerid, float x, float y, float z);

            public delegate bool SetPlayerChatBubbleImpl(int playerid, string text, int color, float drawdistance,
                int expiretime);

            public delegate bool SetPlayerCheckpointImpl(int playerid, float x, float y, float z, float size);

            public delegate bool SetPlayerColorImpl(int playerid, int color);

            public delegate bool SetPlayerDrunkLevelImpl(int playerid, int level);

            public delegate bool SetPlayerFacingAngleImpl(int playerid, float angle);

            public delegate bool SetPlayerFightingStyleImpl(int playerid, int style);

            public delegate bool SetPlayerHealthImpl(int playerid, float health);

            public delegate bool SetPlayerInteriorImpl(int playerid, int interiorid);

            public delegate bool SetPlayerMapIconImpl(
                int playerid, int iconid, float x, float y, float z, int markertype,
                int color, int style);

            public delegate bool SetPlayerMarkerForPlayerImpl(int playerid, int showplayerid, int color);

            public delegate int SetPlayerNameImpl(int playerid, string name);

            public delegate bool SetPlayerPosFindZImpl(int playerid, float x, float y, float z);

            public delegate bool SetPlayerPosImpl(int playerid, float x, float y, float z);

            public delegate bool SetPlayerRaceCheckpointImpl(
                int playerid, int type, float x, float y, float z, float nextx,
                float nexty, float nextz, float size);

            public delegate bool SetPlayerScoreImpl(int playerid, int score);

            public delegate bool SetPlayerShopNameImpl(int playerid, string shopname);

            public delegate bool SetPlayerSkillLevelImpl(int playerid, int skill, int level);

            public delegate bool SetPlayerSkinImpl(int playerid, int skinid);

            public delegate bool SetPlayerSpecialActionImpl(int playerid, int actionid);

            public delegate bool SetPlayerTeamImpl(int playerid, int teamid);

            public delegate bool SetPlayerTimeImpl(int playerid, int hour, int minute);

            public delegate bool SetPlayerVelocityImpl(int playerid, float x, float y, float z);

            public delegate bool SetPlayerVirtualWorldImpl(int playerid, int worldid);

            public delegate bool SetPlayerWantedLevelImpl(int playerid, int level);

            public delegate bool SetPlayerWeatherImpl(int playerid, int weather);

            public delegate bool SetPlayerWorldBoundsImpl(int playerid, float xMax, float xMin, float yMax, float yMin);

            public delegate bool SetSpawnInfoImpl(int playerid, int team, int skin, float x, float y, float z,
                float rotation, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo
                );

            public delegate bool ShowPlayerNameTagForPlayerImpl(int playerid, int showplayerid, bool show);

            public delegate bool SpawnPlayerImpl(int playerid);

            public delegate bool StartRecordingPlayerDataImpl(int playerid, int recordtype, string recordname);

            public delegate bool StopAudioStreamForPlayerImpl(int playerid);

            public delegate bool StopRecordingPlayerDataImpl(int playerid);

            public delegate bool TogglePlayerClockImpl(int playerid, bool toggle);

            public delegate bool TogglePlayerControllableImpl(int playerid, bool toggle);

            public delegate bool TogglePlayerSpectatingImpl(int playerid, bool toggle);

            public delegate bool GPCIImpl(int playerid, out string buffer, int size);


            [Native("SetSpawnInfo")] public static readonly SetSpawnInfoImpl NativeSetSpawnInfo = null;
            [Native("SpawnPlayer")] public static readonly SpawnPlayerImpl SpawnPlayer = null;
            [Native("SetPlayerPos")] public static readonly SetPlayerPosImpl SetPlayerPos = null;
            [Native("SetPlayerPosFindZ")] public static readonly SetPlayerPosFindZImpl SetPlayerPosFindZ = null;
            [Native("GetPlayerPos")] public static readonly GetPlayerPosImpl GetPlayerPos = null;
            [Native("SetPlayerFacingAngle")] public static readonly SetPlayerFacingAngleImpl SetPlayerFacingAngle = null;
            [Native("GetPlayerFacingAngle")] public static readonly GetPlayerFacingAngleImpl GetPlayerFacingAngle = null;

            [Native("IsPlayerInRangeOfPoint")] public static readonly IsPlayerInRangeOfPointImpl IsPlayerInRangeOfPoint
                =
                null;

            [Native("GetPlayerDistanceFromPoint")] public static readonly GetPlayerDistanceFromPointImpl
                GetPlayerDistanceFromPoint = null;

            [Native("IsPlayerStreamedIn")] public static readonly IsPlayerStreamedInImpl NativeIsPlayerStreamedIn = null;
            [Native("SetPlayerInterior")] public static readonly SetPlayerInteriorImpl SetPlayerInterior = null;
            [Native("GetPlayerInterior")] public static readonly GetPlayerInteriorImpl GetPlayerInterior = null;
            [Native("SetPlayerHealth")] public static readonly SetPlayerHealthImpl SetPlayerHealth = null;
            [Native("GetPlayerHealth")] public static readonly GetPlayerHealthImpl GetPlayerHealth = null;
            [Native("SetPlayerArmour")] public static readonly SetPlayerArmourImpl SetPlayerArmour = null;
            [Native("GetPlayerArmour")] public static readonly GetPlayerArmourImpl GetPlayerArmour = null;
            [Native("SetPlayerAmmo")] public static readonly SetPlayerAmmoImpl SetPlayerAmmo = null;
            [Native("GetPlayerAmmo")] public static readonly GetPlayerAmmoImpl GetPlayerAmmo = null;
            [Native("GetPlayerWeaponState")] public static readonly GetPlayerWeaponStateImpl GetPlayerWeaponState = null;

            [Native("GetPlayerTargetPlayer")] public static readonly GetPlayerTargetPlayerImpl GetPlayerTargetPlayer =
                null;

            [Native("GetPlayerTargetActor")] public static readonly GetPlayerTargetActorImpl GetPlayerTargetActor = null;
            [Native("SetPlayerTeam")] public static readonly SetPlayerTeamImpl SetPlayerTeam = null;
            [Native("GetPlayerTeam")] public static readonly GetPlayerTeamImpl GetPlayerTeam = null;
            [Native("SetPlayerScore")] public static readonly SetPlayerScoreImpl SetPlayerScore = null;
            [Native("GetPlayerScore")] public static readonly GetPlayerScoreImpl GetPlayerScore = null;
            [Native("GetPlayerDrunkLevel")] public static readonly GetPlayerDrunkLevelImpl GetPlayerDrunkLevel = null;
            [Native("SetPlayerDrunkLevel")] public static readonly SetPlayerDrunkLevelImpl SetPlayerDrunkLevel = null;
            [Native("SetPlayerColor")] public static readonly SetPlayerColorImpl SetPlayerColor = null;
            [Native("GetPlayerColor")] public static readonly GetPlayerColorImpl GetPlayerColor = null;
            [Native("SetPlayerSkin")] public static readonly SetPlayerSkinImpl SetPlayerSkin = null;
            [Native("GetPlayerSkin")] public static readonly GetPlayerSkinImpl GetPlayerSkin = null;
            [Native("GivePlayerWeapon")] public static readonly GivePlayerWeaponImpl GivePlayerWeapon = null;
            [Native("ResetPlayerWeapons")] public static readonly ResetPlayerWeaponsImpl ResetPlayerWeapons = null;
            [Native("SetPlayerArmedWeapon")] public static readonly SetPlayerArmedWeaponImpl SetPlayerArmedWeapon = null;
            [Native("GetPlayerWeaponData")] public static readonly GetPlayerWeaponDataImpl GetPlayerWeaponData = null;
            [Native("GivePlayerMoney")] public static readonly GivePlayerMoneyImpl GivePlayerMoney = null;
            [Native("ResetPlayerMoney")] public static readonly ResetPlayerMoneyImpl ResetPlayerMoney = null;
            [Native("SetPlayerName")] public static readonly SetPlayerNameImpl SetPlayerName = null;
            [Native("GetPlayerMoney")] public static readonly GetPlayerMoneyImpl GetPlayerMoney = null;
            [Native("GetPlayerState")] public static readonly GetPlayerStateImpl GetPlayerState = null;
            [Native("GetPlayerIp")] public static readonly GetPlayerIpImpl GetPlayerIp = null;
            [Native("GetPlayerPing")] public static readonly GetPlayerPingImpl GetPlayerPing = null;
            [Native("GetPlayerWeapon")] public static readonly GetPlayerWeaponImpl GetPlayerWeapon = null;
            [Native("GetPlayerKeys")] public static readonly GetPlayerKeysImpl GetPlayerKeys = null;
            [Native("GetPlayerName")] public static readonly GetPlayerNameImpl GetPlayerName = null;
            [Native("SetPlayerTime")] public static readonly SetPlayerTimeImpl SetPlayerTime = null;
            [Native("GetPlayerTime")] public static readonly GetPlayerTimeImpl GetPlayerTime = null;
            [Native("TogglePlayerClock")] public static readonly TogglePlayerClockImpl TogglePlayerClock = null;
            [Native("SetPlayerWeather")] public static readonly SetPlayerWeatherImpl SetPlayerWeather = null;

            [Native("ForceClassSelection")] public static readonly ForceClassSelectionImpl NativeForceClassSelection =
                null;

            [Native("SetPlayerWantedLevel")] public static readonly SetPlayerWantedLevelImpl SetPlayerWantedLevel = null;
            [Native("GetPlayerWantedLevel")] public static readonly GetPlayerWantedLevelImpl GetPlayerWantedLevel = null;

            [Native("SetPlayerFightingStyle")] public static readonly SetPlayerFightingStyleImpl SetPlayerFightingStyle
                =
                null;

            [Native("GetPlayerFightingStyle")] public static readonly GetPlayerFightingStyleImpl GetPlayerFightingStyle
                =
                null;

            [Native("SetPlayerVelocity")] public static readonly SetPlayerVelocityImpl SetPlayerVelocity = null;
            [Native("GetPlayerVelocity")] public static readonly GetPlayerVelocityImpl GetPlayerVelocity = null;

            [Native("PlayCrimeReportForPlayer")] public static readonly PlayCrimeReportForPlayerImpl
                PlayCrimeReportForPlayer = null;

            [Native("PlayAudioStreamForPlayer")] public static readonly PlayAudioStreamForPlayerImpl
                PlayAudioStreamForPlayer = null;

            [Native("StopAudioStreamForPlayer")] public static readonly StopAudioStreamForPlayerImpl
                StopAudioStreamForPlayer = null;

            [Native("SetPlayerShopName")] public static readonly SetPlayerShopNameImpl SetPlayerShopName = null;
            [Native("SetPlayerSkillLevel")] public static readonly SetPlayerSkillLevelImpl SetPlayerSkillLevel = null;

            [Native("GetPlayerSurfingVehicleID")] public static readonly GetPlayerSurfingVehicleIDImpl
                GetPlayerSurfingVehicleID = null;

            [Native("GetPlayerSurfingObjectID")] public static readonly GetPlayerSurfingObjectIDImpl
                GetPlayerSurfingObjectID = null;

            [Native("SetPlayerAttachedObject")] public static readonly SetPlayerAttachedObjectImpl
                SetPlayerAttachedObject =
                    null;

            [Native("RemovePlayerAttachedObject")] public static readonly RemovePlayerAttachedObjectImpl
                RemovePlayerAttachedObject = null;

            [Native("IsPlayerAttachedObjectSlotUsed")] public static readonly IsPlayerAttachedObjectSlotUsedImpl
                IsPlayerAttachedObjectSlotUsed = null;

            [Native("EditAttachedObject")] public static readonly EditAttachedObjectImpl NativeEditAttachedObject = null;

            [Native("SetPlayerChatBubble")] public static readonly SetPlayerChatBubbleImpl SetPlayerChatBubble = null;
            [Native("PutPlayerInVehicle")] public static readonly PutPlayerInVehicleImpl PutPlayerInVehicle = null;
            [Native("GetPlayerVehicleID")] public static readonly GetPlayerVehicleIDImpl GetPlayerVehicleID = null;
            [Native("GetPlayerVehicleSeat")] public static readonly GetPlayerVehicleSeatImpl GetPlayerVehicleSeat = null;

            [Native("RemovePlayerFromVehicle")] public static readonly RemovePlayerFromVehicleImpl
                RemovePlayerFromVehicle =
                    null;

            [Native("TogglePlayerControllable")] public static readonly TogglePlayerControllableImpl
                TogglePlayerControllable = null;

            [Native("PlayerPlaySound")] public static readonly PlayerPlaySoundImpl PlayerPlaySound = null;
            [Native("ApplyAnimation")] public static readonly ApplyAnimationImpl NativeApplyAnimation = null;
            [Native("ClearAnimations")] public static readonly ClearAnimationsImpl NativeClearAnimations = null;

            [Native("GetPlayerAnimationIndex")] public static readonly GetPlayerAnimationIndexImpl
                GetPlayerAnimationIndex =
                    null;

            [Native("GetAnimationName")] public static readonly GetAnimationNameImpl NativeGetAnimationName = null;

            [Native("GetPlayerSpecialAction")] public static readonly GetPlayerSpecialActionImpl GetPlayerSpecialAction
                =
                null;

            [Native("SetPlayerSpecialAction")] public static readonly SetPlayerSpecialActionImpl SetPlayerSpecialAction
                =
                null;

            [Native("DisableRemoteVehicleCollisions")] public static readonly DisableRemoteVehicleCollisionsImpl
                NativeDisableRemoteVehicleCollisions = null;

            [Native("SetPlayerCheckpoint")] public static readonly SetPlayerCheckpointImpl SetPlayerCheckpoint = null;

            [Native("DisablePlayerCheckpoint")] public static readonly DisablePlayerCheckpointImpl
                DisablePlayerCheckpoint =
                    null;

            [Native("SetPlayerRaceCheckpoint")] public static readonly SetPlayerRaceCheckpointImpl
                SetPlayerRaceCheckpoint =
                    null;

            [Native("DisablePlayerRaceCheckpoint")] public static readonly DisablePlayerRaceCheckpointImpl
                DisablePlayerRaceCheckpoint = null;

            [Native("SetPlayerWorldBounds")] public static readonly SetPlayerWorldBoundsImpl SetPlayerWorldBounds = null;

            [Native("SetPlayerMarkerForPlayer")] public static readonly SetPlayerMarkerForPlayerImpl
                SetPlayerMarkerForPlayer = null;

            [Native("ShowPlayerNameTagForPlayer")] public static readonly ShowPlayerNameTagForPlayerImpl
                ShowPlayerNameTagForPlayer = null;

            [Native("SetPlayerMapIcon")] public static readonly SetPlayerMapIconImpl SetPlayerMapIcon = null;
            [Native("RemovePlayerMapIcon")] public static readonly RemovePlayerMapIconImpl RemovePlayerMapIcon = null;
            [Native("SetPlayerCameraPos")] public static readonly SetPlayerCameraPosImpl SetPlayerCameraPos = null;

            [Native("SetPlayerCameraLookAt")] public static readonly SetPlayerCameraLookAtImpl SetPlayerCameraLookAt =
                null;

            [Native("SetCameraBehindPlayer")] public static readonly SetCameraBehindPlayerImpl SetCameraBehindPlayer =
                null;

            [Native("GetPlayerCameraPos")] public static readonly GetPlayerCameraPosImpl GetPlayerCameraPos = null;

            [Native("GetPlayerCameraFrontVector")] public static readonly GetPlayerCameraFrontVectorImpl
                GetPlayerCameraFrontVector = null;

            [Native("GetPlayerCameraMode")] public static readonly GetPlayerCameraModeImpl GetPlayerCameraMode = null;

            [Native("EnablePlayerCameraTarget")] public static readonly EnablePlayerCameraTargetImpl
                NativeEnablePlayerCameraTarget = null;

            [Native("GetPlayerCameraTargetObject")] public static readonly GetPlayerCameraTargetObjectImpl
                GetPlayerCameraTargetObject = null;

            [Native("GetPlayerCameraTargetVehicle")] public static readonly GetPlayerCameraTargetVehicleImpl
                GetPlayerCameraTargetVehicle = null;

            [Native("GetPlayerCameraTargetPlayer")] public static readonly GetPlayerCameraTargetPlayerImpl
                GetPlayerCameraTargetPlayer = null;

            [Native("GetPlayerCameraTargetActor")] public static readonly GetPlayerCameraTargetActorImpl
                GetPlayerCameraTargetActor = null;

            [Native("InterpolateCameraPos")] public static readonly InterpolateCameraPosImpl InterpolateCameraPos = null;

            [Native("InterpolateCameraLookAt")] public static readonly InterpolateCameraLookAtImpl
                NativeInterpolateCameraLookAt =
                    null;

            [Native("IsPlayerConnected")] public static readonly IsPlayerConnectedImpl IsPlayerConnected = null;
            [Native("IsPlayerInVehicle")] public static readonly IsPlayerInVehicleImpl IsPlayerInVehicle = null;
            [Native("IsPlayerInAnyVehicle")] public static readonly IsPlayerInAnyVehicleImpl IsPlayerInAnyVehicle = null;
            [Native("IsPlayerInCheckpoint")] public static readonly IsPlayerInCheckpointImpl IsPlayerInCheckpoint = null;

            [Native("IsPlayerInRaceCheckpoint")] public static readonly IsPlayerInRaceCheckpointImpl
                IsPlayerInRaceCheckpoint = null;

            [Native("SetPlayerVirtualWorld")] public static readonly SetPlayerVirtualWorldImpl SetPlayerVirtualWorld =
                null;

            [Native("GetPlayerVirtualWorld")] public static readonly GetPlayerVirtualWorldImpl GetPlayerVirtualWorld =
                null;

            [Native("EnableStuntBonusForPlayer")] public static readonly EnableStuntBonusForPlayerImpl
                EnableStuntBonusForPlayer = null;

            [Native("TogglePlayerSpectating")] public static readonly TogglePlayerSpectatingImpl TogglePlayerSpectating
                =
                null;

            [Native("PlayerSpectatePlayer")] public static readonly PlayerSpectatePlayerImpl PlayerSpectatePlayer = null;

            [Native("PlayerSpectateVehicle")] public static readonly PlayerSpectateVehicleImpl PlayerSpectateVehicle =
                null;

            [Native("StartRecordingPlayerData")] public static readonly StartRecordingPlayerDataImpl
                NativeStartRecordingPlayerData = null;

            [Native("StopRecordingPlayerData")] public static readonly StopRecordingPlayerDataImpl
                NativeStopRecordingPlayerData =
                    null;

            [Native("CreateExplosionForPlayer")] public static readonly CreateExplosionForPlayerImpl
                CreateExplosionForPlayer = null;

            [Native("SelectTextDraw")] public static readonly SelectTextDrawImpl NativeSelectTextDraw = null;

            [Native("CancelSelectTextDraw")] public static readonly CancelSelectTextDrawImpl NativeCancelSelectTextDraw
                = null;

            [Native("SendClientMessage")] public static readonly SendClientMessageImpl NativeSendClientMessage = null;

            [Native("SendClientMessageToAll")] public static readonly SendClientMessageToAllImpl
                NativeSendClientMessageToAll =
                    null;

            [Native("SendPlayerMessageToPlayer")] public static readonly SendPlayerMessageToPlayerImpl
                NativeSendPlayerMessageToPlayer = null;

            [Native("SendPlayerMessageToAll")] public static readonly SendPlayerMessageToAllImpl
                NativeSendPlayerMessageToAll =
                    null;

            [Native("SendDeathMessage")] public static readonly SendDeathMessageImpl NativeSendDeathMessage = null;

            [Native("SendDeathMessageToPlayer")] public static readonly SendDeathMessageToPlayerImpl
                SendDeathMessageToPlayer = null;

            [Native("GameTextForAll")] public static readonly GameTextForAllImpl NativeGameTextForAll = null;

            [Native("GameTextForPlayer")] public static readonly GameTextForPlayerImpl GameTextForPlayer = null;
            [Native("Kick")] public static readonly KickImpl NativeKick = null;
            [Native("Ban")] public static readonly BanImpl NativeBan = null;
            [Native("BanEx")] public static readonly BanExImpl BanEx = null;

            [Native("CreateExplosion")] public static readonly CreateExplosionImpl NativeCreateExplosion = null;

            [Native("IsPlayerNPC")] public static readonly IsPlayerNPCImpl IsPlayerNPC = null;
            [Native("IsPlayerAdmin")] public static readonly IsPlayerAdminImpl IsPlayerAdmin = null;

            [Native("GetPlayerPoolSize")] public static readonly GetPlayerPoolSizeImpl GetPlayerPoolSize = null;

            [Native("GetPlayerVersion")] public static readonly GetPlayerVersionImpl GetPlayerVersion = null;

            [Native("GetPlayerNetworkStats")] public static readonly GetPlayerNetworkStatsImpl GetPlayerNetworkStats =
                null;

            [Native("gpci")] public static readonly GPCIImpl GPCI = null;
        }
    }
}