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

#include "pipe_server.h"

#if SAMPSHARP_WINDOWS

#include <string.h>
#include <stdio.h>
#include <assert.h>
#include "server.h"
#define VC_EXTRALEAN
#include <windows.h>

#define vsnprintf           vsprintf_s
#define PIPE_NONE           INVALID_HANDLE_VALUE
#define LEN_NETBUF          (20000)

pipe_server::pipe_server(const char *pipe_name) :
    pipe_(PIPE_NONE),
    connected_(false) {
    buf_ = new uint8_t[LEN_NETBUF];

    /* default pipe name */
    sampsharp_sprintf(pipe_name_, MAX_PIPE_NAME_LEN,
        "\\\\.\\pipe\\SampSharp");

    if (pipe_name && strlen(pipe_name) > 0 && 
        strlen(pipe_name) <= MAX_PIPE_NAME_LEN - 10) {
        sampsharp_sprintf(pipe_name_, MAX_PIPE_NAME_LEN, 
            "\\\\.\\pipe\\%s", pipe_name);
    }
}

pipe_server::~pipe_server() {
    delete[] buf_;
}

bool pipe_server::is_connected() {
    return is_ready() && connected_;
}

bool pipe_server::is_ready() {
    return pipe_ != PIPE_NONE;
}

bool pipe_server::setup(server *svr) {
    if (is_ready()) {
        return true;
    }

    if (!svr) {
        return false;
    }

    server_ = svr;

    server_->log_info("Creating pipe %s...", pipe_name_);

    pipe_ = CreateNamedPipe(pipe_name_,
        PIPE_ACCESS_DUPLEX,
        PIPE_TYPE_BYTE |                // from message
        PIPE_READMODE_BYTE |            // from message
        PIPE_NOWAIT |
        PIPE_REJECT_REMOTE_CLIENTS,
        PIPE_UNLIMITED_INSTANCES,       // max. instances 
        LEN_NETBUF,                     // output buffer size 
        LEN_NETBUF,                     // input buffer size 
        NMPWAIT_USE_DEFAULT_WAIT,       // client time-out 
        NULL);                          // no security attribute 

    if (pipe_ == PIPE_NONE) {
        DWORD error = GetLastError();
        switch (error) {
        case ERROR_PIPE_BUSY:
            server_->log_error("Failed to create pipe with error 0x%x. Is the server "
                "already running?", error);
            break;
        default:
            server_->log_error("Failed to create pipe with error 0x%x.", error);
            break;
        }
        return false;
    }
    else {
        server_->log_info("Pipe created.");
    }

    return true;
}

bool pipe_server::connect() {
    /* ensure need to connect */
    if (!is_ready() || is_connected()) {
        return false;
    }
    
    if (!ConnectNamedPipe(pipe_, NULL)) {
        DWORD error = GetLastError();

        switch (error) {
        case ERROR_PIPE_CONNECTED: /* process on other side of pipe */
            return connected_ = true;
        case ERROR_PIPE_LISTENING: /* other end of pipe not connected */
            return false;
        default:
            server_->log_error("Failed to connect to pipe with error 0x%x.", error);
            return false;
        }
    }

    return connected_ = true;
}

void pipe_server::disconnect() {
    queue_messages_.clear();
    connected_ = false;

    DisconnectNamedPipe(pipe_);
    CloseHandle(pipe_);
    pipe_ = PIPE_NONE;
}

bool pipe_server::send(uint8_t cmd, uint32_t len, uint8_t *buf) {
    if (!is_connected()) {
        server_->log_error("Cannot send data; pipe not connected!");
        return false;
    }

    if (!buf) {
        len = 0;
    }

    DWORD tlen;
    if (!WriteFile(pipe_, &cmd, 1, &tlen, NULL) ||
        !WriteFile(pipe_, &len, 4, &tlen, NULL)) {
        server_->log_error("Failed to write to pipe with error 0x%x.", GetLastError());
        server_->disconnect("Failed to write to pipe.");

        return false;
    }

    if (buf && len > 0 && !WriteFile(pipe_, buf, len, &tlen, NULL)) {
        server_->log_error("Failed to write to pipe with error 0x%x.", GetLastError());
        server_->disconnect("Failed to write to pipe.");

        return false;
    }

    return true;
}

cmd_status pipe_server::receive(uint8_t *command, uint8_t *buf, 
    uint32_t *len) {
    assert(command);
    assert(buf);
    assert(len);
    assert(sizeof(unsigned long) == sizeof(uint32_t));

    if (!is_connected() && !connect()) {
        return conn_dead;
    }

    //Read from client
    DWORD rlen = 0;
    if (!ReadFile(pipe_, buf_, LEN_NETBUF, &rlen, NULL)) {
        DWORD error = GetLastError();
        if (error == ERROR_NO_DATA) {
            rlen = 0;
        }
        else {
            rlen = 0;
            server_->log_error("Failed to read from pipe with error 0x%x.", error);
            server_->disconnect("Failed to read from pipe.");
        }
    }


    if (rlen > 0) {
        queue_messages_.add(buf_, rlen);
    }

    if (!queue_messages_.can_get()) {
        return no_cmd;
    }

    *len = queue_messages_.get(command, buf, *len);
    if (*len == MESSAGE_QUEUE_BUFFER_TOO_SMALL) {
        server_->log_error("Message buffer too small.");
        len = 0;
    }

    return unhandled;
}

#endif // SAMPSHARP_WINDOWS