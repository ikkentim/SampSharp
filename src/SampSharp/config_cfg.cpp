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

#include "config_cfg.h"
#include <fstream>
#include <sstream>

static void trim(std::string& s) {
    s.erase(s.begin(), std::find_if(s.begin(), s.end(), [](unsigned char ch) {
        return !std::isspace(ch);
    }));

    s.erase(std::find_if(s.rbegin(), s.rend(), [](unsigned char ch) {
        return !std::isspace(ch);
    }).base(), s.end());
}

config_cfg::config_cfg() {
    std::ifstream file("server.cfg");

    if (file.is_open()) {
        std::string line, name, value;

        while (std::getline(file, line, '\n')) {
            std::stringstream stream(line);

            std::getline(stream, name, ' ');
            std::getline(stream, value, '\n');

            trim(name);
            trim(value);

            values_.insert(std::make_pair(name, value));
        }
    }
}

bool config_cfg::get_config_string(std::string name, std::string& result) {
    const auto iterator = values_.find(name);

    if (iterator == values_.end()) {
        return false;
    }

    result = iterator->second;
    return true;
}
