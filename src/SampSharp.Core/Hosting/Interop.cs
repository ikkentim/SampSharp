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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SampSharp.Core.Natives.NativeObjects.FastNatives;

namespace SampSharp.Core.Hosting
{
    /// <summary>
    /// Contains interop functions for the SampSharp plugin.
    /// </summary>
    public static unsafe class Interop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0032:Use auto property", Justification = "Performance optimization")]
        private static SampSharpApi* _api;
        
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
        
        /// <summary>
        /// Invokes a native by a pointer.
        /// </summary>
        /// <param name="native">The pointer to the native.</param>
        /// <param name="format">The format of the arguments.</param>
        /// <param name="args">A pointer to the arguments array.</param>
        /// <returns>The return value of the native.</returns>
        private static int FastNativeInvokeViaApi(IntPtr native, string format, int* args)
        {
            // TODO: Make this replace FastNativeInvoke
            var bytes = Encoding.ASCII.GetByteCount(format);
            var formatSt = stackalloc byte[bytes];
            Encoding.ASCII.GetBytes(format, new Span<byte>(formatSt, bytes));

            return _api->InvokeNative((void*)native, formatSt, args);
        }
        
        /// <summary>
        /// Gets a pointer to a native.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <returns>A pointer to a native.</returns>
        private static IntPtr FastNativeFindViaApi(string name)
        {
            // TODO: Make this replace FastNativeFind
            var bytes = Encoding.ASCII.GetByteCount(name);
            var nameStack = stackalloc byte[bytes];
            Encoding.ASCII.GetBytes(name, new Span<byte>(nameStack, bytes));

            return (IntPtr)_api->FindNative(nameStack);
        }
        
        /// <summary>
        /// Prints the specified message to the SA:MP server log.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Print(string message)
        {
            var format = stackalloc byte[3];
            format[0] = 0x25; // "%s\0"
            format[1] = 0x73;
            format[2] = 0x00;

            message ??= string.Empty;
            
            var bytes = NativeUtils.GetByteCount(message);
            var buffer = bytes < 200 ? stackalloc byte[bytes] : new byte[bytes];

            NativeUtils.GetBytes(message, buffer);
            
            fixed (byte* ptr = &buffer.GetPinnableReference())
            {
                _api->PluginData->Logprintf((char*)format, (char*)ptr);
            }
        }

        [DllImport("SampSharp", EntryPoint = "sampsharp_api_initialize", CallingConvention = CallingConvention.StdCall)]
        private static extern SampSharpApi* InitializeApi(void *publicCall, void *tick);

        /// <summary>
        /// Gets the contents of the SampSharp plugin API.
        /// </summary>
        public static SampSharpApi* Api => _api;
        
        internal static void Initialize()
        {
            var ptr = (delegate* unmanaged[Stdcall] <IntPtr, sbyte*, IntPtr, IntPtr, void>)&PublicCall;
            var ptrTick = (delegate* unmanaged[Stdcall]<void>)&Tick;

            _api = InitializeApi(ptr, ptrTick);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static void PublicCall(IntPtr amx, sbyte *name, IntPtr parameters, IntPtr retval)
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            var nameStr = Marshal.PtrToStringAnsi((IntPtr)name);
            client?.PublicCall(amx, nameStr, parameters, retval);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static void Tick()
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            client?.Tick();
        }
    }
}