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

#include <map>
#include <string>
#include <inttypes.h>
#include <sampgdk/sampgdk.h>

class remote_server;

class callbacks_map
{
public:
    callbacks_map();
    void clear();
    void register_buffer(uint8_t *buf);
    uint32_t fill_call_buffer(AMX *amx, const char *name, cell *params, 
        uint8_t *buf, uint32_t len, bool include_name);
private:
    std::map<std::string, uint8_t*> callbacks_;
};
