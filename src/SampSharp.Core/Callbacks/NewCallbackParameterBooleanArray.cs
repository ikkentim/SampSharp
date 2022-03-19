using System;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Callbacks;

internal class NewCallbackParameterBooleanArray : INewCallbackParameter
{
    private readonly int _lengthOffset;

    public NewCallbackParameterBooleanArray(int lengthOffset)
    {
        _lengthOffset = lengthOffset;
    }

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        var par = *(int**)parameter.ToPointer();
        var len = *(int*)IntPtr.Add(parameter, _lengthOffset * 4).ToPointer(); // assuming length is next parameter

        var phys = (int *)Interop.GetAddress(amx, new IntPtr(par)).ToPointer();

        var result = new bool[len];

        for (var i = 0; i < len; i++)
        {
            result[i] = phys[i] != 0;
        }

        return result;
    }
}