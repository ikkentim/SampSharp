// Copyright (c) 2011-2013, Zeex
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice,
//    this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.

#include <algorithm>
#include <cctype>
#include <cstdlib>
#include <fstream>
#include <iostream>
#include <sstream>
#include <string>

#include "ConfigReader.h"
#include "StringUtil.h"

ConfigReader::ConfigReader()
: loaded_(false)
{
}

ConfigReader::ConfigReader(const std::string &filename)
: loaded_(false)
{
    LoadFile(filename);
}

bool ConfigReader::LoadFile(const std::string &filename) {
    std::ifstream cfg(filename.c_str());

    if (cfg.is_open()) {
        std::string line, name, value;

        while (std::getline(cfg, line, '\n')) {
            std::stringstream stream(line);

            std::getline(stream, name, ' ');
            StringUtil::TrimString(name);

            std::getline(stream, value, '\n');
            StringUtil::TrimString(value);

            options_.insert(std::make_pair(name, value));
        }

        loaded_ = true;
    }

    return loaded_;
}

void ConfigReader::GetOptionAsString(const std::string &name, std::string &value) const {
    value = GetOptionAsStringDefault(name, value);
}

std::string ConfigReader::GetOptionAsStringDefault(const std::string &name, const std::string &default_) const {
    OptionMap::const_iterator iterator = options_.find(name);
    return iterator != options_.end() ? iterator->second : default_;
}
