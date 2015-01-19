// SampSharp
// Copyright 2015 Tim Potze
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

using System.Runtime.CompilerServices;

namespace SampSharp.GameMode.Natives
{
    public static partial class Native
    {
        /// <summary>
        ///     Prints a message to the serverlog.
        /// </summary>
        /// <param name="msg">The message to print to the serverlog.</param>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Print(string msg);

        /// <summary>
        ///     Sets the currently active codepage.
        /// </summary>
        /// <param name="codepage">Codepage to use.</param>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SetCodepage(int codepage);
    }
}