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

enum paramtypes_t { 
	PARAM_INT,
	PARAM_FLOAT,
	PARAM_BOOL,
	PARAM_STRING,
	PARAM_INT_ARRAY,
	PARAM_FLOAT_ARRAY,
	PARAM_BOOL_ARRAY
};
struct param_t {
	paramtypes_t type;
	int length_idx;
};
typedef map<int, param_t *> ParamMap;

struct event_t {
	MonoMethod *method;
	ParamMap params;
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

	static int SampSharp::GetParamLengthIndex(MonoMethod *method, int idx);
    static void SampSharp::Test(MonoArray *arr);
    static cell SampSharp::CallNativeArray(MonoString *name, MonoString *format, MonoArray *args);

	static MonoDomain *rootDomain;

	static MonoImage *gameModeImage;
	static MonoImage *baseModeImage;

	static MonoClass *gameModeClassType;
	static MonoClass *baseModeClassType;
	static MonoClass *parameterLengthAttributeClassType;
	
	static MonoMethod *parameterLengthAttributeGetIndexMethod;

	static uint32_t gameModeHandle;

	static EventMap events;
};

