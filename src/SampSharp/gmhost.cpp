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

#include "gmhost.h"
#include "logging.h"
#include <assert.h>

#define INTEROP_LIB "SampSharp.Core"
#define INTEROP_CLASS INTEROP_LIB ".Hosting.Interop"


#if SAMPSHARP_WINDOWS
#  include <Windows.h>
#  define STR(s) L ## s
#elif SAMPSHARP_LINUX
#  include <dlfcn.h>
#  define STR(s) s
#endif

void *load_library(const char_t *);
void *get_export(void *, const char *);
thread_id get_cur_thread();

gmhost::gmhost(const char *hostfxr_dir, const char* gamemode_path) : main_thread_(get_cur_thread()),
														             rcon_(false),
																	 handle_(nullptr),
                                                                     tick_(nullptr),
																	 public_call_(nullptr) {
	const auto buffer = STR("D:\\projects\\sampsharp\\env\\runtime\\host\\fxr\\6.0.3\\hostfxr.dll");
    
	// Load hostfxr and get desired exports
    void *lib = load_library(buffer);
    const auto get_delegate_fptr = (hostfxr_get_runtime_delegate_fn)get_export(lib, "hostfxr_get_runtime_delegate");
    close_fptr_ = (hostfxr_close_fn)get_export(lib, "hostfxr_close");
    const auto run_fptr = (hostfxr_run_app_fn)get_export(lib, "hostfxr_run_app");
    const auto init_fptr = (hostfxr_initialize_for_dotnet_command_line_fn)get_export(lib, "hostfxr_initialize_for_dotnet_command_line");

    if(!(init_fptr && run_fptr && get_delegate_fptr && close_fptr_)) {
    	log_error("Failed to load hostfxr exports.");
        return; 
    }

    // Initialize hostfxr
    std::string gm = gamemode_path;
    std::wstring wgm = std::wstring(gm.begin(), gm.end());
    
    const char_t *startarg[] = {
        wgm.c_str()
    };

    int rc = init_fptr(1, startarg, nullptr, &handle_);

    if(rc != 0 || !handle_) {
	    log_error("Failed to initialize hostfxr. Status: %d", rc);
        return;
    }
    
    // Get interop pointers
    get_function_pointer_fn get_function_pointer;
    rc = get_delegate_fptr(handle_, hdt_get_function_pointer, (void **)&get_function_pointer);

    if(rc != 0 || !get_function_pointer) {
        log_error("Failed to get delegate hdt_get_function_pointer");
	    return;
    }

    log_info("step 1");

    const auto interop = STR("SampSharp.Core.Hosting.Interop, SampSharp.Core");

    rc = get_function_pointer(interop, STR("OnTick"), UNMANAGEDCALLERSONLY_METHOD, nullptr, nullptr, (void**)&tick_);
    
    log_info("step 2");

    if(rc != 0 || !tick_) {
        log_error("Failed to get pointer to OnTick function");
	    return;
    }

    get_function_pointer(interop, STR("OnPublicCall"), UNMANAGEDCALLERSONLY_METHOD, nullptr, nullptr, (void**)&public_call_);
  
    log_info("step 3");

    if(rc != 0 || !public_call_) {
        log_error("Failed to get pointer to OnPublicCall function");
	    return;
    }

    // Run managed assembly
    rc = run_fptr(handle_);

    log_info("main ran");

    if(rc != 0) {
        log_error("Failed to start managed assembly: %d", rc);
	    return;
    }

    log_info("Game mode host running.");
}

gmhost::~gmhost() {
    if(close_fptr_ && handle_) {
        log_info("Closing handle");
	    close_fptr_(handle_);
        handle_ = nullptr;
    }
}

void gmhost::tick() {
    if(rcon_)
    {
        // public_call_(nullptr, "OnRconCommand", nullptr, nullptr);
        // rcon_=false;
    }
	if(tick_) {
        // mutex_.lock();
		tick_();
        // mutex_.unlock();
	}
}

void gmhost::public_call(AMX *amx, const char *name, cell *params,
    cell *retval) {
    if(public_call_) {
        if(get_cur_thread() == main_thread_) {
	         log_info("pub call %s", name);
   
	        //mutex_.lock();
	        log_info("mut lock");

	        public_call_(static_cast<void*>(amx), name, strlen(name), static_cast<void*>(params), static_cast<void*>(retval));
	        
	        log_info("mut unlock");
	        //mutex_.unlock();

	        log_info("pub call completed"); 
        } else {
	        log_error("Call to %s not on main thread", name);
        }
    }
}


#if SAMPSHARP_WINDOWS
void *load_library(const char_t *path)
{
    HMODULE h = ::LoadLibraryW(path);
    assert(h != nullptr);
    return (void*)h;
}
void *get_export(void *h, const char *name)
{
    void *f = ::GetProcAddress((HMODULE)h, name);
    assert(f != nullptr);
    return f;
}
thread_id get_cur_thread() {
	return GetCurrentThreadId();
}
#elif SAMPSHARP_LINUX
void *load_library(const char_t *path)
{
    void *h = dlopen(path, RTLD_LAZY | RTLD_LOCAL);
    assert(h != nullptr);
    return h;
}
void *get_export(void *h, const char *name)
{
    void *f = dlsym(h, name);
    assert(f != nullptr);
    return f;
}
#endif
