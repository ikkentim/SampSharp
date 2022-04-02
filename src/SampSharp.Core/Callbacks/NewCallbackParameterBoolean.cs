using System;

namespace SampSharp.Core.Hosting;

internal class NewCallbackParameterBoolean : INewCallbackParameter
{
    public static readonly NewCallbackParameterBoolean Instance = new();
    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        return *(int*)parameter.ToPointer() != 0;
    }
}