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

#include "platforms.h"

#if SAMPSHARP_WINDOWS

#include "commsvr.h"
#include "message_queue.h"

#define MAX_PIPE_NAME_LEN       (256)

class remote_server;
class pipesvr_win32 :
    public commsvr {
public:
    pipesvr_win32(const char *pipe_name);
    ~pipesvr_win32();
    COMMSVR_DECL_PUB();

private:
    remote_server *svr_;
    bool connected_;
    char pipe_name_[MAX_PIPE_NAME_LEN];
    void *pipe_;
    uint8_t *buf_;
    message_queue queue_messages_;
};

#endif // SAMPSHARP_WINDOWS
