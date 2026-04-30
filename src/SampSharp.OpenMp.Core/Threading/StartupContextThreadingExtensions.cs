namespace SampSharp.OpenMp.Core;

/// <summary>
/// Provides extension methods for <see cref="IStartupContext" /> related to threading.
/// </summary>
public static class StartupContextThreadingExtensions
{
    /// <summary>
    /// Configures a <see cref="SynchronizationContext" /> which allows synchronization with the main thread using <see cref="TaskHelper.SwitchToMainThread" />.
    /// </summary>
    /// <param name="context">The startup context.</param>
    /// <returns>The startup context.</returns>
    public static IStartupContext UseSynchronizationContext(this IStartupContext context)
    {
        var ext = context.Core.TryGetExtension<SynchronizationContextExtension>();

        if (ext == null)
        {
            ext = new SynchronizationContextExtension(context.Core);

            context.Core.AddExtension(ext);
            context.Cleanup += (_, _) => ext.Dispose();
        }

        return context;
    }
}