#pragma once
#include <inttypes.h>
#include <vector>
#include <string>
#include <map>
#include <sampgdk/sampgdk.h>

class server;

class natives_map
{
public:
    natives_map(server *svr);
    int32_t get_handle(uint8_t *name);
    void invoke(uint8_t *rxbuf, uint32_t rxlen, uint8_t *txbuf, uint32_t *txlen);
    void clear();
private:
    std::vector<AMX_NATIVE> natives_;
    std::map<std::string,int32_t> natives_map_;
    server *svr_;
};

