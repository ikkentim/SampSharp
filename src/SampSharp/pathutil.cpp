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

#if !defined(PATH_MAX) && defined(MAX_PATH)
#  define PATH_MAX MAX_PATH
#endif

#ifdef __DIR_SEPARATOR_FORWARD
#  define NOT_DIR_SEPARATOR "\\"
#else
#  define NOT_DIR_SEPARATOR "/"
#endif

bool path_has_extension(const char *path, const char *ext) {
    if(!path || !ext) {
        return false;
    }

    size_t path_len = strlen(path);
    size_t ext_len = strlen(ext);

    if(path_len < ext_len) {
        return false;
    }

    return _strcmpi(path + (path_len - ext_len), ext) == 0;
}

void path_change_extension(const char *path, const char *ext, std::string &result) {
    if(!path || !ext) {
        return;
    }

    result.assign(path);

    // Remove extension
    size_t last_dot = result.find_last_of('.');
    if(last_dot != std::string::npos) {
        result = result.substr(0, last_dot);
    }

    result.append(ext);
}

void path_append(const char *path, const char *append, std::string &result) {
    // Normalize path
    std::string tmp;
    get_absolute_path(path, tmp);

    // Append
    if(!append) {
        result = tmp;
        return;
    }

    std::string appends = std::string(append);

    if(appends.length() > 0 && appends[0] == NOT_DIR_SEPARATOR[0]) {
        appends[0] = DIR_SEPARATOR[0];
    }

    if(appends[0] != DIR_SEPARATOR[0]) {
        tmp.append(DIR_SEPARATOR);
    }

    tmp.append(appends);

    // Normalize with supplement
    get_absolute_path(tmp.c_str(), result);
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
    char cpath[PATH_MAX];

#if SAMPSHARP_LINUX
    strcpy(cpath, path);
#elif SAMPSHARP_WINDOWS
    strcpy_s(cpath, path);
#endif

    for(int i = strlen(cpath) - 1; i >= 0; i--) {
        if(cpath[i] == NOT_DIR_SEPARATOR[0]) {
            cpath[i] = DIR_SEPARATOR[0];
        }
    }

#if SAMPSHARP_LINUX
    char real_path[PATH_MAX];

    if (realpath(cpath, real_path) != nullptr && real_path[0] != '\0') {
        absolute_path.assign(real_path);
        // realpath should return canonicalized path without the trailing slash
        assert(absolute_path.back() != DIR_SEPARATOR[0]);

        return true;
    }

    return false;
#elif SAMPSHARP_WINDOWS

    wchar_t wreal_path[PATH_MAX];
    wchar_t wpath[PATH_MAX];
    mbstowcs_s(nullptr, wpath, cpath, PATH_MAX);

	GetFullPathNameW(wpath, PATH_MAX, wreal_path, nullptr);

    std::wstring wreal_path_s(wreal_path);
    absolute_path = std::string(wreal_path_s.begin(), wreal_path_s.end());

    // not sure if GetFullPathNameW could return a trailing slash
    if(absolute_path.back() == DIR_SEPARATOR[0]) {
        absolute_path.pop_back();
    }
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
