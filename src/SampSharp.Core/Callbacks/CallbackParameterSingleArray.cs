using System;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Callbacks;

internal class CallbackParameterSingleArray : ICallbackParameter
{
    private readonly int _lengthOffset;

    public CallbackParameterSingleArray(int lengthOffset)
    {
        _lengthOffset = lengthOffset;
    }

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        float* physAddr;
        Interop.Api->PluginData->AmxExports->GetAddr((void *)amx, *(int*)parameter, (void**)&physAddr);
            
        if ((IntPtr)physAddr == IntPtr.Zero)
        {
            return null;
        }
            
        var len = *(int*)IntPtr.Add(parameter, _lengthOffset * 4);

        var result = new float[len];
        new Span<float>(physAddr, len).CopyTo(new Span<float>(result));

        return result;
    }
}