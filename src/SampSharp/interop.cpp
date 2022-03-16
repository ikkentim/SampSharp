#include <cassert>
#include <sampgdk/sampgdk.h>
#include "platforms.h"
#include "logging.h"

SAMPSHARP_EXPORT void SAMPSHARP_CALL sampsharp_print(const char *msg) {
    log_print("%s", msg);
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL_PTR sampsharp_fast_native_find(const char *name) {
    assert(name != nullptr);
    return (void *)sampgdk_FindNative(name);
}

SAMPSHARP_EXPORT int32_t SAMPSHARP_CALL sampsharp_fast_native_invoke(void *native, const char *format, void **args) {
    assert(native != nullptr);
    assert(format != nullptr);
    assert(args != nullptr);
    return sampgdk_InvokeNativeArray((AMX_NATIVE)native, format, args);
}
