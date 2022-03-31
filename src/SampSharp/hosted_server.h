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

#pragma once

#include "server.h"
#include <sampgdk/sampgdk.h>
#include "coreclr_app.h"
#include "callbacks_map.h"
#include <mutex>
#include <inttypes.h>

#define LEN_CBBUF (1024 * 16)

typedef void (CORECLR_CALL *tick_ptr)();

typedef int32_t (CORECLR_CALL *public_call_ptr)(const char *name, uint8_t *args,
    uint32_t length);

/** a CLR hosted game mode server */
class hosted_server : public server {
public:
    hosted_server(const char *clr_dir, const char* exe_path);
    ~hosted_server();
    void tick() override;
    void public_call(AMX *amx, const char *name, cell *params, cell *retval) override;
    void register_callback(uint8_t *buf);

private:
    /** the running game mode CLR instance */
    coreclr_app app_;
    /** buffer */
    uint8_t buf_[LEN_CBBUF];
    /** map of registered callbacks */
    callbacks_map callbacks_;
    /** lock for callbacks/ticks */
    std::recursive_mutex mutex_;
    /** pointer to the tick CLR function */
    tick_ptr tick_ = NULL;
    /** pointer to the public call CLR function */
    public_call_ptr public_call_ = NULL;
    /** indicates whether the game mode is running */
    bool running_ = false;
};
