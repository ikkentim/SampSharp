using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a marshaller entrypoint for marshalling <see cref="TimeSpan" /> to a native <see cref="Seconds" /> structure.
/// </summary>
[CustomMarshaller(typeof(TimeSpan), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToNative))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.UnmanagedToManagedOut, typeof(ManagedToNative))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.ManagedToUnmanagedOut, typeof(NativeToManaged))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.UnmanagedToManagedIn, typeof(NativeToManaged))]
public static class SecondsMarshaller
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ManagedToNative
    {
        public static Seconds ConvertToUnmanaged(TimeSpan managed)
        {
            return new Seconds((long)managed.TotalSeconds);
        }
    }
    public static class NativeToManaged
    {
        public static TimeSpan ConvertToManaged(Seconds unmanaged)
        {
            return unmanaged.AsTimeSpan();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}