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

#include "platforms.h"

#if SAMPSHARP_WINDOWS

#include "communication_server.h"
#include "message_queue.h"

#define MAX_PIPE_NAME_LEN       (256)

class server;
class pipe_server :
	public communication_server {
public:
	pipe_server(const char *pipe_name);
	~pipe_server();
    bool setup(server *svr);
    bool connect();
    void disconnect();
    bool send(uint8_t cmd, uint32_t len, uint8_t *buf);
    cmd_status receive(uint8_t *command, uint8_t *buf, uint32_t *len);
    bool is_connected();
    bool is_ready();

private:
    server *server_;
    bool connected_;
    char pipe_name_[MAX_PIPE_NAME_LEN];
    void *pipe_;
    uint8_t *buf_;
    message_queue queue_messages_;
};

#endif // SAMPSHARP_WINDOWS
