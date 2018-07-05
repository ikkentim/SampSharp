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

#include <sampgdk/sampgdk.h>

class server {
public:
    /** frees memory allocated by this instance */
    virtual ~server() {}
    /** called when a server tick occurs */
    virtual void tick() = 0;
    /** called when a public call is send from the server */
    virtual void public_call(AMX *amx, const char *name, cell *params,
        cell *retval) = 0;
};
