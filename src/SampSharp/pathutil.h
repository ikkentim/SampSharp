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
#include "platforms.h"

#if SAMPSHARP_LINUX
#  define DIR_SEPARATOR "/"
#elif SAMPSHARP_WINDOWS
#  define DIR_SEPARATOR "\\"
#endif

void path_append(const char *path, const char *append, std::string &result);
bool dir_exists(const char *path);
bool file_exists(const char *path);
bool get_directory(const char *absolute_path, std::string &directory);
bool get_absolute_path(const char* path, std::string &absolute_path);
