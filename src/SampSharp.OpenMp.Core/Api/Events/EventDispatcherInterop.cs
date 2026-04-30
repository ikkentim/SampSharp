using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

internal partial class EventDispatcherInterop
{
    [LibraryImport("SampSharp", EntryPoint = "IEventDispatcher_addEventHandler")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool AddEventHandler(nint dispatcherHandle, nint handlerHandle, EventPriority priority);

    [LibraryImport("SampSharp", EntryPoint = "IEventDispatcher_removeEventHandler")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool RemoveEventHandler(nint dispatcherHandle, nint handlerHandle);

    [LibraryImport("SampSharp", EntryPoint = "IEventDispatcher_hasEventHandler")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool HasEventHandler(nint dispatcherHandle, nint handlerHandle, out EventPriority priority);
    
    [LibraryImport("SampSharp", EntryPoint = "IEventDispatcher_count")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial Size Count(nint dispatcherHandle);
}