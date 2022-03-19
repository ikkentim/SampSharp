using System;
using SampSharp.Core.Hosting;
using SampSharp.Core.Natives.NativeObjects.FastNatives;

namespace SampSharp.Core.Callbacks;

internal class NewCallbackParameterString : INewCallbackParameter
{
    public static readonly NewCallbackParameterString Instance = new();

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        var par = *(int**)parameter.ToPointer();

        var phys = Interop.GetAddress(amx, new IntPtr(par));
        var len = Interop.GetStringLength(phys) + 1; // space for terminator
        var buf = len < 100 ? stackalloc byte[len] : new byte[len];

        fixed (byte* p = &buf.GetPinnableReference())
        {
            _ = Interop.GetString(phys, new IntPtr(p), (uint)len);
        }

        return NativeUtils.GetString(buf);
    }
}