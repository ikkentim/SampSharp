using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.RobinHood;

/// <summary>
/// Represents a marshaller entrypoint for marshalling a native <see cref="FlatHashSetStringView" /> structure to to a managed enumerable of <see langword="string" /> values.
/// </summary>
[CustomMarshaller(typeof(IEnumerable<string>), MarshalMode.UnmanagedToManagedIn, typeof(FlatHashSetStringViewMarshaller))]
public static class FlatHashSetStringViewMarshaller
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static IEnumerable<string> ConvertToManaged(FlatHashSetStringView set)
    {
        return set;
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}