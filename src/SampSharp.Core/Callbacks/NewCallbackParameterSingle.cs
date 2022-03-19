using System;

namespace SampSharp.Core.Callbacks;

internal class NewCallbackParameterSingle : INewCallbackParameter
{
    public static readonly NewCallbackParameterSingle Instance = new();
    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        return *(float*)parameter.ToPointer();
    }
}