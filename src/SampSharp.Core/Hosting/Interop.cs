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
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class Interop
    {
        [DllImport("SampSharp", EntryPoint = "sampsharp_print", CallingConvention = CallingConvention.StdCall)]
        public static extern void Print(string message);

        [DllImport("SampSharp", EntryPoint = "sampsharp_register_callback", CallingConvention = CallingConvention.StdCall)]
        public static extern void RegisterCallback(IntPtr data);

        [DllImport("SampSharp", EntryPoint = "sampsharp_get_native_handle", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetNativeHandle(string name);

        [DllImport("SampSharp", EntryPoint = "sampsharp_invoke_native", CallingConvention = CallingConvention.StdCall)]
        public static extern void InvokeNative(IntPtr inbuf, int inlen, IntPtr outbuf, ref int outlen);
        
        [DllImport("SampSharp", EntryPoint = "sampsharp_fast_native_find", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FastNativeFind(string name);

        [DllImport("SampSharp", EntryPoint = "sampsharp_fast_native_invoke", CallingConvention = CallingConvention.StdCall)]
        public static unsafe extern int FastNativeInvoke(IntPtr native, string format, int* args);

        public static int PublicCall(string name, IntPtr argumentsPtr, int length)
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            return client?.PublicCall(name, argumentsPtr, length) ?? 1;
        }

        public static void Tick()
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;

            client?.Tick();
        }
    }
}