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
#include <chrono> // todo: only for debugging
#ifdef SAMPSHARP_WINDOWS

#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#pragma comment (lib, "ws2_32.lib")
#include <winsock2.h>

#define vsnprintf vsprintf_s

#elif SAMPSHARP_LINUX
#endif

#include "server.h"

#define LEN_PRINT_BUFFER    (1024)
#define LEN_NETBUF          (16384)

/* receive */
#define CMD_PING            (0x01) /* request a pong */
#define CMD_PRINT           (0x02) /* print data */
#define CMD_RESPONSE        (0x03) /* response to public call */
#define CMD_RECONNECT       (0x04) /* expect client to reconnect */ /* TODO: Unimplemented */
#define CMD_REGISTER_CALL   (0x05) /* register a public call */
#define CMD_FIND_NATIVE     (0x06) /* return native id */
#define CMD_INVOKE_NATIVE   (0x07) /* invoke a native */ /* TODO: Unimplemented */
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
    sock_server_(SOCKET_ERROR),
    sock_client_(SOCKET_ERROR),
    gamemode_started_(false),
    started_(false),
    reconnecting_(false) {
    /* networking buffer */
    rxbuf_ = new uint8_t[LEN_NETBUF];
    txbuf_ = new uint8_t[LEN_NETBUF];

    /* initialize windows sockets */
    WSADATA WsaDat;
    WSAStartup(MAKEWORD(2, 2), &WsaDat);

    /* store main thread handle for later reference  */
    main_thread_ = std::this_thread::get_id();

    /* setup the server sockets to allow clients to connect */
    setup_sock_server();
}

server::~server() {
    delete[] rxbuf_;
    delete[] txbuf_;

    if (sock_client_ != SOCKET_ERROR) {
        // TODO: kill
    }
    if (sock_server_ != SOCKET_ERROR) {
        // TODO: kill
    }

    // TODO: kill thread
}

DWORD WINAPI server_loop_f(LPVOID svr_ptr) {
    assert(svr_ptr);
    ((server *)svr_ptr)->loop();

    return 0;
}

void server::load() {
    HANDLE handle = CreateThread(0, 0, server_loop_f, this, 0, NULL);
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

#pragma endregion

#pragma region Getters

bool server::is_client_connected() {
    return sock_client_ != SOCKET_ERROR;
}

bool server::is_client_ready() {
    return is_client_connected() && started_;
}

#pragma endregion

#pragma region Networking - Setup

bool server::setup_sock_server() {
    if (sock_server_ != SOCKET_ERROR) {
        return true;
    }

    /* create socket */
    sock_server_ = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

    /* bind socket */
    SOCKADDR_IN serverInf;
    serverInf.sin_family = AF_INET;
    serverInf.sin_addr.s_addr = INADDR_ANY;
    serverInf.sin_port = htons(8888); // TODO: config

    if (bind(sock_server_, (SOCKADDR*)(&serverInf), sizeof(serverInf))) {
        print("Failed to bind server socket!");
        return false;
    }

    /* listen to incoming messages */
    listen(sock_server_, 1);

    u_long iMode = 1;
    ioctlsocket(sock_server_, FIONBIO, &iMode);
    return true;
}

bool server::setup_sock_client() {
    if (is_client_connected()) {
        return true;
    }

    sock_client_ = accept(sock_server_, NULL, NULL); // TODO: check remote

    if (sock_client_ == SOCKET_ERROR) {
        return false;
    }

    int on = 1;
    setsockopt(sock_client_, IPPROTO_TCP, TCP_NODELAY, (const char *)&on, sizeof(on));

    u_long iMode = 1;
    ioctlsocket(sock_client_, FIONBIO, &iMode);

    return true;
}

void server::disconnect_client(bool expected) {
    if (!is_client_connected()) {
        return;
    }

    shutdown(sock_client_, SD_SEND);
    closesocket(sock_client_);
    sock_client_ = SOCKET_ERROR;

    // TODO: Clear buffers
    if (!expected) {
        natives_.clear();
        callbacks_.clear();
    }

    started_ &= expected;
}

#pragma endregion

#pragma region Networking - Sending

bool server::cmd_send(uint8_t cmd, uint32_t len, uint8_t *buf) {
    if (!is_client_connected()) {
        return false;
    }

    if (!buf) {
        len = 0;
    }

    uint32_t lennet = len;

    send(sock_client_, (const char *)&cmd, sizeof(uint8_t), 0);
    send(sock_client_, (const char *)&lennet, sizeof(uint32_t), 0);

    if (len > 0) {
        send(sock_client_, (const char *)buf, len, 0);
    }

    int error = WSAGetLastError();
    if (error != 0) {
        printf("err sock %d\n", error);

        disconnect_client();
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

#pragma region Networking - Receiving

server::cmd_status server::cmd_process(uint8_t cmd, uint8_t *buf, 
    uint32_t buflen, uint8_t **resp, uint32_t *resplen) {
    switch (cmd) {
        case CMD_PING:
            cmd_send(CMD_PONG, 0, NULL);
            return cmd_status::handled;
        case CMD_PRINT:
            print("%s", buf);
            return cmd_status::handled;
        case CMD_REGISTER_CALL:
            callbacks_.register_buffer(buf);
            return cmd_status::handled;
        case CMD_FIND_NATIVE:
        {
            uint32_t handle = (uint32_t)queue_server_.enqueue([this, buf] {
                return (void *)natives_.get_handle(buf);
            }).get();

            cmd_send(CMD_RESPONSE, sizeof(uint32_t), (uint8_t *)&handle);
            return cmd_status::handled;
        }
        case CMD_INVOKE_NATIVE:
        {
            using namespace std;
            using namespace std::chrono;

            //printf("[Server] Received call to native %d...\n", *(uint32_t*)buf);

            high_resolution_clock::time_point t1 = high_resolution_clock::now();
            uint32_t txlen = (uint32_t)queue_server_.enqueue([this, buf, buflen] {
                uint32_t txlen = LEN_NETBUF;
                natives_.invoke(buf, buflen, txbuf_, &txlen);

                return (void *)txlen;
            }).get();

            high_resolution_clock::time_point t2 = high_resolution_clock::now();

            auto duration = duration_cast<microseconds>(t2 - t1).count();

            printf("[Server] Native call took %d micros\n", duration);
            cmd_send(CMD_RESPONSE, txlen, txbuf_);

            high_resolution_clock::time_point t3 = high_resolution_clock::now(); 
            duration = duration_cast<microseconds>(t3 - t2).count();

            printf("[Server] Send reply took %d micros\n", duration);
            return cmd_status::handled;
        }
        case CMD_RECONNECT:
            reconnecting_ = true;
            disconnect_client(true);
            return cmd_status::handled;
        case CMD_START:
            started_ = true;
            if (gamemode_started_) {
                cmd_send_gamemode_init();
            }
            return cmd_status::handled;
        case CMD_RESPONSE:
        default:
            if (buflen > 0) {
                *resp = new uint8_t[buflen];
                memcpy(*resp, buf, buflen);
                *resplen = buflen;
            }
            return cmd_status::unhandled;
    }
}

server::cmd_status server::cmd_receive_one(uint8_t **response, uint32_t *len) {
    assert(response);
    assert(len);
    assert(sizeof(unsigned long) == sizeof(uint32_t));

    if (!setup_sock_client()) {
        return cmd_status::conn_dead;
    }
    
    int result = recv(sock_client_, (char *)rxbuf_, LEN_NETBUF, 0);
    if (result < 0) {
        int error = WSAGetLastError();
        if (error != WSAEWOULDBLOCK && error != 0)
        {
            print("recv err: %d", error);
            shutdown(sock_client_, SD_SEND);
            closesocket(sock_client_);

            sock_client_ = SOCKET_ERROR;

            return cmd_status::conn_dead;
        }
    }

    if (result > 0) {
        queue_messages_.add(rxbuf_, result);
    }

    if (!queue_messages_.can_get()) {
        return cmd_status::no_cmd;
    }

    uint8_t command;
    uint32_t command_len = queue_messages_.get(&command, rxbuf_, LEN_NETBUF);

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

#pragma endregion

void server::loop() {
    uint8_t *response = NULL;
    uint32_t len;

    for (;;) {
        /* receive calls from the game mode client */
        cmd_receive_one(&response, &len);

        if (response) {
            print("UNHANDLED RESPONSE IN LOOP!!!!");// TODO
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
            printf("received no response!\n"); // todo
            return (void*)NULL;
        }

        if (len >= 5 && response[0] && retval) {
            /* get return value */
            uint32_t ret = *((uint32_t *)(response + 1));

            printf("retval %s %d\n", name, ret);
            *retval = ret;
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
