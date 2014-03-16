using System;
using System.Runtime.CompilerServices;
using GameMode.Definitions;

namespace GameMode
{
    public class Server
    {
        #region SA:MP Natives
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
        public static extern bool RemoveBuildingForPlayer(int playerid, int modelid, float fX, float fY, float fZ,
            float fRadius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerAttachedObject(int playerid, int index, int modelid, int bone, float fOffsetX,
            float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX, float fScaleY,
            float fScaleZ, int materialcolor1, int materialcolor2);

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
        public static extern bool PlayerTextDrawSetPreviewRot(int playerid, int text, float fRotX, float fRotY,
            float fRotZ, float fZoom);

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
        public static extern bool AllowPlayerTeleport(int playerid, bool allow);

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
        public static extern bool SendClientMessage(int playerid, int color, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendClientMessageToAll(int color, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendPlayerMessageToPlayer(int playerid, int senderid, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendPlayerMessageToAll(int senderid, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendDeathMessage(int killer, int killee, int weapon);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameTextForAll(string text, int time, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameTextForPlayer(int playerid, string text, int time, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetTickCount();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetMaxPlayers();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetGameModeText(string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetTeamCount(int count);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddPlayerClass(int modelid, float spawnX, float spawnY, float spawnZ, float zAngle,
            int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddPlayerClassEx(int teamid, int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticVehicle(int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int color1, int color2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticVehicleEx(int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int color1, int color2, int respawnDelay);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticPickup(int model, int type, float x, float y, float z, int virtualworld);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePickup(int model, int type, float x, float y, float z, int virtualworld);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyPickup(int pickup);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowNameTags(bool show);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerMarkers(int mode);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameModeExit();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetWorldTime(int hour);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetWeaponName(int weaponid, out string name, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableTirePopping(bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableVehicleFriendlyFire();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AllowInteriorWeapons(bool allow);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetWeather(int weatherid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetGravity(float gravity);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AllowAdminTeleport(bool allow);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetDeathDropAmount(int amount);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CreateExplosion(float x, float y, float z, int type, float radius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableZoneNames(bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UsePlayerPedAnims();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableInteriorEnterExits();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetNameTagDrawDistance(float distance);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableNameTagLOS();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LimitGlobalChatRadius(float chatRadius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LimitPlayerMarkerRadius(float markerRadius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ConnectNPC(string name, string script);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerNPC(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerAdmin(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Kick(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Ban(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool BanEx(int playerid, string reason);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendRconCommand(string command);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetServerVarAsString(string varname, out string value, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetServerVarAsInt(string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetServerVarAsBool(string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerNetworkStats(int playerid, out string retstr, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetNetworkStats(out string retstr, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerVersion(int playerid, out string version, int len);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreateMenu(string title, int columns, float x, float y, float col1Width,
            float col2Width);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyMenu(int menuid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddMenuItem(int menuid, int column, string menutext);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetMenuColumnHeader(int menuid, int column, string columnheader);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowMenuForPlayer(int menuid, int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool HideMenuForPlayer(int menuid, int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidMenu(int menuid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableMenu(int menuid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableMenuRow(int menuid, int row);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerMenu(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int TextDrawCreate(float x, float y, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawDestroy(int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawLetterSize(int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawTextSize(int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawAlignment(int text, int alignment);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawColor(int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawUseBox(int text, bool use);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawBoxColor(int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetShadow(int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetOutline(int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawBackgroundColor(int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawFont(int text, int font);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetProportional(int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetSelectable(int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawShowForPlayer(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawHideForPlayer(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawShowForAll(int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawHideForAll(int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetString(int text, string str);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewModel(int text, int modelindex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewRot(int text, float fRotX, float fRotY, float fRotZ, float fZoom);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewVehCol(int text, int color1, int color2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SelectTextDraw(int playerid, int hovercolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CancelSelectTextDraw(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GangZoneCreate(float minx, float miny, float maxx, float maxy);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneDestroy(int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneShowForPlayer(int playerid, int zone, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneShowForAll(int zone, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneHideForPlayer(int playerid, int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneHideForAll(int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneFlashForPlayer(int playerid, int zone, int flashcolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneFlashForAll(int zone, int flashcolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneStopFlashForPlayer(int playerid, int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneStopFlashForAll(int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int Create3DTextLabel(string text, int color, float x, float y, float z, float drawDistance,
            int virtualworld, bool testLOS);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Delete3DTextLabel(int id);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Attach3DTextLabelToPlayer(int id, int playerid, float offsetX, float offsetY,
            float offsetZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Attach3DTextLabelToVehicle(int id, int vehicleid, float offsetX, float offsetY,
            float offsetZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Update3DTextLabelText(int id, int color, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePlayer3DTextLabel(int playerid, string text, int color, float x, float y, float z,
            float drawDistance, int attachedplayer, int attachedvehicle, bool testLOS);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DeletePlayer3DTextLabel(int playerid, int id);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UpdatePlayer3DTextLabelText(int playerid, int id, int color, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerDialog(int playerid, int dialogid, int style, string caption, string info,
            string button1, string button2);

        //[MethodImpl(MethodImplOptions.InternalCall)]
        //public static extern int SetTimer(int interval, bool repeat, TimerCallback callback, void * param);

        //[MethodImpl(MethodImplOptions.InternalCall)]
        //public static extern bool KillTimer(int timerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        // ReSharper disable once InconsistentNaming
        public static extern bool gpci(int playerid, out string buffer, int size);
        #endregion

        #region Wrapping methods
        public static bool SetSpawnInfo(int playerid, int team, int skin, float x, float y, float z,
            float rotation, Weapon weapon1, int weapon1Ammo, Weapon weapon2, int weapon2Ammo, Weapon weapon3, int weapon3Ammo)
        {
            return SetSpawnInfo(playerid, team, skin, x, y, z, rotation, (int) weapon1, weapon1Ammo, (int) weapon2,
                weapon2Ammo, (int) weapon3, weapon3Ammo);
        }

        public static float GetPlayerFacingAngle(int playerid)
        {
            float angle;
            GetPlayerFacingAngle(playerid, out angle);
            return angle;
        }

        public static float GetPlayerHealth(int playerid)
        {
            float health;
            GetPlayerHealth(playerid, out health);
            return health;
        }

        public static float GetPlayerArmour(int playerid)
        {
            float armour;
            GetPlayerArmour(playerid, out armour);
            return armour;
        }

        public static string GetPlayerIp(int playerid)
        {
            string ip;
            GetPlayerIp(playerid, out ip, 16);
            return ip;
        }

        public static string GetPlayerName(int playerid)
        {
            string name;
            GetPlayerName(playerid, out name, Limits.MaxPlayerName);
            return name;
        }

        public static string GetPVarString(int playerid, string varname)
        {
            string value;
            GetPVarString(playerid, varname, out value, 64);
            return value;
        }

        public static string GetPVarNameAtIndex(int playerid, int index)
        {
            string varname;
            GetPVarNameAtIndex(playerid, index, out varname, 64);
            return varname;
        }

        public static string GetWeaponName(int weaponid)
        {
            string name;
            GetWeaponName(weaponid, out name, 32);
            return name;
        }

        public static string GetServerVarAsString(string varname)
        {
            string value;
            GetServerVarAsString(varname, out value, 64);
            return value;
        }

        public static string GetPlayerNetworkStats(int playerid)
        {
            string retstr;
            GetPlayerNetworkStats (playerid, out retstr, 256);
            return retstr;
        }

        public static string GetNetworkStats()
        {
            string retstr;
            GetNetworkStats(out retstr, 256);
            return retstr;
        }

        public static string GetPlayerVersion(int playerid)
        {
            string version;
            GetPlayerVersion(playerid, out version, 64);
            return version;
        }

        // ReSharper disable once InconsistentNaming
        public static string gpci(int playerid)
        {
            string buffer;
            gpci(playerid, out buffer, 64);
            return buffer;
        }
        #endregion

        #region Callbacks
        public virtual bool OnGameModeInit()
        {
            return true;
        }

        public virtual bool OnGameModeExit()
        {
            return true;
        }

        public virtual bool OnPlayerConnect(int playerid)
        {
            return true;
        }

        public virtual bool OnPlayerDisconnect(int playerid, int reason)
        {
            return true;
        }

        public virtual bool OnPlayerSpawn(int playerid)
        {
            return true;
        }

        public virtual bool OnPlayerDeath(int playerid, int killerid, int reason)
        {
            return true;
        }

        public virtual bool OnVehicleSpawn(int vehicleid)
        {
            return true;
        }

        public virtual bool OnVehicleDeath(int vehicleid, int killerid)
        {
            return true;
        }

        public virtual bool OnPlayerText(int playerid, string text)
        {
            return true;
        }

        public virtual bool OnPlayerCommandText(int playerid, string cmdtext)
        {
            return true;
        }

        public virtual bool OnPlayerRequestClass(int playerid, int classid)
        {
            return true;
        }

        public virtual bool OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger)
        {
            return true;
        }

        public virtual bool OnPlayerExitVehicle(int playerid, int vehicleid)
        {
            return true;
        }

        public virtual bool OnPlayerStateChange(int playerid, int newstate, int oldstate)
        {
            return true;
        }

        public virtual bool OnPlayerEnterCheckpoint(int playerid)
        {
            return true;
        }

        public virtual bool OnPlayerLeaveCheckpoint(int playerid)
        {
            return true;
        }

        public virtual bool OnPlayerEnterRaceCheckpoint(int playerid)
        {
            return true;
        }

        public virtual bool OnPlayerLeaveRaceCheckpoint(int playerid)
        {
            return true;
        }

        public virtual bool OnRconCommand(string cmd)
        {
            return true;
        }

        public virtual bool OnPlayerRequestSpawn(int playerid)
        {
            return true;
        }

        public virtual bool OnObjectMoved(int objectid)
        {
            return true;
        }

        public virtual bool OnPlayerObjectMoved(int playerid, int objectid)
        {
            return true;
        }

        public virtual bool OnPlayerPickUpPickup(int playerid, int pickupid)
        {
            return true;
        }

        public virtual bool OnVehicleMod(int playerid, int vehicleid, int componentid)
        {
            return true;
        }

        public virtual bool OnEnterExitModShop(int playerid, int enterexit, int interiorid)
        {
            return true;
        }

        public virtual bool OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid)
        {
            return true;
        }

        public virtual bool OnVehicleRespray(int playerid, int vehicleid, int color1, int color2)
        {
            return true;
        }

        public virtual bool OnVehicleDamageStatusUpdate(int vehicleid, int playerid)
        {
            return true;
        }

        public virtual bool OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passengerSeat)
        {
            return true;
        }

        public virtual bool OnPlayerSelectedMenuRow(int playerid, int row)
        {
            return true;
        }

        public virtual bool OnPlayerExitedMenu(int playerid)
        {
            return true;
        }

        public virtual bool OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid)
        {
            return true;
        }

        public virtual bool OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys)
        {
            return true;
        }

        public virtual bool OnRconLoginAttempt(string ip, string password, bool success)
        {
            return true;
        }

        public virtual bool OnPlayerUpdate(int playerid)
        {
            return true;
        }

        public virtual bool OnPlayerStreamIn(int playerid, int forplayerid)
        {
            return true;
        }

        public virtual bool OnPlayerStreamOut(int playerid, int forplayerid)
        {
            return true;
        }

        public virtual bool OnVehicleStreamIn(int vehicleid, int forplayerid)
        {
            return true;
        }

        public virtual bool OnVehicleStreamOut(int vehicleid, int forplayerid)
        {
            return true;
        }

        public virtual bool OnDialogResponse(int playerid, int dialogid, int response, int listitem, string inputtext)
        {
            return true;
        }

        public virtual bool OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart)
        {
            return true;
        }

        public virtual bool OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart)
        {
            return true;
        }

        public virtual bool OnPlayerClickMap(int playerid, float fX, float fY, float fZ)
        {
            return true;
        }

        public virtual bool OnPlayerClickTextDraw(int playerid, int clickedid)
        {
            return true;
        }

        public virtual bool OnPlayerClickPlayerTextDraw(int playerid, int playertextid)
        {
            return true;
        }

        public virtual bool OnPlayerClickPlayer(int playerid, int clickedplayerid, int source)
        {
            return true;
        }

        public virtual bool OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX, float fY,
            float fZ, float fRotX, float fRotY, float fRotZ)
        {
            return true;
        }

        public virtual bool OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid,
            float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX,
            float fScaleY, float fScaleZ)
        {
            return true;
        }

        public virtual bool OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY, float fZ)
        {
            return true;
        }

        public virtual bool OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY, float fZ)
        {
            return true;
        }
        #endregion
    }
}
