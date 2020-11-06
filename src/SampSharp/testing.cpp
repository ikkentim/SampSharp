#include "testing.h"
#include <cstring>
#include "logging.h"

cell test_inout(AMX *amx, cell *params) {
#ifdef ENABLE_TEST_LOGGING
    log_info("test_inout %d", params[1]);
#endif
    return params[1];
}

cell test_inrefout(AMX *amx, cell *params) {
    cell *address;
	amx_GetAddr(amx, params[2], &address);
	*address = params[1];
    
#ifdef ENABLE_TEST_LOGGING
    log_info("test_inrefout %d", params[1]);
#endif
    return 1;
}

cell test_inoutstr(AMX *amx, cell *params) {
    char *str;
    amx_StrParam(amx, params[1], str);

    cell *address = NULL;
	amx_GetAddr(amx, params[2], &address);
	amx_SetString(address, str, 0, 0, static_cast<size_t>(params[3]));
   
#ifdef ENABLE_TEST_LOGGING 
    log_info("test_inoutstr %s, blen=%d", str, params[3]);
#endif
    return 1;
}

cell test_inoutarr(AMX *amx, cell *params) {
    cell *addr_a;
    cell *addr_b;

    amx_GetAddr(amx, params[1], &addr_a);
    amx_GetAddr(amx, params[2], &addr_b);

    cell len = params[3];
    memcpy(addr_b, addr_a, sizeof(cell) * len);
   
#ifdef ENABLE_TEST_LOGGING 
    log_info("test_inoutarr ablen=%d", params[3]);
#endif
    return 1;
}
