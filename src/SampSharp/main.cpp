// SampSharp
// Copyright 2022 Tim Potze
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
#include <sstream>
#include <sampgdk/sampgdk.h>
#include "config_cfg.h"
#include "config_composite.h"
#include "config_omp.h"
#include "config_win.h"
#include "version.h"
#include "logging.h"
#include "testing.h"
#include "interop.h"
#include "nethost_coreclr.h"

nethost *host = nullptr;
bool started = false;

extern void *pAMXFunctions;

void write_empty_gamemode() {
    if(fs::exists("gamemodes/empty.amx")) {
        return;
    }

    log_info("Writing gamemodes/empty.amx to disk");

    if(!fs::exists("gamemodes")) {
        fs::create_directory("gamemodes");
    }

    std::fstream fout;
    fout.open("gamemodes/empty.amx", std::ios::binary | std::ios::out);

    // compiled bytecode for script: main() return;
    const uint8_t empty_amx[] = {
        0x45, 0x00, 0x00, 0x00, 0xE0, 0xF1, 0x08, 0x08, 0x04, 0x00, 0x08, 0x00, 0x3C, 0x00, 0x00, 0x00, 
        0x54, 0x00, 0x00, 0x00, 0x54, 0x00, 0x00, 0x00, 0x54, 0x40, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 
        0x38, 0x00, 0x00, 0x00, 0x38, 0x00, 0x00, 0x00, 0x38, 0x00, 0x00, 0x00, 0x38, 0x00, 0x00, 0x00, 
        0x38, 0x00, 0x00, 0x00, 0x38, 0x00, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x80, 0x78, 0x00, 0x2E, 
        0x81, 0x09, 0x80, 0x59, 0x30};

    fout.write((const char *)empty_amx, sizeof(empty_amx));
    fout.close();
}

bool validate_config(config *cfg) {
    bool skip;
    if(cfg->get_config_bool("skip_empty_check", skip) && skip) {
        return true;
    }
    
    /* check whether gamemodeN values contain acceptable values. */
    for (int i = 0; i < 15; i++) {
        std::ostringstream gamemode_key;
        std::string gamemode_value;

        gamemode_key << "gamemode";
        gamemode_key << i;

        const bool exists = cfg->get_config_string(gamemode_key.str(), gamemode_value);
        const bool is_empty = gamemode_value == "empty" || gamemode_value.rfind("empty ") == 0;

        if (i == 0 && !is_empty) {
            log_error("Can not load sampsharp if a non-SampSharp gamemode is set to load.");
            log_error("Please ensure you set 'gamemode0 empty 1' in your server.cfg file.");
            log_error("To override this behaviour add 'skip_empty_check 1' to your server.cfg file.");
            return false;
        }

        if (i > 0 && exists) {
            log_error("Can not load sampsharp if a non-SampSharp gamemode is set to load.");
            log_error("Please ensure you only specify one script gamemode, namely 'gamemode0"
                "empty 1' in your server.cfg file.");
            log_error("To override this behaviour add 'skip_empty_check 1' to your server.cfg file.");
            return false;
        }
    }

    write_empty_gamemode();

    return true;
}

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK | SUPPORTS_AMX_NATIVES;
}

bool is_open_mp(void **ppData) {
    // open.mp sets plugin data to zero for uninitialized fields. probe a few to make some assumptions.
    // it might however be a better idea to simply check the name of the current process...
    for (int i = 0x02; i < 0x10; i++) {
        if (ppData[i] != nullptr) {
            return false;
        }
    }

    return true;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
    if (!sampgdk::Load(ppData)) {
        return false;
    }

    pAMXFunctions = ppData[PLUGIN_DATA_AMX_EXPORTS];
    sampsharp_api_setup(ppData);

    log_info("v%s, (C)2014-2022 Tim Potze", PLUGIN_VERSION_STR);
    
    config_composite config;

#if SAMPSHARP_WINDOWS
    // decorate config with command line arguments
    config_win config_win;
    config.add_config(&config_win);
#endif

    config_omp config_omp;
    if(is_open_mp(ppData)) {
        log_debug("Adding open.mp config reader");
        config.add_config(&config_omp);
    }

    config_cfg config_cfg;
    config.add_config(&config_cfg);

    locator loc(&config);

    if(!validate_config(&config)) {
        return false;
    }
    
    host = new nethost_coreclr();

    if(!host->setup(&loc, &config)) {
        delete host;
        host = nullptr;
        return false;
    }
    
    return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    delete host;
    host = nullptr;

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
    sampsharp_api_tick();
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPublicCall(AMX *amx, const char *name,
    cell *params, cell *retval) {
    if(!started && host) {
        started = true;
        host->start();
    }
    
    sampsharp_api_public_call(amx, name, params, retval);
    return true;
}
