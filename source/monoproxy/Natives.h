#pragma once

#include <sampgdk/a_players.h>
#include <sampgdk/a_samp.h>

#include "Callbacks.h"

//String converters
static int p_SetPlayerName(int playerid, MonoString * name) {
	return sampgdk_SetPlayerName(playerid, mono_string_to_utf8(name));
}
static bool p_PlayAudioStreamForPlayer(int playerid, MonoString * url, float posX, float posY, float posZ, float distance, bool usepos) {
	return sampgdk_PlayAudioStreamForPlayer(playerid, mono_string_to_utf8(url), posX, posY, posZ, distance, usepos);
}
static bool p_SetPlayerShopName(int playerid, MonoString * shopname) {
	return sampgdk_SetPlayerShopName(playerid, mono_string_to_utf8(shopname));
}
static int p_CreatePlayerTextDraw(int playerid, float x, float y, MonoString * text) {
	return sampgdk_CreatePlayerTextDraw(playerid, x, y, mono_string_to_utf8(text));
}
static bool p_PlayerTextDrawSetString(int playerid, int text, MonoString * string) {
	return sampgdk_PlayerTextDrawSetString(playerid, text, mono_string_to_utf8(string));
}
static bool p_SetPVarInt(int playerid, MonoString * varname, int value) {
	return sampgdk_SetPVarInt(playerid, mono_string_to_utf8(varname), value);
}
static int p_GetPVarInt(int playerid, MonoString * varname) {
	return sampgdk_GetPVarInt(playerid, mono_string_to_utf8(varname));
}
static bool p_SetPVarString(int playerid, MonoString * varname, MonoString * value) {
	return sampgdk_SetPVarString(playerid, mono_string_to_utf8(varname), mono_string_to_utf8(value));
}
static bool p_GetPVarString(int playerid, MonoString * varname, MonoString ** value, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetPVarString(playerid, mono_string_to_utf8(varname), buffer, size);
	*value = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static bool p_SetPVarFloat(int playerid, MonoString * varname, float value) {
	return sampgdk_SetPVarFloat(playerid, mono_string_to_utf8(varname), value);
}
static float p_GetPVarFloat(int playerid, MonoString * varname) {
	return sampgdk_GetPVarFloat(playerid, mono_string_to_utf8(varname));
}
static bool p_DeletePVar(int playerid, MonoString * varname) {
	return sampgdk_DeletePVar(playerid, mono_string_to_utf8(varname));
}
static int p_GetPVarType(int playerid, MonoString * varname) {
	return sampgdk_GetPVarType(playerid, mono_string_to_utf8(varname));
}
static bool p_SetPlayerChatBubble(int playerid, MonoString * text, int color, float drawdistance, int expiretime) {
	return sampgdk_SetPlayerChatBubble(playerid, mono_string_to_utf8(text), color, drawdistance, expiretime);
}
static bool p_ApplyAnimation(int playerid, MonoString * animlib, MonoString * animname, float fDelta, bool loop, bool lockx, bool locky, bool freeze, int time, bool forcesync) {
	return sampgdk_ApplyAnimation(playerid, mono_string_to_utf8(animlib), mono_string_to_utf8(animname), fDelta, loop, lockx, locky, freeze, time, forcesync);
}
static bool p_StartRecordingPlayerData(int playerid, int recordtype, MonoString * recordname) {
	return sampgdk_StartRecordingPlayerData(playerid, recordtype, mono_string_to_utf8(recordname));
}
static bool p_SendClientMessage(int playerid, int color, MonoString * message) {
	return sampgdk_SendClientMessage(playerid, color, mono_string_to_utf8(message));
}
static bool p_SendClientMessageToAll(int color, MonoString * message) {
	return sampgdk_SendClientMessageToAll(color, mono_string_to_utf8(message));
}
static bool p_SendPlayerMessageToPlayer(int playerid, int senderid, MonoString * message) {
	return sampgdk_SendPlayerMessageToPlayer(playerid, senderid, mono_string_to_utf8(message));
}
static bool p_SendPlayerMessageToAll(int senderid, MonoString * message) {
	return sampgdk_SendPlayerMessageToAll(senderid, mono_string_to_utf8(message));
}
static bool p_GameTextForAll(MonoString * text, int time, int style) {
	return sampgdk_GameTextForAll(mono_string_to_utf8(text), time, style);
}
static bool p_GameTextForPlayer(int playerid, MonoString * text, int time, int style) {
	return sampgdk_GameTextForPlayer(playerid, mono_string_to_utf8(text), time, style);
}
static bool p_SetGameModeText(MonoString * text) {
	return sampgdk_SetGameModeText(mono_string_to_utf8(text));
}
static bool p_ConnectNPC(MonoString * name, MonoString * script) {
	return sampgdk_ConnectNPC(mono_string_to_utf8(name), mono_string_to_utf8(script));
}
static bool p_BanEx(int playerid, MonoString * reason) {
	return sampgdk_BanEx(playerid, mono_string_to_utf8(reason));
}
static bool p_SendRconCommand(MonoString * command) {
	return sampgdk_SendRconCommand(mono_string_to_utf8(command));
}
static bool p_GetServerVarAsString(MonoString * varname, MonoString ** value, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetServerVarAsString(mono_string_to_utf8(varname), buffer, size);
	*value = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static int p_GetServerVarAsInt(MonoString * varname) {
	return sampgdk_GetServerVarAsInt(mono_string_to_utf8(varname));
}
static bool p_GetServerVarAsBool(MonoString * varname) {
	return sampgdk_GetServerVarAsBool(mono_string_to_utf8(varname));
}
static int p_CreateMenu(MonoString * title, int columns, float x, float y, float col1width, float col2width) {
	return sampgdk_CreateMenu(mono_string_to_utf8(title), columns, x, y, col1width, col2width);
}
static int p_AddMenuItem(int menuid, int column, MonoString * menutext) {
	return sampgdk_AddMenuItem(menuid, column, mono_string_to_utf8(menutext));
}
static bool p_SetMenuColumnHeader(int menuid, int column, MonoString * columnheader) {
	return sampgdk_SetMenuColumnHeader(menuid, column, mono_string_to_utf8(columnheader));
}
static int p_TextDrawCreate(float x, float y, MonoString * text) {
	return sampgdk_TextDrawCreate(x, y, mono_string_to_utf8(text));
}
static bool p_TextDrawSetString(int text, MonoString * string) {
	return sampgdk_TextDrawSetString(text, mono_string_to_utf8(string));
}
static int p_Create3DTextLabel(MonoString * text, int color, float x, float y, float z, float DrawDistance, int virtualworld, bool testLOS) {
	return sampgdk_Create3DTextLabel(mono_string_to_utf8(text), color, x, y, z, DrawDistance, virtualworld, testLOS);
}
static bool p_Update3DTextLabelText(int id, int color, MonoString * text) {
	return sampgdk_Update3DTextLabelText(id, color, mono_string_to_utf8(text));
}
static int p_CreatePlayer3DTextLabel(int playerid, MonoString * text, int color, float x, float y, float z, float DrawDistance, int attachedplayer, int attachedvehicle, bool testLOS) {
	return sampgdk_CreatePlayer3DTextLabel(playerid, mono_string_to_utf8(text), color, x, y, z, DrawDistance, attachedplayer, attachedvehicle, testLOS);
}
static bool p_UpdatePlayer3DTextLabelText(int playerid, int id, int color, MonoString * text) {
	return sampgdk_UpdatePlayer3DTextLabelText(playerid, id, color, mono_string_to_utf8(text));
}
static bool p_ShowPlayerDialog(int playerid, int dialogid, int style, MonoString * caption, MonoString * info, MonoString * button1, MonoString * button2) {
	return sampgdk_ShowPlayerDialog(playerid, dialogid, style, mono_string_to_utf8(caption), mono_string_to_utf8(info), mono_string_to_utf8(button1), mono_string_to_utf8(button2));
}
static bool p_GetPlayerIp(int playerid, MonoString ** ip, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetPlayerIp(playerid, buffer, size);
	*ip = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static int p_GetPlayerName(int playerid, MonoString ** name, int size) {
	char * buffer = new char[size];
	int retint = sampgdk_GetPlayerName(playerid, buffer, size);
	*name = mono_string_new(mono_domain_get(), buffer);
	return retint;
}
static bool p_GetPVarNameAtIndex(int playerid, int index, MonoString ** varname, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetPVarNameAtIndex(playerid, index, buffer, size);
	*varname = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static bool p_GetAnimationName(int index, MonoString ** animlib, int animlib_size, MonoString ** animname, int animname_size) {
	char * libbuffer = new char[animlib_size];
	char * namebuffer = new char[animname_size];

	bool retbool = sampgdk_GetAnimationName(index, libbuffer, animlib_size, namebuffer, animname_size);
	*animlib = mono_string_new(mono_domain_get(), libbuffer);
	*animname = mono_string_new(mono_domain_get(), namebuffer);
	return retbool;
}
static bool p_GetWeaponName(int weaponid, MonoString ** name, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetWeaponName(weaponid, buffer, size);
	*name = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static bool p_GetPlayerNetworkStats(int playerid, MonoString ** retstr, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetPlayerNetworkStats(playerid, buffer, size);
	*retstr = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static bool p_GetPlayerVersion(int playerid, MonoString ** version, int len) {
	char * buffer = new char[len];
	bool retbool = sampgdk_GetPlayerVersion(playerid, buffer, len);
	*version = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
static bool p_gpci(int playerid, MonoString ** buffer, int size) {
	char * b_buffer = new char[size];
	bool retbool = sampgdk_gpci(playerid, b_buffer, size);
	*buffer = mono_string_new(mono_domain_get(), b_buffer);
	return retbool;
}
static bool p_GetNetworkStats(MonoString ** retstr, int size) {
	char * buffer = new char[size];
	bool retbool = sampgdk_GetNetworkStats(buffer, size);
	*retstr = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}

static int p_SetTimer(int interval, bool repeat, MonoObject * params)
{ 
	return SetTimer(interval, repeat, p_TimerCallback, params);
}

static void LoadNatives()
{

	mono_add_internal_call("GameMode.Server::SetSpawnInfo", sampgdk_SetSpawnInfo);
	mono_add_internal_call("GameMode.Server::SpawnPlayer", sampgdk_SpawnPlayer);
	mono_add_internal_call("GameMode.Server::SetPlayerPos", sampgdk_SetPlayerPos);
	mono_add_internal_call("GameMode.Server::SetPlayerPosFindZ", sampgdk_SetPlayerPosFindZ);
	mono_add_internal_call("GameMode.Server::GetPlayerPos", sampgdk_GetPlayerPos);
	mono_add_internal_call("GameMode.Server::SetPlayerFacingAngle", sampgdk_SetPlayerFacingAngle);
	mono_add_internal_call("GameMode.Server::GetPlayerFacingAngle", sampgdk_GetPlayerFacingAngle);
	mono_add_internal_call("GameMode.Server::IsPlayerInRangeOfPoint", sampgdk_IsPlayerInRangeOfPoint);
	mono_add_internal_call("GameMode.Server::GetPlayerDistanceFromPoint", sampgdk_GetPlayerDistanceFromPoint);
	mono_add_internal_call("GameMode.Server::IsPlayerStreamedIn", sampgdk_IsPlayerStreamedIn);
	mono_add_internal_call("GameMode.Server::SetPlayerInterior", sampgdk_SetPlayerInterior);
	mono_add_internal_call("GameMode.Server::GetPlayerInterior", sampgdk_GetPlayerInterior);
	mono_add_internal_call("GameMode.Server::SetPlayerHealth", sampgdk_SetPlayerHealth);
	mono_add_internal_call("GameMode.Server::GetPlayerHealth", sampgdk_GetPlayerHealth);
	mono_add_internal_call("GameMode.Server::SetPlayerArmour", sampgdk_SetPlayerArmour);
	mono_add_internal_call("GameMode.Server::GetPlayerArmour", sampgdk_GetPlayerArmour);
	mono_add_internal_call("GameMode.Server::SetPlayerAmmo", sampgdk_SetPlayerAmmo);
	mono_add_internal_call("GameMode.Server::GetPlayerAmmo", sampgdk_GetPlayerAmmo);
	mono_add_internal_call("GameMode.Server::GetPlayerWeaponState", sampgdk_GetPlayerWeaponState);
	mono_add_internal_call("GameMode.Server::GetPlayerTargetPlayer", sampgdk_GetPlayerTargetPlayer);
	mono_add_internal_call("GameMode.Server::SetPlayerTeam", sampgdk_SetPlayerTeam);
	mono_add_internal_call("GameMode.Server::GetPlayerTeam", sampgdk_GetPlayerTeam);
	mono_add_internal_call("GameMode.Server::SetPlayerScore", sampgdk_SetPlayerScore);
	mono_add_internal_call("GameMode.Server::GetPlayerScore", sampgdk_GetPlayerScore);
	mono_add_internal_call("GameMode.Server::GetPlayerDrunkLevel", sampgdk_GetPlayerDrunkLevel);
	mono_add_internal_call("GameMode.Server::SetPlayerDrunkLevel", sampgdk_SetPlayerDrunkLevel);
	mono_add_internal_call("GameMode.Server::SetPlayerColor", sampgdk_SetPlayerColor);
	mono_add_internal_call("GameMode.Server::GetPlayerColor", sampgdk_GetPlayerColor);
	mono_add_internal_call("GameMode.Server::SetPlayerSkin", sampgdk_SetPlayerSkin);
	mono_add_internal_call("GameMode.Server::GetPlayerSkin", sampgdk_GetPlayerSkin);
	mono_add_internal_call("GameMode.Server::GivePlayerWeapon", sampgdk_GivePlayerWeapon);
	mono_add_internal_call("GameMode.Server::ResetPlayerWeapons", sampgdk_ResetPlayerWeapons);
	mono_add_internal_call("GameMode.Server::SetPlayerArmedWeapon", sampgdk_SetPlayerArmedWeapon);
	mono_add_internal_call("GameMode.Server::GetPlayerWeaponData", sampgdk_GetPlayerWeaponData);
	mono_add_internal_call("GameMode.Server::GivePlayerMoney", sampgdk_GivePlayerMoney);
	mono_add_internal_call("GameMode.Server::ResetPlayerMoney", sampgdk_ResetPlayerMoney);
	mono_add_internal_call("GameMode.Server::SetPlayerName", p_SetPlayerName);
	mono_add_internal_call("GameMode.Server::GetPlayerMoney", sampgdk_GetPlayerMoney);
	mono_add_internal_call("GameMode.Server::GetPlayerState", sampgdk_GetPlayerState);
	mono_add_internal_call("GameMode.Server::GetPlayerIp", p_GetPlayerIp);
	mono_add_internal_call("GameMode.Server::GetPlayerPing", sampgdk_GetPlayerPing);
	mono_add_internal_call("GameMode.Server::GetPlayerWeapon", sampgdk_GetPlayerWeapon);
	mono_add_internal_call("GameMode.Server::GetPlayerKeys", sampgdk_GetPlayerKeys);
	mono_add_internal_call("GameMode.Server::GetPlayerName", sampgdk_GetPlayerName);
	mono_add_internal_call("GameMode.Server::SetPlayerTime", sampgdk_SetPlayerTime);
	mono_add_internal_call("GameMode.Server::GetPlayerTime", sampgdk_GetPlayerTime);
	mono_add_internal_call("GameMode.Server::TogglePlayerClock", sampgdk_TogglePlayerClock);
	mono_add_internal_call("GameMode.Server::SetPlayerWeather", sampgdk_SetPlayerWeather);
	mono_add_internal_call("GameMode.Server::ForceClassSelection", sampgdk_ForceClassSelection);
	mono_add_internal_call("GameMode.Server::SetPlayerWantedLevel", sampgdk_SetPlayerWantedLevel);
	mono_add_internal_call("GameMode.Server::GetPlayerWantedLevel", sampgdk_GetPlayerWantedLevel);
	mono_add_internal_call("GameMode.Server::SetPlayerFightingStyle", sampgdk_SetPlayerFightingStyle);
	mono_add_internal_call("GameMode.Server::GetPlayerFightingStyle", sampgdk_GetPlayerFightingStyle);
	mono_add_internal_call("GameMode.Server::SetPlayerVelocity", sampgdk_SetPlayerVelocity);
	mono_add_internal_call("GameMode.Server::GetPlayerVelocity", sampgdk_GetPlayerVelocity);
	mono_add_internal_call("GameMode.Server::PlayCrimeReportForPlayer", sampgdk_PlayCrimeReportForPlayer);
	mono_add_internal_call("GameMode.Server::PlayAudioStreamForPlayer", p_PlayAudioStreamForPlayer);
	mono_add_internal_call("GameMode.Server::StopAudioStreamForPlayer", sampgdk_StopAudioStreamForPlayer);
	mono_add_internal_call("GameMode.Server::SetPlayerShopName", p_SetPlayerShopName);
	mono_add_internal_call("GameMode.Server::SetPlayerSkillLevel", sampgdk_SetPlayerSkillLevel);
	mono_add_internal_call("GameMode.Server::GetPlayerSurfingVehicleID", sampgdk_GetPlayerSurfingVehicleID);
	mono_add_internal_call("GameMode.Server::GetPlayerSurfingObjectID", sampgdk_GetPlayerSurfingObjectID);
	mono_add_internal_call("GameMode.Server::RemoveBuildingForPlayer", sampgdk_RemoveBuildingForPlayer);
	mono_add_internal_call("GameMode.Server::SetPlayerAttachedObject", sampgdk_SetPlayerAttachedObject);
	mono_add_internal_call("GameMode.Server::RemovePlayerAttachedObject", sampgdk_RemovePlayerAttachedObject);
	mono_add_internal_call("GameMode.Server::IsPlayerAttachedObjectSlotUsed", sampgdk_IsPlayerAttachedObjectSlotUsed);
	mono_add_internal_call("GameMode.Server::EditAttachedObject", sampgdk_EditAttachedObject);
	mono_add_internal_call("GameMode.Server::CreatePlayerTextDraw", p_CreatePlayerTextDraw);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawDestroy", sampgdk_PlayerTextDrawDestroy);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawLetterSize", sampgdk_PlayerTextDrawLetterSize);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawTextSize", sampgdk_PlayerTextDrawTextSize);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawAlignment", sampgdk_PlayerTextDrawAlignment);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawColor", sampgdk_PlayerTextDrawColor);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawUseBox", sampgdk_PlayerTextDrawUseBox);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawBoxColor", sampgdk_PlayerTextDrawBoxColor);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawSetShadow", sampgdk_PlayerTextDrawSetShadow);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawSetOutline", sampgdk_PlayerTextDrawSetOutline);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawBackgroundColor", sampgdk_PlayerTextDrawBackgroundColor);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawFont", sampgdk_PlayerTextDrawFont);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawSetProportional", sampgdk_PlayerTextDrawSetProportional);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawSetSelectable", sampgdk_PlayerTextDrawSetSelectable);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawShow", sampgdk_PlayerTextDrawShow);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawHide", sampgdk_PlayerTextDrawHide);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawSetString", p_PlayerTextDrawSetString);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawSetPreviewModel", sampgdk_PlayerTextDrawSetPreviewModel);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawSetPreviewRot", sampgdk_PlayerTextDrawSetPreviewRot);
	mono_add_internal_call("GameMode.Server::PlayerTextDrawSetPreviewVehCol", sampgdk_PlayerTextDrawSetPreviewVehCol);
	mono_add_internal_call("GameMode.Server::SetPVarInt", p_SetPVarInt);
	mono_add_internal_call("GameMode.Server::GetPVarInt", p_GetPVarInt);
	mono_add_internal_call("GameMode.Server::SetPVarString", p_SetPVarString);
	mono_add_internal_call("GameMode.Server::GetPVarString", p_GetPVarString);
	mono_add_internal_call("GameMode.Server::SetPVarFloat", p_SetPVarFloat);
	mono_add_internal_call("GameMode.Server::GetPVarFloat", p_GetPVarFloat);
	mono_add_internal_call("GameMode.Server::DeletePVar", p_DeletePVar);
	mono_add_internal_call("GameMode.Server::GetPVarsUpperIndex", sampgdk_GetPVarsUpperIndex);
	mono_add_internal_call("GameMode.Server::GetPVarNameAtIndex", sampgdk_GetPVarNameAtIndex);
	mono_add_internal_call("GameMode.Server::GetPVarType", p_GetPVarType);
	mono_add_internal_call("GameMode.Server::SetPlayerChatBubble", p_SetPlayerChatBubble);
	mono_add_internal_call("GameMode.Server::PutPlayerInVehicle", sampgdk_PutPlayerInVehicle);
	mono_add_internal_call("GameMode.Server::GetPlayerVehicleID", sampgdk_GetPlayerVehicleID);
	mono_add_internal_call("GameMode.Server::GetPlayerVehicleSeat", sampgdk_GetPlayerVehicleSeat);
	mono_add_internal_call("GameMode.Server::RemovePlayerFromVehicle", sampgdk_RemovePlayerFromVehicle);
	mono_add_internal_call("GameMode.Server::TogglePlayerControllable", sampgdk_TogglePlayerControllable);
	mono_add_internal_call("GameMode.Server::PlayerPlaySound", sampgdk_PlayerPlaySound);
	mono_add_internal_call("GameMode.Server::ApplyAnimation", p_ApplyAnimation);
	mono_add_internal_call("GameMode.Server::ClearAnimations", sampgdk_ClearAnimations);
	mono_add_internal_call("GameMode.Server::GetPlayerAnimationIndex", sampgdk_GetPlayerAnimationIndex);
	mono_add_internal_call("GameMode.Server::GetAnimationName", sampgdk_GetAnimationName);
	mono_add_internal_call("GameMode.Server::GetPlayerSpecialAction", sampgdk_GetPlayerSpecialAction);
	mono_add_internal_call("GameMode.Server::SetPlayerSpecialAction", sampgdk_SetPlayerSpecialAction);
	mono_add_internal_call("GameMode.Server::SetPlayerCheckpoint", sampgdk_SetPlayerCheckpoint);
	mono_add_internal_call("GameMode.Server::DisablePlayerCheckpoint", sampgdk_DisablePlayerCheckpoint);
	mono_add_internal_call("GameMode.Server::SetPlayerRaceCheckpoint", sampgdk_SetPlayerRaceCheckpoint);
	mono_add_internal_call("GameMode.Server::DisablePlayerRaceCheckpoint", sampgdk_DisablePlayerRaceCheckpoint);
	mono_add_internal_call("GameMode.Server::SetPlayerWorldBounds", sampgdk_SetPlayerWorldBounds);
	mono_add_internal_call("GameMode.Server::SetPlayerMarkerForPlayer", sampgdk_SetPlayerMarkerForPlayer);
	mono_add_internal_call("GameMode.Server::ShowPlayerNameTagForPlayer", sampgdk_ShowPlayerNameTagForPlayer);
	mono_add_internal_call("GameMode.Server::SetPlayerMapIcon", sampgdk_SetPlayerMapIcon);
	mono_add_internal_call("GameMode.Server::RemovePlayerMapIcon", sampgdk_RemovePlayerMapIcon);
	mono_add_internal_call("GameMode.Server::AllowPlayerTeleport", sampgdk_AllowPlayerTeleport);
	mono_add_internal_call("GameMode.Server::SetPlayerCameraPos", sampgdk_SetPlayerCameraPos);
	mono_add_internal_call("GameMode.Server::SetPlayerCameraLookAt", sampgdk_SetPlayerCameraLookAt);
	mono_add_internal_call("GameMode.Server::SetCameraBehindPlayer", sampgdk_SetCameraBehindPlayer);
	mono_add_internal_call("GameMode.Server::GetPlayerCameraPos", sampgdk_GetPlayerCameraPos);
	mono_add_internal_call("GameMode.Server::GetPlayerCameraFrontVector", sampgdk_GetPlayerCameraFrontVector);
	mono_add_internal_call("GameMode.Server::GetPlayerCameraMode", sampgdk_GetPlayerCameraMode);
	mono_add_internal_call("GameMode.Server::AttachCameraToObject", sampgdk_AttachCameraToObject);
	mono_add_internal_call("GameMode.Server::AttachCameraToPlayerObject", sampgdk_AttachCameraToPlayerObject);
	mono_add_internal_call("GameMode.Server::InterpolateCameraPos", sampgdk_InterpolateCameraPos);
	mono_add_internal_call("GameMode.Server::InterpolateCameraLookAt", sampgdk_InterpolateCameraLookAt);
	mono_add_internal_call("GameMode.Server::IsPlayerConnected", sampgdk_IsPlayerConnected);
	mono_add_internal_call("GameMode.Server::IsPlayerInVehicle", sampgdk_IsPlayerInVehicle);
	mono_add_internal_call("GameMode.Server::IsPlayerInAnyVehicle", sampgdk_IsPlayerInAnyVehicle);
	mono_add_internal_call("GameMode.Server::IsPlayerInCheckpoint", sampgdk_IsPlayerInCheckpoint);
	mono_add_internal_call("GameMode.Server::IsPlayerInRaceCheckpoint", sampgdk_IsPlayerInRaceCheckpoint);
	mono_add_internal_call("GameMode.Server::SetPlayerVirtualWorld", sampgdk_SetPlayerVirtualWorld);
	mono_add_internal_call("GameMode.Server::GetPlayerVirtualWorld", sampgdk_GetPlayerVirtualWorld);
	mono_add_internal_call("GameMode.Server::EnableStuntBonusForPlayer", sampgdk_EnableStuntBonusForPlayer);
	mono_add_internal_call("GameMode.Server::EnableStuntBonusForAll", sampgdk_EnableStuntBonusForAll);
	mono_add_internal_call("GameMode.Server::TogglePlayerSpectating", sampgdk_TogglePlayerSpectating);
	mono_add_internal_call("GameMode.Server::PlayerSpectatePlayer", sampgdk_PlayerSpectatePlayer);
	mono_add_internal_call("GameMode.Server::PlayerSpectateVehicle", sampgdk_PlayerSpectateVehicle);
	mono_add_internal_call("GameMode.Server::StartRecordingPlayerData", p_StartRecordingPlayerData);
	mono_add_internal_call("GameMode.Server::StopRecordingPlayerData", sampgdk_StopRecordingPlayerData);
	mono_add_internal_call("GameMode.Server::SendClientMessage", p_SendClientMessage);
	mono_add_internal_call("GameMode.Server::SendClientMessageToAll", p_SendClientMessageToAll);
	mono_add_internal_call("GameMode.Server::SendPlayerMessageToPlayer", p_SendPlayerMessageToPlayer);
	mono_add_internal_call("GameMode.Server::SendPlayerMessageToAll", p_SendPlayerMessageToAll);
	mono_add_internal_call("GameMode.Server::SendDeathMessage", sampgdk_SendDeathMessage);
	mono_add_internal_call("GameMode.Server::GameTextForAll", p_GameTextForAll);
	mono_add_internal_call("GameMode.Server::GameTextForPlayer", p_GameTextForPlayer);
	mono_add_internal_call("GameMode.Server::GetTickCount", sampgdk_GetTickCount);
	mono_add_internal_call("GameMode.Server::GetMaxPlayers", sampgdk_GetMaxPlayers);
	mono_add_internal_call("GameMode.Server::SetGameModeText", p_SetGameModeText);
	mono_add_internal_call("GameMode.Server::SetTeamCount", sampgdk_SetTeamCount);
	mono_add_internal_call("GameMode.Server::AddPlayerClass", sampgdk_AddPlayerClass);
	mono_add_internal_call("GameMode.Server::AddPlayerClassEx", sampgdk_AddPlayerClassEx);
	mono_add_internal_call("GameMode.Server::AddStaticVehicle", sampgdk_AddStaticVehicle);
	mono_add_internal_call("GameMode.Server::AddStaticVehicleEx", sampgdk_AddStaticVehicleEx);
	mono_add_internal_call("GameMode.Server::AddStaticPickup", sampgdk_AddStaticPickup);
	mono_add_internal_call("GameMode.Server::CreatePickup", sampgdk_CreatePickup);
	mono_add_internal_call("GameMode.Server::DestroyPickup", sampgdk_DestroyPickup);
	mono_add_internal_call("GameMode.Server::ShowNameTags", sampgdk_ShowNameTags);
	mono_add_internal_call("GameMode.Server::ShowPlayerMarkers", sampgdk_ShowPlayerMarkers);
	mono_add_internal_call("GameMode.Server::GameModeExit", sampgdk_GameModeExit);
	mono_add_internal_call("GameMode.Server::SetWorldTime", sampgdk_SetWorldTime);
	mono_add_internal_call("GameMode.Server::GetWeaponName", sampgdk_GetWeaponName);
	mono_add_internal_call("GameMode.Server::EnableTirePopping", sampgdk_EnableTirePopping);
	mono_add_internal_call("GameMode.Server::EnableVehicleFriendlyFire", sampgdk_EnableVehicleFriendlyFire);
	mono_add_internal_call("GameMode.Server::AllowInteriorWeapons", sampgdk_AllowInteriorWeapons);
	mono_add_internal_call("GameMode.Server::SetWeather", sampgdk_SetWeather);
	mono_add_internal_call("GameMode.Server::SetGravity", sampgdk_SetGravity);
	mono_add_internal_call("GameMode.Server::AllowAdminTeleport", sampgdk_AllowAdminTeleport);
	mono_add_internal_call("GameMode.Server::SetDeathDropAmount", sampgdk_SetDeathDropAmount);
	mono_add_internal_call("GameMode.Server::CreateExplosion", sampgdk_CreateExplosion);
	mono_add_internal_call("GameMode.Server::EnableZoneNames", sampgdk_EnableZoneNames);
	mono_add_internal_call("GameMode.Server::UsePlayerPedAnims", sampgdk_UsePlayerPedAnims);
	mono_add_internal_call("GameMode.Server::DisableInteriorEnterExits", sampgdk_DisableInteriorEnterExits);
	mono_add_internal_call("GameMode.Server::SetNameTagDrawDistance", sampgdk_SetNameTagDrawDistance);
	mono_add_internal_call("GameMode.Server::DisableNameTagLOS", sampgdk_DisableNameTagLOS);
	mono_add_internal_call("GameMode.Server::LimitGlobalChatRadius", sampgdk_LimitGlobalChatRadius);
	mono_add_internal_call("GameMode.Server::LimitPlayerMarkerRadius", sampgdk_LimitPlayerMarkerRadius);
	mono_add_internal_call("GameMode.Server::ConnectNPC", p_ConnectNPC);
	mono_add_internal_call("GameMode.Server::IsPlayerNPC", sampgdk_IsPlayerNPC);
	mono_add_internal_call("GameMode.Server::IsPlayerAdmin", sampgdk_IsPlayerAdmin);
	mono_add_internal_call("GameMode.Server::Kick", sampgdk_Kick);
	mono_add_internal_call("GameMode.Server::Ban", sampgdk_Ban);
	mono_add_internal_call("GameMode.Server::BanEx", p_BanEx);
	mono_add_internal_call("GameMode.Server::SendRconCommand", p_SendRconCommand);
	mono_add_internal_call("GameMode.Server::GetServerVarAsString", p_GetServerVarAsString);
	mono_add_internal_call("GameMode.Server::GetServerVarAsInt", p_GetServerVarAsInt);
	mono_add_internal_call("GameMode.Server::GetServerVarAsBool", p_GetServerVarAsBool);
	mono_add_internal_call("GameMode.Server::GetPlayerNetworkStats", sampgdk_GetPlayerNetworkStats);
	mono_add_internal_call("GameMode.Server::GetNetworkStats", sampgdk_GetNetworkStats);
	mono_add_internal_call("GameMode.Server::GetPlayerVersion", sampgdk_GetPlayerVersion);
	mono_add_internal_call("GameMode.Server::CreateMenu", p_CreateMenu);
	mono_add_internal_call("GameMode.Server::DestroyMenu", sampgdk_DestroyMenu);
	mono_add_internal_call("GameMode.Server::AddMenuItem", p_AddMenuItem);
	mono_add_internal_call("GameMode.Server::SetMenuColumnHeader", p_SetMenuColumnHeader);
	mono_add_internal_call("GameMode.Server::ShowMenuForPlayer", sampgdk_ShowMenuForPlayer);
	mono_add_internal_call("GameMode.Server::HideMenuForPlayer", sampgdk_HideMenuForPlayer);
	mono_add_internal_call("GameMode.Server::IsValidMenu", sampgdk_IsValidMenu);
	mono_add_internal_call("GameMode.Server::DisableMenu", sampgdk_DisableMenu);
	mono_add_internal_call("GameMode.Server::DisableMenuRow", sampgdk_DisableMenuRow);
	mono_add_internal_call("GameMode.Server::GetPlayerMenu", sampgdk_GetPlayerMenu);
	mono_add_internal_call("GameMode.Server::TextDrawCreate", p_TextDrawCreate);
	mono_add_internal_call("GameMode.Server::TextDrawDestroy", sampgdk_TextDrawDestroy);
	mono_add_internal_call("GameMode.Server::TextDrawLetterSize", sampgdk_TextDrawLetterSize);
	mono_add_internal_call("GameMode.Server::TextDrawTextSize", sampgdk_TextDrawTextSize);
	mono_add_internal_call("GameMode.Server::TextDrawAlignment", sampgdk_TextDrawAlignment);
	mono_add_internal_call("GameMode.Server::TextDrawColor", sampgdk_TextDrawColor);
	mono_add_internal_call("GameMode.Server::TextDrawUseBox", sampgdk_TextDrawUseBox);
	mono_add_internal_call("GameMode.Server::TextDrawBoxColor", sampgdk_TextDrawBoxColor);
	mono_add_internal_call("GameMode.Server::TextDrawSetShadow", sampgdk_TextDrawSetShadow);
	mono_add_internal_call("GameMode.Server::TextDrawSetOutline", sampgdk_TextDrawSetOutline);
	mono_add_internal_call("GameMode.Server::TextDrawBackgroundColor", sampgdk_TextDrawBackgroundColor);
	mono_add_internal_call("GameMode.Server::TextDrawFont", sampgdk_TextDrawFont);
	mono_add_internal_call("GameMode.Server::TextDrawSetProportional", sampgdk_TextDrawSetProportional);
	mono_add_internal_call("GameMode.Server::TextDrawSetSelectable", sampgdk_TextDrawSetSelectable);
	mono_add_internal_call("GameMode.Server::TextDrawShowForPlayer", sampgdk_TextDrawShowForPlayer);
	mono_add_internal_call("GameMode.Server::TextDrawHideForPlayer", sampgdk_TextDrawHideForPlayer);
	mono_add_internal_call("GameMode.Server::TextDrawShowForAll", sampgdk_TextDrawShowForAll);
	mono_add_internal_call("GameMode.Server::TextDrawHideForAll", sampgdk_TextDrawHideForAll);
	mono_add_internal_call("GameMode.Server::TextDrawSetString", p_TextDrawSetString);
	mono_add_internal_call("GameMode.Server::TextDrawSetPreviewModel", sampgdk_TextDrawSetPreviewModel);
	mono_add_internal_call("GameMode.Server::TextDrawSetPreviewRot", sampgdk_TextDrawSetPreviewRot);
	mono_add_internal_call("GameMode.Server::TextDrawSetPreviewVehCol", sampgdk_TextDrawSetPreviewVehCol);
	mono_add_internal_call("GameMode.Server::SelectTextDraw", sampgdk_SelectTextDraw);
	mono_add_internal_call("GameMode.Server::CancelSelectTextDraw", sampgdk_CancelSelectTextDraw);
	mono_add_internal_call("GameMode.Server::GangZoneCreate", sampgdk_GangZoneCreate);
	mono_add_internal_call("GameMode.Server::GangZoneDestroy", sampgdk_GangZoneDestroy);
	mono_add_internal_call("GameMode.Server::GangZoneShowForPlayer", sampgdk_GangZoneShowForPlayer);
	mono_add_internal_call("GameMode.Server::GangZoneShowForAll", sampgdk_GangZoneShowForAll);
	mono_add_internal_call("GameMode.Server::GangZoneHideForPlayer", sampgdk_GangZoneHideForPlayer);
	mono_add_internal_call("GameMode.Server::GangZoneHideForAll", sampgdk_GangZoneHideForAll);
	mono_add_internal_call("GameMode.Server::GangZoneFlashForPlayer", sampgdk_GangZoneFlashForPlayer);
	mono_add_internal_call("GameMode.Server::GangZoneFlashForAll", sampgdk_GangZoneFlashForAll);
	mono_add_internal_call("GameMode.Server::GangZoneStopFlashForPlayer", sampgdk_GangZoneStopFlashForPlayer);
	mono_add_internal_call("GameMode.Server::GangZoneStopFlashForAll", sampgdk_GangZoneStopFlashForAll);
	mono_add_internal_call("GameMode.Server::Create3DTextLabel", p_Create3DTextLabel);
	mono_add_internal_call("GameMode.Server::Delete3DTextLabel", sampgdk_Delete3DTextLabel);
	mono_add_internal_call("GameMode.Server::Attach3DTextLabelToPlayer", sampgdk_Attach3DTextLabelToPlayer);
	mono_add_internal_call("GameMode.Server::Attach3DTextLabelToVehicle", sampgdk_Attach3DTextLabelToVehicle);
	mono_add_internal_call("GameMode.Server::Update3DTextLabelText", p_Update3DTextLabelText);
	mono_add_internal_call("GameMode.Server::CreatePlayer3DTextLabel", p_CreatePlayer3DTextLabel);
	mono_add_internal_call("GameMode.Server::DeletePlayer3DTextLabel", sampgdk_DeletePlayer3DTextLabel);
	mono_add_internal_call("GameMode.Server::UpdatePlayer3DTextLabelText", p_UpdatePlayer3DTextLabelText);
	mono_add_internal_call("GameMode.Server::ShowPlayerDialog", p_ShowPlayerDialog);
	mono_add_internal_call("GameMode.Server::SetTimer", p_SetTimer);
	mono_add_internal_call("GameMode.Server::KillTimer", sampgdk_KillTimer);
	mono_add_internal_call("GameMode.Server::gpci", sampgdk_gpci);

}