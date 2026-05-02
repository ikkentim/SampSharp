using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

/// <summary>
/// Binds the lifecycle of the ECS host to the lifecycle of the startup context, ensuring that the ECS host is properly initialized and cleaned up along with the application.
/// </summary>
internal class HostContextBinder(IStartupContext context, EcsHostBuilder hostBuilder) : IDisposable
{
    private EcsHost? _host;

    public void Bind()
    {
        context.Initialized += OnContextInitialized;
        context.Cleanup += OnContextCleanup;
    }

    private void OnContextInitialized(object? sender, EventArgs e)
    {
        _host = hostBuilder.Build(context);
        context.Core.AddExtension(_host);
        _host.Start(context);
    }

    private void OnContextCleanup(object? sender, EventArgs e)
    {
        Dispose();
    }

    public void Dispose()
    {
        context.Initialized -= OnContextInitialized;
        context.Cleanup -= OnContextCleanup;

        _host?.Dispose();
    }
}