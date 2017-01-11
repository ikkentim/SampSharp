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

#include <string.h>
#include "Config.h"
#include "ConfigReader.h"
#include "StringUtil.h"
#include "PathUtil.h"
#include "platforms.h"

using std::string;

string Config::monoAssemblyDir_;
string Config::monoConfigDir_;
string Config::traceLevel_;
string Config::gameModeNamespace_;
string Config::gameModeClass_;
string Config::codepage_;
string Config::debuggerEnable_;
string Config::debuggerAddress_;

string Config::GetEnv(const char *name) {
    string result = "";
#if SAMPSHARP_WINDOWS
    size_t required_size;
    getenv_s(&required_size, NULL, 0, name);
    if (required_size > 0) {
        char* tmp = new char[required_size + 1];
        getenv_s(&required_size, tmp, required_size, name);

        if (tmp != NULL && strlen(tmp) > 0) {
            result = string(tmp);
        }

        delete[] tmp;
    }
#elif SAMPSHARP_LINUX
    char* tmp = getenv(name);

    if (tmp != NULL && strlen(tmp) > 0) {
        result = string(tmp);
    }
#endif

    return result;
}

void Config::Read()
{
    ConfigReader server_cfg("server.cfg");

    string tmpGameMode = "GameMode::GameMode";
    traceLevel_ = "error";
    codepage_ = "cp1252";
    debuggerEnable_ = "0";
    debuggerAddress_ = "0.0.0.0:7776";

    server_cfg.GetOptionAsString("gamemode", tmpGameMode);
    server_cfg.GetOptionAsString("trace_level", traceLevel_);
    server_cfg.GetOptionAsString("mono_assembly_dir", monoAssemblyDir_);
    server_cfg.GetOptionAsString("mono_config_dir", monoConfigDir_);
    server_cfg.GetOptionAsString("codepage", codepage_);
    server_cfg.GetOptionAsString("debugger", debuggerEnable_);
    server_cfg.GetOptionAsString("debugger_address", debuggerAddress_);

    string env = GetEnv("gamemode");
    if (env.length() > 0) {
        tmpGameMode = env;
    }

    std::stringstream gamemode_stream(tmpGameMode);

    std::getline(gamemode_stream, gameModeNamespace_, ':');
    StringUtil::TrimString(gameModeNamespace_);

    std::getline(gamemode_stream, gameModeClass_, '\n');
    StringUtil::TrimString(gameModeClass_);
}

string Config::GetMonoAssemblyDir() {
    return monoAssemblyDir_;
}
string Config::GetMonoConfigDir() {
    return monoConfigDir_;
}
string Config::GetTraceLevel() {
    return traceLevel_;
}
string Config::GetGameModeNameSpace() {
    return gameModeNamespace_;
}
string Config::GetGameModeClass() {
    return gameModeClass_;
}
string Config::GetCodepage() {
    return codepage_;
}
string Config::GetDebuggerEnable() {
    return debuggerEnable_;
}
string Config::GetDebuggerAddress() {
    return debuggerAddress_;
}
