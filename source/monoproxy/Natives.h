#pragma once

#include <sampgdk/a_players.h>
#include <sampgdk/a_samp.h>
#include <sampgdk/a_objects.h>
#include <sampgdk/a_vehicles.h>

#include "Callbacks.h"

//
//a_players string converters
static inline int p_SetPlayerName(int playerid, MonoString * name) {
	return sampgdk_SetPlayerName(playerid, mono_string_to_utf8(name));
}
static inline bool p_PlayAudioStreamForPlayer(int playerid, MonoString * url, float posX, float posY, float posZ, float distance, bool usepos) {
	return sampgdk_PlayAudioStreamForPlayer(playerid, mono_string_to_utf8(url), posX, posY, posZ, distance, usepos);
}
static inline bool p_SetPlayerShopName(int playerid, MonoString * shopname) {
	return sampgdk_SetPlayerShopName(playerid, mono_string_to_utf8(shopname));
}
static inline int p_CreatePlayerTextDraw(int playerid, float x, float y, MonoString * text) {
	return sampgdk_CreatePlayerTextDraw(playerid, x, y, mono_string_to_utf8(text));
}
static inline bool p_PlayerTextDrawSetString(int playerid, int text, MonoString * string) {
	return sampgdk_PlayerTextDrawSetString(playerid, text, mono_string_to_utf8(string));
}
static inline bool p_SetPVarInt(int playerid, MonoString * varname, int value) {
	return sampgdk_SetPVarInt(playerid, mono_string_to_utf8(varname), value);
}
static inline int p_GetPVarInt(int playerid, MonoString * varname) {
	return sampgdk_GetPVarInt(playerid, mono_string_to_utf8(varname));
}
static inline bool p_SetPVarString(int playerid, MonoString * varname, MonoString * value) {
	return sampgdk_SetPVarString(playerid, mono_string_to_utf8(varname), mono_string_to_utf8(value));
}
static inline bool p_GetPVarString(int playerid, MonoString * varname, MonoString ** value, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetPVarString(playerid, mono_string_to_utf8(varname), buffer, size);
	*value = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static inline bool p_SetPVarFloat(int playerid, MonoString * varname, float value) {
	return sampgdk_SetPVarFloat(playerid, mono_string_to_utf8(varname), value);
}
static inline float p_GetPVarFloat(int playerid, MonoString * varname) {
	return sampgdk_GetPVarFloat(playerid, mono_string_to_utf8(varname));
}
static inline bool p_DeletePVar(int playerid, MonoString * varname) {
	return sampgdk_DeletePVar(playerid, mono_string_to_utf8(varname));
}
static inline int p_GetPVarType(int playerid, MonoString * varname) {
	return sampgdk_GetPVarType(playerid, mono_string_to_utf8(varname));
}
static inline bool p_SetPlayerChatBubble(int playerid, MonoString * text, int color, float drawdistance, int expiretime) {
	return sampgdk_SetPlayerChatBubble(playerid, mono_string_to_utf8(text), color, drawdistance, expiretime);
}
static inline bool p_ApplyAnimation(int playerid, MonoString * animlib, MonoString * animname, float fDelta, bool loop, bool lockx, bool locky, bool freeze, int time, bool forcesync) {
	return sampgdk_ApplyAnimation(playerid, mono_string_to_utf8(animlib), mono_string_to_utf8(animname), fDelta, loop, lockx, locky, freeze, time, forcesync);
}
static inline bool p_StartRecordingPlayerData(int playerid, int recordtype, MonoString * recordname) {
	return sampgdk_StartRecordingPlayerData(playerid, recordtype, mono_string_to_utf8(recordname));
}
static inline bool p_GetPlayerIp(int playerid, MonoString ** ip, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetPlayerIp(playerid, buffer, size);
	*ip = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static inline int p_GetPlayerName(int playerid, MonoString ** name, int size) {
	char * buffer = new char[size];
	int retint = sampgdk_GetPlayerName(playerid, buffer, size);
	*name = mono_string_new(mono_domain_get(), buffer);
	return retint;
}
static inline bool p_GetPVarNameAtIndex(int playerid, int index, MonoString ** varname, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetPVarNameAtIndex(playerid, index, buffer, size);
	*varname = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static inline bool p_GetAnimationName(int index, MonoString ** animlib, int animlib_size, MonoString ** animname, int animname_size) {
	char * libbuffer = new char[animlib_size];
	char * namebuffer = new char[animname_size];

	bool retbool = sampgdk_GetAnimationName(index, libbuffer, animlib_size, namebuffer, animname_size);
	*animlib = mono_string_new(mono_domain_get(), libbuffer);
	*animname = mono_string_new(mono_domain_get(), namebuffer);
	return retbool;
}

//
//a_samp string converters
static inline bool p_SendClientMessage(int playerid, int color, MonoString * message) {
	return sampgdk_SendClientMessage(playerid, color, mono_string_to_utf8(message));
}
static inline bool p_SendClientMessageToAll(int color, MonoString * message) {
	return sampgdk_SendClientMessageToAll(color, mono_string_to_utf8(message));
}
static inline bool p_SendPlayerMessageToPlayer(int playerid, int senderid, MonoString * message) {
	return sampgdk_SendPlayerMessageToPlayer(playerid, senderid, mono_string_to_utf8(message));
}
static inline bool p_SendPlayerMessageToAll(int senderid, MonoString * message) {
	return sampgdk_SendPlayerMessageToAll(senderid, mono_string_to_utf8(message));
}
static inline bool p_GameTextForAll(MonoString * text, int time, int style) {
	return sampgdk_GameTextForAll(mono_string_to_utf8(text), time, style);
}
static inline bool p_GameTextForPlayer(int playerid, MonoString * text, int time, int style) {
	return sampgdk_GameTextForPlayer(playerid, mono_string_to_utf8(text), time, style);
}
static inline bool p_SetGameModeText(MonoString * text) {
	return sampgdk_SetGameModeText(mono_string_to_utf8(text));
}
static inline bool p_ConnectNPC(MonoString * name, MonoString * script) {
	return sampgdk_ConnectNPC(mono_string_to_utf8(name), mono_string_to_utf8(script));
}
static inline bool p_BanEx(int playerid, MonoString * reason) {
	return sampgdk_BanEx(playerid, mono_string_to_utf8(reason));
}
static inline bool p_SendRconCommand(MonoString * command) {
	return sampgdk_SendRconCommand(mono_string_to_utf8(command));
}
static inline bool p_GetServerVarAsString(MonoString * varname, MonoString ** value, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetServerVarAsString(mono_string_to_utf8(varname), buffer, size);
	*value = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static inline int p_GetServerVarAsInt(MonoString * varname) {
	return sampgdk_GetServerVarAsInt(mono_string_to_utf8(varname));
}
static inline bool p_GetServerVarAsBool(MonoString * varname) {
	return sampgdk_GetServerVarAsBool(mono_string_to_utf8(varname));
}
static inline int p_CreateMenu(MonoString * title, int columns, float x, float y, float col1width, float col2width) {
	return sampgdk_CreateMenu(mono_string_to_utf8(title), columns, x, y, col1width, col2width);
}
static inline int p_AddMenuItem(int menuid, int column, MonoString * menutext) {
	return sampgdk_AddMenuItem(menuid, column, mono_string_to_utf8(menutext));
}
static inline bool p_SetMenuColumnHeader(int menuid, int column, MonoString * columnheader) {
	return sampgdk_SetMenuColumnHeader(menuid, column, mono_string_to_utf8(columnheader));
}
static inline int p_TextDrawCreate(float x, float y, MonoString * text) {
	return sampgdk_TextDrawCreate(x, y, mono_string_to_utf8(text));
}
static inline bool p_TextDrawSetString(int text, MonoString * string) {
	return sampgdk_TextDrawSetString(text, mono_string_to_utf8(string));
}
static inline int p_Create3DTextLabel(MonoString * text, int color, float x, float y, float z, float DrawDistance, int virtualworld, bool testLOS) {
	return sampgdk_Create3DTextLabel(mono_string_to_utf8(text), color, x, y, z, DrawDistance, virtualworld, testLOS);
}
static inline bool p_Update3DTextLabelText(int id, int color, MonoString * text) {
	return sampgdk_Update3DTextLabelText(id, color, mono_string_to_utf8(text));
}
static inline int p_CreatePlayer3DTextLabel(int playerid, MonoString * text, int color, float x, float y, float z, float DrawDistance, int attachedplayer, int attachedvehicle, bool testLOS) {
	return sampgdk_CreatePlayer3DTextLabel(playerid, mono_string_to_utf8(text), color, x, y, z, DrawDistance, attachedplayer, attachedvehicle, testLOS);
}
static inline bool p_UpdatePlayer3DTextLabelText(int playerid, int id, int color, MonoString * text) {
	return sampgdk_UpdatePlayer3DTextLabelText(playerid, id, color, mono_string_to_utf8(text));
}
static inline bool p_ShowPlayerDialog(int playerid, int dialogid, int style, MonoString * caption, MonoString * info, MonoString * button1, MonoString * button2) {
	return sampgdk_ShowPlayerDialog(playerid, dialogid, style, mono_string_to_utf8(caption), mono_string_to_utf8(info), mono_string_to_utf8(button1), mono_string_to_utf8(button2));
}
static inline bool p_GetWeaponName(int weaponid, MonoString ** name, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetWeaponName(weaponid, buffer, size);
	*name = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static inline bool p_GetPlayerNetworkStats(int playerid, MonoString ** retstr, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetPlayerNetworkStats(playerid, buffer, size);
	*retstr = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static inline bool p_GetPlayerVersion(int playerid, MonoString ** version, int len) {
	char * buffer = new char[len];
	bool retbool = sampgdk_GetPlayerVersion(playerid, buffer, len);
	*version = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static inline bool p_gpci(int playerid, MonoString ** buffer, int size) {
	char * b_buffer = new char[size];
	bool retbool = sampgdk_gpci(playerid, b_buffer, size);
	*buffer = mono_string_new(mono_domain_get(), b_buffer);
	return retbool;
}
static inline bool p_GetNetworkStats(MonoString ** retstr, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetNetworkStats(buffer, size);
	*retstr = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static inline int p_SetTimer(int interval, bool repeat, MonoObject * params) {
	return SetTimer(interval, repeat, p_TimerCallback, params);
}

//
// a_objects string converters
static inline bool p_SetObjectMaterial(int objectid, int materialindex, int modelid, MonoString * txdname, MonoString * texturename, int materialcolor) {
	return sampgdk_SetObjectMaterial(objectid, materialindex, modelid, mono_string_to_utf8(txdname), mono_string_to_utf8(texturename), materialcolor);
}
static inline bool p_SetPlayerObjectMaterial(int playerid, int objectid, int materialindex, int modelid, MonoString * txdname, MonoString * texturename, int materialcolor) {
	return sampgdk_SetPlayerObjectMaterial(playerid, objectid, materialindex, modelid, mono_string_to_utf8(txdname), mono_string_to_utf8(texturename), materialcolor);
}
static inline bool p_SetObjectMaterialText(int objectid, MonoString * text, int materialindex, int materialsize, MonoString * fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment) {
	return sampgdk_SetObjectMaterialText(objectid, mono_string_to_utf8(text), materialindex, materialsize, mono_string_to_utf8(fontface), fontsize, bold, fontcolor, backcolor, textalignment);
}
static inline bool p_SetPlayerObjectMaterialText(int playerid, int objectid, MonoString * text, int materialindex, int materialsize, MonoString * fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment) {
	return sampgdk_SetPlayerObjectMaterialText(playerid, objectid, mono_string_to_utf8(text), materialindex, materialsize, mono_string_to_utf8(fontface), fontsize, bold, fontcolor, backcolor, textalignment);
}

//
// a_vehicles string converters
static inline bool p_SetVehicleNumberPlate(int vehicleid, MonoString * numberplate) {
	return sampgdk_SetVehicleNumberPlate(vehicleid, mono_string_to_utf8(numberplate));
}

static void LoadNatives()
{
	//
	//a_players natives
	mono_add_internal_call("GameMode.Native::SetSpawnInfo", sampgdk_SetSpawnInfo);
	mono_add_internal_call("GameMode.Native::SpawnPlayer", sampgdk_SpawnPlayer);
	mono_add_internal_call("GameMode.Native::SetPlayerPos", sampgdk_SetPlayerPos);
	mono_add_internal_call("GameMode.Native::SetPlayerPosFindZ", sampgdk_SetPlayerPosFindZ);
	mono_add_internal_call("GameMode.Native::GetPlayerPos", sampgdk_GetPlayerPos);
	mono_add_internal_call("GameMode.Native::SetPlayerFacingAngle", sampgdk_SetPlayerFacingAngle);
	mono_add_internal_call("GameMode.Native::GetPlayerFacingAngle", sampgdk_GetPlayerFacingAngle);
	mono_add_internal_call("GameMode.Native::IsPlayerInRangeOfPoint", sampgdk_IsPlayerInRangeOfPoint);
	mono_add_internal_call("GameMode.Native::GetPlayerDistanceFromPoint", sampgdk_GetPlayerDistanceFromPoint);
	mono_add_internal_call("GameMode.Native::IsPlayerStreamedIn", sampgdk_IsPlayerStreamedIn);
	mono_add_internal_call("GameMode.Native::SetPlayerInterior", sampgdk_SetPlayerInterior);
	mono_add_internal_call("GameMode.Native::GetPlayerInterior", sampgdk_GetPlayerInterior);
	mono_add_internal_call("GameMode.Native::SetPlayerHealth", sampgdk_SetPlayerHealth);
	mono_add_internal_call("GameMode.Native::GetPlayerHealth", sampgdk_GetPlayerHealth);
	mono_add_internal_call("GameMode.Native::SetPlayerArmour", sampgdk_SetPlayerArmour);
	mono_add_internal_call("GameMode.Native::GetPlayerArmour", sampgdk_GetPlayerArmour);
	mono_add_internal_call("GameMode.Native::SetPlayerAmmo", sampgdk_SetPlayerAmmo);
	mono_add_internal_call("GameMode.Native::GetPlayerAmmo", sampgdk_GetPlayerAmmo);
	mono_add_internal_call("GameMode.Native::GetPlayerWeaponState", sampgdk_GetPlayerWeaponState);
	mono_add_internal_call("GameMode.Native::GetPlayerTargetPlayer", sampgdk_GetPlayerTargetPlayer);
	mono_add_internal_call("GameMode.Native::SetPlayerTeam", sampgdk_SetPlayerTeam);
	mono_add_internal_call("GameMode.Native::GetPlayerTeam", sampgdk_GetPlayerTeam);
	mono_add_internal_call("GameMode.Native::SetPlayerScore", sampgdk_SetPlayerScore);
	mono_add_internal_call("GameMode.Native::GetPlayerScore", sampgdk_GetPlayerScore);
	mono_add_internal_call("GameMode.Native::GetPlayerDrunkLevel", sampgdk_GetPlayerDrunkLevel);
	mono_add_internal_call("GameMode.Native::SetPlayerDrunkLevel", sampgdk_SetPlayerDrunkLevel);
	mono_add_internal_call("GameMode.Native::SetPlayerColor", sampgdk_SetPlayerColor);
	mono_add_internal_call("GameMode.Native::GetPlayerColor", sampgdk_GetPlayerColor);
	mono_add_internal_call("GameMode.Native::SetPlayerSkin", sampgdk_SetPlayerSkin);
	mono_add_internal_call("GameMode.Native::GetPlayerSkin", sampgdk_GetPlayerSkin);
	mono_add_internal_call("GameMode.Native::GivePlayerWeapon", sampgdk_GivePlayerWeapon);
	mono_add_internal_call("GameMode.Native::ResetPlayerWeapons", sampgdk_ResetPlayerWeapons);
	mono_add_internal_call("GameMode.Native::SetPlayerArmedWeapon", sampgdk_SetPlayerArmedWeapon);
	mono_add_internal_call("GameMode.Native::GetPlayerWeaponData", sampgdk_GetPlayerWeaponData);
	mono_add_internal_call("GameMode.Native::GivePlayerMoney", sampgdk_GivePlayerMoney);
	mono_add_internal_call("GameMode.Native::ResetPlayerMoney", sampgdk_ResetPlayerMoney);
	mono_add_internal_call("GameMode.Native::SetPlayerName", p_SetPlayerName);
	mono_add_internal_call("GameMode.Native::GetPlayerMoney", sampgdk_GetPlayerMoney);
	mono_add_internal_call("GameMode.Native::GetPlayerState", sampgdk_GetPlayerState);
	mono_add_internal_call("GameMode.Native::GetPlayerIp", p_GetPlayerIp);
	mono_add_internal_call("GameMode.Native::GetPlayerPing", sampgdk_GetPlayerPing);
	mono_add_internal_call("GameMode.Native::GetPlayerWeapon", sampgdk_GetPlayerWeapon);
	mono_add_internal_call("GameMode.Native::GetPlayerKeys", sampgdk_GetPlayerKeys);
	mono_add_internal_call("GameMode.Native::GetPlayerName", p_GetPlayerName);
	mono_add_internal_call("GameMode.Native::SetPlayerTime", sampgdk_SetPlayerTime);
	mono_add_internal_call("GameMode.Native::GetPlayerTime", sampgdk_GetPlayerTime);
	mono_add_internal_call("GameMode.Native::TogglePlayerClock", sampgdk_TogglePlayerClock);
	mono_add_internal_call("GameMode.Native::SetPlayerWeather", sampgdk_SetPlayerWeather);
	mono_add_internal_call("GameMode.Native::ForceClassSelection", sampgdk_ForceClassSelection);
	mono_add_internal_call("GameMode.Native::SetPlayerWantedLevel", sampgdk_SetPlayerWantedLevel);
	mono_add_internal_call("GameMode.Native::GetPlayerWantedLevel", sampgdk_GetPlayerWantedLevel);
	mono_add_internal_call("GameMode.Native::SetPlayerFightingStyle", sampgdk_SetPlayerFightingStyle);
	mono_add_internal_call("GameMode.Native::GetPlayerFightingStyle", sampgdk_GetPlayerFightingStyle);
	mono_add_internal_call("GameMode.Native::SetPlayerVelocity", sampgdk_SetPlayerVelocity);
	mono_add_internal_call("GameMode.Native::GetPlayerVelocity", sampgdk_GetPlayerVelocity);
	mono_add_internal_call("GameMode.Native::PlayCrimeReportForPlayer", sampgdk_PlayCrimeReportForPlayer);
	mono_add_internal_call("GameMode.Native::PlayAudioStreamForPlayer", p_PlayAudioStreamForPlayer);
	mono_add_internal_call("GameMode.Native::StopAudioStreamForPlayer", sampgdk_StopAudioStreamForPlayer);
	mono_add_internal_call("GameMode.Native::SetPlayerShopName", p_SetPlayerShopName);
	mono_add_internal_call("GameMode.Native::SetPlayerSkillLevel", sampgdk_SetPlayerSkillLevel);
	mono_add_internal_call("GameMode.Native::GetPlayerSurfingVehicleID", sampgdk_GetPlayerSurfingVehicleID);
	mono_add_internal_call("GameMode.Native::GetPlayerSurfingObjectID", sampgdk_GetPlayerSurfingObjectID);
	mono_add_internal_call("GameMode.Native::RemoveBuildingForPlayer", sampgdk_RemoveBuildingForPlayer);
	mono_add_internal_call("GameMode.Native::SetPlayerAttachedObject", sampgdk_SetPlayerAttachedObject);
	mono_add_internal_call("GameMode.Native::RemovePlayerAttachedObject", sampgdk_RemovePlayerAttachedObject);
	mono_add_internal_call("GameMode.Native::IsPlayerAttachedObjectSlotUsed", sampgdk_IsPlayerAttachedObjectSlotUsed);
	mono_add_internal_call("GameMode.Native::EditAttachedObject", sampgdk_EditAttachedObject);
	mono_add_internal_call("GameMode.Native::CreatePlayerTextDraw", p_CreatePlayerTextDraw);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawDestroy", sampgdk_PlayerTextDrawDestroy);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawLetterSize", sampgdk_PlayerTextDrawLetterSize);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawTextSize", sampgdk_PlayerTextDrawTextSize);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawAlignment", sampgdk_PlayerTextDrawAlignment);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawColor", sampgdk_PlayerTextDrawColor);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawUseBox", sampgdk_PlayerTextDrawUseBox);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawBoxColor", sampgdk_PlayerTextDrawBoxColor);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawSetShadow", sampgdk_PlayerTextDrawSetShadow);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawSetOutline", sampgdk_PlayerTextDrawSetOutline);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawBackgroundColor", sampgdk_PlayerTextDrawBackgroundColor);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawFont", sampgdk_PlayerTextDrawFont);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawSetProportional", sampgdk_PlayerTextDrawSetProportional);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawSetSelectable", sampgdk_PlayerTextDrawSetSelectable);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawShow", sampgdk_PlayerTextDrawShow);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawHide", sampgdk_PlayerTextDrawHide);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawSetString", p_PlayerTextDrawSetString);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawSetPreviewModel", sampgdk_PlayerTextDrawSetPreviewModel);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawSetPreviewRot", sampgdk_PlayerTextDrawSetPreviewRot);
	mono_add_internal_call("GameMode.Native::PlayerTextDrawSetPreviewVehCol", sampgdk_PlayerTextDrawSetPreviewVehCol);
	mono_add_internal_call("GameMode.Native::SetPVarInt", p_SetPVarInt);
	mono_add_internal_call("GameMode.Native::GetPVarInt", p_GetPVarInt);
	mono_add_internal_call("GameMode.Native::SetPVarString", p_SetPVarString);
	mono_add_internal_call("GameMode.Native::GetPVarString", p_GetPVarString);
	mono_add_internal_call("GameMode.Native::SetPVarFloat", p_SetPVarFloat);
	mono_add_internal_call("GameMode.Native::GetPVarFloat", p_GetPVarFloat);
	mono_add_internal_call("GameMode.Native::DeletePVar", p_DeletePVar);
	mono_add_internal_call("GameMode.Native::GetPVarsUpperIndex", sampgdk_GetPVarsUpperIndex);
	mono_add_internal_call("GameMode.Native::GetPVarNameAtIndex", p_GetPVarNameAtIndex);
	mono_add_internal_call("GameMode.Native::GetPVarType", p_GetPVarType);
	mono_add_internal_call("GameMode.Native::SetPlayerChatBubble", p_SetPlayerChatBubble);
	mono_add_internal_call("GameMode.Native::PutPlayerInVehicle", sampgdk_PutPlayerInVehicle);
	mono_add_internal_call("GameMode.Native::GetPlayerVehicleID", sampgdk_GetPlayerVehicleID);
	mono_add_internal_call("GameMode.Native::GetPlayerVehicleSeat", sampgdk_GetPlayerVehicleSeat);
	mono_add_internal_call("GameMode.Native::RemovePlayerFromVehicle", sampgdk_RemovePlayerFromVehicle);
	mono_add_internal_call("GameMode.Native::TogglePlayerControllable", sampgdk_TogglePlayerControllable);
	mono_add_internal_call("GameMode.Native::PlayerPlaySound", sampgdk_PlayerPlaySound);
	mono_add_internal_call("GameMode.Native::ApplyAnimation", p_ApplyAnimation);
	mono_add_internal_call("GameMode.Native::ClearAnimations", sampgdk_ClearAnimations);
	mono_add_internal_call("GameMode.Native::GetPlayerAnimationIndex", sampgdk_GetPlayerAnimationIndex);
	mono_add_internal_call("GameMode.Native::GetAnimationName", p_GetAnimationName);
	mono_add_internal_call("GameMode.Native::GetPlayerSpecialAction", sampgdk_GetPlayerSpecialAction);
	mono_add_internal_call("GameMode.Native::SetPlayerSpecialAction", sampgdk_SetPlayerSpecialAction);
	mono_add_internal_call("GameMode.Native::SetPlayerCheckpoint", sampgdk_SetPlayerCheckpoint);
	mono_add_internal_call("GameMode.Native::DisablePlayerCheckpoint", sampgdk_DisablePlayerCheckpoint);
	mono_add_internal_call("GameMode.Native::SetPlayerRaceCheckpoint", sampgdk_SetPlayerRaceCheckpoint);
	mono_add_internal_call("GameMode.Native::DisablePlayerRaceCheckpoint", sampgdk_DisablePlayerRaceCheckpoint);
	mono_add_internal_call("GameMode.Native::SetPlayerWorldBounds", sampgdk_SetPlayerWorldBounds);
	mono_add_internal_call("GameMode.Native::SetPlayerMarkerForPlayer", sampgdk_SetPlayerMarkerForPlayer);
	mono_add_internal_call("GameMode.Native::ShowPlayerNameTagForPlayer", sampgdk_ShowPlayerNameTagForPlayer);
	mono_add_internal_call("GameMode.Native::SetPlayerMapIcon", sampgdk_SetPlayerMapIcon);
	mono_add_internal_call("GameMode.Native::RemovePlayerMapIcon", sampgdk_RemovePlayerMapIcon);
	mono_add_internal_call("GameMode.Native::AllowPlayerTeleport", sampgdk_AllowPlayerTeleport);
	mono_add_internal_call("GameMode.Native::SetPlayerCameraPos", sampgdk_SetPlayerCameraPos);
	mono_add_internal_call("GameMode.Native::SetPlayerCameraLookAt", sampgdk_SetPlayerCameraLookAt);
	mono_add_internal_call("GameMode.Native::SetCameraBehindPlayer", sampgdk_SetCameraBehindPlayer);
	mono_add_internal_call("GameMode.Native::GetPlayerCameraPos", sampgdk_GetPlayerCameraPos);
	mono_add_internal_call("GameMode.Native::GetPlayerCameraFrontVector", sampgdk_GetPlayerCameraFrontVector);
	mono_add_internal_call("GameMode.Native::GetPlayerCameraMode", sampgdk_GetPlayerCameraMode);
	mono_add_internal_call("GameMode.Native::AttachCameraToObject", sampgdk_AttachCameraToObject);
	mono_add_internal_call("GameMode.Native::AttachCameraToPlayerObject", sampgdk_AttachCameraToPlayerObject);
	mono_add_internal_call("GameMode.Native::InterpolateCameraPos", sampgdk_InterpolateCameraPos);
	mono_add_internal_call("GameMode.Native::InterpolateCameraLookAt", sampgdk_InterpolateCameraLookAt);
	mono_add_internal_call("GameMode.Native::IsPlayerConnected", sampgdk_IsPlayerConnected);
	mono_add_internal_call("GameMode.Native::IsPlayerInVehicle", sampgdk_IsPlayerInVehicle);
	mono_add_internal_call("GameMode.Native::IsPlayerInAnyVehicle", sampgdk_IsPlayerInAnyVehicle);
	mono_add_internal_call("GameMode.Native::IsPlayerInCheckpoint", sampgdk_IsPlayerInCheckpoint);
	mono_add_internal_call("GameMode.Native::IsPlayerInRaceCheckpoint", sampgdk_IsPlayerInRaceCheckpoint);
	mono_add_internal_call("GameMode.Native::SetPlayerVirtualWorld", sampgdk_SetPlayerVirtualWorld);
	mono_add_internal_call("GameMode.Native::GetPlayerVirtualWorld", sampgdk_GetPlayerVirtualWorld);
	mono_add_internal_call("GameMode.Native::EnableStuntBonusForPlayer", sampgdk_EnableStuntBonusForPlayer);
	mono_add_internal_call("GameMode.Native::EnableStuntBonusForAll", sampgdk_EnableStuntBonusForAll);
	mono_add_internal_call("GameMode.Native::TogglePlayerSpectating", sampgdk_TogglePlayerSpectating);
	mono_add_internal_call("GameMode.Native::PlayerSpectatePlayer", sampgdk_PlayerSpectatePlayer);
	mono_add_internal_call("GameMode.Native::PlayerSpectateVehicle", sampgdk_PlayerSpectateVehicle);
	mono_add_internal_call("GameMode.Native::StartRecordingPlayerData", p_StartRecordingPlayerData);
	mono_add_internal_call("GameMode.Native::StopRecordingPlayerData", sampgdk_StopRecordingPlayerData);

	//
	//a_samp natives
	mono_add_internal_call("GameMode.Native::SendClientMessage", p_SendClientMessage);
	mono_add_internal_call("GameMode.Native::SendClientMessageToAll", p_SendClientMessageToAll);
	mono_add_internal_call("GameMode.Native::SendPlayerMessageToPlayer", p_SendPlayerMessageToPlayer);
	mono_add_internal_call("GameMode.Native::SendPlayerMessageToAll", p_SendPlayerMessageToAll);
	mono_add_internal_call("GameMode.Native::SendDeathMessage", sampgdk_SendDeathMessage);
	mono_add_internal_call("GameMode.Native::GameTextForAll", p_GameTextForAll);
	mono_add_internal_call("GameMode.Native::GameTextForPlayer", p_GameTextForPlayer);
	mono_add_internal_call("GameMode.Native::GetTickCount", sampgdk_GetTickCount);
	mono_add_internal_call("GameMode.Native::GetMaxPlayers", sampgdk_GetMaxPlayers);
	mono_add_internal_call("GameMode.Native::SetGameModeText", p_SetGameModeText);
	mono_add_internal_call("GameMode.Native::SetTeamCount", sampgdk_SetTeamCount);
	mono_add_internal_call("GameMode.Native::AddPlayerClass", sampgdk_AddPlayerClass);
	mono_add_internal_call("GameMode.Native::AddPlayerClassEx", sampgdk_AddPlayerClassEx);
	mono_add_internal_call("GameMode.Native::AddStaticVehicle", sampgdk_AddStaticVehicle);
	mono_add_internal_call("GameMode.Native::AddStaticVehicleEx", sampgdk_AddStaticVehicleEx);
	mono_add_internal_call("GameMode.Native::AddStaticPickup", sampgdk_AddStaticPickup);
	mono_add_internal_call("GameMode.Native::CreatePickup", sampgdk_CreatePickup);
	mono_add_internal_call("GameMode.Native::DestroyPickup", sampgdk_DestroyPickup);
	mono_add_internal_call("GameMode.Native::ShowNameTags", sampgdk_ShowNameTags);
	mono_add_internal_call("GameMode.Native::ShowPlayerMarkers", sampgdk_ShowPlayerMarkers);
	mono_add_internal_call("GameMode.Native::GameModeExit", sampgdk_GameModeExit);
	mono_add_internal_call("GameMode.Native::SetWorldTime", sampgdk_SetWorldTime);
	mono_add_internal_call("GameMode.Native::GetWeaponName", p_GetWeaponName);
	mono_add_internal_call("GameMode.Native::EnableTirePopping", sampgdk_EnableTirePopping);
	mono_add_internal_call("GameMode.Native::EnableVehicleFriendlyFire", sampgdk_EnableVehicleFriendlyFire);
	mono_add_internal_call("GameMode.Native::AllowInteriorWeapons", sampgdk_AllowInteriorWeapons);
	mono_add_internal_call("GameMode.Native::SetWeather", sampgdk_SetWeather);
	mono_add_internal_call("GameMode.Native::SetGravity", sampgdk_SetGravity);
	mono_add_internal_call("GameMode.Native::AllowAdminTeleport", sampgdk_AllowAdminTeleport);
	mono_add_internal_call("GameMode.Native::SetDeathDropAmount", sampgdk_SetDeathDropAmount);
	mono_add_internal_call("GameMode.Native::CreateExplosion", sampgdk_CreateExplosion);
	mono_add_internal_call("GameMode.Native::EnableZoneNames", sampgdk_EnableZoneNames);
	mono_add_internal_call("GameMode.Native::UsePlayerPedAnims", sampgdk_UsePlayerPedAnims);
	mono_add_internal_call("GameMode.Native::DisableInteriorEnterExits", sampgdk_DisableInteriorEnterExits);
	mono_add_internal_call("GameMode.Native::SetNameTagDrawDistance", sampgdk_SetNameTagDrawDistance);
	mono_add_internal_call("GameMode.Native::DisableNameTagLOS", sampgdk_DisableNameTagLOS);
	mono_add_internal_call("GameMode.Native::LimitGlobalChatRadius", sampgdk_LimitGlobalChatRadius);
	mono_add_internal_call("GameMode.Native::LimitPlayerMarkerRadius", sampgdk_LimitPlayerMarkerRadius);
	mono_add_internal_call("GameMode.Native::ConnectNPC", p_ConnectNPC);
	mono_add_internal_call("GameMode.Native::IsPlayerNPC", sampgdk_IsPlayerNPC);
	mono_add_internal_call("GameMode.Native::IsPlayerAdmin", sampgdk_IsPlayerAdmin);
	mono_add_internal_call("GameMode.Native::Kick", sampgdk_Kick);
	mono_add_internal_call("GameMode.Native::Ban", sampgdk_Ban);
	mono_add_internal_call("GameMode.Native::BanEx", p_BanEx);
	mono_add_internal_call("GameMode.Native::SendRconCommand", p_SendRconCommand);
	mono_add_internal_call("GameMode.Native::GetServerVarAsString", p_GetServerVarAsString);
	mono_add_internal_call("GameMode.Native::GetServerVarAsInt", p_GetServerVarAsInt);
	mono_add_internal_call("GameMode.Native::GetServerVarAsBool", p_GetServerVarAsBool);
	mono_add_internal_call("GameMode.Native::GetPlayerNetworkStats", p_GetPlayerNetworkStats);
	mono_add_internal_call("GameMode.Native::GetNetworkStats", p_GetNetworkStats);
	mono_add_internal_call("GameMode.Native::GetPlayerVersion", p_GetPlayerVersion);
	mono_add_internal_call("GameMode.Native::CreateMenu", p_CreateMenu);
	mono_add_internal_call("GameMode.Native::DestroyMenu", sampgdk_DestroyMenu);
	mono_add_internal_call("GameMode.Native::AddMenuItem", p_AddMenuItem);
	mono_add_internal_call("GameMode.Native::SetMenuColumnHeader", p_SetMenuColumnHeader);
	mono_add_internal_call("GameMode.Native::ShowMenuForPlayer", sampgdk_ShowMenuForPlayer);
	mono_add_internal_call("GameMode.Native::HideMenuForPlayer", sampgdk_HideMenuForPlayer);
	mono_add_internal_call("GameMode.Native::IsValidMenu", sampgdk_IsValidMenu);
	mono_add_internal_call("GameMode.Native::DisableMenu", sampgdk_DisableMenu);
	mono_add_internal_call("GameMode.Native::DisableMenuRow", sampgdk_DisableMenuRow);
	mono_add_internal_call("GameMode.Native::GetPlayerMenu", sampgdk_GetPlayerMenu);
	mono_add_internal_call("GameMode.Native::TextDrawCreate", p_TextDrawCreate);
	mono_add_internal_call("GameMode.Native::TextDrawDestroy", sampgdk_TextDrawDestroy);
	mono_add_internal_call("GameMode.Native::TextDrawLetterSize", sampgdk_TextDrawLetterSize);
	mono_add_internal_call("GameMode.Native::TextDrawTextSize", sampgdk_TextDrawTextSize);
	mono_add_internal_call("GameMode.Native::TextDrawAlignment", sampgdk_TextDrawAlignment);
	mono_add_internal_call("GameMode.Native::TextDrawColor", sampgdk_TextDrawColor);
	mono_add_internal_call("GameMode.Native::TextDrawUseBox", sampgdk_TextDrawUseBox);
	mono_add_internal_call("GameMode.Native::TextDrawBoxColor", sampgdk_TextDrawBoxColor);
	mono_add_internal_call("GameMode.Native::TextDrawSetShadow", sampgdk_TextDrawSetShadow);
	mono_add_internal_call("GameMode.Native::TextDrawSetOutline", sampgdk_TextDrawSetOutline);
	mono_add_internal_call("GameMode.Native::TextDrawBackgroundColor", sampgdk_TextDrawBackgroundColor);
	mono_add_internal_call("GameMode.Native::TextDrawFont", sampgdk_TextDrawFont);
	mono_add_internal_call("GameMode.Native::TextDrawSetProportional", sampgdk_TextDrawSetProportional);
	mono_add_internal_call("GameMode.Native::TextDrawSetSelectable", sampgdk_TextDrawSetSelectable);
	mono_add_internal_call("GameMode.Native::TextDrawShowForPlayer", sampgdk_TextDrawShowForPlayer);
	mono_add_internal_call("GameMode.Native::TextDrawHideForPlayer", sampgdk_TextDrawHideForPlayer);
	mono_add_internal_call("GameMode.Native::TextDrawShowForAll", sampgdk_TextDrawShowForAll);
	mono_add_internal_call("GameMode.Native::TextDrawHideForAll", sampgdk_TextDrawHideForAll);
	mono_add_internal_call("GameMode.Native::TextDrawSetString", p_TextDrawSetString);
	mono_add_internal_call("GameMode.Native::TextDrawSetPreviewModel", sampgdk_TextDrawSetPreviewModel);
	mono_add_internal_call("GameMode.Native::TextDrawSetPreviewRot", sampgdk_TextDrawSetPreviewRot);
	mono_add_internal_call("GameMode.Native::TextDrawSetPreviewVehCol", sampgdk_TextDrawSetPreviewVehCol);
	mono_add_internal_call("GameMode.Native::SelectTextDraw", sampgdk_SelectTextDraw);
	mono_add_internal_call("GameMode.Native::CancelSelectTextDraw", sampgdk_CancelSelectTextDraw);
	mono_add_internal_call("GameMode.Native::GangZoneCreate", sampgdk_GangZoneCreate);
	mono_add_internal_call("GameMode.Native::GangZoneDestroy", sampgdk_GangZoneDestroy);
	mono_add_internal_call("GameMode.Native::GangZoneShowForPlayer", sampgdk_GangZoneShowForPlayer);
	mono_add_internal_call("GameMode.Native::GangZoneShowForAll", sampgdk_GangZoneShowForAll);
	mono_add_internal_call("GameMode.Native::GangZoneHideForPlayer", sampgdk_GangZoneHideForPlayer);
	mono_add_internal_call("GameMode.Native::GangZoneHideForAll", sampgdk_GangZoneHideForAll);
	mono_add_internal_call("GameMode.Native::GangZoneFlashForPlayer", sampgdk_GangZoneFlashForPlayer);
	mono_add_internal_call("GameMode.Native::GangZoneFlashForAll", sampgdk_GangZoneFlashForAll);
	mono_add_internal_call("GameMode.Native::GangZoneStopFlashForPlayer", sampgdk_GangZoneStopFlashForPlayer);
	mono_add_internal_call("GameMode.Native::GangZoneStopFlashForAll", sampgdk_GangZoneStopFlashForAll);
	mono_add_internal_call("GameMode.Native::Create3DTextLabel", p_Create3DTextLabel);
	mono_add_internal_call("GameMode.Native::Delete3DTextLabel", sampgdk_Delete3DTextLabel);
	mono_add_internal_call("GameMode.Native::Attach3DTextLabelToPlayer", sampgdk_Attach3DTextLabelToPlayer);
	mono_add_internal_call("GameMode.Native::Attach3DTextLabelToVehicle", sampgdk_Attach3DTextLabelToVehicle);
	mono_add_internal_call("GameMode.Native::Update3DTextLabelText", p_Update3DTextLabelText);
	mono_add_internal_call("GameMode.Native::CreatePlayer3DTextLabel", p_CreatePlayer3DTextLabel);
	mono_add_internal_call("GameMode.Native::DeletePlayer3DTextLabel", sampgdk_DeletePlayer3DTextLabel);
	mono_add_internal_call("GameMode.Native::UpdatePlayer3DTextLabelText", p_UpdatePlayer3DTextLabelText);
	mono_add_internal_call("GameMode.Native::ShowPlayerDialog", p_ShowPlayerDialog);
	mono_add_internal_call("GameMode.Native::SetTimer", p_SetTimer);
	mono_add_internal_call("GameMode.Native::KillTimer", sampgdk_KillTimer);
	mono_add_internal_call("GameMode.Native::gpci", p_gpci);

	//
	//a_objects natives
	mono_add_internal_call("GameMode.Native::CreateObject", sampgdk_CreateObject);
	mono_add_internal_call("GameMode.Native::AttachObjectToVehicle", sampgdk_AttachObjectToVehicle);
	mono_add_internal_call("GameMode.Native::AttachObjectToObject", sampgdk_AttachObjectToObject);
	mono_add_internal_call("GameMode.Native::AttachObjectToPlayer", sampgdk_AttachObjectToPlayer);
	mono_add_internal_call("GameMode.Native::SetObjectPos", sampgdk_SetObjectPos);
	mono_add_internal_call("GameMode.Native::GetObjectPos", sampgdk_GetObjectPos);
	mono_add_internal_call("GameMode.Native::SetObjectRot", sampgdk_SetObjectRot);
	mono_add_internal_call("GameMode.Native::GetObjectRot", sampgdk_GetObjectRot);
	mono_add_internal_call("GameMode.Native::IsValidObject", sampgdk_IsValidObject);
	mono_add_internal_call("GameMode.Native::DestroyObject", sampgdk_DestroyObject);
	mono_add_internal_call("GameMode.Native::MoveObject", sampgdk_MoveObject);
	mono_add_internal_call("GameMode.Native::StopObject", sampgdk_StopObject);
	mono_add_internal_call("GameMode.Native::IsObjectMoving", sampgdk_IsObjectMoving);
	mono_add_internal_call("GameMode.Native::EditObject", sampgdk_EditObject);
	mono_add_internal_call("GameMode.Native::EditPlayerObject", sampgdk_EditPlayerObject);
	mono_add_internal_call("GameMode.Native::SelectObject", sampgdk_SelectObject);
	mono_add_internal_call("GameMode.Native::CancelEdit", sampgdk_CancelEdit);
	mono_add_internal_call("GameMode.Native::CreatePlayerObject", sampgdk_CreatePlayerObject);
	mono_add_internal_call("GameMode.Native::AttachPlayerObjectToPlayer", sampgdk_AttachPlayerObjectToPlayer);
	mono_add_internal_call("GameMode.Native::AttachPlayerObjectToVehicle", sampgdk_AttachPlayerObjectToVehicle);
	mono_add_internal_call("GameMode.Native::SetPlayerObjectPos", sampgdk_SetPlayerObjectPos);
	mono_add_internal_call("GameMode.Native::GetPlayerObjectPos", sampgdk_GetPlayerObjectPos);
	mono_add_internal_call("GameMode.Native::SetPlayerObjectRot", sampgdk_SetPlayerObjectRot);
	mono_add_internal_call("GameMode.Native::GetPlayerObjectRot", sampgdk_GetPlayerObjectRot);
	mono_add_internal_call("GameMode.Native::IsValidPlayerObject", sampgdk_IsValidPlayerObject);
	mono_add_internal_call("GameMode.Native::DestroyPlayerObject", sampgdk_DestroyPlayerObject);
	mono_add_internal_call("GameMode.Native::MovePlayerObject", sampgdk_MovePlayerObject);
	mono_add_internal_call("GameMode.Native::StopPlayerObject", sampgdk_StopPlayerObject);
	mono_add_internal_call("GameMode.Native::IsPlayerObjectMoving", sampgdk_IsPlayerObjectMoving);
	mono_add_internal_call("GameMode.Native::SetObjectMaterial", p_SetObjectMaterial);
	mono_add_internal_call("GameMode.Native::SetPlayerObjectMaterial", p_SetPlayerObjectMaterial);
	mono_add_internal_call("GameMode.Native::SetObjectMaterialText", p_SetObjectMaterialText);
	mono_add_internal_call("GameMode.Native::SetPlayerObjectMaterialText", p_SetPlayerObjectMaterialText);

	//
	//a_vehicles natives
	mono_add_internal_call("GameMode.Native::IsValidVehicle", sampgdk_IsValidVehicle);
	mono_add_internal_call("GameMode.Native::GetVehicleDistanceFromPoint", sampgdk_GetVehicleDistanceFromPoint);
	mono_add_internal_call("GameMode.Native::CreateVehicle", sampgdk_CreateVehicle);
	mono_add_internal_call("GameMode.Native::DestroyVehicle", sampgdk_DestroyVehicle);
	mono_add_internal_call("GameMode.Native::IsVehicleStreamedIn", sampgdk_IsVehicleStreamedIn);
	mono_add_internal_call("GameMode.Native::GetVehiclePos", sampgdk_GetVehiclePos);
	mono_add_internal_call("GameMode.Native::SetVehiclePos", sampgdk_SetVehiclePos);
	mono_add_internal_call("GameMode.Native::GetVehicleZAngle", sampgdk_GetVehicleZAngle);
	mono_add_internal_call("GameMode.Native::GetVehicleRotationQuat", sampgdk_GetVehicleRotationQuat);
	mono_add_internal_call("GameMode.Native::SetVehicleZAngle", sampgdk_SetVehicleZAngle);
	mono_add_internal_call("GameMode.Native::SetVehicleParamsForPlayer", sampgdk_SetVehicleParamsForPlayer);
	mono_add_internal_call("GameMode.Native::ManualVehicleEngineAndLights", sampgdk_ManualVehicleEngineAndLights);
	mono_add_internal_call("GameMode.Native::SetVehicleParamsEx", sampgdk_SetVehicleParamsEx);
	mono_add_internal_call("GameMode.Native::GetVehicleParamsEx", sampgdk_GetVehicleParamsEx);
	mono_add_internal_call("GameMode.Native::SetVehicleToRespawn", sampgdk_SetVehicleToRespawn);
	mono_add_internal_call("GameMode.Native::LinkVehicleToInterior", sampgdk_LinkVehicleToInterior);
	mono_add_internal_call("GameMode.Native::AddVehicleComponent", sampgdk_AddVehicleComponent);
	mono_add_internal_call("GameMode.Native::RemoveVehicleComponent", sampgdk_RemoveVehicleComponent);
	mono_add_internal_call("GameMode.Native::ChangeVehicleColor", sampgdk_ChangeVehicleColor);
	mono_add_internal_call("GameMode.Native::ChangeVehiclePaintjob", sampgdk_ChangeVehiclePaintjob);
	mono_add_internal_call("GameMode.Native::SetVehicleHealth", sampgdk_SetVehicleHealth);
	mono_add_internal_call("GameMode.Native::GetVehicleHealth", sampgdk_GetVehicleHealth);
	mono_add_internal_call("GameMode.Native::AttachTrailerToVehicle", sampgdk_AttachTrailerToVehicle);
	mono_add_internal_call("GameMode.Native::DetachTrailerFromVehicle", sampgdk_DetachTrailerFromVehicle);
	mono_add_internal_call("GameMode.Native::IsTrailerAttachedToVehicle", sampgdk_IsTrailerAttachedToVehicle);
	mono_add_internal_call("GameMode.Native::GetVehicleTrailer", sampgdk_GetVehicleTrailer);
	mono_add_internal_call("GameMode.Native::SetVehicleNumberPlate", p_SetVehicleNumberPlate);
	mono_add_internal_call("GameMode.Native::GetVehicleModel", sampgdk_GetVehicleModel);
	mono_add_internal_call("GameMode.Native::GetVehicleComponentInSlot", sampgdk_GetVehicleComponentInSlot);
	mono_add_internal_call("GameMode.Native::GetVehicleComponentType", sampgdk_GetVehicleComponentType);
	mono_add_internal_call("GameMode.Native::RepairVehicle", sampgdk_RepairVehicle);
	mono_add_internal_call("GameMode.Native::GetVehicleVelocity", sampgdk_GetVehicleVelocity);
	mono_add_internal_call("GameMode.Native::SetVehicleVelocity", sampgdk_SetVehicleVelocity);
	mono_add_internal_call("GameMode.Native::SetVehicleAngularVelocity", sampgdk_SetVehicleAngularVelocity);
	mono_add_internal_call("GameMode.Native::GetVehicleDamageStatus", sampgdk_GetVehicleDamageStatus);
	mono_add_internal_call("GameMode.Native::UpdateVehicleDamageStatus", sampgdk_UpdateVehicleDamageStatus);
	mono_add_internal_call("GameMode.Native::SetVehicleVirtualWorld", sampgdk_SetVehicleVirtualWorld);
	mono_add_internal_call("GameMode.Native::GetVehicleVirtualWorld", sampgdk_GetVehicleVirtualWorld);
	mono_add_internal_call("GameMode.Native::GetVehicleModelInfo", sampgdk_GetVehicleModelInfo);
}