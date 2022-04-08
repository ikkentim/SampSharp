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

#include <string>
#include "platforms.h"
#if SAMPSHARP_LINUX
#  include <coreclr/coreclrhost.h>
#elif SAMPSHARP_WINDOWS
#  include <coreclr/mscoree.h>
#endif

#if SAMPSHARP_LINUX
#  define CORECLR_CALL
#  define CORECLR_LIB "libcoreclr.so"
#  define TPA_DELIMITER ":"
#elif SAMPSHARP_WINDOWS
#  define CORECLR_CALL __stdcall
#  define CORECLR_LIB "coreclr.dll"
#  define TPA_DELIMITER ";"
#endif

#if SAMPSHARP_LINUX
typedef void host_t;
typedef unsigned int domaind_id_t;
typedef void * module_t;
#elif SAMPSHARP_WINDOWS
typedef ICLRRuntimeHost2 host_t;
typedef DWORD domaind_id_t;
typedef HMODULE module_t;
#endif


class coreclr_app {
public:
    int initialize(const char *clr_dir, const char* exe_path,
        const char* app_domain_friendly_name);
    int create_delegate(const char* assembly_name, const char* type_name,
        const char* method_name, void** delegate);
    int execute_assembly(int argc, const char** argv, unsigned int* exit_code);
    int release();

private:
    int construct_tpa(const char *directory, std::string &tpa_list);
    void add_deps_to_tpa(std::string abs_exe_path, std::string &tpa_list);

private:
    std::string abs_exe_path_;
    host_t *host_ = NULL;
    module_t module_ = NULL;
    domaind_id_t domain_id_ = 0;

#if SAMPSHARP_LINUX
private:
    bool load_symbol(void *coreclr_lib, const char *symbol, void **ptr);

    coreclr_initialize_ptr coreclr_initialize_ = NULL;
    coreclr_shutdown_ptr coreclr_shutdown_ = NULL;
    coreclr_shutdown_2_ptr coreclr_shutdown_2_ = NULL;
    coreclr_create_delegate_ptr coreclr_create_delegate_ = NULL;
    coreclr_execute_assembly_ptr coreclr_execute_assembly_ = NULL;
#endif
};

