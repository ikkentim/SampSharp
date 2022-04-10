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
using System.Runtime.Serialization;

namespace SampSharp.Entities
{
    /// <summary>
    /// The exception that is thrown when one of the arguments is of an invalid entity identifier type.
    /// </summary>
    [Serializable]
    public class InvalidEntityArgumentException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntityArgumentException" /> class.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="expectedType">The expected entity identifier type.</param>
        public InvalidEntityArgumentException(string paramName, Guid expectedType) : base(
            $"Invalid entity identifier type, expected type to be {EntityTypeRegistry.GetTypeName(expectedType)}.",
            paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntityArgumentException" /> class.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="expectedTypes">The expected entity identifier types.</param>
        public InvalidEntityArgumentException(string paramName, params Guid[] expectedTypes) : base(
            $"Invalid entity identifier type, expected type to be any of {expectedTypes.Select(EntityTypeRegistry.GetTypeName)}.",
            paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntityArgumentException" /> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual
        /// information about the source or destination.
        /// </param>
        protected InvalidEntityArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}