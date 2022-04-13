#pragma once
#include "coreclr_app.h"
#include "nethost.h"

class nethost_coreclr : public nethost {
public:
    ~nethost_coreclr() override;
    bool setup(locator *locator, config* cfg) override;
    void start() override;
    void stop() override;
private:
    void release();

    coreclr_app app_;
    bool running_ = false;
    bool host_init_ = false;
    fs::path coreclr_;
    fs::path gamemode_;
};
