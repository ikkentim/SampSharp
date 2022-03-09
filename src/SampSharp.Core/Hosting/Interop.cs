// SampSharp
// Copyright 2018 Tim Potze
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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace SampSharp.Core.Hosting
{
    /// <summary>
    /// Contains interop functions for the SampSharp plugin.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Interop
    {
        /// <summary>
        /// Prints the specified message to the SA:MP server log.
        /// </summary>
        /// <param name="message">The message.</param>
        [DllImport("SampSharp", EntryPoint = "sampsharp_print", CallingConvention = CallingConvention.StdCall)]
        public static extern void Print(string message);

        /// <summary>
        /// Registers a callback in the SampSharp plugin.
        /// </summary>
        /// <param name="data">The callback data.</param>
        [DllImport("SampSharp", EntryPoint = "sampsharp_register_callback", CallingConvention = CallingConvention.StdCall)]
        public static extern void RegisterCallback(IntPtr data);

        /// <summary>
        /// Gets the handle of a native.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <returns>The handle of the native</returns>
        [DllImport("SampSharp", EntryPoint = "sampsharp_get_native_handle", CallingConvention = CallingConvention.StdCall)]
        [Obsolete("Native handle based native invocation is deprecated and will be removed in a future version.")]
        public static extern int GetNativeHandle(string name);

        /// <summary>
        /// Invokes a native by a handle.
        /// </summary>
        /// <param name="inbuf">The input buffer.</param>
        /// <param name="inlen">The input buffer length.</param>
        /// <param name="outbuf">The output buffer.</param>
        /// <param name="outlen">The output buffer length.</param>
        [DllImport("SampSharp", EntryPoint = "sampsharp_invoke_native", CallingConvention = CallingConvention.StdCall)]
        [Obsolete("Native handle based native invocation is deprecated and will be removed in a future version.")]
        public static extern void InvokeNative(IntPtr inbuf, int inlen, IntPtr outbuf, ref int outlen);

        /// <summary>
        /// Gets a pointer to a native.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <returns>A pointer to a native.</returns>
        [DllImport("SampSharp", EntryPoint = "sampsharp_fast_native_find", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FastNativeFind(string name);

        /// <summary>
        /// Invokes a native by a pointer.
        /// </summary>
        /// <param name="native">The pointer to the native.</param>
        /// <param name="format">The format of the arguments.</param>
        /// <param name="args">A pointer to the arguments array.</param>
        /// <returns>The return value of the native.</returns>
        [DllImport("SampSharp", EntryPoint = "sampsharp_fast_native_invoke", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe int FastNativeInvoke(IntPtr native, string format, int* args);

        internal static int PublicCall(string name, IntPtr argumentsPtr, int length)
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            return client?.PublicCall(name, argumentsPtr, length) ?? 1;
        }

        internal static void Tick()
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            client?.Tick();
        }
    }
}