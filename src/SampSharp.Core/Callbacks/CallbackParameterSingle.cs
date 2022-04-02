using System;

namespace SampSharp.Core.Callbacks;

internal class CallbackParameterSingle : ICallbackParameter
{
    public static readonly CallbackParameterSingle Instance = new();
    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        return *(float*)parameter.ToPointer();
    }
}