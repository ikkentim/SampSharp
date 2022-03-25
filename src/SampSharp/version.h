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

#define PLUGIN_VERSION_MAJOR        0
#define PLUGIN_VERSION_MINOR        10
#define PLUGIN_VERSION_PATCH        0
#define PLUGIN_VERSION_ALPHA        2
#define PLUGIN_PROTOCOL_VERSION     5

#define __PLUGIN_STRINGIZE(x)       #x
#define __PLUGIN_STRINGIZEX(x)      __PLUGIN_STRINGIZE(x)

#define __PLUGIN_VERSION_STR        \
    __PLUGIN_STRINGIZEX(PLUGIN_VERSION_MAJOR) "." \
    __PLUGIN_STRINGIZEX(PLUGIN_VERSION_MINOR) "." \
    __PLUGIN_STRINGIZEX(PLUGIN_VERSION_PATCH)

#if PLUGIN_VERSION_ALPHA > 0
#  define PLUGIN_VERSION_STR          __PLUGIN_VERSION_STR "-alpha" \
    __PLUGIN_STRINGIZEX(PLUGIN_VERSION_ALPHA)
#else
#  define PLUGIN_VERSION_STR          __PLUGIN_VERSION_STR
#endif

#define PLUGIN_VERSION              ( \
                                        (PLUGIN_VERSION_MAJOR << 16) | \
                                        (PLUGIN_VERSION_MINOR << 8) | \
                                        (PLUGIN_VERSION_PATCH) | \
                                        (PLUGIN_VERSION_ALPHA << 24))
