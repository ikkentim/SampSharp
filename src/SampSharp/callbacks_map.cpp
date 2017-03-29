#include "callbacks_map.h"
#include <assert.h>
#include "server.h"

#define ARG_TERM    0x00
#define ARG_VALUE   0x01
#define ARG_ARRAY   0x02
#define ARG_STRING  0x04

callbacks_map::callbacks_map(server *svr) : svr_(svr) {
    clear();
}


callbacks_map::~callbacks_map() {
}

void callbacks_map::clear() {
    for (std::map<std::string,uint8_t *>::iterator it = callbacks_.begin(); it != callbacks_.end(); it++) {
        delete[] it->second;
    }

    callbacks_.clear();

    uint8_t *e = new uint8_t[1];
    e[0] = ARG_TERM;

    callbacks_["OnGameModeInit"] = e;
    callbacks_["OnGameModeExit"] = e;
}

void callbacks_map::register_buffer(uint8_t *buf) {
    char *name = (char *)buf;
    size_t name_len = strlen(name);

    uint8_t *info = buf + name_len + 1;
    size_t info_len = 0;
    
    printf("[Server] Registering %s\n", name);
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
            printf("ERROR: invalid cb arguments (value %d)\n", info[info_len]);// TODO: proper logging (prefixed)
            return;
        }
    }

    info_len++;

    uint8_t *info_buf = new uint8_t[info_len];
    memcpy(info_buf, info, info_len);

    // TODO: Clear previous entries
    callbacks_[name] = info_buf;
}

uint32_t callbacks_map::fill_call_buffer(AMX *amx, const char *name, 
    cell *params, uint8_t *buf, uint32_t len) {
    assert(sizeof(cell) == sizeof(uint32_t));

    uint32_t call_len = 0;
    std::map<std::string, uint8_t *>::const_iterator it = callbacks_.find(name);
    if (it == callbacks_.end()) {
        return 0;
    }

    size_t name_len = strlen(name);
    if (len < name_len + 1) {
        svr_->print("ERROR:buffer too small!");//TODO: logging
        return 0;
    }

    memcpy(buf, name, name_len + 1);
    call_len = name_len + 1;

    uint32_t i = 0;
    uint32_t params_count = params[0] / sizeof(cell);
    for (uint8_t *info = it->second; *info != ARG_TERM; i++) {
        int val_len = NULL;
        cell *val_addr = NULL;
        uint8_t instr = *info;
        info++;

        if (params_count < i) {
            svr_->print("ERROR: cb arguments mismatch!");//TODO: logging
        }
        switch (instr) {
            case ARG_VALUE:
                if (len - call_len < sizeof(cell)) {
                    svr_->print("ERROR:buffer too small!");//TODO: logging
                    return 0;
                }
                memcpy(buf + call_len, (const char *)&params[i + 1], sizeof(cell));
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

                if ((int)len - (int)call_len < val_len + 1) {
                    svr_->print("ERROR:buffer too small!");//TODO: logging
                    return 0;
                }

                if (val_len) {
                    printf("[Server] AMX_GETSTRING LEN %d\n", val_len);
                    amx_GetString((char *)buf + call_len, val_addr, 0, len);
                }
                buf[call_len + val_len] = 0;
                call_len += val_len + 1;
                break;
            case ARG_ARRAY:
                val_len = *(int *)info; /* index of value length */
                info += sizeof(uint32_t);

                if (val_len >= (int)params_count) {
                    svr_->print("ERROR:invalid cb params (array size index)!");//TODO: logging
                    return 0;
                }

                val_len = params[val_len + 1];
                amx_GetAddr(amx, params[i + 1], &val_addr);

                /* length */
                memcpy(buf + call_len, (const char *)&val_len, sizeof(int));
                call_len += sizeof(int);

                /* values */
                for (int j = 0; j < val_len; j++) {
                    memcpy(buf + call_len, (const char *)(val_addr + j), sizeof(cell));
                    call_len += sizeof(cell);
                }
                break;
            default:
                svr_->print("ERROR: invalid cb arguments");// TODO: proper logging (prefixed)
                return 0;
        }
    }

    if (params[0] / sizeof(cell) != i) {
        svr_->print("ERROR: cb arguments mismatch!");//TODO: logging
    }

    return call_len;
}
