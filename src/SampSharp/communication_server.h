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

#include <inttypes.h>
#include "command.h"

class server;
class communication_server {
public:
    /** setup communication */
    virtual bool setup(server *svr) = 0;
    /** let user connect */
    virtual bool connect() = 0;
    /** is user connected */
    virtual bool is_connected() = 0;
    /** is set up */
    virtual bool is_ready() = 0;
    /** disconnect user */
    virtual void disconnect() = 0;
    /** send command to server */
    virtual bool send(uint8_t cmd, uint32_t len, uint8_t *buf) = 0;
    /** receive command from server */
    virtual cmd_status receive(uint8_t *command, uint8_t *buf, uint32_t *len)
        = 0;
};