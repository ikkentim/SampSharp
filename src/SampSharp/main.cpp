// SampSharp
// Copyright 2020 Tim Potze
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
#include "version.h"
#include "plugin.h"
#include "coreclr_app.h"
#include "logging.h"
#include "hosted_server.h"
#include "testing.h"
#include "interop.h"

server *svr = NULL;
plugin *plg = NULL;

void print_info() {
    log_print("");
    log_print("SampSharp Plugin");
    log_print("----------------");
    log_print("v%s, (C)2014-2022 Tim Potze", PLUGIN_VERSION_STR);
    log_print("");
}

void start_server() {
    if(!(plg->state() & STATE_INITIALIZED)) {
        /* workaround for SA-MP error which prevents OnRconCommand from working
         * without a filterscript which implements it
         */
        sampgdk_SendRconCommand("loadfs empty");

        print_info();

        plg->state_set(STATE_INITIALIZED);
    }

    if(svr) {
        delete svr;
        svr = NULL;
    }
    
    svr = new hosted_server(plg->get_coreclr()->c_str(), plg->get_gamemode()->c_str());
}

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK | SUPPORTS_AMX_NATIVES;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
    if (!sampgdk::Load(ppData)) {
        return false;
    }

    sampsharp_api_setup(ppData);

    plg = new plugin(ppData);

    /* validate the server config is fit for running SampSharp */
    return plg && plg->config_validate();
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    delete svr;
    delete plg;
    
    plg = NULL;
    svr = NULL;

    sampsharp_api_cleanup();
    sampgdk::Unload();
}

PLUGIN_EXPORT int PLUGIN_CALL AmxLoad(AMX* amx) {
    return load_test_natives(amx);
}

PLUGIN_EXPORT int PLUGIN_CALL AmxUnload(AMX* amx) {
    return 1;
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
    api_tick();
    /*if (svr) {
        svr->tick();
    }*/
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPublicCall(AMX *amx, const char *name,
    cell *params, cell *retval) {
    if (!plg || !(plg->state() & STATE_CONFIG_VALID)) {
        return true;
    }
    if (!svr) {
        start_server();
    }
    
    api_public_call(amx, name, params, retval);
    /*
    if (svr) {
        svr->public_call(amx, name, params, retval);
    }*/
    return true;
}
