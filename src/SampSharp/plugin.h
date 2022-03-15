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
#include <inttypes.h>
#include <sampgdk/sampgdk.h>
#include "ConfigReader.h"

class plugin
{
public:
    plugin();
    ConfigReader *config();
    void config(const std::string &name, std::string &value) const;
    bool config_validate();
    bool is_running() const;
    bool is_config_valid() const;

    std::string *get_coreclr();
    std::string *get_gamemode();
    
private:
    bool detect_coreclr(std::string &value);
    bool detect_coreclr(std::string &value, std::filesystem::path path);
    bool detect_gamemode(std::string &value);
    bool detect_gamemode(std::string &value, std::filesystem::path path);

    bool configvalid_;
    bool running_;
    ConfigReader config_;
    std::string coreclr_;
    std::string gamemode_;
};
