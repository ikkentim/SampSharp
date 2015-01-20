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
#include <string>
#include <sampgdk/sampgdk.h>
#include <fstream>
#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/mono-debug.h>
#include <mono/metadata/debug-helpers.h>
#include <mono/utils/mono-logger.h>

#include "SampSharp.h"
#include "ConfigReader.h"
#include "StringUtil.h"
#include "MonoUtil.h"
#include "PathUtil.h"
#include "unicode.h"

extern void *pAMXFunctions;

using sampgdk::logprintf;
using std::string;
using std::stringstream;

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

bool fexists(const char *filename) {
    std::ifstream ifile(filename);
    return !!ifile;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {


    int codepage = 1250; /* codepage to use for encoding */
    string gamemode = "TestMode:GameMode"; /* gamemode to start */
    string trace_level = "error"; /* mono tracelevel */
    string symbols; /* string of libraties to convert symbols files for */
    string mono_assembly_dir; /* path to mono assembly directory */
    string mono_config_dir; /* path to mono config directory */


    if (!sampgdk::Load(ppData)) return false;

    pAMXFunctions = ppData[PLUGIN_DATA_AMX_EXPORTS];
    logprintf("[SampSharp] Loading SampSharp v%s by ikkentim", PLUGIN_VERSION);


    /* Read configuration file.
     */
    ConfigReader server_cfg("server.cfg");

    server_cfg.GetOptionAsString("gamemode", gamemode);
    server_cfg.GetOptionAsString("symbols", symbols);
    server_cfg.GetOptionAsString("trace_level", trace_level);
    server_cfg.GetOptionAsString("mono_assembly_dir", mono_assembly_dir);
    server_cfg.GetOptionAsString("mono_config_dir", mono_config_dir);
    server_cfg.GetOption("codepage", codepage);

    stringstream gamemode_stream(gamemode);

    string name_space;
    std::getline(gamemode_stream, name_space, ':');
    StringUtil::TrimString(name_space);

    string klass;
    std::getline(gamemode_stream, klass, '\n');
    StringUtil::TrimString(klass);


    string gamemode_path = PathUtil::GetPathInBin("gamemode/");
    string library_path = PathUtil::GetPathInBin("gamemode/").append(name_space).append(".dll");
    string config_path = PathUtil::GetPathInBin("gamemode/").append(name_space).append(".dll.config");


    /* Validate gamemode path.
     */

    if (!fexists(library_path.c_str())) {
        logprintf("[SampSharp] File \"%s\" not found!", library_path.c_str());
        return true;
    }

    /* Initialize the mono runtime.
     */
    if (!mono_assembly_dir.empty() && !mono_config_dir.empty()) {
        logprintf("[SampSharp] Loading mono from %s, %s", mono_assembly_dir.c_str(), mono_config_dir.c_str());
        mono_set_dirs(mono_assembly_dir.c_str(), mono_config_dir.c_str());
    }
    #ifdef _WIN32
    else {
        mono_set_dirs(PathUtil::GetLibDirectory().c_str(),
            PathUtil::GetConfigDirectory().c_str());
    }
    #endif

    mono_trace_set_level_string(trace_level.c_str());
    mono_debug_init(MONO_DEBUG_FORMAT_MONO);
    set_codepage(codepage);

    MonoDomain *root = mono_jit_init(library_path.c_str());

    /* Convert symbols files.
     */
    if(symbols.length() > 0) {
        logprintf("[SampSharp] Generating symbol files...");
        
        stringstream symbols_stream(symbols);
        string file;
        while (std::getline(symbols_stream, file, ' ')) {
            if(file.empty() || !fexists(file.c_str())) {
                    logprintf("[SampSharp] Processing \"%s\"... File not found!", file.c_str());
                    continue;
                }

                logprintf("[SampSharp] Processing \"%s\"...", file.c_str());
                MonoUtil::GenerateSymbols(file.c_str());
        }
        sampgdk::logprintf("[SampSharp] Symbol files generated!\n");
    }

    /* Load gamemode image.
     */
    logprintf("[SampSharp] Loading gamemode: %s::%s.", name_space.c_str(), klass.c_str());

    mono_domain_set_config(mono_domain_get(), gamemode_path.c_str(), config_path.c_str());

    MonoImage *image = mono_assembly_get_image(mono_assembly_open(library_path.c_str(), NULL));
    MonoClass *class_from_name = mono_class_from_name(image, name_space.c_str(), klass.c_str());

    if (!class_from_name) {
        logprintf("[SampSharp] %s::%s was not found inside image.", name_space.c_str(), klass.c_str());
        return true;
    }

    SampSharp::Load(root, image, class_from_name);

    return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    SampSharp::Unload();

    mono_jit_cleanup(mono_domain_get());

    sampgdk::Unload();

}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
    SampSharp::ProcessTick();
    sampgdk::ProcessTick();
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPublicCall(AMX *amx, const char *name, cell *params, cell *retval) {
    SampSharp::ProcessPublicCall(amx, name, params, retval);

    return true;
}
