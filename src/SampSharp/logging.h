#pragma once

/** prints text to the output */
void print(const char *format, ...);
/** log an error */
void log_error(const char *format, ...);
/** log a debug */
void log_debug(const char *format, ...);
/** log info */
void log_info(const char *format, ...);
