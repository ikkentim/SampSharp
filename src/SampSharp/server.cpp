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

#include <assert.h>
#include <thread>

#ifdef SAMPSHARP_WINDOWS

#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#define vsnprintf vsprintf_s

#elif SAMPSHARP_LINUX
#endif

#include "server.h"

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
#define CMD_PUBLIC_CALL     (0x13) /* public call*/
#define CMD_REPLY           (0x14) /* reply to find native or native invoke */
#define CMD_ANNOUNCE        (0x15) /* announce with version */

#pragma region Constructors and loading

server::server() : 
    callbacks_(callbacks_map(this)),
    natives_(natives_map(this)),
    pipe_(PIPE_NONE),
    thread_(INVALID_HANDLE_VALUE),
    pipe_connected_(false),
    gamemode_started_(false),
    started_(false),
    reconnecting_(false) {

    /* pipe buffer */
    rxbuf_ = new uint8_t[LEN_NETBUF];
    txbuf_ = new uint8_t[LEN_NETBUF];
}

server::~server() {
    if (thread_ != INVALID_HANDLE_VALUE) {
        TerminateThread(thread_, 0);
    }

    if (pipe_ != PIPE_NONE) {
        pipe_disconnect(NULL, true);
    }

    delete[] rxbuf_;
    delete[] txbuf_;
}

DWORD WINAPI server_loop_f(LPVOID svr_ptr) {
    assert(svr_ptr);
    ((server *)svr_ptr)->loop();

    return 0;
}

void server::start() {
    /* store main thread handle for later reference  */
    main_thread_ = std::this_thread::get_id();

    /* setup the server sockets to allow clients to connect */
    pipe_create();

    /* create the gamemode thread */
    thread_ = CreateThread(0, 0, server_loop_f, this, 0, NULL);

    if (thread_ == INVALID_HANDLE_VALUE) {
        log_error("Failed to create a thread.");
    }
}

#pragma endregion

#pragma region Logging

void server::print(const char *format, ...) {
    va_list args;

    if (main_thread_ == std::this_thread::get_id()) {
        va_start(args, format);
        sampgdk_vlogprintf(format, args);
        va_end(args);
    }
    else {
        /* the format and arguments are likely stored on the stack, print the
         * values to a buffer on the heap so it's accessible from the main
         * thread. 
         */

        char *buffer = new char[LEN_PRINT_BUFFER];

        va_start(args, format);
        vsnprintf(buffer, LEN_PRINT_BUFFER, format, args);
        va_end(args);

        buffer[LEN_PRINT_BUFFER - 1] = '\0';

        queue_server_.enqueue([buffer] {
            sampgdk_logprintf("%s", buffer);
            delete[] buffer;
            return (void *)NULL;
        });
    }
}

void server::log_error(const char * format, ...) {
    va_list args;
    va_start(args, format);
    vlog("ERROR", format, args);
    va_end(args);
}

void server::log_info(const char * format, ...) {
    va_list args;
    va_start(args, format);
    vlog("INFO", format, args);
    va_end(args);
}

void server::vlog(const char* prefix, const char *format, va_list args) {
    if (main_thread_ == std::this_thread::get_id()) {
        char buffer[LEN_PRINT_BUFFER];
        vsnprintf(buffer, LEN_PRINT_BUFFER, format, args);
        buffer[LEN_PRINT_BUFFER - 1] = '\0';

        sampgdk_logprintf("[SampSharp:%s] %s", prefix, buffer);
    }
    else {
        /* the format and arguments are likely stored on the stack, print the
        * values to a buffer on the heap so it's accessible from the main
        * thread.
        */

        char *buffer = new char[LEN_PRINT_BUFFER];
        vsnprintf(buffer, LEN_PRINT_BUFFER, format, args);
        buffer[LEN_PRINT_BUFFER - 1] = '\0';

        queue_server_.enqueue([prefix, buffer] {
            sampgdk_logprintf("[SampSharp:%s] %s", prefix, buffer);
            delete[] buffer;
            return (void *)NULL;
        });
    }
}

#pragma endregion

#pragma region Getters

bool server::is_pipe_connected() {
    return pipe_ != PIPE_NONE && pipe_connected_;
}

bool server::is_client_ready() {
    return is_pipe_connected() && started_;
}

#pragma endregion

#pragma region Pipes - Setup

bool server::pipe_create() {
    if (pipe_ != PIPE_NONE) {
        return true;
    }

    LPTSTR name = L"\\\\.\\pipe\\SampSharpSvr2";

    pipe_ = CreateNamedPipe(name,
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

bool server::pipe_connect() {
    if (pipe_ == PIPE_NONE && !pipe_create()) {
        return false;
    }

    if (is_pipe_connected()) {
        return true;
    }

    if (!ConnectNamedPipe(pipe_, NULL)) {
        DWORD error = GetLastError();


        /* ERROR_PIPE_CONNECTED indicates it has connected? */
        if (error == ERROR_PIPE_CONNECTED) {
            log_info("Connected to the pipe.");
            return pipe_connected_ = true;
        }

        /* ERROR_PIPE_LISTENING indicates nothing is listening on the other
         * side of the pipe */
        if (error != ERROR_PIPE_LISTENING) {
            log_error("Failed to connect to pipe with error 0x%x.", error);
        }

        return false;
    }

    log_info("Connected to pipe.");
    return pipe_connected_ = true;
}

void server::pipe_disconnect(const char *context, bool expected) {
    if (!is_pipe_connected()) {
        return;
    }
    
    if (!expected) {
        if (!context) {
            context = "";
        }
        log_error("Unexpected disconnect of pipe. %s", context);

        natives_.clear();
        callbacks_.clear();
        queue_messages_.clear();
    }
    
    /* disconnect and close */
    DisconnectNamedPipe(pipe_);
    CloseHandle(pipe_);
    pipe_ = PIPE_NONE;

    started_ &= expected;
    pipe_connected_ = false;
}

#pragma endregion

#pragma region Pipes - Sending

bool server::cmd_send(uint8_t cmd, uint32_t len, uint8_t *buf) {
    if (!is_pipe_connected()) {
        return false;
    }

    if (!buf) {
        len = 0;
    }

    DWORD tlen;
    if (!WriteFile(pipe_, &cmd, 1, &tlen, NULL) || 
        !WriteFile(pipe_, &len, 4, &tlen, NULL) || 
        !WriteFile(pipe_, buf, len, &tlen, NULL)) {
        log_error("Failed to write to pipe with error 0x%x.", GetLastError());
        pipe_disconnect("Failed to write to pipe.");

        return false;
    }

    return true;
}

void server::cmd_send_gamemode_init() {
    const char* name = "OnGameModeInit";
    uint32_t len = strlen(name) + 1;
    uint8_t *response;

    cmd_send(CMD_PUBLIC_CALL, len, (uint8_t*)name);

    if (cmd_receive_unhandled(&response, &len) && response) {
        delete[] response;
    }
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
    callbacks_.register_buffer(buf);
}

CMD_DEFINE(cmd_find_native) {
    uint32_t handle = (uint32_t)queue_server_.enqueue([this, buf] {
        return (void *)natives_.get_handle(buf);
    }).get();

    cmd_send(CMD_RESPONSE, sizeof(uint32_t), (uint8_t *)&handle);
}

CMD_DEFINE(cmd_invoke_native) {
    uint32_t txlen = (uint32_t)queue_server_.enqueue([this, buf, buflen] {
        uint32_t txlen = LEN_NETBUF;
        natives_.invoke(buf, buflen, txbuf_, &txlen);

        return (void *)txlen;
    }).get();

    cmd_send(CMD_RESPONSE, txlen, txbuf_);
}

CMD_DEFINE(cmd_reconnect) {
    reconnecting_ = true;
    pipe_disconnect(NULL, true);
}

CMD_DEFINE(cmd_start) {
    started_ = true;
    if (gamemode_started_) {
        cmd_send_gamemode_init();
    }
}

#pragma endregion

#pragma region Pipes - Receiving

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

void server::loop() {
    uint8_t *response = NULL;
    uint32_t len;

    for (;;) {
        /* receive calls from the game mode client */
        cmd_receive_one(&response, &len);

        if (response) {
            log_error("Unhandled response in loop.");
            delete[] response;
        }

        /* process jobs on the queue */
        queue_gamemode_.run_all();
    }
}

void server::public_call(AMX *amx, const char *name, cell *params, cell *retval) {
    if (!is_client_ready()) {
        return;
    }

    task_queue::promise promise = queue_gamemode_.enqueue([this,amx,name,params,retval] {
        uint32_t len = callbacks_.fill_call_buffer(amx, name, params, txbuf_, LEN_NETBUF);
        uint8_t *response = NULL;

        if (len == 0 || !is_client_ready()) {
            return (void*)NULL;
        }

        /* send */
        cmd_send(CMD_PUBLIC_CALL, len, txbuf_);
  
        /* receive */
        if(!cmd_receive_unhandled(&response, &len) || !response || len == 0) {
            log_error("Received no response to callback %s.", name);
            return (void*)NULL;
        }

        if (len >= 5 && response[0] && retval) {
            /* get return value */
            *retval = *((uint32_t *)(response + 1));
        }

        delete[] response;
      
        return (void*)NULL;
    });

    // TODO: Timeout mechanism
    while (!promise._Is_ready()) {
        queue_server_.run_all();
    }

    if (!strcmp(name, "OnGameModeInit")) {
        gamemode_started_ = true;
    }
    else if (!strcmp(name, "OnGameModeExit")) {
        gamemode_started_ = false;
    }
}

void server::tick() {
    if (is_client_ready()) {
        /*
        queue_gamemode_.enqueue([this] {
            cmd_send(CMD_TICK, 0, NULL);
            return (void*)NULL;
        });
        /**/
    }
    queue_server_.run_all_for(1000 / 250);// TODO: Improve timing
}
