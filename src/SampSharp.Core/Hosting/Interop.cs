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
using System.Runtime.InteropServices;
using System.Text;

namespace SampSharp.Core.Hosting
{
    /// <summary>
    /// Contains interop functions for the SampSharp plugin.
    /// </summary>
    public static unsafe class Interop
    {
        /// <summary>
        /// Registers a callback in the SampSharp plugin.
        /// </summary>
        /// <param name="data">The callback data.</param>
        [DllImport("SampSharp", EntryPoint = "sampsharp_register_callback", CallingConvention = CallingConvention.StdCall)]
        public static extern void RegisterCallback(IntPtr data);
        
        public static int FastNativeInvoke(IntPtr native, string format, int* args)
        {
            var bytes = Encoding.ASCII.GetByteCount(format);
            var formatSt = stackalloc byte[bytes];
            Encoding.ASCII.GetBytes(format, new Span<byte>(formatSt, bytes));

            return api->InvokeNative(native.ToPointer(), (char *)formatSt, args);
        }

        public static IntPtr FastNativeFind(string name)
        {
            var bytes = Encoding.ASCII.GetByteCount(name);
            var nameStack = stackalloc byte[bytes];
            Encoding.ASCII.GetBytes(name, new Span<byte>(nameStack, bytes));

            return (IntPtr)api->FindNative((char *)nameStack);
        }

        public static void Print(string txt)
        {
            var format = stackalloc byte[3];
            format[0] = 0x25; // "%s\0"
            format[1] = 0x73;
            format[2] = 0x00;
            
            var bytes = Encoding.ASCII.GetByteCount(txt);

            if (bytes < 200)
            {
                var txtStack = stackalloc byte[bytes];
                Encoding.ASCII.GetBytes(txt, new Span<byte>(txtStack, bytes));
                api->PluginData->Logprintf((char *)format, (char *)txtStack);
            }
            else
            {
                var ptr = Marshal.StringToHGlobalAnsi(txt);

                try
                {
                    api->PluginData->Logprintf((char *)format, (char *)ptr.ToPointer());
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
        }

        private static SampSharpApi* api;

        [DllImport("SampSharp", EntryPoint = "sampsharp_get_api", CallingConvention = CallingConvention.StdCall)]
        private static extern SampSharpApi* GetApi();
        
        
        public static void Testing()
        {
            Console.WriteLine("testing things");
            
            Print("hello worlddd");
        }
        
        internal static void Init()
        {
            api = GetApi();
        }

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