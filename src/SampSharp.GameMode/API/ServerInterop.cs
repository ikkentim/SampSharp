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
namespace SampSharp.GameMode.API
{
    public sealed class ServerInterop : IInterop
    {
        public int LoadNative(string name, string format, int[] sizes)
        {
            return Interop.LoadNative(name, format, sizes);
        }

        public int InvokeNative(int handle, object[] args)
        {
            return Interop.InvokeNative(handle, args);
        }

        public bool NativeExists(string name)
        {
            return Interop.NativeExists(name);
        }

        public bool RegisterExtension(object extension)
        {
            return Interop.RegisterExtension(extension);
        }

        public int SetTimer(int interval, bool repeat, object args)
        {
            return Interop.SetTimer(interval, repeat, args);
        }

        public bool KillTimer(int timerid)
        {
            return Interop.KillTimer(timerid);
        }

        public void Print(string msg)
        {
            Interop.Print(msg);
        }

        public void SetCodepage(string codepage)
        {
            Interop.SetCodepage(codepage);
        }
    }
}