#pragma once

#include <sdk.hpp>

namespace sampsharp::crash
{
    // Installs a process-wide crash handler. Safe to call once during onLoad.
    // On Windows: top-level SEH filter + vectored continue handler that logs
    // exception code/address, module, stack trace, and writes a minidump next
    // to the executable. No-op on other platforms.
    void install(ICore* core);
}
