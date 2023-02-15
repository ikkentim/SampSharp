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

#include "locator.h"
#include "logging.h"
#include "strutil.h"
#include <regex>

#ifndef strlen
#include <cstring>
#endif

#if SAMPSHARP_WINDOWS
#define HOSTFXR_LIB "hostfxr.dll"
#  define CORECLR_LIB "coreclr.dll"
#elif SAMPSHARP_LINUX
#define HOSTFXR_LIB "libhostfxr.so"
#  define CORECLR_LIB "libcoreclr.so"
#endif

locator::locator(config* cfg) : cfg_(cfg) {}

fs::path locator::get_hostfxr() const {
    std::string hint;
    fs::path result;

    cfg_->get_config_string("coreclr", hint) ||
        cfg_->get_config_string("runtime", hint);

    detect_lib(result, hint, HOSTFXR_LIB);

    return result;
}

fs::path locator::get_coreclr() const {
    std::string hint;
    fs::path result;
    cfg_->get_config_string("runtime", hint);

    detect_lib(result, hint, CORECLR_LIB);

    return result;
}

fs::path locator::get_gamemode() const {
    std::string base, gamemode;
    fs::path result;
    cfg_->get_config_string("gamemode", gamemode);
    cfg_->get_config_string("gamemode_base", base);

    detect_gamemode(base, gamemode, result);

    return result;
}

bool locator::detect_lib_recursive(fs::path& result, const fs::path& search_path, const std::string& lib) {
    if (!is_directory(search_path)) {
        return false;
    }

    log_debug("Checking for lib in %s...", search_path.string().c_str());

    // Check in current directory
    const fs::path check_path = search_path / lib;
    if (exists(check_path)) {
        result = check_path;
        return true;
    }

    // Check every subdirectory
    for (const auto& entry : fs::directory_iterator(search_path)) {
        if (entry.is_directory() && detect_lib_recursive(result, entry.path(), lib)) {
            return true;
        }
    }

    return false;
}

bool locator::detect_lib(fs::path& result, const std::string& path_hint, const std::string& lib) {
    if (!path_hint.empty()) {
        const fs::path hint = path_hint;


        if (hint.filename() == lib && exists(hint)) {
            result = hint;
            return true;
        }

        if (is_directory(hint) && detect_lib_recursive(result, fs::path("runtime"), lib)) {
            return true;
        }
    }

    const auto runtime = getenv("SAMPSHARP_RUNTIME");

    return
        runtime && strlen(runtime) > 0 && detect_lib_recursive(result, fs::path(runtime), lib) ||
        detect_lib_recursive(result, fs::path("runtime"), lib) ||
        detect_lib_recursive(result, fs::path("dotnet"), lib);
}

bool is_runtime_config(const fs::path& path, const std::string& search_name) {
    if(search_name.length() == 0) {
        return path.extension() == ".json" && path.stem().extension() == ".runtimeconfig";
    }

    return iequals(path.filename().string(), search_name);
}

bool locator::detect_gamemode_recursive(fs::path& result, const std::string& search_name, const fs::path& search_path) {
    if (!is_directory(search_path)) {
        return false;
    }

    log_debug("Checking for game mode in %s...", search_path.string().c_str());

    // Check in current directory
    for (const auto& entry : fs::directory_iterator(search_path)) {

        if (entry.is_regular_file() && is_runtime_config(entry.path(), search_name)) {
            fs::path rc_path = entry.path();
            fs::path dll_path = rc_path
                                .replace_extension("")
                                .replace_extension(".dll"); // .runtimeconfig.json -> .dll

            if (exists(dll_path)) {
                result = dll_path;
                return true;
            }
        }
    }

    // If directory contains subdirectories for different runtime versions, pick the newest
    std::regex const regex_version{R"(^net[coreapp]?([0-9])+\.([0-9])+$)"};

    int best_version_number = 0;
    fs::path best_version;
    for (const auto& entry : fs::directory_iterator(search_path)) {
        if (!entry.is_directory()) {
            continue;
        }

        fs::path entry_path = entry.path();
        std::string entry_dirname = entry_path.filename().string();
        std::smatch match;
        if (std::regex_match(entry_dirname, match, regex_version)) {
            int version_number = stoi(match[1]) * 1000 + stoi(match[2]);

            if (version_number > best_version_number) {
                best_version = entry_path;
                best_version_number = version_number;
            }
        }
    }

    if (!best_version.empty() && detect_gamemode_recursive(result, search_name, best_version)) {
        return true;
    }

    // Check every subdirectory
    for (const auto& entry : fs::directory_iterator(search_path)) {
        if (entry.is_directory() && detect_gamemode_recursive(result, search_name, entry.path())) {
            return true;
        }
    }

    return false;
}

bool locator::detect_gamemode(const std::string& dir_hint, const std::string& name, fs::path& result) {
    const auto search_name = name.length() == 0
                                 ? ""
                                 : name + ".runtimeconfig.json";

    return
        (dir_hint.length() > 0 && detect_gamemode_recursive(result, search_name, dir_hint)) ||
        detect_gamemode_recursive(result, search_name, "gamemode") ||
        detect_gamemode_recursive(result, search_name, "gamemodes");
}
