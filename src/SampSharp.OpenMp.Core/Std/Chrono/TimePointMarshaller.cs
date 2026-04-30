using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a marshaller entrypoint for marshalling <see cref="DateTimeOffset" /> to a native <see cref="TimePoint" /> structure.
/// </summary>
[CustomMarshaller(typeof(DateTimeOffset), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToNative))]
[CustomMarshaller(typeof(DateTimeOffset), MarshalMode.UnmanagedToManagedOut, typeof(ManagedToNative))]
[CustomMarshaller(typeof(DateTimeOffset), MarshalMode.ManagedToUnmanagedOut, typeof(NativeToManaged))]
[CustomMarshaller(typeof(DateTimeOffset), MarshalMode.UnmanagedToManagedIn, typeof(NativeToManaged))]
public static class TimePointMarshaller
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ManagedToNative
    {
        public static TimePoint ConvertToUnmanaged(DateTimeOffset managed)
        {
            return TimePoint.FromDateTimeOffset(managed);
        }
    }
    public static class NativeToManaged
    {
        public static DateTimeOffset ConvertToManaged(TimePoint unmanaged)
        {
            return unmanaged.ToDateTimeOffset();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}