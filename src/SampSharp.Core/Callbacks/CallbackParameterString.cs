// SampSharp
// Copyright 2022 Tim Potze
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
using System.Text;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Callbacks;

internal class CallbackParameterString : ICallbackParameter
{
    public static readonly CallbackParameterString Instance = new();

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        AmxCell* physAddr;
        Interop.Api->PluginData->AmxExports->GetAddr((Amx*)amx, *(int *)parameter, &physAddr);

        if ((IntPtr)physAddr == IntPtr.Zero)
        {
            return null;
        }

        var len = 0;
        Interop.Api->PluginData->AmxExports->StrLen(physAddr, &len);
        len++;

        var buf = len < 100 ? stackalloc byte[len] : new byte[len];
            
        fixed (byte* p = &buf.GetPinnableReference())
        {
            Interop.Api->PluginData->AmxExports->GetString(p, physAddr, 0, len);
        }

        while (buf.Length > 0 && buf[^1] == 0)
        {
            buf = buf[..^1];
        }

        return Encoding.ASCII.GetString(buf);
    }
}