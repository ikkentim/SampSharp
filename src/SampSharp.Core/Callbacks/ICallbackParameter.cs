using System;

namespace SampSharp.Core.Callbacks;

internal interface ICallbackParameter
{
    object GetValue(IntPtr amx, IntPtr parameter);
}