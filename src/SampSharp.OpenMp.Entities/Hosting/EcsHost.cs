using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

[Extension(0x57e43771d28c5e7e)]
internal class EcsHost(IServiceProvider serviceProvider, UnhandledExceptionHandler? exceptionHandler) : Extension
{
    private IServiceProvider? _serviceProvider = serviceProvider;

    public IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException();

    public void Start(IStartupContext context)
    {
        context.UseSynchronizationContext();

        context.UnhandledExceptionHandler = UnhandledExceptionHandler;

        LoadSystems();

        // Fire initial event
        OnGameModeInit();
    }

    protected override void Cleanup()
    {
        OnGameModeExit();

        if (_serviceProvider is IDisposable disposable)
        {
            //  TODO: This cleanup is called so late - we can't unsubscribe event handlers anymore, but the disposables in registered systems will try to unsubscribe them. This may cause a System.ExecutionEngineException on shutdown.
            disposable.Dispose();
            _serviceProvider = null;
        }
    }

    private void UnhandledExceptionHandler(string context, Exception exception)
    {
        if (exceptionHandler != null)
        {
            exceptionHandler(ServiceProvider, context, exception);
        }
        else
        {
            DefaultExceptionHandler(context, exception);
        }
    }

    private void DefaultExceptionHandler(string context, Exception exception)
    {
        ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(context)
            .LogError(exception, "Unhandled exception during: {context}", context);
    }

    private void OnGameModeInit()
    {
        ServiceProvider.GetRequiredService<IEventDispatcher>().Invoke("OnGameModeInit");
    }

    private void OnGameModeExit()
    {
        ServiceProvider.GetRequiredService<IEventDispatcher>().Invoke("OnGameModeExit");
    }

    private void LoadSystems()
    {
        ServiceProvider.GetRequiredService<SystemRegistry>().LoadSystems();
    }
}