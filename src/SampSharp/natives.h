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

#include <sampgdk/sampgdk.h>
#include <string>
#include <mono/metadata/exception.h>

#include "monohelper.h"
#include "unicode.h"

#pragma once

#define MAX_NATIVE_ARGS				(32)
#define MAX_NATIVE_ARG_FORMAT_LEN	(8)
#define ERR_EXCEPTION				(-1)

using std::string;

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

	int retval = sampgdk_CreatePlayerTextDraw(playerid, x, y,  buffer);

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
        drawDistance,  virtualworld, testLOS);

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

//
// native functions
bool native_exists(MonoString *name_string) {
    char *name;

    if (!name_string) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "name cannot be null"));
        return ERR_EXCEPTION;
    }

    name = mono_string_to_utf8(name_string);

    AMX_NATIVE native = sampgdk::FindNative(name);
    
    return !!native;
}
int call_native_array(MonoString *name_string, MonoString *format_string,
    MonoArray *args_array, MonoArray *sizes_array) {
    char *name;
    char *format;
    char amx_format[MAX_NATIVE_ARGS * MAX_NATIVE_ARG_FORMAT_LEN] = "";
    void *params[MAX_NATIVE_ARGS];
    int param_size[MAX_NATIVE_ARGS];
    int args_count = 0;
    int size_idx = 0;

    if (!name_string) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "name cannot be null"));
        return ERR_EXCEPTION;
    }

    name = mono_string_to_utf8(name_string);
    format = !format_string ? "" : mono_string_to_utf8(format_string);

    args_count = !args_array ? 0 : mono_array_length(args_array);

    if (args_count > MAX_NATIVE_ARGS) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "too many arguments"));
        return ERR_EXCEPTION;
    }

    AMX_NATIVE native = sampgdk::FindNative(name);
    if (!native) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "native not found"));
        return ERR_EXCEPTION;
    }

    for (int i = 0; i < args_count; i++) {
        switch (format[i]) {
        case 'd': /* integer */
        case 'b': /* boolean */
            params[i] = mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i));
            sprintf(amx_format, "%sd", amx_format);
            break;
        case 'f': /* floating-point */
            params[i] = &amx_ftoc(*(float *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i)));
            sprintf(amx_format, "%sf", amx_format);
            break;
        case 's': { /* const string */
            params[i] = monostring_to_string(
                mono_array_get(args_array, MonoString *, i));
            sprintf(amx_format, "%ss", amx_format);
            break;
        }
        case 'a': { /* array of integers */
            MonoArray *values_array =
                mono_array_get(args_array, MonoArray *, i);

            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            cell *value = new cell[param_size[i]];
            for (int j = 0; j < param_size[i]; j++) {
                value[j] = mono_array_get(values_array, int, j);
            }
            params[i] = value;

            sprintf(amx_format, "%sa[%d]", amx_format, param_size[i]);
            break;
        }
        case 'v': { /* array of floats */
            MonoArray *values_array =
                mono_array_get(args_array, MonoArray *, i);

            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            cell *value = new cell[param_size[i]];
            for (int j = 0; j<param_size[i]; j++) {
                value[j] = amx_ftoc(mono_array_get(values_array, float, j));
            }
            params[i] = value;

            sprintf(amx_format, "%sa[%d]", amx_format, param_size[i]);
            break;
        }
        case 'D': /* integer reference */
            params[i] = *(int **)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i));
            sprintf(amx_format, "%sR", amx_format);
            break;
        case 'F': /* floating-point reference */
            params[i] = &amx_ftoc(**(float **)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i)));
            sprintf(amx_format, "%sR", amx_format);
            break;
        case 'S': /* non-const string (writeable) */ {
            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            params[i] = new cell[param_size[i] + 1] {'\0'};

            assert(params[i]);

            sprintf(amx_format, "%sS[%d]", amx_format, param_size[i]);
            break;
        }
        case 'A': { /* array of integers reference */
            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            cell *value = new cell[param_size[i]];
            for (int j = 0; j < param_size[i]; j++) {
                /* Set default value to int.MinValue */
                value[j] = -2147483648;
            }
            params[i] = value;

            sprintf(amx_format, "%sA[%d]", amx_format, param_size[i]);
            break;
        }
        case 'V': { /* array of floating-points reference */
            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter."));
                return ERR_EXCEPTION;
            }

            int size_info = mono_array_get(sizes_array, int, size_idx++);
            param_size[i] = size_info < 0 ? -size_info
                : *(int *)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, size_info));

            params[i] = new cell[param_size[i]];

            sprintf(amx_format, "%sR[%d]", amx_format, param_size[i]);
            break;
        }
        default:
            mono_raise_exception(mono_get_exception_invalid_operation(
                "invalid format type"));
            return ERR_EXCEPTION;
            break;
        }

    }

    int return_value = sampgdk::InvokeNativeArray(native, amx_format, params);

    for (int i = 0; i < args_count; i++) {
        switch (format[i]) {
        case 's': /* const string */
        case 'a': /* array of integers */
        case 'v': /* array of floats */
            delete[] params[i];
            break;
        case 'D': /* integer reference */
            **(int **)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i)) =
                *(int *)params[i];
            break;
        case 'F': /* floating-point reference */
            **(float **)mono_object_unbox(
                mono_array_get(args_array, MonoObject *, i)) =
                amx_ctof(*(cell *)params[i]);
            break;
        case 'S': /* non-const string (writeable) */
            *mono_array_get(args_array, MonoString **, i) =
                string_to_monostring((char *)params[i], param_size[i]);

            delete[] params[i];
            break;
        case 'A': { /* array of integers reference */
            cell *param_array = (cell *)params[i];
            MonoArray *arr = mono_array_new(mono_domain_get(),
                mono_get_int32_class(), param_size[i]);
            for (int j = 0; j < param_size[i]; j++) {
                mono_array_set(arr, int, j, param_array[j]);
            }
            *mono_array_get(args_array, MonoArray **, i) = arr;

            delete[] params[i];
            break;
        }
        case 'V': { /* array of floating-points reference */
            cell *param_array = (cell *)params[i];

            MonoArray *arr = mono_array_new(mono_domain_get(),
                mono_get_single_class(), param_size[i]);
            for (int j = 0; j<param_size[i]; j++) {
                mono_array_set(arr, float, j, amx_ctof(param_array[j]));
            }
            *mono_array_get(args_array, MonoArray **, i) = arr;

            delete[] params[i];
            break;
        }
        }
    }

    return return_value;
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
    mono_add_internal_call("SampSharp.GameMode.Natives.Native::SetCodepage", (void *)set_codepage);

	//
	// natives
	mono_add_internal_call("SampSharp.GameMode.Natives.Native::CallNativeArray", (void *)call_native_array);
    mono_add_internal_call("SampSharp.GameMode.Natives.Native::NativeExists", (void *)native_exists);

}
