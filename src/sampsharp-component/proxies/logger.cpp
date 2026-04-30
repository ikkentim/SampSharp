#include <sdk.hpp>

extern "C" SDK_EXPORT void __CDECL ILogger_printLn(ILogger * subject, const char * msg)
{
    if (!subject) { return; }
    return subject->printLn("%s", msg);
}

extern "C" SDK_EXPORT void __CDECL ILogger_logLn(ILogger * subject, LogLevel level, const char * msg)
{
    if (!subject) { return; }
    return subject->logLn(level, "%s", msg);
}

extern "C" SDK_EXPORT void __CDECL ILogger_printLnU8(ILogger * subject, const char * msg)
{
    if (!subject) { return; }
    return subject->printLnU8("%s", msg);
}

extern "C" SDK_EXPORT void __CDECL ILogger_logLnU8(ILogger * subject, LogLevel level, const char * msg)
{
    if (!subject) { return; }
    return subject->logLnU8(level, "%s", msg);
}
