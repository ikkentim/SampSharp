using System;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Callbacks;

internal class CallbackParameterBooleanArray : ICallbackArrayParameter
{
    private readonly int _lengthOffset;
    
    public CallbackParameterBooleanArray(int lengthOffset)
    {
        _lengthOffset = lengthOffset;
    }

    public int LengthOffset => _lengthOffset;

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        AmxCell* physAddr;
        Interop.Api->PluginData->AmxExports->GetAddr((Amx*)amx, *(int *)parameter, &physAddr);
            
        if ((IntPtr)physAddr == IntPtr.Zero)
        {
            return null;
        }
            
        var len = *(int*)IntPtr.Add(parameter, _lengthOffset * AmxCell.Size).ToPointer();

        var result = new bool[len];
        for (var i = 0; i < len; i++)
        {
            result[i] = physAddr[i] != 0;
        }

        return result;
    }
}