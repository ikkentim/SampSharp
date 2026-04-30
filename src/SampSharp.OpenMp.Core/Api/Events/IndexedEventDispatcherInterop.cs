using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

internal partial class IndexedEventDispatcherInterop
{
    [LibraryImport("SampSharp", EntryPoint = "IIndexedEventDispatcher_count")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial Size Count(nint dispatcherHandle);

    [LibraryImport("SampSharp", EntryPoint = "IIndexedEventDispatcher_count_index")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial Size Count(nint dispatcherHandle, Size index);
    
    [LibraryImport("SampSharp", EntryPoint = "IIndexedEventDispatcher_addEventHandler")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool AddEventHandler(nint dispatcherHandle, nint handlerHandle, Size index, EventPriority priority);

    [LibraryImport("SampSharp", EntryPoint = "IIndexedEventDispatcher_removeEventHandler")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool RemoveEventHandler(nint dispatcherHandle, nint handlerHandle, Size index);

    [LibraryImport("SampSharp", EntryPoint = "IIndexedEventDispatcher_hasEventHandler")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool HasEventHandler(nint dispatcherHandle, nint handlerHandle, Size index, out EventPriority priority);

}