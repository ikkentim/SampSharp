/* Copyright (C) 2011-2013 Zeex
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#ifndef SAMPGDK_EXPORT_H
#define SAMPGDK_EXPORT_H

#undef SAMPGDK_EXPORT
#undef SAMPGDK_CALL

#include <sampgdk/platform.h>
#include <sampgdk/plugincommon.h>

#ifdef __cplusplus
  #define SAMPGDK_EXTERN_C extern "C"
#else
  #define SAMPGDK_EXTERN_C
#endif

#if defined SAMPGDK_STATIC
  #define SAMPGDK_CALL
#else
  #define SAMPGDK_CALL SAMPGDK_CDECL
#endif

#if defined SAMPGDK_STATIC
  #define SAMPGDK_EXPORT SAMPGDK_EXTERN_C
#else
  #if SAMPGDK_LINUX
    #if defined IN_SAMPGDK
      #define SAMPGDK_EXPORT SAMPGDK_EXTERN_C __attribute__((visibility("default")))
    #else
      #define SAMPGDK_EXPORT SAMPGDK_EXTERN_C
    #endif
  #elif SAMPGDK_WINDOWS
    #if defined IN_SAMPGDK
      #define SAMPGDK_EXPORT SAMPGDK_EXTERN_C __declspec(dllexport)
    #else
      #define SAMPGDK_EXPORT SAMPGDK_EXTERN_C __declspec(dllimport)
    #endif
  #else
    #error Usupported operating system
  #endif
#endif

#ifndef SAMPGDK_NATIVE_EXPORT
  #define SAMPGDK_NATIVE_EXPORT SAMPGDK_EXPORT
#endif
#ifndef SAMPGDK_NATIVE_CALL
  #define SAMPGDK_NATIVE_CALL SAMPGDK_CALL
#endif
#ifndef SAMPGDK_NATIVE
  #define SAMPGDK_NATIVE(type, func) \
    SAMPGDK_NATIVE_EXPORT type SAMPGDK_NATIVE_CALL sampgdk_##func
#endif

#ifndef SAMPGDK_CALLBACK_EXPORT
  #define SAMPGDK_CALLBACK_EXPORT PLUGIN_EXPORT
#endif
#ifndef SAMPGDK_CALLBACK_CALL
  #define SAMPGDK_CALLBACK_CALL PLUGIN_CALL
#endif
#ifndef SAMPGDK_CALLBACK
  #define SAMPGDK_CALLBACK(type, func) \
    SAMPGDK_CALLBACK_EXPORT type SAMPGDK_CALLBACK_CALL func
#endif

#define SAMPGDK_TIMER_CALL SAMPGDK_CALL

#endif /* !SAMPGDK_EXPORT_H */
