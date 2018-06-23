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

#include "sock_unix.h"

#if SAMPSHARP_LINUX

#include "server.h"
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/un.h>
#include <fcntl.h>
#include <errno.h>
#include <signal.h>
#include <string.h>
#include "logging.h"

#define SOCK_NONE           (-1)
#define LEN_NETBUF          (20000)

sock_unix::sock_unix() :
    sock_(SOCK_NONE),
    sockc_(SOCK_NONE) {
    buf_ = new uint8_t[LEN_NETBUF];
}


sock_unix::~sock_unix() {
    delete[] buf_;
}

bool sock_unix::wouldblock() {
    return errno == EWOULDBLOCK;
}

void sock_unix::logerr(const char *pfx) {
    int err = errno;
    char* errstr = strerror(err);
    log_error(pfx, errstr);
}

bool sock_unix::setup(server *svr) {
    struct sockaddr* addr = NULL;
    size_t addrlen;

    signal(SIGPIPE, SIG_IGN); /* ignore broken pipes */

    if (is_ready()) {
        return true;
    }

    if (!svr) {
        return false;
    }

    svr_ = svr;
    sock_ = socket_create();

    if (sock_ == SOCK_NONE) {
        logerr("Failed to create socket. %s");
        return false;
    }

    if (fcntl(sock_, F_SETFL, O_NONBLOCK) == -1) {
        logerr("Failed to set socket option. %s");
        close(sock_);
        sock_ = SOCK_NONE;
        return false;
    }

    addrlen = addr_alloc(&addr);

    if (!addr) {
        log_error("Failed to allocate socket address.");
        close(sock_);
        sock_ = SOCK_NONE;
        return false;
    }

    if (bind(sock_, addr, addrlen) == -1) {
        logerr("Failed to bind socket. %s");
        close(sock_);
        free(addr);
        sock_ = SOCK_NONE;
        return false;
    }

    free(addr);

    if (listen(sock_, 5) == -1) {
        logerr("Failed to listen to socket. %s");
        close(sock_);
        sock_ = SOCK_NONE;
        return false;
    }

    log_info("Socket created.");
}


bool sock_unix::accept_addr(struct sockaddr *addr, socklen_t addrlen) {
    return true;
}

socklen_t sock_unix::accept_addr_len() {
    return 0;
}

bool sock_unix::connect() {
    struct sockaddr *addr = NULL;
    socklen_t *addrlenptr = NULL;
    socklen_t addrlen;

    /* ensure need to connect */
    if (!is_ready() || is_connected()) {
        return false;
    }

    addrlen = accept_addr_len();

    if (addrlen > 0) {
        addr = (struct sockaddr*)malloc(addrlen);
        addrlenptr = &addrlen;
    }

    if ((sockc_ = accept(sock_, addr, addrlenptr)) == SOCK_NONE) {
        if (!wouldblock()) {
            logerr("Could not accept connection. %s");
        }
        if (addr) {
            free(addr);
        }

        return false;
    }

    if (addr && !accept_addr(addr, addrlen)) {
        log_error("A non-whitelisted client was refused.");
        close(sockc_);
        sockc_ = SOCK_NONE;
        return false;
    }

    if (fcntl(sockc_, F_SETFL, O_NONBLOCK) == -1) {
        logerr("Failed to set socket option. %s");
        close(sockc_);
        sockc_ = SOCK_NONE;
        return false;
    }

    return true;
}

void sock_unix::disconnect() {
    if (!is_ready()) {
        return;
    }

    queue_messages_.clear();

    if (is_connected()) {
        close(sockc_);
        sockc_ = SOCK_NONE;
    }

    close(sock_);
    sock_ = SOCK_NONE;
}

bool sock_unix::send(uint8_t cmd, uint32_t len, uint8_t *buf) {
    uint8_t pfx[5];
    int wb;

    if (!is_connected()) {
        log_error("Cannot send data; pipe not connected!");
        return false;
    }

    if (!buf) {
        len = 0;
    }

    pfx[0] = cmd;
    *(uint32_t *)(pfx + 1) = len;
    if ((wb = write(sockc_, (char *)pfx, sizeof(pfx))) != sizeof(pfx)) {
        log_error("Partial write %d/%d.", wb, sizeof(pfx));
        logerr("Failed to write to socket. %s");
        svr_->terminate("Failed to write to socket.");
        return false;
    }
    if (len > 0) {
        if ((wb = write(sockc_, (char *)buf, len)) != len) {
            log_error("Partial write %d/%d.", wb, len);
            logerr("Failed to write to socket. %s");
            svr_->terminate("Failed to write to socket.");
            return false;
        }
    }

    return true;
}

cmd_status sock_unix::receive(uint8_t *command, uint8_t *buf, uint32_t *len) {
    int count;

    if (!is_connected() && !connect()) {
        return conn_dead;
    }

    count = read(sockc_, (char *)buf_, LEN_NETBUF);
    if (count > 0) {
        queue_messages_.add(buf_, count);
    }
    else if (count < 0 && errno != EAGAIN) {
        logerr("Failed to read from socket. %s");
        svr_->terminate("Failed to read from socket.");
        return conn_dead;
    }

    if (!queue_messages_.can_get()) {
        return no_cmd;
    }

    *len = queue_messages_.get(command, buf, *len);
    if (*len == MESSAGE_QUEUE_BUFFER_TOO_SMALL) {
        log_error("Message buffer too small.");
        len = 0;
    }

    return unhandled;
}

bool sock_unix::is_connected() {
    return is_ready() && sockc_ != SOCK_NONE;
}

bool sock_unix::is_ready() {
    return sock_ != SOCK_NONE;
}

#endif // SAMPSHARP_LINUX
