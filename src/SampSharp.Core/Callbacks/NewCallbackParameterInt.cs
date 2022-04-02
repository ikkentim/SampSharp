using System;

namespace SampSharp.Core.Hosting;

internal class NewCallbackParameterInt : INewCallbackParameter
{
    public static readonly NewCallbackParameterInt Instance = new();
    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        return *(int*)parameter.ToPointer();
    }
}