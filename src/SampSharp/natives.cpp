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

#include "natives.h"
#include <sampgdk/sampgdk.h>
#include "monohelper.h"
#include "unicode.h"
#include "customnatives.h"

//
//a_players string converters
inline int p_SetPlayerName(int playerid, MonoString *name) {
    char *buffer = monostring_to_string(name);

    int retval = sampgdk_SetPlayerName(playerid, buffer);

    delete[] buffer;
    return retval;
}

inline bool p_PlayAudioStreamForPlayer(int playerid, MonoString *url,
    float posX, float posY, float posZ, float distance, bool usepos) {
    char *buffer = monostring_to_string(url);

    bool retval = sampgdk_PlayAudioStreamForPlayer(playerid, buffer, posX, posY,
        posZ, distance, usepos);

    delete[] buffer;
    return retval;
}

inline bool p_SetPlayerShopName(int playerid, MonoString *shopname) {
    char *buffer = monostring_to_string(shopname);

    bool retval = sampgdk_SetPlayerShopName(playerid, buffer);

    delete[] buffer;
    return retval;
}

inline int p_CreatePlayerTextDraw(int playerid, float x, float y,
    MonoString *text) {
    char *buffer = monostring_to_string(text);

    int retval = sampgdk_CreatePlayerTextDraw(playerid, x, y, buffer);

    delete[] buffer;
    return retval;
}

inline bool p_PlayerTextDrawSetString(int playerid, int text,
    MonoString *string) {
    char *buffer = monostring_to_string(string);

    bool retval = sampgdk_PlayerTextDrawSetString(playerid, text, buffer);

    delete[] buffer;
    return retval;
}

inline bool p_SetPVarInt(int playerid, MonoString *varname, int value) {
    char *buffer = monostring_to_string(varname);

    bool retval = sampgdk_SetPVarInt(playerid, buffer, value);

    delete[] buffer;
    return retval;
}

inline int p_GetPVarInt(int playerid, MonoString *varname) {
    char *buffer = monostring_to_string(varname);

    int retval = sampgdk_GetPVarInt(playerid, buffer);

    delete[] buffer;
    return retval;
}

inline bool p_SetPVarString(int playerid, MonoString *varname,
    MonoString *value) {
    char *varname_buffer = monostring_to_string(varname);
    char *value_buffer = monostring_to_string(value);

    bool retval = sampgdk_SetPVarString(playerid, varname_buffer, value_buffer);

    delete[] varname_buffer;
    delete[] value_buffer;
    return retval;
}

inline bool p_GetPVarString(int playerid, MonoString *varname,
    MonoString ** value, int size) {
    char *varname_buffer = monostring_to_string(varname);
    char *value_buffer = new char[size];

    bool retval = sampgdk_GetPVarString(playerid, varname_buffer, value_buffer,
        size);

    *value = string_to_monostring(value_buffer, size);

    delete[] varname_buffer;
    delete[] value_buffer;
    return retval;
}

inline bool p_SetPVarFloat(int playerid, MonoString *varname, float value) {
    char *buffer = monostring_to_string(varname);

    bool retval = sampgdk_SetPVarFloat(playerid, buffer, value);

    delete[] buffer;
    return retval;
}

inline float p_GetPVarFloat(int playerid, MonoString *varname) {
    char *buffer = monostring_to_string(varname);

    float retval = sampgdk_GetPVarFloat(playerid, buffer);

    delete[] buffer;
    return retval;
}

inline bool p_DeletePVar(int playerid, MonoString *varname) {
    char *buffer = monostring_to_string(varname);

    bool retval = sampgdk_DeletePVar(playerid, buffer);

    delete[] buffer;
    return retval;
}

inline int p_GetPVarType(int playerid, MonoString *varname) {
    char *buffer = monostring_to_string(varname);

    int retval = sampgdk_GetPVarType(playerid, buffer);

    delete[] buffer;
    return retval;
}

inline bool p_SetPlayerChatBubble(int playerid, MonoString *text, int color,
    float drawdistance, int expiretime) {
    char *buffer = monostring_to_string(text);

    bool retval = sampgdk_SetPlayerChatBubble(playerid, buffer, color,
        drawdistance, expiretime);

    delete[] buffer;
    return retval;
}

inline bool p_ApplyAnimation(int playerid, MonoString *animlib,
    MonoString *animname, float fDelta, bool loop, bool lockx, bool locky,
    bool freeze, int time, bool forcesync) {
    char *animlib_buffer = monostring_to_string(animlib);
    char *animname_buffer = monostring_to_string(animname);

    bool retval = sampgdk_ApplyAnimation(playerid, animlib_buffer,
        animname_buffer, fDelta, loop, lockx, locky, freeze, time, forcesync);

    delete[] animlib_buffer;
    delete[] animname_buffer;
    return retval;
}

inline bool p_StartRecordingPlayerData(int playerid, int recordtype,
    MonoString *recordname) {
    char *buffer = monostring_to_string(recordname);

    bool retval = sampgdk_StartRecordingPlayerData(playerid, recordtype,
        buffer);

    delete[] buffer;
    return retval;
}

inline bool p_GetPlayerIp(int playerid, MonoString ** ip, int size) {
    char *buffer = new char[size];

    bool retval = sampgdk_GetPlayerIp(playerid, buffer, size);

    *ip = string_to_monostring(buffer, size);

    delete[] buffer;
    return retval;
}

inline int p_GetPlayerName(int playerid, MonoString ** name, int size) {
    char *buffer = new char[size];

    int retval = sampgdk_GetPlayerName(playerid, buffer, size);

    *name = string_to_monostring(buffer, size);

    delete[] buffer;
    return retval;
}

inline bool p_GetPVarNameAtIndex(int playerid, int index, MonoString ** varname,
    int size) {
    char *buffer = new char[size];

    bool retval = sampgdk_GetPVarNameAtIndex(playerid, index, buffer, size);
    *varname = string_to_monostring(buffer, size);

    delete[] buffer;
    return retval;
}

inline bool p_GetAnimationName(int index, MonoString ** animlib,
    int animlib_size, MonoString ** animname, int animname_size) {
    char *libbuffer = new char[animlib_size];
    char *namebuffer = new char[animname_size];

    bool retval = sampgdk_GetAnimationName(index, libbuffer, animlib_size,
        namebuffer, animname_size);

    *animlib = string_to_monostring(libbuffer, animlib_size);
    *animname = string_to_monostring(namebuffer, animname_size);

    delete[] libbuffer;
    delete[] namebuffer;
    return retval;
}

inline bool p_ApplyActorAnimation(int actorid, MonoString *animlib,
    MonoString *animname, float fDelta, bool loop, bool lockx, bool locky,
    bool freeze, int time) {
    char *animlib_buffer = monostring_to_string(animlib);
    char *animname_buffer = monostring_to_string(animname);

    bool retval = sampgdk_ApplyActorAnimation(actorid, animlib_buffer,
        animname_buffer, fDelta, loop, lockx, locky, freeze, time);

    delete[] animlib_buffer;
    delete[] animname_buffer;
    return retval;
}

//
//a_samp string converters
inline bool p_SendClientMessage(int playerid, int color, MonoString *message) {
    char *buffer = monostring_to_string(message);

    bool retval = sampgdk_SendClientMessage(playerid, color, buffer);

    delete[] buffer;
    return retval;
}
inline bool p_SendClientMessageToAll(int color, MonoString *message) {
    char *buffer = monostring_to_string(message);

    bool retval = sampgdk_SendClientMessageToAll(color, buffer);

    delete[] buffer;
    return retval;
}
inline bool p_SendPlayerMessageToPlayer(int playerid, int senderid,
    MonoString *message) {
    char *buffer = monostring_to_string(message);

    bool retval = sampgdk_SendPlayerMessageToPlayer(playerid, senderid, buffer);

    delete[] buffer;
    return retval;
}
inline bool p_SendPlayerMessageToAll(int senderid, MonoString *message) {
    char *buffer = monostring_to_string(message);

    bool retval = sampgdk_SendPlayerMessageToAll(senderid, buffer);

    delete[] buffer;
    return retval;
}
inline bool p_GameTextForAll(MonoString *text, int time, int style) {
    char *buffer = monostring_to_string(text);

    bool retval = sampgdk_GameTextForAll(buffer, time, style);

    delete[] buffer;
    return retval;
}
inline bool p_GameTextForPlayer(int playerid, MonoString *text, int time,
    int style) {
    char *buffer = monostring_to_string(text);

    bool retval = sampgdk_GameTextForPlayer(playerid, buffer, time, style);

    delete[] buffer;
    return retval;
}
inline bool p_SetGameModeText(MonoString *text) {
    char *buffer = monostring_to_string(text);

    bool retval = sampgdk_SetGameModeText(buffer);

    delete[] buffer;
    return retval;
}
inline bool p_ConnectNPC(MonoString *name, MonoString *script) {
    char *name_buffer = monostring_to_string(name);
    char *script_buffer = monostring_to_string(script);

    bool retval = sampgdk_ConnectNPC(name_buffer, script_buffer);

    delete[] name_buffer;
    delete[] script_buffer;
    return retval;
}
inline bool p_BanEx(int playerid, MonoString *reason) {
    char *buffer = monostring_to_string(reason);

    bool retval = sampgdk_BanEx(playerid, buffer);

    delete[] buffer;
    return retval;
}
inline bool p_SendRconCommand(MonoString *command) {
    char *buffer = monostring_to_string(command);

    bool retval = sampgdk_SendRconCommand(buffer);

    delete[] buffer;
    return retval;
}
inline bool p_GetServerVarAsString(MonoString *varname, MonoString ** value,
    int size) {
    char *varname_buffer = monostring_to_string(varname);
    char *value_buffer = new char[size];

    bool retval = sampgdk_GetServerVarAsString(varname_buffer, value_buffer,
        size);

    *value = string_to_monostring(value_buffer, size);

    delete[] varname_buffer;
    delete[] value_buffer;
    return retval;
}
inline int p_GetServerVarAsInt(MonoString *varname) {
    char *buffer = monostring_to_string(varname);

    int retval = sampgdk_GetServerVarAsInt(buffer);

    delete[] buffer;
    return retval;
}
inline bool p_GetServerVarAsBool(MonoString *varname) {
    char *buffer = monostring_to_string(varname);

    bool retval = sampgdk_GetServerVarAsBool(buffer);

    delete[] buffer;
    return retval;
}
inline int p_CreateMenu(MonoString *title, int columns, float x, float y,
    float col1width, float col2width) {
    char *buffer = monostring_to_string(title);

    int retval = sampgdk_CreateMenu(buffer, columns, x, y, col1width,
        col2width);

    delete[] buffer;
    return retval;
}
inline int p_AddMenuItem(int menuid, int column, MonoString *menutext) {
    char *buffer = monostring_to_string(menutext);

    int retval = sampgdk_AddMenuItem(menuid, column, buffer);

    delete[] buffer;
    return retval;
}
inline bool p_SetMenuColumnHeader(int menuid, int column,
    MonoString *columnheader) {
    char *buffer = monostring_to_string(columnheader);

    bool retval = sampgdk_SetMenuColumnHeader(menuid, column, buffer);

    delete[] buffer;
    return retval;
}
inline int p_TextDrawCreate(float x, float y, MonoString *text) {
    char *buffer = monostring_to_string(text);

    int retval = sampgdk_TextDrawCreate(x, y, buffer);

    delete[] buffer;
    return retval;
}
inline bool p_TextDrawSetString(int text, MonoString *string) {
    char *buffer = monostring_to_string(string);

    bool retval = sampgdk_TextDrawSetString(text, buffer);

    delete[] buffer;
    return retval;
}
inline int p_Create3DTextLabel(MonoString *text, int color, float x, float y,
    float z, float drawDistance, int virtualworld, bool testLOS) {
    char *buffer = monostring_to_string(text);

    int retval = sampgdk_Create3DTextLabel(buffer, color, x, y, z,
        drawDistance, virtualworld, testLOS);

    delete[] buffer;
    return retval;
}
inline bool p_Update3DTextLabelText(int id, int color, MonoString *text) {
    char *buffer = monostring_to_string(text);

    bool retval = sampgdk_Update3DTextLabelText(id, color, buffer);

    delete[] buffer;
    return retval;
}
inline int p_CreatePlayer3DTextLabel(int playerid, MonoString *text, int color,
    float x, float y, float z, float drawDistance, int attachedplayer,
    int attachedvehicle, bool testLOS) {
    char *buffer = monostring_to_string(text);

    int retval = sampgdk_CreatePlayer3DTextLabel(playerid, buffer, color, x, y,
        z, drawDistance, attachedplayer, attachedvehicle, testLOS);

    delete[] buffer;
    return retval;
}
inline bool p_UpdatePlayer3DTextLabelText(int playerid, int id, int color,
    MonoString *text) {
    char *buffer = monostring_to_string(text);

    bool retval = sampgdk_UpdatePlayer3DTextLabelText(playerid, id, color,
        buffer);

    delete[] buffer;
    return retval;
}
inline bool p_ShowPlayerDialog(int playerid, int dialogid, int style,
    MonoString *caption, MonoString *info, MonoString *button1,
    MonoString *button2) {
    char *caption_buffer = monostring_to_string(caption);
    char *info_buffer = monostring_to_string(info);
    char *button1_buffer = monostring_to_string(button1);
    char *button2_buffer = monostring_to_string(button2);

    bool retval = sampgdk_ShowPlayerDialog(playerid, dialogid, style,
        caption_buffer, info_buffer, button1_buffer, button2_buffer);

    delete[] caption_buffer;
    delete[] info_buffer;
    delete[] button1_buffer;
    delete[] button2_buffer;
    return retval;
}
inline bool p_GetWeaponName(int weaponid, MonoString ** name, int size) {
    char *buffer = new char[size];

    bool retval = sampgdk_GetWeaponName(weaponid, buffer, size);

    *name = string_to_monostring(buffer, size);

    delete[] buffer;
    return retval;
}
inline bool p_GetPlayerNetworkStats(int playerid, MonoString ** retstr,
    int size) {
    char *buffer = new char[size];

    bool retval = sampgdk_GetPlayerNetworkStats(playerid, buffer, size);

    *retstr = string_to_monostring(buffer, size);

    delete[] buffer;
    return retval;
}
inline bool p_GetPlayerVersion(int playerid, MonoString ** version, int len) {
    char *buffer = new char[len];

    bool retval = sampgdk_GetPlayerVersion(playerid, buffer, len);

    *version = string_to_monostring(buffer, len);

    delete[] buffer;
    return retval;
}
inline bool p_gpci(int playerid, MonoString ** buffer, int size) {
    char *buffer_buffer = new char[size];

    bool retval = sampgdk_gpci(playerid, buffer_buffer, size);

    *buffer = string_to_monostring(buffer_buffer, size);

    delete[] buffer_buffer;
    return retval;
}
inline bool p_GetNetworkStats(MonoString ** retstr, int size) {
    char *buffer = new char[size];

    bool retval = sampgdk_GetNetworkStats(buffer, size);

    *retstr = string_to_monostring(buffer, size);

    delete[] buffer;
    return retval;
}

inline bool p_BlockIpAddress(MonoString *ip_address, int timems) {
    char *buffer = monostring_to_string(ip_address);

    bool retval = sampgdk_BlockIpAddress(buffer, timems);

    delete[] buffer;
    return retval;
}
inline bool p_UnBlockIpAddress(MonoString *ip_address) {
    char *buffer = monostring_to_string(ip_address);

    bool retval = sampgdk_UnBlockIpAddress(buffer);

    delete[] buffer;
    return retval;
}

//
// a_objects string converters
inline bool p_SetObjectMaterial(int objectid, int materialindex, int modelid,
    MonoString *txdname, MonoString *texturename, int materialcolor) {
    char *txdname_buffer = monostring_to_string(txdname);
    char *texturename_buffer = monostring_to_string(texturename);

    bool retval = sampgdk_SetObjectMaterial(objectid, materialindex, modelid,
        txdname_buffer, texturename_buffer, materialcolor);

    delete[] txdname_buffer;
    delete[] texturename_buffer;
    return retval;
}
inline bool p_SetPlayerObjectMaterial(int playerid, int objectid,
    int materialindex, int modelid, MonoString *txdname,
    MonoString *texturename, int materialcolor) {
    char *txdname_buffer = monostring_to_string(txdname);
    char *texturename_buffer = monostring_to_string(texturename);

    bool retval = sampgdk_SetPlayerObjectMaterial(playerid, objectid,
        materialindex, modelid, txdname_buffer, texturename_buffer,
        materialcolor);

    delete[] txdname_buffer;
    delete[] texturename_buffer;
    return retval;
}
inline bool p_SetObjectMaterialText(int objectid, MonoString *text,
    int materialindex, int materialsize, MonoString *fontface, int fontsize,
    bool bold, int fontcolor, int backcolor, int textalignment) {
    char *text_buffer = monostring_to_string(text);
    char *fontface_buffer = monostring_to_string(fontface);

    bool retval = sampgdk_SetObjectMaterialText(objectid, text_buffer,
        materialindex, materialsize, fontface_buffer, fontsize, bold, fontcolor,
        backcolor, textalignment);

    delete[] text_buffer;
    delete[] fontface_buffer;
    return retval;
}
inline bool p_SetPlayerObjectMaterialText(int playerid, int objectid,
    MonoString *text, int materialindex, int materialsize, MonoString *fontface,
    int fontsize, bool bold, int fontcolor, int backcolor, int textalignment) {
    char *text_buffer = monostring_to_string(text);
    char *fontface_buffer = monostring_to_string(fontface);

    bool retval = sampgdk_SetPlayerObjectMaterialText(playerid, objectid,
        text_buffer, materialindex, materialsize, fontface_buffer, fontsize,
        bold, fontcolor, backcolor, textalignment);

    delete[] text_buffer;
    delete[] fontface_buffer;
    return retval;
}

//
// a_vehicles string converters
inline bool p_SetVehicleNumberPlate(int vehicleid, MonoString *numberplate) {
    char *buffer = monostring_to_string(numberplate);

    bool retval = sampgdk_SetVehicleNumberPlate(vehicleid, buffer);

    delete[] buffer;
    return retval;
}

//
// serverlog string converters
inline void p_Print(MonoString *str) {
    char *buffer = monostring_to_string(str);

    sampgdk_logprintf("%s", buffer);

    delete[] buffer;
}


void LoadNatives(void (add_call(const char * name, const void * method)))
{
    //
    // a_players natives
    add_call("SetSpawnInfo", (void *)sampgdk_SetSpawnInfo);
    add_call("SpawnPlayer", (void *)sampgdk_SpawnPlayer);
    add_call("SetPlayerPos", (void *)sampgdk_SetPlayerPos);
    add_call("SetPlayerPosFindZ", (void *)sampgdk_SetPlayerPosFindZ);
    add_call("GetPlayerPos", (void *)sampgdk_GetPlayerPos);
    add_call("SetPlayerFacingAngle", (void *)sampgdk_SetPlayerFacingAngle);
    add_call("GetPlayerFacingAngle", (void *)sampgdk_GetPlayerFacingAngle);
    add_call("IsPlayerInRangeOfPoint", (void *)sampgdk_IsPlayerInRangeOfPoint);
    add_call("GetPlayerDistanceFromPoint", (void *)sampgdk_GetPlayerDistanceFromPoint);
    add_call("IsPlayerStreamedIn", (void *)sampgdk_IsPlayerStreamedIn);
    add_call("SetPlayerInterior", (void *)sampgdk_SetPlayerInterior);
    add_call("GetPlayerInterior", (void *)sampgdk_GetPlayerInterior);
    add_call("SetPlayerHealth", (void *)sampgdk_SetPlayerHealth);
    add_call("GetPlayerHealth", (void *)sampgdk_GetPlayerHealth);
    add_call("SetPlayerArmour", (void *)sampgdk_SetPlayerArmour);
    add_call("GetPlayerArmour", (void *)sampgdk_GetPlayerArmour);
    add_call("SetPlayerAmmo", (void *)sampgdk_SetPlayerAmmo);
    add_call("GetPlayerAmmo", (void *)sampgdk_GetPlayerAmmo);
    add_call("GetPlayerWeaponState", (void *)sampgdk_GetPlayerWeaponState);
    add_call("GetPlayerTargetPlayer", (void *)sampgdk_GetPlayerTargetPlayer);
    add_call("GetPlayerTargetActor", (void *)sampgdk_GetPlayerTargetActor);
    add_call("SetPlayerTeam", (void *)sampgdk_SetPlayerTeam);
    add_call("GetPlayerTeam", (void *)sampgdk_GetPlayerTeam);
    add_call("SetPlayerScore", (void *)sampgdk_SetPlayerScore);
    add_call("GetPlayerScore", (void *)sampgdk_GetPlayerScore);
    add_call("GetPlayerDrunkLevel", (void *)sampgdk_GetPlayerDrunkLevel);
    add_call("SetPlayerDrunkLevel", (void *)sampgdk_SetPlayerDrunkLevel);
    add_call("SetPlayerColor", (void *)sampgdk_SetPlayerColor);
    add_call("GetPlayerColor", (void *)sampgdk_GetPlayerColor);
    add_call("SetPlayerSkin", (void *)sampgdk_SetPlayerSkin);
    add_call("GetPlayerSkin", (void *)sampgdk_GetPlayerSkin);
    add_call("GivePlayerWeapon", (void *)sampgdk_GivePlayerWeapon);
    add_call("ResetPlayerWeapons", (void *)sampgdk_ResetPlayerWeapons);
    add_call("SetPlayerArmedWeapon", (void *)sampgdk_SetPlayerArmedWeapon);
    add_call("GetPlayerWeaponData", (void *)sampgdk_GetPlayerWeaponData);
    add_call("GivePlayerMoney", (void *)sampgdk_GivePlayerMoney);
    add_call("ResetPlayerMoney", (void *)sampgdk_ResetPlayerMoney);
    add_call("SetPlayerName", (void *)p_SetPlayerName);
    add_call("GetPlayerMoney", (void *)sampgdk_GetPlayerMoney);
    add_call("GetPlayerState", (void *)sampgdk_GetPlayerState);
    add_call("GetPlayerIp", (void *)p_GetPlayerIp);
    add_call("GetPlayerPing", (void *)sampgdk_GetPlayerPing);
    add_call("GetPlayerWeapon", (void *)sampgdk_GetPlayerWeapon);
    add_call("GetPlayerKeys", (void *)sampgdk_GetPlayerKeys);
    add_call("GetPlayerName", (void *)p_GetPlayerName);
    add_call("SetPlayerTime", (void *)sampgdk_SetPlayerTime);
    add_call("GetPlayerTime", (void *)sampgdk_GetPlayerTime);
    add_call("TogglePlayerClock", (void *)sampgdk_TogglePlayerClock);
    add_call("SetPlayerWeather", (void *)sampgdk_SetPlayerWeather);
    add_call("ForceClassSelection", (void *)sampgdk_ForceClassSelection);
    add_call("SetPlayerWantedLevel", (void *)sampgdk_SetPlayerWantedLevel);
    add_call("GetPlayerWantedLevel", (void *)sampgdk_GetPlayerWantedLevel);
    add_call("SetPlayerFightingStyle", (void *)sampgdk_SetPlayerFightingStyle);
    add_call("GetPlayerFightingStyle", (void *)sampgdk_GetPlayerFightingStyle);
    add_call("SetPlayerVelocity", (void *)sampgdk_SetPlayerVelocity);
    add_call("GetPlayerVelocity", (void *)sampgdk_GetPlayerVelocity);
    add_call("PlayCrimeReportForPlayer", (void *)sampgdk_PlayCrimeReportForPlayer);
    add_call("PlayAudioStreamForPlayer", (void *)p_PlayAudioStreamForPlayer);
    add_call("StopAudioStreamForPlayer", (void *)sampgdk_StopAudioStreamForPlayer);
    add_call("SetPlayerShopName", (void *)p_SetPlayerShopName);
    add_call("SetPlayerSkillLevel", (void *)sampgdk_SetPlayerSkillLevel);
    add_call("GetPlayerSurfingVehicleID", (void *)sampgdk_GetPlayerSurfingVehicleID);
    add_call("GetPlayerSurfingObjectID", (void *)sampgdk_GetPlayerSurfingObjectID);
    add_call("RemoveBuildingForPlayer", (void *)sampgdk_RemoveBuildingForPlayer);
    add_call("SetPlayerAttachedObject", (void *)sampgdk_SetPlayerAttachedObject);
    add_call("RemovePlayerAttachedObject", (void *)sampgdk_RemovePlayerAttachedObject);
    add_call("IsPlayerAttachedObjectSlotUsed", (void *)sampgdk_IsPlayerAttachedObjectSlotUsed);
    add_call("EditAttachedObject", (void *)sampgdk_EditAttachedObject);
    add_call("CreatePlayerTextDraw", (void *)p_CreatePlayerTextDraw);
    add_call("PlayerTextDrawDestroy", (void *)sampgdk_PlayerTextDrawDestroy);
    add_call("PlayerTextDrawLetterSize", (void *)sampgdk_PlayerTextDrawLetterSize);
    add_call("PlayerTextDrawTextSize", (void *)sampgdk_PlayerTextDrawTextSize);
    add_call("PlayerTextDrawAlignment", (void *)sampgdk_PlayerTextDrawAlignment);
    add_call("PlayerTextDrawColor", (void *)sampgdk_PlayerTextDrawColor);
    add_call("PlayerTextDrawUseBox", (void *)sampgdk_PlayerTextDrawUseBox);
    add_call("PlayerTextDrawBoxColor", (void *)sampgdk_PlayerTextDrawBoxColor);
    add_call("PlayerTextDrawSetShadow", (void *)sampgdk_PlayerTextDrawSetShadow);
    add_call("PlayerTextDrawSetOutline", (void *)sampgdk_PlayerTextDrawSetOutline);
    add_call("PlayerTextDrawBackgroundColor", (void *)sampgdk_PlayerTextDrawBackgroundColor);
    add_call("PlayerTextDrawFont", (void *)sampgdk_PlayerTextDrawFont);
    add_call("PlayerTextDrawSetProportional", (void *)sampgdk_PlayerTextDrawSetProportional);
    add_call("PlayerTextDrawSetSelectable", (void *)sampgdk_PlayerTextDrawSetSelectable);
    add_call("PlayerTextDrawShow", (void *)sampgdk_PlayerTextDrawShow);
    add_call("PlayerTextDrawHide", (void *)sampgdk_PlayerTextDrawHide);
    add_call("PlayerTextDrawSetString", (void *)p_PlayerTextDrawSetString);
    add_call("PlayerTextDrawSetPreviewModel", (void *)sampgdk_PlayerTextDrawSetPreviewModel);
    add_call("PlayerTextDrawSetPreviewRot", (void *)sampgdk_PlayerTextDrawSetPreviewRot);
    add_call("PlayerTextDrawSetPreviewVehCol", (void *)sampgdk_PlayerTextDrawSetPreviewVehCol);
    add_call("SetPVarInt", (void *)p_SetPVarInt);
    add_call("GetPVarInt", (void *)p_GetPVarInt);
    add_call("SetPVarString", (void *)p_SetPVarString);
    add_call("GetPVarString", (void *)p_GetPVarString);
    add_call("SetPVarFloat", (void *)p_SetPVarFloat);
    add_call("GetPVarFloat", (void *)p_GetPVarFloat);
    add_call("DeletePVar", (void *)p_DeletePVar);
    add_call("GetPVarsUpperIndex", (void *)sampgdk_GetPVarsUpperIndex);
    add_call("GetPVarNameAtIndex", (void *)p_GetPVarNameAtIndex);
    add_call("GetPVarType", (void *)p_GetPVarType);
    add_call("SetPlayerChatBubble", (void *)p_SetPlayerChatBubble);
    add_call("PutPlayerInVehicle", (void *)sampgdk_PutPlayerInVehicle);
    add_call("GetPlayerVehicleID", (void *)sampgdk_GetPlayerVehicleID);
    add_call("GetPlayerVehicleSeat", (void *)sampgdk_GetPlayerVehicleSeat);
    add_call("RemovePlayerFromVehicle", (void *)sampgdk_RemovePlayerFromVehicle);
    add_call("TogglePlayerControllable", (void *)sampgdk_TogglePlayerControllable);
    add_call("PlayerPlaySound", (void *)sampgdk_PlayerPlaySound);
    add_call("ApplyAnimation", (void *)p_ApplyAnimation);
    add_call("ClearAnimations", (void *)sampgdk_ClearAnimations);
    add_call("GetPlayerAnimationIndex", (void *)sampgdk_GetPlayerAnimationIndex);
    add_call("GetAnimationName", (void *)p_GetAnimationName);
    add_call("GetPlayerSpecialAction", (void *)sampgdk_GetPlayerSpecialAction);
    add_call("SetPlayerSpecialAction", (void *)sampgdk_SetPlayerSpecialAction);
    add_call("DisableRemoteVehicleCollisions", (void *)sampgdk_DisableRemoteVehicleCollisions);
    add_call("SetPlayerCheckpoint", (void *)sampgdk_SetPlayerCheckpoint);
    add_call("DisablePlayerCheckpoint", (void *)sampgdk_DisablePlayerCheckpoint);
    add_call("SetPlayerRaceCheckpoint", (void *)sampgdk_SetPlayerRaceCheckpoint);
    add_call("DisablePlayerRaceCheckpoint", (void *)sampgdk_DisablePlayerRaceCheckpoint);
    add_call("SetPlayerWorldBounds", (void *)sampgdk_SetPlayerWorldBounds);
    add_call("SetPlayerMarkerForPlayer", (void *)sampgdk_SetPlayerMarkerForPlayer);
    add_call("ShowPlayerNameTagForPlayer", (void *)sampgdk_ShowPlayerNameTagForPlayer);
    add_call("SetPlayerMapIcon", (void *)sampgdk_SetPlayerMapIcon);
    add_call("RemovePlayerMapIcon", (void *)sampgdk_RemovePlayerMapIcon);
    add_call("SetPlayerCameraPos", (void *)sampgdk_SetPlayerCameraPos);
    add_call("SetPlayerCameraLookAt", (void *)sampgdk_SetPlayerCameraLookAt);
    add_call("SetCameraBehindPlayer", (void *)sampgdk_SetCameraBehindPlayer);
    add_call("GetPlayerCameraPos", (void *)sampgdk_GetPlayerCameraPos);
    add_call("GetPlayerCameraFrontVector", (void *)sampgdk_GetPlayerCameraFrontVector);
    add_call("EnablePlayerCameraTarget", (void *)sampgdk_EnablePlayerCameraTarget);
    add_call("GetPlayerCameraMode", (void *)sampgdk_GetPlayerCameraMode);
    add_call("GetPlayerCameraTargetObject", (void *)sampgdk_GetPlayerCameraTargetObject);
    add_call("GetPlayerCameraTargetVehicle", (void *)sampgdk_GetPlayerCameraTargetVehicle);
    add_call("GetPlayerCameraTargetPlayer", (void *)sampgdk_GetPlayerCameraTargetPlayer);
    add_call("GetPlayerCameraTargetActor", (void *)sampgdk_GetPlayerCameraTargetActor);
    add_call("AttachCameraToObject", (void *)sampgdk_AttachCameraToObject);
    add_call("AttachCameraToPlayerObject", (void *)sampgdk_AttachCameraToPlayerObject);
    add_call("InterpolateCameraPos", (void *)sampgdk_InterpolateCameraPos);
    add_call("InterpolateCameraLookAt", (void *)sampgdk_InterpolateCameraLookAt);
    add_call("IsPlayerConnected", (void *)sampgdk_IsPlayerConnected);
    add_call("IsPlayerInVehicle", (void *)sampgdk_IsPlayerInVehicle);
    add_call("IsPlayerInAnyVehicle", (void *)sampgdk_IsPlayerInAnyVehicle);
    add_call("IsPlayerInCheckpoint", (void *)sampgdk_IsPlayerInCheckpoint);
    add_call("IsPlayerInRaceCheckpoint", (void *)sampgdk_IsPlayerInRaceCheckpoint);
    add_call("SetPlayerVirtualWorld", (void *)sampgdk_SetPlayerVirtualWorld);
    add_call("GetPlayerVirtualWorld", (void *)sampgdk_GetPlayerVirtualWorld);
    add_call("EnableStuntBonusForPlayer", (void *)sampgdk_EnableStuntBonusForPlayer);
    add_call("EnableStuntBonusForAll", (void *)sampgdk_EnableStuntBonusForAll);
    add_call("TogglePlayerSpectating", (void *)sampgdk_TogglePlayerSpectating);
    add_call("PlayerSpectatePlayer", (void *)sampgdk_PlayerSpectatePlayer);
    add_call("PlayerSpectateVehicle", (void *)sampgdk_PlayerSpectateVehicle);
    add_call("StartRecordingPlayerData", (void *)p_StartRecordingPlayerData);
    add_call("StopRecordingPlayerData", (void *)sampgdk_StopRecordingPlayerData);
    add_call("CreateExplosionForPlayer", (void *)sampgdk_CreateExplosionForPlayer);

    //
    // a_samp natives
    add_call("SendClientMessage", (void *)p_SendClientMessage);
    add_call("SendClientMessageToAll", (void *)p_SendClientMessageToAll);
    add_call("SendPlayerMessageToPlayer", (void *)p_SendPlayerMessageToPlayer);
    add_call("SendPlayerMessageToAll", (void *)p_SendPlayerMessageToAll);
    add_call("SendDeathMessage", (void *)sampgdk_SendDeathMessage);
    add_call("GameTextForAll", (void *)p_GameTextForAll);
    add_call("GameTextForPlayer", (void *)p_GameTextForPlayer);
    add_call("GetTickCount", (void *)sampgdk_GetTickCount);
    add_call("GetMaxPlayers", (void *)sampgdk_GetMaxPlayers);
    add_call("GetPlayerPoolSize", (void *)sampgdk_GetPlayerPoolSize);
    add_call("GetVehiclePoolSize", (void *)sampgdk_GetVehiclePoolSize);
    add_call("GetActorPoolSize", (void *)sampgdk_GetActorPoolSize);
    add_call("VectorSize", (void *)sampgdk_VectorSize);
    add_call("SetGameModeText", (void *)p_SetGameModeText);
    add_call("SetTeamCount", (void *)sampgdk_SetTeamCount);
    add_call("AddPlayerClass", (void *)sampgdk_AddPlayerClass);
    add_call("AddPlayerClassEx", (void *)sampgdk_AddPlayerClassEx);
    add_call("AddStaticVehicle", (void *)sampgdk_AddStaticVehicle);
    add_call("AddStaticVehicleEx", (void *)sampgdk_AddStaticVehicleEx);
    add_call("AddStaticPickup", (void *)sampgdk_AddStaticPickup);
    add_call("CreatePickup", (void *)sampgdk_CreatePickup);
    add_call("DestroyPickup", (void *)sampgdk_DestroyPickup);
    add_call("ShowNameTags", (void *)sampgdk_ShowNameTags);
    add_call("ShowPlayerMarkers", (void *)sampgdk_ShowPlayerMarkers);
    add_call("GameModeExit", (void *)sampgdk_GameModeExit);
    add_call("SetWorldTime", (void *)sampgdk_SetWorldTime);
    add_call("GetWeaponName", (void *)p_GetWeaponName);
    add_call("EnableVehicleFriendlyFire", (void *)sampgdk_EnableVehicleFriendlyFire);
    add_call("AllowInteriorWeapons", (void *)sampgdk_AllowInteriorWeapons);
    add_call("SetWeather", (void *)sampgdk_SetWeather);
    add_call("SetGravity", (void *)sampgdk_SetGravity);
    add_call("GetGravity", (void *)sampgdk_GetGravity);
    add_call("CreateExplosion", (void *)sampgdk_CreateExplosion);
    add_call("UsePlayerPedAnims", (void *)sampgdk_UsePlayerPedAnims);
    add_call("DisableInteriorEnterExits", (void *)sampgdk_DisableInteriorEnterExits);
    add_call("SetNameTagDrawDistance", (void *)sampgdk_SetNameTagDrawDistance);
    add_call("DisableNameTagLOS", (void *)sampgdk_DisableNameTagLOS);
    add_call("LimitGlobalChatRadius", (void *)sampgdk_LimitGlobalChatRadius);
    add_call("LimitPlayerMarkerRadius", (void *)sampgdk_LimitPlayerMarkerRadius);
    add_call("ConnectNPC", (void *)p_ConnectNPC);
    add_call("IsPlayerNPC", (void *)sampgdk_IsPlayerNPC);
    add_call("IsPlayerAdmin", (void *)sampgdk_IsPlayerAdmin);
    add_call("Kick", (void *)sampgdk_Kick);
    add_call("Ban", (void *)sampgdk_Ban);
    add_call("BanEx", (void *)p_BanEx);
    add_call("SendRconCommand", (void *)p_SendRconCommand);
    add_call("GetServerVarAsString", (void *)p_GetServerVarAsString);
    add_call("GetServerVarAsInt", (void *)p_GetServerVarAsInt);
    add_call("GetServerVarAsBool", (void *)p_GetServerVarAsBool);
    add_call("GetPlayerNetworkStats", (void *)p_GetPlayerNetworkStats);
    add_call("GetNetworkStats", (void *)p_GetNetworkStats);
    add_call("GetPlayerVersion", (void *)p_GetPlayerVersion);
    add_call("CreateMenu", (void *)p_CreateMenu);
    add_call("DestroyMenu", (void *)sampgdk_DestroyMenu);
    add_call("AddMenuItem", (void *)p_AddMenuItem);
    add_call("SetMenuColumnHeader", (void *)p_SetMenuColumnHeader);
    add_call("ShowMenuForPlayer", (void *)sampgdk_ShowMenuForPlayer);
    add_call("HideMenuForPlayer", (void *)sampgdk_HideMenuForPlayer);
    add_call("IsValidMenu", (void *)sampgdk_IsValidMenu);
    add_call("DisableMenu", (void *)sampgdk_DisableMenu);
    add_call("DisableMenuRow", (void *)sampgdk_DisableMenuRow);
    add_call("GetPlayerMenu", (void *)sampgdk_GetPlayerMenu);
    add_call("TextDrawCreate", (void *)p_TextDrawCreate);
    add_call("TextDrawDestroy", (void *)sampgdk_TextDrawDestroy);
    add_call("TextDrawLetterSize", (void *)sampgdk_TextDrawLetterSize);
    add_call("TextDrawTextSize", (void *)sampgdk_TextDrawTextSize);
    add_call("TextDrawAlignment", (void *)sampgdk_TextDrawAlignment);
    add_call("TextDrawColor", (void *)sampgdk_TextDrawColor);
    add_call("TextDrawUseBox", (void *)sampgdk_TextDrawUseBox);
    add_call("TextDrawBoxColor", (void *)sampgdk_TextDrawBoxColor);
    add_call("TextDrawSetShadow", (void *)sampgdk_TextDrawSetShadow);
    add_call("TextDrawSetOutline", (void *)sampgdk_TextDrawSetOutline);
    add_call("TextDrawBackgroundColor", (void *)sampgdk_TextDrawBackgroundColor);
    add_call("TextDrawFont", (void *)sampgdk_TextDrawFont);
    add_call("TextDrawSetProportional", (void *)sampgdk_TextDrawSetProportional);
    add_call("TextDrawSetSelectable", (void *)sampgdk_TextDrawSetSelectable);
    add_call("TextDrawShowForPlayer", (void *)sampgdk_TextDrawShowForPlayer);
    add_call("TextDrawHideForPlayer", (void *)sampgdk_TextDrawHideForPlayer);
    add_call("TextDrawShowForAll", (void *)sampgdk_TextDrawShowForAll);
    add_call("TextDrawHideForAll", (void *)sampgdk_TextDrawHideForAll);
    add_call("TextDrawSetString", (void *)p_TextDrawSetString);
    add_call("TextDrawSetPreviewModel", (void *)sampgdk_TextDrawSetPreviewModel);
    add_call("TextDrawSetPreviewRot", (void *)sampgdk_TextDrawSetPreviewRot);
    add_call("TextDrawSetPreviewVehCol", (void *)sampgdk_TextDrawSetPreviewVehCol);
    add_call("SelectTextDraw", (void *)sampgdk_SelectTextDraw);
    add_call("CancelSelectTextDraw", (void *)sampgdk_CancelSelectTextDraw);
    add_call("GangZoneCreate", (void *)sampgdk_GangZoneCreate);
    add_call("GangZoneDestroy", (void *)sampgdk_GangZoneDestroy);
    add_call("GangZoneShowForPlayer", (void *)sampgdk_GangZoneShowForPlayer);
    add_call("GangZoneShowForAll", (void *)sampgdk_GangZoneShowForAll);
    add_call("GangZoneHideForPlayer", (void *)sampgdk_GangZoneHideForPlayer);
    add_call("GangZoneHideForAll", (void *)sampgdk_GangZoneHideForAll);
    add_call("GangZoneFlashForPlayer", (void *)sampgdk_GangZoneFlashForPlayer);
    add_call("GangZoneFlashForAll", (void *)sampgdk_GangZoneFlashForAll);
    add_call("GangZoneStopFlashForPlayer", (void *)sampgdk_GangZoneStopFlashForPlayer);
    add_call("GangZoneStopFlashForAll", (void *)sampgdk_GangZoneStopFlashForAll);
    add_call("Create3DTextLabel", (void *)p_Create3DTextLabel);
    add_call("Delete3DTextLabel", (void *)sampgdk_Delete3DTextLabel);
    add_call("Attach3DTextLabelToPlayer", (void *)sampgdk_Attach3DTextLabelToPlayer);
    add_call("Attach3DTextLabelToVehicle", (void *)sampgdk_Attach3DTextLabelToVehicle);
    add_call("Update3DTextLabelText", (void *)p_Update3DTextLabelText);
    add_call("CreatePlayer3DTextLabel", (void *)p_CreatePlayer3DTextLabel);
    add_call("DeletePlayer3DTextLabel", (void *)sampgdk_DeletePlayer3DTextLabel);
    add_call("UpdatePlayer3DTextLabelText", (void *)p_UpdatePlayer3DTextLabelText);
    add_call("ShowPlayerDialog", (void *)p_ShowPlayerDialog);
    add_call("gpci", (void *)p_gpci);
    add_call("SendDeathMessageToPlayer", (void *)sampgdk_SendDeathMessageToPlayer);
    add_call("BlockIpAddress", (void *)p_BlockIpAddress);
    add_call("UnBlockIpAddress", (void *)p_UnBlockIpAddress);

    //
    // a_objects natives
    add_call("CreateObject", (void *)sampgdk_CreateObject);
    add_call("AttachObjectToVehicle", (void *)sampgdk_AttachObjectToVehicle);
    add_call("AttachObjectToObject", (void *)sampgdk_AttachObjectToObject);
    add_call("AttachObjectToPlayer", (void *)sampgdk_AttachObjectToPlayer);
    add_call("SetObjectPos", (void *)sampgdk_SetObjectPos);
    add_call("GetObjectPos", (void *)sampgdk_GetObjectPos);
    add_call("SetObjectRot", (void *)sampgdk_SetObjectRot);
    add_call("GetObjectRot", (void *)sampgdk_GetObjectRot);
    add_call("GetObjectModel", (void *)sampgdk_GetObjectModel);
    add_call("SetObjectNoCameraCol", (void *)sampgdk_SetObjectNoCameraCol);
    add_call("IsValidObject", (void *)sampgdk_IsValidObject);
    add_call("DestroyObject", (void *)sampgdk_DestroyObject);
    add_call("MoveObject", (void *)sampgdk_MoveObject);
    add_call("StopObject", (void *)sampgdk_StopObject);
    add_call("IsObjectMoving", (void *)sampgdk_IsObjectMoving);
    add_call("EditObject", (void *)sampgdk_EditObject);
    add_call("EditPlayerObject", (void *)sampgdk_EditPlayerObject);
    add_call("SelectObject", (void *)sampgdk_SelectObject);
    add_call("CancelEdit", (void *)sampgdk_CancelEdit);
    add_call("CreatePlayerObject", (void *)sampgdk_CreatePlayerObject);
    add_call("AttachPlayerObjectToPlayer", (void *)sampgdk_AttachPlayerObjectToPlayer);
    add_call("AttachPlayerObjectToVehicle", (void *)sampgdk_AttachPlayerObjectToVehicle);
    add_call("SetPlayerObjectPos", (void *)sampgdk_SetPlayerObjectPos);
    add_call("GetPlayerObjectPos", (void *)sampgdk_GetPlayerObjectPos);
    add_call("SetPlayerObjectRot", (void *)sampgdk_SetPlayerObjectRot);
    add_call("GetPlayerObjectRot", (void *)sampgdk_GetPlayerObjectRot);
    add_call("GetPlayerObjectModel", (void *)sampgdk_GetPlayerObjectModel);
    add_call("SetPlayerObjectNoCameraCol", (void *)sampgdk_SetPlayerObjectNoCameraCol);
    add_call("IsValidPlayerObject", (void *)sampgdk_IsValidPlayerObject);
    add_call("DestroyPlayerObject", (void *)sampgdk_DestroyPlayerObject);
    add_call("MovePlayerObject", (void *)sampgdk_MovePlayerObject);
    add_call("StopPlayerObject", (void *)sampgdk_StopPlayerObject);
    add_call("IsPlayerObjectMoving", (void *)sampgdk_IsPlayerObjectMoving);
    add_call("SetObjectsDefaultCameraCol", (void *)sampgdk_SetObjectsDefaultCameraCol);
    add_call("SetObjectMaterial", (void *)p_SetObjectMaterial);
    add_call("SetPlayerObjectMaterial", (void *)p_SetPlayerObjectMaterial);
    add_call("SetObjectMaterialText", (void *)p_SetObjectMaterialText);
    add_call("SetPlayerObjectMaterialText", (void *)p_SetPlayerObjectMaterialText);

    //
    // a_vehicles natives
    add_call("IsValidVehicle", (void *)sampgdk_IsValidVehicle);
    add_call("GetVehicleDistanceFromPoint", (void *)sampgdk_GetVehicleDistanceFromPoint);
    add_call("CreateVehicle", (void *)sampgdk_CreateVehicle);
    add_call("DestroyVehicle", (void *)sampgdk_DestroyVehicle);
    add_call("IsVehicleStreamedIn", (void *)sampgdk_IsVehicleStreamedIn);
    add_call("GetVehiclePos", (void *)sampgdk_GetVehiclePos);
    add_call("SetVehiclePos", (void *)sampgdk_SetVehiclePos);
    add_call("GetVehicleZAngle", (void *)sampgdk_GetVehicleZAngle);
    add_call("GetVehicleRotationQuat", (void *)sampgdk_GetVehicleRotationQuat);
    add_call("SetVehicleZAngle", (void *)sampgdk_SetVehicleZAngle);
    add_call("SetVehicleParamsForPlayer", (void *)sampgdk_SetVehicleParamsForPlayer);
    add_call("ManualVehicleEngineAndLights", (void *)sampgdk_ManualVehicleEngineAndLights);
    add_call("SetVehicleParamsEx", (void *)sampgdk_SetVehicleParamsEx);
    add_call("GetVehicleParamsEx", (void *)sampgdk_GetVehicleParamsEx);
    add_call("GetVehicleParamsSirenState", (void *)sampgdk_GetVehicleParamsSirenState);
    add_call("SetVehicleParamsCarDoors", (void *)sampgdk_SetVehicleParamsCarDoors);
    add_call("GetVehicleParamsCarDoors", (void *)sampgdk_GetVehicleParamsCarDoors);
    add_call("SetVehicleParamsCarWindows", (void *)sampgdk_SetVehicleParamsCarWindows);
    add_call("GetVehicleParamsCarWindows", (void *)sampgdk_GetVehicleParamsCarWindows);
    add_call("SetVehicleToRespawn", (void *)sampgdk_SetVehicleToRespawn);
    add_call("LinkVehicleToInterior", (void *)sampgdk_LinkVehicleToInterior);
    add_call("AddVehicleComponent", (void *)sampgdk_AddVehicleComponent);
    add_call("RemoveVehicleComponent", (void *)sampgdk_RemoveVehicleComponent);
    add_call("ChangeVehicleColor", (void *)sampgdk_ChangeVehicleColor);
    add_call("ChangeVehiclePaintjob", (void *)sampgdk_ChangeVehiclePaintjob);
    add_call("SetVehicleHealth", (void *)sampgdk_SetVehicleHealth);
    add_call("GetVehicleHealth", (void *)sampgdk_GetVehicleHealth);
    add_call("AttachTrailerToVehicle", (void *)sampgdk_AttachTrailerToVehicle);
    add_call("DetachTrailerFromVehicle", (void *)sampgdk_DetachTrailerFromVehicle);
    add_call("IsTrailerAttachedToVehicle", (void *)sampgdk_IsTrailerAttachedToVehicle);
    add_call("GetVehicleTrailer", (void *)sampgdk_GetVehicleTrailer);
    add_call("SetVehicleNumberPlate", (void *)p_SetVehicleNumberPlate);
    add_call("GetVehicleModel", (void *)sampgdk_GetVehicleModel);
    add_call("GetVehicleComponentInSlot", (void *)sampgdk_GetVehicleComponentInSlot);
    add_call("GetVehicleComponentType", (void *)sampgdk_GetVehicleComponentType);
    add_call("RepairVehicle", (void *)sampgdk_RepairVehicle);
    add_call("GetVehicleVelocity", (void *)sampgdk_GetVehicleVelocity);
    add_call("SetVehicleVelocity", (void *)sampgdk_SetVehicleVelocity);
    add_call("SetVehicleAngularVelocity", (void *)sampgdk_SetVehicleAngularVelocity);
    add_call("GetVehicleDamageStatus", (void *)sampgdk_GetVehicleDamageStatus);
    add_call("UpdateVehicleDamageStatus", (void *)sampgdk_UpdateVehicleDamageStatus);
    add_call("SetVehicleVirtualWorld", (void *)sampgdk_SetVehicleVirtualWorld);
    add_call("GetVehicleVirtualWorld", (void *)sampgdk_GetVehicleVirtualWorld);
    add_call("GetVehicleModelInfo", (void *)sampgdk_GetVehicleModelInfo);

    //
    // a_actor natives
    add_call("CreateActor", (void *)sampgdk_CreateActor);
    add_call("DestroyActor", (void *)sampgdk_DestroyActor);
    add_call("IsActorStreamedIn", (void *)sampgdk_IsActorStreamedIn);
    add_call("SetActorVirtualWorld", (void *)sampgdk_SetActorVirtualWorld);
    add_call("GetActorVirtualWorld", (void *)sampgdk_GetActorVirtualWorld);
    add_call("ApplyActorAnimation", (void *)p_ApplyActorAnimation);
    add_call("ClearActorAnimations", (void *)sampgdk_ClearActorAnimations);
    add_call("SetActorPos", (void *)sampgdk_SetActorPos);
    add_call("GetActorPos", (void *)sampgdk_GetActorPos);
    add_call("SetActorFacingAngle", (void *)sampgdk_SetActorFacingAngle);
    add_call("GetActorFacingAngle", (void *)sampgdk_GetActorFacingAngle);
    add_call("SetActorHealth", (void *)sampgdk_SetActorHealth);
    add_call("GetActorHealth", (void *)sampgdk_GetActorHealth);
    add_call("SetActorInvulnerable", (void *)sampgdk_SetActorInvulnerable);
    add_call("IsActorInvulnerable", (void *)sampgdk_IsActorInvulnerable);
    add_call("IsValidActor", (void *)sampgdk_IsValidActor);

    //
    // logging
    add_call("Print", (void *)p_Print);
    add_call("SetCodepage", (void *)set_codepage);

    //
    // natives
    add_call("CallNativeArray", (void *)call_native_array);
    add_call("NativeExists", (void *)native_exists);
}
