using System;

namespace SampSharp.Core.Callbacks;

internal interface INewCallbackParameter
{
    object GetValue(IntPtr amx, IntPtr parameter);
}