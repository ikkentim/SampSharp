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

namespace SampSharp.Core
{
    /// <summary>
    ///     Contains the possible callback parameter types.
    /// </summary>
    public enum CallbackParameterType : byte
    {
        /// <summary>
        ///     A value (either of <see cref="int" />, <see cref="float" /> or <see cref="bool" />).
        /// </summary>
        Value = 0x01,

        /// <summary>
        ///     An array (of element type <see cref="int" />, <see cref="float" /> or <see cref="bool" />).
        /// </summary>
        Array = 0x02,

        /// <summary>
        ///     A string.
        /// </summary>
        String = 0x04
    }
}