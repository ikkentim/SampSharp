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

#include "sock_unix.h"

#if SAMPSHARP_LINUX

#include <inttypes.h>
#include <netinet/ip.h>

class tcp_unix : public sock_unix
{
public:
    tcp_unix(const char *ip, uint16_t port);
protected:
    int socket_create();
    socklen_t addr_alloc(struct sockaddr** addrptr);
    socklen_t accept_addr_len();
    bool accept_addr(struct sockaddr *addr, socklen_t addrlen);
private:
    struct in_addr addr_;
    uint16_t port_;
};

#endif // SAMPSHARP_LINUX