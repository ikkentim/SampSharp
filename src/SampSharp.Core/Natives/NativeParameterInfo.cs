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

namespace SampSharp.Core.Natives
{
    /// <summary>
    ///     Contains information about a native's parameter.
    /// </summary>
    [Obsolete("Native handle based native invocation is deprecated and will be removed in a future version.")]
    public readonly struct NativeParameterInfo
    {
        /// <summary>
        ///     A mask for all supported argument value types.
        /// </summary>
        private const NativeParameterType ArgumentMask = NativeParameterType.Int32 |
                                                         NativeParameterType.Single |
                                                         NativeParameterType.Bool |
                                                         NativeParameterType.String;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeParameterInfo" /> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="lengthIndex">Index of the length.</param>
        /// <param name="isOutput">A value indicating whether this parameter has no input.</param>
        public NativeParameterInfo(NativeParameterType type, uint lengthIndex, bool isOutput)
        {
            Type = type;
            LengthIndex = lengthIndex;
            IsOutput = isOutput;

        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeParameterInfo" /> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="isOutput">A value indicating whether this parameter has no input.</param>
        public NativeParameterInfo(NativeParameterType type, bool isOutput)
        {
            Type = type;
            LengthIndex = 0;
            IsOutput = isOutput;
        }

        /// <summary>
        ///     Gets the type.
        /// </summary>
        public NativeParameterType Type { get; }
        
        /// <summary>
        ///     Returns a <see cref="NativeParameterInfo" /> for the specified <paramref name="type" />.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="isOutput">A value indicating whether the parameter has no input.</param>
        /// <returns>A struct for the type.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="type" /> is not a valid native parameter
        ///     type.
        /// </exception>
        public static NativeParameterInfo ForType(Type type, bool isOutput = false)
        {
            var isByRef = type.IsByRef;
            var elementType = isByRef ? type.GetElementType()! : type;
            var isArray = elementType.IsArray;
            elementType = isArray ? elementType.GetElementType() : elementType;

            NativeParameterType parameterType;
            if (elementType == typeof(int)) parameterType = NativeParameterType.Int32;
            else if (elementType == typeof(float)) parameterType = NativeParameterType.Single;
            else if (elementType == typeof(bool)) parameterType = NativeParameterType.Bool;
            else if (elementType == typeof(string)) parameterType = NativeParameterType.String;
            else throw new ArgumentOutOfRangeException(nameof(type));

            if (isArray) parameterType |= NativeParameterType.Array;
            if (isByRef) parameterType |= NativeParameterType.Reference;

            return new NativeParameterInfo(parameterType, isByRef && isOutput);
        }

        /// <summary>
        ///     Gets a value indicating whether the parameter info requires length information.
        /// </summary>
        public bool RequiresLength => Type.HasFlag(NativeParameterType.Array) || Type == NativeParameterType.StringReference;

        /// <summary>
        ///     Gets the index of the length parameter specifying the length of this parameter.
        /// </summary>
        public uint LengthIndex { get; }

        /// <summary>
        /// Gets a value indicating whether this parameter has no input.
        /// </summary>
        public bool IsOutput { get; }
    }
}