// SampSharp
// Copyright 2017 Tim Potze
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
using System.Collections.Generic;
using SampSharp.Core.Communication;

namespace SampSharp.Core.Callbacks
{
    /// <summary>
    ///     Contains information about a callback parameter.
    /// </summary>
    public struct CallbackParameterInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CallbackParameterInfo" /> struct.
        /// </summary>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="lengthIndex">Index of the length of the parameter.</param>
        /// <remarks>Only applies when <paramref name="type" /> is equal to <see cref="CallbackParameterType.Array" />.</remarks>
        public CallbackParameterInfo(CallbackParameterType type, uint lengthIndex)
        {
            Type = type;
            LengthIndex = lengthIndex;
        }

        /// <summary>
        ///     Gets the type of the parameter.
        /// </summary>
        public CallbackParameterType Type { get; }

        /// <summary>
        ///     Gets the index of the length.
        /// </summary>
        /// <remarks>Only applies when <see cref="Type" /> is equal to <see cref="CallbackParameterType.Array" />.</remarks>
        public uint LengthIndex { get; }

        /// <summary>
        ///     The parameter contains a value (either of <see cref="int" />, <see cref="float" /> or <see cref="bool" />).
        /// </summary>
        public static CallbackParameterInfo Value => new CallbackParameterInfo(CallbackParameterType.Value, 0);

        /// <summary>
        ///     The parameter contains a <see cref="string" /> value.
        /// </summary>
        public static CallbackParameterInfo String => new CallbackParameterInfo(CallbackParameterType.String, 0);

        /// <summary>
        ///     The parameter contains an array (of element type <see cref="int" />, <see cref="float" /> or <see cref="bool" />)
        ///     of
        ///     which the length can be found in the parameter at <see cref="LengthIndex" />.
        /// </summary>
        /// <param name="lengthIndex">The index of the parameter contains the length of the array.</param>
        /// <returns>The callback parameter info for the specified array length index.</returns>
        public static CallbackParameterInfo Array(uint lengthIndex)
        {
            return new CallbackParameterInfo(CallbackParameterType.Array, lengthIndex);
        }

        /// <summary>
        ///     Gets the byte representation of the parameter info.
        /// </summary>
        /// <returns>The bytes.</returns>
        public IEnumerable<byte> GetBytes()
        {
            switch (Type)
            {
                case CallbackParameterType.Value:
                case CallbackParameterType.String:
                    return new[] { (byte) Type };
                case CallbackParameterType.Array:
                    var result = new byte[5];
                    result[0] = (byte) Type;
                    ValueConverter.GetBytes(LengthIndex).CopyTo(result, 1);
                    return result;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type));
            }
        }
    }
}