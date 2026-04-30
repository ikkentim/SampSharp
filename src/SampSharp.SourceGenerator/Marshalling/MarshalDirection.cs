namespace SampSharp.SourceGenerator.Marshalling;

/// <summary>
/// Represents the direction of marshal code, unmanaged-to-managed or managed-to-unmanaged.
/// </summary>
public enum MarshalDirection
{
    /// <summary>
    /// managed-to-unmanaged.
    /// </summary>
    ManagedToUnmanaged,

    /// <summary>
    /// unmanaged-to-managed.
    /// </summary>
    UnmanagedToManaged
}