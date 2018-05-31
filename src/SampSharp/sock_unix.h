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

#if SAMPSHARP_LINUX

#include "stdlib.h"
#include "commsvr.h"
#include "message_queue.h"
#include <sys/types.h>
#include <sys/socket.h>

class sock_unix : public commsvr
{
public:
    sock_unix();
    ~sock_unix();
    COMMSVR_DECL_PUB();
protected:
    virtual bool accept_addr(struct sockaddr *addr, socklen_t addrlen);
    virtual socklen_t accept_addr_len();
    virtual int socket_create() = 0;
    virtual socklen_t addr_alloc(struct sockaddr** addrptr) = 0;
private:
    void logerr(const char *pfx);
    bool wouldblock();
    int sock_;
    int sockc_;
    uint8_t *buf_;
    server *svr_;
    message_queue queue_messages_;
};

#endif // SAMPSHARP_LINUX