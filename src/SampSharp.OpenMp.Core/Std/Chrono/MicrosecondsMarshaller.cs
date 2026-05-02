using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a marshaller entrypoint for marshalling <see cref="TimeSpan" /> to a native <see cref="Microseconds" /> structure.
/// </summary>
[CustomMarshaller(typeof(TimeSpan), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToNative))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.UnmanagedToManagedOut, typeof(ManagedToNative))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.ManagedToUnmanagedOut, typeof(NativeToManaged))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.UnmanagedToManagedIn, typeof(NativeToManaged))]
public static class MicrosecondsMarshaller
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ManagedToNative
    {
        public static Microseconds ConvertToUnmanaged(TimeSpan managed)
        {
            return new Microseconds((long)managed.TotalMicroseconds);
        }
    }

    public static class NativeToManaged
    {
        public static TimeSpan ConvertToManaged(Microseconds unmanaged)
        {
            return unmanaged.AsTimeSpan();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}