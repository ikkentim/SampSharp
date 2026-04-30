using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities;

/// <summary>Extension methods for adding systems to an <see cref="IServiceCollection" />.</summary>
public static class ServiceCollectionSystemExtensions
{
    /// <summary>Adds the system of the specified <paramref name="type" /> as a singleton and enables the system in the system registry.</summary>
    /// <param name="services">The service collection to add the system to.</param>
    /// <param name="type">The type of the system to add.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystem(this IServiceCollection services, Type type)
    {
        return services.AddSingleton(type)
            .AddSingleton(new SystemEntry(type));
    }

    /// <summary>Adds the system of the specified type <typeparamref name="T" /> as a singleton and enables the system in the system registry.</summary>
    /// <typeparam name="T">The type of the system to add.</typeparam>
    /// <param name="services">The service collection to add the system to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystem<T>(this IServiceCollection services) where T : class, ISystem
    {
        return services.AddSystem(typeof(T));
    }

    /// <summary>
    /// Adds the all types which implement <see cref="ISystem" /> in the specified <paramref name="assembly" /> as singletons and enable the systems in the
    /// system registry.
    /// </summary>
    /// <param name="services">The service collection to add the systems to.</param>
    /// <param name="assembly">The assembly of which to add its types as systems.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystemsInAssembly(this IServiceCollection services, Assembly assembly)
    {
        var types = ClassScanner.Create()
            .IncludeAssembly(assembly)
            .Implements<ISystem>()
            .ScanTypes();

        foreach (var type in types)
            services.AddSystem(type);

        return services;
    }

    /// <summary>
    /// Adds the all types which implement <see cref="ISystem" /> in the assembly of the specified type <typeparamref name="TTypeInAssembly" /> as singletons
    /// and enable the systems in the system registry.
    /// </summary>
    /// <typeparam name="TTypeInAssembly">A type in the assembly of which to add its types as system.</typeparam>
    /// <param name="services">The service collection to add the systems to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystemsInAssembly<TTypeInAssembly>(this IServiceCollection services)
    {
        return services.AddSystemsInAssembly(typeof(TTypeInAssembly).Assembly);
    }

    /// <summary>Adds the all types which implement <see cref="ISystem" /> in the calling assembly as singletons and enable the systems in the system registry.</summary>
    /// <param name="services">The service collection to add the systems to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystemsInAssembly(this IServiceCollection services)
    {
        return services.AddSystemsInAssembly(Assembly.GetCallingAssembly());
    }
}