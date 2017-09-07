#include "intermission.h"
#include <sampgdk/sampgdk.h>
#include "StringUtil.h"

extern void *pAMXFunctions;

intermission::intermission(plugin *plg) :
    on_(false),
    enable_(true),
    plg_(plg){

    std::string value;
    plg->config()->GetOptionAsString("intermission", value);
    if (value.length() > 0) {
        value = StringUtil::TrimString(value);
        value = StringUtil::ToLower(value);
        if (!value.compare("on") || !value.compare("yes") || !value.compare("true")) {
            enable_ = true;
        }
        else if (!value.compare("off") || !value.compare("no") || !value.compare("false")) {
                enable_ = false;
            }
        else {
            enable_ = !!atoi(value.c_str());
        }
    }
}


intermission::~intermission() {
        set_on(false);
}

bool intermission::is_on() {
    return enable_ && on_;
}

void intermission::set_on(bool on) {
    if (!enable_ || is_on() == on) {
        return;
    }

    on_ = on;
    sampgdk_SendRconCommand(on ? "loadfs intermission" : "unloadfs intermission");
}

void intermission::signal_starting() {
    set_on(true);
    if (is_on()) {
        plg_->filterscript_call("OnSampSharpStart");
    }
}

void intermission::signal_disconnect() {
    set_on(true);
    if (is_on()) {
        plg_->filterscript_call("OnSampSharpDisconnect");
    }
}

void intermission::signal_error() {
    set_on(true);
    if (is_on()) {
        plg_->filterscript_call("OnSampSharpError");
    }
}
