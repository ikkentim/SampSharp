#pragma once
#include <string>
#include "platforms.h"

#if SAMPSHARP_LINUX
#  define DIR_SEPARATOR "/"
#elif SAMPSHARP_WINDOWS
#  define DIR_SEPARATOR "\\"
#endif

void path_append(const char *path, const char *append, std::string &result);
bool dir_exists(const char *path);
bool file_exists(const char *path);
bool get_directory(const char *absolute_path, std::string &directory);
bool get_absolute_path(const char* path, std::string &absolute_path);
