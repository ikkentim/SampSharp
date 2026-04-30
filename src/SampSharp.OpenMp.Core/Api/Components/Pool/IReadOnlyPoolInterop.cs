using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

internal static class IReadOnlyPoolInterop
{
    [DllImport("SampSharp", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IReadOnlyPool_get", ExactSpelling = true)]
    public static extern nint IReadOnlyPool_get(nint handle_, int index);

    [DllImport("SampSharp", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IReadOnlyPool_bounds", ExactSpelling = true)]
    public static extern void IReadOnlyPool_bounds(nint handle_, out Pair<Size, Size> bounds);
}