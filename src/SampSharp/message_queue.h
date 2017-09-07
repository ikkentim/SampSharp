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

#pragma once

#include <inttypes.h>
#include <deque>

#define MESSAGE_QUEUE_BUFFER_TOO_SMALL  0xffffffffu

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

