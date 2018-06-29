// SampSharp
// Copyright 2018 Tim Potze
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

#pragma once

void log_debug2(const char *format, ...);

/** prints text to the output */
void log_print(const char *format, ...);

/** log an error */
void log_error(const char *format, ...);

/** log a warning */
void log_warning(const char *format, ...);

/** log debug info */
#ifdef LOG_DEBUG
#define log_debug(...) log_debug2(##__VA_ARGS__)
#else
#define log_debug(...) while(0)
#endif

/** log info */
void log_info(const char *format, ...);
