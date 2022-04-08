#pragma once

#include <sampgdk/sampgdk.h>

void sampsharp_api_setup(void **plugin_data);
void sampsharp_api_cleanup();

void sampsharp_api_public_call(AMX *amx, const char *name, cell *params, cell *retval);
void sampsharp_api_tick();