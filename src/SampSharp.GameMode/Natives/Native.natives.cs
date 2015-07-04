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

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern int LoadNative(string name, string format, int[] sizes);

        [MethodImpl((MethodImplOptions.InternalCall))]
        internal static extern int InvokeNative(int handle, object[] args);

        [MethodImpl((MethodImplOptions.InternalCall))]
        internal static extern float InvokeNativeFloat(int handle, object[] args);
        
        /// <summary>
        ///     Utility method. Checks whether current thread is main thread.
        /// </summary>
        /// <returns>
        ///     True if current thread is main thread; False otherwise.
        /// </returns>
        /// <remarks>
        ///     This method can be used for debugging purposes. In general,
        ///     comparing <see cref="Thread.CurrentThread" /> works just as well.
        /// </remarks>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsMainThread();

        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool NativeExists(string name);

        /// <summary>
        ///     Registers an extension to the plugin.
        /// </summary>
        /// <param name="extension">The extension to register.</param>
        /// <returns>
        ///     True on success, False otherwise.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RegisterExtension(object extension);
    }
}