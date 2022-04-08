using System.Runtime.InteropServices;

namespace SampSharp.Core.Hosting;

/// <summary>
/// Provides the functions and dat exposed by the SampSharp plugin.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct SampSharpApi
{
    /// <summary>
    /// The plugin data provided to the SampSharp plugin by SA-MP.
    /// </summary>
    public readonly PluginData* PluginData;
    /// <summary>
    /// The sampgdk_FindNative function.
    /// </summary>
    public readonly delegate* unmanaged[Stdcall] <byte*, void*> FindNative;
    /// <summary>
    /// The sampgdk_InvokeNativeArray function.
    /// </summary>
    public readonly delegate* unmanaged[Stdcall] <void*, byte*, int*, int> InvokeNative;
}