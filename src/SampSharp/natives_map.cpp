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

#include "natives_map.h"
#include "platforms.h"
#include "server.h"
#include <string.h>
#include <stdio.h>
#include <assert.h>

#define NATIVE_NOT_FOUND        -1
#define MAX_ARGS                (32)
#define MAX_ARGS_FORMAT         (128)
#define LEN_ARG_BUFFER          (8192)

#define ARG_VALUE               (1)
#define ARG_VALUE_REF           (9)
#define ARG_ARRAY               (2) /* require size */
#define ARG_ARRAY_REF           (10)/* require size */
#define ARG_STRING              (4)
#define ARG_STRING_REF          (12)/* require size */

natives_map::natives_map(server *svr) : svr_(svr) {
}

int32_t natives_map::get_handle(uint8_t *name) {
    /* check for the native in the map */
    std::map<std::string, int32_t>::const_iterator it = 
        natives_map_.find((char *)name);
    if (it != natives_map_.end()) {
        return it->second;
    }

    /* find the native trough amx */
    AMX_NATIVE native = sampgdk_FindNative((char *)name);

    if (!native) {
        return NATIVE_NOT_FOUND;
    }

    int32_t handle = natives_.size();
    natives_.push_back(native);
    natives_map_[(char *)name] = handle;

    return handle;
}

void natives_map::invoke(uint8_t *rxbuf, uint32_t rxlen, uint8_t *txbuf, 
    uint32_t *txlen) {
    assert(rxbuf);
    assert(rxlen);
    assert(txbuf);
    assert(txlen);

#define STOP_ERR(err, ...) *txlen = 0; svr_->log_error(err, __VA_ARGS__); return
#define ARG_LEN() *(uint32_t *)(rxbuf + rxpos)
#define ARG_BUF_REQUIRE(len); \
    if (*txlen < txpos + (len)) {STOP_ERR("Native output buffer is full.");}
#define ARG_FORMAT_ADD(c) sampsharp_strcat(format, MAX_ARGS_FORMAT, c)
#define ARG_FORMAT_ADDF(c, ...) sampsharp_sprintf(formattmp,MAX_ARGS_FORMAT, c,\
    ##__VA_ARGS__); sampsharp_strcat(format, MAX_ARGS_FORMAT, formattmp)

    uint32_t
        arglen,
        txpos = sizeof(uint32_t); /* space for response */
    int32_t handle = *(int32_t *)rxbuf;
    void* args[MAX_ARGS];
    char format[MAX_ARGS_FORMAT] = { 0 };
    char formattmp[MAX_ARGS_FORMAT];

    if (handle < 0 || handle >= (int32_t)natives_.size()) {
        STOP_ERR("Invoking invalid native handle.");
    }

    for (uint32_t rxpos = sizeof(uint32_t) + 1, j = 0; rxpos < rxlen; j++, 
        rxpos++) {
        if (j >= MAX_ARGS) {
            STOP_ERR("Too many native arguments.");
        }

        switch (rxbuf[rxpos - 1]) {
            case ARG_VALUE:
                ARG_FORMAT_ADD("d");

                args[j] = rxbuf + rxpos;

                rxpos += sizeof(uint32_t);
                break;
            case ARG_VALUE_REF:
                ARG_BUF_REQUIRE(sizeof(uint32_t));
                ARG_FORMAT_ADD("R");

                args[j] = txbuf + txpos;
                memcpy(txbuf + txpos, rxbuf + rxpos, sizeof(uint32_t));

                txpos += sizeof(uint32_t);
                rxpos += sizeof(uint32_t);
                break;
            case ARG_STRING:
                ARG_FORMAT_ADD("s");

                args[j] = &rxbuf[rxpos];
                rxpos += strlen((char *)(rxbuf + rxpos)) + 1;
                break;
            case ARG_STRING_REF:
                arglen = ARG_LEN();
                ARG_BUF_REQUIRE(arglen);
                ARG_FORMAT_ADDF("S[%d]", arglen);

                args[j] = txbuf + txpos;
                *(char *)args[j] = '\0';

                txpos += arglen;
                rxpos += sizeof(uint32_t);
                break;
            case ARG_ARRAY:
                arglen = ARG_LEN();
                ARG_FORMAT_ADDF("a[%d]", arglen);

                args[j] =  rxbuf + rxpos + sizeof(uint32_t);

                rxpos += sizeof(uint32_t) + arglen * sizeof(uint32_t);
                break;
            case ARG_ARRAY_REF:
                arglen = ARG_LEN();
                ARG_BUF_REQUIRE(arglen);
                ARG_FORMAT_ADDF("A[%d]", arglen);

                args[j] = txbuf + txpos;
                memset(txbuf + txpos, 0, arglen * sizeof(uint32_t));

                txpos += arglen * sizeof(uint32_t);
                rxpos += sizeof(uint32_t);
                break;
            default:
                STOP_ERR("Invalid native argument type. %d @%d@%d", rxbuf[rxpos - 1], j, rxpos - 1);
        }
    }

    *txlen = txpos;
    *(uint32_t*)txbuf = sampgdk::InvokeNativeArray(natives_[handle], format,
                                                   args);
}

void natives_map::clear() {
    natives_.clear();
    natives_map_.clear();
}
