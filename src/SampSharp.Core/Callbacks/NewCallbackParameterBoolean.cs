using System;

namespace SampSharp.Core.Callbacks;

internal class NewCallbackParameterBoolean : INewCallbackParameter
{
    public static readonly NewCallbackParameterBoolean Instance = new();
    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        return *(int*)parameter.ToPointer() != 0;
    }
}