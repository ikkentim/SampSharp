#pragma once

#include <sdk.hpp>

#include "managed-host.hpp"
#include "sampsharp-api.hpp"

struct ISampSharpComponent : IComponent
{
    PROVIDE_UID(0x0B61929D1E94A319);
};

class SampSharpComponent final : public ISampSharpComponent
{
private:
    ICore* core_ = nullptr;
    ManagedHost managed_host_{};
    inline static SampSharpComponent* instance_ = nullptr;
    on_cleanup_fn on_cleanup_ = nullptr;
    on_ready_fn on_ready_ = nullptr;
    on_free_component_fn on_free_component_ = nullptr;

public:
    StringView componentName() const override;

    SemanticVersion componentVersion() const override;

    void onLoad(ICore* c) override;

    void provideConfiguration(ILogger& logger, IEarlyConfig& config, bool defaults) override;

    void onInit(IComponentList* components) override;

    void onReady() override;

    void free() override;

    void onFree(IComponent* component) override;

    void reset() override;

    void setOnCleanup(on_cleanup_fn cb) { on_cleanup_ = cb; }

    void setOnReady(on_ready_fn cb) { on_ready_ = cb; }

    void setOnFreeComponent(on_free_component_fn cb) { on_free_component_ = cb; }

    static SampSharpComponent* getInstance();
};
