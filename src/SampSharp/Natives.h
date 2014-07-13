#pragma once

#include <sampgdk/core.h>
#include <sampgdk/a_players.h>
#include <sampgdk/a_samp.h>
#include <sampgdk/a_objects.h>
#include <sampgdk/a_vehicles.h>

#define MAX_NATIVE_ARGS 32

using std::string;

string mono_string_to_string(MonoString *str)
{
	//TODO: seems a little sloppy, should research better solutions.
	mono_unichar2 *chl = mono_string_chars(str);
	string out("");
	for (int i = 0; i < mono_string_length(str); i++) {
		out += (char)chl[i];
	}
	return out;
}

//
//a_players string converters
inline int p_SetPlayerName(int playerid, MonoString *name) {
	return sampgdk_SetPlayerName(playerid, mono_string_to_string(name).c_str());
}
inline bool p_PlayAudioStreamForPlayer(int playerid, MonoString *url, float posX, float posY, float posZ, float distance, bool usepos) {
	return sampgdk_PlayAudioStreamForPlayer(playerid, mono_string_to_string(url).c_str(), posX, posY, posZ, distance, usepos);
}
inline bool p_SetPlayerShopName(int playerid, MonoString *shopname) {
	return sampgdk_SetPlayerShopName(playerid, mono_string_to_string(shopname).c_str());
}
inline int p_CreatePlayerTextDraw(int playerid, float x, float y, MonoString *text) {
	return sampgdk_CreatePlayerTextDraw(playerid, x, y, mono_string_to_string(text).c_str());
}
inline bool p_PlayerTextDrawSetString(int playerid, int text, MonoString *string) {
	return sampgdk_PlayerTextDrawSetString(playerid, text, mono_string_to_string(string).c_str());
}
inline bool p_SetPVarInt(int playerid, MonoString *varname, int value) {
	return sampgdk_SetPVarInt(playerid, mono_string_to_string(varname).c_str(), value);
}
inline int p_GetPVarInt(int playerid, MonoString *varname) {
	return sampgdk_GetPVarInt(playerid, mono_string_to_string(varname).c_str());
}
inline bool p_SetPVarString(int playerid, MonoString *varname, MonoString *value) {
	return sampgdk_SetPVarString(playerid, mono_string_to_string(varname).c_str(), mono_string_to_string(value).c_str());
}
inline bool p_GetPVarString(int playerid, MonoString *varname, MonoString ** value, int size) {
	char *buffer = new char[size];
	bool retbool = sampgdk_GetPVarString(playerid, mono_string_to_string(varname).c_str(), buffer, size);
	*value = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
inline bool p_SetPVarFloat(int playerid, MonoString *varname, float value) {
	return sampgdk_SetPVarFloat(playerid, mono_string_to_string(varname).c_str(), value);
}
inline float p_GetPVarFloat(int playerid, MonoString *varname) {
	return sampgdk_GetPVarFloat(playerid, mono_string_to_string(varname).c_str());
}
inline bool p_DeletePVar(int playerid, MonoString *varname) {
	return sampgdk_DeletePVar(playerid, mono_string_to_string(varname).c_str());
}
inline int p_GetPVarType(int playerid, MonoString *varname) {
	return sampgdk_GetPVarType(playerid, mono_string_to_string(varname).c_str());
}
inline bool p_SetPlayerChatBubble(int playerid, MonoString *text, int color, float drawdistance, int expiretime) {
	return sampgdk_SetPlayerChatBubble(playerid, mono_string_to_string(text).c_str(), color, drawdistance, expiretime);
}
inline bool p_ApplyAnimation(int playerid, MonoString *animlib, MonoString *animname, float fDelta, bool loop, bool lockx, bool locky, bool freeze, int time, bool forcesync) {
	return sampgdk_ApplyAnimation(playerid, mono_string_to_string(animlib).c_str(), mono_string_to_string(animname).c_str(), fDelta, loop, lockx, locky, freeze, time, forcesync);
}
inline bool p_StartRecordingPlayerData(int playerid, int recordtype, MonoString *recordname) {
	return sampgdk_StartRecordingPlayerData(playerid, recordtype, mono_string_to_string(recordname).c_str());
}
inline bool p_GetPlayerIp(int playerid, MonoString ** ip, int size) {
	char *buffer = new char[size];
	bool retbool = sampgdk_GetPlayerIp(playerid, buffer, size);
	*ip = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
inline int p_GetPlayerName(int playerid, MonoString ** name, int size) {
	char *buffer = new char[size];
	int retint = sampgdk_GetPlayerName(playerid, buffer, size);
	*name = mono_string_new(mono_domain_get(), buffer);
	return retint;
}
inline bool p_GetPVarNameAtIndex(int playerid, int index, MonoString ** varname, int size) {
	char *buffer = new char[size];
	bool retbool = sampgdk_GetPVarNameAtIndex(playerid, index, buffer, size);
	*varname = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
inline bool p_GetAnimationName(int index, MonoString ** animlib, int animlib_size, MonoString ** animname, int animname_size) {
	char *libbuffer = new char[animlib_size];
	char *namebuffer = new char[animname_size];

	bool retbool = sampgdk_GetAnimationName(index, libbuffer, animlib_size, namebuffer, animname_size);
	*animlib = mono_string_new(mono_domain_get(), libbuffer);
	*animname = mono_string_new(mono_domain_get(), namebuffer);
	return retbool;
}

//
//a_samp string converters
inline bool p_SendClientMessage(int playerid, int color, MonoString *message) {
	return sampgdk_SendClientMessage(playerid, color, mono_string_to_string(message).c_str());
}
inline bool p_SendClientMessageToAll(int color, MonoString *message) {
	return sampgdk_SendClientMessageToAll(color, mono_string_to_string(message).c_str());
}
inline bool p_SendPlayerMessageToPlayer(int playerid, int senderid, MonoString *message) {
	return sampgdk_SendPlayerMessageToPlayer(playerid, senderid, mono_string_to_string(message).c_str());
}
inline bool p_SendPlayerMessageToAll(int senderid, MonoString *message) {
	return sampgdk_SendPlayerMessageToAll(senderid, mono_string_to_string(message).c_str());
}
inline bool p_GameTextForAll(MonoString *text, int time, int style) {
	return sampgdk_GameTextForAll(mono_string_to_string(text).c_str(), time, style);
}
inline bool p_GameTextForPlayer(int playerid, MonoString *text, int time, int style) {
	return sampgdk_GameTextForPlayer(playerid, mono_string_to_string(text).c_str(), time, style);
}
inline bool p_SetGameModeText(MonoString *text) {
	return sampgdk_SetGameModeText(mono_string_to_string(text).c_str());
}
inline bool p_ConnectNPC(MonoString *name, MonoString *script) {
	return sampgdk_ConnectNPC(mono_string_to_string(name).c_str(), mono_string_to_string(script).c_str());
}
inline bool p_BanEx(int playerid, MonoString *reason) {
	return sampgdk_BanEx(playerid, mono_string_to_string(reason).c_str());
}
inline bool p_SendRconCommand(MonoString *command) {
	return sampgdk_SendRconCommand(mono_string_to_string(command).c_str());
}
inline bool p_GetServerVarAsString(MonoString *varname, MonoString ** value, int size) {
	char *buffer = new char[size];
	bool retbool = sampgdk_GetServerVarAsString(mono_string_to_string(varname).c_str(), buffer, size);
	*value = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
inline int p_GetServerVarAsInt(MonoString *varname) {
	return sampgdk_GetServerVarAsInt(mono_string_to_string(varname).c_str());
}
inline bool p_GetServerVarAsBool(MonoString *varname) {
	return sampgdk_GetServerVarAsBool(mono_string_to_string(varname).c_str());
}
inline int p_CreateMenu(MonoString *title, int columns, float x, float y, float col1width, float col2width) {
	return sampgdk_CreateMenu(mono_string_to_string(title).c_str(), columns, x, y, col1width, col2width);
}
inline int p_AddMenuItem(int menuid, int column, MonoString *menutext) {
	return sampgdk_AddMenuItem(menuid, column, mono_string_to_string(menutext).c_str());
}
inline bool p_SetMenuColumnHeader(int menuid, int column, MonoString *columnheader) {
	return sampgdk_SetMenuColumnHeader(menuid, column, mono_string_to_string(columnheader).c_str());
}
inline int p_TextDrawCreate(float x, float y, MonoString *text) {
	return sampgdk_TextDrawCreate(x, y, mono_string_to_string(text).c_str());
}
inline bool p_TextDrawSetString(int text, MonoString *string) {
	return sampgdk_TextDrawSetString(text, mono_string_to_string(string).c_str());
}
inline int p_Create3DTextLabel(MonoString *text, int color, float x, float y, float z, float DrawDistance, int virtualworld, bool testLOS) {
	return sampgdk_Create3DTextLabel(mono_string_to_string(text).c_str(), color, x, y, z, DrawDistance, virtualworld, testLOS);
}
inline bool p_Update3DTextLabelText(int id, int color, MonoString *text) {
	return sampgdk_Update3DTextLabelText(id, color, mono_string_to_string(text).c_str());
}
inline int p_CreatePlayer3DTextLabel(int playerid, MonoString *text, int color, float x, float y, float z, float DrawDistance, int attachedplayer, int attachedvehicle, bool testLOS) {
	return sampgdk_CreatePlayer3DTextLabel(playerid, mono_string_to_string(text).c_str(), color, x, y, z, DrawDistance, attachedplayer, attachedvehicle, testLOS);
}
inline bool p_UpdatePlayer3DTextLabelText(int playerid, int id, int color, MonoString *text) {
	return sampgdk_UpdatePlayer3DTextLabelText(playerid, id, color, mono_string_to_string(text).c_str());
}
inline bool p_ShowPlayerDialog(int playerid, int dialogid, int style, MonoString *caption, MonoString *info, MonoString *button1, MonoString *button2) {
	return sampgdk_ShowPlayerDialog(playerid, dialogid, style, mono_string_to_string(caption).c_str(), mono_string_to_string(info).c_str(), mono_string_to_string(button1).c_str(), mono_string_to_string(button2).c_str());
}
inline bool p_GetWeaponName(int weaponid, MonoString ** name, int size) {
	char *buffer = new char[size];
	bool retbool = sampgdk_GetWeaponName(weaponid, buffer, size);
	*name = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
inline bool p_GetPlayerNetworkStats(int playerid, MonoString ** retstr, int size) {
	char *buffer = new char[size];
	bool retbool = sampgdk_GetPlayerNetworkStats(playerid, buffer, size);
	*retstr = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
inline bool p_GetPlayerVersion(int playerid, MonoString ** version, int len) {
	char *buffer = new char[len];
	bool retbool = sampgdk_GetPlayerVersion(playerid, buffer, len);
	*version = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}
inline bool p_gpci(int playerid, MonoString ** buffer, int size) {
	char *b_buffer = new char[size];
	bool retbool = sampgdk_gpci(playerid, b_buffer, size);
	*buffer = mono_string_new(mono_domain_get(), b_buffer);
	return retbool;
}
inline bool p_GetNetworkStats(MonoString ** retstr, int size) {
	char *buffer = new char[size];
	bool retbool = sampgdk_GetNetworkStats(buffer, size);
	*retstr = mono_string_new(mono_domain_get(), buffer);
	return retbool;
}



inline int p_SetTimer(int interval, bool repeat, MonoObject *params) {
	return SetTimer(interval, repeat, SampSharp::ProcessTimerTick, params);
}
inline bool p_BlockIpAddress(MonoString *ip_address, int timems) {
	return sampgdk_BlockIpAddress(mono_string_to_string(ip_address).c_str(), timems);
}
inline bool p_UnBlockIpAddress(MonoString *ip_address) {
	return sampgdk_UnBlockIpAddress(mono_string_to_string(ip_address).c_str());
}

//
// a_objects string converters
inline bool p_SetObjectMaterial(int objectid, int materialindex, int modelid, MonoString *txdname, MonoString *texturename, int materialcolor) {
	return sampgdk_SetObjectMaterial(objectid, materialindex, modelid, mono_string_to_string(txdname).c_str(), mono_string_to_string(texturename).c_str(), materialcolor);
}
inline bool p_SetPlayerObjectMaterial(int playerid, int objectid, int materialindex, int modelid, MonoString *txdname, MonoString *texturename, int materialcolor) {
	return sampgdk_SetPlayerObjectMaterial(playerid, objectid, materialindex, modelid, mono_string_to_string(txdname).c_str(), mono_string_to_string(texturename).c_str(), materialcolor);
}
inline bool p_SetObjectMaterialText(int objectid, MonoString *text, int materialindex, int materialsize, MonoString *fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment) {
	return sampgdk_SetObjectMaterialText(objectid, mono_string_to_string(text).c_str(), materialindex, materialsize, mono_string_to_string(fontface).c_str(), fontsize, bold, fontcolor, backcolor, textalignment);
}
inline bool p_SetPlayerObjectMaterialText(int playerid, int objectid, MonoString *text, int materialindex, int materialsize, MonoString *fontface, int fontsize, bool bold, int fontcolor, int backcolor, int textalignment) {
	return sampgdk_SetPlayerObjectMaterialText(playerid, objectid, mono_string_to_string(text).c_str(), materialindex, materialsize, mono_string_to_string(fontface).c_str(), fontsize, bold, fontcolor, backcolor, textalignment);
}

//
// a_vehicles string converters
inline bool p_SetVehicleNumberPlate(int vehicleid, MonoString *numberplate) {
	return sampgdk_SetVehicleNumberPlate(vehicleid, mono_string_to_string(numberplate).c_str());
}

//
// serverlog string converters 
inline void p_Print(MonoString *str) {
	sampgdk_logprintf("%s", mono_string_to_string(str).c_str());
}

//
// native functions
cell call_native_array(MonoString *name, MonoString *format, MonoArray *args) {
    assert(name != NULL);
    assert(format != NULL);
    assert(args != NULL);


    char *native_str = mono_string_to_utf8(name);
    char *format_str = mono_string_to_utf8(format);
    int len = mono_array_length(args);

    void *params[MAX_NATIVE_ARGS];
    string amx_format;

	if(strlen(format_str) != len)
	{
		mono_raise_exception(mono_get_exception_invalid_operation(
            "invalid format length"));
		return -1;
	}

    if(len > MAX_NATIVE_ARGS)
    {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "too many arguments"));
		return -1;
    }

    for (int i = 0; i < len; i++) {
        switch (format_str[i]) {
        case 'i': /* integer */
        case 'd': /* integer */
        case 'b': /* boolean */ {
            params[i] = mono_object_unbox(mono_array_get(args, MonoObject *, i));
			amx_format += format_str[i];
            break;
        }
        
        case 'f': /* floating-point */ {
            params[i] = &amx_ftoc(*(float *)mono_object_unbox(
                mono_array_get(args, MonoObject *, i)));
			amx_format += 'f';
            break;
        }
        case 'F': /* floating-point reference */ {
            params[i] = &amx_ftoc(**(float **)mono_object_unbox(
                mono_array_get(args, MonoObject *, i)));
			amx_format += 'R';
            break;
        }
        case 's': /* const string */ {
            MonoString *str = mono_array_get(args, MonoString *, i);
            //char *value = mono_string_to_utf8(str);
            string std_str = mono_string_to_string(str).c_str();

            char *value = new char[std_str.length() + 1];
            strcpy(value, std_str.c_str());

			//value_ref[i] = *value;
            params[i] = value;
			amx_format += 's';
            break;
        }
        case 'S': /* non-const string (writeable) */ {
            /*
            TODO:
            Dynamic memory allocation seems to slow down the process
            by a considerable amount. Should research how to do this properly.
            */
            char *value[1024];// = (char *)malloc(sizeof(char) * (len + 1));
         
            params[i] = value;
			char *length_param = new char[7];
			sprintf(length_param, "S[*%d]", i+1);
			amx_format += length_param;
            break;
        }
        default:
			mono_raise_exception(mono_get_exception_invalid_operation(
                "invalid format type"));
			return -1;
        }
    }

    AMX_NATIVE native = sampgdk::FindNative(native_str);

    if(native == NULL) {
        mono_raise_exception(mono_get_exception_invalid_operation(
                "native not found"));
			return -1;
    }

    int retval = sampgdk::InvokeNativeArray(native, amx_format.c_str(), params);

    for (int i = 0; i < len; i++) {
        switch (format_str[i]) {
        case 'S': {
            *mono_array_get(args, MonoString **, i) = mono_string_new(mono_domain_get(), (char *)params[i]);
            break;
        }
        case 'F': {
			**(float **)mono_object_unbox(mono_array_get(args, MonoObject *, i)) = amx_ctof(*(cell *)params[i]);
            break;
        }
        }
    }

    return retval;
}

float call_native_array_float(MonoString *name, MonoString *format, MonoArray *args) {
	cell value = call_native_array(name, format, args);

	return value == -1 ? 0 : amx_ctof(value);
}
void LoadNatives()
{
	//
	// a_players natives
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetSpawnInfo", (void *)sampgdk_SetSpawnInfo);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SpawnPlayer", (void *)sampgdk_SpawnPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerPos", (void *)sampgdk_SetPlayerPos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerPosFindZ", (void *)sampgdk_SetPlayerPosFindZ);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerPos", (void *)sampgdk_GetPlayerPos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerFacingAngle", (void *)sampgdk_SetPlayerFacingAngle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerFacingAngle", (void *)sampgdk_GetPlayerFacingAngle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerInRangeOfPoint", (void *)sampgdk_IsPlayerInRangeOfPoint);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerDistanceFromPoint", (void *)sampgdk_GetPlayerDistanceFromPoint);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerStreamedIn", (void *)sampgdk_IsPlayerStreamedIn);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerInterior", (void *)sampgdk_SetPlayerInterior);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerInterior", (void *)sampgdk_GetPlayerInterior);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerHealth", (void *)sampgdk_SetPlayerHealth);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerHealth", (void *)sampgdk_GetPlayerHealth);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerArmour", (void *)sampgdk_SetPlayerArmour);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerArmour", (void *)sampgdk_GetPlayerArmour);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerAmmo", (void *)sampgdk_SetPlayerAmmo);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerAmmo", (void *)sampgdk_GetPlayerAmmo);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerWeaponState", (void *)sampgdk_GetPlayerWeaponState);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerTargetPlayer", (void *)sampgdk_GetPlayerTargetPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerTeam", (void *)sampgdk_SetPlayerTeam);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerTeam", (void *)sampgdk_GetPlayerTeam);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerScore", (void *)sampgdk_SetPlayerScore);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerScore", (void *)sampgdk_GetPlayerScore);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerDrunkLevel", (void *)sampgdk_GetPlayerDrunkLevel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerDrunkLevel", (void *)sampgdk_SetPlayerDrunkLevel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerColor", (void *)sampgdk_SetPlayerColor);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerColor", (void *)sampgdk_GetPlayerColor);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerSkin", (void *)sampgdk_SetPlayerSkin);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerSkin", (void *)sampgdk_GetPlayerSkin);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GivePlayerWeapon", (void *)sampgdk_GivePlayerWeapon);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ResetPlayerWeapons", (void *)sampgdk_ResetPlayerWeapons);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerArmedWeapon", (void *)sampgdk_SetPlayerArmedWeapon);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerWeaponData", (void *)sampgdk_GetPlayerWeaponData);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GivePlayerMoney", (void *)sampgdk_GivePlayerMoney);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ResetPlayerMoney", (void *)sampgdk_ResetPlayerMoney);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerName", (void *)p_SetPlayerName);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerMoney", (void *)sampgdk_GetPlayerMoney);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerState", (void *)sampgdk_GetPlayerState);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerIp", (void *)p_GetPlayerIp);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerPing", (void *)sampgdk_GetPlayerPing);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerWeapon", (void *)sampgdk_GetPlayerWeapon);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerKeys", (void *)sampgdk_GetPlayerKeys);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerName", (void *)p_GetPlayerName);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerTime", (void *)sampgdk_SetPlayerTime);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerTime", (void *)sampgdk_GetPlayerTime);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TogglePlayerClock", (void *)sampgdk_TogglePlayerClock);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerWeather", (void *)sampgdk_SetPlayerWeather);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ForceClassSelection", (void *)sampgdk_ForceClassSelection);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerWantedLevel", (void *)sampgdk_SetPlayerWantedLevel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerWantedLevel", (void *)sampgdk_GetPlayerWantedLevel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerFightingStyle", (void *)sampgdk_SetPlayerFightingStyle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerFightingStyle", (void *)sampgdk_GetPlayerFightingStyle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerVelocity", (void *)sampgdk_SetPlayerVelocity);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerVelocity", (void *)sampgdk_GetPlayerVelocity);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayCrimeReportForPlayer", (void *)sampgdk_PlayCrimeReportForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayAudioStreamForPlayer", (void *)p_PlayAudioStreamForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::StopAudioStreamForPlayer", (void *)sampgdk_StopAudioStreamForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerShopName", (void *)p_SetPlayerShopName);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerSkillLevel", (void *)sampgdk_SetPlayerSkillLevel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerSurfingVehicleID", (void *)sampgdk_GetPlayerSurfingVehicleID);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerSurfingObjectID", (void *)sampgdk_GetPlayerSurfingObjectID);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::RemoveBuildingForPlayer", (void *)sampgdk_RemoveBuildingForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerAttachedObject", (void *)sampgdk_SetPlayerAttachedObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::RemovePlayerAttachedObject", (void *)sampgdk_RemovePlayerAttachedObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerAttachedObjectSlotUsed", (void *)sampgdk_IsPlayerAttachedObjectSlotUsed);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::EditAttachedObject", (void *)sampgdk_EditAttachedObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CreatePlayerTextDraw", (void *)p_CreatePlayerTextDraw);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawDestroy", (void *)sampgdk_PlayerTextDrawDestroy);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawLetterSize", (void *)sampgdk_PlayerTextDrawLetterSize);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawTextSize", (void *)sampgdk_PlayerTextDrawTextSize);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawAlignment", (void *)sampgdk_PlayerTextDrawAlignment);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawColor", (void *)sampgdk_PlayerTextDrawColor);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawUseBox", (void *)sampgdk_PlayerTextDrawUseBox);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawBoxColor", (void *)sampgdk_PlayerTextDrawBoxColor);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawSetShadow", (void *)sampgdk_PlayerTextDrawSetShadow);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawSetOutline", (void *)sampgdk_PlayerTextDrawSetOutline);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawBackgroundColor", (void *)sampgdk_PlayerTextDrawBackgroundColor);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawFont", (void *)sampgdk_PlayerTextDrawFont);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawSetProportional", (void *)sampgdk_PlayerTextDrawSetProportional);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawSetSelectable", (void *)sampgdk_PlayerTextDrawSetSelectable);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawShow", (void *)sampgdk_PlayerTextDrawShow);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawHide", (void *)sampgdk_PlayerTextDrawHide);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawSetString", (void *)p_PlayerTextDrawSetString);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawSetPreviewModel", (void *)sampgdk_PlayerTextDrawSetPreviewModel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawSetPreviewRot", (void *)sampgdk_PlayerTextDrawSetPreviewRot);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerTextDrawSetPreviewVehCol", (void *)sampgdk_PlayerTextDrawSetPreviewVehCol);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPVarInt", (void *)p_SetPVarInt);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPVarInt", (void *)p_GetPVarInt);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPVarString", (void *)p_SetPVarString);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPVarString", (void *)p_GetPVarString);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPVarFloat", (void *)p_SetPVarFloat);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPVarFloat", (void *)p_GetPVarFloat);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DeletePVar", (void *)p_DeletePVar);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPVarsUpperIndex", (void *)sampgdk_GetPVarsUpperIndex);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPVarNameAtIndex", (void *)p_GetPVarNameAtIndex);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPVarType", (void *)p_GetPVarType);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerChatBubble", (void *)p_SetPlayerChatBubble);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PutPlayerInVehicle", (void *)sampgdk_PutPlayerInVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerVehicleID", (void *)sampgdk_GetPlayerVehicleID);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerVehicleSeat", (void *)sampgdk_GetPlayerVehicleSeat);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::RemovePlayerFromVehicle", (void *)sampgdk_RemovePlayerFromVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TogglePlayerControllable", (void *)sampgdk_TogglePlayerControllable);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerPlaySound", (void *)sampgdk_PlayerPlaySound);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ApplyAnimation", (void *)p_ApplyAnimation);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ClearAnimations", (void *)sampgdk_ClearAnimations);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerAnimationIndex", (void *)sampgdk_GetPlayerAnimationIndex);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetAnimationName", (void *)p_GetAnimationName);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerSpecialAction", (void *)sampgdk_GetPlayerSpecialAction);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerSpecialAction", (void *)sampgdk_SetPlayerSpecialAction);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerCheckpoint", (void *)sampgdk_SetPlayerCheckpoint);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DisablePlayerCheckpoint", (void *)sampgdk_DisablePlayerCheckpoint);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerRaceCheckpoint", (void *)sampgdk_SetPlayerRaceCheckpoint);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DisablePlayerRaceCheckpoint", (void *)sampgdk_DisablePlayerRaceCheckpoint);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerWorldBounds", (void *)sampgdk_SetPlayerWorldBounds);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerMarkerForPlayer", (void *)sampgdk_SetPlayerMarkerForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ShowPlayerNameTagForPlayer", (void *)sampgdk_ShowPlayerNameTagForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerMapIcon", (void *)sampgdk_SetPlayerMapIcon);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::RemovePlayerMapIcon", (void *)sampgdk_RemovePlayerMapIcon);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AllowPlayerTeleport", (void *)sampgdk_AllowPlayerTeleport);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerCameraPos", (void *)sampgdk_SetPlayerCameraPos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerCameraLookAt", (void *)sampgdk_SetPlayerCameraLookAt);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetCameraBehindPlayer", (void *)sampgdk_SetCameraBehindPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerCameraPos", (void *)sampgdk_GetPlayerCameraPos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerCameraFrontVector", (void *)sampgdk_GetPlayerCameraFrontVector);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerCameraMode", (void *)sampgdk_GetPlayerCameraMode);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AttachCameraToObject", (void *)sampgdk_AttachCameraToObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AttachCameraToPlayerObject", (void *)sampgdk_AttachCameraToPlayerObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::InterpolateCameraPos", (void *)sampgdk_InterpolateCameraPos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::InterpolateCameraLookAt", (void *)sampgdk_InterpolateCameraLookAt);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerConnected", (void *)sampgdk_IsPlayerConnected);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerInVehicle", (void *)sampgdk_IsPlayerInVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerInAnyVehicle", (void *)sampgdk_IsPlayerInAnyVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerInCheckpoint", (void *)sampgdk_IsPlayerInCheckpoint);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerInRaceCheckpoint", (void *)sampgdk_IsPlayerInRaceCheckpoint);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerVirtualWorld", (void *)sampgdk_SetPlayerVirtualWorld);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerVirtualWorld", (void *)sampgdk_GetPlayerVirtualWorld);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::EnableStuntBonusForPlayer", (void *)sampgdk_EnableStuntBonusForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::EnableStuntBonusForAll", (void *)sampgdk_EnableStuntBonusForAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TogglePlayerSpectating", (void *)sampgdk_TogglePlayerSpectating);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerSpectatePlayer", (void *)sampgdk_PlayerSpectatePlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::PlayerSpectateVehicle", (void *)sampgdk_PlayerSpectateVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::StartRecordingPlayerData", (void *)p_StartRecordingPlayerData);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::StopRecordingPlayerData", (void *)sampgdk_StopRecordingPlayerData);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CreateExplosionForPlayer", (void *)sampgdk_CreateExplosionForPlayer);

	//
	// a_samp natives
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SendClientMessage", (void *)p_SendClientMessage);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SendClientMessageToAll", (void *)p_SendClientMessageToAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SendPlayerMessageToPlayer", (void *)p_SendPlayerMessageToPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SendPlayerMessageToAll", (void *)p_SendPlayerMessageToAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SendDeathMessage", (void *)sampgdk_SendDeathMessage);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GameTextForAll", (void *)p_GameTextForAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GameTextForPlayer", (void *)p_GameTextForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetTickCount", (void *)sampgdk_GetTickCount);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetMaxPlayers", (void *)sampgdk_GetMaxPlayers);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetGameModeText", (void *)p_SetGameModeText);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetTeamCount", (void *)sampgdk_SetTeamCount);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AddPlayerClass", (void *)sampgdk_AddPlayerClass);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AddPlayerClassEx", (void *)sampgdk_AddPlayerClassEx);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AddStaticVehicle", (void *)sampgdk_AddStaticVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AddStaticVehicleEx", (void *)sampgdk_AddStaticVehicleEx);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AddStaticPickup", (void *)sampgdk_AddStaticPickup);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CreatePickup", (void *)sampgdk_CreatePickup);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DestroyPickup", (void *)sampgdk_DestroyPickup);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ShowNameTags", (void *)sampgdk_ShowNameTags);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ShowPlayerMarkers", (void *)sampgdk_ShowPlayerMarkers);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GameModeExit", (void *)sampgdk_GameModeExit);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetWorldTime", (void *)sampgdk_SetWorldTime);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetWeaponName", (void *)p_GetWeaponName);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::EnableTirePopping", (void *)sampgdk_EnableTirePopping);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::EnableVehicleFriendlyFire", (void *)sampgdk_EnableVehicleFriendlyFire);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AllowInteriorWeapons", (void *)sampgdk_AllowInteriorWeapons);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetWeather", (void *)sampgdk_SetWeather);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetGravity", (void *)sampgdk_SetGravity);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AllowAdminTeleport", (void *)sampgdk_AllowAdminTeleport);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetDeathDropAmount", (void *)sampgdk_SetDeathDropAmount);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CreateExplosion", (void *)sampgdk_CreateExplosion);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::EnableZoneNames", (void *)sampgdk_EnableZoneNames);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::UsePlayerPedAnims", (void *)sampgdk_UsePlayerPedAnims);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DisableInteriorEnterExits", (void *)sampgdk_DisableInteriorEnterExits);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetNameTagDrawDistance", (void *)sampgdk_SetNameTagDrawDistance);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DisableNameTagLOS", (void *)sampgdk_DisableNameTagLOS);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::LimitGlobalChatRadius", (void *)sampgdk_LimitGlobalChatRadius);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::LimitPlayerMarkerRadius", (void *)sampgdk_LimitPlayerMarkerRadius);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ConnectNPC", (void *)p_ConnectNPC);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerNPC", (void *)sampgdk_IsPlayerNPC);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerAdmin", (void *)sampgdk_IsPlayerAdmin);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::Kick", (void *)sampgdk_Kick);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::Ban", (void *)sampgdk_Ban);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::BanEx", (void *)p_BanEx);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SendRconCommand", (void *)p_SendRconCommand);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetServerVarAsString", (void *)p_GetServerVarAsString);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetServerVarAsInt", (void *)p_GetServerVarAsInt);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetServerVarAsBool", (void *)p_GetServerVarAsBool);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerNetworkStats", (void *)p_GetPlayerNetworkStats);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetNetworkStats", (void *)p_GetNetworkStats);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerVersion", (void *)p_GetPlayerVersion);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CreateMenu", (void *)p_CreateMenu);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DestroyMenu", (void *)sampgdk_DestroyMenu);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AddMenuItem", (void *)p_AddMenuItem);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetMenuColumnHeader", (void *)p_SetMenuColumnHeader);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ShowMenuForPlayer", (void *)sampgdk_ShowMenuForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::HideMenuForPlayer", (void *)sampgdk_HideMenuForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsValidMenu", (void *)sampgdk_IsValidMenu);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DisableMenu", (void *)sampgdk_DisableMenu);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DisableMenuRow", (void *)sampgdk_DisableMenuRow);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerMenu", (void *)sampgdk_GetPlayerMenu);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawCreate", (void *)p_TextDrawCreate);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawDestroy", (void *)sampgdk_TextDrawDestroy);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawLetterSize", (void *)sampgdk_TextDrawLetterSize);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawTextSize", (void *)sampgdk_TextDrawTextSize);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawAlignment", (void *)sampgdk_TextDrawAlignment);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawColor", (void *)sampgdk_TextDrawColor);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawUseBox", (void *)sampgdk_TextDrawUseBox);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawBoxColor", (void *)sampgdk_TextDrawBoxColor);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawSetShadow", (void *)sampgdk_TextDrawSetShadow);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawSetOutline", (void *)sampgdk_TextDrawSetOutline);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawBackgroundColor", (void *)sampgdk_TextDrawBackgroundColor);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawFont", (void *)sampgdk_TextDrawFont);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawSetProportional", (void *)sampgdk_TextDrawSetProportional);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawSetSelectable", (void *)sampgdk_TextDrawSetSelectable);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawShowForPlayer", (void *)sampgdk_TextDrawShowForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawHideForPlayer", (void *)sampgdk_TextDrawHideForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawShowForAll", (void *)sampgdk_TextDrawShowForAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawHideForAll", (void *)sampgdk_TextDrawHideForAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawSetString", (void *)p_TextDrawSetString);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawSetPreviewModel", (void *)sampgdk_TextDrawSetPreviewModel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawSetPreviewRot", (void *)sampgdk_TextDrawSetPreviewRot);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::TextDrawSetPreviewVehCol", (void *)sampgdk_TextDrawSetPreviewVehCol);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SelectTextDraw", (void *)sampgdk_SelectTextDraw);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CancelSelectTextDraw", (void *)sampgdk_CancelSelectTextDraw);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneCreate", (void *)sampgdk_GangZoneCreate);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneDestroy", (void *)sampgdk_GangZoneDestroy);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneShowForPlayer", (void *)sampgdk_GangZoneShowForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneShowForAll", (void *)sampgdk_GangZoneShowForAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneHideForPlayer", (void *)sampgdk_GangZoneHideForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneHideForAll", (void *)sampgdk_GangZoneHideForAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneFlashForPlayer", (void *)sampgdk_GangZoneFlashForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneFlashForAll", (void *)sampgdk_GangZoneFlashForAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneStopFlashForPlayer", (void *)sampgdk_GangZoneStopFlashForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GangZoneStopFlashForAll", (void *)sampgdk_GangZoneStopFlashForAll);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::Create3DTextLabel", (void *)p_Create3DTextLabel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::Delete3DTextLabel", (void *)sampgdk_Delete3DTextLabel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::Attach3DTextLabelToPlayer", (void *)sampgdk_Attach3DTextLabelToPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::Attach3DTextLabelToVehicle", (void *)sampgdk_Attach3DTextLabelToVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::Update3DTextLabelText", (void *)p_Update3DTextLabelText);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CreatePlayer3DTextLabel", (void *)p_CreatePlayer3DTextLabel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DeletePlayer3DTextLabel", (void *)sampgdk_DeletePlayer3DTextLabel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::UpdatePlayer3DTextLabelText", (void *)p_UpdatePlayer3DTextLabelText);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ShowPlayerDialog", (void *)p_ShowPlayerDialog);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetTimer", (void *)p_SetTimer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::KillTimer", (void *)sampgdk_KillTimer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::gpci", (void *)p_gpci);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SendDeathMessageToPlayer", (void *)sampgdk_SendDeathMessageToPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::BlockIpAddress", (void *)p_BlockIpAddress);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::UnBlockIpAddress", (void *)p_UnBlockIpAddress);

	//
	// a_objects natives
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CreateObject", (void *)sampgdk_CreateObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AttachObjectToVehicle", (void *)sampgdk_AttachObjectToVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AttachObjectToObject", (void *)sampgdk_AttachObjectToObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AttachObjectToPlayer", (void *)sampgdk_AttachObjectToPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetObjectPos", (void *)sampgdk_SetObjectPos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetObjectPos", (void *)sampgdk_GetObjectPos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetObjectRot", (void *)sampgdk_SetObjectRot);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetObjectRot", (void *)sampgdk_GetObjectRot);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsValidObject", (void *)sampgdk_IsValidObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DestroyObject", (void *)sampgdk_DestroyObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::MoveObject", (void *)sampgdk_MoveObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::StopObject", (void *)sampgdk_StopObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsObjectMoving", (void *)sampgdk_IsObjectMoving);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::EditObject", (void *)sampgdk_EditObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::EditPlayerObject", (void *)sampgdk_EditPlayerObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SelectObject", (void *)sampgdk_SelectObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CancelEdit", (void *)sampgdk_CancelEdit);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CreatePlayerObject", (void *)sampgdk_CreatePlayerObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AttachPlayerObjectToPlayer", (void *)sampgdk_AttachPlayerObjectToPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AttachPlayerObjectToVehicle", (void *)sampgdk_AttachPlayerObjectToVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerObjectPos", (void *)sampgdk_SetPlayerObjectPos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerObjectPos", (void *)sampgdk_GetPlayerObjectPos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerObjectRot", (void *)sampgdk_SetPlayerObjectRot);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetPlayerObjectRot", (void *)sampgdk_GetPlayerObjectRot);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsValidPlayerObject", (void *)sampgdk_IsValidPlayerObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DestroyPlayerObject", (void *)sampgdk_DestroyPlayerObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::MovePlayerObject", (void *)sampgdk_MovePlayerObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::StopPlayerObject", (void *)sampgdk_StopPlayerObject);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsPlayerObjectMoving", (void *)sampgdk_IsPlayerObjectMoving);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetObjectMaterial", (void *)p_SetObjectMaterial);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerObjectMaterial", (void *)p_SetPlayerObjectMaterial);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetObjectMaterialText", (void *)p_SetObjectMaterialText);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetPlayerObjectMaterialText", (void *)p_SetPlayerObjectMaterialText);

	//
	// a_vehicles natives
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsValidVehicle", (void *)sampgdk_IsValidVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleDistanceFromPoint", (void *)sampgdk_GetVehicleDistanceFromPoint);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CreateVehicle", (void *)sampgdk_CreateVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DestroyVehicle", (void *)sampgdk_DestroyVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsVehicleStreamedIn", (void *)sampgdk_IsVehicleStreamedIn);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehiclePos", (void *)sampgdk_GetVehiclePos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehiclePos", (void *)sampgdk_SetVehiclePos);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleZAngle", (void *)sampgdk_GetVehicleZAngle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleRotationQuat", (void *)sampgdk_GetVehicleRotationQuat);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehicleZAngle", (void *)sampgdk_SetVehicleZAngle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehicleParamsForPlayer", (void *)sampgdk_SetVehicleParamsForPlayer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ManualVehicleEngineAndLights", (void *)sampgdk_ManualVehicleEngineAndLights);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehicleParamsEx", (void *)sampgdk_SetVehicleParamsEx);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleParamsEx", (void *)sampgdk_GetVehicleParamsEx);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehicleToRespawn", (void *)sampgdk_SetVehicleToRespawn);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::LinkVehicleToInterior", (void *)sampgdk_LinkVehicleToInterior);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AddVehicleComponent", (void *)sampgdk_AddVehicleComponent);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::RemoveVehicleComponent", (void *)sampgdk_RemoveVehicleComponent);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ChangeVehicleColor", (void *)sampgdk_ChangeVehicleColor);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::ChangeVehiclePaintjob", (void *)sampgdk_ChangeVehiclePaintjob);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehicleHealth", (void *)sampgdk_SetVehicleHealth);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleHealth", (void *)sampgdk_GetVehicleHealth);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::AttachTrailerToVehicle", (void *)sampgdk_AttachTrailerToVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::DetachTrailerFromVehicle", (void *)sampgdk_DetachTrailerFromVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::IsTrailerAttachedToVehicle", (void *)sampgdk_IsTrailerAttachedToVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleTrailer", (void *)sampgdk_GetVehicleTrailer);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehicleNumberPlate", (void *)p_SetVehicleNumberPlate);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleModel", (void *)sampgdk_GetVehicleModel);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleComponentInSlot", (void *)sampgdk_GetVehicleComponentInSlot);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleComponentType", (void *)sampgdk_GetVehicleComponentType);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::RepairVehicle", (void *)sampgdk_RepairVehicle);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleVelocity", (void *)sampgdk_GetVehicleVelocity);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehicleVelocity", (void *)sampgdk_SetVehicleVelocity);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehicleAngularVelocity", (void *)sampgdk_SetVehicleAngularVelocity);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleDamageStatus", (void *)sampgdk_GetVehicleDamageStatus);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::UpdateVehicleDamageStatus", (void *)sampgdk_UpdateVehicleDamageStatus);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetVehicleVirtualWorld", (void *)sampgdk_SetVehicleVirtualWorld);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleVirtualWorld", (void *)sampgdk_GetVehicleVirtualWorld);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::GetVehicleModelInfo", (void *)sampgdk_GetVehicleModelInfo);

	//
	// logging
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::Print", (void *)p_Print);

	//
	// natives
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CallNativeArray", (void *)call_native_array);
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CallNativeArrayFloat", (void *)call_native_array_float);

}