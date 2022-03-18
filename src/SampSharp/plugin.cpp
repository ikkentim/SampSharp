// SampSharp
// Copyright 2018 Tim Potze
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

#include "plugin.h"
#include "logging.h"
#include "StringUtil.h"
#include <regex>

plugin::plugin() : host_(nullptr),
				   configvalid_(false),
                   config_(ConfigReader("server.cfg")) {
}

plugin::~plugin() {
	if(host_) {
		delete host_;
        host_ = nullptr;
	}
}

void plugin::config(const std::string &name, std::string &value) const {
    config_.GetOptionAsString(name, value);
}

bool plugin::config_validate() {
    std::string
        gamemode,
        skip_empty_check,
		hostfxr,
		gamemode_base;

    config("skip_empty_check", skip_empty_check);
    config("hostfxr", hostfxr);
    config("gamemode", gamemode);
    config("gamemode_base", gamemode_base);

    if(!StringUtil::ToBool(skip_empty_check, false)) {
    /* check whether gamemodeN values contain acceptable values. */
        for (int i = 0; i < 15; i++) {
            std::ostringstream gamemode_key;
            gamemode_key << "gamemode";
            gamemode_key << i;

            std::string gamemode_value;
            config(gamemode_key.str(), gamemode_value);
            gamemode_value = StringUtil::TrimString(gamemode_value);

            if (i == 0 && gamemode_value.compare("empty 1") != 0) {
                log_error("Can not load sampsharp if a non-SampSharp gamemode is "
                    "set to load.");
                log_error("Please ensure you set 'gamemode0 empty 1' in your "
                    "server.cfg file.");
                return false;
            }
            if (i > 0 && gamemode_value.length() > 0) {
                log_error("Can not load sampsharp if a non-SampSharp gamemode is "
                    "set to load.");
                log_error("Please ensure you only specify one script gamemode, "
                    "namely 'gamemode0 empty 1' in your server.cfg file.");
                return false;
            }
        }
    }
     
    if(!locator_.locate(hostfxr, gamemode_base, gamemode)) {
	    return false;
    }
    
    configvalid_ = true;
    
    return true; 
}

bool plugin::start() {
    if(!is_config_valid()) {
	    return false;
    }
    
	const auto host = new gmhost(locator_.get_hostfxr(), locator_.get_gamemode());

    if(!host->start()) {
	    delete host;
        return false;
    }

    host_ = host;
    return true;
}

gmhost *plugin::host() const {
	return host_;
}

bool plugin::is_config_valid() const {
	return configvalid_;
}
