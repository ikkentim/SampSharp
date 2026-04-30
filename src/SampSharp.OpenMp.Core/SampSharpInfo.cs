using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Api;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// Provides information about the SampSharp open.mp component.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct SampSharpInfo
{
    /// <summary>
    /// The size of this structure. Can be used to check if the structure is the correct version.
    /// </summary>
    public readonly Size Size;

    /// <summary>
    /// The unmanaged &lt;&gt; managed API version of the SampSharp open.mp component.
    /// </summary>
    public readonly int ApiVersion;

    /// <summary>
    /// The version of the SampSharp open.mp component.
    /// </summary>
    public readonly SemanticVersion Version;
}