using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

internal readonly unsafe struct HybridStringDynamicStorage
{
    public readonly byte* Data;
    public readonly delegate* unmanaged[Cdecl]<nint, void> FreePointer;

    private HybridStringDynamicStorage(byte* data, delegate* unmanaged[Cdecl]<nint, void> freePointer)
    {
        Data = data;
        FreePointer = freePointer;
    }

    public static HybridStringDynamicStorage Allocate(int length)
    {
        var data = (byte*)Marshal.AllocHGlobal(length);
        return new HybridStringDynamicStorage(data, &Free);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void Free(nint data)
    {
        Marshal.FreeHGlobal(data);
    }
}