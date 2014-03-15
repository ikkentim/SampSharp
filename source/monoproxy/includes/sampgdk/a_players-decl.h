SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetSpawnInfo(int playerid, int team, int skin, float x, float y, float z, float rotation, int weapon1, int weapon1_ammo, int weapon2, int weapon2_ammo, int weapon3, int weapon3_ammo);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SpawnPlayer(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerPos(int playerid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerPosFindZ(int playerid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerPos(int playerid, float * x, float * y, float * z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerFacingAngle(int playerid, float angle);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerFacingAngle(int playerid, float * angle);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerInRangeOfPoint(int playerid, float range, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT float SAMPGDK_NATIVE_CALL sampgdk_GetPlayerDistanceFromPoint(int playerid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerStreamedIn(int playerid, int forplayerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerInterior(int playerid, int interiorid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerInterior(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerHealth(int playerid, float health);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerHealth(int playerid, float * health);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerArmour(int playerid, float armour);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerArmour(int playerid, float * armour);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerAmmo(int playerid, int weaponid, int ammo);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerAmmo(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerWeaponState(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerTargetPlayer(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerTeam(int playerid, int teamid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerTeam(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerScore(int playerid, int score);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerScore(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerDrunkLevel(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerDrunkLevel(int playerid, int level);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerColor(int playerid, int color);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerColor(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerSkin(int playerid, int skinid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerSkin(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GivePlayerWeapon(int playerid, int weaponid, int ammo);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ResetPlayerWeapons(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerArmedWeapon(int playerid, int weaponid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerWeaponData(int playerid, int slot, int * weapon, int * ammo);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GivePlayerMoney(int playerid, int money);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ResetPlayerMoney(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_SetPlayerName(int playerid, const char * name);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerMoney(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerState(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerIp(int playerid, char * ip, int size);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerPing(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerWeapon(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerKeys(int playerid, int * keys, int * updown, int * leftright);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerName(int playerid, char * name, int size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerTime(int playerid, int hour, int minute);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerTime(int playerid, int * hour, int * minute);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TogglePlayerClock(int playerid, bool toggle);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerWeather(int playerid, int weather);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ForceClassSelection(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerWantedLevel(int playerid, int level);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerWantedLevel(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerFightingStyle(int playerid, int style);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerFightingStyle(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerVelocity(int playerid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerVelocity(int playerid, float * x, float * y, float * z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayCrimeReportForPlayer(int playerid, int suspectid, int crime);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayAudioStreamForPlayer(int playerid, const char * url, float posX, float posY, float posZ, float distance, bool usepos);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_StopAudioStreamForPlayer(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerShopName(int playerid, const char * shopname);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerSkillLevel(int playerid, int skill, int level);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerSurfingVehicleID(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerSurfingObjectID(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_RemoveBuildingForPlayer(int playerid, int modelid, float fX, float fY, float fZ, float fRadius);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerAttachedObject(int playerid, int index, int modelid, int bone, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX, float fScaleY, float fScaleZ, int materialcolor1, int materialcolor2);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_RemovePlayerAttachedObject(int playerid, int index);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerAttachedObjectSlotUsed(int playerid, int index);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_EditAttachedObject(int playerid, int index);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_CreatePlayerTextDraw(int playerid, float x, float y, const char * text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawDestroy(int playerid, int text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawLetterSize(int playerid, int text, float x, float y);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawTextSize(int playerid, int text, float x, float y);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawAlignment(int playerid, int text, int alignment);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawColor(int playerid, int text, int color);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawUseBox(int playerid, int text, bool use);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawBoxColor(int playerid, int text, int color);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawSetShadow(int playerid, int text, int size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawSetOutline(int playerid, int text, int size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawBackgroundColor(int playerid, int text, int color);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawFont(int playerid, int text, int font);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawSetProportional(int playerid, int text, bool set);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawSetSelectable(int playerid, int text, bool set);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawShow(int playerid, int text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawHide(int playerid, int text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawSetString(int playerid, int text, const char * string);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawSetPreviewModel(int playerid, int text, int modelindex);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawSetPreviewRot(int playerid, int text, float fRotX, float fRotY, float fRotZ, float fZoom);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerTextDrawSetPreviewVehCol(int playerid, int text, int color1, int color2);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPVarInt(int playerid, const char * varname, int value);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPVarInt(int playerid, const char * varname);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPVarString(int playerid, const char * varname, const char * value);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPVarString(int playerid, const char * varname, char * value, int size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPVarFloat(int playerid, const char * varname, float value);
SAMPGDK_NATIVE_EXPORT float SAMPGDK_NATIVE_CALL sampgdk_GetPVarFloat(int playerid, const char * varname);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DeletePVar(int playerid, const char * varname);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPVarsUpperIndex(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPVarNameAtIndex(int playerid, int index, char * varname, int size);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPVarType(int playerid, const char * varname);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerChatBubble(int playerid, const char * text, int color, float drawdistance, int expiretime);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PutPlayerInVehicle(int playerid, int vehicleid, int seatid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerVehicleID(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerVehicleSeat(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_RemovePlayerFromVehicle(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TogglePlayerControllable(int playerid, bool toggle);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerPlaySound(int playerid, int soundid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ApplyAnimation(int playerid, const char * animlib, const char * animname, float fDelta, bool loop, bool lockx, bool locky, bool freeze, int time, bool forcesync);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ClearAnimations(int playerid, bool forcesync);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerAnimationIndex(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetAnimationName(int index, char * animlib, int animlib_size, char * animname, int animname_size);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerSpecialAction(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerSpecialAction(int playerid, int actionid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerCheckpoint(int playerid, float x, float y, float z, float size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DisablePlayerCheckpoint(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerRaceCheckpoint(int playerid, int type, float x, float y, float z, float nextx, float nexty, float nextz, float size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DisablePlayerRaceCheckpoint(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerWorldBounds(int playerid, float x_max, float x_min, float y_max, float y_min);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerMarkerForPlayer(int playerid, int showplayerid, int color);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ShowPlayerNameTagForPlayer(int playerid, int showplayerid, bool show);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerMapIcon(int playerid, int iconid, float x, float y, float z, int markertype, int color, int style);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_RemovePlayerMapIcon(int playerid, int iconid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AllowPlayerTeleport(int playerid, bool allow);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerCameraPos(int playerid, float x, float y, float z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerCameraLookAt(int playerid, float x, float y, float z, int cut);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetCameraBehindPlayer(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerCameraPos(int playerid, float * x, float * y, float * z);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerCameraFrontVector(int playerid, float * x, float * y, float * z);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerCameraMode(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AttachCameraToObject(int playerid, int objectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AttachCameraToPlayerObject(int playerid, int playerobjectid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_InterpolateCameraPos(int playerid, float FromX, float FromY, float FromZ, float ToX, float ToY, float ToZ, int time, int cut);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_InterpolateCameraLookAt(int playerid, float FromX, float FromY, float FromZ, float ToX, float ToY, float ToZ, int time, int cut);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerConnected(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerInVehicle(int playerid, int vehicleid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerInAnyVehicle(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerInCheckpoint(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerInRaceCheckpoint(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetPlayerVirtualWorld(int playerid, int worldid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerVirtualWorld(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_EnableStuntBonusForPlayer(int playerid, bool enable);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_EnableStuntBonusForAll(bool enable);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TogglePlayerSpectating(int playerid, bool toggle);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerSpectatePlayer(int playerid, int targetplayerid, int mode);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_PlayerSpectateVehicle(int playerid, int targetvehicleid, int mode);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_StartRecordingPlayerData(int playerid, int recordtype, const char * recordname);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_StopRecordingPlayerData(int playerid);

#ifndef __cplusplus

#define SPECIAL_ACTION_NONE (0)
#define SPECIAL_ACTION_DUCK (1)
#define SPECIAL_ACTION_USEJETPACK (2)
#define SPECIAL_ACTION_ENTER_VEHICLE (3)
#define SPECIAL_ACTION_EXIT_VEHICLE (4)
#define SPECIAL_ACTION_DANCE1 (5)
#define SPECIAL_ACTION_DANCE2 (6)
#define SPECIAL_ACTION_DANCE3 (7)
#define SPECIAL_ACTION_DANCE4 (8)
#define SPECIAL_ACTION_HANDSUP (10)
#define SPECIAL_ACTION_USECELLPHONE (11)
#define SPECIAL_ACTION_SITTING (12)
#define SPECIAL_ACTION_STOPUSECELLPHONE (13)
#define SPECIAL_ACTION_DRINK_BEER (20)
#define SPECIAL_ACTION_SMOKE_CIGGY (21)
#define SPECIAL_ACTION_DRINK_WINE (22)
#define SPECIAL_ACTION_DRINK_SPRUNK (23)
#define SPECIAL_ACTION_CUFFED (24)
#define SPECIAL_ACTION_CARRY (25)
#define FIGHT_STYLE_NORMAL (4)
#define FIGHT_STYLE_BOXING (5)
#define FIGHT_STYLE_KUNGFU (6)
#define FIGHT_STYLE_KNEEHEAD (7)
#define FIGHT_STYLE_GRABKICK (15)
#define FIGHT_STYLE_ELBOW (16)
#define WEAPONSKILL_PISTOL (0)
#define WEAPONSKILL_PISTOL_SILENCED (1)
#define WEAPONSKILL_DESERT_EAGLE (2)
#define WEAPONSKILL_SHOTGUN (3)
#define WEAPONSKILL_SAWNOFF_SHOTGUN (4)
#define WEAPONSKILL_SPAS12_SHOTGUN (5)
#define WEAPONSKILL_MICRO_UZI (6)
#define WEAPONSKILL_MP5 (7)
#define WEAPONSKILL_AK47 (8)
#define WEAPONSKILL_M4 (9)
#define WEAPONSKILL_SNIPERRIFLE (10)
#define WEAPONSTATE_UNKNOWN (-1)
#define WEAPONSTATE_NO_BULLETS (0)
#define WEAPONSTATE_LAST_BULLET (1)
#define WEAPONSTATE_MORE_BULLETS (2)
#define WEAPONSTATE_RELOADING (3)
#define MAX_PLAYER_ATTACHED_OBJECTS (10)
#define PLAYER_VARTYPE_NONE (0)
#define PLAYER_VARTYPE_INT (1)
#define PLAYER_VARTYPE_STRING (2)
#define PLAYER_VARTYPE_FLOAT (3)
#define MAX_CHATBUBBLE_LENGTH (144)
#define MAPICON_LOCAL (0)
#define MAPICON_GLOBAL (1)
#define MAPICON_LOCAL_CHECKPOINT (2)
#define MAPICON_GLOBAL_CHECKPOINT (3)
#define CAMERA_CUT (2)
#define CAMERA_MOVE (1)
#define SPECTATE_MODE_NORMAL (1)
#define SPECTATE_MODE_FIXED (2)
#define SPECTATE_MODE_SIDE (3)
#define PLAYER_RECORDING_TYPE_NONE (0)
#define PLAYER_RECORDING_TYPE_DRIVER (1)
#define PLAYER_RECORDING_TYPE_ONFOOT (2)

#undef  SetSpawnInfo
#define SetSpawnInfo sampgdk_SetSpawnInfo
#undef  SpawnPlayer
#define SpawnPlayer sampgdk_SpawnPlayer
#undef  SetPlayerPos
#define SetPlayerPos sampgdk_SetPlayerPos
#undef  SetPlayerPosFindZ
#define SetPlayerPosFindZ sampgdk_SetPlayerPosFindZ
#undef  GetPlayerPos
#define GetPlayerPos sampgdk_GetPlayerPos
#undef  SetPlayerFacingAngle
#define SetPlayerFacingAngle sampgdk_SetPlayerFacingAngle
#undef  GetPlayerFacingAngle
#define GetPlayerFacingAngle sampgdk_GetPlayerFacingAngle
#undef  IsPlayerInRangeOfPoint
#define IsPlayerInRangeOfPoint sampgdk_IsPlayerInRangeOfPoint
#undef  GetPlayerDistanceFromPoint
#define GetPlayerDistanceFromPoint sampgdk_GetPlayerDistanceFromPoint
#undef  IsPlayerStreamedIn
#define IsPlayerStreamedIn sampgdk_IsPlayerStreamedIn
#undef  SetPlayerInterior
#define SetPlayerInterior sampgdk_SetPlayerInterior
#undef  GetPlayerInterior
#define GetPlayerInterior sampgdk_GetPlayerInterior
#undef  SetPlayerHealth
#define SetPlayerHealth sampgdk_SetPlayerHealth
#undef  GetPlayerHealth
#define GetPlayerHealth sampgdk_GetPlayerHealth
#undef  SetPlayerArmour
#define SetPlayerArmour sampgdk_SetPlayerArmour
#undef  GetPlayerArmour
#define GetPlayerArmour sampgdk_GetPlayerArmour
#undef  SetPlayerAmmo
#define SetPlayerAmmo sampgdk_SetPlayerAmmo
#undef  GetPlayerAmmo
#define GetPlayerAmmo sampgdk_GetPlayerAmmo
#undef  GetPlayerWeaponState
#define GetPlayerWeaponState sampgdk_GetPlayerWeaponState
#undef  GetPlayerTargetPlayer
#define GetPlayerTargetPlayer sampgdk_GetPlayerTargetPlayer
#undef  SetPlayerTeam
#define SetPlayerTeam sampgdk_SetPlayerTeam
#undef  GetPlayerTeam
#define GetPlayerTeam sampgdk_GetPlayerTeam
#undef  SetPlayerScore
#define SetPlayerScore sampgdk_SetPlayerScore
#undef  GetPlayerScore
#define GetPlayerScore sampgdk_GetPlayerScore
#undef  GetPlayerDrunkLevel
#define GetPlayerDrunkLevel sampgdk_GetPlayerDrunkLevel
#undef  SetPlayerDrunkLevel
#define SetPlayerDrunkLevel sampgdk_SetPlayerDrunkLevel
#undef  SetPlayerColor
#define SetPlayerColor sampgdk_SetPlayerColor
#undef  GetPlayerColor
#define GetPlayerColor sampgdk_GetPlayerColor
#undef  SetPlayerSkin
#define SetPlayerSkin sampgdk_SetPlayerSkin
#undef  GetPlayerSkin
#define GetPlayerSkin sampgdk_GetPlayerSkin
#undef  GivePlayerWeapon
#define GivePlayerWeapon sampgdk_GivePlayerWeapon
#undef  ResetPlayerWeapons
#define ResetPlayerWeapons sampgdk_ResetPlayerWeapons
#undef  SetPlayerArmedWeapon
#define SetPlayerArmedWeapon sampgdk_SetPlayerArmedWeapon
#undef  GetPlayerWeaponData
#define GetPlayerWeaponData sampgdk_GetPlayerWeaponData
#undef  GivePlayerMoney
#define GivePlayerMoney sampgdk_GivePlayerMoney
#undef  ResetPlayerMoney
#define ResetPlayerMoney sampgdk_ResetPlayerMoney
#undef  SetPlayerName
#define SetPlayerName sampgdk_SetPlayerName
#undef  GetPlayerMoney
#define GetPlayerMoney sampgdk_GetPlayerMoney
#undef  GetPlayerState
#define GetPlayerState sampgdk_GetPlayerState
#undef  GetPlayerIp
#define GetPlayerIp sampgdk_GetPlayerIp
#undef  GetPlayerPing
#define GetPlayerPing sampgdk_GetPlayerPing
#undef  GetPlayerWeapon
#define GetPlayerWeapon sampgdk_GetPlayerWeapon
#undef  GetPlayerKeys
#define GetPlayerKeys sampgdk_GetPlayerKeys
#undef  GetPlayerName
#define GetPlayerName sampgdk_GetPlayerName
#undef  SetPlayerTime
#define SetPlayerTime sampgdk_SetPlayerTime
#undef  GetPlayerTime
#define GetPlayerTime sampgdk_GetPlayerTime
#undef  TogglePlayerClock
#define TogglePlayerClock sampgdk_TogglePlayerClock
#undef  SetPlayerWeather
#define SetPlayerWeather sampgdk_SetPlayerWeather
#undef  ForceClassSelection
#define ForceClassSelection sampgdk_ForceClassSelection
#undef  SetPlayerWantedLevel
#define SetPlayerWantedLevel sampgdk_SetPlayerWantedLevel
#undef  GetPlayerWantedLevel
#define GetPlayerWantedLevel sampgdk_GetPlayerWantedLevel
#undef  SetPlayerFightingStyle
#define SetPlayerFightingStyle sampgdk_SetPlayerFightingStyle
#undef  GetPlayerFightingStyle
#define GetPlayerFightingStyle sampgdk_GetPlayerFightingStyle
#undef  SetPlayerVelocity
#define SetPlayerVelocity sampgdk_SetPlayerVelocity
#undef  GetPlayerVelocity
#define GetPlayerVelocity sampgdk_GetPlayerVelocity
#undef  PlayCrimeReportForPlayer
#define PlayCrimeReportForPlayer sampgdk_PlayCrimeReportForPlayer
#undef  PlayAudioStreamForPlayer
#define PlayAudioStreamForPlayer sampgdk_PlayAudioStreamForPlayer
#undef  StopAudioStreamForPlayer
#define StopAudioStreamForPlayer sampgdk_StopAudioStreamForPlayer
#undef  SetPlayerShopName
#define SetPlayerShopName sampgdk_SetPlayerShopName
#undef  SetPlayerSkillLevel
#define SetPlayerSkillLevel sampgdk_SetPlayerSkillLevel
#undef  GetPlayerSurfingVehicleID
#define GetPlayerSurfingVehicleID sampgdk_GetPlayerSurfingVehicleID
#undef  GetPlayerSurfingObjectID
#define GetPlayerSurfingObjectID sampgdk_GetPlayerSurfingObjectID
#undef  RemoveBuildingForPlayer
#define RemoveBuildingForPlayer sampgdk_RemoveBuildingForPlayer
#undef  SetPlayerAttachedObject
#define SetPlayerAttachedObject sampgdk_SetPlayerAttachedObject
#undef  RemovePlayerAttachedObject
#define RemovePlayerAttachedObject sampgdk_RemovePlayerAttachedObject
#undef  IsPlayerAttachedObjectSlotUsed
#define IsPlayerAttachedObjectSlotUsed sampgdk_IsPlayerAttachedObjectSlotUsed
#undef  EditAttachedObject
#define EditAttachedObject sampgdk_EditAttachedObject
#undef  CreatePlayerTextDraw
#define CreatePlayerTextDraw sampgdk_CreatePlayerTextDraw
#undef  PlayerTextDrawDestroy
#define PlayerTextDrawDestroy sampgdk_PlayerTextDrawDestroy
#undef  PlayerTextDrawLetterSize
#define PlayerTextDrawLetterSize sampgdk_PlayerTextDrawLetterSize
#undef  PlayerTextDrawTextSize
#define PlayerTextDrawTextSize sampgdk_PlayerTextDrawTextSize
#undef  PlayerTextDrawAlignment
#define PlayerTextDrawAlignment sampgdk_PlayerTextDrawAlignment
#undef  PlayerTextDrawColor
#define PlayerTextDrawColor sampgdk_PlayerTextDrawColor
#undef  PlayerTextDrawUseBox
#define PlayerTextDrawUseBox sampgdk_PlayerTextDrawUseBox
#undef  PlayerTextDrawBoxColor
#define PlayerTextDrawBoxColor sampgdk_PlayerTextDrawBoxColor
#undef  PlayerTextDrawSetShadow
#define PlayerTextDrawSetShadow sampgdk_PlayerTextDrawSetShadow
#undef  PlayerTextDrawSetOutline
#define PlayerTextDrawSetOutline sampgdk_PlayerTextDrawSetOutline
#undef  PlayerTextDrawBackgroundColor
#define PlayerTextDrawBackgroundColor sampgdk_PlayerTextDrawBackgroundColor
#undef  PlayerTextDrawFont
#define PlayerTextDrawFont sampgdk_PlayerTextDrawFont
#undef  PlayerTextDrawSetProportional
#define PlayerTextDrawSetProportional sampgdk_PlayerTextDrawSetProportional
#undef  PlayerTextDrawSetSelectable
#define PlayerTextDrawSetSelectable sampgdk_PlayerTextDrawSetSelectable
#undef  PlayerTextDrawShow
#define PlayerTextDrawShow sampgdk_PlayerTextDrawShow
#undef  PlayerTextDrawHide
#define PlayerTextDrawHide sampgdk_PlayerTextDrawHide
#undef  PlayerTextDrawSetString
#define PlayerTextDrawSetString sampgdk_PlayerTextDrawSetString
#undef  PlayerTextDrawSetPreviewModel
#define PlayerTextDrawSetPreviewModel sampgdk_PlayerTextDrawSetPreviewModel
#undef  PlayerTextDrawSetPreviewRot
#define PlayerTextDrawSetPreviewRot sampgdk_PlayerTextDrawSetPreviewRot
#undef  PlayerTextDrawSetPreviewVehCol
#define PlayerTextDrawSetPreviewVehCol sampgdk_PlayerTextDrawSetPreviewVehCol
#undef  SetPVarInt
#define SetPVarInt sampgdk_SetPVarInt
#undef  GetPVarInt
#define GetPVarInt sampgdk_GetPVarInt
#undef  SetPVarString
#define SetPVarString sampgdk_SetPVarString
#undef  GetPVarString
#define GetPVarString sampgdk_GetPVarString
#undef  SetPVarFloat
#define SetPVarFloat sampgdk_SetPVarFloat
#undef  GetPVarFloat
#define GetPVarFloat sampgdk_GetPVarFloat
#undef  DeletePVar
#define DeletePVar sampgdk_DeletePVar
#undef  GetPVarsUpperIndex
#define GetPVarsUpperIndex sampgdk_GetPVarsUpperIndex
#undef  GetPVarNameAtIndex
#define GetPVarNameAtIndex sampgdk_GetPVarNameAtIndex
#undef  GetPVarType
#define GetPVarType sampgdk_GetPVarType
#undef  SetPlayerChatBubble
#define SetPlayerChatBubble sampgdk_SetPlayerChatBubble
#undef  PutPlayerInVehicle
#define PutPlayerInVehicle sampgdk_PutPlayerInVehicle
#undef  GetPlayerVehicleID
#define GetPlayerVehicleID sampgdk_GetPlayerVehicleID
#undef  GetPlayerVehicleSeat
#define GetPlayerVehicleSeat sampgdk_GetPlayerVehicleSeat
#undef  RemovePlayerFromVehicle
#define RemovePlayerFromVehicle sampgdk_RemovePlayerFromVehicle
#undef  TogglePlayerControllable
#define TogglePlayerControllable sampgdk_TogglePlayerControllable
#undef  PlayerPlaySound
#define PlayerPlaySound sampgdk_PlayerPlaySound
#undef  ApplyAnimation
#define ApplyAnimation sampgdk_ApplyAnimation
#undef  ClearAnimations
#define ClearAnimations sampgdk_ClearAnimations
#undef  GetPlayerAnimationIndex
#define GetPlayerAnimationIndex sampgdk_GetPlayerAnimationIndex
#undef  GetAnimationName
#define GetAnimationName sampgdk_GetAnimationName
#undef  GetPlayerSpecialAction
#define GetPlayerSpecialAction sampgdk_GetPlayerSpecialAction
#undef  SetPlayerSpecialAction
#define SetPlayerSpecialAction sampgdk_SetPlayerSpecialAction
#undef  SetPlayerCheckpoint
#define SetPlayerCheckpoint sampgdk_SetPlayerCheckpoint
#undef  DisablePlayerCheckpoint
#define DisablePlayerCheckpoint sampgdk_DisablePlayerCheckpoint
#undef  SetPlayerRaceCheckpoint
#define SetPlayerRaceCheckpoint sampgdk_SetPlayerRaceCheckpoint
#undef  DisablePlayerRaceCheckpoint
#define DisablePlayerRaceCheckpoint sampgdk_DisablePlayerRaceCheckpoint
#undef  SetPlayerWorldBounds
#define SetPlayerWorldBounds sampgdk_SetPlayerWorldBounds
#undef  SetPlayerMarkerForPlayer
#define SetPlayerMarkerForPlayer sampgdk_SetPlayerMarkerForPlayer
#undef  ShowPlayerNameTagForPlayer
#define ShowPlayerNameTagForPlayer sampgdk_ShowPlayerNameTagForPlayer
#undef  SetPlayerMapIcon
#define SetPlayerMapIcon sampgdk_SetPlayerMapIcon
#undef  RemovePlayerMapIcon
#define RemovePlayerMapIcon sampgdk_RemovePlayerMapIcon
#undef  AllowPlayerTeleport
#define AllowPlayerTeleport sampgdk_AllowPlayerTeleport
#undef  SetPlayerCameraPos
#define SetPlayerCameraPos sampgdk_SetPlayerCameraPos
#undef  SetPlayerCameraLookAt
#define SetPlayerCameraLookAt sampgdk_SetPlayerCameraLookAt
#undef  SetCameraBehindPlayer
#define SetCameraBehindPlayer sampgdk_SetCameraBehindPlayer
#undef  GetPlayerCameraPos
#define GetPlayerCameraPos sampgdk_GetPlayerCameraPos
#undef  GetPlayerCameraFrontVector
#define GetPlayerCameraFrontVector sampgdk_GetPlayerCameraFrontVector
#undef  GetPlayerCameraMode
#define GetPlayerCameraMode sampgdk_GetPlayerCameraMode
#undef  AttachCameraToObject
#define AttachCameraToObject sampgdk_AttachCameraToObject
#undef  AttachCameraToPlayerObject
#define AttachCameraToPlayerObject sampgdk_AttachCameraToPlayerObject
#undef  InterpolateCameraPos
#define InterpolateCameraPos sampgdk_InterpolateCameraPos
#undef  InterpolateCameraLookAt
#define InterpolateCameraLookAt sampgdk_InterpolateCameraLookAt
#undef  IsPlayerConnected
#define IsPlayerConnected sampgdk_IsPlayerConnected
#undef  IsPlayerInVehicle
#define IsPlayerInVehicle sampgdk_IsPlayerInVehicle
#undef  IsPlayerInAnyVehicle
#define IsPlayerInAnyVehicle sampgdk_IsPlayerInAnyVehicle
#undef  IsPlayerInCheckpoint
#define IsPlayerInCheckpoint sampgdk_IsPlayerInCheckpoint
#undef  IsPlayerInRaceCheckpoint
#define IsPlayerInRaceCheckpoint sampgdk_IsPlayerInRaceCheckpoint
#undef  SetPlayerVirtualWorld
#define SetPlayerVirtualWorld sampgdk_SetPlayerVirtualWorld
#undef  GetPlayerVirtualWorld
#define GetPlayerVirtualWorld sampgdk_GetPlayerVirtualWorld
#undef  EnableStuntBonusForPlayer
#define EnableStuntBonusForPlayer sampgdk_EnableStuntBonusForPlayer
#undef  EnableStuntBonusForAll
#define EnableStuntBonusForAll sampgdk_EnableStuntBonusForAll
#undef  TogglePlayerSpectating
#define TogglePlayerSpectating sampgdk_TogglePlayerSpectating
#undef  PlayerSpectatePlayer
#define PlayerSpectatePlayer sampgdk_PlayerSpectatePlayer
#undef  PlayerSpectateVehicle
#define PlayerSpectateVehicle sampgdk_PlayerSpectateVehicle
#undef  StartRecordingPlayerData
#define StartRecordingPlayerData sampgdk_StartRecordingPlayerData
#undef  StopRecordingPlayerData
#define StopRecordingPlayerData sampgdk_StopRecordingPlayerData

#else /* __cplusplus */

SAMPGDK_BEGIN_NAMESPACE

const int SPECIAL_ACTION_NONE = 0;
const int SPECIAL_ACTION_DUCK = 1;
const int SPECIAL_ACTION_USEJETPACK = 2;
const int SPECIAL_ACTION_ENTER_VEHICLE = 3;
const int SPECIAL_ACTION_EXIT_VEHICLE = 4;
const int SPECIAL_ACTION_DANCE1 = 5;
const int SPECIAL_ACTION_DANCE2 = 6;
const int SPECIAL_ACTION_DANCE3 = 7;
const int SPECIAL_ACTION_DANCE4 = 8;
const int SPECIAL_ACTION_HANDSUP = 10;
const int SPECIAL_ACTION_USECELLPHONE = 11;
const int SPECIAL_ACTION_SITTING = 12;
const int SPECIAL_ACTION_STOPUSECELLPHONE = 13;
const int SPECIAL_ACTION_DRINK_BEER = 20;
const int SPECIAL_ACTION_SMOKE_CIGGY = 21;
const int SPECIAL_ACTION_DRINK_WINE = 22;
const int SPECIAL_ACTION_DRINK_SPRUNK = 23;
const int SPECIAL_ACTION_CUFFED = 24;
const int SPECIAL_ACTION_CARRY = 25;
const int FIGHT_STYLE_NORMAL = 4;
const int FIGHT_STYLE_BOXING = 5;
const int FIGHT_STYLE_KUNGFU = 6;
const int FIGHT_STYLE_KNEEHEAD = 7;
const int FIGHT_STYLE_GRABKICK = 15;
const int FIGHT_STYLE_ELBOW = 16;
const int WEAPONSKILL_PISTOL = 0;
const int WEAPONSKILL_PISTOL_SILENCED = 1;
const int WEAPONSKILL_DESERT_EAGLE = 2;
const int WEAPONSKILL_SHOTGUN = 3;
const int WEAPONSKILL_SAWNOFF_SHOTGUN = 4;
const int WEAPONSKILL_SPAS12_SHOTGUN = 5;
const int WEAPONSKILL_MICRO_UZI = 6;
const int WEAPONSKILL_MP5 = 7;
const int WEAPONSKILL_AK47 = 8;
const int WEAPONSKILL_M4 = 9;
const int WEAPONSKILL_SNIPERRIFLE = 10;
const int WEAPONSTATE_UNKNOWN = -1;
const int WEAPONSTATE_NO_BULLETS = 0;
const int WEAPONSTATE_LAST_BULLET = 1;
const int WEAPONSTATE_MORE_BULLETS = 2;
const int WEAPONSTATE_RELOADING = 3;
const int MAX_PLAYER_ATTACHED_OBJECTS = 10;
const int PLAYER_VARTYPE_NONE = 0;
const int PLAYER_VARTYPE_INT = 1;
const int PLAYER_VARTYPE_STRING = 2;
const int PLAYER_VARTYPE_FLOAT = 3;
const int MAX_CHATBUBBLE_LENGTH = 144;
const int MAPICON_LOCAL = 0;
const int MAPICON_GLOBAL = 1;
const int MAPICON_LOCAL_CHECKPOINT = 2;
const int MAPICON_GLOBAL_CHECKPOINT = 3;
const int CAMERA_CUT = 2;
const int CAMERA_MOVE = 1;
const int SPECTATE_MODE_NORMAL = 1;
const int SPECTATE_MODE_FIXED = 2;
const int SPECTATE_MODE_SIDE = 3;
const int PLAYER_RECORDING_TYPE_NONE = 0;
const int PLAYER_RECORDING_TYPE_DRIVER = 1;
const int PLAYER_RECORDING_TYPE_ONFOOT = 2;

static inline bool SetSpawnInfo(int playerid, int team, int skin, float x, float y, float z, float rotation, int weapon1, int weapon1_ammo, int weapon2, int weapon2_ammo, int weapon3, int weapon3_ammo) {
  return ::sampgdk_SetSpawnInfo(playerid, team, skin, x, y, z, rotation, weapon1, weapon1_ammo, weapon2, weapon2_ammo, weapon3, weapon3_ammo);
}
static inline bool SpawnPlayer(int playerid) {
  return ::sampgdk_SpawnPlayer(playerid);
}
static inline bool SetPlayerPos(int playerid, float x, float y, float z) {
  return ::sampgdk_SetPlayerPos(playerid, x, y, z);
}
static inline bool SetPlayerPosFindZ(int playerid, float x, float y, float z) {
  return ::sampgdk_SetPlayerPosFindZ(playerid, x, y, z);
}
static inline bool GetPlayerPos(int playerid, float * x, float * y, float * z) {
  return ::sampgdk_GetPlayerPos(playerid, x, y, z);
}
static inline bool SetPlayerFacingAngle(int playerid, float angle) {
  return ::sampgdk_SetPlayerFacingAngle(playerid, angle);
}
static inline bool GetPlayerFacingAngle(int playerid, float * angle) {
  return ::sampgdk_GetPlayerFacingAngle(playerid, angle);
}
static inline bool IsPlayerInRangeOfPoint(int playerid, float range, float x, float y, float z) {
  return ::sampgdk_IsPlayerInRangeOfPoint(playerid, range, x, y, z);
}
static inline float GetPlayerDistanceFromPoint(int playerid, float x, float y, float z) {
  return ::sampgdk_GetPlayerDistanceFromPoint(playerid, x, y, z);
}
static inline bool IsPlayerStreamedIn(int playerid, int forplayerid) {
  return ::sampgdk_IsPlayerStreamedIn(playerid, forplayerid);
}
static inline bool SetPlayerInterior(int playerid, int interiorid) {
  return ::sampgdk_SetPlayerInterior(playerid, interiorid);
}
static inline int GetPlayerInterior(int playerid) {
  return ::sampgdk_GetPlayerInterior(playerid);
}
static inline bool SetPlayerHealth(int playerid, float health) {
  return ::sampgdk_SetPlayerHealth(playerid, health);
}
static inline bool GetPlayerHealth(int playerid, float * health) {
  return ::sampgdk_GetPlayerHealth(playerid, health);
}
static inline bool SetPlayerArmour(int playerid, float armour) {
  return ::sampgdk_SetPlayerArmour(playerid, armour);
}
static inline bool GetPlayerArmour(int playerid, float * armour) {
  return ::sampgdk_GetPlayerArmour(playerid, armour);
}
static inline bool SetPlayerAmmo(int playerid, int weaponid, int ammo) {
  return ::sampgdk_SetPlayerAmmo(playerid, weaponid, ammo);
}
static inline int GetPlayerAmmo(int playerid) {
  return ::sampgdk_GetPlayerAmmo(playerid);
}
static inline int GetPlayerWeaponState(int playerid) {
  return ::sampgdk_GetPlayerWeaponState(playerid);
}
static inline int GetPlayerTargetPlayer(int playerid) {
  return ::sampgdk_GetPlayerTargetPlayer(playerid);
}
static inline bool SetPlayerTeam(int playerid, int teamid) {
  return ::sampgdk_SetPlayerTeam(playerid, teamid);
}
static inline int GetPlayerTeam(int playerid) {
  return ::sampgdk_GetPlayerTeam(playerid);
}
static inline bool SetPlayerScore(int playerid, int score) {
  return ::sampgdk_SetPlayerScore(playerid, score);
}
static inline int GetPlayerScore(int playerid) {
  return ::sampgdk_GetPlayerScore(playerid);
}
static inline int GetPlayerDrunkLevel(int playerid) {
  return ::sampgdk_GetPlayerDrunkLevel(playerid);
}
static inline bool SetPlayerDrunkLevel(int playerid, int level) {
  return ::sampgdk_SetPlayerDrunkLevel(playerid, level);
}
static inline bool SetPlayerColor(int playerid, int color) {
  return ::sampgdk_SetPlayerColor(playerid, color);
}
static inline int GetPlayerColor(int playerid) {
  return ::sampgdk_GetPlayerColor(playerid);
}
static inline bool SetPlayerSkin(int playerid, int skinid) {
  return ::sampgdk_SetPlayerSkin(playerid, skinid);
}
static inline int GetPlayerSkin(int playerid) {
  return ::sampgdk_GetPlayerSkin(playerid);
}
static inline bool GivePlayerWeapon(int playerid, int weaponid, int ammo) {
  return ::sampgdk_GivePlayerWeapon(playerid, weaponid, ammo);
}
static inline bool ResetPlayerWeapons(int playerid) {
  return ::sampgdk_ResetPlayerWeapons(playerid);
}
static inline bool SetPlayerArmedWeapon(int playerid, int weaponid) {
  return ::sampgdk_SetPlayerArmedWeapon(playerid, weaponid);
}
static inline bool GetPlayerWeaponData(int playerid, int slot, int * weapon, int * ammo) {
  return ::sampgdk_GetPlayerWeaponData(playerid, slot, weapon, ammo);
}
static inline bool GivePlayerMoney(int playerid, int money) {
  return ::sampgdk_GivePlayerMoney(playerid, money);
}
static inline bool ResetPlayerMoney(int playerid) {
  return ::sampgdk_ResetPlayerMoney(playerid);
}
static inline int SetPlayerName(int playerid, const char * name) {
  return ::sampgdk_SetPlayerName(playerid, name);
}
static inline int GetPlayerMoney(int playerid) {
  return ::sampgdk_GetPlayerMoney(playerid);
}
static inline int GetPlayerState(int playerid) {
  return ::sampgdk_GetPlayerState(playerid);
}
static inline bool GetPlayerIp(int playerid, char * ip, int size) {
  return ::sampgdk_GetPlayerIp(playerid, ip, size);
}
static inline int GetPlayerPing(int playerid) {
  return ::sampgdk_GetPlayerPing(playerid);
}
static inline int GetPlayerWeapon(int playerid) {
  return ::sampgdk_GetPlayerWeapon(playerid);
}
static inline bool GetPlayerKeys(int playerid, int * keys, int * updown, int * leftright) {
  return ::sampgdk_GetPlayerKeys(playerid, keys, updown, leftright);
}
static inline int GetPlayerName(int playerid, char * name, int size) {
  return ::sampgdk_GetPlayerName(playerid, name, size);
}
static inline bool SetPlayerTime(int playerid, int hour, int minute) {
  return ::sampgdk_SetPlayerTime(playerid, hour, minute);
}
static inline bool GetPlayerTime(int playerid, int * hour, int * minute) {
  return ::sampgdk_GetPlayerTime(playerid, hour, minute);
}
static inline bool TogglePlayerClock(int playerid, bool toggle) {
  return ::sampgdk_TogglePlayerClock(playerid, toggle);
}
static inline bool SetPlayerWeather(int playerid, int weather) {
  return ::sampgdk_SetPlayerWeather(playerid, weather);
}
static inline bool ForceClassSelection(int playerid) {
  return ::sampgdk_ForceClassSelection(playerid);
}
static inline bool SetPlayerWantedLevel(int playerid, int level) {
  return ::sampgdk_SetPlayerWantedLevel(playerid, level);
}
static inline int GetPlayerWantedLevel(int playerid) {
  return ::sampgdk_GetPlayerWantedLevel(playerid);
}
static inline bool SetPlayerFightingStyle(int playerid, int style) {
  return ::sampgdk_SetPlayerFightingStyle(playerid, style);
}
static inline int GetPlayerFightingStyle(int playerid) {
  return ::sampgdk_GetPlayerFightingStyle(playerid);
}
static inline bool SetPlayerVelocity(int playerid, float x, float y, float z) {
  return ::sampgdk_SetPlayerVelocity(playerid, x, y, z);
}
static inline bool GetPlayerVelocity(int playerid, float * x, float * y, float * z) {
  return ::sampgdk_GetPlayerVelocity(playerid, x, y, z);
}
static inline bool PlayCrimeReportForPlayer(int playerid, int suspectid, int crime) {
  return ::sampgdk_PlayCrimeReportForPlayer(playerid, suspectid, crime);
}
static inline bool PlayAudioStreamForPlayer(int playerid, const char * url, float posX, float posY, float posZ, float distance, bool usepos) {
  return ::sampgdk_PlayAudioStreamForPlayer(playerid, url, posX, posY, posZ, distance, usepos);
}
static inline bool StopAudioStreamForPlayer(int playerid) {
  return ::sampgdk_StopAudioStreamForPlayer(playerid);
}
static inline bool SetPlayerShopName(int playerid, const char * shopname) {
  return ::sampgdk_SetPlayerShopName(playerid, shopname);
}
static inline bool SetPlayerSkillLevel(int playerid, int skill, int level) {
  return ::sampgdk_SetPlayerSkillLevel(playerid, skill, level);
}
static inline int GetPlayerSurfingVehicleID(int playerid) {
  return ::sampgdk_GetPlayerSurfingVehicleID(playerid);
}
static inline int GetPlayerSurfingObjectID(int playerid) {
  return ::sampgdk_GetPlayerSurfingObjectID(playerid);
}
static inline bool RemoveBuildingForPlayer(int playerid, int modelid, float fX, float fY, float fZ, float fRadius) {
  return ::sampgdk_RemoveBuildingForPlayer(playerid, modelid, fX, fY, fZ, fRadius);
}
static inline bool SetPlayerAttachedObject(int playerid, int index, int modelid, int bone, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX, float fScaleY, float fScaleZ, int materialcolor1, int materialcolor2) {
  return ::sampgdk_SetPlayerAttachedObject(playerid, index, modelid, bone, fOffsetX, fOffsetY, fOffsetZ, fRotX, fRotY, fRotZ, fScaleX, fScaleY, fScaleZ, materialcolor1, materialcolor2);
}
static inline bool RemovePlayerAttachedObject(int playerid, int index) {
  return ::sampgdk_RemovePlayerAttachedObject(playerid, index);
}
static inline bool IsPlayerAttachedObjectSlotUsed(int playerid, int index) {
  return ::sampgdk_IsPlayerAttachedObjectSlotUsed(playerid, index);
}
static inline bool EditAttachedObject(int playerid, int index) {
  return ::sampgdk_EditAttachedObject(playerid, index);
}
static inline int CreatePlayerTextDraw(int playerid, float x, float y, const char * text) {
  return ::sampgdk_CreatePlayerTextDraw(playerid, x, y, text);
}
static inline bool PlayerTextDrawDestroy(int playerid, int text) {
  return ::sampgdk_PlayerTextDrawDestroy(playerid, text);
}
static inline bool PlayerTextDrawLetterSize(int playerid, int text, float x, float y) {
  return ::sampgdk_PlayerTextDrawLetterSize(playerid, text, x, y);
}
static inline bool PlayerTextDrawTextSize(int playerid, int text, float x, float y) {
  return ::sampgdk_PlayerTextDrawTextSize(playerid, text, x, y);
}
static inline bool PlayerTextDrawAlignment(int playerid, int text, int alignment) {
  return ::sampgdk_PlayerTextDrawAlignment(playerid, text, alignment);
}
static inline bool PlayerTextDrawColor(int playerid, int text, int color) {
  return ::sampgdk_PlayerTextDrawColor(playerid, text, color);
}
static inline bool PlayerTextDrawUseBox(int playerid, int text, bool use) {
  return ::sampgdk_PlayerTextDrawUseBox(playerid, text, use);
}
static inline bool PlayerTextDrawBoxColor(int playerid, int text, int color) {
  return ::sampgdk_PlayerTextDrawBoxColor(playerid, text, color);
}
static inline bool PlayerTextDrawSetShadow(int playerid, int text, int size) {
  return ::sampgdk_PlayerTextDrawSetShadow(playerid, text, size);
}
static inline bool PlayerTextDrawSetOutline(int playerid, int text, int size) {
  return ::sampgdk_PlayerTextDrawSetOutline(playerid, text, size);
}
static inline bool PlayerTextDrawBackgroundColor(int playerid, int text, int color) {
  return ::sampgdk_PlayerTextDrawBackgroundColor(playerid, text, color);
}
static inline bool PlayerTextDrawFont(int playerid, int text, int font) {
  return ::sampgdk_PlayerTextDrawFont(playerid, text, font);
}
static inline bool PlayerTextDrawSetProportional(int playerid, int text, bool set) {
  return ::sampgdk_PlayerTextDrawSetProportional(playerid, text, set);
}
static inline bool PlayerTextDrawSetSelectable(int playerid, int text, bool set) {
  return ::sampgdk_PlayerTextDrawSetSelectable(playerid, text, set);
}
static inline bool PlayerTextDrawShow(int playerid, int text) {
  return ::sampgdk_PlayerTextDrawShow(playerid, text);
}
static inline bool PlayerTextDrawHide(int playerid, int text) {
  return ::sampgdk_PlayerTextDrawHide(playerid, text);
}
static inline bool PlayerTextDrawSetString(int playerid, int text, const char * string) {
  return ::sampgdk_PlayerTextDrawSetString(playerid, text, string);
}
static inline bool PlayerTextDrawSetPreviewModel(int playerid, int text, int modelindex) {
  return ::sampgdk_PlayerTextDrawSetPreviewModel(playerid, text, modelindex);
}
static inline bool PlayerTextDrawSetPreviewRot(int playerid, int text, float fRotX, float fRotY, float fRotZ, float fZoom) {
  return ::sampgdk_PlayerTextDrawSetPreviewRot(playerid, text, fRotX, fRotY, fRotZ, fZoom);
}
static inline bool PlayerTextDrawSetPreviewVehCol(int playerid, int text, int color1, int color2) {
  return ::sampgdk_PlayerTextDrawSetPreviewVehCol(playerid, text, color1, color2);
}
static inline bool SetPVarInt(int playerid, const char * varname, int value) {
  return ::sampgdk_SetPVarInt(playerid, varname, value);
}
static inline int GetPVarInt(int playerid, const char * varname) {
  return ::sampgdk_GetPVarInt(playerid, varname);
}
static inline bool SetPVarString(int playerid, const char * varname, const char * value) {
  return ::sampgdk_SetPVarString(playerid, varname, value);
}
static inline bool GetPVarString(int playerid, const char * varname, char * value, int size) {
  return ::sampgdk_GetPVarString(playerid, varname, value, size);
}
static inline bool SetPVarFloat(int playerid, const char * varname, float value) {
  return ::sampgdk_SetPVarFloat(playerid, varname, value);
}
static inline float GetPVarFloat(int playerid, const char * varname) {
  return ::sampgdk_GetPVarFloat(playerid, varname);
}
static inline bool DeletePVar(int playerid, const char * varname) {
  return ::sampgdk_DeletePVar(playerid, varname);
}
static inline int GetPVarsUpperIndex(int playerid) {
  return ::sampgdk_GetPVarsUpperIndex(playerid);
}
static inline bool GetPVarNameAtIndex(int playerid, int index, char * varname, int size) {
  return ::sampgdk_GetPVarNameAtIndex(playerid, index, varname, size);
}
static inline int GetPVarType(int playerid, const char * varname) {
  return ::sampgdk_GetPVarType(playerid, varname);
}
static inline bool SetPlayerChatBubble(int playerid, const char * text, int color, float drawdistance, int expiretime) {
  return ::sampgdk_SetPlayerChatBubble(playerid, text, color, drawdistance, expiretime);
}
static inline bool PutPlayerInVehicle(int playerid, int vehicleid, int seatid) {
  return ::sampgdk_PutPlayerInVehicle(playerid, vehicleid, seatid);
}
static inline int GetPlayerVehicleID(int playerid) {
  return ::sampgdk_GetPlayerVehicleID(playerid);
}
static inline int GetPlayerVehicleSeat(int playerid) {
  return ::sampgdk_GetPlayerVehicleSeat(playerid);
}
static inline bool RemovePlayerFromVehicle(int playerid) {
  return ::sampgdk_RemovePlayerFromVehicle(playerid);
}
static inline bool TogglePlayerControllable(int playerid, bool toggle) {
  return ::sampgdk_TogglePlayerControllable(playerid, toggle);
}
static inline bool PlayerPlaySound(int playerid, int soundid, float x, float y, float z) {
  return ::sampgdk_PlayerPlaySound(playerid, soundid, x, y, z);
}
static inline bool ApplyAnimation(int playerid, const char * animlib, const char * animname, float fDelta, bool loop, bool lockx, bool locky, bool freeze, int time, bool forcesync) {
  return ::sampgdk_ApplyAnimation(playerid, animlib, animname, fDelta, loop, lockx, locky, freeze, time, forcesync);
}
static inline bool ClearAnimations(int playerid, bool forcesync) {
  return ::sampgdk_ClearAnimations(playerid, forcesync);
}
static inline int GetPlayerAnimationIndex(int playerid) {
  return ::sampgdk_GetPlayerAnimationIndex(playerid);
}
static inline bool GetAnimationName(int index, char * animlib, int animlib_size, char * animname, int animname_size) {
  return ::sampgdk_GetAnimationName(index, animlib, animlib_size, animname, animname_size);
}
static inline int GetPlayerSpecialAction(int playerid) {
  return ::sampgdk_GetPlayerSpecialAction(playerid);
}
static inline bool SetPlayerSpecialAction(int playerid, int actionid) {
  return ::sampgdk_SetPlayerSpecialAction(playerid, actionid);
}
static inline bool SetPlayerCheckpoint(int playerid, float x, float y, float z, float size) {
  return ::sampgdk_SetPlayerCheckpoint(playerid, x, y, z, size);
}
static inline bool DisablePlayerCheckpoint(int playerid) {
  return ::sampgdk_DisablePlayerCheckpoint(playerid);
}
static inline bool SetPlayerRaceCheckpoint(int playerid, int type, float x, float y, float z, float nextx, float nexty, float nextz, float size) {
  return ::sampgdk_SetPlayerRaceCheckpoint(playerid, type, x, y, z, nextx, nexty, nextz, size);
}
static inline bool DisablePlayerRaceCheckpoint(int playerid) {
  return ::sampgdk_DisablePlayerRaceCheckpoint(playerid);
}
static inline bool SetPlayerWorldBounds(int playerid, float x_max, float x_min, float y_max, float y_min) {
  return ::sampgdk_SetPlayerWorldBounds(playerid, x_max, x_min, y_max, y_min);
}
static inline bool SetPlayerMarkerForPlayer(int playerid, int showplayerid, int color) {
  return ::sampgdk_SetPlayerMarkerForPlayer(playerid, showplayerid, color);
}
static inline bool ShowPlayerNameTagForPlayer(int playerid, int showplayerid, bool show) {
  return ::sampgdk_ShowPlayerNameTagForPlayer(playerid, showplayerid, show);
}
static inline bool SetPlayerMapIcon(int playerid, int iconid, float x, float y, float z, int markertype, int color, int style) {
  return ::sampgdk_SetPlayerMapIcon(playerid, iconid, x, y, z, markertype, color, style);
}
static inline bool RemovePlayerMapIcon(int playerid, int iconid) {
  return ::sampgdk_RemovePlayerMapIcon(playerid, iconid);
}
static inline bool AllowPlayerTeleport(int playerid, bool allow) {
  return ::sampgdk_AllowPlayerTeleport(playerid, allow);
}
static inline bool SetPlayerCameraPos(int playerid, float x, float y, float z) {
  return ::sampgdk_SetPlayerCameraPos(playerid, x, y, z);
}
static inline bool SetPlayerCameraLookAt(int playerid, float x, float y, float z, int cut) {
  return ::sampgdk_SetPlayerCameraLookAt(playerid, x, y, z, cut);
}
static inline bool SetCameraBehindPlayer(int playerid) {
  return ::sampgdk_SetCameraBehindPlayer(playerid);
}
static inline bool GetPlayerCameraPos(int playerid, float * x, float * y, float * z) {
  return ::sampgdk_GetPlayerCameraPos(playerid, x, y, z);
}
static inline bool GetPlayerCameraFrontVector(int playerid, float * x, float * y, float * z) {
  return ::sampgdk_GetPlayerCameraFrontVector(playerid, x, y, z);
}
static inline int GetPlayerCameraMode(int playerid) {
  return ::sampgdk_GetPlayerCameraMode(playerid);
}
static inline bool AttachCameraToObject(int playerid, int objectid) {
  return ::sampgdk_AttachCameraToObject(playerid, objectid);
}
static inline bool AttachCameraToPlayerObject(int playerid, int playerobjectid) {
  return ::sampgdk_AttachCameraToPlayerObject(playerid, playerobjectid);
}
static inline bool InterpolateCameraPos(int playerid, float FromX, float FromY, float FromZ, float ToX, float ToY, float ToZ, int time, int cut) {
  return ::sampgdk_InterpolateCameraPos(playerid, FromX, FromY, FromZ, ToX, ToY, ToZ, time, cut);
}
static inline bool InterpolateCameraLookAt(int playerid, float FromX, float FromY, float FromZ, float ToX, float ToY, float ToZ, int time, int cut) {
  return ::sampgdk_InterpolateCameraLookAt(playerid, FromX, FromY, FromZ, ToX, ToY, ToZ, time, cut);
}
static inline bool IsPlayerConnected(int playerid) {
  return ::sampgdk_IsPlayerConnected(playerid);
}
static inline bool IsPlayerInVehicle(int playerid, int vehicleid) {
  return ::sampgdk_IsPlayerInVehicle(playerid, vehicleid);
}
static inline bool IsPlayerInAnyVehicle(int playerid) {
  return ::sampgdk_IsPlayerInAnyVehicle(playerid);
}
static inline bool IsPlayerInCheckpoint(int playerid) {
  return ::sampgdk_IsPlayerInCheckpoint(playerid);
}
static inline bool IsPlayerInRaceCheckpoint(int playerid) {
  return ::sampgdk_IsPlayerInRaceCheckpoint(playerid);
}
static inline bool SetPlayerVirtualWorld(int playerid, int worldid) {
  return ::sampgdk_SetPlayerVirtualWorld(playerid, worldid);
}
static inline int GetPlayerVirtualWorld(int playerid) {
  return ::sampgdk_GetPlayerVirtualWorld(playerid);
}
static inline bool EnableStuntBonusForPlayer(int playerid, bool enable) {
  return ::sampgdk_EnableStuntBonusForPlayer(playerid, enable);
}
static inline bool EnableStuntBonusForAll(bool enable) {
  return ::sampgdk_EnableStuntBonusForAll(enable);
}
static inline bool TogglePlayerSpectating(int playerid, bool toggle) {
  return ::sampgdk_TogglePlayerSpectating(playerid, toggle);
}
static inline bool PlayerSpectatePlayer(int playerid, int targetplayerid, int mode) {
  return ::sampgdk_PlayerSpectatePlayer(playerid, targetplayerid, mode);
}
static inline bool PlayerSpectateVehicle(int playerid, int targetvehicleid, int mode) {
  return ::sampgdk_PlayerSpectateVehicle(playerid, targetvehicleid, mode);
}
static inline bool StartRecordingPlayerData(int playerid, int recordtype, const char * recordname) {
  return ::sampgdk_StartRecordingPlayerData(playerid, recordtype, recordname);
}
static inline bool StopRecordingPlayerData(int playerid) {
  return ::sampgdk_StopRecordingPlayerData(playerid);
}

SAMPGDK_END_NAMESPACE

#endif /* __cplusplus */

