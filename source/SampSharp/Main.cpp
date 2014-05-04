#include <iostream>
#include <sampgdk/core.h>
#include <sampgdk/a_samp.h>

#include "SampSharp.h"
#include "ConfigReader.h"

using namespace std;
using sampgdk::logprintf;

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
	return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
	//Load plugin
	//TODO: should check if ::Load succeeds?
	sampgdk::Load(ppData);

	//Load proxy information from config
	ConfigReader server_cfg("server.cfg");
	string basemode_path = "plugins\\SampSharp.GameMode.dll"; 
	string gamemode_path = "plugins\\GameMode.dll";
	string gamemode_namespace = "GameMode";
	string gamemode_class = "GameMode";
	bool gamemode_generate_symbols = false;

	server_cfg.GetOption("basemode_path", basemode_path);
	server_cfg.GetOption("gamemode_path", gamemode_path);
	server_cfg.GetOption("gamemode_namespace", gamemode_namespace);
	server_cfg.GetOption("gamemode_class", gamemode_class);
	server_cfg.GetOption("gamemode_generate_symbols", gamemode_generate_symbols);

	//Load Mono
	logprintf("[SampSharp] Loading gamemode: %s::%s at \"%s\".", 
		(char*)gamemode_namespace.c_str(), 
		(char*)gamemode_class.c_str(), 
		(char*)gamemode_path.c_str());

	CSampSharp::instance = new CSampSharp((char *)basemode_path.c_str(),
		(char *)gamemode_path.c_str(),
		(char *)gamemode_namespace.c_str(), 
		(char *)gamemode_class.c_str(), 
		gamemode_generate_symbols);

	logprintf("[SampSharp] SampSharp is ready!");
	return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
	delete CSampSharp::instance;
	sampgdk::Unload();
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
	sampgdk::ProcessTick();
	CSampSharp::instance->CallCallback(CSampSharp::instance->onTick, NULL);
}