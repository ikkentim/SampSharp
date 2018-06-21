#include "logging.h"
#include <cstdarg>
#include <stdio.h>
#include <string.h>
#include <sampgdk/sampgdk.h>
#include "platforms.h"

/* platform specifics */
#if SAMPSHARP_WINDOWS
#  define vsnprintf vsprintf_s
#endif

#define LEN_PRINT_BUFFER    (1024)

/** log a message */
void vlog(const char* prefix, const char *format, va_list args) {
    char buffer[LEN_PRINT_BUFFER];
    vsnprintf(buffer, LEN_PRINT_BUFFER, format, args);
    buffer[LEN_PRINT_BUFFER - 1] = '\0';

    sampgdk_logprintf("[SampSharp:%s] %s", prefix, buffer);
}

void print(const char *format, ...) {
    va_list args;
    va_start(args, format);
    sampgdk_vlogprintf(format, args);
    va_end(args);
}

/** log error */
void log_error(const char * format, ...) {
    va_list args;
    va_start(args, format);
    vlog("ERROR", format, args);
    va_end(args);
}

/** log debug */
void log_debug(const char * format, ...) {
#if ((defined DEBUG) || (defined _DEBUG))
    va_list args;
    va_start(args, format);
    vlog("DEBUG", format, args);
    va_end(args);
#endif
}

/** log info */
void log_info(const char * format, ...) {
    va_list args;
    va_start(args, format);
    vlog("INFO", format, args);
    va_end(args);
}
