namespace SampSharp.SourceGenerator.Marshalling;

public enum MarshalMode
{
    Default,
    ManagedToUnmanagedIn,
    ManagedToUnmanagedRef,
    ManagedToUnmanagedOut,
    UnmanagedToManagedIn,
    UnmanagedToManagedRef,
    UnmanagedToManagedOut,
    Other
}