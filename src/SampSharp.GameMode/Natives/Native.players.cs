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

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {
        public delegate bool ApplyAnimationImpl(int playerid, string animlib, string animname, float fDelta, bool loop,
            bool lockx, bool locky, bool freeze, int time, bool forcesync);

        public delegate bool AttachCameraToObjectImpl(int playerid, int objectid);

        public delegate bool AttachCameraToPlayerObjectImpl(int playerid, int playerobjectid);

        public delegate bool ClearAnimationsImpl(int playerid, bool forcesync);

        public delegate bool CreateExplosionForPlayerImpl(int playerid, float x, float y, float z, int type,
            float radius);

        public delegate int CreatePlayerTextDrawImpl(int playerid, float x, float y, string text);

        public delegate bool DeletePVarImpl(int playerid, string varname);

        public delegate bool DisablePlayerCheckpointImpl(int playerid);

        public delegate bool DisablePlayerRaceCheckpointImpl(int playerid);

        public delegate bool DisableRemoteVehicleCollisionsImpl(int playerid, bool disable);

        public delegate bool EditAttachedObjectImpl(int playerid, int index);

        public delegate bool EnablePlayerCameraTargetImpl(int playerid, bool enable);

        public delegate bool EnableStuntBonusForAllImpl(bool enable);

        public delegate bool EnableStuntBonusForPlayerImpl(int playerid, bool enable);

        public delegate bool ForceClassSelectionImpl(int playerid);

        public delegate bool GetAnimationNameImpl(int index, out string animlib, int animlibSize, out string animname,
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

        public delegate int GetPlayerPingImpl(int playerid);

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

        public delegate int GetPlayerVirtualWorldImpl(int playerid);

        public delegate int GetPlayerWantedLevelImpl(int playerid);

        public delegate bool GetPlayerWeaponDataImpl(int playerid, int slot, out int weapon, out int ammo);

        public delegate int GetPlayerWeaponImpl(int playerid);

        public delegate int GetPlayerWeaponStateImpl(int playerid);

        public delegate float GetPVarFloatImpl(int playerid, string varname);

        public delegate int GetPVarIntImpl(int playerid, string varname);

        public delegate bool GetPVarNameAtIndexImpl(int playerid, int index, out string varname, int size);

        public delegate bool GetPVarStringImpl(int playerid, string varname, out string value, int size);

        public delegate int GetPVarsUpperIndexImpl(int playerid);

        public delegate int GetPVarTypeImpl(int playerid, string varname);

        public delegate bool GivePlayerMoneyImpl(int playerid, int money);

        public delegate bool GivePlayerWeaponImpl(int playerid, int weaponid, int ammo);

        public delegate bool InterpolateCameraLookAtImpl(int playerid, float fromX, float fromY, float fromZ, float toX,
            float toY, float toZ, int time, int cut);

        public delegate bool InterpolateCameraPosImpl(int playerid, float fromX, float fromY, float fromZ, float toX,
            float toY, float toZ, int time, int cut);

        public delegate bool IsPlayerAttachedObjectSlotUsedImpl(int playerid, int index);

        public delegate bool IsPlayerConnectedImpl(int playerid);

        public delegate bool IsPlayerInAnyVehicleImpl(int playerid);

        public delegate bool IsPlayerInCheckpointImpl(int playerid);

        public delegate bool IsPlayerInRaceCheckpointImpl(int playerid);

        public delegate bool IsPlayerInRangeOfPointImpl(int playerid, float range, float x, float y, float z);

        public delegate bool IsPlayerInVehicleImpl(int playerid, int vehicleid);

        public delegate bool IsPlayerStreamedInImpl(int playerid, int forplayerid);

        public delegate bool PlayAudioStreamForPlayerImpl(int playerid, string url, float posX, float posY, float posZ,
            float distance, bool usepos);

        public delegate bool PlayCrimeReportForPlayerImpl(int playerid, int suspectid, int crime);

        public delegate bool PlayerPlaySoundImpl(int playerid, int soundid, float x, float y, float z);

        public delegate bool PlayerSpectatePlayerImpl(int playerid, int targetplayerid, int mode);

        public delegate bool PlayerSpectateVehicleImpl(int playerid, int targetvehicleid, int mode);

        public delegate bool PlayerTextDrawAlignmentImpl(int playerid, int text, int alignment);

        public delegate bool PlayerTextDrawBackgroundColorImpl(int playerid, int text, int color);

        public delegate bool PlayerTextDrawBoxColorImpl(int playerid, int text, int color);

        public delegate bool PlayerTextDrawColorImpl(int playerid, int text, int color);

        public delegate bool PlayerTextDrawDestroyImpl(int playerid, int text);

        public delegate bool PlayerTextDrawFontImpl(int playerid, int text, int font);

        public delegate bool PlayerTextDrawHideImpl(int playerid, int text);

        public delegate bool PlayerTextDrawLetterSizeImpl(int playerid, int text, float x, float y);

        public delegate bool PlayerTextDrawSetOutlineImpl(int playerid, int text, int size);

        public delegate bool PlayerTextDrawSetPreviewModelImpl(int playerid, int text, int modelindex);

        public delegate bool PlayerTextDrawSetPreviewRotImpl(int playerid, int text, float rotX, float rotY,
            float rotZ, float zoom);

        public delegate bool PlayerTextDrawSetPreviewVehColImpl(int playerid, int text, int color1, int color2);

        public delegate bool PlayerTextDrawSetProportionalImpl(int playerid, int text, bool set);

        public delegate bool PlayerTextDrawSetSelectableImpl(int playerid, int text, bool set);

        public delegate bool PlayerTextDrawSetShadowImpl(int playerid, int text, int size);

        public delegate bool PlayerTextDrawSetStringImpl(int playerid, int text, string str);

        public delegate bool PlayerTextDrawShowImpl(int playerid, int text);

        public delegate bool PlayerTextDrawTextSizeImpl(int playerid, int text, float x, float y);

        public delegate bool PlayerTextDrawUseBoxImpl(int playerid, int text, bool use);

        public delegate bool PutPlayerInVehicleImpl(int playerid, int vehicleid, int seatid);

        public delegate bool RemoveBuildingForPlayerImpl(int playerid, int modelid, float x, float y, float z,
            float radius);

        public delegate bool RemovePlayerAttachedObjectImpl(int playerid, int index);

        public delegate bool RemovePlayerFromVehicleImpl(int playerid);

        public delegate bool RemovePlayerMapIconImpl(int playerid, int iconid);

        public delegate bool ResetPlayerMoneyImpl(int playerid);

        public delegate bool ResetPlayerWeaponsImpl(int playerid);

        public delegate bool SetCameraBehindPlayerImpl(int playerid);

        public delegate bool SetPlayerAmmoImpl(int playerid, int weaponid, int ammo);

        public delegate bool SetPlayerArmedWeaponImpl(int playerid, int weaponid);

        public delegate bool SetPlayerArmourImpl(int playerid, float armour);

        public delegate bool SetPlayerAttachedObjectImpl(int playerid, int index, int modelid, int bone, float offsetX,
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

        public delegate bool SetPlayerMapIconImpl(int playerid, int iconid, float x, float y, float z, int markertype,
            int color, int style);

        public delegate bool SetPlayerMarkerForPlayerImpl(int playerid, int showplayerid, int color);

        public delegate int SetPlayerNameImpl(int playerid, string name);

        public delegate bool SetPlayerPosFindZImpl(int playerid, float x, float y, float z);

        public delegate bool SetPlayerPosImpl(int playerid, float x, float y, float z);

        public delegate bool SetPlayerRaceCheckpointImpl(int playerid, int type, float x, float y, float z, float nextx,
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

        public delegate bool SetPVarFloatImpl(int playerid, string varname, float value);

        public delegate bool SetPVarIntImpl(int playerid, string varname, int value);

        public delegate bool SetPVarStringImpl(int playerid, string varname, string value);

        public delegate bool SetSpawnInfoImpl(int playerid, int team, int skin, float x, float y, float z,
            float rotation, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        public delegate bool ShowPlayerNameTagForPlayerImpl(int playerid, int showplayerid, bool show);

        public delegate bool SpawnPlayerImpl(int playerid);

        public delegate bool StartRecordingPlayerDataImpl(int playerid, int recordtype, string recordname);

        public delegate bool StopAudioStreamForPlayerImpl(int playerid);

        public delegate bool StopRecordingPlayerDataImpl(int playerid);

        public delegate bool TogglePlayerClockImpl(int playerid, bool toggle);

        public delegate bool TogglePlayerControllableImpl(int playerid, bool toggle);

        public delegate bool TogglePlayerSpectatingImpl(int playerid, bool toggle);

        [Native("SetSpawnInfo")] public static readonly SetSpawnInfoImpl SetSpawnInfo = null;
        [Native("SpawnPlayer")] public static readonly SpawnPlayerImpl SpawnPlayer = null;
        [Native("SetPlayerPos")] public static readonly SetPlayerPosImpl SetPlayerPos = null;
        [Native("SetPlayerPosFindZ")] public static readonly SetPlayerPosFindZImpl SetPlayerPosFindZ = null;
        [Native("GetPlayerPos")] public static readonly GetPlayerPosImpl GetPlayerPos = null;
        [Native("SetPlayerFacingAngle")] public static readonly SetPlayerFacingAngleImpl SetPlayerFacingAngle = null;
        [Native("GetPlayerFacingAngle")] public static readonly GetPlayerFacingAngleImpl GetPlayerFacingAngle = null;

        [Native("IsPlayerInRangeOfPoint")] public static readonly IsPlayerInRangeOfPointImpl IsPlayerInRangeOfPoint =
            null;

        [Native("GetPlayerDistanceFromPoint")] public static readonly GetPlayerDistanceFromPointImpl
            GetPlayerDistanceFromPoint = null;

        [Native("IsPlayerStreamedIn")] public static readonly IsPlayerStreamedInImpl IsPlayerStreamedIn = null;
        [Native("SetPlayerInterior")] public static readonly SetPlayerInteriorImpl SetPlayerInterior = null;
        [Native("GetPlayerInterior")] public static readonly GetPlayerInteriorImpl GetPlayerInterior = null;
        [Native("SetPlayerHealth")] public static readonly SetPlayerHealthImpl SetPlayerHealth = null;
        [Native("GetPlayerHealth")] public static readonly GetPlayerHealthImpl GetPlayerHealth = null;
        [Native("SetPlayerArmour")] public static readonly SetPlayerArmourImpl SetPlayerArmour = null;
        [Native("GetPlayerArmour")] public static readonly GetPlayerArmourImpl GetPlayerArmour = null;
        [Native("SetPlayerAmmo")] public static readonly SetPlayerAmmoImpl SetPlayerAmmo = null;
        [Native("GetPlayerAmmo")] public static readonly GetPlayerAmmoImpl GetPlayerAmmo = null;
        [Native("GetPlayerWeaponState")] public static readonly GetPlayerWeaponStateImpl GetPlayerWeaponState = null;
        [Native("GetPlayerTargetPlayer")] public static readonly GetPlayerTargetPlayerImpl GetPlayerTargetPlayer = null;
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
        [Native("ForceClassSelection")] public static readonly ForceClassSelectionImpl ForceClassSelection = null;
        [Native("SetPlayerWantedLevel")] public static readonly SetPlayerWantedLevelImpl SetPlayerWantedLevel = null;
        [Native("GetPlayerWantedLevel")] public static readonly GetPlayerWantedLevelImpl GetPlayerWantedLevel = null;

        [Native("SetPlayerFightingStyle")] public static readonly SetPlayerFightingStyleImpl SetPlayerFightingStyle =
            null;

        [Native("GetPlayerFightingStyle")] public static readonly GetPlayerFightingStyleImpl GetPlayerFightingStyle =
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

        [Native("RemoveBuildingForPlayer")] public static readonly RemoveBuildingForPlayerImpl RemoveBuildingForPlayer =
            null;

        [Native("SetPlayerAttachedObject")] public static readonly SetPlayerAttachedObjectImpl SetPlayerAttachedObject =
            null;

        [Native("RemovePlayerAttachedObject")] public static readonly RemovePlayerAttachedObjectImpl
            RemovePlayerAttachedObject = null;

        [Native("IsPlayerAttachedObjectSlotUsed")] public static readonly IsPlayerAttachedObjectSlotUsedImpl
            IsPlayerAttachedObjectSlotUsed = null;

        [Native("EditAttachedObject")] public static readonly EditAttachedObjectImpl EditAttachedObject = null;
        [Native("CreatePlayerTextDraw")] public static readonly CreatePlayerTextDrawImpl CreatePlayerTextDraw = null;
        [Native("PlayerTextDrawDestroy")] public static readonly PlayerTextDrawDestroyImpl PlayerTextDrawDestroy = null;

        [Native("PlayerTextDrawLetterSize")] public static readonly PlayerTextDrawLetterSizeImpl
            PlayerTextDrawLetterSize = null;

        [Native("PlayerTextDrawTextSize")] public static readonly PlayerTextDrawTextSizeImpl PlayerTextDrawTextSize =
            null;

        [Native("PlayerTextDrawAlignment")] public static readonly PlayerTextDrawAlignmentImpl PlayerTextDrawAlignment =
            null;

        [Native("PlayerTextDrawColor")] public static readonly PlayerTextDrawColorImpl PlayerTextDrawColor = null;
        [Native("PlayerTextDrawUseBox")] public static readonly PlayerTextDrawUseBoxImpl PlayerTextDrawUseBox = null;

        [Native("PlayerTextDrawBoxColor")] public static readonly PlayerTextDrawBoxColorImpl PlayerTextDrawBoxColor =
            null;

        [Native("PlayerTextDrawSetShadow")] public static readonly PlayerTextDrawSetShadowImpl PlayerTextDrawSetShadow =
            null;

        [Native("PlayerTextDrawSetOutline")] public static readonly PlayerTextDrawSetOutlineImpl
            PlayerTextDrawSetOutline = null;

        [Native("PlayerTextDrawBackgroundColor")] public static readonly PlayerTextDrawBackgroundColorImpl
            PlayerTextDrawBackgroundColor = null;

        [Native("PlayerTextDrawFont")] public static readonly PlayerTextDrawFontImpl PlayerTextDrawFont = null;

        [Native("PlayerTextDrawSetProportional")] public static readonly PlayerTextDrawSetProportionalImpl
            PlayerTextDrawSetProportional = null;

        [Native("PlayerTextDrawSetSelectable")] public static readonly PlayerTextDrawSetSelectableImpl
            PlayerTextDrawSetSelectable = null;

        [Native("PlayerTextDrawShow")] public static readonly PlayerTextDrawShowImpl PlayerTextDrawShow = null;
        [Native("PlayerTextDrawHide")] public static readonly PlayerTextDrawHideImpl PlayerTextDrawHide = null;

        [Native("PlayerTextDrawSetString")] public static readonly PlayerTextDrawSetStringImpl PlayerTextDrawSetString =
            null;

        [Native("PlayerTextDrawSetPreviewModel")] public static readonly PlayerTextDrawSetPreviewModelImpl
            PlayerTextDrawSetPreviewModel = null;

        [Native("PlayerTextDrawSetPreviewRot")] public static readonly PlayerTextDrawSetPreviewRotImpl
            PlayerTextDrawSetPreviewRot = null;

        [Native("PlayerTextDrawSetPreviewVehCol")] public static readonly PlayerTextDrawSetPreviewVehColImpl
            PlayerTextDrawSetPreviewVehCol = null;

        [Native("SetPVarInt")] public static readonly SetPVarIntImpl SetPVarInt = null;
        [Native("GetPVarInt")] public static readonly GetPVarIntImpl GetPVarInt = null;
        [Native("SetPVarString")] public static readonly SetPVarStringImpl SetPVarString = null;
        [Native("GetPVarString")] public static readonly GetPVarStringImpl GetPVarString = null;
        [Native("SetPVarFloat")] public static readonly SetPVarFloatImpl SetPVarFloat = null;
        [Native("GetPVarFloat")] public static readonly GetPVarFloatImpl GetPVarFloat = null;
        [Native("DeletePVar")] public static readonly DeletePVarImpl DeletePVar = null;
        [Native("GetPVarsUpperIndex")] public static readonly GetPVarsUpperIndexImpl GetPVarsUpperIndex = null;
        [Native("GetPVarNameAtIndex")] public static readonly GetPVarNameAtIndexImpl GetPVarNameAtIndex = null;
        [Native("GetPVarType")] public static readonly GetPVarTypeImpl GetPVarType = null;
        [Native("SetPlayerChatBubble")] public static readonly SetPlayerChatBubbleImpl SetPlayerChatBubble = null;
        [Native("PutPlayerInVehicle")] public static readonly PutPlayerInVehicleImpl PutPlayerInVehicle = null;
        [Native("GetPlayerVehicleID")] public static readonly GetPlayerVehicleIDImpl GetPlayerVehicleID = null;
        [Native("GetPlayerVehicleSeat")] public static readonly GetPlayerVehicleSeatImpl GetPlayerVehicleSeat = null;

        [Native("RemovePlayerFromVehicle")] public static readonly RemovePlayerFromVehicleImpl RemovePlayerFromVehicle =
            null;

        [Native("TogglePlayerControllable")] public static readonly TogglePlayerControllableImpl
            TogglePlayerControllable = null;

        [Native("PlayerPlaySound")] public static readonly PlayerPlaySoundImpl PlayerPlaySound = null;
        [Native("ApplyAnimation")] public static readonly ApplyAnimationImpl ApplyAnimation = null;
        [Native("ClearAnimations")] public static readonly ClearAnimationsImpl ClearAnimations = null;

        [Native("GetPlayerAnimationIndex")] public static readonly GetPlayerAnimationIndexImpl GetPlayerAnimationIndex =
            null;

        [Native("GetAnimationName")] public static readonly GetAnimationNameImpl GetAnimationName = null;

        [Native("GetPlayerSpecialAction")] public static readonly GetPlayerSpecialActionImpl GetPlayerSpecialAction =
            null;

        [Native("SetPlayerSpecialAction")] public static readonly SetPlayerSpecialActionImpl SetPlayerSpecialAction =
            null;

        [Native("DisableRemoteVehicleCollisions")] public static readonly DisableRemoteVehicleCollisionsImpl
            DisableRemoteVehicleCollisions = null;

        [Native("SetPlayerCheckpoint")] public static readonly SetPlayerCheckpointImpl SetPlayerCheckpoint = null;

        [Native("DisablePlayerCheckpoint")] public static readonly DisablePlayerCheckpointImpl DisablePlayerCheckpoint =
            null;

        [Native("SetPlayerRaceCheckpoint")] public static readonly SetPlayerRaceCheckpointImpl SetPlayerRaceCheckpoint =
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
        [Native("SetPlayerCameraLookAt")] public static readonly SetPlayerCameraLookAtImpl SetPlayerCameraLookAt = null;
        [Native("SetCameraBehindPlayer")] public static readonly SetCameraBehindPlayerImpl SetCameraBehindPlayer = null;
        [Native("GetPlayerCameraPos")] public static readonly GetPlayerCameraPosImpl GetPlayerCameraPos = null;

        [Native("GetPlayerCameraFrontVector")] public static readonly GetPlayerCameraFrontVectorImpl
            GetPlayerCameraFrontVector = null;

        [Native("GetPlayerCameraMode")] public static readonly GetPlayerCameraModeImpl GetPlayerCameraMode = null;

        [Native("EnablePlayerCameraTarget")] public static readonly EnablePlayerCameraTargetImpl
            EnablePlayerCameraTarget = null;

        [Native("GetPlayerCameraTargetObject")] public static readonly GetPlayerCameraTargetObjectImpl
            GetPlayerCameraTargetObject = null;

        [Native("GetPlayerCameraTargetVehicle")] public static readonly GetPlayerCameraTargetVehicleImpl
            GetPlayerCameraTargetVehicle = null;

        [Native("GetPlayerCameraTargetPlayer")] public static readonly GetPlayerCameraTargetPlayerImpl
            GetPlayerCameraTargetPlayer = null;

        [Native("GetPlayerCameraTargetActor")] public static readonly GetPlayerCameraTargetActorImpl
            GetPlayerCameraTargetActor = null;

        [Native("AttachCameraToObject")] public static readonly AttachCameraToObjectImpl AttachCameraToObject = null;

        [Native("AttachCameraToPlayerObject")] public static readonly AttachCameraToPlayerObjectImpl
            AttachCameraToPlayerObject = null;

        [Native("InterpolateCameraPos")] public static readonly InterpolateCameraPosImpl InterpolateCameraPos = null;

        [Native("InterpolateCameraLookAt")] public static readonly InterpolateCameraLookAtImpl InterpolateCameraLookAt =
            null;

        [Native("IsPlayerConnected")] public static readonly IsPlayerConnectedImpl IsPlayerConnected = null;
        [Native("IsPlayerInVehicle")] public static readonly IsPlayerInVehicleImpl IsPlayerInVehicle = null;
        [Native("IsPlayerInAnyVehicle")] public static readonly IsPlayerInAnyVehicleImpl IsPlayerInAnyVehicle = null;
        [Native("IsPlayerInCheckpoint")] public static readonly IsPlayerInCheckpointImpl IsPlayerInCheckpoint = null;

        [Native("IsPlayerInRaceCheckpoint")] public static readonly IsPlayerInRaceCheckpointImpl
            IsPlayerInRaceCheckpoint = null;

        [Native("SetPlayerVirtualWorld")] public static readonly SetPlayerVirtualWorldImpl SetPlayerVirtualWorld = null;
        [Native("GetPlayerVirtualWorld")] public static readonly GetPlayerVirtualWorldImpl GetPlayerVirtualWorld = null;

        [Native("EnableStuntBonusForPlayer")] public static readonly EnableStuntBonusForPlayerImpl
            EnableStuntBonusForPlayer = null;

        [Native("EnableStuntBonusForAll")] public static readonly EnableStuntBonusForAllImpl EnableStuntBonusForAll =
            null;

        [Native("TogglePlayerSpectating")] public static readonly TogglePlayerSpectatingImpl TogglePlayerSpectating =
            null;

        [Native("PlayerSpectatePlayer")] public static readonly PlayerSpectatePlayerImpl PlayerSpectatePlayer = null;
        [Native("PlayerSpectateVehicle")] public static readonly PlayerSpectateVehicleImpl PlayerSpectateVehicle = null;

        [Native("StartRecordingPlayerData")] public static readonly StartRecordingPlayerDataImpl
            StartRecordingPlayerData = null;

        [Native("StopRecordingPlayerData")] public static readonly StopRecordingPlayerDataImpl StopRecordingPlayerData =
            null;

        [Native("CreateExplosionForPlayer")] public static readonly CreateExplosionForPlayerImpl
            CreateExplosionForPlayer = null;
    }
}