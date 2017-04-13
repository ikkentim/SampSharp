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
    ///     Contains possible types for a parameter of a native function.
    /// </summary>
    [Flags]
    public enum NativeParameterType : byte
    {
        /// <summary>
        ///     An <see cref="int" />.
        /// </summary>
        Int32 = 1 << 0,

        /// <summary>
        ///     A <see cref="float" />.
        /// </summary>
        Single = 1 << 1,

        /// <summary>
        ///     A <see cref="bool" />.
        /// </summary>
        Bool = 1 << 2,

        /// <summary>
        ///     A <see cref="string" />.
        /// </summary>
        String = 1 << 3,

        /// <summary>
        ///     An array.
        /// </summary>
        Array = 1 << 4,

        /// <summary>
        ///     A reference to a value.
        /// </summary>
        Reference = 1 << 5,

        /// <summary>
        ///     A reference to an <see cref="int" />.
        /// </summary>
        Int32Reference = Int32 | Reference,

        /// <summary>
        ///     A reference to a <see cref="float" />.
        /// </summary>
        SingleReference = Single | Reference,

        /// <summary>
        ///     A reference to a <see cref="bool" />.
        /// </summary>
        BoolReference = Bool | Reference,

        /// <summary>
        ///     A reference to a <see cref="string" />.
        /// </summary>
        StringReference = String | Reference,

        /// <summary>
        ///     An array of <see cref="int" />.
        /// </summary>
        Int32Array = Int32 | Array,

        /// <summary>
        ///     An array of <see cref="float" />.
        /// </summary>
        SingleArray = Single | Array,

        /// <summary>
        ///     An array of <see cref="bool" />.
        /// </summary>
        BoolArray = Bool | Array,

        /// <summary>
        ///     A reference to an array of <see cref="int" />.
        /// </summary>
        Int32ArrayReference = Int32 | Array | Reference,

        /// <summary>
        ///     A reference to an array of <see cref="float" />.
        /// </summary>
        SingleArrayReference = Single | Array | Reference,

        /// <summary>
        ///     A reference to an array of <see cref="bool" />.
        /// </summary>
        BoolArrayReference = Bool | Array | Reference
    }
}