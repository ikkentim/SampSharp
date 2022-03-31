using System.Runtime.InteropServices;

namespace SampSharp.Core.Hosting;

[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct SampSharpApi
{
    /// <summary>
    /// The plugin data provided to the SampSharp plugin by SA-MP.
    /// </summary>
    public readonly PluginData* PluginData;
    public readonly delegate* unmanaged[Stdcall] <char*, void*> FindNative;
    public readonly delegate* unmanaged[Stdcall] <void*, char*, int*, int> InvokeNative;
}