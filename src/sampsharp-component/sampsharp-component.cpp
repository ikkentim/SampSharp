#include "sampsharp-component.hpp"
#include "crash-handler.hpp"
#include "version.hpp"

#define CFG_DIRECTORY "sampsharp.directory"
#define CFG_ASSEMBLY "sampsharp.assembly"
#define CFG_ENTRY_POINT_TYPE "sampsharp.entry_point_type"
#define CFG_ENTRY_POINT_METHOD "sampsharp.entry_point_method"
#define CFG_CLEANUP_METHOD "sampsharp.cleanup_method"
#define CFG_DISABLE_CRASH_HANDLER "sampsharp.disable_crash_handler"

StringView SampSharpComponent::componentName() const
{
    return "SampSharp";
}

SemanticVersion SampSharpComponent::componentVersion() const
{
    return { VERSION_MAJOR, VERSION_MINOR, VERSION_PATCH, VERSION_BUILD };
}

void SampSharpComponent::onLoad(ICore* c)
{
    core_ = c;

    bool disableCrashHandler = *c->getConfig().getBool(CFG_DISABLE_CRASH_HANDLER);
    if(!disableCrashHandler) {
        sampsharp::crash::install(c);
    }
}

void SampSharpComponent::provideConfiguration(ILogger& logger, IEarlyConfig& config, const bool defaults)
{
    #define initConfigString(key, value) \
        if(defaults || config.getType(key) == ConfigOptionType_None) { \
            config.setString(key, value); \
        }
    
    initConfigString(CFG_DIRECTORY, "gamemode");
    initConfigString(CFG_ASSEMBLY, "GameMode");
    initConfigString(CFG_ENTRY_POINT_TYPE, "SampSharp.Entrypoint");
    initConfigString(CFG_ENTRY_POINT_METHOD, "Initialize");
    initConfigString(CFG_CLEANUP_METHOD, "Cleanup");

    if(defaults || config.getType(CFG_DISABLE_CRASH_HANDLER) == ConfigOptionType_None) {
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
    const auto cleanup_method = config.getString(CFG_CLEANUP_METHOD);

    std::string entry_point = entry_point_type.to_string() + ", " + assembly.to_string();
    const auto full_entry_point = StringView(entry_point);

    const char * error = nullptr;
    
    if(!managed_host_.initialize(&error))
    {
        core_->logLn(Error, "Failed to initialize the .NET host framework resolver. Has the .NET runtime been installed?");
        core_->logLn(Error, "Error message: %s", error);
        return;
    }

    if(!managed_host_.loadFor(directory, assembly, &error))
    {
        core_->logLn(Error, "Failed to initialize the .NET runtime for '%s/%s'. Is the '*.runtimeconfig.json' file available? Is the .NET runtime available?", directory.to_string().c_str(), assembly.to_string().c_str());
        core_->logLn(Error, "Error message: %s", error);
        return;
    }

    on_init_fn on_init;
    if(!managed_host_.getEntryPoint(full_entry_point, entry_point_method, reinterpret_cast<void**>(&on_init), &error))
    {
        core_->logLn(Error, "The entrypoint '%s.%s, %s' could not be found.", entry_point_type.to_string().c_str(), entry_point_method.to_string().c_str(), assembly.to_string().c_str());
        core_->logLn(Error, "Error message: %s", error);
        return;
    }
    
    if(!managed_host_.getEntryPoint(full_entry_point, cleanup_method, reinterpret_cast<void**>(&on_cleanup_), &error))
    {
        core_->logLn(Error, "The entrypoint '%s.%s, %s' could not be found.", entry_point_type.to_string().c_str(), cleanup_method.to_string().c_str(), assembly.to_string().c_str());
        core_->logLn(Error, "Error message: %s", error);
        return;
    }

    SampSharpInfo info { VERSION_API, componentVersion() };
    SampSharpInitParams init { core_, components, &info };
    
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
