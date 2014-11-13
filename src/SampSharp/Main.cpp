#include "Main.h"
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
#include "Benchmark.h"
#include "unicode.h"

extern void *pAMXFunctions;

using sampgdk::logprintf;


PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
    return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

bool fexists(const char *filename)
{
    std::ifstream ifile(filename);
    return !!ifile;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {

    /*
    * Loading sampgdk
    */
    if (!sampgdk::Load(ppData)) {
        return false;
    }

    pAMXFunctions = ppData[PLUGIN_DATA_AMX_EXPORTS];

    logprintf("[SampSharp] Loading SampSharp v%s", PLUGIN_VERSION);

    /*
    * Reading configuration file
    */

    ConfigReader server_cfg("server.cfg");
    int codepage = 1250;
    std::string gamemode = "TestMode:GameMode";
    std::string trace_level = "error";
    std::string 
        gamemode_path,
        library_path,
        config_path,
        name_space, 
        klass, 
        symbols;

    //Read configuration
    server_cfg.GetOptionAsString("gamemode", gamemode);
    server_cfg.GetOptionAsString("symbols", symbols);
    server_cfg.GetOptionAsString("trace_level", trace_level);
    server_cfg.GetOption("codepage", codepage);

    std::stringstream gamemode_stream(gamemode);
    
    
    std::getline(gamemode_stream, name_space, ':');
    std::getline(gamemode_stream, klass, '\n');

    StringUtil::TrimString(library_path);
    StringUtil::TrimString(name_space);
    StringUtil::TrimString(klass);

    gamemode_path = PathUtil::GetPathInBin("gamemode/");
    library_path = PathUtil::GetPathInBin("gamemode/").append(name_space).append(".dll");
    config_path = PathUtil::GetPathInBin("gamemode/").append(name_space).append(".dll.config");

    set_codepage(codepage);

    /*
    * Filecheck
    */

    if (!fexists(library_path.c_str()))
    {
        logprintf("[SampSharp] File \"%s\" not found!", library_path.c_str());
        return true;
    }

    /*
    * Loading mono
    */

    #ifdef _WIN32
    mono_set_dirs(PathUtil::GetLibDirectory().c_str(), 
        PathUtil::GetConfigDirectory().c_str());
    #endif

    mono_trace_set_level_string(trace_level.c_str());
    mono_debug_init(MONO_DEBUG_FORMAT_MONO);

    MonoDomain *root = mono_jit_init(library_path.c_str());
    /*
    * Symbol generation
    */

    #ifdef _WIN32
    if(symbols.length() > 0) {
        logprintf("[SampSharp] Generating symbol files...");
        
        std::stringstream symbols_stream(symbols);
        std::string file;
        while (std::getline(symbols_stream, file, ' ')) {
            if(file.length() > 0) {
                if (fexists(file.c_str()))
                {
                    logprintf("[SampSharp] Processing \"%s\"...", file.c_str());
                    MonoUtil::GenerateSymbols(file.c_str());
                }
                else
                {
                    logprintf("[SampSharp] Processing \"%s\"... File not found!", file.c_str());
                }
            }
        }
        sampgdk::logprintf("[SampSharp] Symbol files generated!\n");
    }
    #endif

    /*
    * Loading gamemode
    */


    char *namespace_ctr = (char *)name_space.c_str();
    char *klass_ctr = (char *)klass.c_str();
    char *path_ctr = (char *)library_path.c_str();

    logprintf("[SampSharp] Loading gamemode: %s::%s.", namespace_ctr, klass_ctr);

    mono_domain_set_config(mono_domain_get(), gamemode_path.c_str(), config_path.c_str());

    MonoImage *image = mono_assembly_get_image(mono_assembly_open(library_path.c_str(), NULL));
    MonoClass *class_from_name = mono_class_from_name(image, namespace_ctr, klass_ctr);

    if (!class_from_name)
    {
        logprintf("[SampSharp] %s::%s was not found inside \"%s\".", namespace_ctr, klass_ctr, path_ctr);
        return true;
    }

    SampSharp::Load(root, image, class_from_name);

    return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
    SampSharp::Unload();
    sampgdk::Unload();
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
    SampSharp::ProcessTick();
    sampgdk::ProcessTick();
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPublicCall(AMX *amx, const char *name, cell *params, cell *retval) {
    #ifdef DO_BENCHMARK
    if(!strcmp(name, "OnGameModeInit")) {
        Benchmark();
    }
    #endif

    SampSharp::ProcessPublicCall(amx, name, params, retval);

    return true;
}
