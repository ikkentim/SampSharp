#include "message_queue.h"
#include <assert.h>

message_queue::message_queue() : local_fill_(false)
{
}

void message_queue::add(uint8_t *buf, uint32_t len) {
    assert(buf);

    for (uint32_t i = 0; i < len; i++) {
        queue_.push_back(buf[i]);
    }
}

bool message_queue::can_get() {
    if (!try_fill_local()) {
        return false;
    }

    return queue_.size() >= command_length_;
}

uint32_t message_queue::get(uint8_t *command, uint8_t *buf, uint32_t len) {
    assert(command);
    assert(buf);

    if (!can_get()) {
        return 0;
    }

    if (command_length_ > len) {
        printf("BUFFER TOO SMALL!!!\n");// TODO
        for (uint32_t i = 0; i < command_length_; i++) {
            pop();
        }
        return 0;
    }
    *command = command_;
    for (uint32_t i = 0; i < command_length_; i++) {
        buf[i] = pop();
    }

    local_fill_ = false;

    return command_length_;
}

bool message_queue::try_fill_local() {
    if (local_fill_) {
        return true;
    }

    if (queue_.size() < 4) {
        return false;
    }

    command_ = pop();

    /*
    command_length_ = (uint32_t)(
        (((uint32_t)pop()) << 24) |
        (((uint32_t)pop()) << 16) |
        (((uint32_t)pop()) << 8) |
        (uint32_t)pop());
    */
    command_length_ = (uint32_t)(
        ((uint32_t)pop() << 0) |
        ((uint32_t)pop() << 8) |
        ((uint32_t)pop() << 16) |
        ((uint32_t)pop() << 24)
        );

    local_fill_ = true;

    return true;
}

uint8_t message_queue::pop()
{
    uint8_t v = queue_.front();
    queue_.pop_front();
    return v;
}
