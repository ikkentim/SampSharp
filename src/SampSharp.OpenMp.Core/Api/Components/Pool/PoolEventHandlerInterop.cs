using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

internal static partial class PoolEventHandlerInterop
{
    [LibraryImport("SampSharp", EntryPoint = "PoolEventHandlerImpl_create")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial nint PoolEventHandlerImpl_create(nint onPoolEntryCreated, nint onPoolEntryDestroyed);

    [LibraryImport("SampSharp", EntryPoint = "PoolEventHandlerImpl_delete")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void PoolEventHandlerImpl_delete(nint ptr);
}