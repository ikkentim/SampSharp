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
#include "commsvr.h"

#define STATE_NONE          0x00
#define STATE_CONFIG_VALID  0x01
#define STATE_HOSTED        0x02
#define STATE_SWAPPING      0x04
#define STATE_INITIALIZED   0x08

typedef uint8_t plugin_state;

class plugin
{
public:
    plugin(void **pp_data);
    int filterscript_call(const char *function_name) const;
    ConfigReader *config();
    void config(const std::string &name, std::string &value) const;
    bool config_validate();
    commsvr *create_commsvr() const;

    plugin_state state() const;
    plugin_state state_set(plugin_state flag);
    plugin_state state_unset(plugin_state flag);
    plugin_state state_reset();
    std::string *get_coreclr();
    std::string *get_gamemode();

    int amx_load(AMX *amx);

private:
    bool detect_coreclr(std::string &value);
    bool detect_coreclr(std::string &value, std::filesystem::path path);
    bool detect_gamemode(std::string &value);
    bool detect_gamemode(std::string &value, std::filesystem::path path);
    void** data_;
    ConfigReader config_;
    plugin_state state_ = STATE_NONE;
    std::string coreclr_;
    std::string gamemode_;
};
