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

#include "liblocator.h"
#include "logging.h"
#include "StringUtil.h"
#include <regex>

#include "ConfigReader.h"

#define HOSTFXR_LIB "hostfxr.dll" // todo multiplat

bool liblocator::locate(const std::string &hostfxr_hint, const std::string &gamemode_dir_hint, const std::string &gamemode) {
    // Verify hosting config values.
    if(!detect_hostfxr(hostfxr_, hostfxr_hint)) {
    	log_error(HOSTFXR_LIB " could not be found.");
        return false;
    }

    if(!detect_gamemode(gamemode_dir_hint, gamemode, gamemode_)) {
        log_error("Invalid gamemode specified in server config.");
        return false;
    }
    
    log_info("Host FX resolver path: %s", hostfxr_.string().c_str());
    log_info("Game mode path: %s", gamemode_.string().c_str());
        
    return true; 
}

fs::path liblocator::get_hostfxr() {
    return hostfxr_;
}

fs::path liblocator::get_gamemode() {
    return gamemode_;
}

bool liblocator::detect_hostfxr(fs::path &result, const fs::path& search_path) {
	if(!is_directory(search_path)) {
		return false;
    }
    
    log_debug("Checking for hostfxr in %s...", search_path.string().c_str());

    // Check in current directory
    const fs::path check_path = search_path / HOSTFXR_LIB;
    if(exists(check_path)) {
        result = check_path;
        return true;
    }

    // Check every subdirectory
    for (const auto &entry : fs::directory_iterator(search_path)) {
        if(entry.is_directory() && detect_hostfxr(result, entry.path())) {
            return true;
        }
    }

    return false;
}

bool liblocator::detect_hostfxr(fs::path &result, const std::string &path_hint) {
    if(!path_hint.empty() && StringUtil::EndsWith(path_hint, HOSTFXR_LIB)) {
		const fs::path hint = path_hint;
        if(exists(hint)) {
            result = hint;
	        return true;
        }
    }

	return
        detect_hostfxr(result, fs::path("runtime")) ||
        detect_hostfxr(result, fs::path("dotnet"));
}

bool liblocator::detect_gamemode(fs::path &result, const std::string &search_name, const fs::path &search_path) {
	if(!is_directory(search_path)) {
		return false;
    }
    
    log_debug("Checking for game mode in %s...", search_path.string().c_str());

    // Check in current directory
    for (const auto &entry : fs::directory_iterator(search_path)) {
        auto lower_filename = StringUtil::ToLower(entry.path().filename().string());
        if(lower_filename == search_name) {
            std::string rc_path = entry.path().string();
            fs::path dll_path = rc_path.substr(0, rc_path.length() - 19) + ".dll"; // .runtimeconfig.json -> .dll

            if(exists(dll_path)) {
	            result = dll_path;
	            return true;
            }
        }
    }

    // If directory contains subdirectories for different runtime versions, pick the newest
    std::regex const regex_version {R"(^net[coreapp]?([0-9])+\.([0-9])+$)"}; 
 
    int best_version_number = 0;
    fs::path best_version;
    for (const auto & entry : fs::directory_iterator(search_path)) {
        if(!entry.is_directory()) {
            continue;
        }

        fs::path entry_path = entry.path();
        std::string entry_dirname = entry_path.filename().string();
        std::smatch match;
        if(std::regex_match(entry_dirname, match, regex_version)) {
            int version_number = stoi(match[1]) * 1000 + stoi(match[2]);

            if(version_number > best_version_number) {
                best_version = entry_path;
                best_version_number = version_number;
            }
        }
    }
    
    if(!best_version.empty() && detect_gamemode(result, search_name, best_version)) {
        return true;
    }

    // Check every subdirectory
    for (const auto & entry : fs::directory_iterator(search_path)) {
        if(entry.is_directory() && detect_hostfxr(result, entry.path())) {
            return true;
        }
    }

    return false;	
}

bool liblocator::detect_gamemode(const std::string &dir_hint, const std::string &name, fs::path &result) {
    const auto search_name = StringUtil::ToLower(name + ".runtimeconfig.json");

	return
		(dir_hint.length() > 0 && detect_gamemode(result, search_name, dir_hint)) ||
        detect_gamemode(result, search_name, "gamemode") ||
        detect_gamemode(result, search_name, "gamemodes");
}
