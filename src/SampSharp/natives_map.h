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

#include <inttypes.h>
#include <vector>
#include <string>
#include <map>
#include <sampgdk/sampgdk.h>

#define NATIVE_NOT_FOUND        -1

class remote_server;

class natives_map
{
public:
    int32_t get_handle(const char *name);
    void invoke(uint8_t *rxbuf, uint32_t rxlen, uint8_t *txbuf, uint32_t *txlen);
    void clear();
private:
    std::vector<AMX_NATIVE> natives_;
    std::map<std::string,int32_t> natives_map_;
};
