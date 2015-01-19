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

#include <time.h>
#include <string.h>

#pragma once

/* Creates a timestamp string based on the system time.
 */
static char *timestamp_create() {
    time_t now = time(0);
    char timestamp[32];
	
    strftime(timestamp, sizeof(timestamp), "[%d/%m/%Y %H:%M:%S]", localtime(&now));

    char *timestamp2 = new char[32];
    strcpy(timestamp2, timestamp);
    return  timestamp2;
}

