using System;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Callbacks;

internal class CallbackParameterSingleArray : ICallbackArrayParameter
{
    private readonly int _lengthOffset;

    public CallbackParameterSingleArray(int lengthOffset)
    {
        _lengthOffset = lengthOffset;
    }

    public int LengthOffset => _lengthOffset;

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        AmxCell* physAddr;
        Interop.Api->PluginData->AmxExports->GetAddr((Amx*)amx, *(int*)parameter, &physAddr);
            
        if ((IntPtr)physAddr == IntPtr.Zero)
        {
            return null;
        }
            
        var len = *(int*)IntPtr.Add(parameter, _lengthOffset * AmxCell.Size);

        var result = new float[len];
        new Span<float>(physAddr, len).CopyTo(new Span<float>(result));

        return result;
    }
}