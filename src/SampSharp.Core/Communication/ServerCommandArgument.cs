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

namespace SampSharp.Core.Communication
{
    /// <summary>
    ///     Contains the possible SampShap server command argument types.
    /// </summary>
    [Flags]
    public enum ServerCommandArgument : byte
    {
        /// <summary>
        ///     A terminator value to indicate no more arguments are past this point.
        /// </summary>
        Terminator = 0 << 0,

        /// <summary>
        ///     A value to indicate the next argument is an integer or float.
        /// </summary>
        Value = 1 << 0,

        /// <summary>
        ///     A value to indicate the next argument is an array of integers or floats.
        /// </summary>
        Array = 1 << 1,

        /// <summary>
        ///     A value to indicate the next argument is a string.
        /// </summary>
        String = 1 << 2,

        /// <summary>
        ///     A value to indicate the next argument is a reference.
        /// </summary>
        Reference = 1 << 3,

        /// <summary>
        ///     A value to indicate the next argument is a value reference.
        /// </summary>
        ValueReference = Value | Reference,

        /// <summary>
        ///     A value to indicate the next argument is an array reference.
        /// </summary>
        ArrayReference = Array | Reference,

        /// <summary>
        ///     A value to indicate the next argument is a string reference.
        /// </summary>
        StringReference = String | Reference
    }
}