#include <cassert>
#include <sampgdk/sampgdk.h>
#include "platforms.h"

extern void *pAMXFunctions;

struct sampsharp_api_t {
    void **plugin_data;
    void *find_native;
    void *invoke_native;
} sampsharp_api;

void sampsharp_api_setup(void **plugin_data) {
    sampsharp_api.plugin_data = plugin_data;
    sampsharp_api.find_native = (void *)sampgdk_FindNative;
    sampsharp_api.invoke_native = (void *)sampgdk_InvokeNativeArray;
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL_PTR sampsharp_get_api() {
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
