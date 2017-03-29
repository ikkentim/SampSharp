#include "natives_map.h"
#include "server.h"
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
    std::map<std::string, uint32_t>::const_iterator it = natives_map_.find((char *)name);
    if (it != natives_map_.end()) {
        return it->second;
    }

    AMX_NATIVE native = sampgdk_FindNative((char *)name);

    if (!native) {
        return UINT32_MAX;
    }

    uint32_t handle = natives_.size();
    natives_.push_back(native);
    natives_map_[(char *)name] = handle;

    return handle;
}

void natives_map::invoke(uint8_t *rxbuf, uint32_t rxlen, uint8_t *txbuf, uint32_t *txlen) {
    uint32_t
        arglen,
        txpos = sizeof(uint32_t), /* space for reponse */
        handle = *(uint32_t *)rxbuf;

    void* args[MAX_ARGS];
    char format[MAX_ARGS_FORMAT] = { 0 };

    if (handle >= natives_.size()) {
        *txlen = 0;
        return;// TODO: log
    }

    for (uint32_t i = sizeof(uint32_t), j = 0; i < rxlen; j++) {
        if (j >= MAX_ARGS) {
            *txlen = 0;
            printf("[Server] too many arguments!\n");// TODO: proper logging
            return;
        }
        switch (rxbuf[i]) {
            case ARG_VALUE:
                args[j] = &rxbuf[i + 1];
                i += 5;
                strcat(format, "d");
                break;
            case ARG_VALUE_REF:
                if (*txlen < txpos + sizeof(uint32_t)) {
                    *txlen = 0;
                    printf("[Server] output buffer is full!\n");// TODO: proper logging
                    return;
                }
                args[j] = &txbuf[txpos];
                *(uint32_t *)&txbuf[txpos] = *(uint32_t *)&rxbuf[i + 1];
                txpos += sizeof(uint32_t);
                i += 5;
                strcat(format, "R");
                break;
            case ARG_STRING:
                args[j] = &rxbuf[i + 1];
                i += 2 + strlen((char *)&rxbuf[i + 1]);
                strcat(format, "s");
                break;
            case ARG_STRING_REF:
                arglen = *(uint32_t *)&rxbuf[i + 1];
                if (*txlen < txpos + arglen) {
                    *txlen = 0;
                    printf("[Server] output buffer is full!! %d < %d+%d\n", *txlen, txpos, arglen);// TODO: proper logging
                    printf("%d %d %d %d", *(uint32_t *)&rxbuf[i + 0], *(uint32_t *)&rxbuf[i + 1], *(uint32_t *)&rxbuf[i + 2], *(uint32_t *)&rxbuf[i + 3]);
                    return;
                }
                args[j] = txbuf + txpos;
                txpos += arglen;
                i += 5;
                sprintf(format + strlen(format), "S[%d]", arglen);
                break;
            case ARG_ARRAY:
                arglen = *(uint32_t *)&rxbuf[i + 1];
                args[j] = &rxbuf[i + 1 + sizeof(uint32_t)];
                i += 1 + sizeof(uint32_t) + arglen;
                sprintf(format + strlen(format), "a[%d]", arglen);
                break;
            case ARG_ARRAY_REF:
                arglen = *(uint32_t *)&rxbuf[i + 1];
                if (*txlen < txpos + arglen) {
                    *txlen = 0;
                    printf("[Server] output buffer is full!!!\n");// TODO: proper logging
                    return;
                }
                args[j] = txbuf + txpos;
                txpos += arglen;
                i += 5;
                sprintf(format + strlen(format), "A[%d]", arglen);
                break;
        }
    }

    *(uint32_t*)txbuf = sampgdk::InvokeNativeArray(natives_[handle], format, args);
    *txlen = txpos;
}

void natives_map::clear() {
    natives_.clear();
    natives_map_.clear();
}
