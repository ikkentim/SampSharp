#include <iostream>
#include <sampgdk/core.h>
#include <sampgdk/a_samp.h>

#include "SampSharp.h"
#include "ConfigReader.h"

using namespace std;

static ThisPlugin sampSharpPlugin;

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
	return SUPPORTS_VERSION | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
	//Load plugin
	if (sampSharpPlugin.Load(ppData) < 0)
		return false;

	//Load proxy information from config
	ConfigReader server_cfg("server.cfg");
	string basemode_path = "plugins/GameMode.dll"; 
	string gamemode_path = "plugins/GameMode.dll";
	string gamemode_namespace = "GameMode";
	string gamemode_class = "BaseMode";
	bool gamemode_generate_symbols = false;

	server_cfg.GetOption("basemode_path", basemode_path);
	server_cfg.GetOption("gamemode_path", gamemode_path);
	server_cfg.GetOption("gamemode_namespace", gamemode_namespace);
	server_cfg.GetOption("gamemode_class", gamemode_class);
	server_cfg.GetOption("gamemode_generate_symbols", gamemode_generate_symbols);

	//Load Mono
	char * runtime_ver = "v4.0.30319";
	ServerLog::Printf("[SampSharp] Loading gamemode: %s::%s at \"%s\".", 
		(char*)gamemode_namespace.c_str(), 
		(char*)gamemode_class.c_str(), 
		(char*)gamemode_path.c_str());

	CSampSharp::instance = new CSampSharp((char *)basemode_path.c_str(),
		(char *)gamemode_path.c_str(),
		(char *)gamemode_namespace.c_str(), 
		(char *)gamemode_class.c_str(), 
		runtime_ver,
		gamemode_generate_symbols);

	ServerLog::Printf("[SampSharp] Running Mono runtime version %s.", runtime_ver);
	return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
	delete CSampSharp::instance;
	sampSharpPlugin.Unload();
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
	sampSharpPlugin.ProcessTimers();
}