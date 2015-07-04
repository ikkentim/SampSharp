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

#include <string>
#include <map>
#include <vector>
#include <mono/jit/jit.h>
#include <mono/metadata/metadata.h>
#include <sampgdk/sampgdk.h>

#pragma once

#define BASEMODE_NAMESPACE                  "SampSharp.GameMode"
#define BASEMODE_CLASS                      "BaseMode"

#define PARAM_LENGTH_ATTRIBUTE_NAMESPACE    "SampSharp.GameMode"
#define PARAM_LENGTH_ATTRIBUTE_CLASS        "ParameterLengthAttribute"

#define MAX_CALLBACK_PARAM_COUNT            (16)
#define MAX_NATIVE_NAME_LEN                 (32)
#define MAX_NATIVE_ARGS                     (32)
#define MAX_NATIVE_ARG_FORMAT_LEN           (8)

class GameMode {
private:
    enum ParameterType {
        PARAM_INVALID,
        PARAM_INT,
        PARAM_FLOAT,
        PARAM_BOOL,
        PARAM_STRING,
        PARAM_INT_ARRAY,
        PARAM_FLOAT_ARRAY,
        PARAM_BOOL_ARRAY
    };
    struct ParameterSignature {
        ParameterType type;
        int length_idx;
    };
    typedef std::map<int, ParameterSignature> ParameterMap;
    struct CallbackSignature {
        MonoMethod *method;
        ParameterMap params;
        uint32_t handle;
    };
    typedef std::map<std::string, CallbackSignature *> CallbackMap;
    struct NativeSignature {
        char name[MAX_NATIVE_NAME_LEN];
        char format[MAX_NATIVE_ARGS * MAX_NATIVE_ARG_FORMAT_LEN];
        char parameters[MAX_NATIVE_ARGS];
        int sizes[MAX_NATIVE_ARGS];
        int param_count;
        AMX_NATIVE native;
    };
    typedef std::vector<NativeSignature> NativeList;
    struct GameModeImage {
        MonoImage *image;
        MonoClass *klass;
    };
    struct RefTimer {
        uint32_t handle;
        bool repeating;
    };
    typedef std::map<int, RefTimer> TimerMap;
    typedef std::vector<uint32_t> ExtensionList;

public:
    static bool Load(std::string namespaceName, std::string className);
    static bool Unload();
    static void ProcessTick();
    static void ProcessPublicCall(AMX *amx, const char *name, cell *params,
        cell *retval);
    static bool IsLoaded() {
        return isLoaded_;
    }

private:
    static bool isLoaded_;
    static unsigned long threadId_;

    static TimerMap timers_;
    static ExtensionList extensions_;
    static CallbackMap callbacks_;
    static NativeList natives_;

    static MonoDomain *domain_;
    static GameModeImage gameMode_;
    static GameModeImage baseMode_;
    static uint32_t gameModeHandle_;

    static MonoMethod *onCallbackException_;
    static MonoMethod *tickMethod_;
    static MonoClass *paramLengthClass_;
    static MonoMethod *paramLengthGetMethod_;

private:
    static void SAMPGDK_CALL ProcessTimerTick(int timerid, void *data);
    static void AddInternalCall(const char * name, const void * method);
    static MonoMethod *LoadEvent(const char *name, int param_count);
    static int GetParamLengthIndex(MonoMethod *method, int idx);
    static int CallEvent(MonoMethod *method, uint32_t handle, void **params);
    static ParameterType GetParameterType(MonoType *type);
    static bool IsMethodValidCallback(MonoImage *image, MonoMethod *method,
        int param_count);
    static MonoMethod *FindMethodForCallbackInClass(const char *name,
        int param_count, MonoClass *klass);
    static MonoMethod *FindMethodForCallback(const char *name,
        int param_count, uint32_t &handle);
    static void PrintException(const char *methodname, MonoObject *exception);

private:
    /* API functions. */
    static bool RegisterExtension(MonoObject *extension);
    static bool IsMainThread();

    static int SetRefTimer(int interval, bool repeat, MonoObject *params);
    static bool KillRefTimer(int id);

    static int LoadNative(MonoString *name, MonoString *format,
        MonoArray *sizes_array);
    static int InvokeNative(int handle, MonoArray *arguments);
    static float InvokeNativeFloat(int handle, MonoArray *arguments);
    static bool NativeExists(MonoString *name);

};
