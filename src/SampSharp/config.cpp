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

#include "config.h"
#include "strutil.h"

bool config::get_config_bool(std::string name, bool& result) {
    std::string value;
    if (!get_config_string(name, value)) {
        return false;
    }

    if (iequals(value, "on") || iequals(value, "yes") || iequals(value, "true")) {
        result = true;
        return true;
    }
    if (iequals(value, "off") || iequals(value, "no") || iequals(value, "false")) {
        result = false;
        return true;
    }

    result = !!atoi(value.c_str());
    return true;
}
