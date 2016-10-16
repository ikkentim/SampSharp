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

#include <string>
#include <string.h>
#include "ConfigReader.h"
#include "StringUtil.h"
#include "PathUtil.h"

#pragma once

class Config {
public:
    static void Read()
    {
        ConfigReader server_cfg("server.cfg");

        std::string tmpGameMode = "GameMode::GameMode";
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


#ifdef _WIN32
        char* envGameMode = NULL;
        size_t required_size;
        getenv_s(&required_size, NULL, 0, "gamemode");
        envGameMode = new char[required_size];
        getenv_s(&required_size, envGameMode, required_size, "gamemode");

        if (envGameMode != NULL && strlen(envGameMode) > 0) {
            tmpGameMode = std::string(envGameMode);
        }

        delete[] envGameMode;
#else
        char* envGameMode = getenv("gamemode");

        if (envGameMode != NULL && strlen(envGameMode) > 0) {
            tmpGameMode = std::string(envGameMode);
        }
#endif

        std::stringstream gamemode_stream(tmpGameMode);

        std::getline(gamemode_stream, gameModeNamespace_, ':');
        StringUtil::TrimString(gameModeNamespace_);

        std::getline(gamemode_stream, gameModeClass_, '\n');
        StringUtil::TrimString(gameModeClass_);
    }

    static std::string GetMonoAssemblyDir() {
        return monoAssemblyDir_;
    }
    static std::string GetMonoConfigDir() {
        return monoConfigDir_;
    }
    static std::string GetTraceLevel() {
        return traceLevel_;
    }
    static std::string GetGameModeNameSpace() {
        return gameModeNamespace_;
    }
    static std::string GetGameModeClass() {
        return gameModeClass_;
    }
    static std::string GetCodepage() {
        return codepage_;
    }
    static std::string GetDebuggerEnable() {
        return debuggerEnable_;
    }
    static std::string GetDebuggerAddress() {
        return debuggerAddress_;
    }

private:
    static std::string monoAssemblyDir_;
    static std::string monoConfigDir_;
    static std::string traceLevel_;
    static std::string gameModeNamespace_;
    static std::string gameModeClass_;
    static std::string codepage_;
    static std::string debuggerEnable_;
    static std::string debuggerAddress_;
};
