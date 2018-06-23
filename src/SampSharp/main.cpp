// SampSharp
// Copyright 2018 Tim Potze
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
#include "StringUtil.h"
#include "remote_server.h"
#include "version.h"
#include "pipesvr_win32.h"
#include "dsock_unix.h"
#include "tcp_unix.h"
#include "plugin.h"
#include "coreclr_app.h"
#include "pathutil.h"
#include "logging.h"
#include "hosted_server.h"


using sampgdk::logprintf;

server *svr = NULL;
commsvr *com = NULL;
plugin *plg = NULL;
std::string coreclr;
std::string gamemode;

bool ready;

void print_info() {
    print("");
    print("SampSharp Plugin");
    print("----------------");
    print("v%s, (C)2014-2018 Tim Potze", PLUGIN_VERSION_STR);
    print("");
}

bool config_validate() {
    /* check whether gamemodeN values contain acceptable values. */
    for (int i = 0; i < 15; i++) {
        std::ostringstream gamemode_key;
        gamemode_key << "gamemode";
        gamemode_key << i;

        std::string gamemode_value;
        plg->config()->GetOptionAsString(gamemode_key.str(), gamemode_value);
        gamemode_value = StringUtil::TrimString(gamemode_value);

        if (i == 0 && gamemode_value.compare("empty 1") != 0) {
            log_error("Can not load sampsharp if a non-SampSharp gamemode is "
                "set to load.");
            log_error("Please ensure you set 'gamemode0 empty 1' in your "
                "server.cfg file.");
            return false;
        }
        else if (i > 0 && gamemode_value.length() > 0) {
            log_error("Can not load sampsharp if a non-SampSharp gamemode is "
                "set to load.");
            log_error("Please ensure you only specify one script gamemode, "
                "namely 'gamemode0 empty 1' in your server.cfg file.");
            return false;
        }
    }
    
    plg->config()->GetOptionAsString("coreclr", coreclr);
    plg->config()->GetOptionAsString("gamemode", gamemode);

    if(coreclr.length() <= 0) {
        return true;
    }

    // Verify hosting config values if coreclr has been specified.
    if(!dir_exists(coreclr.c_str())) {
        log_error("Invalid coreclr directory specified in server.cfg.");
        log_error("Directory could not be found.");
        return false;
    }

    std::string coreclr_path;
    path_append(coreclr.c_str(), CORECLR_LIB, coreclr_path);

    if(!file_exists(coreclr_path.c_str())) {
        log_error("Invalid coreclr directory specified in server.cfg.");
        log_error(CORECLR_LIB " could not be found.");
        return false;
    }

    if(!file_exists(gamemode.c_str())) {
        log_error("Invalid gamemode specified in server.cfg.");
        log_error("File could not be found.");
        return false;
    }
    return true;
}

#if SAMPSHARP_WINDOWS
void com_pipe() {
    std::string value;
    plg->config()->GetOptionAsString("com_pipe", value);
    com = new pipesvr_win32(value.c_str());
}

void com_tcp() {
    // TODO: Not yet implemented
    com_pipe();
}
#elif SAMPSHARP_LINUX
void com_dsock() {
    std::string value;
    plg->config()->GetOptionAsString("com_dsock", value);
    com = new dsock_unix(value.c_str());
}

void com_tcp() {
    std::string ip, port;
    plg->config()->GetOptionAsString("com_ip", ip);
    plg->config()->GetOptionAsString("com_port", port);
    uint16_t portnum = atoi(port.c_str());

    com = new tcp_unix(ip.c_str(), portnum);
}
#endif

void start_server() {

    // Workaround for SA-MP bug which prevents OnRconCommand from working
    // without a filterscript which implements it.
    sampgdk_SendRconCommand("loadfs empty");

    print_info();

    if(coreclr.length() > 0) {
        svr = new hosted_server(coreclr.c_str(), gamemode.c_str());
    }
    else {
        std::string type;
        std::string debug_str;

        plg->config()->GetOptionAsString("com_type", type);
        plg->config()->GetOptionAsString("com_debug", debug_str);

    #if SAMPSHARP_WINDOWS
        com_pipe();
    #elif SAMPSHARP_LINUX
        com_dsock();
    #endif
        

        if (!type.compare("tcp")) {
            com_tcp();
        }
    #if SAMPSHARP_WINDOWS
        else if (!type.compare("pipe")) {
            com_pipe();
        }
    #elif SAMPSHARP_LINUX
        else if (!type.compare("dsock")) {
            com_dsock();
        }
    #endif

        bool debug = debug_str == "1" || debug_str == "true";

        if (com) {
            svr = new remote_server(plg, com, debug);
        }
    }
}

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
    if (!sampgdk::Load(ppData)) {
        return false;
    }

    plg = new plugin(ppData);

    /* validate the server config is fit for running SampSharp */
    if (!plg || !((ready = config_validate()))) {
        return ready = false;
    }

    return ready = true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    sampgdk_Unload();

    if (svr) {
        logprintf("Shutting down SampSharp server...");
        delete svr;
    }
    if (com) {
        com->disconnect();
        delete com;
    }

    delete plg;

    plg = NULL;
    svr = NULL;
    com = NULL;
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
    if (svr) {
        svr->public_call(amx, name, params, retval);
    }
    return true;
}
