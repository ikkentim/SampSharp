#include <string>

#include "PathUtil.h"

#pragma once

class MonoRuntime {
public:
    static bool IsLoaded() {
        return isLoaded_;
    }
    static void Load(std::string assemblyDir, std::string configDir,
        std::string traceLevel, std::string file);
    static void Unload();
private:
    static bool isLoaded_;
};