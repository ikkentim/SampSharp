// SampSharp
// Copyright 2015 Tim Potze
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

#include <mono/jit/jit.h>

#pragma once

#define UNICODE_DEFAULT_CHAR 0x3F
#define UNICODE_ERROR_CHAR 0x9999

/* Converts unicode character to the set encoding.
 */
char get_char_from_unicode(mono_unichar2 ch);

/* converts a character from the set encoding to an unicode character.
 */
mono_unichar2 get_unicode_from_char(char ch);

/* Change codepage used by encoding functions get_char_from_unicode and
 * get_unicode_from_char.
 */
void set_codepage(int page);
