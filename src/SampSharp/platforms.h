// SampSharp
// Copyright 2017 Tim Potze
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

#if (defined __CYGWIN32__ || defined RC_INVOKED) && !defined WIN32
#define WIN32
#endif

#if defined WIN32 || defined _WIN32 || defined __WIN32__
#define SAMPSHARP_LINUX 0
#define SAMPSHARP_WINDOWS 1

#endif

#if defined __linux__ || defined __linux || defined linux
#if !defined LINUX
#define LINUX
#endif
#define SAMPSHARP_LINUX 1
#define SAMPSHARP_WINDOWS 0
#endif
