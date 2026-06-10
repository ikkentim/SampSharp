using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

[Extension(0x57e43771d28c5e7e)]
internal sealed partial class EcsHost(IServiceProvider serviceProvider, UnhandledExceptionHandler? exceptionHandler) : Extension
{
    private IStartupContext? _context;
    private IServiceProvider? _serviceProvider = serviceProvider;

    public IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException();

    public void Start(IStartupContext context)
    {
        _context = context;

        context.UseSynchronizationContext();

        context.UnhandledExceptionHandler = UnhandledExceptionHandler;

        LoadSystems();

        // Fire initial event
        OnGameModeInit();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            OnGameModeExit();

            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }

            _context?.ResetExceptionHandler();
            _context = null;
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
        LogUnhandledException(ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(context), context, exception);
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

    [LoggerMessage(LogLevel.Error, "Unhandled exception during: {Context}")]
    private static partial void LogUnhandledException(ILogger logger, string context, Exception exception);
}