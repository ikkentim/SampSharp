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
#include "ConfigReader.h"
#include "StringUtil.h"
#include "server.h"

extern void *pAMXFunctions;

using std::string;
using std::ostringstream;
using sampgdk::logprintf;

server *svr;

bool ValidateConfig() {
    /* check whether gamemodeN values contain acceptable values. */
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
            return false;
        }
        else if (i > 0 && gamemode_value.length() > 0) {
            logprintf("ERROR: Can not load sampsharp if a non-SampSharp"
                "gamemode is set to load.");
            logprintf("ERROR: Please ensure you only specify one script"
                "gamemode, namely 'gamemode0 empty 1' in your server.cfg file.");
            return false;
        }
    }

    return true;
}

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
    /* load sampgdk. if loading fails, prevent the whole plugin from loading. */
    if (!sampgdk::Load(ppData)) {
        return false;
    }

    logprintf("");
    logprintf("SampSharp Plugin");
    logprintf("----------------");
    logprintf("v%s, (C)2014-2017 Tim Potze", PLUGIN_VERSION);
    logprintf("");

    /* store amx functions pointer in order to be able to interface with amx. */
    pAMXFunctions = ppData[PLUGIN_DATA_AMX_EXPORTS];

    if (!ValidateConfig()) {
        /* during development, the server crashed while returning false here. 
         * seeing the user will need to change their configuration file, it 
         * doesn't really matter.
         */
        return false;
    }

    svr = new server();

    svr->load();

    return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    sampgdk_Unload();

    delete svr;
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
    sampgdk_ProcessTick();
    svr->tick();
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPublicCall(AMX *amx, const char *name,
    cell *params, cell *retval) {

    svr->public_call(amx, name, params, retval);
    return true;
}
