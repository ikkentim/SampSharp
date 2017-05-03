// SampSharp
// Copyright 2017 Tim Potze
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

#include "plugin.h"
#include <sampgdk/sampgdk.h>

/* amxplugin's reference */
extern void *pAMXFunctions;

typedef int(*amx_call)(char *function_name);

plugin::plugin(void **pp_data) :
    config_(ConfigReader("server.cfg")) {
    data_ = pp_data;
    pAMXFunctions = pp_data[PLUGIN_DATA_AMX_EXPORTS];

}

ConfigReader *plugin::config() {
    return &config_;
}

int plugin::filterscript_call(const char * function_name) {
    return ((amx_call)data_[PLUGIN_DATA_CALLPUBLIC_FS])(function_name);
}
