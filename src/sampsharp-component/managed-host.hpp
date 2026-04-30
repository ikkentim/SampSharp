#pragma once

#include <hostfxr.h>
#include <coreclr_delegates.h>
#include <string>
#include "types.hpp"
#include "compat.hpp"

class ManagedHost final
{
private:
    bool _isReady = false;

    string_t assem_path_;
    hostfxr_initialize_for_runtime_config_fn init_for_config_fptr = nullptr;
    hostfxr_get_runtime_delegate_fn get_delegate_fptr = nullptr;
    hostfxr_close_fn close_fptr = nullptr;
    load_assembly_and_get_function_pointer_fn load_assembly_and_get_function_pointer_fptr = nullptr;

    static void * load_library(const char_t *);
    static void * get_export(void *, const char *);

    int load_hostfxr(const char_t * assembly_path);

    int load_runtime(const char_t * config_path, load_assembly_and_get_function_pointer_fn * fptr) const;
    const char * get_error(int code) const;

public:
    bool isReady() const;
    bool initialize(const char ** error_ptr);
    bool loadFor(const StringView root_path, const StringView assembly_name, const char ** error_ptr);
    bool getEntryPoint(const StringView entry_type_name, const StringView name, void ** delegate_ptr, const char ** error_ptr) const;
};