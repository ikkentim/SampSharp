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

#define INTEROP_LIB "SampSharp.Core"
#define INTEROP_CLASS INTEROP_LIB ".Hosting.Interop"

hosted_server *hosting = NULL;

hosted_server::hosted_server(const char *clr_dir, const char* exe_path) {
    int retval;
    unsigned int exitcode;
    if((retval = app_.initialize(clr_dir, exe_path, "SampSharp Host")) < 0) {
        log_error("Failed to initialize CoreCLR runtime. Error %d.", retval);
        return;
    }

    if((retval = app_.create_delegate(INTEROP_LIB, INTEROP_CLASS, "Tick",
        (void **)&tick_)) < 0) {
        log_warning("Failed to load Tick delegate. Error %d.", retval);
    }
    if((retval = app_.create_delegate(INTEROP_LIB, INTEROP_CLASS, "PublicCall",
        (void **)&public_call_)) < 0) {
        log_warning("Failed to load PublicCall delegate. Error %d.", retval);
    }

    hosting = this;
    const char *args[1];
    args[0] = "--hosted";

    if((retval = app_.execute_assembly(sizeof(args) / sizeof(args[0]), args,
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

hosted_server::~hosted_server() {
    if(hosting == this) {
        hosting = NULL;
    }
}

void hosted_server::tick() {
    if(tick_) {
        tick_();
    }
}

void hosted_server::public_call(AMX *amx, const char *name, cell *params,
    cell *retval) {
    uint32_t 
        response, 
        len;

    if(public_call_) {

        len = callbacks_.fill_call_buffer(amx, name, params, buf_, LEN_CBBUF,
            false);

        if (len == 0) {
            return;
        }

        mutex_.lock();

        response = public_call_(name, buf_, len);

        mutex_.unlock();

        if (retval) {
            *retval = response;
        }
    }
}

void hosted_server::terminate(const char *context) {
    log_warning("Hosted server termination not implemented.");
    log_warning(context);

    //natives_.clear();
    //callbacks_.clear();
}

void hosted_server::print(const char* msg) const
{
    log_print("%s", msg);
}

int hosted_server::get_native_handle(const char* name) {
    return natives_.get_handle(name);
}

void hosted_server::invoke_native(uint8_t *inbuf, uint32_t inlen,
    uint8_t *outbuf, uint32_t *outlen) {
    natives_.invoke(inbuf, inlen, outbuf, outlen);
}

void hosted_server::register_callback(uint8_t* buf) {
    log_debug("Register callback %s", buf);
    callbacks_.register_buffer(buf);
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL sampsharp_print(const char *msg) {
    if(hosting) {
        hosting->print(msg);
    }
}

SAMPSHARP_EXPORT int SAMPSHARP_CALL sampsharp_get_native_handle(
    const char *name) {
    if(hosting) {
        return hosting->get_native_handle(name);
    }

    return NATIVE_NOT_FOUND;
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL sampsharp_invoke_native(uint8_t *inbuf,
    uint32_t inlen, uint8_t *outbuf, uint32_t *outlen) {
    if(hosting) {
        hosting->invoke_native(inbuf, inlen, outbuf, outlen);
    }
}

SAMPSHARP_EXPORT void SAMPSHARP_CALL sampsharp_register_callback(uint8_t *buf) {
    if(hosting) {
        hosting->register_callback(buf);
    }
}
