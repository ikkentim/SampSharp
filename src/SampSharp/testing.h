#pragma once

#include <sampgdk/sampgdk.h>

// SampSharp proves a small number of natives which can be used for testing functionality
// related to invoking natives, processing the return value of the natives and handling of
// references in native parameters.

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

// native sampsharptest_varargs2(a, b, {Float,_}:...);
cell test_varargs2(AMX *amx, cell *params);
