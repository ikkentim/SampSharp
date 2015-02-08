#include <string>
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
        codepage_ = 1250;

        server_cfg.GetOptionAsString("gamemode", tmpGameMode);
        server_cfg.GetOptionAsString("trace_level", traceLevel_);
        server_cfg.GetOptionAsString("mono_assembly_dir", monoAssemblyDir_);
        server_cfg.GetOptionAsString("mono_config_dir", monoConfigDir_);
        server_cfg.GetOption("codepage", codepage_);
        server_cfg.GetOptionAsString("symbols", symbolFiles_);

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
    static int GetCodepage() {
        return codepage_;
    }
    static std::string GetSymbolFiles() {
        return symbolFiles_;
    }

private:
    static std::string monoAssemblyDir_;
    static std::string monoConfigDir_;
    static std::string traceLevel_;
    static std::string gameModeNamespace_;
    static std::string gameModeClass_;
    static int codepage_;
    static std::string symbolFiles_;
};