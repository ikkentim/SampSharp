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

#include <map>
#include <sstream>
#include <string>

#pragma once

class ConfigReader {
public:
    typedef std::map<std::string, std::string> OptionMap;

    ConfigReader();
    ConfigReader(const std::string &filename);

    bool LoadFile(const std::string &filename);

    template<typename T>
    void GetOption(const std::string &name, T &value) const;

    void GetOptionAsString(const std::string &name, std::string &value) const;

    template<typename T>
    T GetOptionDefault(const std::string &name, const T &defaultValue) const;

    std::string GetOptionAsStringDefault(const std::string &name, const std::string &defaultValue) const;

    bool IsLoaded() const { return loaded_; }

private:
    bool loaded_;
    OptionMap options_;
};

template<typename T>
void ConfigReader::GetOption(const std::string &name, T &value) const {
    value = GetOptionDefault(name, value);
}

template<typename T>
T ConfigReader::GetOptionDefault(const std::string &name,
    const T &default_) const {
    OptionMap::const_iterator iterator = options_.find(name);
    if (iterator != options_.end()) {
        std::stringstream sstream(iterator->second);
        T value;
        sstream >> value;
        if (sstream) {
            return value;
        }
    }
    return default_;
}
