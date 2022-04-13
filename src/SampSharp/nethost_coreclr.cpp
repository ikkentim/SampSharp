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

#include "nethost_coreclr.h"
#include "logging.h"

nethost_coreclr::~nethost_coreclr() {
    release();
}

bool nethost_coreclr::setup(locator *locator, config* cfg) {
    coreclr_ = locator->get_coreclr();
    gamemode_ = locator->get_gamemode();

    if(coreclr_.empty() || !exists(coreclr_)) {
        log_error("Invalid coreclr directory specified in server.cfg.");
        return false;
    }
    
    
    if(gamemode_.empty() || !exists(gamemode_)) {
        log_error("Invalid gamemode specified in server.cfg.");
        return false;
    }

    log_info("Gamemode: %s", gamemode_.string().c_str());
    log_info("CoreCLR: %s", coreclr_.string().c_str());
    return true;
}

void nethost_coreclr::start() {
    int retval;
    unsigned int exitcode;

    const auto clr_dir = (coreclr_.parent_path()).string();
    const auto exe_path = (gamemode_).string();

    log_info("Initializing .NET runtime...");
    if((retval = app_.initialize(clr_dir.c_str(), exe_path.c_str(), "SampSharp Host")) < 0) {
        log_error("Failed to initialize CoreCLR runtime. Error %d.", retval);
        return;
    }

    host_init_ = true;
    
    log_info("Starting game mode host...");
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

void nethost_coreclr::stop() {
    release();
}

void nethost_coreclr::release() {
     if(host_init_) {
        app_.release();
        host_init_ = false;
    }   
}