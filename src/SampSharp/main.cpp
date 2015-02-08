// SampSharp
// Copyright 2015 Tim Potze
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
#include "Config.h"
#include "MonoRuntime.h"
#include "unicode.h"
#include "monohelper.h"
#include "GameMode.h"
#include <iostream>

extern void *pAMXFunctions;

using std::string;
using std::stringstream;
using sampgdk::logprintf;

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
    if (!sampgdk::Load(ppData)) return false;

    pAMXFunctions = ppData[PLUGIN_DATA_AMX_EXPORTS];
    logprintf("[SampSharp] Loading SampSharp v%s by ikkentim", PLUGIN_VERSION);

    Config::Read();
    return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    GameMode::Unload();
    MonoRuntime::Unload();
    sampgdk::Unload();
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
    GameMode::ProcessTick();
    sampgdk::ProcessTick();
}

void convertSymbols() {
    string symbols = Config::GetSymbolFiles();

    if (symbols.length() > 0) {
        logprintf("[SampSharp] Generating symbol files...");

        stringstream symbols_stream(symbols);
        string file;
        while (std::getline(symbols_stream, file, ' ')) {
            std::ifstream ifile(file.c_str());
            if (file.empty() || !ifile) {
                logprintf("[SampSharp] Processing \"%s\"... File not found!", file.c_str());
                continue;
            }

            logprintf("[SampSharp] Processing \"%s\"...", file.c_str());
            mono_convert_symbols(file.c_str());
        }
        sampgdk::logprintf("[SampSharp] Symbol files generated!\n");
    }
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPublicCall(AMX *amx, const char *name, cell *params, cell *retval) {
    if (!GameMode::IsLoaded() && !strcmp(name, "OnGameModeInit")) {
        /* Load mono */
        if (!MonoRuntime::IsLoaded()) {
            MonoRuntime::Load(Config::GetMonoAssemblyDir(),
                Config::GetMonoConfigDir(), Config::GetTraceLevel(),
                PathUtil::GetPathInBin("gamemode/")
                .append(Config::GetGameModeNameSpace()).append(".dll"));
        }

        /* Set initial codepage to the configured one. */
        int codepage = Config::GetCodepage();
        set_codepage(codepage);

        convertSymbols();

        /* Load game mode */
        if(!GameMode::Load(Config::GetGameModeNameSpace(), 
            Config::GetGameModeClass())) {
            logprintf("[SampSharp] Failed to load game mode.");
        }
    }
    else if (GameMode::IsLoaded() && !strcmp(name, "OnGameModeExit")) {
        GameMode::ProcessPublicCall(amx, name, params, retval);
        GameMode::Unload();
    }

    if (GameMode::IsLoaded()) {
        GameMode::ProcessPublicCall(amx, name, params, retval);
    }

    return true;
}
