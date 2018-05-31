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

#include "tcp_unix.h"

#if SAMPSHARP_LINUX

#include "server.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>

tcp_unix::tcp_unix(const char *ip, uint16_t port) {
    port_ = port;

    if (port_ == 0) {
        port_ = 8888;
    }

    if (inet_aton(ip, &addr_) == 0) {
        /* default ip */
        inet_aton("127.0.0.1", &addr_);
    }
}

int tcp_unix::socket_create() {
    return socket(AF_INET, SOCK_STREAM, 0);
}

socklen_t tcp_unix::addr_alloc(struct sockaddr** addrptr) {
    struct sockaddr_in addr;
    struct sockaddr_in *ap;
    size_t len = sizeof(addr);
    int offs = 1;

    if (!addrptr) {
        return 0;
    }

    memset(&addr, 0, sizeof(addr));
    addr.sin_family = AF_INET;
    addr.sin_port = htons(port_);
    addr.sin_addr.s_addr = INADDR_ANY;

    ap = (struct sockaddr_in *)malloc(len);
    memcpy(ap, &addr, len);
    *addrptr = (struct sockaddr*)ap;
    return len;
}

socklen_t tcp_unix::accept_addr_len() {
    return sizeof(struct sockaddr_in);
}

bool tcp_unix::accept_addr(struct sockaddr *addr, socklen_t addrlen) {
    if (!addr || addr->sa_family != AF_INET) {
        return false;
    }

    struct sockaddr_in *inaddr = (struct sockaddr_in *)addr;

    return !memcmp(&inaddr->sin_addr, &addr_, sizeof(struct in_addr));
}

#endif // SAMPSHARP_LINUX