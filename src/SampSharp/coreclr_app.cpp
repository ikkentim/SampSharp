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

#include "coreclr_app.h"
#include <algorithm>
#include <cstdlib>
#include <cstring>
#include <string.h>
#include <assert.h>
#include <limits.h>
#include <fstream>
#include <vector>
#include <set>
#include <string>
#include "pathutil.h"
#include "logging.h"
#include "json.h"
#if SAMPSHARP_LINUX
#  include <dirent.h>
#  include <dlfcn.h>
#  include <unistd.h>
#  include <sys/stat.h>
#  if defined(__FreeBSD__)
#    include <sys/types.h>
#    include <sys/param.h>
#  endif
#  if defined(HAVE_SYS_SYSCTL_H) || defined(__FreeBSD__)
#    include <sys/sysctl.h>
#  endif
#endif

#if !defined(PATH_MAX) && defined(MAX_PATH)
#  define PATH_MAX MAX_PATH
#endif

#define JSON_IS(obj, type) (obj.JSONType() == json::JSON::Class::type)
#define JSON_KEY_IS(obj, key, type) (obj.hasKey(key) && JSON_IS(obj[key], type))

#if SAMPSHARP_LINUX
bool coreclr_app::load_symbol(void *coreclr_lib, const char *symbol,
    void **ptr) {

    *ptr = dlsym(coreclr_lib, symbol);

    if(!*ptr) {
        log_error("Function %s not found in the libcoreclr.so.", symbol);
        return false;
    }

    return true;
}
#endif

int coreclr_app::initialize(const char *clr_dir_c, const char* exe_path,
    const char* app_domain_friendly_name) {
    std::string abs_exe_path;
    if(!get_absolute_path(exe_path, abs_exe_path)) {
        log_error("Failed to get absolute executable path.");
        return -1;
    }

    abs_exe_path_ = abs_exe_path;

    std::string app_dir;
    if(!get_directory(abs_exe_path.c_str(), app_dir)) {
        log_error("Failed to get app path.");
        return -1;
    }

    std::string clr_dir;
    if(!get_absolute_path(clr_dir_c, clr_dir)) {
        log_error("Failed to get absolute clr path.");
        return -1;
    }

    std::string coreclr_dll((clr_dir));
    coreclr_dll.append(DIR_SEPARATOR);
    coreclr_dll.append(CORECLR_LIB);

    if (coreclr_dll.length() >= PATH_MAX)
    {
        log_error("Absolute path to libcoreclr.so too long.");
        return -1;
    }

    // Construct tpa
    std::string tpa_list;

    tpa_list = abs_exe_path;
    tpa_list.append(TPA_DELIMITER);

    construct_tpa(clr_dir.c_str(), tpa_list);
    add_deps_to_tpa(abs_exe_path, tpa_list);

    // Construct native search directory paths
    std::string plugins_dir;
    get_absolute_path("plugins", plugins_dir);
    std::string native_search_dirs(app_dir);
    native_search_dirs.append(TPA_DELIMITER);
    native_search_dirs.append(clr_dir);
    native_search_dirs.append(TPA_DELIMITER);
    native_search_dirs.append(plugins_dir);

#if SAMPSHARP_LINUX
    module_ = dlopen(coreclr_dll.c_str(), RTLD_NOW | RTLD_LOCAL);

    if(!module_) {
        const char *error = dlerror();
        log_error("Failed to load CoreCLR library: %s.", error);
        return -1;
    }

    if(!load_symbol(module_, "coreclr_initialize", (void **)&coreclr_initialize_)) {
        return -1;
    }
    if(!load_symbol(module_, "coreclr_shutdown", (void **)&coreclr_shutdown_)) {
        return -1;
    }
    if(!load_symbol(module_, "coreclr_shutdown_2", (void **)&coreclr_shutdown_2_)) {
        return -1;
    }
    if(!load_symbol(module_, "coreclr_create_delegate", (void **)&coreclr_create_delegate_)) {
        return -1;
    }
    if(!load_symbol(module_, "coreclr_execute_assembly", (void **)&coreclr_execute_assembly_)) {
        return -1;
    }

    const char *property_keys[] = {
        "TRUSTED_PLATFORM_ASSEMBLIES",
        "APP_PATHS",
        "APP_NI_PATHS",
        "NATIVE_DLL_SEARCH_DIRECTORIES",
        "System.GC.Server",
        "System.Globalization.Invariant",
    };
    const char *property_values[] = {
        // TRUSTED_PLATFORM_ASSEMBLIES
        tpa_list.c_str(),
        // APP_PATHS
        app_dir.c_str(),
        // APP_NI_PATHS
        app_dir.c_str(),
        // NATIVE_DLL_SEARCH_DIRECTORIES
        native_search_dirs.c_str(),
        // System.GC.Server
        "false",
        // System.Globalization.Invariant
        "false",
    };

    return coreclr_initialize_(
        abs_exe_path.c_str(),
        app_domain_friendly_name,
        sizeof(property_keys) / sizeof(property_keys[0]),
        property_keys,
        property_values,
        &host_,
        &domain_id_
    );
#elif SAMPSHARP_WINDOWS
    std::wstring wapp_dir = std::wstring(app_dir.begin(), app_dir.end());
    std::wstring wclr_dir = std::wstring(clr_dir.begin(), clr_dir.end());
    std::wstring wcoreclr_dll = std::wstring(coreclr_dll.begin(), coreclr_dll.end());
    std::string adfn = std::string(app_domain_friendly_name);
    std::wstring wapp_domain_friendly_name = std::wstring(adfn.begin(), adfn.end());

    module_ = LoadLibraryExW(wcoreclr_dll.c_str(), nullptr, 0);

	if (!module_)
	{
		log_error("CoreCLR.dll could not be found.");
		return -1;
	}


    const FnGetCLRRuntimeHost get_clr_runtime_host = FnGetCLRRuntimeHost(
        ::GetProcAddress(module_, "GetCLRRuntimeHost"));

	if (!get_clr_runtime_host)
	{
		log_error("GetCLRRuntimeHost not found.");
		return -1;
	}

	// Get the hosting interface
	HRESULT hr = get_clr_runtime_host(IID_ICLRRuntimeHost2, reinterpret_cast<IUnknown**>(&host_));

	if (FAILED(hr))
	{
		log_error("Failed to get ICLRRuntimeHost2 instance. Error code:%x.", hr);
		return -1;
	}


    hr = host_->SetStartupFlags(
		static_cast<STARTUP_FLAGS>(
			// STARTUP_SERVER_GC |								// Use server GC
			// STARTUP_LOADER_OPTIMIZATION_MULTI_DOMAIN |		// Maximize domain-neutral loading
			// STARTUP_LOADER_OPTIMIZATION_MULTI_DOMAIN_HOST |	// Domain-neutral loading for strongly-named assemblies
			STARTUP_CONCURRENT_GC |						// Use concurrent GC
			STARTUP_SINGLE_APPDOMAIN |					// All code executes in the default AppDomain
																		// (required to use the runtimeHost->ExecuteAssembly helper function)
			STARTUP_LOADER_OPTIMIZATION_SINGLE_DOMAIN	// Prevents domain-neutral loading
		)
	);

    if (FAILED(hr))
	{
		log_error("Failed to set startup flags. Error code:%x.", hr);
		return -1;
	}

	// Starting the runtime will initialize the JIT, GC, loader, etc.
	hr = host_->Start();
	if (FAILED(hr))
	{
		log_error("Failed to start the runtime. Error code:%x.", hr);
		return -1;
	}

    const int app_domain_flags =
		// APPDOMAIN_FORCE_TRIVIAL_WAIT_OPERATIONS |		// Do not pump messages during wait
		// APPDOMAIN_SECURITY_SANDBOXED |					// Causes assemblies not from the TPA list to be loaded as partially trusted
		APPDOMAIN_ENABLE_PLATFORM_SPECIFIC_APPS |			// Enable platform-specific assemblies to run
		APPDOMAIN_ENABLE_PINVOKE_AND_CLASSIC_COMINTEROP |	// Allow PInvoking from non-TPA assemblies
		APPDOMAIN_DISABLE_TRANSPARENCY_ENFORCEMENT;			// Entirely disables transparency checks

    // Setup key/value pairs for AppDomain  properties
	const wchar_t* propertyKeys[] = {
		L"TRUSTED_PLATFORM_ASSEMBLIES",
		L"APP_PATHS",
		L"APP_NI_PATHS",
		L"NATIVE_DLL_SEARCH_DIRECTORIES",
		L"PLATFORM_RESOURCE_ROOTS",
		L"AppDomainCompatSwitch"
	};

    std::wstring wtpa_list = std::wstring(tpa_list.begin(), tpa_list.end());
    std::wstring wnative_search_dirs = std::wstring(native_search_dirs.begin(), native_search_dirs.end());

	// Property values which were constructed in step 5
	const wchar_t* propertyValues[] = {
		wtpa_list.c_str(),
		wapp_dir.c_str(),
		wapp_dir.c_str(),
		wnative_search_dirs.c_str(),
		 wapp_dir.c_str(),
		L"UseLatestBehaviorWhenTFMNotSpecified"
	};


	// Create the AppDomain
	hr = host_->CreateAppDomainWithManager(
		wapp_domain_friendly_name.c_str(),
		app_domain_flags,
		nullptr,
		nullptr,
		sizeof(propertyKeys)/sizeof(wchar_t*),
		propertyKeys,
		propertyValues,
		&domain_id_);

	if (FAILED(hr))
	{
		log_error("Failed to create AppDomain. Error code:%x.", hr);
		return -1;
	}

    return 0;
#endif
}

int coreclr_app::construct_tpa(const char *directory, std::string &tpa_list) {
#if SAMPSHARP_LINUX
    const char * const tpaExtensions[] = {
                ".ni.dll",      // Probe for .ni.dll first so that it's preferred if ni and il coexist in the same dir
                ".dll",
                ".ni.exe",
                ".exe",
                };

    DIR* dir = opendir(directory);
    if (dir == nullptr)
    {
        log_error("Could not open directory for TPA construction.");
        return 1;
    }

    std::set<std::string> addedAssemblies;

    // Walk the directory for each extension separately so that we first get files with .ni.dll extension,
    // then files with .dll extension, etc.
    for (int extIndex = 0; extIndex < sizeof(tpaExtensions) / sizeof(tpaExtensions[0]); extIndex++)
    {
        const char* ext = tpaExtensions[extIndex];
        int extLength = strlen(ext);

        struct dirent* entry;

        // For all entries in the directory
        while ((entry = readdir(dir)) != nullptr)
        {
            // We are interested in files only
            switch (entry->d_type)
            {
            case DT_REG:
                break;

            // Handle symlinks and file systems that do not support d_type
            case DT_LNK:
            case DT_UNKNOWN:
                {
                    std::string fullFilename;

                    fullFilename.append(directory);
                    fullFilename.append("/");
                    fullFilename.append(entry->d_name);

                    struct stat sb;
                    if (stat(fullFilename.c_str(), &sb) == -1)
                    {
                        continue;
                    }

                    if (!S_ISREG(sb.st_mode))
                    {
                        continue;
                    }
                }
                break;

            default:
                continue;
            }

            std::string filename(entry->d_name);

            // Check if the extension matches the one we are looking for
            int extPos = filename.length() - extLength;
            if ((extPos <= 0) || (filename.compare(extPos, extLength, ext) != 0))
            {
                continue;
            }

            std::string filenameWithoutExt(filename.substr(0, extPos));

            // Make sure if we have an assembly with multiple extensions present,
            // we insert only one version of it.
            if (addedAssemblies.find(filenameWithoutExt) == addedAssemblies.end())
            {
                addedAssemblies.insert(filenameWithoutExt);

                tpa_list.append(directory);
                tpa_list.append("/");
                tpa_list.append(filename);
                tpa_list.append(TPA_DELIMITER);
            }
        }

        // Rewind the directory stream to be able to iterate over it for the next extension
        rewinddir(dir);
    }

    closedir(dir);
#elif SAMPSHARP_WINDOWS
    const wchar_t *tpaExtensions[] = {
		L"*.dll",
		L"*.exe",
		L"*.winmd"
	};

	for (int i = 0; i < _countof(tpaExtensions); i++)
	{
		// Construct the file name search pattern
		wchar_t searchPath[PATH_MAX];
        mbstowcs_s(NULL, searchPath, directory, PATH_MAX);
		wcscat_s(searchPath, PATH_MAX, L"\\");
		wcscat_s(searchPath, PATH_MAX, tpaExtensions[i]);

		// Find files matching the search pattern
		WIN32_FIND_DATAW find;
		HANDLE fhandle = FindFirstFileW(searchPath, &find);

		if (fhandle != INVALID_HANDLE_VALUE)
		{
			do
			{
                std::wstring wname = std::wstring(find.cFileName);

                tpa_list.append(std::string(directory));
                tpa_list.append(DIR_SEPARATOR);
                tpa_list.append(std::string(wname.begin(), wname.end()));
				tpa_list.append(TPA_DELIMITER);

                std::cout << std::string(wname.begin(), wname.end()) << std::endl;
			}
			while (FindNextFileW(fhandle, &find));
			FindClose(fhandle);
		}
	}
#endif
    return 0;
}

void coreclr_app::add_deps_to_tpa(std::string abs_exe_path,
    std::string &tpa_list) {
    if(!path_has_extension(abs_exe_path.c_str(), ".dll")) {
        return;
    }

    std::string runtimeconfig_path;
    std::string deps_path;

    // Find runtime config
    path_change_extension(abs_exe_path.c_str(), ".runtimeconfig.dev.json",
        runtimeconfig_path);

    if(!file_exists(runtimeconfig_path.c_str())) {
        path_change_extension(abs_exe_path.c_str(), ".runtimeconfig.json",
            runtimeconfig_path);
    }

    if(!file_exists(runtimeconfig_path.c_str())) {
        return;
    }

    // Find deps file
    path_change_extension(abs_exe_path.c_str(), ".deps.json", deps_path);
    if(!file_exists(deps_path.c_str())) {
        return;
    }

    log_debug("runtimeconfig path: %s", runtimeconfig_path.c_str());
    log_debug("deps path: %s", deps_path.c_str());

    // Read files
    std::ifstream runtimeconfig_stream(runtimeconfig_path);
    std::string runtimeconfig_json;

    runtimeconfig_stream.seekg(0, std::ios::end);   
    runtimeconfig_json.reserve((size_t)runtimeconfig_stream.tellg());
    runtimeconfig_stream.seekg(0, std::ios::beg);

    runtimeconfig_json.assign(
        std::istreambuf_iterator<char>(runtimeconfig_stream),
        std::istreambuf_iterator<char>());

    runtimeconfig_stream.close();

    std::ifstream deps_stream(deps_path);
    std::string deps_json;

    deps_stream.seekg(0, std::ios::end);   
    deps_json.reserve((size_t)deps_stream.tellg());
    deps_stream.seekg(0, std::ios::beg);

    deps_json.assign(
        std::istreambuf_iterator<char>(deps_stream),
        std::istreambuf_iterator<char>());

    deps_stream.close();

    // Find probing paths
    json::JSON runtimeconfig = json::JSON::Load(runtimeconfig_json);

    if(!runtimeconfig.hasKey("runtimeOptions") ||
        !runtimeconfig["runtimeOptions"].hasKey("additionalProbingPaths")) {
        return;
    }

    json::JSON probes =
        runtimeconfig["runtimeOptions"]["additionalProbingPaths"];

    if(!JSON_IS(probes, Array)) {
        return;
    }

    std::vector<std::string> probing_paths;
    for(const auto& path_obj : probes.ArrayRange()) {
        std::string path = path_obj.ToString();
        if(dir_exists(path.c_str())) {
            probing_paths.push_back(path);

            log_debug("Probe path found: %s", path.c_str());
        }
    }

    // Find dependencies
    json::JSON deps = json::JSON::Load(deps_json);

    if(!JSON_KEY_IS(deps, "runtimeTarget", Object) ||
        !JSON_KEY_IS(deps, "targets", Object) ||
        !JSON_KEY_IS(deps, "libraries", Object) ||
        !JSON_KEY_IS(deps["runtimeTarget"], "name", String)) {
        return;
    }

    std::string target_name = deps["runtimeTarget"]["name"].ToString();

    if(!deps["targets"].hasKey(target_name)) {
        return;
    }

    json::JSON target = deps["targets"][target_name];

    for(auto obj : target.ObjectRange()) {
        if(!JSON_IS(obj.second, Object) ||
            !JSON_KEY_IS(obj.second, "runtime", Object) ||
            !JSON_KEY_IS(deps["libraries"], obj.first, Object) ||
            !JSON_KEY_IS(deps["libraries"][obj.first], "type", String) ||
            deps["libraries"][obj.first]["type"].ToString() != "package" ||
            !JSON_KEY_IS(deps["libraries"][obj.first], "path", String)
            ) {
            continue;
        }

        std::string lib_path = deps["libraries"][obj.first]["path"].ToString();
        

        for(const auto& runtime_obj : obj.second["runtime"].ObjectRange()) {
            for(const auto& probing_path : probing_paths) {
                std::string path;
                path_append(probing_path.c_str(), lib_path.c_str(), path);
                path_append(path.c_str(), runtime_obj.first.c_str(), path);

                if(file_exists(path.c_str())) {
                    log_debug("Found dependency %s", path.c_str());

                    tpa_list.append(path);
                    tpa_list.append(TPA_DELIMITER);
                }
            }
        }
    }
}

int coreclr_app::release()
{
    int retval = -1;
#if SAMPSHARP_LINUX
    if(coreclr_shutdown_) {
        retval = coreclr_shutdown_(host_, domain_id_);
    }

    if(module_) {
        dlclose(module_);
        module_ = NULL;
    }

#elif SAMPSHARP_WINDOWS
    // TODO? Other errors???
    host_->UnloadAppDomain(domain_id_, true);
	host_->Stop();
	retval = host_->Release();

    if(module_) {
        FreeLibrary(module_);
        module_ = NULL;
    }
#endif

    return retval;
}

int coreclr_app::create_delegate(const char* assembly_name,
    const char* type_name, const char* method_name, void** delegate) {
#if SAMPSHARP_LINUX
    if(!coreclr_create_delegate_) {
        return -1;
    }

    return coreclr_create_delegate_(host_, domain_id_, assembly_name,
        type_name, method_name, delegate);
#elif SAMPSHARP_WINDOWS

    std::string an = std::string(assembly_name);
    std::string tn = std::string(type_name);
    std::string mn = std::string(method_name);

    std::wstring wan = std::wstring(an.begin(), an.end());
    std::wstring wtn = std::wstring(tn.begin(), tn.end());
    std::wstring wmn = std::wstring(mn.begin(), mn.end());

    return  host_->CreateDelegate(domain_id_, wan.c_str(), wtn.c_str(), wmn.c_str(), (INT_PTR*)delegate);
#endif
}

int coreclr_app::execute_assembly(int argc, const char** argv, unsigned int* exit_code) {
#if SAMPSHARP_LINUX
    if(!coreclr_execute_assembly_) {
        return -1;
    }

    return coreclr_execute_assembly_(host_, domain_id_, argc, argv, abs_exe_path_.c_str(), exit_code);
#elif SAMPSHARP_WINDOWS
    std::wstring wabs_exe_path = std::wstring(abs_exe_path_.begin(), abs_exe_path_.end());

    // Convert args from single to wide chars.
    size_t pos = 0;
    size_t written;
    size_t len = argc;

    for(int i = 0; i < argc; i++) {
        len += strlen(argv[i]);
    }
    
    wchar_t *wargs = new wchar_t[len];
    wchar_t **wargv = new wchar_t*[argc];

    for(int i = 0; i < argc; i++) {
        wargs[pos] = L'\0';
        mbstowcs_s(&written, wargs + pos, len - pos, argv[i], len - pos);

        wargv[i] = wargs + pos;
        pos += written;
    }

    // Execute
	DWORD dexit_code = -1;
    const DWORD retval = host_->ExecuteAssembly(domain_id_, wabs_exe_path.c_str(), argc, (const wchar_t **)wargv, &dexit_code);

    *exit_code = dexit_code;

    delete[] wargs;
    delete[] wargv;

    return retval;
#endif
}
