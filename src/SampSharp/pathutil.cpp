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

#include "pathutil.h"
#include "platforms.h"
#include <assert.h>
#include <string.h>
#if SAMPSHARP_WINDOWS
#  include "Windows.h"
#  include "Shlwapi.h"
#elif SAMPSHARP_LINUX
#  include <unistd.h>
#  include <dirent.h>
#endif

void path_append(const char *path, const char *append, std::string &result) {
    if(!path && !append) {
        return;
    }

    if(!path || strlen(path) == 0) {
        result.assign(append);
    }
    else if(!append || strlen(append) == 0) {
        result.assign(path);
    }
    else if(path[strlen(path) - 1] == DIR_SEPARATOR[0] || append[0] == DIR_SEPARATOR[0])
    {
        result.assign(path);
        result.append(append);
    } 
    else {
        result.assign(path);
        result.append(DIR_SEPARATOR);
        result.append(append);
    }
}

bool dir_exists(const char *path) {
#if SAMPSHARP_WINDOWS
  DWORD attrib = GetFileAttributes(path);
  return attrib != INVALID_FILE_ATTRIBUTES && 
         (attrib & FILE_ATTRIBUTE_DIRECTORY);
#elif SAMPSHARP_LINUX
    DIR* dir = opendir(path);
    if (dir) {
        closedir(dir);
        return true;
    }
    else {
        return false;
    }
#endif
}
bool file_exists(const char *path) {
#if SAMPSHARP_WINDOWS
    return PathFileExists(path);
#elif SAMPSHARP_LINUX
    return access(path, F_OK) != -1;
#endif
}

bool get_absolute_path(const char *path, std::string &absolute_path) {
#if SAMPSHARP_LINUX
    char real_path[PATH_MAX];
    if (realpath(path, real_path) != nullptr && real_path[0] != '\0')
    {
        absolute_path.assign(real_path);
        // realpath should return canonicalized path without the trailing slash
        assert(absolute_path.back() != '/');

        return true;
    }

    return false;
#elif SAMPSHARP_WINDOWS

    wchar_t wreal_path[MAX_PATH];
    wchar_t wpath[MAX_PATH];
    mbstowcs_s(nullptr, wpath, path, MAX_PATH);

	GetFullPathNameW(wpath, MAX_PATH, wreal_path, nullptr);

    std::wstring wreal_path_s(wreal_path);
    absolute_path = std::string(wreal_path_s.begin(), wreal_path_s.end());

    return true;
#endif
}

bool get_directory(const char* absolute_path, std::string& directory) {
    directory.assign(absolute_path);

    size_t fwd = directory.rfind('/');
    size_t bwd = directory.rfind('\\');
    size_t last_slash;
    
    if(fwd == std::string::npos)
    {
        last_slash = bwd;
    }
    else if(bwd == std::string::npos)
    {
        last_slash = fwd;
    }
    else
    {
        last_slash = fwd > bwd ? fwd : bwd;
    }


    if (last_slash != std::string::npos)
    {
        directory.erase(last_slash);
        return true;
    }

    return false;
}
