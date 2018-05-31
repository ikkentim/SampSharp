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

#include "dsock_unix.h"

#if SAMPSHARP_LINUX

#include "server.h"
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/socket.h>
#include <sys/un.h>

dsock_unix::dsock_unix(const char *path) {
    /* default path */
    sampsharp_sprintf(path_, MAX_DSOCK_PATH_LEN, "/tmp/SampSharp");

    if (path && strlen(path) > 0 && strlen(path) <= MAX_DSOCK_PATH_LEN) {
        sampsharp_sprintf(path_, MAX_DSOCK_PATH_LEN, "%s", path);
    }
}

int dsock_unix::socket_create() {
    return socket(AF_UNIX, SOCK_STREAM, 0);
}

socklen_t dsock_unix::addr_alloc(struct sockaddr** addrptr) {
    struct sockaddr_un addr;
    struct sockaddr_un *ap;
    socklen_t len = sizeof(addr);

    if (!addrptr) {
        return 0;
    }

    memset(&addr, 0, len);
    addr.sun_family = AF_UNIX;
    strncpy(addr.sun_path, path_, sizeof(addr.sun_path) - 2);
    unlink(path_);

    ap = (struct sockaddr_un *)malloc(len);
    memcpy(ap, &addr, len);
    *addrptr = (struct sockaddr*)ap;
    return len;
}

void dsock_unix::disconnect() {
    sock_unix::disconnect();
    unlink(path_);
}

#endif // SAMPSHARP_LINUX