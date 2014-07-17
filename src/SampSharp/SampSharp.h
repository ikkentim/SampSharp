#include <map>
#include <list>
#include <sampgdk/export.h>
#include <mono/jit/jit.h>

#pragma once

#define BASEMODE_NAMESPACE "SampSharp.GameMode"
#define BASEMODE_CLASS "BaseMode"
#define PARAM_LENGTH_ATTRIBUTE_NAMESPACE "SampSharp.GameMode"
#define PARAM_LENGTH_ATTRIBUTE_CLASS "ParameterLengthAttribute"

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
typedef std::map<int, param_t *> ParamMap;

struct event_t {
	MonoMethod *method;
	ParamMap params;
};
typedef std::map<std::string, event_t *> EventMap;

typedef struct gamemodeimage_t {
    MonoImage *image;
    MonoClass *klass;
} GamemodeImage;

typedef std::list<MonoObject *> ExtensionList;

class SampSharp
{
public:
    static void Load(const char *basemode_path, const char *gamemode_path,
                     const char *gamemode_namespace,
                     const char *gamemode_class, bool debug);
    static void Unload();
	static bool ProcessPublicCall(AMX *amx, const char *name, cell *params,
                                  cell *retval);
    static void SAMPGDK_CALL ProcessTimerTick(int timerid, void *data);
    static void ProcessTick();
 
private:
	static MonoMethod *LoadEvent(const char *name, int param_count);
	static int GetParamLengthIndex(MonoMethod *method, int idx);
    static int CallEvent(MonoMethod *method, void **params);
    static bool RegisterExtension(MonoObject *extension);
	static MonoDomain *root;
    static GamemodeImage gamemode;
    static GamemodeImage basemode;

	static uint32_t gameModeHandle;
	static EventMap events;
    static ExtensionList extensions;
};

