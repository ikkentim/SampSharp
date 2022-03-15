#include "testing.h"
#include <cstring>
#include "platforms.h"
#include "logging.h"


// native sampsharptest_inout(a);
cell test_inout(AMX *amx, cell *params);

// native sampsharptest_inrefout(a, &b);
cell test_inrefout(AMX *amx, cell *params);

// native sampsharptest_inoutstr(const a[], b[], blen = sizeof b);
cell test_inoutstr(AMX *amx, cell *params);

// native sampsharptest_inoutarr(const a[], b[], ablen = sizeof a);
cell test_inoutarr(AMX *amx, cell *params);

// native sampsharptest_varargs({Float,_}:...);
cell test_varargs(AMX *amx, cell *params);

// native sampsharptest_varargs_mix(a, b, {Float,_}:...);
cell test_varargs_mix(AMX *amx, cell *params);

// native sampsharptest_varargs_str({_}:...);
cell test_varargs_str(AMX *amx, cell *params);

extern "C" const AMX_NATIVE_INFO native_list[] = {
    { "sampsharptest_inout", test_inout },
    { "sampsharptest_inrefout", test_inrefout },
    { "sampsharptest_inoutstr", test_inoutstr },
    { "sampsharptest_inoutarr", test_inoutarr },
    { "sampsharptest_varargs", test_varargs },
    { "sampsharptest_varargs_mix", test_varargs_mix },
    { "sampsharptest_varargs_str", test_varargs_str },
    { NULL, NULL }
};

int register_test_natives(AMX* amx) {
#ifdef DISABLE_TEST_NATIVES
    return 1;
#else
    return amx_Register(amx, native_list, -1);
#endif
}

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

cell test_varargs(AMX *amx, cell *params) {
#ifdef ENABLE_TEST_LOGGING 
    log_info("test_varargs len=%d", params[0]);

    for (unsigned int i = 0; i < params[0] / sizeof(cell); i++) {
        cell *addr;
        amx_GetAddr(amx, params[i + 1], &addr);

        log_info("test_varargs arg[%d]=%d", i, *addr);
    }
#endif
    return 1;
}

cell test_varargs_mix(AMX *amx, cell *params) {
#ifdef ENABLE_TEST_LOGGING 
    log_info("test_varargs_mix len=%d", params[0]);
    
    log_info("test_varargs_mix arg[0]=%d", params[1]);
    log_info("test_varargs_mix arg[1]=%d", params[2]);

    for (unsigned int i = 2; i < params[0] / sizeof(cell); i++) {
        cell *addr;
        amx_GetAddr(amx, params[i + 1], &addr);

        log_info("test_varargs_mix arg[%d]=%d", i, *addr);
    }
#endif
    return 1;
}

cell test_varargs_str(AMX *amx, cell *params) {
#ifdef ENABLE_TEST_LOGGING 
    log_info("test_varargs_str len=%d", params[0]);
    
    for (unsigned int i = 0; i < params[0] / sizeof(cell); i++) {
        char *str;

        amx_StrParam(amx, params[i + 1], str);
        
        log_info("test_varargs_str arg[%d]=%s", i, str);
    }
#endif
    return 1;
}
