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

#include "hosted_server.h"
#include "logging.h"
#include <assert.h>

#define INTEROP_LIB "SampSharp.Core"
#define INTEROP_CLASS INTEROP_LIB ".Hosting.Interop"

hosted_server *hosting = nullptr;

hosted_server::hosted_server(const char *clr_dir, const char* exe_path) {
    int retval;
    unsigned int exitcode;

    log_info("Initializing .NET runtime...");
    if((retval = app_.initialize(clr_dir, exe_path, "SampSharp Host")) < 0) {
        log_error("Failed to initialize CoreCLR runtime. Error %d.", retval);
        return;
    }
    
    log_info("Starting game mode host...");
    hosting = this;
    const char *args[1];
    args[0] = "--hosted";

    if((retval = app_.execute_assembly(std::size(args), args, &exitcode)) < 0)  {
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

hosted_server::~hosted_server() {
    app_.release();

    if(hosting == this) {
        hosting = NULL;
    }
}
