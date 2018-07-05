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

#include "plugin.h"
#include <sampgdk/sampgdk.h>
#include "platforms.h"
#include "logging.h"
#include "pathutil.h"
#include "coreclr_app.h"
#include "StringUtil.h"
#include "pipesvr_win32.h"
#include "dsock_unix.h"
#include "tcp_unix.h"

/* amxplugin's reference */
// ReSharper disable once CppInconsistentNaming
extern void *pAMXFunctions;

typedef int(*amx_call)(char *function_name);

plugin::plugin(void **pp_data) :
    config_(ConfigReader("server.cfg")) {
    data_ = pp_data;
    pAMXFunctions = pp_data[PLUGIN_DATA_AMX_EXPORTS];
}

ConfigReader *plugin::config() {
    return &config_;
}

int plugin::filterscript_call(const char * function_name) const {
    return ((amx_call)data_[PLUGIN_DATA_CALLPUBLIC_FS])((char *)function_name);
}

void plugin::config(const std::string &name, std::string &value) const {
    config_.GetOptionAsString(name, value);
}

bool plugin::config_validate() {
    std::string
        coreclr,
        coreclr_path,
        gamemode;

    /* check whether gamemodeN values contain acceptable values. */
    for (int i = 0; i < 15; i++) {
        std::ostringstream gamemode_key;
        gamemode_key << "gamemode";
        gamemode_key << i;

        std::string gamemode_value;
        config(gamemode_key.str(), gamemode_value);
        gamemode_value = StringUtil::TrimString(gamemode_value);

        if (i == 0 && gamemode_value.compare("empty 1") != 0) {
            log_error("Can not load sampsharp if a non-SampSharp gamemode is "
                "set to load.");
            log_error("Please ensure you set 'gamemode0 empty 1' in your "
                "server.cfg file.");
            return false;
        }
        if (i > 0 && gamemode_value.length() > 0) {
            log_error("Can not load sampsharp if a non-SampSharp gamemode is "
                "set to load.");
            log_error("Please ensure you only specify one script gamemode, "
                "namely 'gamemode0 empty 1' in your server.cfg file.");
            return false;
        }
    }
    
    config("coreclr", coreclr);
    config("gamemode", gamemode);

    if(coreclr.length() > 0 && gamemode.length() > 0) {
        state_set(STATE_HOSTED);
    }

    if(!(state() & STATE_HOSTED)) {
        state_set(STATE_CONFIG_VALID);
        return true;
    }

    // Verify hosting config values if coreclr has been specified.
    if(!dir_exists(coreclr.c_str())) {
        log_error("Invalid coreclr directory specified in server.cfg.");
        log_error("Directory could not be found.");
        return false;
    }

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
    
    state_set(STATE_CONFIG_VALID);
    return true; 
}

commsvr *plugin::create_commsvr() const {
    std::string 
        type,
        value;

    commsvr *com = NULL;

    if(state() & STATE_HOSTED) {
        return com;
    }

    config("com_type", type);
    
    #if SAMPSHARP_LINUX
    if (!type.compare("tcp")) {
        std::string ip, port;
        config("com_ip", ip);
        config("com_port", port);
        uint16_t portnum = atoi(port.c_str());

        com = new tcp_unix(ip.c_str(), portnum);
    }
    #endif

    if(!com) {
    #if SAMPSHARP_WINDOWS
        config("com_pipe", value);
        com = new pipesvr_win32(value.c_str());
    #elif SAMPSHARP_LINUX
        config("com_dsock", value);
        com = new dsock_unix(value.c_str());
    #endif
    }

    return com;
}

plugin_state plugin::state() const {
    return state_;
}

plugin_state plugin::state_set(const plugin_state flag) {
    return state_ |= flag;
}

plugin_state plugin::state_unset(const plugin_state flag) {
    return state_ &= ~flag;
}

plugin_state plugin::state_reset() {
    return state_ = STATE_NONE;
}
