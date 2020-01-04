using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities
{
    /// <summary>
    /// Extension methods for adding systems to an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the system of the specified type <typeparamref name="T"/> as a singleton and enables the system in the system registry.
        /// </summary>
        /// <typeparam name="T">The type of the system to add.</typeparam>
        /// <param name="services">The service collection to add the system to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddSystem<T>(this IServiceCollection services) where T : class, ISystem
        {
            return services
                .AddSingleton<T>()
                .AddSingleton(new SystemTypeWrapper(typeof(T)));
        }
    }
}