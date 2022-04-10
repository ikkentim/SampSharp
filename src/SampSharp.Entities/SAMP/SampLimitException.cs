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
using System.Runtime.Serialization;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Represents an error which occurs when an entity or object could not be created because a SA-MP limit was reached.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class SampLimitException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampLimitException" /> class.
        /// </summary>
        public SampLimitException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampLimitException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SampLimitException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampLimitException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The inner exception.</param>
        public SampLimitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampLimitException" /> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual
        /// information about the source or destination.
        /// </param>
        protected SampLimitException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}