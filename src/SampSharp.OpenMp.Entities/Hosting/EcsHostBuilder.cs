using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampSharp.Entities.Logging;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

[Extension(0xb0eac2ea9239714c)]
internal sealed class EcsHostBuilder : Extension, IEcsHostBuilder
{
    private readonly List<Action<IEcsBuilder>> _ecsConfigurations = [];
    private readonly List<Action<ILoggingBuilder>> _loggerConfigurations = [];
    private readonly List<Action<SampSharpEnvironment, IServiceCollection>> _serviceConfigurations = [];
    private Func<IServiceCollection, IServiceProvider>? _serviceProviderFactory;
    private bool _systemsLoadingDisabled;
    private UnhandledExceptionHandler? _unhandledExceptionHandler;

    public IEcsHostBuilder Configure(Action<IEcsBuilder> build)
    {
        ArgumentNullException.ThrowIfNull(build);
        _ecsConfigurations.Add(build);
        return this;
    }

    public IEcsHostBuilder ConfigureServices(Action<SampSharpEnvironment, IServiceCollection> build)
    {
        ArgumentNullException.ThrowIfNull(build);
        _serviceConfigurations.Add(build);
        return this;
    }

    public IEcsHostBuilder ConfigureServices(Action<IServiceCollection> build)
    {
        ArgumentNullException.ThrowIfNull(build);
        return ConfigureServices((_, services) => build(services));
    }

    public IEcsHostBuilder ConfigureLogging(Action<ILoggingBuilder> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        _loggerConfigurations.Add(builder);
        return this;
    }

    public IEcsHostBuilder ConfigureUnhandledExceptionhandler(UnhandledExceptionHandler handler)
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
        var environment = new SampSharpEnvironment(context.Configurator.GetType().Assembly, context.Core, context.ComponentList);

        var services = new ServiceCollection();

        ConfigureDefaultServices(services);

        services.AddSingleton(environment);
        ConfigureServices(environment, services);

        var factory = _serviceProviderFactory ?? DefaultServiceProviderFactory;
        return factory(services);
    }

    private void ConfigureDefaultServices(IServiceCollection services)
    {
        services
            .AddLogging(builder =>
            {
                builder.AddOpenMp();
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
            .AddSamp()
            ;
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

    private void ConfigureServices(SampSharpEnvironment environment, IServiceCollection services)
    {
        foreach (var configuration in _serviceConfigurations)
        {
            configuration(environment, services);
        }

        if (!_systemsLoadingDisabled)
        {
            services.AddSystemsInAssembly(environment.EntryAssembly);
            _systemsLoadingDisabled = true;
        }

        _serviceConfigurations.Clear();
    }
}