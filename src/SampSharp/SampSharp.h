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

#include <map>
#include <list>
#include <sampgdk/sampgdk.h>
#include <string>
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
    uint32_t handle;
};
typedef std::map<std::string, event_t *> EventMap;

struct samp_timer_t {
    uint32_t handle;
    bool repeating;
};
typedef std::map<int, samp_timer_t> TimerMap;

typedef struct gamemodeimage_t {
    MonoImage *image;
    MonoClass *klass;
} GamemodeImage;

typedef std::list<uint32_t> ExtensionList;

class SampSharp
{
public:
    static void Load(MonoDomain * domain, MonoImage * image, MonoClass *klass);
    static void Unload();
	static void ProcessPublicCall(AMX *amx, const char *name, cell *params,
                                  cell *retval);
    static void SAMPGDK_CALL ProcessTimerTick(int timerid, void *data);
    static void ProcessTick();
 
private:
	static MonoMethod *LoadEvent(const char *name, int param_count);
	static int GetParamLengthIndex(MonoMethod *method, int idx);
    static int CallEvent(MonoMethod *method, uint32_t handle, void **params);
    static bool RegisterExtension(MonoObject *extension);
    static int SetRefTimer(int interval, bool repeat, MonoObject *params);
    static int KillRefTimer(int timerid);
    static bool loaded;
	static MonoDomain *root;
    static GamemodeImage gamemode;
    static GamemodeImage basemode;

	static uint32_t gameModeHandle;
	static EventMap events;
    static TimerMap timers;
    static ExtensionList extensions;
};

