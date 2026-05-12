using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Api;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// Provides the parameters for initializing the SampSharp application as provided by the SampSharp open.mp component.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly ref struct SampSharpInitParams
{
    /// <summary>
    /// The size of this structure. Can be used to check if the structure is the correct version.
    /// </summary>
    public readonly Size Size;

    private readonly BlittableStructRef<SampSharpInfo> _info;

    /// <summary>
    /// The open.mp core.
    /// </summary>
    public readonly ICore Core;

    /// <summary>
    /// The open.mp component list.
    /// </summary>
    public readonly IComponentList ComponentList;

    /// <summary>
    /// Gets information about the SampSharp open.mp component.
    /// </summary>
    public SampSharpInfo Info => _info.Value;

    /// <summary>
    /// Gets a pointer to an unmanaged function that configures a cleanup callback to be invoked.
    /// </summary>
    public readonly unsafe delegate* unmanaged[Cdecl]<nint, void> SetOnCleanup;

    /// <summary>
    /// Gets a pointer to an unmanaged function that configures a callback to be invoked when a component is being freed.
    /// </summary>
    public readonly unsafe delegate* unmanaged[Cdecl]<nint, void> SetOnFreeComponent;
}
