using System;

namespace SampSharp.Core.Hosting;

internal class NewCallbackParameterSingle : INewCallbackParameter
{
    public static readonly NewCallbackParameterSingle Instance = new();
    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        return *(float*)parameter.ToPointer();
    }
}