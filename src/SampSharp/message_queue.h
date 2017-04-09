#pragma once
#include <inttypes.h>
#include <deque>

#define MESSAGE_QUEUE_BUFFER_TOO_SMALL  UINT32_MAX

class server;
class message_queue
{
public:
    message_queue();
    void add(uint8_t *buf, uint32_t len);
    bool can_get();
    uint32_t get(uint8_t *command, uint8_t *buf, uint32_t len);
    void clear();
private:
    std::deque<uint8_t> queue_;
    uint8_t command_;
    uint32_t command_length_;
    bool local_fill_;
    bool try_fill_local();
    uint8_t pop();
};

