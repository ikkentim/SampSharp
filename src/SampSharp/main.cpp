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

#include <fstream>
#include <assert.h>
#include <string.h>
#include <iostream>
#include <sampgdk/sampgdk.h>
#include "ConfigReader.h"
#include "StringUtil.h"
#include "server.h"
#include "version.h"
#include "pipe_server.h"

using sampgdk::logprintf;

extern void *pAMXFunctions;

server *svr = NULL;
bool ready;

void print_err(const char* error) {
    logprintf("[SampSharp:ERROR] %s", error);
}

void print_info() {
    logprintf("");
    logprintf("SampSharp Plugin");
    logprintf("----------------");
    logprintf("v%s, (C)2014-2017 Tim Potze", PLUGIN_VERSION_STR);
    logprintf("");
}

bool config_validate() {
    /* check whether gamemodeN values contain acceptable values. */
    ConfigReader server_cfg("server.cfg");
    for (int i = 0; i < 15; i++) {
        std::ostringstream gamemode_key;
        gamemode_key << "gamemode";
        gamemode_key << i;

        std::string gamemode_value;
        server_cfg.GetOptionAsString(gamemode_key.str(), gamemode_value);
        gamemode_value = StringUtil::TrimString(gamemode_value);

        if (i == 0 && gamemode_value.compare("empty 1") != 0) {
            print_err("Can not load sampsharp if a non-SampSharp gamemode is "
                "set to load.");
            print_err("Please ensure you set 'gamemode0 empty 1' in your "
                "server.cfg file.");
            return false;
        }
        else if (i > 0 && gamemode_value.length() > 0) {
            print_err("Can not load sampsharp if a non-SampSharp gamemode is "
                "set to load.");
            print_err("Please ensure you only specify one script gamemode, "
                "namely 'gamemode0 empty 1' in your server.cfg file.");
            return false;
        }
    }

    return true;
}

void start_server() {
    sampgdk_SendRconCommand("loadfs empty");

    print_info();


    ConfigReader server_cfg("server.cfg");
    std::string value;
    server_cfg.GetOptionAsString("sampsharp_pipe", value);

    pipe_server *p = new pipe_server(value.c_str());

    svr = new server(p);
    svr->start();
}

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
    if (!sampgdk::Load(ppData)) {
        return false;
    }

    /* amx functions are used to gather info from callbacks */
    pAMXFunctions = ppData[PLUGIN_DATA_AMX_EXPORTS];

    /* validate the server config is fit for running SampSharp */
    if (!(ready = config_validate())) {
        return ready = false;
    }

    return ready = true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    sampgdk_Unload();
    delete svr;
    svr = NULL;
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
    sampgdk_ProcessTick();
    if (svr) {
        svr->tick();
    }
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPublicCall(AMX *amx, const char *name,
    cell *params, cell *retval) {
    if (!ready) {
        return true;
    }
    if (!svr) {
        start_server();
    }

    svr->public_call(amx, name, params, retval);
    return true;
}
