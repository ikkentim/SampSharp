using System;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Callbacks;

internal class CallbackParameterIntArray : ICallbackParameter
{
    private readonly int _lengthOffset;

    public CallbackParameterIntArray(int lengthOffset)
    {
        _lengthOffset = lengthOffset;
    }

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        int* physAddr;
        Interop.Api->PluginData->AmxExports->GetAddr((void *)amx, *(int *)parameter, (void**)&physAddr);
            
        if ((IntPtr)physAddr == IntPtr.Zero)
        {
            return null;
        }
            
        var len = *(int*)IntPtr.Add(parameter, _lengthOffset * 4); // assuming length is next parameter

        var result = new int[len];
        new Span<int>(physAddr, len).CopyTo(new Span<int>(result));

        return result;
    }
}