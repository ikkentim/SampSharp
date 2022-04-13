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

#include "config_win.h"

#include <algorithm>
#include <sstream>

#include "platforms.h"

#if SAMPSHARP_WINDOWS
#include <Windows.h>

std::string wide_to_string(wchar_t *wc) {
    std::wstring wide = wc;
    std::string str(wide.length(), 0);

    std::transform(wide.begin(), wide.end(), str.begin(), [] (wchar_t c) {
        return (char)c;
    });

    return str;
}

#endif

config_win::config_win(config* inner) : inner_(inner) {
#if SAMPSHARP_WINDOWS
    int argc;
    wchar_t** argv = ::CommandLineToArgvW(::GetCommandLineW(), &argc);

    for (int i = 0; i < argc; i++)
    {
        auto key = wide_to_string(argv[i]);

        if(key.find("--") != 0 || key.length() <= 2) {
            continue;
        }

        key = key.substr(2);
        
        const size_t eq = key.find('=');

        if(eq != std::string::npos){
            const auto value = key.substr(eq + 1);
            key = key.substr(0, eq);
            values_.insert(std::make_pair(key, value));
        }
        else if (argc > i + 1){ 
            const auto value = wide_to_string(argv[++i]);
            values_.insert(std::make_pair(key, value));
        }
    }
#endif
}

bool config_win::get_config_string(std::string name, std::string& result) {
    const auto iterator = values_.find(name);

    if (iterator == values_.end()) {
        return inner_->get_config_string(name, result);
    }

    result = iterator->second;
    return true;
}
