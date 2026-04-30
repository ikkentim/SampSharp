#include "managed-host.hpp"

#include <cassert>
#include <nethost.h>

#if !defined WINDOWS
#  include <dlfcn.h>
#  include <limits.h>
#  define MAX_PATH PATH_MAX
#endif

#define ERROR_MISSING_EXPORT 0x99999999
#define ERROR_UNKNOWN 0x99999998

bool ManagedHost::initialize(const char ** error_ptr)
{
    if(_isReady)
    {
        return true;
    }

    // Load host resolver and load functions from hosting library
    int rc = load_hostfxr(nullptr);
    if (rc)
    {
        *error_ptr = get_error(rc);
        return false;
    }

    _isReady = true;
    return true;
}

bool ManagedHost::isReady() const
{
    return _isReady;
}

bool ManagedHost::loadFor(const StringView root_path, const StringView assembly_name, const char ** error_ptr)
{
    auto root_path_ = root_path.to_string();
    const auto assembly_name_ = assembly_name.to_string();

    const char sep = root_path_.back();
    if(sep != '\\' && sep != '/') {
        root_path_ = root_path_ + PATH_SEP;
    }
    
    const string_t config_path = widen(root_path_) + widen(assembly_name_) + STR(".runtimeconfig.json");
    int rc = load_runtime(config_path.c_str(), &load_assembly_and_get_function_pointer_fptr);

    if (rc)
    {
        *error_ptr = get_error(rc);
        return false;
    }

    if (load_assembly_and_get_function_pointer_fptr == nullptr)
    {
        *error_ptr = get_error(ERROR_UNKNOWN);
        return false;
    }
    
    assem_path_ = widen((root_path_ + assembly_name_ + ".dll"));
 
    return true;
}

bool ManagedHost::getEntryPoint(const StringView entry_type_name, const StringView name, void ** delegate_ptr, const char ** error_ptr) const
{
    if(load_assembly_and_get_function_pointer_fptr == nullptr)
    {
        return false;
    }

    const string_t entry_type_name_w = widen(entry_type_name.to_string());
    const string_t name_w = widen(name.to_string());

    const int rc = load_assembly_and_get_function_pointer_fptr(
        assem_path_.c_str(),
        entry_type_name_w.c_str(),
        name_w.c_str(),
        UNMANAGEDCALLERSONLY_METHOD,
        nullptr,
        delegate_ptr);

    if (rc)
    {
        *error_ptr = get_error(rc);
        return false;
    }

    return true;
}

void *ManagedHost::load_library(const char_t * path)
{
#ifdef WINDOWS
    HMODULE h = ::LoadLibraryW(path);
    assert(h != nullptr);
    return (void *)h;
#else
    void * h = dlopen(path, RTLD_LAZY | RTLD_LOCAL);
    assert(h != nullptr);
    return h;
#endif
}

void * ManagedHost::get_export(void * h, const char * name)
{
#ifdef WINDOWS
    auto f = (void*)::GetProcAddress(static_cast<HMODULE>(h), name);  // NOLINT(clang-diagnostic-microsoft-cast)
    assert(f != nullptr);
    return f;
#else
    void * f = dlsym(h, name);
    assert(f != nullptr);
    return f;
#endif
}

int ManagedHost::load_hostfxr(const char_t * assembly_path)
{
    // Pre-allocate a large buffer for the path to hostfxr
    char_t buffer[MAX_PATH];
    size_t buffer_size = sizeof(buffer) / sizeof(char_t);
    
    const get_hostfxr_parameters params { sizeof(get_hostfxr_parameters), assembly_path, nullptr };
    const int rc = get_hostfxr_path(buffer, &buffer_size, &params);
    if (rc != 0)
    {
        return rc;
    }

    // Load hostfxr and get desired exports
    void *lib = load_library(buffer);
    init_for_config_fptr = (hostfxr_initialize_for_runtime_config_fn)get_export(lib, "hostfxr_initialize_for_runtime_config");
    get_delegate_fptr = (hostfxr_get_runtime_delegate_fn)get_export(lib, "hostfxr_get_runtime_delegate");
    close_fptr = (hostfxr_close_fn)get_export(lib, "hostfxr_close");
 
    if (!init_for_config_fptr || !get_delegate_fptr || !close_fptr)
    {
        return ERROR_MISSING_EXPORT;
    }

    return 0;
}

int ManagedHost::load_runtime(const char_t * config_path, load_assembly_and_get_function_pointer_fn * fptr) const
{
    // Load .NET Core
    void * ptr = nullptr;
    hostfxr_handle cxt = nullptr;
    int rc = init_for_config_fptr(config_path, nullptr, &cxt);
    if (rc != 0)
    {
        close_fptr(cxt);
        return rc;
    }

    if (cxt == nullptr)
    {
        return ERROR_UNKNOWN;
    }

    // Get the load assembly function pointer
    rc = get_delegate_fptr(
        cxt,
        hdt_load_assembly_and_get_function_pointer,
        &ptr);

    close_fptr(cxt);
    *fptr = (load_assembly_and_get_function_pointer_fn)ptr;
    return rc;
}

const char * ManagedHost::get_error(int code) const
{
    switch ((unsigned int)code)
    {
        case 0x00000000: return "Operation was successful";
        case 0x00000001: return "Initialization was successful, but another host context is already initialized";
        case 0x00000002: return "Initialization was successful, but another host context is already initialized and the requested context specified runtime properties which are not the same";
        case 0x80008081: return "One or more arguments are invalid";
        case 0x80008082: return "Failed to load a hosting component";
        case 0x80008083: return "One of the hosting components is missing";
        case 0x80008084: return "One of the hosting components is missing a required entry point";
        case 0x80008085: return "Failed to get the path of the current hosting component and determine the .NET installation location";
        case 0x80008087: return "The `coreclr` library could not be found";
        case 0x80008088: return "Failed to load the `coreclr` library or finding one of the required entry points";
        case 0x80008089: return "Call to `coreclr_initialize` failed";
        case 0x8000808a: return "Call to `coreclr_execute_assembly` failed";
        case 0x8000808b: return "Initialization of the `hostpolicy` dependency resolver failed";
        case 0x8000808c: return "Resolution of dependencies in `hostpolicy` failed";
        case 0x8000808e: return "Initialization of the `hostpolicy` library failed";
        case 0x80008092: return "Arguments to `hostpolicy` are invalid";
        case 0x80008093: return "The `.runtimeconfig.json` file is invalid";
        case 0x80008094: return "[internal usage only]";
        case 0x80008095: return "`apphost` failed to determine which application to run";
        case 0x80008096: return "Failed to find a compatible framework version";
        case 0x80008097: return "Host command failed";
        case 0x80008098: return "Buffer provided to a host API is too small to fit the requested value";
        case 0x8000809a: return "Application path imprinted in `apphost` doesn't exist";
        case 0x8000809b: return "Failed to find the requested SDK";
        case 0x8000809c: return "Application has multiple references to the same framework which are not compatible";
        case 0x8000809d: return "[internal usage only]";
        case 0x8000809f: return "Error extracting single-file bundle";
        case 0x800080a0: return "Error reading or writing files during single-file bundle extraction";
        case 0x800080a1: return "The application's `.runtimeconfig.json` contains a runtime property which is produced by the hosting layer";
        case 0x800080a2: return "Feature which requires certain version of the hosting layer was used on a version which doesn't support it";
        case 0x800080a3: return "Current state is incompatible with the requested operation";
        case 0x800080a4: return "Property requested by `hostfxr_get_runtime_property_value` doesn't exist";
        case 0x800080a5: return "Host configuration is incompatible with existing host context";
        case 0x800080a6: return "Hosting API does not support the requested scenario";
        case 0x800080a7: return "Support for a requested feature is disabled";
        case ERROR_MISSING_EXPORT: return "Missing export fuction in host library";
        default: return "Unkown error";
    }
}