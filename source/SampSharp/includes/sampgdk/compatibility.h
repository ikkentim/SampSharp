/* Copyright (C) 2013 Zeex
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

#ifndef SAMPGDK_COMPATIBILITY_H
#define SAMPGDK_COMPATIBILITY_H

#if defined __cplusplus
  #if defined SAMPGDK_USE_NAMESPACE
    #define SAMPGDK_NAMESPACE sampgdk
    #define SAMPGDK_BEGIN_NAMESPACE namespace SAMPGDK_NAMESPACE {
    #define SAMPGDK_END_NAMESPACE }
  #else
    #define SAMPGDK_NAMESPACE
    #define SAMPGDK_BEGIN_NAMESPACE
    #define SAMPGDK_END_NAMESPACE
  #endif
#endif

#endif /* !SAMPGDK_COMPATIBILITY_H */
