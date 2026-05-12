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

#include "config_composite.h"
#include <utility>

void config_composite::add_config(config* config) {
    configs_.push_back(config);
}

bool config_composite::get_config_string(std::string name, std::string& result) {
    for (auto && config : configs_) {
        if(config->get_config_string(name, result)) {
            return true;
        }
    }

    return false;
}

bool config_composite::get_config_bool(std::string name, bool& result) {
    for (auto && config : configs_) {
        if(config->get_config_bool(name, result)) {
            return true;
        }
    }

    return false;
}
