using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a semantic version.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct SemanticVersion
{
    /// <summary>
    /// The major version number.
    /// </summary>
    public readonly byte Major;

    /// <summary>
    /// The minor version number.
    /// </summary>
    public readonly byte Minor;

    /// <summary>
    /// The patch version number.
    /// </summary>
    public readonly byte Patch;

    /// <summary>
    /// The pre-release version number.
    /// </summary>
    public readonly ushort Prerel;

    /// <summary>
    /// Converts this <see cref="SemanticVersion" /> to a <see cref="Version" />.
    /// </summary>
    /// <returns>The version representation of this semantic version.</returns>
    public Version AsVersion()
    {
        return new Version(Major, Minor, Patch, Prerel);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Major}.{Minor}.{Patch}.{Prerel}";
    }
}