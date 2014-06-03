#include <iostream>

#include <sampgdk/core.h>
#include <sampgdk/a_samp.h>

#include "SampSharp.h"
#include "ConfigReader.h"

#include "amxplugin.cpp"

using namespace std;
using sampgdk::logprintf;

extern void *pAMXFunctions;

PLUGIN_EXPORT unsigned int PLUGIN_CALL Supports() {
	return sampgdk::Supports() | SUPPORTS_PROCESS_TICK;
}

PLUGIN_EXPORT bool PLUGIN_CALL Load(void **ppData) {
	//Load plugin

	pAMXFunctions = ppData[PLUGIN_DATA_AMX_EXPORTS];

	//TODO: should check if ::Load succeeds?
	sampgdk::Load(ppData);

	//Load proxy information from config
	ConfigReader server_cfg("server.cfg");
	string basemode_path = "plugins/SampSharp.GameMode.dll"; 
	string gamemode_path = "plugins/GameMode.dll";
	string gamemode_namespace = "GameMode";
	string gamemode_class = "GameMode";
	bool gamemode_debug = false;

	server_cfg.GetOption("basemode_path", basemode_path);
	server_cfg.GetOption("gamemode_path", gamemode_path);
	server_cfg.GetOption("gamemode_namespace", gamemode_namespace);
	server_cfg.GetOption("gamemode_class", gamemode_class);
	server_cfg.GetOption("gamemode_debug", gamemode_debug);

	//Load Mono
	logprintf("[SampSharp] Loading gamemode: %s::%s at \"%s\".", 
		(char*)gamemode_namespace.c_str(), 
		(char*)gamemode_class.c_str(), 
		(char*)gamemode_path.c_str());

	SampSharp::Load((char *)basemode_path.c_str(),
		(char *)gamemode_path.c_str(),
		(char *)gamemode_namespace.c_str(), 
		(char *)gamemode_class.c_str(), 
		gamemode_debug);

	logprintf("[SampSharp] SampSharp is ready!");
	return true;
}

PLUGIN_EXPORT void PLUGIN_CALL Unload() {
	SampSharp::Unload();
	sampgdk::Unload();
}

PLUGIN_EXPORT void PLUGIN_CALL ProcessTick() {
	sampgdk::ProcessTick();
	SampSharp::CallEvent(SampSharp::onTick, NULL);
}

PLUGIN_EXPORT bool PLUGIN_CALL
OnPublicCall(AMX * amx, const char * name, cell * params)
{
	const int param_count = params[0] / sizeof(cell);

	if (strcmp("OnRconCommand", name) == 0)
	{
		int
			len = NULL;

		cell *addr = NULL;

		amx_GetAddr(amx, params[1], &addr);
		amx_StrLen(addr, &len);

		if (len)
		{
			len++;

			char* text = new char[len];
			amx_GetString(text, addr, 0, len);


			delete[] text;
		}
		return true;
	}

	if (strcmp("OnPlayerCommandText", name) == 0)
	{
		cout << "PCT" << endl;
		return false;
	}
	return false;
}