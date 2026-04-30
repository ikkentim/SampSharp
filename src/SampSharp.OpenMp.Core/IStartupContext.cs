using SampSharp.OpenMp.Core.Api;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// Provides the properties and events which provide context to the startup of the application.
/// </summary>
public interface IStartupContext
{
    /// <summary>
    /// Gets the open.mp component list.
    /// </summary>
    IComponentList ComponentList { get; }

    /// <summary>
    /// Gets the configured startup configurator.
    /// </summary>
    IStartup Configurator { get; }

    /// <summary>
    /// Gets the open.mp core.
    /// </summary>
    ICore Core { get; }

    /// <summary>
    /// Gets information about the SampSharp open.mp component.
    /// </summary>
    SampSharpInfo Info { get; }

    /// <summary>
    /// Gets or sets the handler for unhandled exceptions which may occur during the application's lifetime.
    /// </summary>
    ExceptionHandler UnhandledExceptionHandler { get; set; }

    /// <summary>
    /// Occurs when the application is being cleaned up.
    /// </summary>
    event EventHandler? Cleanup;

    /// <summary>
    /// Occurs when the application has been initialized.
    /// </summary>
    event EventHandler? Initialized;
}