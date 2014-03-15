SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SendClientMessage(int playerid, int color, const char * message);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SendClientMessageToAll(int color, const char * message);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SendPlayerMessageToPlayer(int playerid, int senderid, const char * message);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SendPlayerMessageToAll(int senderid, const char * message);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SendDeathMessage(int killer, int killee, int weapon);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GameTextForAll(const char * text, int time, int style);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GameTextForPlayer(int playerid, const char * text, int time, int style);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetTickCount();
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetMaxPlayers();
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetGameModeText(const char * text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetTeamCount(int count);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_AddPlayerClass(int modelid, float spawn_x, float spawn_y, float spawn_z, float z_angle, int weapon1, int weapon1_ammo, int weapon2, int weapon2_ammo, int weapon3, int weapon3_ammo);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_AddPlayerClassEx(int teamid, int modelid, float spawn_x, float spawn_y, float spawn_z, float z_angle, int weapon1, int weapon1_ammo, int weapon2, int weapon2_ammo, int weapon3, int weapon3_ammo);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_AddStaticVehicle(int modelid, float spawn_x, float spawn_y, float spawn_z, float z_angle, int color1, int color2);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_AddStaticVehicleEx(int modelid, float spawn_x, float spawn_y, float spawn_z, float z_angle, int color1, int color2, int respawn_delay);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_AddStaticPickup(int model, int type, float x, float y, float z, int virtualworld);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_CreatePickup(int model, int type, float x, float y, float z, int virtualworld);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DestroyPickup(int pickup);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ShowNameTags(bool show);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ShowPlayerMarkers(int mode);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GameModeExit();
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetWorldTime(int hour);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetWeaponName(int weaponid, char * name, int size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_EnableTirePopping(bool enable);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_EnableVehicleFriendlyFire();
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AllowInteriorWeapons(bool allow);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetWeather(int weatherid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetGravity(float gravity);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_AllowAdminTeleport(bool allow);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetDeathDropAmount(int amount);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_CreateExplosion(float x, float y, float z, int type, float radius);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_EnableZoneNames(bool enable);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_UsePlayerPedAnims();
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DisableInteriorEnterExits();
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetNameTagDrawDistance(float distance);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DisableNameTagLOS();
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_LimitGlobalChatRadius(float chat_radius);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_LimitPlayerMarkerRadius(float marker_radius);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ConnectNPC(const char * name, const char * script);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerNPC(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsPlayerAdmin(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_Kick(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_Ban(int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_BanEx(int playerid, const char * reason);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SendRconCommand(const char * command);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetServerVarAsString(const char * varname, char * value, int size);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetServerVarAsInt(const char * varname);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetServerVarAsBool(const char * varname);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerNetworkStats(int playerid, char * retstr, int size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetNetworkStats(char * retstr, int size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GetPlayerVersion(int playerid, char * version, int len);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_CreateMenu(const char * title, int columns, float x, float y, float col1width, float col2width);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DestroyMenu(int menuid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_AddMenuItem(int menuid, int column, const char * menutext);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SetMenuColumnHeader(int menuid, int column, const char * columnheader);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ShowMenuForPlayer(int menuid, int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_HideMenuForPlayer(int menuid, int playerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_IsValidMenu(int menuid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DisableMenu(int menuid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DisableMenuRow(int menuid, int row);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GetPlayerMenu(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_TextDrawCreate(float x, float y, const char * text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawDestroy(int text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawLetterSize(int text, float x, float y);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawTextSize(int text, float x, float y);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawAlignment(int text, int alignment);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawColor(int text, int color);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawUseBox(int text, bool use);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawBoxColor(int text, int color);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawSetShadow(int text, int size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawSetOutline(int text, int size);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawBackgroundColor(int text, int color);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawFont(int text, int font);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawSetProportional(int text, bool set);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawSetSelectable(int text, bool set);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawShowForPlayer(int playerid, int text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawHideForPlayer(int playerid, int text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawShowForAll(int text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawHideForAll(int text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawSetString(int text, const char * string);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawSetPreviewModel(int text, int modelindex);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawSetPreviewRot(int text, float fRotX, float fRotY, float fRotZ, float fZoom);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_TextDrawSetPreviewVehCol(int text, int color1, int color2);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_SelectTextDraw(int playerid, int hovercolor);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_CancelSelectTextDraw(int playerid);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_GangZoneCreate(float minx, float miny, float maxx, float maxy);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GangZoneDestroy(int zone);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GangZoneShowForPlayer(int playerid, int zone, int color);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GangZoneShowForAll(int zone, int color);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GangZoneHideForPlayer(int playerid, int zone);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GangZoneHideForAll(int zone);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GangZoneFlashForPlayer(int playerid, int zone, int flashcolor);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GangZoneFlashForAll(int zone, int flashcolor);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GangZoneStopFlashForPlayer(int playerid, int zone);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_GangZoneStopFlashForAll(int zone);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_Create3DTextLabel(const char * text, int color, float x, float y, float z, float DrawDistance, int virtualworld, bool testLOS);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_Delete3DTextLabel(int id);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_Attach3DTextLabelToPlayer(int id, int playerid, float OffsetX, float OffsetY, float OffsetZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_Attach3DTextLabelToVehicle(int id, int vehicleid, float OffsetX, float OffsetY, float OffsetZ);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_Update3DTextLabelText(int id, int color, const char * text);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_CreatePlayer3DTextLabel(int playerid, const char * text, int color, float x, float y, float z, float DrawDistance, int attachedplayer, int attachedvehicle, bool testLOS);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_DeletePlayer3DTextLabel(int playerid, int id);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_UpdatePlayer3DTextLabelText(int playerid, int id, int color, const char * text);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_ShowPlayerDialog(int playerid, int dialogid, int style, const char * caption, const char * info, const char * button1, const char * button2);
SAMPGDK_NATIVE_EXPORT int SAMPGDK_NATIVE_CALL sampgdk_SetTimer(int interval, bool repeat, TimerCallback callback, void * param);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_KillTimer(int timerid);
SAMPGDK_NATIVE_EXPORT bool SAMPGDK_NATIVE_CALL sampgdk_gpci(int playerid, char * buffer, int size);

#ifndef __cplusplus

#define MAX_PLAYER_NAME (24)
#define MAX_PLAYERS (500)
#define MAX_VEHICLES (2000)
#define INVALID_PLAYER_ID (0xFFFF)
#define INVALID_VEHICLE_ID (0xFFFF)
#define NO_TEAM (255)
#define MAX_OBJECTS (1000)
#define INVALID_OBJECT_ID (0xFFFF)
#define MAX_GANG_ZONES (1024)
#define MAX_TEXT_DRAWS (2048)
#define MAX_PLAYER_TEXT_DRAWS (256)
#define MAX_MENUS (128)
#define MAX_3DTEXT_GLOBAL (1024)
#define MAX_3DTEXT_PLAYER (1024)
#define MAX_PICKUPS (4096)
#define INVALID_MENU (0xFF)
#define INVALID_TEXT_DRAW (0xFFFF)
#define INVALID_GANG_ZONE (-1)
#define INVALID_3DTEXT_ID (0xFFFF)
#define TEXT_DRAW_FONT_SPRITE_DRAW (4)
#define TEXT_DRAW_FONT_MODEL_PREVIEW (5)
#define DIALOG_STYLE_MSGBOX (0)
#define DIALOG_STYLE_INPUT (1)
#define DIALOG_STYLE_LIST (2)
#define DIALOG_STYLE_PASSWORD (3)
#define PLAYER_STATE_NONE (0)
#define PLAYER_STATE_ONFOOT (1)
#define PLAYER_STATE_DRIVER (2)
#define PLAYER_STATE_PASSENGER (3)
#define PLAYER_STATE_EXIT_VEHICLE (4)
#define PLAYER_STATE_ENTER_VEHICLE_DRIVER (5)
#define PLAYER_STATE_ENTER_VEHICLE_PASSENGER (6)
#define PLAYER_STATE_WASTED (7)
#define PLAYER_STATE_SPAWNED (8)
#define PLAYER_STATE_SPECTATING (9)
#define PLAYER_MARKERS_MODE_OFF (0)
#define PLAYER_MARKERS_MODE_GLOBAL (1)
#define PLAYER_MARKERS_MODE_STREAMED (2)
#define WEAPON_BRASSKNUCKLE (1)
#define WEAPON_GOLFCLUB (2)
#define WEAPON_NITESTICK (3)
#define WEAPON_KNIFE (4)
#define WEAPON_BAT (5)
#define WEAPON_SHOVEL (6)
#define WEAPON_POOLSTICK (7)
#define WEAPON_KATANA (8)
#define WEAPON_CHAINSAW (9)
#define WEAPON_DILDO (10)
#define WEAPON_DILDO2 (11)
#define WEAPON_VIBRATOR (12)
#define WEAPON_VIBRATOR2 (13)
#define WEAPON_FLOWER (14)
#define WEAPON_CANE (15)
#define WEAPON_GRENADE (16)
#define WEAPON_TEARGAS (17)
#define WEAPON_MOLTOV (18)
#define WEAPON_COLT45 (22)
#define WEAPON_SILENCED (23)
#define WEAPON_DEAGLE (24)
#define WEAPON_SHOTGUN (25)
#define WEAPON_SAWEDOFF (26)
#define WEAPON_SHOTGSPA (27)
#define WEAPON_UZI (28)
#define WEAPON_MP5 (29)
#define WEAPON_AK47 (30)
#define WEAPON_M4 (31)
#define WEAPON_TEC9 (32)
#define WEAPON_RIFLE (33)
#define WEAPON_SNIPER (34)
#define WEAPON_ROCKETLAUNCHER (35)
#define WEAPON_HEATSEEKER (36)
#define WEAPON_FLAMETHROWER (37)
#define WEAPON_MINIGUN (38)
#define WEAPON_SATCHEL (39)
#define WEAPON_BOMB (40)
#define WEAPON_SPRAYCAN (41)
#define WEAPON_FIREEXTINGUISHER (42)
#define WEAPON_CAMERA (43)
#define WEAPON_PARACHUTE (46)
#define WEAPON_VEHICLE (49)
#define WEAPON_DROWN (53)
#define WEAPON_COLLISION (54)
#define KEY_ACTION (1)
#define KEY_CROUCH (2)
#define KEY_FIRE (4)
#define KEY_SPRINT (8)
#define KEY_SECONDARY_ATTACK (16)
#define KEY_JUMP (32)
#define KEY_LOOK_RIGHT (64)
#define KEY_HANDBRAKE (128)
#define KEY_LOOK_LEFT (256)
#define KEY_SUBMISSION (512)
#define KEY_LOOK_BEHIND (512)
#define KEY_WALK (1024)
#define KEY_ANALOG_UP (2048)
#define KEY_ANALOG_DOWN (4096)
#define KEY_ANALOG_LEFT (8192)
#define KEY_ANALOG_RIGHT (16384)
#define KEY_YES (65536)
#define KEY_NO (131072)
#define KEY_CTRL_BACK (262144)
#define KEY_UP (-128)
#define KEY_DOWN (128)
#define KEY_LEFT (-128)
#define KEY_RIGHT (128)
#define CLICK_SOURCE_SCOREBOARD (0)
#define EDIT_RESPONSE_CANCEL (0)
#define EDIT_RESPONSE_FINAL (1)
#define EDIT_RESPONSE_UPDATE (2)
#define SELECT_OBJECT_GLOBAL_OBJECT (1)
#define SELECT_OBJECT_PLAYER_OBJECT (2)
#define BULLET_HIT_TYPE_NONE (0)
#define BULLET_HIT_TYPE_PLAYER (1)
#define BULLET_HIT_TYPE_VEHICLE (2)
#define BULLET_HIT_TYPE_OBJECT (3)
#define BULLET_HIT_TYPE_PLAYER_OBJECT (4)

#undef  SendClientMessage
#define SendClientMessage sampgdk_SendClientMessage
#undef  SendClientMessageToAll
#define SendClientMessageToAll sampgdk_SendClientMessageToAll
#undef  SendPlayerMessageToPlayer
#define SendPlayerMessageToPlayer sampgdk_SendPlayerMessageToPlayer
#undef  SendPlayerMessageToAll
#define SendPlayerMessageToAll sampgdk_SendPlayerMessageToAll
#undef  SendDeathMessage
#define SendDeathMessage sampgdk_SendDeathMessage
#undef  GameTextForAll
#define GameTextForAll sampgdk_GameTextForAll
#undef  GameTextForPlayer
#define GameTextForPlayer sampgdk_GameTextForPlayer
#undef  GetTickCount
#define GetTickCount sampgdk_GetTickCount
#undef  GetMaxPlayers
#define GetMaxPlayers sampgdk_GetMaxPlayers
#undef  SetGameModeText
#define SetGameModeText sampgdk_SetGameModeText
#undef  SetTeamCount
#define SetTeamCount sampgdk_SetTeamCount
#undef  AddPlayerClass
#define AddPlayerClass sampgdk_AddPlayerClass
#undef  AddPlayerClassEx
#define AddPlayerClassEx sampgdk_AddPlayerClassEx
#undef  AddStaticVehicle
#define AddStaticVehicle sampgdk_AddStaticVehicle
#undef  AddStaticVehicleEx
#define AddStaticVehicleEx sampgdk_AddStaticVehicleEx
#undef  AddStaticPickup
#define AddStaticPickup sampgdk_AddStaticPickup
#undef  CreatePickup
#define CreatePickup sampgdk_CreatePickup
#undef  DestroyPickup
#define DestroyPickup sampgdk_DestroyPickup
#undef  ShowNameTags
#define ShowNameTags sampgdk_ShowNameTags
#undef  ShowPlayerMarkers
#define ShowPlayerMarkers sampgdk_ShowPlayerMarkers
#undef  GameModeExit
#define GameModeExit sampgdk_GameModeExit
#undef  SetWorldTime
#define SetWorldTime sampgdk_SetWorldTime
#undef  GetWeaponName
#define GetWeaponName sampgdk_GetWeaponName
#undef  EnableTirePopping
#define EnableTirePopping sampgdk_EnableTirePopping
#undef  EnableVehicleFriendlyFire
#define EnableVehicleFriendlyFire sampgdk_EnableVehicleFriendlyFire
#undef  AllowInteriorWeapons
#define AllowInteriorWeapons sampgdk_AllowInteriorWeapons
#undef  SetWeather
#define SetWeather sampgdk_SetWeather
#undef  SetGravity
#define SetGravity sampgdk_SetGravity
#undef  AllowAdminTeleport
#define AllowAdminTeleport sampgdk_AllowAdminTeleport
#undef  SetDeathDropAmount
#define SetDeathDropAmount sampgdk_SetDeathDropAmount
#undef  CreateExplosion
#define CreateExplosion sampgdk_CreateExplosion
#undef  EnableZoneNames
#define EnableZoneNames sampgdk_EnableZoneNames
#undef  UsePlayerPedAnims
#define UsePlayerPedAnims sampgdk_UsePlayerPedAnims
#undef  DisableInteriorEnterExits
#define DisableInteriorEnterExits sampgdk_DisableInteriorEnterExits
#undef  SetNameTagDrawDistance
#define SetNameTagDrawDistance sampgdk_SetNameTagDrawDistance
#undef  DisableNameTagLOS
#define DisableNameTagLOS sampgdk_DisableNameTagLOS
#undef  LimitGlobalChatRadius
#define LimitGlobalChatRadius sampgdk_LimitGlobalChatRadius
#undef  LimitPlayerMarkerRadius
#define LimitPlayerMarkerRadius sampgdk_LimitPlayerMarkerRadius
#undef  ConnectNPC
#define ConnectNPC sampgdk_ConnectNPC
#undef  IsPlayerNPC
#define IsPlayerNPC sampgdk_IsPlayerNPC
#undef  IsPlayerAdmin
#define IsPlayerAdmin sampgdk_IsPlayerAdmin
#undef  Kick
#define Kick sampgdk_Kick
#undef  Ban
#define Ban sampgdk_Ban
#undef  BanEx
#define BanEx sampgdk_BanEx
#undef  SendRconCommand
#define SendRconCommand sampgdk_SendRconCommand
#undef  GetServerVarAsString
#define GetServerVarAsString sampgdk_GetServerVarAsString
#undef  GetServerVarAsInt
#define GetServerVarAsInt sampgdk_GetServerVarAsInt
#undef  GetServerVarAsBool
#define GetServerVarAsBool sampgdk_GetServerVarAsBool
#undef  GetPlayerNetworkStats
#define GetPlayerNetworkStats sampgdk_GetPlayerNetworkStats
#undef  GetNetworkStats
#define GetNetworkStats sampgdk_GetNetworkStats
#undef  GetPlayerVersion
#define GetPlayerVersion sampgdk_GetPlayerVersion
#undef  CreateMenu
#define CreateMenu sampgdk_CreateMenu
#undef  DestroyMenu
#define DestroyMenu sampgdk_DestroyMenu
#undef  AddMenuItem
#define AddMenuItem sampgdk_AddMenuItem
#undef  SetMenuColumnHeader
#define SetMenuColumnHeader sampgdk_SetMenuColumnHeader
#undef  ShowMenuForPlayer
#define ShowMenuForPlayer sampgdk_ShowMenuForPlayer
#undef  HideMenuForPlayer
#define HideMenuForPlayer sampgdk_HideMenuForPlayer
#undef  IsValidMenu
#define IsValidMenu sampgdk_IsValidMenu
#undef  DisableMenu
#define DisableMenu sampgdk_DisableMenu
#undef  DisableMenuRow
#define DisableMenuRow sampgdk_DisableMenuRow
#undef  GetPlayerMenu
#define GetPlayerMenu sampgdk_GetPlayerMenu
#undef  TextDrawCreate
#define TextDrawCreate sampgdk_TextDrawCreate
#undef  TextDrawDestroy
#define TextDrawDestroy sampgdk_TextDrawDestroy
#undef  TextDrawLetterSize
#define TextDrawLetterSize sampgdk_TextDrawLetterSize
#undef  TextDrawTextSize
#define TextDrawTextSize sampgdk_TextDrawTextSize
#undef  TextDrawAlignment
#define TextDrawAlignment sampgdk_TextDrawAlignment
#undef  TextDrawColor
#define TextDrawColor sampgdk_TextDrawColor
#undef  TextDrawUseBox
#define TextDrawUseBox sampgdk_TextDrawUseBox
#undef  TextDrawBoxColor
#define TextDrawBoxColor sampgdk_TextDrawBoxColor
#undef  TextDrawSetShadow
#define TextDrawSetShadow sampgdk_TextDrawSetShadow
#undef  TextDrawSetOutline
#define TextDrawSetOutline sampgdk_TextDrawSetOutline
#undef  TextDrawBackgroundColor
#define TextDrawBackgroundColor sampgdk_TextDrawBackgroundColor
#undef  TextDrawFont
#define TextDrawFont sampgdk_TextDrawFont
#undef  TextDrawSetProportional
#define TextDrawSetProportional sampgdk_TextDrawSetProportional
#undef  TextDrawSetSelectable
#define TextDrawSetSelectable sampgdk_TextDrawSetSelectable
#undef  TextDrawShowForPlayer
#define TextDrawShowForPlayer sampgdk_TextDrawShowForPlayer
#undef  TextDrawHideForPlayer
#define TextDrawHideForPlayer sampgdk_TextDrawHideForPlayer
#undef  TextDrawShowForAll
#define TextDrawShowForAll sampgdk_TextDrawShowForAll
#undef  TextDrawHideForAll
#define TextDrawHideForAll sampgdk_TextDrawHideForAll
#undef  TextDrawSetString
#define TextDrawSetString sampgdk_TextDrawSetString
#undef  TextDrawSetPreviewModel
#define TextDrawSetPreviewModel sampgdk_TextDrawSetPreviewModel
#undef  TextDrawSetPreviewRot
#define TextDrawSetPreviewRot sampgdk_TextDrawSetPreviewRot
#undef  TextDrawSetPreviewVehCol
#define TextDrawSetPreviewVehCol sampgdk_TextDrawSetPreviewVehCol
#undef  SelectTextDraw
#define SelectTextDraw sampgdk_SelectTextDraw
#undef  CancelSelectTextDraw
#define CancelSelectTextDraw sampgdk_CancelSelectTextDraw
#undef  GangZoneCreate
#define GangZoneCreate sampgdk_GangZoneCreate
#undef  GangZoneDestroy
#define GangZoneDestroy sampgdk_GangZoneDestroy
#undef  GangZoneShowForPlayer
#define GangZoneShowForPlayer sampgdk_GangZoneShowForPlayer
#undef  GangZoneShowForAll
#define GangZoneShowForAll sampgdk_GangZoneShowForAll
#undef  GangZoneHideForPlayer
#define GangZoneHideForPlayer sampgdk_GangZoneHideForPlayer
#undef  GangZoneHideForAll
#define GangZoneHideForAll sampgdk_GangZoneHideForAll
#undef  GangZoneFlashForPlayer
#define GangZoneFlashForPlayer sampgdk_GangZoneFlashForPlayer
#undef  GangZoneFlashForAll
#define GangZoneFlashForAll sampgdk_GangZoneFlashForAll
#undef  GangZoneStopFlashForPlayer
#define GangZoneStopFlashForPlayer sampgdk_GangZoneStopFlashForPlayer
#undef  GangZoneStopFlashForAll
#define GangZoneStopFlashForAll sampgdk_GangZoneStopFlashForAll
#undef  Create3DTextLabel
#define Create3DTextLabel sampgdk_Create3DTextLabel
#undef  Delete3DTextLabel
#define Delete3DTextLabel sampgdk_Delete3DTextLabel
#undef  Attach3DTextLabelToPlayer
#define Attach3DTextLabelToPlayer sampgdk_Attach3DTextLabelToPlayer
#undef  Attach3DTextLabelToVehicle
#define Attach3DTextLabelToVehicle sampgdk_Attach3DTextLabelToVehicle
#undef  Update3DTextLabelText
#define Update3DTextLabelText sampgdk_Update3DTextLabelText
#undef  CreatePlayer3DTextLabel
#define CreatePlayer3DTextLabel sampgdk_CreatePlayer3DTextLabel
#undef  DeletePlayer3DTextLabel
#define DeletePlayer3DTextLabel sampgdk_DeletePlayer3DTextLabel
#undef  UpdatePlayer3DTextLabelText
#define UpdatePlayer3DTextLabelText sampgdk_UpdatePlayer3DTextLabelText
#undef  ShowPlayerDialog
#define ShowPlayerDialog sampgdk_ShowPlayerDialog
#undef  SetTimer
#define SetTimer sampgdk_SetTimer
#undef  KillTimer
#define KillTimer sampgdk_KillTimer
#undef  gpci
#define gpci sampgdk_gpci

#else /* __cplusplus */

SAMPGDK_BEGIN_NAMESPACE

const int MAX_PLAYER_NAME = 24;
const int MAX_PLAYERS = 500;
const int MAX_VEHICLES = 2000;
const int INVALID_PLAYER_ID = 0xFFFF;
const int INVALID_VEHICLE_ID = 0xFFFF;
const int NO_TEAM = 255;
const int MAX_OBJECTS = 1000;
const int INVALID_OBJECT_ID = 0xFFFF;
const int MAX_GANG_ZONES = 1024;
const int MAX_TEXT_DRAWS = 2048;
const int MAX_PLAYER_TEXT_DRAWS = 256;
const int MAX_MENUS = 128;
const int MAX_3DTEXT_GLOBAL = 1024;
const int MAX_3DTEXT_PLAYER = 1024;
const int MAX_PICKUPS = 4096;
const int INVALID_MENU = 0xFF;
const int INVALID_TEXT_DRAW = 0xFFFF;
const int INVALID_GANG_ZONE = -1;
const int INVALID_3DTEXT_ID = 0xFFFF;
const int TEXT_DRAW_FONT_SPRITE_DRAW = 4;
const int TEXT_DRAW_FONT_MODEL_PREVIEW = 5;
const int DIALOG_STYLE_MSGBOX = 0;
const int DIALOG_STYLE_INPUT = 1;
const int DIALOG_STYLE_LIST = 2;
const int DIALOG_STYLE_PASSWORD = 3;
const int PLAYER_STATE_NONE = 0;
const int PLAYER_STATE_ONFOOT = 1;
const int PLAYER_STATE_DRIVER = 2;
const int PLAYER_STATE_PASSENGER = 3;
const int PLAYER_STATE_EXIT_VEHICLE = 4;
const int PLAYER_STATE_ENTER_VEHICLE_DRIVER = 5;
const int PLAYER_STATE_ENTER_VEHICLE_PASSENGER = 6;
const int PLAYER_STATE_WASTED = 7;
const int PLAYER_STATE_SPAWNED = 8;
const int PLAYER_STATE_SPECTATING = 9;
const int PLAYER_MARKERS_MODE_OFF = 0;
const int PLAYER_MARKERS_MODE_GLOBAL = 1;
const int PLAYER_MARKERS_MODE_STREAMED = 2;
const int WEAPON_BRASSKNUCKLE = 1;
const int WEAPON_GOLFCLUB = 2;
const int WEAPON_NITESTICK = 3;
const int WEAPON_KNIFE = 4;
const int WEAPON_BAT = 5;
const int WEAPON_SHOVEL = 6;
const int WEAPON_POOLSTICK = 7;
const int WEAPON_KATANA = 8;
const int WEAPON_CHAINSAW = 9;
const int WEAPON_DILDO = 10;
const int WEAPON_DILDO2 = 11;
const int WEAPON_VIBRATOR = 12;
const int WEAPON_VIBRATOR2 = 13;
const int WEAPON_FLOWER = 14;
const int WEAPON_CANE = 15;
const int WEAPON_GRENADE = 16;
const int WEAPON_TEARGAS = 17;
const int WEAPON_MOLTOV = 18;
const int WEAPON_COLT45 = 22;
const int WEAPON_SILENCED = 23;
const int WEAPON_DEAGLE = 24;
const int WEAPON_SHOTGUN = 25;
const int WEAPON_SAWEDOFF = 26;
const int WEAPON_SHOTGSPA = 27;
const int WEAPON_UZI = 28;
const int WEAPON_MP5 = 29;
const int WEAPON_AK47 = 30;
const int WEAPON_M4 = 31;
const int WEAPON_TEC9 = 32;
const int WEAPON_RIFLE = 33;
const int WEAPON_SNIPER = 34;
const int WEAPON_ROCKETLAUNCHER = 35;
const int WEAPON_HEATSEEKER = 36;
const int WEAPON_FLAMETHROWER = 37;
const int WEAPON_MINIGUN = 38;
const int WEAPON_SATCHEL = 39;
const int WEAPON_BOMB = 40;
const int WEAPON_SPRAYCAN = 41;
const int WEAPON_FIREEXTINGUISHER = 42;
const int WEAPON_CAMERA = 43;
const int WEAPON_PARACHUTE = 46;
const int WEAPON_VEHICLE = 49;
const int WEAPON_DROWN = 53;
const int WEAPON_COLLISION = 54;
const int KEY_ACTION = 1;
const int KEY_CROUCH = 2;
const int KEY_FIRE = 4;
const int KEY_SPRINT = 8;
const int KEY_SECONDARY_ATTACK = 16;
const int KEY_JUMP = 32;
const int KEY_LOOK_RIGHT = 64;
const int KEY_HANDBRAKE = 128;
const int KEY_LOOK_LEFT = 256;
const int KEY_SUBMISSION = 512;
const int KEY_LOOK_BEHIND = 512;
const int KEY_WALK = 1024;
const int KEY_ANALOG_UP = 2048;
const int KEY_ANALOG_DOWN = 4096;
const int KEY_ANALOG_LEFT = 8192;
const int KEY_ANALOG_RIGHT = 16384;
const int KEY_YES = 65536;
const int KEY_NO = 131072;
const int KEY_CTRL_BACK = 262144;
const int KEY_UP = -128;
const int KEY_DOWN = 128;
const int KEY_LEFT = -128;
const int KEY_RIGHT = 128;
const int CLICK_SOURCE_SCOREBOARD = 0;
const int EDIT_RESPONSE_CANCEL = 0;
const int EDIT_RESPONSE_FINAL = 1;
const int EDIT_RESPONSE_UPDATE = 2;
const int SELECT_OBJECT_GLOBAL_OBJECT = 1;
const int SELECT_OBJECT_PLAYER_OBJECT = 2;
const int BULLET_HIT_TYPE_NONE = 0;
const int BULLET_HIT_TYPE_PLAYER = 1;
const int BULLET_HIT_TYPE_VEHICLE = 2;
const int BULLET_HIT_TYPE_OBJECT = 3;
const int BULLET_HIT_TYPE_PLAYER_OBJECT = 4;

static inline bool SendClientMessage(int playerid, int color, const char * message) {
  return ::sampgdk_SendClientMessage(playerid, color, message);
}
static inline bool SendClientMessageToAll(int color, const char * message) {
  return ::sampgdk_SendClientMessageToAll(color, message);
}
static inline bool SendPlayerMessageToPlayer(int playerid, int senderid, const char * message) {
  return ::sampgdk_SendPlayerMessageToPlayer(playerid, senderid, message);
}
static inline bool SendPlayerMessageToAll(int senderid, const char * message) {
  return ::sampgdk_SendPlayerMessageToAll(senderid, message);
}
static inline bool SendDeathMessage(int killer, int killee, int weapon) {
  return ::sampgdk_SendDeathMessage(killer, killee, weapon);
}
static inline bool GameTextForAll(const char * text, int time, int style) {
  return ::sampgdk_GameTextForAll(text, time, style);
}
static inline bool GameTextForPlayer(int playerid, const char * text, int time, int style) {
  return ::sampgdk_GameTextForPlayer(playerid, text, time, style);
}
static inline int GetTickCount() {
  return ::sampgdk_GetTickCount();
}
static inline int GetMaxPlayers() {
  return ::sampgdk_GetMaxPlayers();
}
static inline bool SetGameModeText(const char * text) {
  return ::sampgdk_SetGameModeText(text);
}
static inline bool SetTeamCount(int count) {
  return ::sampgdk_SetTeamCount(count);
}
static inline int AddPlayerClass(int modelid, float spawn_x, float spawn_y, float spawn_z, float z_angle, int weapon1, int weapon1_ammo, int weapon2, int weapon2_ammo, int weapon3, int weapon3_ammo) {
  return ::sampgdk_AddPlayerClass(modelid, spawn_x, spawn_y, spawn_z, z_angle, weapon1, weapon1_ammo, weapon2, weapon2_ammo, weapon3, weapon3_ammo);
}
static inline int AddPlayerClassEx(int teamid, int modelid, float spawn_x, float spawn_y, float spawn_z, float z_angle, int weapon1, int weapon1_ammo, int weapon2, int weapon2_ammo, int weapon3, int weapon3_ammo) {
  return ::sampgdk_AddPlayerClassEx(teamid, modelid, spawn_x, spawn_y, spawn_z, z_angle, weapon1, weapon1_ammo, weapon2, weapon2_ammo, weapon3, weapon3_ammo);
}
static inline int AddStaticVehicle(int modelid, float spawn_x, float spawn_y, float spawn_z, float z_angle, int color1, int color2) {
  return ::sampgdk_AddStaticVehicle(modelid, spawn_x, spawn_y, spawn_z, z_angle, color1, color2);
}
static inline int AddStaticVehicleEx(int modelid, float spawn_x, float spawn_y, float spawn_z, float z_angle, int color1, int color2, int respawn_delay) {
  return ::sampgdk_AddStaticVehicleEx(modelid, spawn_x, spawn_y, spawn_z, z_angle, color1, color2, respawn_delay);
}
static inline int AddStaticPickup(int model, int type, float x, float y, float z, int virtualworld) {
  return ::sampgdk_AddStaticPickup(model, type, x, y, z, virtualworld);
}
static inline int CreatePickup(int model, int type, float x, float y, float z, int virtualworld) {
  return ::sampgdk_CreatePickup(model, type, x, y, z, virtualworld);
}
static inline bool DestroyPickup(int pickup) {
  return ::sampgdk_DestroyPickup(pickup);
}
static inline bool ShowNameTags(bool show) {
  return ::sampgdk_ShowNameTags(show);
}
static inline bool ShowPlayerMarkers(int mode) {
  return ::sampgdk_ShowPlayerMarkers(mode);
}
static inline bool GameModeExit() {
  return ::sampgdk_GameModeExit();
}
static inline bool SetWorldTime(int hour) {
  return ::sampgdk_SetWorldTime(hour);
}
static inline bool GetWeaponName(int weaponid, char * name, int size) {
  return ::sampgdk_GetWeaponName(weaponid, name, size);
}
static inline bool EnableTirePopping(bool enable) {
  return ::sampgdk_EnableTirePopping(enable);
}
static inline bool EnableVehicleFriendlyFire() {
  return ::sampgdk_EnableVehicleFriendlyFire();
}
static inline bool AllowInteriorWeapons(bool allow) {
  return ::sampgdk_AllowInteriorWeapons(allow);
}
static inline bool SetWeather(int weatherid) {
  return ::sampgdk_SetWeather(weatherid);
}
static inline bool SetGravity(float gravity) {
  return ::sampgdk_SetGravity(gravity);
}
static inline bool AllowAdminTeleport(bool allow) {
  return ::sampgdk_AllowAdminTeleport(allow);
}
static inline bool SetDeathDropAmount(int amount) {
  return ::sampgdk_SetDeathDropAmount(amount);
}
static inline bool CreateExplosion(float x, float y, float z, int type, float radius) {
  return ::sampgdk_CreateExplosion(x, y, z, type, radius);
}
static inline bool EnableZoneNames(bool enable) {
  return ::sampgdk_EnableZoneNames(enable);
}
static inline bool UsePlayerPedAnims() {
  return ::sampgdk_UsePlayerPedAnims();
}
static inline bool DisableInteriorEnterExits() {
  return ::sampgdk_DisableInteriorEnterExits();
}
static inline bool SetNameTagDrawDistance(float distance) {
  return ::sampgdk_SetNameTagDrawDistance(distance);
}
static inline bool DisableNameTagLOS() {
  return ::sampgdk_DisableNameTagLOS();
}
static inline bool LimitGlobalChatRadius(float chat_radius) {
  return ::sampgdk_LimitGlobalChatRadius(chat_radius);
}
static inline bool LimitPlayerMarkerRadius(float marker_radius) {
  return ::sampgdk_LimitPlayerMarkerRadius(marker_radius);
}
static inline bool ConnectNPC(const char * name, const char * script) {
  return ::sampgdk_ConnectNPC(name, script);
}
static inline bool IsPlayerNPC(int playerid) {
  return ::sampgdk_IsPlayerNPC(playerid);
}
static inline bool IsPlayerAdmin(int playerid) {
  return ::sampgdk_IsPlayerAdmin(playerid);
}
static inline bool Kick(int playerid) {
  return ::sampgdk_Kick(playerid);
}
static inline bool Ban(int playerid) {
  return ::sampgdk_Ban(playerid);
}
static inline bool BanEx(int playerid, const char * reason) {
  return ::sampgdk_BanEx(playerid, reason);
}
static inline bool SendRconCommand(const char * command) {
  return ::sampgdk_SendRconCommand(command);
}
static inline bool GetServerVarAsString(const char * varname, char * value, int size) {
  return ::sampgdk_GetServerVarAsString(varname, value, size);
}
static inline int GetServerVarAsInt(const char * varname) {
  return ::sampgdk_GetServerVarAsInt(varname);
}
static inline bool GetServerVarAsBool(const char * varname) {
  return ::sampgdk_GetServerVarAsBool(varname);
}
static inline bool GetPlayerNetworkStats(int playerid, char * retstr, int size) {
  return ::sampgdk_GetPlayerNetworkStats(playerid, retstr, size);
}
static inline bool GetNetworkStats(char * retstr, int size) {
  return ::sampgdk_GetNetworkStats(retstr, size);
}
static inline bool GetPlayerVersion(int playerid, char * version, int len) {
  return ::sampgdk_GetPlayerVersion(playerid, version, len);
}
static inline int CreateMenu(const char * title, int columns, float x, float y, float col1width, float col2width) {
  return ::sampgdk_CreateMenu(title, columns, x, y, col1width, col2width);
}
static inline bool DestroyMenu(int menuid) {
  return ::sampgdk_DestroyMenu(menuid);
}
static inline int AddMenuItem(int menuid, int column, const char * menutext) {
  return ::sampgdk_AddMenuItem(menuid, column, menutext);
}
static inline bool SetMenuColumnHeader(int menuid, int column, const char * columnheader) {
  return ::sampgdk_SetMenuColumnHeader(menuid, column, columnheader);
}
static inline bool ShowMenuForPlayer(int menuid, int playerid) {
  return ::sampgdk_ShowMenuForPlayer(menuid, playerid);
}
static inline bool HideMenuForPlayer(int menuid, int playerid) {
  return ::sampgdk_HideMenuForPlayer(menuid, playerid);
}
static inline bool IsValidMenu(int menuid) {
  return ::sampgdk_IsValidMenu(menuid);
}
static inline bool DisableMenu(int menuid) {
  return ::sampgdk_DisableMenu(menuid);
}
static inline bool DisableMenuRow(int menuid, int row) {
  return ::sampgdk_DisableMenuRow(menuid, row);
}
static inline int GetPlayerMenu(int playerid) {
  return ::sampgdk_GetPlayerMenu(playerid);
}
static inline int TextDrawCreate(float x, float y, const char * text) {
  return ::sampgdk_TextDrawCreate(x, y, text);
}
static inline bool TextDrawDestroy(int text) {
  return ::sampgdk_TextDrawDestroy(text);
}
static inline bool TextDrawLetterSize(int text, float x, float y) {
  return ::sampgdk_TextDrawLetterSize(text, x, y);
}
static inline bool TextDrawTextSize(int text, float x, float y) {
  return ::sampgdk_TextDrawTextSize(text, x, y);
}
static inline bool TextDrawAlignment(int text, int alignment) {
  return ::sampgdk_TextDrawAlignment(text, alignment);
}
static inline bool TextDrawColor(int text, int color) {
  return ::sampgdk_TextDrawColor(text, color);
}
static inline bool TextDrawUseBox(int text, bool use) {
  return ::sampgdk_TextDrawUseBox(text, use);
}
static inline bool TextDrawBoxColor(int text, int color) {
  return ::sampgdk_TextDrawBoxColor(text, color);
}
static inline bool TextDrawSetShadow(int text, int size) {
  return ::sampgdk_TextDrawSetShadow(text, size);
}
static inline bool TextDrawSetOutline(int text, int size) {
  return ::sampgdk_TextDrawSetOutline(text, size);
}
static inline bool TextDrawBackgroundColor(int text, int color) {
  return ::sampgdk_TextDrawBackgroundColor(text, color);
}
static inline bool TextDrawFont(int text, int font) {
  return ::sampgdk_TextDrawFont(text, font);
}
static inline bool TextDrawSetProportional(int text, bool set) {
  return ::sampgdk_TextDrawSetProportional(text, set);
}
static inline bool TextDrawSetSelectable(int text, bool set) {
  return ::sampgdk_TextDrawSetSelectable(text, set);
}
static inline bool TextDrawShowForPlayer(int playerid, int text) {
  return ::sampgdk_TextDrawShowForPlayer(playerid, text);
}
static inline bool TextDrawHideForPlayer(int playerid, int text) {
  return ::sampgdk_TextDrawHideForPlayer(playerid, text);
}
static inline bool TextDrawShowForAll(int text) {
  return ::sampgdk_TextDrawShowForAll(text);
}
static inline bool TextDrawHideForAll(int text) {
  return ::sampgdk_TextDrawHideForAll(text);
}
static inline bool TextDrawSetString(int text, const char * string) {
  return ::sampgdk_TextDrawSetString(text, string);
}
static inline bool TextDrawSetPreviewModel(int text, int modelindex) {
  return ::sampgdk_TextDrawSetPreviewModel(text, modelindex);
}
static inline bool TextDrawSetPreviewRot(int text, float fRotX, float fRotY, float fRotZ, float fZoom) {
  return ::sampgdk_TextDrawSetPreviewRot(text, fRotX, fRotY, fRotZ, fZoom);
}
static inline bool TextDrawSetPreviewVehCol(int text, int color1, int color2) {
  return ::sampgdk_TextDrawSetPreviewVehCol(text, color1, color2);
}
static inline bool SelectTextDraw(int playerid, int hovercolor) {
  return ::sampgdk_SelectTextDraw(playerid, hovercolor);
}
static inline bool CancelSelectTextDraw(int playerid) {
  return ::sampgdk_CancelSelectTextDraw(playerid);
}
static inline int GangZoneCreate(float minx, float miny, float maxx, float maxy) {
  return ::sampgdk_GangZoneCreate(minx, miny, maxx, maxy);
}
static inline bool GangZoneDestroy(int zone) {
  return ::sampgdk_GangZoneDestroy(zone);
}
static inline bool GangZoneShowForPlayer(int playerid, int zone, int color) {
  return ::sampgdk_GangZoneShowForPlayer(playerid, zone, color);
}
static inline bool GangZoneShowForAll(int zone, int color) {
  return ::sampgdk_GangZoneShowForAll(zone, color);
}
static inline bool GangZoneHideForPlayer(int playerid, int zone) {
  return ::sampgdk_GangZoneHideForPlayer(playerid, zone);
}
static inline bool GangZoneHideForAll(int zone) {
  return ::sampgdk_GangZoneHideForAll(zone);
}
static inline bool GangZoneFlashForPlayer(int playerid, int zone, int flashcolor) {
  return ::sampgdk_GangZoneFlashForPlayer(playerid, zone, flashcolor);
}
static inline bool GangZoneFlashForAll(int zone, int flashcolor) {
  return ::sampgdk_GangZoneFlashForAll(zone, flashcolor);
}
static inline bool GangZoneStopFlashForPlayer(int playerid, int zone) {
  return ::sampgdk_GangZoneStopFlashForPlayer(playerid, zone);
}
static inline bool GangZoneStopFlashForAll(int zone) {
  return ::sampgdk_GangZoneStopFlashForAll(zone);
}
static inline int Create3DTextLabel(const char * text, int color, float x, float y, float z, float DrawDistance, int virtualworld, bool testLOS) {
  return ::sampgdk_Create3DTextLabel(text, color, x, y, z, DrawDistance, virtualworld, testLOS);
}
static inline bool Delete3DTextLabel(int id) {
  return ::sampgdk_Delete3DTextLabel(id);
}
static inline bool Attach3DTextLabelToPlayer(int id, int playerid, float OffsetX, float OffsetY, float OffsetZ) {
  return ::sampgdk_Attach3DTextLabelToPlayer(id, playerid, OffsetX, OffsetY, OffsetZ);
}
static inline bool Attach3DTextLabelToVehicle(int id, int vehicleid, float OffsetX, float OffsetY, float OffsetZ) {
  return ::sampgdk_Attach3DTextLabelToVehicle(id, vehicleid, OffsetX, OffsetY, OffsetZ);
}
static inline bool Update3DTextLabelText(int id, int color, const char * text) {
  return ::sampgdk_Update3DTextLabelText(id, color, text);
}
static inline int CreatePlayer3DTextLabel(int playerid, const char * text, int color, float x, float y, float z, float DrawDistance, int attachedplayer, int attachedvehicle, bool testLOS) {
  return ::sampgdk_CreatePlayer3DTextLabel(playerid, text, color, x, y, z, DrawDistance, attachedplayer, attachedvehicle, testLOS);
}
static inline bool DeletePlayer3DTextLabel(int playerid, int id) {
  return ::sampgdk_DeletePlayer3DTextLabel(playerid, id);
}
static inline bool UpdatePlayer3DTextLabelText(int playerid, int id, int color, const char * text) {
  return ::sampgdk_UpdatePlayer3DTextLabelText(playerid, id, color, text);
}
static inline bool ShowPlayerDialog(int playerid, int dialogid, int style, const char * caption, const char * info, const char * button1, const char * button2) {
  return ::sampgdk_ShowPlayerDialog(playerid, dialogid, style, caption, info, button1, button2);
}
static inline int SetTimer(int interval, bool repeat, TimerCallback callback, void * param) {
  return ::sampgdk_SetTimer(interval, repeat, callback, param);
}
static inline bool KillTimer(int timerid) {
  return ::sampgdk_KillTimer(timerid);
}
static inline bool gpci(int playerid, char * buffer, int size) {
  return ::sampgdk_gpci(playerid, buffer, size);
}

SAMPGDK_END_NAMESPACE

#endif /* __cplusplus */

SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnGameModeInit();
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnGameModeExit();
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerConnect(int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerDisconnect(int playerid, int reason);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerSpawn(int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerDeath(int playerid, int killerid, int reason);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnVehicleSpawn(int vehicleid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnVehicleDeath(int vehicleid, int killerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerText(int playerid, const char * text);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerCommandText(int playerid, const char * cmdtext);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerRequestClass(int playerid, int classid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerExitVehicle(int playerid, int vehicleid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerStateChange(int playerid, int newstate, int oldstate);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerEnterCheckpoint(int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerLeaveCheckpoint(int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerEnterRaceCheckpoint(int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerLeaveRaceCheckpoint(int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnRconCommand(const char * cmd);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerRequestSpawn(int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnObjectMoved(int objectid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerObjectMoved(int playerid, int objectid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerPickUpPickup(int playerid, int pickupid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnVehicleMod(int playerid, int vehicleid, int componentid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnEnterExitModShop(int playerid, int enterexit, int interiorid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnVehicleRespray(int playerid, int vehicleid, int color1, int color2);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnVehicleDamageStatusUpdate(int vehicleid, int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passenger_seat);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerSelectedMenuRow(int playerid, int row);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerExitedMenu(int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnRconLoginAttempt(const char * ip, const char * password, bool success);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerUpdate(int playerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerStreamIn(int playerid, int forplayerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerStreamOut(int playerid, int forplayerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnVehicleStreamIn(int vehicleid, int forplayerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnVehicleStreamOut(int vehicleid, int forplayerid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnDialogResponse(int playerid, int dialogid, int response, int listitem, const char * inputtext);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerClickMap(int playerid, float fX, float fY, float fZ);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerClickTextDraw(int playerid, int clickedid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerClickPlayerTextDraw(int playerid, int playertextid);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerClickPlayer(int playerid, int clickedplayerid, int source);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX, float fY, float fZ, float fRotX, float fRotY, float fRotZ);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX, float fScaleY, float fScaleZ);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY, float fZ);
SAMPGDK_CALLBACK_EXPORT bool SAMPGDK_CALLBACK_CALL OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY, float fZ);
