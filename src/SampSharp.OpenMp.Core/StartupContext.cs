using SampSharp.OpenMp.Core.Api;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// Represents the context in which the application is started.
/// </summary>
public sealed class StartupContext : IStartupContext
{
    /// <summary>
    /// The API version of the native open.mp component which supports this version of the managed API. This is used to check for version mismatches between the managed and native components.
    /// </summary>
    private const int SupportedApiVersion = 1;

    private IStartup? _configurator;
    private ExceptionHandler _unhandledExceptionHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="StartupContext" /> class.
    /// </summary>
    /// <param name="init">The initialization parameters.</param>
    public StartupContext(SampSharpInitParams init)
    {
        VersionCheck(init);
        Core = init.Core;
        ComponentList = init.ComponentList;
        Info = init.Info;
        _unhandledExceptionHandler = (context, ex) =>
        {
            Core.LogLine(LogLevel.Error, $"Unhandled exception during {context}:");
            Core.LogLine(LogLevel.Error, ex.ToString());
        };
        SampSharpExceptionHandler.SetExceptionHandler(_unhandledExceptionHandler);
    }

    /// <inheritdoc />
    public ICore Core { get; }

    /// <inheritdoc />
    public IComponentList ComponentList { get; }

    /// <inheritdoc />
    public SampSharpInfo Info { get; }

    /// <inheritdoc />
    public IStartup Configurator => _configurator ?? throw new InvalidOperationException("The configurator has not been set.");

    /// <inheritdoc />
    public ExceptionHandler UnhandledExceptionHandler
    {
        get => _unhandledExceptionHandler;
        set
        {
            _unhandledExceptionHandler = value;
            SampSharpExceptionHandler.SetExceptionHandler(value);
        }
    }

    /// <inheritdoc />
    public event EventHandler? Cleanup;

    /// <inheritdoc />
    public event EventHandler? Initialized;

    /// <summary>
    /// Internal method. Do not invoke manually.
    /// </summary>
    public void InitializeUsing(IStartup configurator)
    {
        _configurator = configurator;

        configurator.Initialize(this);
        Initialized?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Internal method. Do not invoke manually.
    /// </summary>
    public void InvokeCleanup()
    {
        Cleanup?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Internal method. Do not invoke manually.
    /// </summary>
    public static void MainInfoProvider()
    {
        LaunchInstructions.Write();
    }

    private static void VersionCheck(SampSharpInitParams init)
    {
        unsafe
        {
            var initParamsSizeMatches = init.Size.Value == sizeof(SampSharpInitParams);
            var infoSizeMatches = init.Info.Size.Value == sizeof(SampSharpInfo);
            var apiVersionMatches = init.Info.ApiVersion == SupportedApiVersion;

            if (initParamsSizeMatches && infoSizeMatches && apiVersionMatches)
            {
                return;
            }

            var version = typeof(StartupContext).Assembly.GetName().Version!.ToString(3);

            // TODO: Write docs
            var message = $"SampSharp version mismatch. The SampSharp open.mp component does not support SampSharp.OpenMp.Core v{version}. See https://sampsharp.net/version-mismatch.html for more details.";

            if (initParamsSizeMatches && infoSizeMatches)
            {
                init.Core.LogLine(LogLevel.Error, message);
            }

            Environment.FailFast(message);
        }
    }
}