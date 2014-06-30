#pragma once

#include <iostream>
#include <fstream>
#include <cstring> //strcpy
#include <map>
#include <time.h>

#include <sampgdk/sdk.h>
#include <plugincommon.h>

#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/mono-debug.h>
#include <mono/metadata/debug-helpers.h>

using namespace std;

struct event_t {
	MonoMethod *method;
	string format;
};
typedef map<string, event_t *> EventMap;

class SampSharp
{
public:
	static void Load(string baseModePath, string gamemodePath, string gameModeNamespace, string gameModeClass, bool debug);
	static void Unload();
	static int CallEvent(MonoMethod *method, void ** params);
	static bool HandleEvent(AMX *amx, const char *name, cell *params, cell *retval);

	static MonoMethod *onTimerTick;
	static MonoMethod *onTick;
 
private:
	static MonoMethod *LoadEvent(const char *cname, const char *name);
	#ifdef _WIN32
	static void GenerateSymbols(string path);
	#endif

	static MonoDomain *rootDomain;

	static MonoImage *gameModeImage;
	static MonoImage *baseModeImage;

	static MonoClass *gameModeClassType;
	static MonoClass *baseModeClassType;

	static uint32_t gameModeHandle;

	static EventMap events;
};

