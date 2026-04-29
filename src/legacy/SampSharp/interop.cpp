// SampSharp
// Copyright 2022 Tim Potze
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

#include "interop.h"
#include "platforms.h"

struct sampsharp_api_t {
    size_t size;
    void **plugin_data;
    void *find_native;
    void *invoke_native;
} sampsharp_api;

typedef void api_public_call_t(AMX *amx, const char *name, cell *params, cell *retval);
typedef void api_tick_t();

api_public_call_t *bound_public_call = nullptr;
api_tick_t *bound_tick = nullptr;

void sampsharp_api_setup(void **plugin_data) {
    sampsharp_api.size = sizeof(sampsharp_api);
    sampsharp_api.plugin_data = plugin_data;
    sampsharp_api.find_native = (void *)sampgdk_FindNative;
    sampsharp_api.invoke_native = (void *)sampgdk_InvokeNativeArray;
}

void sampsharp_api_cleanup() {
    sampsharp_api.plugin_data = nullptr;
    bound_public_call = nullptr;
    bound_tick = nullptr;
}

void sampsharp_api_public_call(AMX *amx, const char *name, cell *params, cell *retval) {
    if(bound_public_call) {
        bound_public_call(amx, name, params, retval);
    }
}

void sampsharp_api_tick() {
    if(bound_tick) {
        bound_tick();
    }
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL_PTR sampsharp_api_initialize(api_public_call_t *public_call, api_tick_t* tick) {
    bound_public_call = public_call;
    bound_tick = tick;
    return &sampsharp_api;
}
