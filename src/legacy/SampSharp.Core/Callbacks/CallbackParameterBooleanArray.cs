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
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Callbacks;

internal class CallbackParameterBooleanArray : ICallbackArrayParameter
{
    public CallbackParameterBooleanArray(int lengthOffset)
    {
        LengthOffset = lengthOffset;
    }

    public int LengthOffset { get; }

    public unsafe object? GetValue(IntPtr amx, IntPtr parameter)
    {
        AmxCell* physAddr;
        Interop.Api->PluginData->AmxExports->GetAddr((Amx*)amx, *(int*)parameter, &physAddr);

        if ((IntPtr)physAddr == IntPtr.Zero)
        {
            return null;
        }

        var len = *(int*)IntPtr.Add(parameter, LengthOffset * AmxCell.Size)
            .ToPointer();

        var result = new bool[len];
        for (var i = 0; i < len; i++)
        {
            result[i] = physAddr[i] != 0;
        }

        return result;
    }
}