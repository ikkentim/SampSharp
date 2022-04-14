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
using System.Globalization;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents an identifier of an entity.
    /// </summary>
    public readonly struct EntityId
    {
        /// <summary>
        /// An empty entity identifier.
        /// </summary>
        public static readonly EntityId Empty = new();

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
        /// Gets a value indicating whether this handle is invalid.
        /// </summary>
        public bool IsInvalidHandle => EntityTypeRegistry.GetTypeInvalidHandle(Type) == Handle;

        /// <summary>
        /// Gets a value indicating whether this handle is empty.
        /// </summary>
        public bool IsEmpty => Type == Guid.Empty;

        /// <summary>
        /// Determines whether the specified <paramref name="other" /> value, is equal to this value.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(EntityId other)
        {
            if (IsEmpty && other.IsInvalidHandle || other.IsEmpty && IsInvalidHandle)
            {
                return true;
            }

            return Type.Equals(other.Type) && (Handle == other.Handle || Type == Guid.Empty);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this value.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this value.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object" /> is equal to this value; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is EntityId other && Equals(other);
        }

        /// <summary>
        /// Returns a hash code for this value.
        /// </summary>
        /// <returns>
        /// A hash code for this value, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Type.GetHashCode() * 397) ^ Handle;
            }
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            if (IsEmpty)
            {
                return "(Empty)";
            }

            return
                $"(Type = {EntityTypeRegistry.GetTypeName(Type)}, Handle = {(IsInvalidHandle ? "Invalid" : Handle.ToString(CultureInfo.InvariantCulture))})";
        }
        
        /// <summary>
        /// Performs an implicit conversion from <see cref="EntityId" /> to <see cref="int" />. Returns the handle of this value.
        /// </summary>
        /// <param name="value">The entity identifier.</param>
        /// <returns>
        /// The handle of this value.
        /// </returns>
        public static implicit operator int(EntityId value)
        {
            return value.Handle;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Component" /> to <see cref="EntityId" />. Returns the entity of the component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>
        /// The entity of the component.
        /// </returns>
        public static implicit operator EntityId(Component component)
        {
            return component?.Entity ?? default;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="EntityId" /> to <see cref="bool" />.  Returns <c>true</c> if the
        /// specified <paramref name="value" /> is not of the default empty type and does not have an invalid handle.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if the specified <paramref name="value" /> is not of the default empty type and does not have an invalid handle; otherwise <c>false</c>.
        /// </returns>
        public static implicit operator bool(EntityId value)
        {
            return !value.IsEmpty && !value.IsInvalidHandle;
        }

        /// <summary>
        /// Implements the operator true. Returns <c>true</c> if the specified <paramref name="value" /> is not of the default
        /// empty type and does not have an invalid handle.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if the specified <paramref name="value" /> is not of the default empty type and does not have an invalid handle; otherwise <c>false</c>.
        /// </returns>
        public static bool operator true(EntityId value)
        {
            return !value.IsEmpty && !value.IsInvalidHandle;
        }

        /// <summary>
        /// Implements the operator false. Returns <c>true</c> if the specified <paramref name="value" /> is of the default empty
        /// type or has an invalid handle.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if the specified <paramref name="value" /> is of the default empty type or has an invalid handle; otherwise <c>false</c>.
        /// </returns>
        public static bool operator false(EntityId value)
        {
            return value.IsEmpty || value.IsInvalidHandle;
        }
        
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="lhs">The left hand side value.</param>
        /// <param name="rhs">The right hand side value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(EntityId lhs, EntityId rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="lhs">The left hand side value.</param>
        /// <param name="rhs">The right hand side value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(EntityId lhs, EntityId rhs)
        {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// Implements the operator !. Returns <c>true</c> if the specified <paramref name="value" /> is of the default empty type or has an invalid handle.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if the specified <paramref name="value" /> is of the default empty type or has an invalid handle; otherwise <c>false</c>.
        /// </returns>
        public static bool operator !(EntityId value)
        {
            return value.IsEmpty || value.IsInvalidHandle;
        }
    }
}