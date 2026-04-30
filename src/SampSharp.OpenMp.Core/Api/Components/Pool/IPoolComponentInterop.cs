using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

internal static class IPoolComponentInterop
{
    [DllImport("SampSharp", EntryPoint = "cast_IPoolComponent_to_IPool")]
    public static extern nint cast_IPoolComponent_to_IPool(nint handle);
}