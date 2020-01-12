// SampSharp
// Copyright 2020 Tim Potze
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

using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities
{
    /// <summary>
    /// Extension methods for adding systems to an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />
    /// .
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the system of the specified type <typeparamref name="T" /> as a singleton and enables the system in the system
        /// registry.
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