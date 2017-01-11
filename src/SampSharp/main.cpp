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

#include "main.h"
#include <fstream>
#include <sampgdk/sampgdk.h>
#include <assert.h>
#include <string.h>
#include <iostream>
#include "Config.h"
#include "MonoRuntime.h"
#include "GameMode.h"
#include "StringUtil.h"


extern void *pAMXFunctions;

using std::string;
using std::ostringstream;
using sampgdk::logprintf;

bool filterscript_loaded = false;
bool initial_load = false;
bool plugin_initialized = false;
int run_signal = 0;
AMX *signal_amx;

void loadGamemode() {
    if (GameMode::IsLoaded()) return;

    // Load empty filterscript.
    if (!filterscript_loaded) {
        SendRconCommand("loadfs empty");
        filterscript_loaded = true;
    }

    // Load mono.
    if (!MonoRuntime::IsLoaded()) {
        MonoRuntime::Load(Config::GetMonoAssemblyDir(),
            Config::GetMonoConfigDir(), Config::GetTraceLevel(),
            PathUtil::GetPathInBin("gamemode/")
            .append(Config::GetGameModeNameSpace()).append(".dll"));
    }

    // Load game mode.
    string namespaceName = Config::GetGameModeNameSpace();
    string className = Config::GetGameModeClass();

    logprintf("Gamemode");
    logprintf("---------------");
    logprintf("Loading gamemode: %s:%s", namespaceName.c_str(),
        className.c_str());

    if (GameMode::Load(Config::GetGameModeNameSpace(),
        Config::GetGameModeClass()))
        logprintf("  Loaded.");
    else
        logprintf("  Failed.");

    logprintf("");
}

void unloadGamemode() {
    if (!GameMode::IsLoaded()) return;

    string namespase = Config::GetGameModeNameSpace();
    string klass = Config::GetGameModeClass();

    logprintf("");
    logprintf("---------------");
    logprintf("Unloading gamemode: %s:%s", namespase.c_str(), klass.c_str());

    GameMode::Unload();

    logprintf("  Unloaded.");
    logprintf("");
}

bool HandleRconCommands(AMX *amx, cell *params, cell *retval) {
    // The plugin handles two RCON commands:
    // sampsharpstop
    // sampsharpstart
    //
    // These commands can be used to unload a SampSharp game mode, replace the
    // DLL file and reloading the newly updated game mode.

    char buf[64];
    cell* addr;
    amx_GetAddr(amx, params[1], &addr);
    amx_GetString(buf, addr, 0, 64);

    if (!strcmp(buf, "sampsharpstop")) {
        if (!GameMode::IsLoaded()) {
            logprintf("A gamemode must be loaded in order to stop.");
            return false;
        }

        logprintf("Stopping SampSharp...");
        run_signal = -1;
        signal_amx = amx;

        return false;
    }
    if (!strcmp(buf, "sampsharpstart")) {
        if (!initial_load) {
            logprintf("OnGameModeInit needs to be called first.");
            return false;
        }
        if (GameMode::IsLoaded()) {
            logprintf("You need to stop the gamemode before you can start it.");
            return false;
        }

        logprintf("Starting SampSharp...");
        run_signal = 1;
        signal_amx = amx;

        return false;
    }

    return true;
}

void ProcessSignals() {
    // Process run signals.
    if (run_signal == -1) {
        cell noParams[1];
        noParams[0] = 0;
        cell unhandledRetval;

        GameMode::ProcessPublicCall(signal_amx, "OnGameModeExit", noParams,
            &unhandledRetval);

        unloadGamemode();

        logprintf("");
        logprintf("=================================");
        logprintf("> SampSharp gamemode has stopped!");
        logprintf("> Replace your gamemode files and run `sampsharp start`");
        logprintf("=================================");
        logprintf("");

        run_signal = 0;
    }
    else if (run_signal == 1) {
        loadGamemode();

        cell noParams[1];
        noParams[0] = 0;
        cell unhandledRetval;

        GameMode::ProcessPublicCall(signal_amx, "OnGameModeInit", noParams,
            &unhandledRetval);

        run_signal = 0;
    }
}

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
    // Load sampgdk. If loading fails, prevent the whole plugin from loading.
    if (!sampgdk::Load(ppData)) {
        return false;
    }

    // Store amx functions pointer in order to be able to interface with amx.
    pAMXFunctions = ppData[PLUGIN_DATA_AMX_EXPORTS];

    // Check whether gamemodeN values contain acceptable values.
    ConfigReader server_cfg("server.cfg");
    for (int i = 0; i < 15; i++) {
        ostringstream gamemode_key;
        gamemode_key << "gamemode";
        gamemode_key << i;

        string gamemode_value;
        server_cfg.GetOptionAsString(gamemode_key.str(), gamemode_value);
        gamemode_value = StringUtil::TrimString(gamemode_value);

        if (i == 0 && gamemode_value.compare("empty 1") != 0) {
            logprintf("ERROR: Can not load sampsharp if a non-SampSharp"
                "gamemode is set to load.");
            logprintf("ERROR: Please ensure you set 'gamemode0 empty 1' in your"
                "server.cfg file.");
            // During development, the server crashed while returning false
            // here. Seeing the user will need to change their configuration
            // file, it doesn't really matter.
            return false;
        }
        else if (i > 0 && gamemode_value.length() > 0) {
            logprintf("ERROR: Can not load sampsharp if a non-SampSharp"
                "gamemode is set to load.");
            logprintf("ERROR: Please ensure you only specify one script"
                "gamemode, namely 'gamemode0 empty 1' in your server.cfg file.");
            // During development, the server crashed while returning false
            // here. Seeing the user will need to change their configuration
            // file, it doesn't really matter.
            return false;
        }
    }

    logprintf("");
    logprintf("SampSharp Plugin");
    logprintf("----------------");
    logprintf("v%s, (C)2014-2017 Tim Potze", PLUGIN_VERSION);
    logprintf("");

    Config::Read();

    plugin_initialized = true;
    return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    if (plugin_initialized) {
        GameMode::Unload();
    }
    sampgdk::Unload();
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
    if (plugin_initialized) {
        GameMode::ProcessTick();
        sampgdk::ProcessTick();
        ProcessSignals();
    }
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPublicCall(AMX *amx, const char *name,
    cell *params, cell *retval) {
    // If the plugin did not initialize successfully, we don't care for any
    // public call arriving here.
    if (!plugin_initialized) {
        return false;
    }

    // If an RCON command has been received, let HandleRconCommands handle it.
    // If this function returns true, it did not handle the command. In this
    // case, let the call trough to the SampSharp game mode.
    if (!strcmp(name, "OnRconCommand") && params && params[0] == sizeof(cell)) {
        if (!HandleRconCommands(amx, params, retval)) {
            return false;
        }
    }
    // If the game mode initialize callback has been received, create the
    // SampSharp game mode instance before passing the call trough to it.
    else if (!strcmp(name, "OnGameModeInit")) {
        loadGamemode();
        initial_load = true;
    }
    // If a SampSharp game mode has been loaded and the game mode exit callback
    // has been received, pass it trough to the SampSharp game mode before
    // unloading the game mode instance.
    else if (GameMode::IsLoaded() && !strcmp(name, "OnGameModeExit")) {
        // Process call before actually unloading the gamemode.
        GameMode::ProcessPublicCall(amx, name, params, retval);

        unloadGamemode();
        return true;
    }

    if (GameMode::IsLoaded())
        GameMode::ProcessPublicCall(amx, name, params, retval);

    return true;
}
