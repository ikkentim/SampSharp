#pragma once

// This header defines the API for the SampSharp managed components. Through this API the managed component is provided
// with access to the open.mp infrastructure and ties the managed component to the lifecycle of this unmanaged component.
// This API is implemented by the SampSharp.Sdk/SampSharp.OpenMp.Core packages.
#include <coreclr_delegates.h>
#include <sdk.hpp>

#include "platform.hpp"

// Provides version information about the SampSharp component and the hosted API. This is used to check for version
// mismatches that would cause launch failure.
struct SampSharpInfo
{
    SampSharpInfo(int api_version, SemanticVersion version) :
        size(sizeof(SampSharpInfo)),
        api_version(api_version),
        version(version)
    {
    }

    // sizeof(SampSharpInfo) for backwards compatibility
    size_t size;
    // version of SampSharp component <> hosted API. Version mismatch will cause launch failure.
    int api_version;
    // version of the SampSharp component
    SemanticVersion version;
};

// Delegate type for the cleanup callback, called when the component is unloaded during shutdown.
typedef void(API_CALLTYPE* on_cleanup_fn)();

// Delegate type for the ready callback, called when all components are loaded open.mp is ready.
typedef void(API_CALLTYPE* on_ready_fn)();

// Delegate type for the on_free_component callback, called when a component is freed.
typedef void(API_CALLTYPE* on_free_component_fn)(IComponent* component);

// Delegate type for the configure callback function, used to set the various callbacks.
typedef void(API_CALLTYPE* configure_callback_fn)(void** cb);

// Parameters for initializing the managed component, passed to the on_init callback.
// This is a struct instead of parameters for backwards compatibility, so parameters can be added without breaking the
// API. The size field is used to check which version of the struct is being used, so new fields should only be added
// at the end.
struct SampSharpInitParams
{
    SampSharpInitParams(ICore* core, IComponentList* componentList, SampSharpInfo* info,
                        configure_callback_fn setOnCleanup, configure_callback_fn setOnFreeComponent, 
                        configure_callback_fn setOnReady) :
        size(sizeof(SampSharpInitParams)),
        info(info),
        core(core),
        componentList(componentList),
        setOnCleanup(setOnCleanup),
        setOnFreeComponent(setOnFreeComponent),
        setOnReady(setOnReady)
    {
    }

    // sizeof(SampSharpInitParams) for backwards compatibility
    size_t size;
    // Version info about the SampSharp component
    SampSharpInfo* info;
    // Pointer to open.mp ICore
    ICore* core;
    // Pointer to open.mp IComponentList
    IComponentList* componentList;
    // Function to configure a cleanup callback. Callback should have signature on_cleanup_fn.
    configure_callback_fn setOnCleanup;
    // Function to configure a onFreeComponent callback. Callback should have signature configure_callback_fn.
    configure_callback_fn setOnFreeComponent;
    // Function to configure a ready callback. Callback should have signature on_ready_fn.
    configure_callback_fn setOnReady;
};

// Delegate type for the on_init callback, called when the component is loaded. The callback should initialize the
// managed component and optionally set the various callbacks.
typedef void(CORECLR_DELEGATE_CALLTYPE* on_init_fn)(SampSharpInitParams);