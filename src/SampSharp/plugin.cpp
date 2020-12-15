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
#include "coreclr_app.h"
#include "StringUtil.h"
#include "pipesvr_win32.h"
#include "dsock_unix.h"
#include "tcp_unix.h"
#include "testing.h"
#include <regex>

namespace fs = std::filesystem;

/* amxplugin's reference */
// ReSharper disable once CppInconsistentNaming
extern void *pAMXFunctions;

typedef int(*amx_call)(char *function_name);

extern "C" const AMX_NATIVE_INFO native_list[] = {
#ifndef  DISABLE_TEST_NATIVES
    { "sampsharptest_inout", test_inout },
    { "sampsharptest_inrefout", test_inrefout },
    { "sampsharptest_inoutstr", test_inoutstr },
    { "sampsharptest_inoutarr", test_inoutarr },
#endif
    { NULL, NULL }
};

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
        gamemode,
        skip_empty_check,
        use_multi_process_mode;

    config("skip_empty_check", skip_empty_check);

    if(!StringUtil::ToBool(skip_empty_check, false)) {
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
    }
    
    config("use_multi_process_mode", use_multi_process_mode);

    if(StringUtil::ToBool(use_multi_process_mode, false)) {
        state_set(STATE_CONFIG_VALID);
        return true;
    }
#if SAMPSHARP_WINDOWS
    char* cmdline = GetCommandLineA();
    if(cmdline && strstr(cmdline, "--multi-process-mode")) {
        state_set(STATE_CONFIG_VALID);
        return true;
    }
#endif
    
    config("coreclr", coreclr);
    config("gamemode", gamemode);
    
    // Verify hosting config values.
    
    if(!exists(fs::path(coreclr) / CORECLR_LIB) && !detect_coreclr(coreclr)) {
        log_error("Invalid coreclr directory specified in server.cfg.");

        if(coreclr.length() > 0) {
            if(!is_directory(fs::path(coreclr))) {
                log_error("Directory could not be found.");
            } else if(!exists(fs::path(coreclr) / CORECLR_LIB)) {
                log_error(CORECLR_LIB " could not be found.");
            }
        }
        return false;
    }

    
    if(!exists(fs::path(gamemode)) && !detect_gamemode(gamemode)) {
        log_error("Invalid gamemode specified in server.cfg.");
        return false;
    }

    log_info("Runtime path: %s", coreclr.c_str());
    log_info("Game mode path: %s", gamemode.c_str());

    coreclr_ = coreclr;
    gamemode_ = gamemode;

    state_set(STATE_HOSTED | STATE_CONFIG_VALID);
    return true; 
}

std::string *plugin::get_gamemode() {
    return &gamemode_;
}

std::string *plugin::get_coreclr() {
    return &coreclr_;
}

bool plugin::detect_coreclr(std::string &value, fs::path path) {
    if(!is_directory(path)) {
        return false;
    }

    if(value.length() > 0 && detect_coreclr(value, path / value)) {
        return true;
    }

    log_debug("Checking for runtime in %s...", path.string().c_str());

    // Check for corelib in current directory
    if(exists(path / CORECLR_LIB)) {
        value = absolute(path).string();
        return true;
    }

    // Check for version numbers in subdirectories
    std::regex const regex_version {R"(^([0-9])+\.([0-9])+\.([0-9])+$)"}; 
 
    int best_version_number = 0;
    fs::path best_version;
    for (const auto & entry : fs::directory_iterator(path)) {
        if(!entry.is_directory()) {
            continue;
        }

        fs::path entry_path = entry.path();
        std::string entry_dirname = entry_path.filename().string();
        
        std::smatch match;
        if(std::regex_match(entry_dirname, match, regex_version) && exists(entry_path / CORECLR_LIB)) {
            int version_number = stoi(match[1]) * 1000000 + stoi(match[2]) * 1000 + stoi(match[3]);

            if(version_number > best_version_number) {
                best_version = entry_path;
                best_version_number = version_number;
            }
        }
    }

    // If a version number was found, use this directory
    if(detect_coreclr(value, best_version)) {
        return true;
    }

    // Check every subdirectory
    for (const auto & entry : fs::directory_iterator(path)) {
        if(entry.is_directory() && detect_coreclr(value, entry.path())) {
            return true;
        }
    }

    return false;
}

bool plugin::detect_coreclr(std::string &value) {
    return
        detect_coreclr(value, "runtime") ||
        detect_coreclr(value, "dotnet");
}

bool plugin::detect_gamemode(std::string &value) {
    std::string gamemode_base;
    config("gamemode_base", gamemode_base);

    return
        detect_gamemode(value, gamemode_base) ||
        detect_gamemode(value, "gamemode") ||
        detect_gamemode(value, "gamemodes");
}

bool plugin::detect_gamemode(std::string &value, fs::path path) {
    if(!is_directory(path)) {
        return false;
    }
    
    log_debug("Checking for game mode in %s...", path.string().c_str());

    // Check for runtimeconfigs
    std::vector<fs::path> runtimeconfigs;
    for (const auto & entry : fs::directory_iterator(path)) {
        if(StringUtil::EndsWith(entry.path().string(), ".runtimeconfig.json")) {
            runtimeconfigs.push_back(entry.path());
        }
    }

    if(!runtimeconfigs.empty()) {
        // Find specified game mode file
        if(value.length() > 0) {
           for (auto runtimeconfig : runtimeconfigs) {
               if(StringUtil::ToLower(runtimeconfig.filename().string()) == StringUtil::ToLower(value + ".runtimeconfig.json")) {
                   fs::path gm_path = runtimeconfig.replace_extension().replace_extension(".dll");
                   if(exists(gm_path)) {
                       value = absolute(gm_path).string();
                       return true;
                   }
               }
           }
        }

        // Find the only game mode in directory
        else if(runtimeconfigs.size() == 1)
        {
            fs::path gm_path = runtimeconfigs[0].replace_extension().replace_extension(".dll");

            if(exists(gm_path)) {
                value = absolute(gm_path).string();
                return true;
            }
        }
    }

    // Check for version numbers in subdirectories
    std::regex const regex_version {R"(^netcoreapp([0-9])+\.([0-9])+$)"}; 
 
    int best_version_number = 0;
    fs::path best_version;
    for (const auto & entry : fs::directory_iterator(path)) {
        if(!entry.is_directory()) {
            continue;
        }

        fs::path entry_path = entry.path();
        std::string entry_dirname = entry_path.filename().string();
        
        std::smatch match;
        if(std::regex_match(entry_dirname, match, regex_version)) {
            int version_number = stoi(match[1]) * 1000 + stoi(match[2]);

            if(version_number > best_version_number) {
                best_version = entry_path;
                best_version_number = version_number;
            }
        }
    }

    if(detect_gamemode(value, best_version)) {
        return true;
    }

    // Check any subdirectories
    if(runtimeconfigs.empty()) {
         for (const auto & entry : fs::directory_iterator(path)) {
            if(detect_gamemode(value, entry.path())) {
                return true;
            }
        }

        return false;
    }

    return false;
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

int plugin::amx_load(AMX* amx) {
    return amx_Register(amx, native_list, -1);
}

