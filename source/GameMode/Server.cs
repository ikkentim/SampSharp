using System;
using System.Runtime.CompilerServices;

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

        #region Callbacks
        public bool OnGameModeInit()
        {
            SetGameModeText("Test gamemode name");

            Console.WriteLine("OnGameModeInit");
            return true;
        }

        public bool OnGameModeExit()
        {
            Console.WriteLine("OnGameModeExit");
            return true;
        }

        public bool OnPlayerConnect(int playerid)
        {
            string ip;
            GetPlayerIp(playerid, out ip, 64);
            Console.WriteLine("OnPlayerConnect {0} : {1}", playerid, ip);
            
            return true;
        }

        public bool OnPlayerDisconnect(int playerid, int reason)
        {
            Console.WriteLine("OnPlayerDisconnect {0}, {1}", playerid, reason);
            return true;
        }

        public bool OnPlayerSpawn(int playerid)
        {
            Console.WriteLine("OnPlayerSpawn {0}", playerid);
            return true;
        }

        public bool OnPlayerDeath(int playerid, int killerid, int reason)
        {
            Console.WriteLine("OnPlayerDeath {0} {1} {2}", playerid, killerid, reason);
            return true;
        }

        public bool OnVehicleSpawn(int vehicleid)
        {
            Console.WriteLine("OnVehicleSpawn {0}", vehicleid);
            return true;
        }

        public bool OnVehicleDeath(int vehicleid, int killerid)
        {
            Console.WriteLine("OnVehicleDeath {0} {1}", vehicleid, killerid);
            return true;
        }

        public bool OnPlayerText(int playerid, string text)
        {
            Console.WriteLine("OnPlayerText {0}, {1}", playerid, text);
            return true;
        }

        public bool OnPlayerCommandText(int playerid, string cmdtext)
        {
            Console.WriteLine("OnPlayerCommandText {0}, {1}", playerid, cmdtext);
            return true;
        }

        public bool OnPlayerRequestClass(int playerid, int classid)
        {
            Console.WriteLine("OnPlayerRequestClass {0}, {1}", playerid, classid);
            return true;
        }

        public bool OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger)
        {
            Console.WriteLine("OnPlayerEnterVehicle {0}, {1}, {2}", playerid, vehicleid, ispassenger);
            return true;
        }

        public bool OnPlayerExitVehicle(int playerid, int vehicleid)
        {
            Console.WriteLine("OnPlayerExitVehicle {0}, {1}", playerid, vehicleid);
            return true;
        }

        public bool OnPlayerStateChange(int playerid, int newstate, int oldstate)
        {
            Console.WriteLine("OnPlayerStateChange {0}, {1}, {2}", playerid, newstate, oldstate);
            return true;
        }

        public bool OnPlayerEnterCheckpoint(int playerid)
        {
            Console.WriteLine("OnPlayerEnterCheckpoint {0}", playerid);
            return true;
        }

        public bool OnPlayerLeaveCheckpoint(int playerid)
        {
            Console.WriteLine("OnPlayerLeaveCheckpoint {0}", playerid);
            return true;
        }

        public bool OnPlayerEnterRaceCheckpoint(int playerid)
        {
            Console.WriteLine("OnPlayerEnterRaceCheckpoint {0}", playerid);
            return true;
        }

        public bool OnPlayerLeaveRaceCheckpoint(int playerid)
        {
            Console.WriteLine("OnPlayerLeaveRaceCheckpoint {0}", playerid);
            return true;
        }

        public bool OnRconCommand(string cmd)
        {
            Console.WriteLine("OnRconCommand {0}", cmd);
            return true;
        }

        public bool OnPlayerRequestSpawn(int playerid)
        {
            Console.WriteLine("OnPlayerRequestSpawn {0}", playerid);
            return true;
        }

        public bool OnObjectMoved(int objectid)
        {
            Console.WriteLine("OnObjectMoved {0}", objectid);
            return true;
        }

        public bool OnPlayerObjectMoved(int playerid, int objectid)
        {
            Console.WriteLine("OnPlayerObjectMoved {0} {1}", playerid, objectid);
            return true;
        }

        public bool OnPlayerPickUpPickup(int playerid, int pickupid)
        {
            Console.WriteLine("OnPlayerPickUpPickup {0} {1}", playerid, pickupid);
            return true;
        }

        public bool OnVehicleMod(int playerid, int vehicleid, int componentid)
        {
            Console.WriteLine("OnVehicleMod {0} {1} {2}", playerid, vehicleid, componentid);
            return true;
        }

        public bool OnEnterExitModShop(int playerid, int enterexit, int interiorid)
        {
            Console.WriteLine("OnEnterExitModShop {0} {1} {2}", playerid, enterexit, interiorid);
            return true;
        }

        public bool OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid)
        {
            Console.WriteLine("OnVehiclePaintjob {0} {1} {2}", playerid, vehicleid, paintjobid);
            return true;
        }

        public bool OnVehicleRespray(int playerid, int vehicleid, int color1, int color2)
        {
            Console.WriteLine("OnVehicleRespray {0} {1} {2} {3}", playerid, vehicleid, color1, color2);
            return true;
        }

        public bool OnVehicleDamageStatusUpdate(int vehicleid, int playerid)
        {
            Console.WriteLine("OnVehicleDamageStatusUpdate {0} {1}", vehicleid, playerid);
            return true;
        }

        public bool OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passengerSeat)
        {
            Console.WriteLine("OnUnoccupiedVehicleUpdate {0} {1} {2}", vehicleid, playerid, passengerSeat);
            return true;
        }

        public bool OnPlayerSelectedMenuRow(int playerid, int row)
        {
            Console.WriteLine("OnPlayerSelectedMenuRow {0} {1}", playerid, row);
            return true;
        }

        public bool OnPlayerExitedMenu(int playerid)
        {
            Console.WriteLine("OnPlayerExitedMenu {0}", playerid);
            return true;
        }

        public bool OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid)
        {
            Console.WriteLine("OnPlayerInteriorChange {0} {1} {2}", playerid, newinteriorid, oldinteriorid);
            return true;
        }

        public bool OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys)
        {
            Console.WriteLine("OnPlayerKeyStateChange {0} {1} {2}", playerid, newkeys, oldkeys);
            return true;
        }

        public bool OnRconLoginAttempt(string ip, string password, bool success)
        {
            Console.WriteLine("OnRconLoginAttempt {0} {1} {2}", ip, password, success);
            return true;
        }

        public bool OnPlayerUpdate(int playerid)
        {
            Console.WriteLine("OnPlayerUpdate {0}", playerid);
            return true;
        }

        public bool OnPlayerStreamIn(int playerid, int forplayerid)
        {
            Console.WriteLine("OnPlayerStreamIn {0} {1}", playerid, forplayerid);
            return true;
        }

        public bool OnPlayerStreamOut(int playerid, int forplayerid)
        {
            Console.WriteLine("OnPlayerStreamOut {0} {1}", playerid, forplayerid);
            return true;
        }

        public bool OnVehicleStreamIn(int vehicleid, int forplayerid)
        {
            Console.WriteLine("OnVehicleStreamIn {0} {1}", vehicleid, forplayerid);
            return true;
        }

        public bool OnVehicleStreamOut(int vehicleid, int forplayerid)
        {
            Console.WriteLine("OnVehicleStreamOut {0} {1}", vehicleid, forplayerid);
            return true;
        }

        public bool OnDialogResponse(int playerid, int dialogid, int response, int listitem, string inputtext)
        {
            Console.WriteLine("OnDialogResponse {0} {1} {2} {3} {4}", playerid, dialogid, response, listitem, inputtext);
            return true;
        }

        public bool OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart)
        {
            Console.WriteLine("OnPlayerTakeDamage {0} {1} {2} {3} {4}", playerid, issuerid, amount, weaponid, bodypart);
            return true;
        }

        public bool OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart)
        {
            Console.WriteLine("OnPlayerGiveDamage {0} {1} {2} {3} {4}", playerid, damagedid, amount, weaponid, bodypart);
            return true;
        }

        public bool OnPlayerClickMap(int playerid, float fX, float fY, float fZ)
        {
            Console.WriteLine("OnPlayerClickMap {0} {1} {2} {3}", playerid, fX, fY, fZ);
            return true;
        }

        public bool OnPlayerClickTextDraw(int playerid, int clickedid)
        {
            Console.WriteLine("OnPlayerClickTextDraw {0} {1}", playerid, clickedid);
            return true;
        }

        public bool OnPlayerClickPlayerTextDraw(int playerid, int playertextid)
        {
            Console.WriteLine("OnPlayerClickPlayerTextDraw {0} {1}", playerid, playertextid);
            return true;
        }

        public bool OnPlayerClickPlayer(int playerid, int clickedplayerid, int source)
        {
            Console.WriteLine("OnPlayerClickPlayer {0} {1} {2}", playerid, clickedplayerid, source);
            return true;
        }

        public bool OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX, float fY,
            float fZ, float fRotX, float fRotY, float fRotZ)
        {
            Console.WriteLine("OnPlayerEditObject {0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", playerid, playerobject,
                objectid, response, fX, fY, fZ, fRotX, fRotY, fRotZ);
            return true;
        }

        public bool OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid,
            float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX,
            float fScaleY, float fScaleZ)
        {
            Console.WriteLine("OnPlayerEditAttachedObject {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}", playerid, response, index, modelid, boneid, fOffsetX, fOffsetY, fOffsetZ, fRotX, fRotY, fRotZ, fScaleX, fScaleY, fScaleZ);
            return true;
        }

        public bool OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY, float fZ)
        {
            Console.WriteLine("OnPlayerSelectObject {0} {1} {2} {3} {4} {5} {6}", playerid, type, objectid, modelid, fX, fY, fZ);
            return true;
        }

        public bool OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY, float fZ)
        {
            Console.WriteLine("OnPlayerWeaponShot {0} {1} {2} {3} {4} {5} {6}", playerid, weaponid, hittype, hitid, fX, fY, fZ);
            return true;
        }
        #endregion
    }
}
