#include <cassert>
#include <sampgdk/sampgdk.h>
#include "platforms.h"

extern void *pAMXFunctions;

struct sampsharp_api_t {
    void **plugin_data;
    void *find_native;
    void *invoke_native;
} sampsharp_api;

typedef void SAMPSHARP_CALL api_public_call_t(AMX *amx, const char *name, cell *params, cell *retval);
typedef void SAMPSHARP_CALL api_tick_t();

api_public_call_t *bound_public_call = nullptr;
api_tick_t *bound_tick = nullptr;

void sampsharp_api_setup(void **plugin_data) {
    sampsharp_api.plugin_data = plugin_data;
    sampsharp_api.find_native = (void *)sampgdk_FindNative;
    sampsharp_api.invoke_native = (void *)sampgdk_InvokeNativeArray;
}

void sampsharp_api_cleanup() {
    sampsharp_api.plugin_data = nullptr;
    bound_public_call = nullptr;
    bound_tick = nullptr;
}

void api_public_call(AMX *amx, const char *name, cell *params, cell *retval) {
    if(bound_public_call) {
        bound_public_call(amx, name, params, retval);
    }
}

void api_tick() {
    if(bound_tick) {
        bound_tick();
    }
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL_PTR sampsharp_get_api(api_public_call_t *public_call, api_tick_t* tick) {
    bound_public_call = public_call;
    bound_tick = tick;
    return &sampsharp_api;
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL sampsharp_print(const char *msg) {
    // todo: skip sampgdk
    sampgdk_logprintf("%s", msg);
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL_PTR sampsharp_fast_native_find(const char *name) {
    assert(name != nullptr);
    return (void *)sampgdk_FindNative(name);
}

SAMPSHARP_EXPORT int SAMPSHARP_CALL sampsharp_fast_native_invoke(void *native, const char *format, void **args) {
    assert(native != nullptr);
    assert(format != nullptr);
    assert(args != nullptr);
    return sampgdk_InvokeNativeArray((AMX_NATIVE)native, format, args);
}
