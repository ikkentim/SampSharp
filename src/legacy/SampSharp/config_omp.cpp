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

#include "config_omp.h"

#include <fstream>
#include <json/json.hpp>

config_omp::config_omp() {
    // read config file as json
    std::ifstream input_file("config.json");
    if (!input_file.is_open()) {
        return;
    }
    const auto str = std::string((std::istreambuf_iterator(input_file)), std::istreambuf_iterator<char>());

    nlohmann::json json = nlohmann::json::parse(str);

    if(!json.is_object()) {
        return;
    }

    // map the json to a simple flat structure like regular SA-MP config file. We'll only be using a few
    // keys in SampSharp. If open.mp will provide a different config API in their SDK we'll use that
    // instead of the json file, once the SDK is released.
    for (auto it = json.begin(); it != json.end(); ++it) {
        switch (it.value().type()) {
            
        case nlohmann::detail::value_t::string:
            values_[it.key()] = it.value().get<std::string>();
            break;
        case nlohmann::detail::value_t::boolean:
            values_[it.key()] = it.value().get<bool>() ? "1" : "0";
            break;
        case nlohmann::detail::value_t::number_integer:
            values_[it.key()] = std::to_string(it.value().get<long>());
            break;
        case nlohmann::detail::value_t::number_unsigned:
            values_[it.key()] = std::to_string(it.value().get<unsigned long>());
            break;
        case nlohmann::detail::value_t::number_float:
            values_[it.key()] = std::to_string(it.value().get<float>());
            break;
        default: ;
        }
    }

    
    // Map the main_scripts (pawn game modes) into gamemodeN keys.
    auto main_scripts = json["pawn"]["main_scripts"];
    int index = 0;
    for (auto it = main_scripts.begin(); it != main_scripts.end(); ++it) {
        if(it.value().is_string()) {
            std::string key = "gamemode" + std::to_string(index++);
            values_[key] = it.value().get<std::string>();
        }
    }
}

bool config_omp::get_config_string(std::string name, std::string& result) {
    const auto iterator = values_.find(name);

    if (iterator == values_.end()) {
        return false;
    }

    result = iterator->second;
    return true;
}
