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
#include "remote_server.h"
#include "version.h"
#include "plugin.h"
#include "coreclr_app.h"
#include "logging.h"
#include "hosted_server.h"

server *svr = NULL;
commsvr *com = NULL;
plugin *plg = NULL;

void print_info() {
    log_print("");
    log_print("SampSharp Plugin");
    log_print("----------------");
    log_print("v%s, (C)2014-2020 Tim Potze", PLUGIN_VERSION_STR);
    log_print("");
}

void print_deprecation_warning() {
    log_print("--------- NOTICE -----------");
    log_print("SampSharp is currently running in development mode (also known as multi-process");
    log_print("mode). The development mode has been deprecated and will be removed in a future");
    log_print(" version of SampSharp. See https://github.com/ikkentim/SampSharp/issues/374 for");
    log_print(" more information about this change.");
    log_print("----------------------------");
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

    if(plg->state() & STATE_HOSTED) {
        svr = new hosted_server(plg->get_coreclr()->c_str(), plg->get_gamemode()->c_str());
    }
    else {
        print_deprecation_warning();

        com = plg->create_commsvr();
        
        if (com) {
            std::string debug_str;

            plg->config("com_debug", debug_str);
            svr = new remote_server(plg, com, debug_str == "1" || debug_str == "true");
        }
    }
}

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK | SUPPORTS_AMX_NATIVES;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
    if (!sampgdk::Load(ppData)) {
        return false;
    }

    plg = new plugin(ppData);

    /* validate the server config is fit for running SampSharp */
    return plg && plg->config_validate();
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    if (com) {
        com->disconnect();
    }
    
    delete svr;
    delete com;
    delete plg;
    
    plg = NULL;
    svr = NULL;
    com = NULL;
    
    sampgdk::Unload();
}

PLUGIN_EXPORT int PLUGIN_CALL AmxLoad(AMX* amx) {
    if(plg) {
        return plg->amx_load(amx);
    }

    return 1;
}

PLUGIN_EXPORT int PLUGIN_CALL AmxUnload(AMX* amx) {
    return 1;
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
    if (svr) {
        svr->tick();
    }
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPublicCall(AMX *amx, const char *name,
    cell *params, cell *retval) {
    if (!plg || !(plg->state() & STATE_CONFIG_VALID)) {
        return true;
    }
    if (!svr &&
        (plg->state() & (STATE_HOSTED | STATE_SWAPPING)) !=
        (STATE_HOSTED | STATE_SWAPPING)) {
        start_server();
    }

    if (svr) {
        svr->public_call(amx, name, params, retval);
    }
    return true;
}
