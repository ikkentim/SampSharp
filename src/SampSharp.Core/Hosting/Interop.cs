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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace SampSharp.Core.Hosting
{
    /// <summary>
    /// Contains interop functions for the SampSharp plugin.
    /// </summary>
    public class Interop
    {
        internal delegate bool EntryPointDelegate(string assemblyName);
        internal delegate void PublicCallDelegate(IntPtr amx, string name, IntPtr parameters, IntPtr retval);

        /// <summary>
        /// Prints the specified message to the SA:MP server log.
        /// </summary>
        /// <param name="message">The message.</param>
        [DllImport("SampSharp", EntryPoint = "sampsharp_print", CallingConvention = CallingConvention.StdCall)]
        public static extern void Print(string message);
        
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
        
        [DllImport("SampSharp", EntryPoint = "sampsharp_get_plugin_version", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPluginVersionInternal();

        [DllImport("SampSharp", EntryPoint = "sampsharp_get_addr", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetAddress(IntPtr amx, IntPtr amxAddress);

        [DllImport("SampSharp", EntryPoint = "sampsharp_get_string", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetString(IntPtr physicalAddress, IntPtr destination, uint destinationLength);

        [DllImport("SampSharp", EntryPoint = "sampsharp_get_string_len", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetStringLength(IntPtr physicalAddress);

        internal static Version GetPluginVersion()
        {
            try
            {
                var num = GetPluginVersionInternal();

                var major = (int)(0xff & (num >> 24));
                var minor = (int)(0xff & (num >> 16));
                var build = (int)(0xff & (num >> 8));

                return new Version(major, minor, build, 0);
            }
            catch (EntryPointNotFoundException)
            {
                return new Version(0, 0, 0, 0);
            }
        }

        [UnmanagedCallersOnly]
        internal static void OnTick()
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;
            client?.Tick();
        }

        internal static bool InvokeEntryPoint(string assemblyName)
        {
            try
            {
                var dmn = Thread.GetDomain();
                SampSharpInfo._entryAssembly = Assembly.Load(assemblyName);
                dmn.ExecuteAssemblyByName(assemblyName, Array.Empty<string>());

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        internal static void OnPublicCall(IntPtr amx, string name, IntPtr parameters, IntPtr retval)
        {
            var client = InternalStorage.RunningClient as HostedGameModeClient;
            client?.PublicCall(amx, name, parameters, retval);
        }
    }
}
