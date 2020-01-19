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

using System;
using System.Linq;

namespace SampSharp.Entities
{
    /// <summary>
    /// Provides extended functionality for the <see cref="EntityId" /> struct.
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the entity identifier of the specified <paramref name="entity" /> is of the
        /// specified <paramref name="type" />.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <param name="type">The entity identifier type to check for.</param>
        /// <returns>
        /// A value indicating whether the entity identifier of the specified <paramref name="entity" /> is of the
        /// specified <paramref name="type" />.
        /// </returns>
        public static bool IsOfType(this EntityId entity, Guid type)
        {
            return entity.Type == type;
        }

        /// <summary>
        /// Returns a value indicating whether the entity identifier of the specified <paramref name="entity" /> is of any of the
        /// specified <paramref name="types" />.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <param name="types">The entity identifier types to check for.</param>
        /// <returns>
        /// A value indicating whether the entity identifier of the specified <paramref name="entity" /> is of any of the
        /// specified <paramref name="types" />.
        /// </returns>
        public static bool IsOfAnyType(this EntityId entity, params Guid[] types)
        {
            return types != null && Array.IndexOf(types, entity.Type) >= 0;
        }

        internal static int OrElse(this EntityId entity, int @else)
        {
            return !entity ? @else : entity.Handle;
        }
    }
}