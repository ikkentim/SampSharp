using System;
using System.Runtime.InteropServices;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Callbacks;

internal class NewCallbackParameterSingleArray : INewCallbackParameter
{
    private readonly int _lengthOffset;

    public NewCallbackParameterSingleArray(int lengthOffset)
    {
        _lengthOffset = lengthOffset;
    }

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        var par = *(int**)parameter.ToPointer();
        var len = *(int*)IntPtr.Add(parameter, _lengthOffset * 4).ToPointer(); // assuming length is next parameter

        var phys = Interop.GetAddress(amx, new IntPtr(par));

        var result = new float[len];
        Marshal.Copy(phys, result, 0, len);

        return result;
    }
}