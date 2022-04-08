using System;

namespace SampSharp.Core.Callbacks;

internal class CallbackParameterInt : ICallbackParameter
{
    public static readonly CallbackParameterInt Instance = new();
    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        return *(int*)parameter.ToPointer();
    }
}