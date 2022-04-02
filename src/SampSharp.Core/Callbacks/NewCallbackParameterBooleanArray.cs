using System;

namespace SampSharp.Core.Hosting;

internal class NewCallbackParameterBooleanArray : INewCallbackParameter
{
    private readonly int _lengthOffset;

    public NewCallbackParameterBooleanArray(int lengthOffset)
    {
        _lengthOffset = lengthOffset;
    }

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        int* physAddr;
        Interop.Api->PluginData->AmxExports->GetAddr(amx.ToPointer(), *(int *)parameter, (void**)&physAddr);
            
        if ((IntPtr)physAddr == IntPtr.Zero)
        {
            return null;
        }
            
        var len = *(int*)IntPtr.Add(parameter, _lengthOffset * 4).ToPointer(); // assuming length is next parameter

        var result = new bool[len];
        for (var i = 0; i < len; i++)
        {
            result[i] = physAddr[i] != 0;
        }

        return result;
    }
}