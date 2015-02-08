// SampSharp
// Copyright 2015 Tim Potze
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

#include "MonoRuntime.h"
#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/utils/mono-logger.h>
#include <sampgdk/sampgdk.h>

bool MonoRuntime::isLoaded_;

void MonoRuntime::Load(std::string assemblyDir, std::string configDir,
    std::string traceLevel, std::string file) {
    if (isLoaded_) {
        return;
    }

    if (!assemblyDir.empty() && !configDir.empty()) {
        mono_set_dirs(assemblyDir.c_str(), configDir.c_str());
    }
#ifdef _WIN32
    else {
        mono_set_dirs(PathUtil::GetLibDirectory().c_str(),
            PathUtil::GetConfigDirectory().c_str());
    }
#endif

    mono_trace_set_level_string(traceLevel.c_str());
    MonoDomain *dom = mono_jit_init(file.c_str());

    isLoaded_ = true;
}

void MonoRuntime::Unload() {
    if (!isLoaded_) {
        return;
    }

    /* For some reason, the process crashes when trying to cleanup mono.
     * For now, lets just not cleanup.
     */
    // mono_jit_cleanup(mono_domain_get());

    isLoaded_ = false;
}