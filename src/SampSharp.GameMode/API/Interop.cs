// SampSharp
// Copyright 2016 Tim Potze
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

namespace SampSharp.GameMode.API
{
    internal class Interop
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int LoadNative(string name, string format, int[] sizes);

        [MethodImpl((MethodImplOptions.InternalCall))]
        public static extern int InvokeNative(int handle, object[] args);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool NativeExists(string name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RegisterExtension(object extension);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int SetTimer(int interval, bool repeat, object args);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool KillTimer(int timerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Print(string msg);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SetCodepage(string codepage);
    }
}