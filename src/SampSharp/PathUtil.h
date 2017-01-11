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

#include "platforms.h"
#if SAMPSHARP_WINDOWS
#include <direct.h>
#define getcwd _getcwd // stupid MSFT "deprecation" warning
#elif SAMPSHARP_LINUX
#include <unistd.h>
#endif
#include <string>

#pragma once

struct PathUtil
{
    static std::string GetBinDirectory()
    {
        #if SAMPSHARP_WINDOWS
        std::string s_cwd(getcwd(NULL, 0));
        return s_cwd.append("/");
        #elif SAMPSHARP_LINUX
        return "./";
        #endif
    }

    static std::string GetPathInBin(std::string append)
    {
        return GetBinDirectory().append(append);
    }

    static std::string GetMonoDirectory()
    {
        return GetPathInBin("mono/");
    }

    static std::string GetLibDirectory()
    {
        return GetMonoDirectory().append("lib/");
    }

    static std::string GetConfigDirectory()
    {
        return GetMonoDirectory().append("etc/");
    }

    static std::string GetGameModeDirectory()
    {
        return GetPathInBin("gamemode/");
    }
};
