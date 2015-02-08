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

class GameMode {
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

    static bool isLoaded_; 
    static TimerMap timers_;
    static ExtensionList extensions_;
    static CallbackMap callbacks_;
    static MonoDomain *domain_;
    static GameModeImage gameMode_;
    static GameModeImage baseMode_;
    static uint32_t gameModeHandle_;

    static MonoMethod *tickMethod_;
    static MonoClass *paramLengthClass_;
    static MonoMethod *paramLengthGetMethod_;

    static bool RegisterExtension(MonoObject *extension);
    static int SetRefTimer(int interval, bool repeat, MonoObject *params);
    static bool KillRefTimer(int id);
    static void SAMPGDK_CALL ProcessTimerTick(int timerid, void *data);

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
};