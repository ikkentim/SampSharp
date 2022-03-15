// SampSharp
// Copyright 2018 Tim Potze
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

#include "coreclrserver.h"
#include "logging.h"
#include <assert.h>

#define INTEROP_LIB "SampSharp.Core"
#define INTEROP_CLASS INTEROP_LIB ".Hosting.Interop"

coreclrserver::coreclrserver(const char *clr_dir, const char* exe_path) {
    int retval;
    unsigned int exitcode;

    log_info("Initializing .NET runtime...");
    if((retval = app_.initialize(clr_dir, exe_path, "SampSharp Host")) < 0) {
        log_error("Failed to initialize CoreCLR runtime. Error %d.", retval);
        return;
    }

    if((retval = app_.create_delegate(INTEROP_LIB, INTEROP_CLASS, "Tick",
        reinterpret_cast<void**>(&tick_))) < 0) {
        log_error("Failed to load Tick delegate. Error %d.", retval);
    }
    if((retval = app_.create_delegate(INTEROP_LIB, INTEROP_CLASS, "PublicCall",
        reinterpret_cast<void**>(&public_call_))) < 0) {
        log_error("Failed to load PublicCall delegate. Error %d.", retval);
    }

    log_info("Starting game mode host...");
    const char *args[1];
    args[0] = "--hosted";

    if((retval = app_.execute_assembly(std::size(args), args,
        &exitcode)) < 0)  {
        log_error("Failed to prepare game mode. Error %d.", retval);
        return;
    }

    if(exitcode) {
        log_error("Failed to prepare game mode. Exit code %d.", exitcode);
        return;
    }

    log_info("Game mode host running.");
    running_ = true;
}

coreclrserver::~coreclrserver() {
    app_.release();
}

void coreclrserver::tick() const {
    if(tick_) {
        tick_();
    }
}

void coreclrserver::public_call(AMX *amx, const char *name, cell *params,
    cell *retval) {
    if(public_call_) {
        mutex_.lock();
        
        public_call_(static_cast<void*>(amx), name, static_cast<void*>(params), static_cast<void*>(retval));
        
        mutex_.unlock();
    }
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL sampsharp_print(const char *msg) {
    log_print("%s", msg);
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
