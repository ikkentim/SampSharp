#include <string>
#include <sampgdk/sampgdk.h>

#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/mono-debug.h>
#include <mono/metadata/debug-helpers.h>

#include "SampSharp.h"
#include "ConfigReader.h"
#include "StringUtil.h"
#include "MonoUtil.h"
#include "PathUtil.h"
#include "Benchmark.h"
#include "amxplugin.cpp"

using sampgdk::logprintf;

extern void *pAMXFunctions;

PLUGIN_EXPORT unsigned int PLUGIN_CALL
Supports() {
	return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL
Load(void **ppData) {
	pAMXFunctions = ppData[PLUGIN_DATA_AMX_EXPORTS];

	if (!sampgdk::Load(ppData)) {
		return false;
	}

	//read config
	ConfigReader server_cfg("server.cfg");
    std::string gamemode = "gamemode/Default.GameMode.dll Default.GameMode:GameMode";
    std::string path, name_space, klass, symbols;

    server_cfg.GetOptionAsString("gamemode", gamemode);
	server_cfg.GetOptionAsString("symbols", symbols);

    std::stringstream gamemode_stream(gamemode);
	std::getline(gamemode_stream, path, ' ');
    std::getline(gamemode_stream, name_space, ':');
    std::getline(gamemode_stream, klass, '\n');

    StringUtil::TrimString(path);
    StringUtil::TrimString(name_space);
    StringUtil::TrimString(klass);

    //init mono
    #ifdef _WIN32
	mono_set_dirs(PathUtil::GetLibDirectory().c_str(), 
        PathUtil::GetConfigDirectory().c_str());
	#endif

	mono_debug_init(MONO_DEBUG_FORMAT_MONO);
	MonoDomain *root = mono_jit_init(PathUtil::GetPathInBin(path).c_str());

    //generate symbol files
    #ifdef _WIN32
    if(symbols.length() > 0) {
	    logprintf("[SampSharp] Generating symbol files");
        
        std::stringstream symbols_stream(symbols);
	    std::string file;
	    while (std::getline(symbols_stream, file, ' ')) {
            if(file.length() > 0) {
                logprintf("[SampSharp] Processing \"%s\"...", file.c_str());
                MonoUtil::GenerateSymbols(file.c_str());
            }
	    }
        sampgdk::logprintf("[SampSharp] Done!\n");
    }
	#endif

    //load gamemode
	logprintf("[SampSharp] Loading gamemode: %s::%s from \"%s\".", 
		(char *)name_space.c_str(), 
		(char *)klass.c_str(), 
		(char *)path.c_str());

    MonoImage *image = mono_assembly_get_image(
        mono_assembly_open(PathUtil::GetPathInBin(path).c_str(), NULL));
    SampSharp::Load(root, image,
        mono_class_from_name(image, name_space.c_str(), klass.c_str()));

	logprintf("[SampSharp] SampSharp is ready!\n");
	return true;
}

PLUGIN_EXPORT void PLUGIN_CALL
Unload() {
	SampSharp::Unload();
	sampgdk::Unload();
}

PLUGIN_EXPORT void PLUGIN_CALL
ProcessTick() {
    SampSharp::ProcessTick();
	sampgdk::ProcessTick();
}

PLUGIN_EXPORT bool PLUGIN_CALL
OnPublicCall(AMX *amx, const char *name, cell *params, cell *retval) {
    #ifdef DO_BENCHMARK
    if(!strcmp(name, "OnGameModeInit")) {
        Benchmark();
    }
    #endif
	return SampSharp::ProcessPublicCall(amx, name, params, retval);
}
