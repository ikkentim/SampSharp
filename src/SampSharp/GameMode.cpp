// SampSharp
// Copyright 2017 Tim Potze
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

#include "GameMode.h"
#include <fstream>
#include <assert.h>
#include <iostream>
#include <fstream>
#include <stdio.h>
#include <string.h>
#include <limits>
#include <time.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/threads.h>
#include <mono/metadata/exception.h>
#include <mono/metadata/debug-helpers.h>
#include <mono/metadata/mono-gc.h>
#include "MonoRuntime.h"
#include "PathUtil.h"
#include "Config.h"

#define ERR_EXCEPTION                   (-1)

#define GET_PAR_SIZE(a, s, x)   (s->sizes[x] < 0 \
                                ? -s->sizes[x] \
                                : *(int *)mono_object_unbox( \
                                mono_array_get(a, MonoObject *, s->sizes[x])))

using std::string;
using sampgdk::logprintf;

bool GameMode::isLoaded_;
MonoDomain *GameMode::domain_;
MonoDomain *GameMode::previousDomain_;
GameMode::GameModeImage GameMode::gameMode_;
GameMode::GameModeImage GameMode::baseMode_;
GameMode::CallbackMap GameMode::callbacks_;
uint32_t GameMode::gameModeHandle_;
GameMode::TimerMap GameMode::timers_;
GameMode::ExtensionList GameMode::extensions_;
GameMode::NativeList GameMode::natives_;

MonoMethod *GameMode::onCallbackException_;
MonoMethod *GameMode::tickMethod_;
MonoClass *GameMode::paramLengthClass_;
MonoMethod *GameMode::paramLengthGetMethod_;
MonoAssembly *GameMode::assemby_;

int GameMode::bootSequenceNumber_;

std::map<uint16_t, uint16_t> GameMode::cptouni_;
std::map<uint16_t, uint16_t> GameMode::unitocp_;
bool GameMode::cpwide_[256];

bool GameMode::Load(std::string namespaceName, std::string className) {
    if (isLoaded_) {
        return false;
    }

    assert(MonoRuntime::IsLoaded());

    LoadCodepage(Config::GetCodepage().c_str());

    // Build paths based on the specified namespace and class names.
    string dirPath = PathUtil::GetPathInBin("gamemode/");
    string libraryPath = PathUtil::GetPathInBin("gamemode/")
        .append(namespaceName).append(".dll");
    string configPath = PathUtil::GetPathInBin("gamemode/")
        .append(namespaceName).append(".dll.config");

    // Check for existance of game mode.
    std::ifstream ifile(libraryPath.c_str());
    if (!ifile) {
        logprintf("ERROR: library does not exist!");
        return false;
    }

    // Create an appdomain for the game mode.
    char appdomainBuf[32];
    snprintf(appdomainBuf, sizeof(appdomainBuf), "sashDomainForBoot%d",
        bootSequenceNumber_++);

    previousDomain_ = mono_domain_get();
    domain_ = mono_domain_create_appdomain(appdomainBuf, NULL);

    mono_domain_set(domain_, 1);
    mono_thread_attach(domain_);

    mono_domain_set_config(domain_, dirPath.c_str(), configPath.c_str());

    logprintf("Loading image...");
    assemby_ = mono_domain_assembly_open(domain_, libraryPath.c_str());
    gameMode_.image = mono_assembly_get_image(assemby_);

    if (!gameMode_.image) {
        logprintf("ERROR: Couldn't open image!");
        return false;
    }

    logprintf("Creating gamemode instance...");
    gameMode_.klass = mono_class_from_name(gameMode_.image,
        namespaceName.c_str(), className.c_str());

    if (!gameMode_.klass) {
        logprintf("ERROR: Couldn't find class %s:%s!",
            namespaceName.c_str(), className.c_str());
        return false;
    }

    baseMode_.klass = mono_class_get_parent(gameMode_.klass);

    if (!baseMode_.klass || strcmp("BaseMode",
        mono_class_get_name(baseMode_.klass)) != 0) {
        logprintf("ERROR: Parent type of %s::%s is not "
            "SampSharp.GameMode::BaseMode!",
            namespaceName.c_str(), className.c_str());
        return false;
    }

    baseMode_.image = mono_class_get_image(baseMode_.klass);

    // Add all internal calls.
    AddInternalCall("RegisterExtension", (void *)RegisterExtension);
    AddInternalCall("SetTimer", (void *)SetRefTimer);
    AddInternalCall("KillTimer", (void *)KillRefTimer);
    AddInternalCall("NativeExists", (void *)NativeExists);
    AddInternalCall("LoadNative", (void *)LoadNative);
    AddInternalCall("InvokeNative", (void *)InvokeNative);
    AddInternalCall("Print", (void *)Print);
    AddInternalCall("SetCodepage", (void *)LoadCodepage);

    MonoObject *gamemode_obj = mono_object_new
        (mono_domain_get(), gameMode_.klass);
    gameModeHandle_ = mono_gchandle_new(gamemode_obj, false);
    mono_runtime_object_init(gamemode_obj);

    MonoMethod *method = LoadEvent("Initialize", 0);

    if (method) {
        MonoObject *exception = NULL;
        CallEvent(method, gameModeHandle_, NULL, &exception);

        return isLoaded_ = !exception;
    }
    else {
        isLoaded_ = true;
        return true;
    }
}

bool GameMode::Unload() {
    if (!isLoaded_) {
        return false;
    }

    // Clear found methods.
    tickMethod_ = NULL;
    paramLengthClass_ = NULL;
    paramLengthGetMethod_ = NULL;
    onCallbackException_ = NULL;

    // Clear timers.
    logprintf("Stopping timers...");
    for (TimerMap::iterator iter = timers_.begin();
        iter != timers_.end(); iter++) {
        int id = iter->first;
        RefTimer timer = (iter->second);

        mono_gchandle_free(timer.handle);
        KillTimer(id);
    }
    timers_.clear();

    // Clear extensions.
    logprintf("Unloading extensions...");
    for (ExtensionList::iterator iter = extensions_.begin();
        iter != extensions_.end(); iter++) {
        mono_gchandle_free(*iter);
    }
    extensions_.clear();

    // Clear callbacks.
    logprintf("Clearing callbacks table...");
    for (CallbackMap::iterator iter = callbacks_.begin();
        iter != callbacks_.end(); iter++) {
        iter->second->params.clear();
        delete iter->second;
    }
    callbacks_.clear();

    // Dispose of game mode.
    mono_thread_attach(domain_);

    MonoMethod *method = LoadEvent("Dispose", 0);

    if (method) {
        logprintf("Disposing gamemode...");
        CallEvent(method, gameModeHandle_, NULL, NULL);
    }

    // Release game mode.
    mono_gchandle_free(gameModeHandle_);


    gameModeHandle_ = 0;

    logprintf("Collecting garbage...");
    mono_gc_collect(mono_gc_max_generation());

    logprintf("Closing images...");
    mono_image_close(gameMode_.image);
    mono_image_close(baseMode_.image);

    logprintf("Collecting garbage...");
    mono_gc_collect(mono_gc_max_generation());

    logprintf("Unloading domain...");
    mono_domain_unload(domain_);

    gameMode_.image = NULL;
    gameMode_.klass = NULL;

    baseMode_.image = NULL;
    baseMode_.klass = NULL;

    domain_ = NULL;
    assemby_ = NULL;

    mono_domain_set(previousDomain_, 1);
    mono_thread_attach(previousDomain_);

    isLoaded_ = false;
    return true;
}

void GameMode::LoadCodepage(const char *name) {
    char path[64];
    snprintf(path, sizeof(path), "codepages/%s.txt", name);

    const uint16_t fallback[256] =
    {
        0x0000, 0x0001, 0x0002, 0x0003, 0x0004, 0x0005, 0x0006, 0x0007, 0x0008,
        0x0009, 0x000A, 0x000B, 0x000C, 0x000D, 0x000E, 0x000F, 0x0010, 0x0011,
        0x0012, 0x0013, 0x0014, 0x0015, 0x0016, 0x0017, 0x0018, 0x0019, 0x001A,
        0x001B, 0x001C, 0x001D, 0x001E, 0x001F, 0x0020, 0x0021, 0x0022, 0x0023,
        0x0024, 0x0025, 0x0026, 0x0027, 0x0028, 0x0029, 0x002A, 0x002B, 0x002C,
        0x002D, 0x002E, 0x002F, 0x0030, 0x0031, 0x0032, 0x0033, 0x0034, 0x0035,
        0x0036, 0x0037, 0x0038, 0x0039, 0x003A, 0x003B, 0x003C, 0x003D, 0x003E,
        0x003F, 0x0040, 0x0041, 0x0042, 0x0043, 0x0044, 0x0045, 0x0046, 0x0047,
        0x0048, 0x0049, 0x004A, 0x004B, 0x004C, 0x004D, 0x004E, 0x004F, 0x0050,
        0x0051, 0x0052, 0x0053, 0x0054, 0x0055, 0x0056, 0x0057, 0x0058, 0x0059,
        0x005A, 0x005B, 0x005C, 0x005D, 0x005E, 0x005F, 0x0060, 0x0061, 0x0062,
        0x0063, 0x0064, 0x0065, 0x0066, 0x0067, 0x0068, 0x0069, 0x006A, 0x006B,
        0x006C, 0x006D, 0x006E, 0x006F, 0x0070, 0x0071, 0x0072, 0x0073, 0x0074,
        0x0075, 0x0076, 0x0077, 0x0078, 0x0079, 0x007A, 0x007B, 0x007C, 0x007D,
        0x007E, 0x007F, 0x20AC, 0x9999, 0x201A, 0x0192, 0x201E, 0x2026, 0x2020,
        0x2021, 0x02C6, 0x2030, 0x0160, 0x2039, 0x0152, 0x9999, 0x017D, 0x9999,
        0x9999, 0x2018, 0x2019, 0x201C, 0x201D, 0x2022, 0x2013, 0x2014, 0x02DC,
        0x2122, 0x0161, 0x203A, 0x0153, 0x9999, 0x017E, 0x0178, 0x00A0, 0x00A1,
        0x00A2, 0x00A3, 0x00A4, 0x00A5, 0x00A6, 0x00A7, 0x00A8, 0x00A9, 0x00AA,
        0x00AB, 0x00AC, 0x00AD, 0x00AE, 0x00AF, 0x00B0, 0x00B1, 0x00B2, 0x00B3,
        0x00B4, 0x00B5, 0x00B6, 0x00B7, 0x00B8, 0x00B9, 0x00BA, 0x00BB, 0x00BC,
        0x00BD, 0x00BE, 0x00BF, 0x00C0, 0x00C1, 0x00C2, 0x00C3, 0x00C4, 0x00C5,
        0x00C6, 0x00C7, 0x00C8, 0x00C9, 0x00CA, 0x00CB, 0x00CC, 0x00CD, 0x00CE,
        0x00CF, 0x00D0, 0x00D1, 0x00D2, 0x00D3, 0x00D4, 0x00D5, 0x00D6, 0x00D7,
        0x00D8, 0x00D9, 0x00DA, 0x00DB, 0x00DC, 0x00DD, 0x00DE, 0x00DF, 0x00E0,
        0x00E1, 0x00E2, 0x00E3, 0x00E4, 0x00E5, 0x00E6, 0x00E7, 0x00E8, 0x00E9,
        0x00EA, 0x00EB, 0x00EC, 0x00ED, 0x00EE, 0x00EF, 0x00F0, 0x00F1, 0x00F2,
        0x00F3, 0x00F4, 0x00F5, 0x00F6, 0x00F7, 0x00F8, 0x00F9, 0x00FA, 0x00FB,
        0x00FC, 0x00FD, 0x00FE, 0x00FF
    };

    std::ifstream infile(path);

    cptouni_.clear();
    unitocp_.clear();
    for (int i = 0; i < 256; i++) {
        cpwide_[i] = false;
    }

    if (!infile.is_open()) {
        logprintf("[SampSharp] WARNING: Could not open codepage file %s. Using "
            "fallback codepage cp1252.", path);

        // Fallback codepage is cp1252
        for (uint16_t i = 0; i < 256; i++)  {
            cptouni_[i] = fallback[i];
            unitocp_[fallback[i]] = i;
        }
        return;
    }

    string line;

    while (std::getline(infile, line)) {
        if (line.substr(0, 2) != string("0x")) {
            continue;
        }

        int tab1 = line.find("\t");
        int tab2 = line.find("\t", tab1 + 1);

        if (tab1 <= 0 || tab2 <= 0) {
            continue;
        }

        string cp = line.substr(0, tab1);

        if (cp.find(" ") != string::npos) {
            continue;
        }

        uint16_t cps = (uint16_t)std::stoul(cp, nullptr, 16);

        string uni = line.substr(tab1 + 1, tab2 - tab1 - 1);

        if (uni.find(" ") != string::npos) {
            if (cps < 256) {
                cpwide_[(uint8_t)cps] = true;
                continue;
            }
            continue;
        }
        uint16_t unis = (uint16_t)std::stoul(uni, nullptr, 16);

        cptouni_[cps] = unis;
        unitocp_[unis] = cps;
    }
}

MonoString* GameMode::StringToMonoString(char* str, int len) {
    std::vector<mono_unichar2> buffer;
    for (int i = 0; i < len; i++) {
        uint16_t c = (uint16_t)str[i];
        if (c < 256 && cpwide_[c]) {
            if (i >= len - 1) {
                continue;
            }
            c <<= 8;
            c |= (uint16_t)str[++i];
        }

        if (c == 0) {
            break;
        }

        if (cptouni_.find(c) != cptouni_.end()) {
            buffer.push_back(cptouni_[c]);
        }
    }

    buffer.push_back('\0');

    return  mono_string_from_utf16(&buffer[0]);
}

char* GameMode::MonoStringToString(MonoString *str) {
    std::vector<char> buffer;
    mono_unichar2* uni_buf = mono_string_chars(str);
    int len = mono_string_length(str);
    for (int i = 0; i < len; i++) {
        uint16_t c = (uint16_t)uni_buf[i];

        if (c == 0) {
            break;
        }

        if (unitocp_.find(c) != unitocp_.end()) {
            uint16_t v = unitocp_[c];
            if (v & 0xff00) {
                buffer.push_back((uint8_t)(v >> 8));
            }
            buffer.push_back((uint8_t)(v & 0xff));
        }
    }

    buffer.push_back('\0');

    // TODO: Change this to something that doesn't give compile warnings.
    char *result = new char[buffer.size()];
    for (size_t i = 0; i < buffer.size(); i++) {
        result[i] = buffer[i];
    }

    return  result;
}


int GameMode::SetRefTimer(int interval, bool repeat, MonoObject *params) {
    if (!params) {
        /* If no params are parsed, there is no need to keep a reference to it,
         * or the id.
         */
        return SetTimer(interval, repeat, ProcessTimerTick, NULL);
    }

    // Stop the GC from collecting the params.
    uint32_t handle = mono_gchandle_new(params, false);
    int id = SetTimer(interval, repeat, ProcessTimerTick, &handle);

    timers_[id] = { handle, repeat };
    return id;
}

bool GameMode::KillRefTimer(int id) {
    // Delete the timer from the map.
    if (timers_.find(id) == timers_.end())
    {
        RefTimer timer = timers_[id];
        mono_gchandle_free(timer.handle);

        timers_.erase(id);
    }

    return KillTimer(id);
}

bool GameMode::RegisterExtension(MonoObject *extension) {
    if (!extension) {
        return false;
    }

    for (ExtensionList::iterator iter = extensions_.begin();
        iter != extensions_.end(); iter++) {
        if (mono_gchandle_get_target(*iter) == extension) {
            return false;
        }
    }

    uint32_t handle = mono_gchandle_new(extension, false);
    extensions_.push_back(handle);
    return true;
}

void GameMode::Print(MonoString *str) {
    char *buffer = MonoStringToString(str);
    logprintf("%s", buffer);
    delete[] buffer;
}

int GameMode::InvokeNative(int handle, MonoArray *args_array) {
    if (handle < 0 || handle >= (int)natives_.size()) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "invalid handle"));
    }

    /* Get the pointer to the native signature in the pool and check whether the
     * arguments count matches the signature. */
    NativeSignature *sig = &natives_[handle];

    if (!((!args_array && sig->param_count == 0) ||
        (args_array && sig->param_count == mono_array_length(args_array)))) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "invalid argument count"));
    }

    // Unbox all mono arguments and store them in the params array.
    void *params[MAX_NATIVE_ARGS];
    cell param_value[MAX_NATIVE_ARGS];
    int param_size[MAX_NATIVE_ARGS];
    for (int i = 0; i < sig->param_count; i++) {
        MonoObject* obj = mono_array_get(args_array, MonoObject *, i);

        switch (sig->parameters[i]) {
        case 'd': // integer
            params[i] = mono_object_unbox(obj);
            break;
        case 's': { // const string
            params[i] = MonoStringToString((MonoString*)obj);
            break;
        }
        case 'a': { // array of integers
            MonoArray *values_array = (MonoArray*)obj;

            param_size[i] = GET_PAR_SIZE(args_array, sig, i);

            cell *value = new cell[param_size[i]];
            for (int j = 0; j < param_size[i]; j++) {
                value[j] = mono_array_get(values_array, int, j);
            }
            params[i] = value;
            break;
        }
        case 'D': { // integer reference
            params[i] = obj
                ? *(int **)mono_object_unbox(obj)
                : &param_value[i];
            break;
        }
        case 'S': { // non-const string (writeable)
            param_size[i] = GET_PAR_SIZE(args_array, sig, i);
            char *str = new char[param_size[i] + 1];
            str[0] = '\0';
            params[i] = str;
            break;
        }
        case 'A': { // array of integers reference
            param_size[i] = GET_PAR_SIZE(args_array, sig, i);

            cell *value = new cell[param_size[i]];
            for (int j = 0; j < param_size[i]; j++) {
                // Set default value to int.MinValue
                value[j] = std::numeric_limits<int>::min();
            }
            params[i] = value;
            break;
        }
        default:
            mono_raise_exception(mono_get_exception_invalid_operation(
                "invalid format type"));
            return ERR_EXCEPTION;
            break;
        }
    }

    int return_value = sampgdk::InvokeNativeArray(sig->native, sig->format,
        params);

    /* Delete buffers and write reference types back to the mono arguments
     * array. */
    for (int i = 0; i < sig->param_count; i++) {
        switch (sig->parameters[i]) {
        case 's': // const string
        case 'a': // array of integers
            delete[] params[i];
            break;
        case 'D': { // integer reference
            int result = *(int *)params[i];
            MonoObject *obj = mono_value_box(mono_domain_get(),
                mono_get_int32_class(), &result);
            mono_array_set(args_array, MonoObject*, i, obj);
            break;
        }
        case 'S': { // non-const string (writeable)
            MonoString *str = StringToMonoString((char *)params[i],
                param_size[i]);
            mono_array_set(args_array, MonoString *, i, str);
            delete[] params[i];
            break;
        }
        case 'A': { // array of integers reference
            cell *param_array = (cell *)params[i];
            MonoArray *arr = mono_array_new(mono_domain_get(),
                mono_get_int32_class(), param_size[i]);
            for (int j = 0; j < param_size[i]; j++) {
                mono_array_set(arr, int, j, param_array[j]);
            }
            mono_array_set(args_array, MonoArray *, i, arr);

            delete[] params[i];
            break;
        }
        }
    }

    return return_value;
}

int GameMode::LoadNative(MonoString *name_string, MonoString *format_string,
    MonoArray *sizes_array)
{
    int size_idx = 0;
    NativeSignature sig;

    if (!name_string) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "name cannot be null"));
        return ERR_EXCEPTION;
    }

    // Check whether the parameters count is less than MAX_NATIVE_ARGS.
    sig.param_count = !format_string ? 0 : mono_string_length(format_string);
    if (sig.param_count >= MAX_NATIVE_ARGS) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "too many arguments"));
        return ERR_EXCEPTION;
    }

	char* utf8_name_string = mono_string_to_utf8(name_string);
    snprintf(sig.name, sizeof(sig.name), "%s", utf8_name_string);
	mono_free(utf8_name_string);

    sig.format[0] = '\0';

	if (!format_string) {
        sig.parameters[0] = '\0';
	}
	else {
		char* utf8_format_string = mono_string_to_utf8(format_string);
        snprintf(sig.parameters, sizeof(sig.parameters), "%s", utf8_format_string);
		mono_free(utf8_format_string);
	}

    // Find the specified native. If it wasn't found throw an exception.
    sig.native = sampgdk::FindNative(sig.name);
    if (!sig.native) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "native not found"));
        return ERR_EXCEPTION;
    }

    // Validate the passed format and create the amx format string.
    for (int i = 0; i < sig.param_count; i++) {
        switch (sig.parameters[i]) {
        case 'd': // integer
            snprintf(sig.format, sizeof(sig.format), "%sd", sig.format);
            break;
        case 's': { // const string
            snprintf(sig.format, sizeof(sig.format), "%ss", sig.format);
            break;
        }
        case 'D': // integer reference
            snprintf(sig.format, sizeof(sig.format), "%sR", sig.format);
            break;
        case 'a': { // array of integers
            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter.a/v"));
                return ERR_EXCEPTION;
            }

            sig.sizes[i] = mono_array_get(sizes_array, int, size_idx++);

            if (sig.sizes[i] < 0) {
                snprintf(sig.format, sizeof(sig.format), "%sa[%d]", sig.format, -sig.sizes[i]);
            }
            else {
                snprintf(sig.format, sizeof(sig.format), "%sa[*%d]", sig.format, sig.sizes[i]);
            }
            break;
        }
        case 'S': { // non-const string (writeable)
            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter.S"));
                return ERR_EXCEPTION;
            }

            sig.sizes[i] = mono_array_get(sizes_array, int, size_idx++);

            if (sig.sizes[i] < 0) {
                snprintf(sig.format, sizeof(sig.format), "%sS[%d]", sig.format, -sig.sizes[i]);
            }
            else {
                snprintf(sig.format, sizeof(sig.format), "%sS[*%d]", sig.format, sig.sizes[i]);
            }
            break;
        }
        case 'A': { // array of integers reference
            if (!sizes_array) {
                mono_raise_exception(mono_get_exception_invalid_operation(
                    "sizes cannot be null when an array or string "
                    "reference type is passed as parameter.A/V"));
                return ERR_EXCEPTION;
            }

            sig.sizes[i] = mono_array_get(sizes_array, int, size_idx++);

            if (sig.sizes[i] < 0) {
                snprintf(sig.format, sizeof(sig.format), "%sA[%d]", sig.format, -sig.sizes[i]);
            }
            else {
                snprintf(sig.format, sizeof(sig.format), "%sA[*%d]", sig.format, sig.sizes[i]);
            }
            break;
        }
        default:
            mono_raise_exception(mono_get_exception_invalid_operation(
                "invalid format type"));
            return ERR_EXCEPTION;
            break;
        }

    }

    /* Check whether the native has already been loaded. If it has check whether
    * the signature matches the specified format and return it's handle.*/
    for (int i = 0; i < (int)natives_.size(); i++) {
        if (!strcmp(natives_[i].name, sig.name)) {
            if (natives_[i].param_count == sig.param_count &&
                !strcmp(natives_[i].format, sig.format)) {
                return i;
            }
        }
    }

    int result = natives_.size();
    natives_.push_back(sig);
    return result;
}

bool GameMode::NativeExists(MonoString *name_string) {
    if (!name_string) {
        mono_raise_exception(mono_get_exception_invalid_operation(
            "name cannot be null"));
        return false;
    }

	char* utf8_name_string = mono_string_to_utf8(name_string);
	bool find_native_result = !!sampgdk::FindNative(utf8_name_string);
	mono_free(utf8_name_string);

	return find_native_result;
}

void GameMode::ProcessTimerTick(int timerid, void *data) {
    if (!isLoaded_) {
        return;
    }

    static MonoMethod *method;

    if (method == NULL) {
        method = LoadEvent("OnTimerTick", 2);
    }

    void *args[2];
    args[0] = &timerid;
    args[1] = NULL;
    RefTimer *timer = NULL;
    if (timers_.find(timerid) != timers_.end()) {
        timer = &timers_[timerid];
        args[1] = mono_gchandle_get_target(timer->handle);
    }

    CallEvent(method, gameModeHandle_, args, NULL);

    /* After OnTimerTick has been called and the timer is not repeating,
     * drop the handle and erase the timer from the map.
     */
    if (timer && !timer->repeating) {
        if (args[1]) {
            // Only free the handle if it was asociated with an object.
            mono_gchandle_free(timer->handle);
        }
        timers_.erase(timerid);
    }
}

void GameMode::ProcessTick() {
    if (!isLoaded_) {
        return;
    }

    if (!tickMethod_) {
        tickMethod_ = LoadEvent("OnTick", 0);
    }

    CallEvent(tickMethod_, gameModeHandle_, NULL, NULL);
}

void GameMode::AddInternalCall(const char * name, const void * method) {
    // Namespace to which every internal call is registered.
    static const char * namespase = "SampSharp.GameMode.API.Interop";

    // Construct combination of 'namespace::method'.
    size_t call_length = strlen(namespase) + 2 /* :: */ + strlen(name) + 1;
    char * call = new char[call_length];
    snprintf(call, call_length, "%s::%s", namespase, name);

    mono_add_internal_call(call, method);

    delete[] call;
}

GameMode::ParameterType GameMode::GetParameterType(MonoType *type) {
    char *type_name = mono_type_get_name(type);

    if (!strcmp(type_name, "System.Int32")) {
        return PARAM_INT;
    }
    else if (!strcmp(type_name, "System.Single")) {
        return PARAM_FLOAT;
    }
    else if (!strcmp(type_name, "System.String")) {
        return PARAM_STRING;
    }
    else if (!strcmp(type_name, "System.Boolean")) {
        return PARAM_BOOL;
    }
    else if (!strcmp(type_name, "System.Int32[]")) {
        return PARAM_INT_ARRAY;
    }
    else if (!strcmp(type_name, "System.Single[]")) {
        return PARAM_FLOAT_ARRAY;
    }
    else if (!strcmp(type_name, "System.Boolean[]")) {
        return PARAM_BOOL_ARRAY;
    }

    return PARAM_INVALID;
}

bool GameMode::IsMethodValidCallback(MonoImage *image, MonoMethod *method,
    int param_count) {
    void *iter = NULL;
    MonoType* type = NULL;

    if (!image || !method) {
        return false;
    }

    MonoMethodSignature *sig = mono_method_get_signature(method, image,
        mono_method_get_token(method));

    if (param_count != mono_signature_get_param_count(sig)) {
        return false;
    }

    while (type = mono_signature_get_params(sig, &iter)) {
        if (GetParameterType(type) == PARAM_INVALID) {
            return false;
        }
    }

    return true;
}

MonoMethod *GameMode::FindMethodForCallbackInClass(const char *name,
    int param_count, MonoClass *klass) {
    MonoMethod *method;
    MonoImage *image;

    /* Find the first occurance of the callback name in the given class and
     * it's base classes. If a method was found, but it's signature contains
     * unsupported types, look for other overloads in this class.
     */

    while (klass) {
        image = mono_class_get_image(klass);
        method = mono_class_get_method_from_name(klass, name, param_count);

        if (IsMethodValidCallback(image, method, param_count)) {
            return method;
        }

        if (method) {
            void *method_iter = NULL;
            while ((method = mono_class_get_methods(klass, &method_iter))) {
                if (!strcmp(mono_method_get_name(method), name) &&
                    IsMethodValidCallback(mono_class_get_image(
                    mono_method_get_class(method)), method, param_count)) {
                    return method;
                }
            }
        }
        klass = mono_class_get_parent(klass);
    }

    return NULL;
}

MonoMethod *GameMode::FindMethodForCallback(const char *name,
    int param_count, uint32_t &handle) {
    MonoMethod *method;

    // Look in the game mode.
    method = FindMethodForCallbackInClass(name, param_count, gameMode_.klass);

    if (method) {
        handle = gameModeHandle_;
        return method;
    }

    // Look in the extensions.
    for (ExtensionList::iterator iter = extensions_.begin();
        iter != extensions_.end(); iter++) {
        MonoClass *klass = mono_object_get_class(
            mono_gchandle_get_target(handle = *iter));

        method = FindMethodForCallbackInClass(name, param_count, klass);

        if (method) {
            return method;
        }
    }

    return NULL;
}

void GameMode::ProcessPublicCall(AMX *amx, const char *name, cell *params,
    cell *retval) {

    CallbackSignature *signature;

    if (!isLoaded_) {
        return;
    }

    int param_count = params[0] / sizeof(cell);
    if (strlen(name) == 0 || param_count > MAX_CALLBACK_PARAM_COUNT) {
        logprintf("[SampSharp] WARNING: Skipped callback with %d parameters.",
            param_count);
        return;
    }

    /* OnRconCommand can sometimes end up on different theads?
     * Just to make sure, attach the current thread to the domain.
     */
    mono_thread_attach(domain_);

    /* If the callback not known in the callbacks_ map, find the callback in
     * the game mode or one of the registered extensions.
     */
    if (callbacks_.find(name) == callbacks_.end()) {
        signature = new CallbackSignature;
        signature->method = FindMethodForCallback(name, param_count,
            signature->handle);

        if (!signature->method) {
            callbacks_[name] = NULL;
            return;
        }

        MonoImage *image = mono_class_get_image(
            mono_method_get_class(signature->method));

        void *iter = NULL;
        int iter_idx = 0;

        MonoMethodSignature *sig = mono_method_get_signature(signature->method,
            image, mono_method_get_token(signature->method));

        MonoType* type = NULL;
        while (type = mono_signature_get_params(sig, &iter)) {
            ParameterSignature parameter_signature;

            parameter_signature.type = GetParameterType(type);

            if (parameter_signature.type == PARAM_INT_ARRAY ||
                parameter_signature.type == PARAM_FLOAT_ARRAY ||
                parameter_signature.type == PARAM_BOOL_ARRAY) {
                parameter_signature.length_idx = GetParamLengthIndex(
                    signature->method, iter_idx);

                if (parameter_signature.length_idx == -1) {
                    callbacks_[name] = NULL;
                    return;
                }
            }

            signature->params[iter_idx++] = parameter_signature;
        }

        callbacks_[name] = signature;
    }

    if (signature = callbacks_[name]) {
        // Handle calls without parameters.
        if (!param_count) {
            int retint = CallEvent(signature->method, signature->handle, NULL,
                NULL);

            /* If there's a cell allocated for the return value and
             * the callback was executed successfuly, fill the cell with
             * the returned value.
             */
            if (retval != NULL && retint != -1) {
                *retval = retint;
            }
            return;
        }

        // Handle calls with parameters.
        void *args[MAX_CALLBACK_PARAM_COUNT];
        int len = 0;
        cell *addr = NULL;
        MonoArray *arr;

        if (signature->params.size() != param_count)
        {
            logprintf("[SampSharp] ERROR: Parameters of callback %s "
                "does not match signature (called: %d, signature: %d)",
                name, param_count, signature->params.size());
            return;
        }

        for (int i = 0; i < param_count; i++) {
            switch (signature->params[i].type) {
            case PARAM_INT:
            case PARAM_FLOAT:
            case PARAM_BOOL: {
                args[i] = &params[i + 1];
                break;
            }
            case PARAM_STRING: {
                amx_GetAddr(amx, params[i + 1], &addr);
                amx_StrLen(addr, &len);

                if (len) {
                    len++;

                    char* text = new char[len];

                    amx_GetString(text, addr, 0, len);
                    args[i] = StringToMonoString(text, len);
                }
                else {
                    args[i] = mono_string_new(mono_domain_get(), "");
                }
                break;
            }

            case PARAM_INT_ARRAY: {
                len = params[signature->params[i].length_idx];
                arr = mono_array_new(mono_domain_get(), mono_get_int32_class(),
                    len);

                if (len > 0) {
                    cell* addr = NULL;
                    amx_GetAddr(amx, params[i + 1], &addr);

                    for (int i = 0; i < len; i++) {
                        mono_array_set(arr, int, i, *(addr + i));
                    }
                }
                args[i] = arr;
                break;
            }
            case PARAM_FLOAT_ARRAY: {
                len = params[signature->params[i].length_idx];
                arr = mono_array_new(mono_domain_get(), mono_get_int32_class(),
                    len);

                if (len > 0) {
                    cell* addr = NULL;
                    amx_GetAddr(amx, params[i + 1], &addr);

                    for (int i = 0; i < len; i++) {
                        mono_array_set(arr, float, i, amx_ctof(*(addr + i)));
                    }
                }
                args[i] = arr;
                break;
            }
            case PARAM_BOOL_ARRAY: {
                len = params[signature->params[i].length_idx];
                arr = mono_array_new(mono_domain_get(), mono_get_int32_class(),
                    len);

                if (len > 0) {
                    cell* addr = NULL;
                    amx_GetAddr(amx, params[i + 1], &addr);

                    for (int i = 0; i < len; i++) {
                        mono_array_set(arr, bool, i, !!*(addr + i));
                    }
                }
                args[i] = arr;
                break;
            }
            default:
                logprintf("[SampSharp] ERROR: Signature of %s contains "
                    "unsupported parameters.", name);
                return;
            }
        }

        int retint = CallEvent(signature->method, signature->handle, args,
            NULL);

        if (retval != NULL && retint != -1) {
            *retval = retint;
        }
    }
}

MonoMethod *GameMode::LoadEvent(const char *name, int param_count) {
    MonoMethod *method = mono_class_get_method_from_name(
        gameMode_.klass, name, param_count);

    if (!method) {
        method = mono_class_get_method_from_name(
            baseMode_.klass, name, param_count);
    }

    return method;
}

void GameMode::PrintException(const char *methodname, MonoObject *exception) {
    char *stacktrace = mono_string_to_utf8(
        mono_object_to_string(exception, NULL));

    /* Print error to console. Cannot print the exception to logprintf; the
     * buffer is too small.
     */
    std::cout << "[SampSharp] Exception thrown during execution of "
        << methodname << ":" << std::endl
        << stacktrace << std::endl;

    // Append error to log file.
    time_t now = time(0);
    tm _Tm;

#if SAMPSHARP_WINDOWS
    localtime_s(&_Tm, &now);
#elif SAMPSHARP_LINUX
    _Tm = *localtime_r(&now, &_Tm);
#endif

    char timestamp[32];
    strftime(timestamp, sizeof(timestamp), "[%d/%m/%Y %H:%M:%S]", &_Tm);

    std::ofstream logfile;
    logfile.open("SampSharp_errors.log", std::ios::app | std::ios::binary);
    logfile << timestamp << " Exception thrown" << methodname << ":"
        << "\r\n" << stacktrace << "\r\n";
	logfile.close();
	mono_free(stacktrace);
}

int GameMode::CallEvent(MonoMethod *method, uint32_t handle, void **params,
    MonoObject **exception_return) {
    assert(method);
    assert(handle);

    MonoObject *exception;
    MonoObject *response = mono_runtime_invoke(method,
        mono_gchandle_get_target(handle), params, &exception);

    if (exception) {
        // Return the exception.
        if (exception_return) {
            *exception_return = exception;
        }

        // Find callback handler if it has not been found previously.
        if (isLoaded_ && !onCallbackException_ &&
            mono_class_get_method_from_name(baseMode_.klass,
                "OnCallbackException", 1)) {

            void *method_iter = NULL;
            while ((onCallbackException_ = mono_class_get_methods(
                baseMode_.klass, &method_iter))) {
                if (!strcmp(mono_method_get_name(onCallbackException_),
                    "OnCallbackException")) {
                    MonoMethodSignature *sig = mono_method_get_signature(
                        onCallbackException_, baseMode_.image,
                        mono_method_get_token(onCallbackException_));

                    void *type_iter = NULL;
                    MonoType *type = mono_signature_get_params(sig, &type_iter);

                    if (!strcmp(mono_type_get_name(type), "System.Exception")) {
                        break;
                    }
                }
            }
        }

        // Invoke the callback handler if it exists.
        if (isLoaded_ && onCallbackException_) {
            MonoObject *exception2;
            MonoObject *response2 = mono_runtime_invoke(onCallbackException_,
                mono_gchandle_get_target(gameModeHandle_), (void **)&exception,
                &exception2);

            if (exception2) {
                PrintException("OnCallbackException", exception2);
            }
            else if (response2 && *(bool *)mono_object_unbox(response2)) {
                return -1;
            }
        }

        PrintException(mono_method_get_name(method), exception);
        return -1;
    }

    // Cast the response of the event to an integer.
    if (!response) {
        return -1;
    }

    if (mono_type_get_type(mono_signature_get_return_type(
        mono_method_signature(method))) ==
        mono_type_get_type(mono_class_get_type(mono_get_boolean_class())))
        return *(bool *)mono_object_unbox(response) ? 1 : 0;
    else
        return *(int *)mono_object_unbox(response);
}

int GameMode::GetParamLengthIndex(MonoMethod *method, int idx) {
    if (!paramLengthClass_) {
        paramLengthClass_ = mono_class_from_name(baseMode_.image,
            PARAM_LENGTH_ATTRIBUTE_NAMESPACE, PARAM_LENGTH_ATTRIBUTE_CLASS);
    }
    if (!paramLengthGetMethod_) {
        paramLengthGetMethod_ = mono_property_get_get_method(
            mono_class_get_property_from_name(paramLengthClass_, "Index"));
    }

    assert(paramLengthClass_);
    assert(paramLengthGetMethod_);

    MonoCustomAttrInfo *attr = mono_custom_attrs_from_param(method, idx + 1);
    if (!attr) {
        logprintf("[SampSharp] ERROR: No callback parameter info for %s @ %d",
            mono_method_get_name(method), idx);
        return -1;
    }

    MonoObject *attrObj = mono_custom_attrs_get_attr(attr, paramLengthClass_);
    if (!attrObj) {
        logprintf("[SampSharp] ERROR: Array parameter has no specified size:"
            "%s @ %d",
            mono_method_get_name(method), idx);
        return -1;
    }

    return *(int*)mono_object_unbox(mono_runtime_invoke(paramLengthGetMethod_,
        attrObj, NULL, NULL));
}
