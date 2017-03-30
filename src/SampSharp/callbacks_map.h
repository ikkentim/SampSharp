#pragma once

#include <map>
#include <string>
#include <inttypes.h>
#include <sampgdk/sampgdk.h>

class server;

class callbacks_map
{
public:
    callbacks_map(server *svr);
    void clear();
    void register_buffer(uint8_t *buf);
    uint32_t fill_call_buffer(AMX *amx, const char *name, cell *params, 
        uint8_t *buf, uint32_t len);
private:
    server *svr_;
    std::map<std::string, uint8_t*> callbacks_;
};

