// SampSharp
// Copyright 2019 Tim Potze
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

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents an identifier of an <see cref="Entity" />.
    /// </summary>
    public struct EntityId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityId" /> struct.
        /// </summary>
        /// <param name="type">An identifier which uniquely identifies the type of the entity identifier.</param>
        /// <param name="handle">The handle of the entity identifier.</param>
        public EntityId(Guid type, int handle)
        {
            Type = type;
            Handle = handle;
        }

        /// <summary>
        /// Gets the identifier which uniquely identifies the type of the entity identifier.
        /// </summary>
        public Guid Type { get; }

        /// <summary>
        /// Gets the handle of the entity identifier.
        /// </summary>
        public int Handle { get; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="EntityId" /> to <see cref="int" />.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator int(EntityId entityId)
        {
            return entityId.Handle;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"(Type = {Type}, Handle = {Handle})";
        }
    }
}