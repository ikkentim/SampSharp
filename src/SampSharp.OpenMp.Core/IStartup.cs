namespace SampSharp.OpenMp.Core;

/// <summary>
/// Provides methods for initializing a SampSharp application.
/// </summary>
public interface IStartup
{
    /// <summary>
    /// Initializes the application with the specified <paramref name="context" />.
    /// </summary>
    /// <param name="context">The context to use for initialization.</param>
    void Initialize(IStartupContext context);
}