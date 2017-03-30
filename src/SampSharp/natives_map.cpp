#include "natives_map.h"
#include "platforms.h"
#include "server.h"

#define NATIVE_NOT_FOUND        UINT32_MAX
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

uint32_t natives_map::get_handle(uint8_t *name) {
    /* check for the native in the map */
    std::map<std::string, uint32_t>::const_iterator it = 
        natives_map_.find((char *)name);
    if (it != natives_map_.end()) {
        return it->second;
    }

    /* find the native trough amx */
    AMX_NATIVE native = sampgdk_FindNative((char *)name);

    if (!native) {
        return NATIVE_NOT_FOUND;
    }

    uint32_t handle = natives_.size();
    natives_.push_back(native);
    natives_map_[(char *)name] = handle;

    return handle;
}

void natives_map::invoke(uint8_t *rxbuf, uint32_t rxlen, uint8_t *txbuf, 
    uint32_t *txlen) {
    uint32_t
        arglen,
        txpos = sizeof(uint32_t), /* space for reponse */
        handle = *(uint32_t *)rxbuf;
    void* args[MAX_ARGS];
    char format[MAX_ARGS_FORMAT] = { 0 };

    if (handle >= natives_.size()) {
        *txlen = 0;
        svr_->log_error("Invoking invalid native handle.");
        return;
    }

    for (uint32_t i = sizeof(uint32_t), j = 0; i < rxlen; j++) {
        if (j >= MAX_ARGS) {
            *txlen = 0;
            svr_->log_error("Too many native arguments.");
            return;
        }
        switch (rxbuf[i]) {
            case ARG_VALUE:
                args[j] = &rxbuf[i + 1];
                i += 5;
                sampsharp_strcat(format, MAX_ARGS_FORMAT, "d");
                break;
            case ARG_VALUE_REF:
                if (*txlen < txpos + sizeof(uint32_t)) {
                    *txlen = 0;
                    svr_->log_error("Native output buffer is full.");
                    return;
                }
                args[j] = &txbuf[txpos];
                *(uint32_t *)&txbuf[txpos] = *(uint32_t *)&rxbuf[i + 1];
                txpos += sizeof(uint32_t);
                i += 5;
                sampsharp_strcat(format, MAX_ARGS_FORMAT, "R");
                break;
            case ARG_STRING:
                args[j] = &rxbuf[i + 1];
                i += 2 + strlen((char *)&rxbuf[i + 1]);
                sampsharp_strcat(format, MAX_ARGS_FORMAT, "s");
                break;
            case ARG_STRING_REF:
                arglen = *(uint32_t *)&rxbuf[i + 1];
                if (*txlen < txpos + arglen) {
                    *txlen = 0;
                    svr_->log_error("Native output buffer is full.");
                    return;
                }
                args[j] = txbuf + txpos;
                txpos += arglen;
                i += 5;
                sampsharp_sprintf(format + strlen(format), MAX_ARGS_FORMAT, 
                    "S[%d]", arglen);
                break;
            case ARG_ARRAY:
                arglen = *(uint32_t *)&rxbuf[i + 1];
                args[j] = &rxbuf[i + 1 + sizeof(uint32_t)];
                i += 1 + sizeof(uint32_t) + arglen;
                sampsharp_sprintf(format + strlen(format), MAX_ARGS_FORMAT, 
                    "a[%d]", arglen);
                break;
            case ARG_ARRAY_REF:
                arglen = *(uint32_t *)&rxbuf[i + 1];
                if (*txlen < txpos + arglen) {
                    *txlen = 0;
                    svr_->log_error("Native output buffer is full.");
                    return;
                }
                args[j] = txbuf + txpos;
                txpos += arglen;
                i += 5;
                sampsharp_sprintf(format + strlen(format), MAX_ARGS_FORMAT, 
                    "A[%d]", arglen);
                break;
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
