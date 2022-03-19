#include <cassert>
#include <sampgdk/sampgdk.h>
#include "platforms.h"
#include "version.h"
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

SAMPSHARP_EXPORT uint32_t SAMPSHARP_CALL sampsharp_get_plugin_version() {
    return PLUGIN_VERSION_NUM;
}

SAMPSHARP_EXPORT cell SAMPSHARP_CALL_PTR sampsharp_get_addr(AMX *amx, cell amx_addr) {
    cell *phys_addr;
    amx_GetAddr(amx, amx_addr, &phys_addr);
    return phys_addr;
}

SAMPSHARP_EXPORT int32_t SAMPSHARP_CALL sampsharp_get_string(cell *phys_addr, char *dest, uint32_t len) {
    return amx_GetString(dest, phys_addr, 0, len);	
}

SAMPSHARP_EXPORT int32_t SAMPSHARP_CALL sampsharp_get_string_len(cell *phys_addr) {
    int32_t len;
    amx_StrLen(phys_addr, &len);
    return len;
}
