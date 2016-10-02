// SampSharp
// Copyright 2016 Tim Potze
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

#define PARAM_LENGTH_ATTRIBUTE_NAMESPACE    "SampSharp.GameMode.API"
#define PARAM_LENGTH_ATTRIBUTE_CLASS        "ParameterLengthAttribute"

#define MAX_CALLBACK_PARAM_COUNT            (16)
#define MAX_NATIVE_NAME_LEN                 (32)
#define MAX_NATIVE_ARGS                     (32)
#define MAX_NATIVE_ARG_FORMAT_LEN           (8)

class GameMode {
    /* Public functions. */
public:
    /* Loads the game mode with the specified namespace class names. */
    static bool Load(std::string namespaceName, std::string className);
    /* Unloads the loaded game mode. */
    static bool Unload();
    /* Processes a server tick. */
    static void ProcessTick();
    /* Processes a public call. */
    static void ProcessPublicCall(AMX *amx, const char *name, cell *params,
        cell *retval);
    /* Gets a value indicating whether a game mode was loaded.*/
    static bool IsLoaded() {
        return isLoaded_;
    }

    /* Internal types. */
private:
    /* Enum of supported parameter types. */
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
    /* Represents a parameter signature of a callback. */
    struct ParameterSignature {
        ParameterType type;
        int length_idx;
    };
    /* Holds a collection of parameters. */
    typedef std::map<int, ParameterSignature> ParameterMap;
    // TODO: ParameterMap can be a vector. The key simply holds the index (0..).
    /* Represents a callback signature. */
    struct CallbackSignature {
        MonoMethod *method;
        ParameterMap params;
        uint32_t handle;
    };
    /* Holds a collection of callbacks. */
    typedef std::map<std::string, CallbackSignature *> CallbackMap;
    /* Represents a signature of a native function. */
    struct NativeSignature {
        char name[MAX_NATIVE_NAME_LEN];
        char format[MAX_NATIVE_ARGS * MAX_NATIVE_ARG_FORMAT_LEN];
        char parameters[MAX_NATIVE_ARGS];
        int sizes[MAX_NATIVE_ARGS];
        int param_count;
        AMX_NATIVE native;
    };
    /* Holds a collection of native function signatures. */
    typedef std::vector<NativeSignature> NativeList;
    struct GameModeImage {
        MonoImage *image;
        MonoClass *klass;
    };
    /* Represents a reference to a timer. */
    struct RefTimer {
        uint32_t handle;
        bool repeating;
    };
    /* Holds a collection of timer references. */
    typedef std::map<int, RefTimer> TimerMap;
    /* Holds a collection of handles of extensions. */
    typedef std::vector<uint32_t> ExtensionList;

    /* Fields */
private:
    static bool isLoaded_;
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
    static int bootSequenceNumber_;
    static MonoDomain *previousDomain_;
    static MonoAssembly *assemby_;
    static std::map<uint16_t, uint16_t> cptouni_;
    static std::map<uint16_t, uint16_t> unitocp_;
    static bool cpwide_[256];


    /* Internal gamemode functions. */
private:
    /* Processes a timer tick. */
    static void SAMPGDK_CALL ProcessTimerTick(int timerid, void *data);
    /* Adds an internal call to the SampSharp.GameMode.API.Interop class with
     * the specified method and name. */
    static void AddInternalCall(const char * name, const void * method);
    /* Loads an event with the specified name and parameter count. */
    static MonoMethod *LoadEvent(const char *name, int param_count);
    /* Gets the index of the length parameter of a callback paramereter with the
     * specified index based on
     * a SampSharp.GameMode.API.ParameterLengthAttribute attribute attached to
     * the specified method.*/
    static int GetParamLengthIndex(MonoMethod *method, int idx);
    /* Calls an event with the specified method on the specified handle with the
     * specified parameters. The exception pointer will be set if an exception
     * is thrown during the executing of the event.*/
    static int CallEvent(MonoMethod *method, uint32_t handle, void **params,
        MonoObject **exception);
    /* Gets the parameter type value asociated with the specified type. */
    static ParameterType GetParameterType(MonoType *type);
    /* Checks whether the specified method in the specified image has the
     * specified parameter count of supported parameter types.*/
    static bool IsMethodValidCallback(MonoImage *image, MonoMethod *method,
        int param_count);
    /* Finds a method for the specified callback name within the specified
     * class. */
    static MonoMethod *FindMethodForCallbackInClass(const char *name,
        int param_count, MonoClass *klass);
    /* Finds a method for the specified callback name within the specified
    * handle. */
    static MonoMethod *FindMethodForCallback(const char *name,
        int param_count, uint32_t &handle);
    /* Prints the specified exception to the log. */
    static void PrintException(const char *methodname, MonoObject *exception);

    static MonoString* StringToMonoString(char* str, int len);
    static char* MonoStringToString(MonoString *str);

    /* Interop/API functions. */
private:
    static bool RegisterExtension(MonoObject *extension);
    static void Print(MonoString *str);

    static int SetRefTimer(int interval, bool repeat, MonoObject *params);
    static bool KillRefTimer(int id);

    static int LoadNative(MonoString *name, MonoString *format,
        MonoArray *sizes_array);
    static int InvokeNative(int handle, MonoArray *arguments);
    static bool NativeExists(MonoString *name);

    static void LoadCodepage(const char *name);

};
