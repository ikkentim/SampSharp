namespace SampSharp.OpenMp.Core;

/// <summary>
/// Provides the methods to work with structs which represent pointers to open.mp interfaces. This interface is the
/// managed counterpart the unmanaged interface. 
/// </summary>
public interface IUnmanagedInterface
{
    /// <summary>
    /// Gets the handle to the unmanaged interface instance.
    /// </summary>
    nint Handle { get; }

    /// <summary>
    /// Gets a value indicating whether the pointer has a value.
    /// </summary>
    bool HasValue { get; }
}