#include <iostream>
#include <sampgdk/core.h>
#include <sampgdk/a_samp.h>

#include "MonoProxy.h"
#include "ConfigReader.h"

static ThisPlugin proxyPlugin;

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
	return SUPPORTS_VERSION | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
	//Load plugin
	if (proxyPlugin.Load(ppData) < 0)
		return false;

	//Load proxy information from config
	ConfigReader server_cfg("server.cfg");
	std::string gamemode_path = "plugins/GameMode.dll";
	std::string gamemode_namespace = "GameMode";
	std::string gamemode_class = "Server";

	server_cfg.GetOption("gamemode_path", gamemode_path);
	server_cfg.GetOption("gamemode_namespace", gamemode_namespace);
	server_cfg.GetOption("gamemode_class", gamemode_class);

	//Load Mono
	char * runtime_ver = "v4.0.30319";
	ServerLog::Printf("[monoproxy] Loading gamemode: %s::%s at \"%s\".", 
		(char*)gamemode_namespace.c_str(), 
		(char*)gamemode_class.c_str(), 
		(char*)gamemode_path.c_str());
	CMonoProxy::p_instance = new CMonoProxy((char*)gamemode_path.c_str(),  
		(char*)gamemode_namespace.c_str(), 
		(char*)gamemode_class.c_str(), 
		runtime_ver);
	ServerLog::Printf("[monoproxy] Running Mono runtime version %s.", runtime_ver);
	return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
	delete CMonoProxy::p_instance;
	
	proxyPlugin.Unload();
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
	proxyPlugin.ProcessTimers();
}