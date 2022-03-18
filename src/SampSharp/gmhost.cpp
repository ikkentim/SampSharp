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

void* load_library(const char_t*);
void* get_export(void*, const char*);

gmhost::gmhost(fs::path hostfxr_path, fs::path gamemode_path) : hostfxr_path_(std::move(hostfxr_path)),
                                                                gamemode_path(std::move(gamemode_path)),
                                                                close_fptr_(nullptr),
                                                                handle_(nullptr),
                                                                tick_(nullptr),
                                                                public_call_(nullptr) {}

gmhost::~gmhost() {
	close_handle();
}

bool gmhost::start() {
	// Extract assembly name from path to runtimeconfig.
	const std::string filename = gamemode_path.filename().string();
	const std::string assembly_name = filename.substr(0, filename.length() - 4); // remove ".dll"


	// Load hostfxr and get desired exports
	const std::wstring libnamew = absolute(hostfxr_path_).wstring();

	void* lib = load_library(libnamew.c_str());

	const auto get_delegate_fptr = (hostfxr_get_runtime_delegate_fn)get_export(lib, "hostfxr_get_runtime_delegate");
	const auto run_fptr = (hostfxr_run_app_fn)get_export(lib, "hostfxr_run_app");
	const auto init_fptr = (hostfxr_initialize_for_runtime_config_fn)get_export(
		lib, "hostfxr_initialize_for_runtime_config");
	const auto init2_fptr = (hostfxr_initialize_for_dotnet_command_line_fn)get_export(
		lib, "hostfxr_initialize_for_dotnet_command_line");
	close_fptr_ = (hostfxr_close_fn)get_export(lib, "hostfxr_close");

	if (!(init_fptr && run_fptr && get_delegate_fptr && close_fptr_)) {
		log_error("Failed to load hostfxr exports.");
		return false;
	}

	// Initialize hostfxr
	const std::wstring wgm = absolute(gamemode_path).wstring();

	const char_t* aaa[] = {
		wgm.c_str()
	};
	int rc = init2_fptr(1, aaa, nullptr, &handle_);
	//int rc = init_fptr(wgm.c_str(), nullptr, &handle_);

	if (rc != 0 || !handle_) {
		log_error("Failed to initialize hostfxr (error code %d)", rc);
		close_handle();
		return false;
	}

	// Get interop pointers
	get_function_pointer_fn get_function_pointer;
	rc = get_delegate_fptr(handle_, hdt_get_function_pointer, (void**)&get_function_pointer);

	if (rc != 0 || !get_function_pointer) {
		log_error("Failed to get delegate hdt_get_function_pointer (error code %d)", rc);
		close_handle();
		return false;
	}

	const auto interop = STR("SampSharp.Core.Hosting.Interop, SampSharp.Core");

	rc = get_function_pointer(interop, STR("OnTick"), UNMANAGEDCALLERSONLY_METHOD, nullptr, nullptr, (void**)&tick_);

	if (rc != 0 || !tick_) {
		log_error("Failed to get pointer to OnTick function (error code %d)", rc);
		close_handle();
		return false;
	}

	rc = get_function_pointer(interop, STR("OnPublicCall"),
	                          STR("SampSharp.Core.Hosting.Interop+PublicCallDelegate, SampSharp.Core"), nullptr,
	                          nullptr, (void**)&public_call_);
	// rc = get_function_pointer(interop, STR("OnPublicCall"), UNMANAGEDCALLERSONLY_METHOD, nullptr, nullptr, (void**)&public_call_);

	if (rc != 0 || !public_call_) {
		log_error("Failed to get pointer to OnPublicCall function (error code %d)", rc);
		close_handle();
		return false;
	}

	// hostfxr_run_app starts and shuts down the runtime. we'll use our Interop library as a workaround.
	typedef bool (CORECLR_DELEGATE_CALLTYPE *entry_point_fn)(char* assembly);
	entry_point_fn entry_point;
	rc = get_function_pointer(interop, STR("InvokeEntryPoint"),
	                          STR("SampSharp.Core.Hosting.Interop+EntryPointDelegate, SampSharp.Core"), nullptr,
	                          nullptr, (void**)&entry_point);

	if (rc != 0) {
		log_error("Failed to get pointer to InvokeEntryPoint (error code %d)", rc);
		close_handle();
		return false;
	}

	// Run managed assembly
	if(!entry_point(const_cast<char*>(assembly_name.c_str()))) {
		log_error("Failed to load game mode.");
		return false;
	}
	else {
		log_info("Game mode host running.");
		return true;
	}
}

void gmhost::close_handle() {
	if (close_fptr_ && handle_) {
		close_fptr_(handle_);
		handle_ = nullptr;
	}
}

void gmhost::tick() {
	if (tick_) {
		mutex_.lock();
		tick_();
		mutex_.unlock();
	}
}

void gmhost::public_call(AMX* amx, const char* name, cell* params, cell* retval) {
	if (public_call_) {
		mutex_.lock();
		public_call_((void*)amx, name, (void*)params, (void*)retval);
		mutex_.unlock();
	}
}


#if SAMPSHARP_WINDOWS
void* load_library(const char_t* path) {
	const HMODULE h = LoadLibraryW(path);
	assert(h != nullptr);
	return (void*)h;
}

void* get_export(void* h, const char* name) {
	const auto f = (void*)GetProcAddress((HMODULE)h, name);
	assert(f != nullptr);
	return f;
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
