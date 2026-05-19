#include "sampsharp-component.hpp"
#include "crash-handler.hpp"
#include "version.hpp"

#define CFG_DIRECTORY "sampsharp.directory"
#define CFG_ASSEMBLY "sampsharp.assembly"
#define CFG_ENTRY_POINT_TYPE "sampsharp.entry_point_type"
#define CFG_ENTRY_POINT_METHOD "sampsharp.entry_point_method"
#define CFG_DISABLE_CRASH_HANDLER "sampsharp.disable_crash_handler"

static void __CDECL __setOnCleanup(void** cb)
{
    SampSharpComponent::getInstance()->setOnCleanup((on_cleanup_fn)cb);
}

static void __CDECL __setOnFreeComponent(void** cb)
{
    SampSharpComponent::getInstance()->setOnFreeComponent((on_free_component_fn)cb);
}

StringView SampSharpComponent::componentName() const
{
    return "SampSharp";
}

SemanticVersion SampSharpComponent::componentVersion() const
{
    return { VERSION_MAJOR, VERSION_MINOR, VERSION_PATCH, 0 };
}

void SampSharpComponent::onLoad(ICore* c)
{
    core_ = c;

    bool disableCrashHandler = *c->getConfig().getBool(CFG_DISABLE_CRASH_HANDLER);
    if (!disableCrashHandler)
    {
        sampsharp::crash::install(c);
    }

#if VERSION_PRERELEASE > 0
    c->logLn(LogLevel::Warning, "SampSharp - You are running a prerelease version of SampSharp. Expect instability and report any issues you encounter to the developers.");
    c->logLn(LogLevel::Warning, "SampSharp - Version: %d.%d.%d prerelease %d", VERSION_MAJOR, VERSION_MINOR, VERSION_PATCH, VERSION_PRERELEASE);
    c->logLn(LogLevel::Warning, "SampSharp - Report issues here: https://github.com/ikkentim/SampSharp/issues");
#endif
}

void SampSharpComponent::provideConfiguration(ILogger& logger, IEarlyConfig& config, const bool defaults)
{
#define initConfigString(key, value)                                                                                   \
    if (defaults || config.getType(key) == ConfigOptionType_None)                                                      \
    {                                                                                                                  \
        config.setString(key, value);                                                                                  \
    }

    initConfigString(CFG_DIRECTORY, "gamemode");
    initConfigString(CFG_ASSEMBLY, "GameMode");
    initConfigString(CFG_ENTRY_POINT_TYPE, "SampSharp.Entrypoint");
    initConfigString(CFG_ENTRY_POINT_METHOD, "Initialize");

    if (defaults || config.getType(CFG_DISABLE_CRASH_HANDLER) == ConfigOptionType_None)
    {
        config.setBool(CFG_DISABLE_CRASH_HANDLER, false);
    }
}

void SampSharpComponent::onInit(IComponentList* components)
{
    const IConfig& config = core_->getConfig();

    const auto directory = config.getString(CFG_DIRECTORY);
    const auto assembly = config.getString(CFG_ASSEMBLY);
    const auto entry_point_type = config.getString(CFG_ENTRY_POINT_TYPE);
    const auto entry_point_method = config.getString(CFG_ENTRY_POINT_METHOD);

    std::string entry_point = entry_point_type.to_string() + ", " + assembly.to_string();
    const auto full_entry_point = StringView(entry_point);

    const char* error = nullptr;

    if (!managed_host_.initialize(&error))
    {
        core_->logLn(Error,
                     "Failed to initialize the .NET host framework resolver. Has the .NET runtime been installed?");
        core_->logLn(Error, "Error message: %s", error);
        return;
    }

    if (!managed_host_.loadFor(directory, assembly, &error))
    {
        core_->logLn(Error,
                     "Failed to initialize the .NET runtime for '%s/%s'. Is the '*.runtimeconfig.json' file available? "
                     "Is the .NET runtime available?",
                     directory.to_string().c_str(), assembly.to_string().c_str());
        core_->logLn(Error, "Error message: %s", error);
        return;
    }

    on_init_fn on_init;
    if (!managed_host_.getEntryPoint(full_entry_point, entry_point_method, reinterpret_cast<void**>(&on_init), &error))
    {
        core_->logLn(Error, "The entrypoint '%s.%s, %s' could not be found.", entry_point_type.to_string().c_str(),
                     entry_point_method.to_string().c_str(), assembly.to_string().c_str());
        core_->logLn(Error, "Error message: %s", error);
        return;
    }

    SampSharpInfo info{VERSION_API, componentVersion()};
    SampSharpInitParams init{core_, components, &info, __setOnCleanup, __setOnFreeComponent};

    on_init(init);
}

void SampSharpComponent::onReady()
{
}

void SampSharpComponent::free()
{
    if (on_cleanup_)
    {
        on_cleanup_();
    }

    delete this;
}

void SampSharpComponent::onFree(IComponent* component)
{
    if (on_free_component_)
    {
        on_free_component_(component);
    }
}

void SampSharpComponent::reset()
{
}

SampSharpComponent* SampSharpComponent::getInstance()
{
    if (instance_ == nullptr)
    {
        instance_ = new SampSharpComponent();
    }
    return instance_;
}
