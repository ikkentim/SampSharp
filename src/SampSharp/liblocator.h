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
#include <filesystem>

namespace fs = std::filesystem;

class liblocator
{
public:
    bool locate(const std::string& hostfxr_hint, const std::string& gamemode_dir_hint, const std::string& gamemode);

    fs::path get_hostfxr();
    fs::path get_gamemode();
    
private:
    bool detect_hostfxr(fs::path &result, const fs::path& search_path);
    bool detect_hostfxr(fs::path &result, const std::string &path_hint);
    bool detect_gamemode(fs::path &result, const std::string &search_name, const fs::path &search_path);
	bool detect_gamemode(const std::string &dir_hint, const std::string &name, fs::path &result);
    
    fs::path hostfxr_;
    fs::path gamemode_;
};
