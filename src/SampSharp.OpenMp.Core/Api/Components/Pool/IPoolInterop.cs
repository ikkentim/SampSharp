using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

internal static partial class IPoolInterop
{
    [LibraryImport("SampSharp", EntryPoint = "IPool_release")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void IPool_release(nint handle, int index);

    [LibraryImport("SampSharp", EntryPoint = "IPool_lock")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void IPool_lock(nint handle, int index);

    [LibraryImport("SampSharp", EntryPoint = "IPool_unlock")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IPool_unlock(nint handle, int index);

    [LibraryImport("SampSharp", EntryPoint = "IPool_getPoolEventDispatcher")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial nint IPool_getPoolEventDispatcher(nint handle);

    [LibraryImport("SampSharp", EntryPoint = "IPool_entries")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial nint IPool_entries(nint handle);

    [LibraryImport("SampSharp", EntryPoint = "IPool_count")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial Size IPool_count(nint handle);
}