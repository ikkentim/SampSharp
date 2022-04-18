// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities;

/// <summary>
/// Extension methods for adding systems to an <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the system of the specified <paramref name="type" /> as a singleton and enables the system in the system
    /// registry.
    /// </summary>
    /// <param name="services">The service collection to add the system to.</param>
    /// <param name="type">The type of the system to add.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystem(this IServiceCollection services, Type type)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (type == null) throw new ArgumentNullException(nameof(type));

        return services
            .AddSingleton(type)
            .AddSingleton(new SystemTypeWrapper(type));
    }

    /// <summary>
    /// Adds the system of the specified type <typeparamref name="T" /> as a singleton and enables the system in the system
    /// registry.
    /// </summary>
    /// <typeparam name="T">The type of the system to add.</typeparam>
    /// <param name="services">The service collection to add the system to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystem<T>(this IServiceCollection services) where T : class, ISystem
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        return AddSystem(services, typeof(T));
    }

    /// <summary>
    /// Adds the all types which implement <see cref="ISystem" /> in the specified <paramref name="assembly" /> as singletons
    /// and enable the systems in the system registry.
    /// </summary>
    /// <param name="services">The service collection to add the systems to.</param>
    /// <param name="assembly">The assembly of which to add its types as systems.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystemsInAssembly(this IServiceCollection services, Assembly assembly)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (assembly == null) throw new ArgumentNullException(nameof(assembly));

        var types = new AssemblyScanner()
            .IncludeAssembly(assembly)
            .Implements<ISystem>()
            .ScanTypes();

        foreach (var type in types)
            AddSystem(services, type);

        return services;
    }

    /// <summary>
    /// Adds the all types which implement <see cref="ISystem" /> in the assembly of the specified type
    /// <typeparamref name="TTypeInAssembly" /> as singletons and enable the systems in the system registry.
    /// </summary>
    /// <typeparam name="TTypeInAssembly">A type in the assembly of which to add its types as system.</typeparam>
    /// <param name="services">The service collection to add the systems to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystemsInAssembly<TTypeInAssembly>(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        return AddSystemsInAssembly(services, typeof(TTypeInAssembly).Assembly);
    }

    /// <summary>
    /// Adds the all types which implement <see cref="ISystem" /> in the calling assembly as singletons and enable the systems
    /// in the system registry.
    /// </summary>
    /// <param name="services">The service collection to add the systems to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddSystemsInAssembly(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        return AddSystemsInAssembly(services, Assembly.GetCallingAssembly());
    }
}