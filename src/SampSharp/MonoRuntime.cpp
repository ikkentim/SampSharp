// SampSharp
// Copyright 2016 Tim Potze
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
#include <mono/metadata/mono-debug.h>
#include <mono/utils/mono-logger.h>
#include <sampgdk/sampgdk.h>
#include "Config.h"

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

    if (Config::GetDebuggerEnable().compare("1") == 0) {
        char* agent = new char[128];
        sprintf(agent, "--debugger-agent=transport=dt_socket,address=%s,server=y", Config::GetDebuggerAddress().c_str());

        sampgdk::logprintf("Launching with debugger at %s...", Config::GetDebuggerAddress().c_str());

        const char* jit_options[] = {
            "--soft-breakpoints",
            agent
        };
        mono_jit_parse_options(2, (char**)jit_options);

        delete agent;

        sampgdk::logprintf("Waiting for debugger to attach...");
    }

    mono_debug_init(MONO_DEBUG_FORMAT_MONO);
    mono_trace_set_level_string(traceLevel.c_str());
    MonoDomain *dom = mono_jit_init(file.c_str());

    isLoaded_ = true;
}
