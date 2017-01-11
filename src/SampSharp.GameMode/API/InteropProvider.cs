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
using System;

namespace SampSharp.GameMode.API
{
    public static class InteropProvider
    {
        private static IInterop _provider = new ServerInterop();

        public static IInterop Provider
        {
            get { return _provider; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                _provider = value;
            }
        }

        public static int LoadNative(string name, string format, int[] sizes)
        {
            return Provider.LoadNative(name, format, sizes);
        }

        public static int InvokeNative(int handle, object[] args)
        {
            return Provider.InvokeNative(handle, args);
        }

        public static bool NativeExists(string name)
        {
            return Provider.NativeExists(name);
        }

        public static bool RegisterExtension(object extension)
        {
            return Provider.RegisterExtension(extension);
        }

        public static int SetTimer(int interval, bool repeat, object args)
        {
            return Provider.SetTimer(interval, repeat, args);
        }

        public static bool KillTimer(int timerid)
        {
            return Provider.KillTimer(timerid);
        }

        public static void Print(string msg)
        {
            Provider.Print(msg);
        }

        public static void SetCodepage(string codepage)
        {
            Provider.SetCodepage(codepage);
        }
    }
}