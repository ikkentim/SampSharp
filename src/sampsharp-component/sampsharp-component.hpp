#pragma once

#include <sdk.hpp>

#include "managed-host.hpp"
#include "platform.hpp"

struct SampSharpInfo
{
    SampSharpInfo(int api_version, SemanticVersion version) : size(sizeof(SampSharpInfo)),
                                                              api_version(api_version),
                                                              version(version) {}

    // sizeof(SampSharpInfo) for backwards compatibility
    size_t size;
    // version of SampSharp component <> hosted API. Version mismatch will cause launch failure.
    int api_version;
    // version of the SampSharp component
    SemanticVersion version;
};

typedef void(API_CALLTYPE *on_cleanup_fn)();
typedef void(API_CALLTYPE *on_free_component_fn)(IComponent *component);

typedef void(API_CALLTYPE *configure_callback_fn)(void **cb);

struct SampSharpInitParams
{
    SampSharpInitParams(ICore *core,
                        IComponentList *componentList,
                        SampSharpInfo *info,
                        configure_callback_fn setOnCleanup,
                        configure_callback_fn setOnFreeComponent) : size(sizeof(SampSharpInitParams)),
                                                                    info(info),
                                                                    core(core),
                                                                    componentList(componentList),
                                                                    setOnCleanup(setOnCleanup),
                                                                    setOnFreeComponent(setOnFreeComponent) {}

    // sizeof(SampSharpInitParams) for backwards compatibility
    size_t size;
    SampSharpInfo *info;
    ICore *core;
    IComponentList *componentList;
    configure_callback_fn setOnCleanup;
    configure_callback_fn setOnFreeComponent;
};

typedef void(CORECLR_DELEGATE_CALLTYPE *on_init_fn)(SampSharpInitParams);

struct ISampSharpComponent : IComponent
{
    PROVIDE_UID(0x0B61929D1E94A319);
};

class SampSharpComponent final
    : public ISampSharpComponent
{
private:
    ICore *core_ = nullptr;
    ManagedHost managed_host_{};
    inline static SampSharpComponent *instance_ = nullptr;
    on_cleanup_fn on_cleanup_ = nullptr;
    on_free_component_fn on_free_component_ = nullptr;

public:
    StringView componentName() const override;

    SemanticVersion componentVersion() const override;

    void onLoad(ICore *c) override;

    void provideConfiguration(ILogger &logger, IEarlyConfig &config, bool defaults) override;

    void onInit(IComponentList *components) override;

    void onReady() override;

    void free() override;

    void onFree(IComponent *component) override;

    void reset() override;

    void setOnCleanup(on_cleanup_fn cb) { on_cleanup_ = cb; }

    void setOnFreeComponent(on_free_component_fn cb) { on_free_component_ = cb; }

    static SampSharpComponent *getInstance();
};
