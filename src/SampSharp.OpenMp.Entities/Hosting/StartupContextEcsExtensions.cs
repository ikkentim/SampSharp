using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

/// <summary>
/// Contains extension methods for <see cref="IStartupContext" /> to configure the ECS system.
/// </summary>
public static class StartupContextEcsExtensions
{
    /// <summary>
    /// Configures the ECS system for the SampSharp application.
    /// </summary>
    /// <param name="context">The startup context.</param>
    /// <returns>A <see cref="IEcsHostBuilder" /> instance which can be used to further configure the ECS system.</returns>
    public static IEcsHostBuilder UseEntities(this IStartupContext context)
    {
        var builder = context.Core.TryGetExtension<EcsHostBuilder>();
        if (builder != null)
        {
            return builder;
        }

        builder = new EcsHostBuilder();
        context.Core.AddExtension(builder);

        if (context.Configurator is IEcsStartup startup)
        {
            builder.Configure(startup.Configure);
            builder.ConfigureServices(startup.ConfigureServices);
        }

        new HostContextBinder(context, builder).Bind();
        
        return builder;
    }
}