using System;

namespace SampSharp.Core.Callbacks;

internal class CallbackParameterBoolean : ICallbackParameter
{
    public static readonly CallbackParameterBoolean Instance = new();
    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        return *(int*)parameter.ToPointer() != 0;
    }
}