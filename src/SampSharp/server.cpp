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

#include "platforms.h"
#include "version.h"
#include <assert.h>

#ifdef SAMPSHARP_WINDOWS

#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#elif SAMPSHARP_LINUX
#endif

#include "server.h"

#define vsnprintf vsprintf_s

#define PIPE_NONE INVALID_HANDLE_VALUE

#define LEN_PRINT_BUFFER    (1024)
#define LEN_NETBUF          (20000)

/* receive */
#define CMD_PING            (0x01) /* request a pong */
#define CMD_PRINT           (0x02) /* print data */
#define CMD_RESPONSE        (0x03) /* response to public call */
#define CMD_RECONNECT       (0x04) /* expect client to reconnect */
#define CMD_REGISTER_CALL   (0x05) /* register a public call */
#define CMD_FIND_NATIVE     (0x06) /* return native id */
#define CMD_INVOKE_NATIVE   (0x07) /* invoke a native */
#define CMD_START           (0x08) /* start sending messages */

/* send */
#define CMD_TICK            (0x11) /* server tick */
#define CMD_PONG            (0x12) /* ping reply */
#define CMD_PUBLIC_CALL     (0x13) /* public call */
#define CMD_REPLY           (0x14) /* reply to find native or native invoke */
#define CMD_ANNOUNCE        (0x15) /* announce with version */

#define STATUS_SET(v) status_ = (status)(status_ | (v))
#define STATUS_UNSET(v) status_ = (status)(status_ & ~(v))
#define STATUS_ISSET(v) ((status_ & (v)) == (v))

#pragma region Constructors and loading

/** initializes and allocates required memory for the server instance */
server::server() : 
    callbacks_(callbacks_map(this)),
    natives_(natives_map(this)),
    pipe_(PIPE_NONE),
    status_(status_none) {

    /* pipe buffer */
    rxbuf_ = new uint8_t[LEN_NETBUF];
    txbuf_ = new uint8_t[LEN_NETBUF];
}

/** frees memory allocated by this instance */
server::~server() {
    if (pipe_ != PIPE_NONE) {
        pipe_disconnect(NULL, true);
    }

    delete[] rxbuf_;
    delete[] txbuf_;
}

/** starts the pipe server */
void server::start(const char *pipe_name) {
    /* default pipe name */
    sampsharp_sprintf(pipe_name_, MAX_PIPE_NAME_LEN,
        "\\\\.\\pipe\\SampSharp");

    if (pipe_name && strlen(pipe_name) > 0) {
        if (strlen(pipe_name) > MAX_PIPE_NAME_LEN - 10) {
            log_error("Pipe name too long.");
        }
        else {
            sampsharp_sprintf(pipe_name_, MAX_PIPE_NAME_LEN,
                "\\\\.\\pipe\\%s", pipe_name);
        }
    }

    /* setup the server sockets to allow clients to connect */
    pipe_create();
}

#pragma endregion

#pragma region Logging

/** prints text to the output */
void server::print(const char *format, ...) {
    va_list args;
    va_start(args, format);
    sampgdk_vlogprintf(format, args);
    va_end(args);
}

/** log error */
void server::log_error(const char * format, ...) {
    va_list args;
    va_start(args, format);
    vlog("ERROR", format, args);
    va_end(args);
}

/** log debug */
void server::log_debug(const char * format, ...) {
#if (defined DEBUG) || (defined _DEBUG)
    va_list args;
    va_start(args, format);
    vlog("DEBUG", format, args);
    va_end(args);
#endif
}

/** log info */
void server::log_info(const char * format, ...) {
    va_list args;
    va_start(args, format);
    vlog("INFO", format, args);
    va_end(args);
}

/** log a message */
void server::vlog(const char* prefix, const char *format, va_list args) {
    char buffer[LEN_PRINT_BUFFER];
    vsnprintf(buffer, LEN_PRINT_BUFFER, format, args);
    buffer[LEN_PRINT_BUFFER - 1] = '\0';

    sampgdk_logprintf("[SampSharp:%s] %s", prefix, buffer);
}

#pragma endregion

#pragma region Getters

/** a value indicating whether the pipe is connected */
bool server::is_pipe_connected() {
    return pipe_ != PIPE_NONE && STATUS_ISSET(status_client_connected);
}

#pragma endregion

#pragma region Commands

CMD_DEFINE(cmd_ping) {
    cmd_send(CMD_PONG, 0, NULL);
}

CMD_DEFINE(cmd_print) {
    print("%s", buf);
}

CMD_DEFINE(cmd_register_call) {
    log_debug("Register call %s", buf);
    callbacks_.register_buffer(buf);
}

CMD_DEFINE(cmd_find_native) {
    log_debug("Find native w/%d data", buflen);
    int32_t handle =  natives_.get_handle(buf);
    cmd_send(CMD_RESPONSE, sizeof(int32_t), (uint8_t *)&handle);
}

CMD_DEFINE(cmd_invoke_native) {
    log_debug("Invoke native w/%d data", buflen);
    uint32_t txlen = LEN_NETBUF;
    natives_.invoke(buf, buflen, txbuf_, &txlen);

    log_debug("Sending response to native w/%d data", txlen);
    cmd_send(CMD_RESPONSE, txlen, txbuf_);
}

CMD_DEFINE(cmd_reconnect) {
    log_info("The gamemode has is reconnecting.");
    STATUS_SET(status_client_reconnecting);
    pipe_disconnect(NULL, true);
}

CMD_DEFINE(cmd_start) {
    log_info("The gamemode has started.");
    STATUS_SET(status_client_started);
    uint8_t type = buflen == 0 ? 0 : buf[0];

    switch (type) {
    case 0:
        log_debug("Using 'none' start method");
        break;
    case 1:
        log_debug("Using 'gmx' start method");
        if (STATUS_ISSET(status_server_received_init)) {
            log_debug("Sending gmx to attach game mode.");
            SendRconCommand("gmx");
        }
        break;
    case 2:
        log_debug("Using 'fake gmx' start method");
        if (STATUS_ISSET(status_server_received_init)) {
            STATUS_SET(status_client_received_init);

            cell params = 0;
            uint32_t len = callbacks_.fill_call_buffer(NULL, "OnGameModeInit",
                &params, txbuf_, LEN_NETBUF);
            uint8_t *response = NULL;

            if (len == 0) {
                break;
            }

            /* send */
            cmd_send(CMD_PUBLIC_CALL, len, txbuf_);

            /* receive */
            if (!cmd_receive_unhandled(&response, &len) || !response || 
                len == 0) {
                log_error("Received no response to callback OnGameModeInit.");
                break;
            }

            delete[] response;
        }
        break;
    default:
        log_error("Invalid game mode start mode");
        break;
    }
}

#pragma endregion

#pragma region Pipes - Setup

/** create the pipe */
bool server::pipe_create() {
    if (pipe_ != PIPE_NONE) {
        return true;
    }

    log_info("Creating pipe %s...", pipe_name_);

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
            log_error("Failed to create pipe with error 0x%x. Is the server "
                "already running?", error);
            break;
        default:
            log_error("Failed to create pipe with error 0x%x.", error);
            break;
        }
        return false;
    }
    else {
        log_info("Pipe created.");
    }

    return true;
}

/* update status for new connection */
void server::pipe_handle_new_connection() {
    STATUS_SET(status_client_connected);

    if (STATUS_ISSET(status_client_reconnecting)) {
        log_info("Client reconnected.");
    }
    else {
        log_info("Connected to pipe.");
        cmd_send_announce();
    }

    STATUS_UNSET(status_client_reconnecting);
}

/** connect to the pipe */
bool server::pipe_connect() {
    /* ensure pipe exists */
    if (pipe_ == PIPE_NONE && !pipe_create()) {
        return false;
    }

    if (is_pipe_connected()) {
        return true;
    }

    if (!ConnectNamedPipe(pipe_, NULL)) {
        DWORD error = GetLastError();

        switch (error) {
        case ERROR_PIPE_CONNECTED: /* process on other side of pipe */
            pipe_handle_new_connection();
            return true;
        case ERROR_PIPE_LISTENING: /* other end of pipe not connected */
            return false;
        default:
            log_error("Failed to connect to pipe with error 0x%x.", error);
            return false;
        }
    }

    pipe_handle_new_connection();
    return true;
}

/** sends the server annoucement to the client */
void server::cmd_send_announce() {
    /* send version */
    uint32_t info[2];
    info[0] = PLUGIN_PROTOCOL_VERSION;
    info[1] = PLUGIN_VERSION;

    cmd_send(CMD_ANNOUNCE, sizeof(info), (uint8_t *)info);

    log_info("Server annoucement sent.");
}

/** disconnects from pipe */
void server::pipe_disconnect(const char *context, bool expected) {
    if (!is_pipe_connected()) {
        return;
    }
    
    if (!expected) {
        if (!context) {
            context = "";
        }
        log_error("Unexpected disconnect of pipe. %s", context);

        STATUS_UNSET(status_client_started);
        natives_.clear();
        callbacks_.clear();
        queue_messages_.clear();
    }
    else {
        log_info("The client is reconnecting to the pipe.");
    }
    
    /* disconnect and close */
    DisconnectNamedPipe(pipe_);
    CloseHandle(pipe_);
    pipe_ = PIPE_NONE;

    STATUS_UNSET(status_client_connected);
}

#pragma endregion

#pragma region Pipes - Sending

/** sends the specified command with the specified buffer as arguments */
bool server::cmd_send(uint8_t cmd, uint32_t len, uint8_t *buf) {
    if (!is_pipe_connected()) {
        log_error("Cannot send data; pipe not connected!");
        return false;
    }

    if (!buf) {
        len = 0;
    }

    DWORD tlen;
    if (!WriteFile(pipe_, &cmd, 1, &tlen, NULL) || 
        !WriteFile(pipe_, &len, 4, &tlen, NULL)) {
        log_error("Failed to write to pipe with error 0x%x.", GetLastError());
        pipe_disconnect("Failed to write to pipe.");

        return false;
    }

    if (buf && len > 0 && !WriteFile(pipe_, buf, len, &tlen, NULL)) {
        log_error("Failed to write to pipe with error 0x%x.", GetLastError());
        pipe_disconnect("Failed to write to pipe.");

        return false;
    }

    return true;
}

#pragma endregion

#pragma region Pipes - Receiving

/** receives a single command if available */
server::cmd_status server::cmd_receive_one(uint8_t **response, uint32_t *len) {
    assert(response);
    assert(len);
    assert(sizeof(unsigned long) == sizeof(uint32_t));

    if (!pipe_connect()) {
        return cmd_status::conn_dead;
    }
    

    //Read from client
    DWORD rlen = 0;
    if (!ReadFile(pipe_, rxbuf_, LEN_NETBUF, &rlen, NULL)) {
        DWORD error = GetLastError();
        if (error == ERROR_NO_DATA) {
            rlen = 0;
        }
        else {
            rlen = 0;
            log_error("Failed to read from pipe with error 0x%x.", error);
            pipe_disconnect("Failed to read from pipe.");
        }
    }


    if (rlen > 0) {
        queue_messages_.add(rxbuf_, rlen);
    }

    if (!queue_messages_.can_get()) {
        return cmd_status::no_cmd;
    }

    uint8_t command;
    uint32_t command_len = queue_messages_.get(&command, rxbuf_, LEN_NETBUF);
    if (command_len == MESSAGE_QUEUE_BUFFER_TOO_SMALL) {
        log_error("Message buffer too small.");
    }

    return cmd_process(command, rxbuf_, command_len, response, len);
}

/** receives commands until an unhandled command appears */
bool server::cmd_receive_unhandled(uint8_t **response, uint32_t *len) {
    assert(response);
    assert(len);

    cmd_status stat;

    do {
        *response = NULL;
        *len = 0;
        stat = cmd_receive_one(response, len);
    } while (stat == cmd_status::handled || stat == cmd_status::no_cmd);

    return stat == cmd_status::unhandled;
}

/** processes a command */
server::cmd_status server::cmd_process(uint8_t cmd, uint8_t *buf, 
    uint32_t buflen, uint8_t **resp, uint32_t *resplen) {
#define MAP_COMMAND(a,b) \
    case a:\
        b(buf, buflen);\
        return cmd_status::handled

    switch (cmd) {
        /* mapped commands */
        MAP_COMMAND(CMD_PING, cmd_ping);
        MAP_COMMAND(CMD_PRINT, cmd_print);
        MAP_COMMAND(CMD_REGISTER_CALL, cmd_register_call);
        MAP_COMMAND(CMD_FIND_NATIVE, cmd_find_native);
        MAP_COMMAND(CMD_INVOKE_NATIVE, cmd_invoke_native);
        MAP_COMMAND(CMD_RECONNECT, cmd_reconnect);
        MAP_COMMAND(CMD_START, cmd_start);

        /* unmapped commands (unhandled) */
        case CMD_RESPONSE:
        default:
            if (buflen > 0) {
                *resp = new uint8_t[buflen];
                memcpy(*resp, buf, buflen);
                *resplen = buflen;
            }
            return cmd_status::unhandled;
    }

#undef MAP_COMMAND
}

#pragma endregion

/** called when a public call is send from the server */
void server::public_call(AMX *amx, const char *name, cell *params, cell *retval) {
    log_debug("Received public call %s (%d)", name, params[0]);

    bool is_gmi = !strcmp(name, "OnGameModeInit");
    bool is_gme = !is_gmi && !strcmp(name, "OnGameModeExit");

    if (is_gmi) {
        STATUS_SET(status_server_received_init);
    }
    else if (is_gme) {
        STATUS_UNSET(status_server_received_init);
    }

    if (!is_pipe_connected() || !STATUS_ISSET(status_client_started)) {
        return;
    }

    if (is_gmi) {
        STATUS_SET(status_client_received_init);
    }
    else if (!STATUS_ISSET(status_client_received_init)) {
        return;
    }

    uint32_t len = callbacks_.fill_call_buffer(amx, name, params, txbuf_, 
        LEN_NETBUF);
    uint8_t *response = NULL;

    if (len == 0) {
        return;
    }

    /* send */
    cmd_send(CMD_PUBLIC_CALL, len, txbuf_);
  
    /* receive */
    if(!cmd_receive_unhandled(&response, &len) || !response || len == 0) {
        log_error("Received no response to callback %s.", name);
        return;
    }

    if (len >= 5 && response[0] && retval) {
        /* get return value */
        *retval = *((uint32_t *)(response + 1));
    }

    delete[] response;
}

/** called when a server tick occurs */
void server::tick() {
    if (is_pipe_connected() && STATUS_ISSET(status_client_started |
        status_client_received_init)) {
        cmd_send(CMD_TICK, 0, NULL);
    }

    uint8_t *response = NULL;
    uint32_t len;
    cmd_status stat;

    /* receive calls from the game mode client */
    do {
        stat = cmd_receive_one(&response, &len);

        if (response) {
            log_error("Unhandled response in tick.");
            delete[] response;
        }
    } while (stat != cmd_status::no_cmd && stat != cmd_status::conn_dead);
}
