using System;

namespace SampSharp.Core.Hosting;

internal interface INewCallbackParameter
{
    object GetValue(IntPtr amx, IntPtr parameter);
}