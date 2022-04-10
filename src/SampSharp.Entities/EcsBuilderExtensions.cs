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

namespace SampSharp.Entities
{
    /// <summary>
    /// Provides extended functionality for configuring a <see cref="IEcsBuilder" /> instance.
    /// </summary>
    public static class EcsBuilderExtensions
    {
        /// <summary>
        /// Enabled a Dependency Injection scope for the event with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="builder">The ECS builder in which to enable the scope.</param>
        /// <param name="name">The name of the event to add the scope to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IEcsBuilder EnableEventScope(this IEcsBuilder builder, string name)
        {
            return builder.UseMiddleware<EventScopeMiddleware>(name);
        }
    }
}