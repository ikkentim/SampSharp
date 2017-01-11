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

#include <string>

#pragma once

class Config {
public:
    static void Read();

    static std::string GetEnv(const char *name);

    static std::string GetMonoAssemblyDir();
    static std::string GetMonoConfigDir();
    static std::string GetTraceLevel();
    static std::string GetGameModeNameSpace();
    static std::string GetGameModeClass();
    static std::string GetCodepage();
    static std::string GetDebuggerEnable();
    static std::string GetDebuggerAddress();
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
