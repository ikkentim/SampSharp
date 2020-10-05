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

#include "callbacks_map.h"
#include <assert.h>
#include <string.h>
#include "remote_server.h"
#include "logging.h"

#define ARG_TERM    0x00
#define ARG_VALUE   0x01
#define ARG_ARRAY   0x02
#define ARG_STRING  0x04

callbacks_map::callbacks_map() {
    clear();
}

void callbacks_map::clear() {
    for (std::map<std::string,uint8_t *>::iterator it = callbacks_.begin(); 
        it != callbacks_.end(); it++) {
        delete[] it->second;
    }

    callbacks_.clear();

    uint8_t
        *a = new uint8_t[1],
        *b = new uint8_t[1];
    a[0] = ARG_TERM;
    b[0] = ARG_TERM;

    callbacks_["OnGameModeInit"] = a;
    callbacks_["OnGameModeExit"] = b;
}

void callbacks_map::register_buffer(uint8_t *buf) {
    assert(buf);

    char *name = (char *)buf;
    size_t 
        name_len = strlen(name),
        info_len = 0;
    uint8_t *info = buf + name_len + 1;

    /* verify buffer and measure length */
    while (info[info_len] != ARG_TERM) {
        switch (info[info_len]) {
        case ARG_VALUE:
        case ARG_STRING:
            info_len++;
            break;
        case ARG_ARRAY:
            info_len += 5;
            break;
        default:
            log_error("Invalid callback argument %d.", info[info_len]);
            return;
        }
    }

    info_len++;

    /* remove previous entry */
    std::map<std::string, uint8_t *>::const_iterator it = callbacks_.find(name);
    if (it != callbacks_.end()) {
        delete[] it->second;
    }

    /* insert new entry */
    uint8_t *info_buf = new uint8_t[info_len];
    memcpy(info_buf, info, info_len);

    callbacks_[name] = info_buf;
}

bool callbacks_map::fill_call_buffer(AMX *amx, const char *name, 
    cell *params, uint8_t *buf, uint32_t *len, bool include_name) {
    assert(sizeof(cell) == sizeof(uint32_t));

    uint32_t call_len = 0;

    /* find the callback in the map */
    std::map<std::string, uint8_t *>::const_iterator it = callbacks_.find(name);
    if (it == callbacks_.end()) {
        return false;
    }

    if(include_name) {
        /* fill the buffer with the callback name */
        size_t name_len = strlen(name);
        if (*len < name_len + 1) {
            log_error("Callback buffer too small.");
            return false;
        }

        memcpy(buf, name, name_len + 1);
        call_len = name_len + 1;
    }

    /* fill the buffer with the callback arguments */
    uint32_t i = 0;
    uint32_t params_count = params[0] / sizeof(cell);
    for (uint8_t *info = it->second; *info != ARG_TERM; i++) {
        int val_len = 0;
        cell *val_addr = NULL;
        uint8_t instr = *info;
        info++;

        if (params_count < i) {
            log_error("Callback parameters count mismatch. Only expecting"
                " %d parameters.", params_count);
        }
        switch (instr) {
            case ARG_VALUE:
                if (*len - call_len < sizeof(cell)) {
                    log_error("Callback buffer too small.");
                    return 0;
                }
                memcpy(buf + call_len, params + i + 1, sizeof(cell));
                call_len += sizeof(cell);
                break;
            case ARG_STRING:
                amx_GetAddr(amx, params[i + 1], &val_addr);
                if (val_addr == NULL) {
                    val_len = 0;
                }
                else {
                    amx_StrLen(val_addr, &val_len);
                }

                if ((int)*len - (int)call_len < val_len + 1) {
                    log_error("Callback buffer too small.");
                    return 0;
                }

                if (val_len) {
                    amx_GetString((char *)buf + call_len, val_addr, 0,
                        *len - call_len);
                }
                buf[call_len + val_len] = 0;
                call_len += val_len + 1;
                break;
            case ARG_ARRAY:
                val_len = *(int *)info; /* index of value length */
                info += sizeof(uint32_t);

                if (val_len >= (int)params_count) {
                    log_error("Invalid callback array size indicator %d.", 
                        val_len);
                    return 0;
                }

                val_len = params[val_len + 1];
                amx_GetAddr(amx, params[i + 1], &val_addr);

                /* length */
                memcpy(buf + call_len, &val_len, sizeof(int));
                call_len += sizeof(int);

                /* values */
                for (int j = 0; j < val_len; j++) {
                    memcpy(buf + call_len, val_addr + j, sizeof(cell));
                    call_len += sizeof(cell);
                }
                break;
            default:
                log_error("Invalid callback instruction %d.", instr);
                return false;
        }
    }

    if (params[0] / sizeof(cell) != i) {
        log_error("Callback parameters count mismatch for %s. Expecting %d but "
            "received %d parameters.", name, params_count, i);
        return false;
    }

    *len = call_len;
    return true;
}
