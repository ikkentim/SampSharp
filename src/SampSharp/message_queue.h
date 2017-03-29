#pragma once
#include <inttypes.h>
#include <deque>
class message_queue
{
public:
    message_queue();
    void add(uint8_t *buf, uint32_t len);
    bool can_get();
    uint32_t get(uint8_t *command, uint8_t *buf, uint32_t len);
private:
    std::deque<uint8_t> queue_;
    uint8_t command_;
    uint32_t command_length_;
    bool local_fill_;
    bool try_fill_local();
    uint8_t pop();
};

