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

#include <string>
#include <algorithm>
#include <algorithm>

#pragma once

struct StringUtil
{
    struct is_not_space {
        bool operator()(const char c) const {
            return !(c == ' ' || c == '\r' || c == '\n' || c == '\t');
        }
    };
    struct test {
    };

    static inline std::string &TrimStringLeft(std::string &s) {
        s.erase(s.begin(), std::find_if(s.begin(), s.end(), is_not_space()));
        return s;
    }

    static inline std::string &TrimStringRight(std::string &s) {
        s.erase(std::find_if(s.rbegin(), s.rend(), is_not_space()).base(), s.end());
        return s;
    }

    static inline std::string &TrimString(std::string &s) {
        return TrimStringLeft(TrimStringRight(s));
    }

    static char to_lower(char in) {
        if (in <= 'Z' && in >= 'A')
            return in - ('Z' - 'z');
        return in;
    }
    static inline std::string ToLower(std::string s) {
        std::transform(s.begin(), s.end(), s.begin(), to_lower);
        return s;
    }

    static inline bool EndsWith(std::string const & value, std::string const & ending) {
        if (ending.size() > value.size()) return false;
        return std::equal(ending.rbegin(), ending.rend(), value.rbegin());
    }

    static inline bool ToBool(std::string value, bool def) {
        value = TrimString(value);
        value = ToLower(value);

        if(value.length() == 0)
            return def;

        if (!value.compare("on") || !value.compare("yes") || !value.compare("true")) {
            return true;
        }
        else if (!value.compare("off") || !value.compare("no") || !value.compare("false")) {
                return false;
        }
       
       return !!atoi(value.c_str());
    }
};
