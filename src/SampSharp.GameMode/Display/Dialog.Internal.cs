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

using SampSharp.GameMode.API;

namespace SampSharp.GameMode.Display
{
    public abstract partial class Dialog
    {
        private static readonly DialogInternal Internal;

        static Dialog()
        {
            Internal = NativeObjectProxyFactory.CreateInstance<DialogInternal>();
        }

        private class DialogInternal
        {
            [NativeMethod]
            public virtual bool ShowPlayerDialog(int playerid, int dialogid, int style, string caption, string info, string button1, string button2)
            {
                throw new NativeNotImplementedException();
            }
        }
    }
}