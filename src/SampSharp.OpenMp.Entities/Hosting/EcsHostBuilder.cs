using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

[Extension(0xb0eac2ea9239714c)]
internal sealed class EcsHostBuilder : Extension, IEcsHostBuilder
{
    private readonly List<Action<IEcsBuilder>> _ecsConfigurations = [];
    private readonly List<Action<ILoggingBuilder>> _loggerConfigurations = [];
    private readonly List<Action<IServiceCollection, IConfiguration, SampSharpEnvironment>> _serviceConfigurations = [];
    private readonly List<Action<IConfigurationBuilder>> _appConfigConfigurations = [];
    private Func<IServiceCollection, IServiceProvider>? _serviceProviderFactory;
    private bool _systemsLoadingDisabled;
    private UnhandledExceptionHandler? _unhandledExceptionHandler;

    public IEcsHostBuilder Configure(Action<IEcsBuilder> build)
    {
        ArgumentNullException.ThrowIfNull(build);
        _ecsConfigurations.Add(build);
        return this;
    }

    public IEcsHostBuilder ConfigureServices(Action<IServiceCollection, IConfiguration, SampSharpEnvironment> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        _serviceConfigurations.Add(configure);
        return this;
    }

    public IEcsHostBuilder ConfigureServices(Action<IServiceCollection, IConfiguration> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        return ConfigureServices((services, configuration, _) => configure(services, configuration));
    }

    public IEcsHostBuilder ConfigureServices(Action<IServiceCollection> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        return ConfigureServices((services, _, _) => configure(services));
    }

    public IEcsHostBuilder ConfigureLogging(Action<ILoggingBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        _loggerConfigurations.Add(configure);
        return this;
    }

    public IEcsHostBuilder ConfigureAppConfiguration(Action<IConfigurationBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        _appConfigConfigurations.Add(configure);
        return this;
    }

    public IEcsHostBuilder ConfigureUnhandledExceptionHandler(UnhandledExceptionHandler handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        _unhandledExceptionHandler = handler;
        return this;
    }

    public IEcsHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> serviceProviderFactory)
        where TContainerBuilder : notnull
    {
        ArgumentNullException.ThrowIfNull(serviceProviderFactory);
        _serviceProviderFactory = services => serviceProviderFactory.CreateServiceProvider(serviceProviderFactory.CreateBuilder(services));
        return this;
    }

    public IEcsHostBuilder DisableDefaultSystemsLoading()
    {
        _systemsLoadingDisabled = true;
        return this;
    }

    internal EcsHost Build(IStartupContext context)
    {
        var serviceProvider = BuildServiceProvider(context);
        Configure(new EcsBuilder(serviceProvider));

        return new EcsHost(serviceProvider, _unhandledExceptionHandler);
    }

    private IServiceProvider BuildServiceProvider(IStartupContext context)
    {
        var environment = EnvironmentFactory.Create(context);
        var configuration = ConfigurationFactory.Create(environment, ConfigureAppConfiguration);

        var services = new ServiceCollection();

        ConfigureDefaultServices(services, configuration);

        services.AddSingleton(environment);
        services.AddSingleton(configuration);

        ConfigureServices(services, configuration, environment);

        if (!_systemsLoadingDisabled)
        {
            services.AddSystemsInAssembly(environment.EntryAssembly);
            _systemsLoadingDisabled = true;
        }

        var factory = _serviceProviderFactory ?? DefaultServiceProviderFactory;
        return factory(services);
    }

    private void ConfigureDefaultServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddLogging(builder =>
            {
                builder.AddOpenMp();
                builder.AddConfiguration(configuration.GetSection("Logging"));
                ConfigureLogger(builder);
            })
            .AddSingleton<IUnhandledExceptionHandler, UnhandledExceptionHandlerImpl>()
            .AddSingleton<EventDispatcher>()
            .AddSingleton<IEventDispatcher>(sp => sp.GetRequiredService<EventDispatcher>())
#pragma warning disable CS0618 // Type or member is obsolete
            .AddSingleton<IEventService>(sp => sp.GetRequiredService<EventDispatcher>())
#pragma warning restore CS0618 // Type or member is obsolete
            .AddSingleton<SystemRegistry>()
            .AddSingleton<ISystemRegistry>(x => x.GetRequiredService<SystemRegistry>())
            .AddSingleton<IEntityManager, EntityManager>()
            .AddSingleton<ITimerService>(s => s.GetRequiredService<TimerSystem>())
            .AddSystem<TimerSystem>()
            .AddSystem<TickingSystem>()
            .AddSamp();
    }

    private static IServiceProvider DefaultServiceProviderFactory(IServiceCollection services)
    {
        return services.BuildServiceProvider();
    }


    private void Configure(IEcsBuilder builder)
    {
        foreach (var configuration in _ecsConfigurations)
        {
            configuration(builder);
        }

        _ecsConfigurations.Clear();
    }

    private void ConfigureLogger(ILoggingBuilder builder)
    {
        foreach (var configuration in _loggerConfigurations)
        {
            configuration(builder);
        }

        _loggerConfigurations.Clear();
    }

    private void ConfigureAppConfiguration(IConfigurationBuilder builder)
    {
        foreach (var configuration in _appConfigConfigurations)
        {
            configuration(builder);
        }

        _appConfigConfigurations.Clear();
    }

    private void ConfigureServices(IServiceCollection services, IConfiguration config, SampSharpEnvironment environment)
    {
        foreach (var configuration in _serviceConfigurations)
        {
            configuration(services, config, environment);
        }

        _serviceConfigurations.Clear();
    }

}