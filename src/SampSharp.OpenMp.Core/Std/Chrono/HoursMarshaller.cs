using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a marshaller entrypoint for marshalling <see cref="TimeSpan" /> to a native <see cref="Hours" />
/// structure.
/// </summary>
[CustomMarshaller(typeof(TimeSpan), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToNative))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.UnmanagedToManagedOut, typeof(ManagedToNative))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.ManagedToUnmanagedOut, typeof(NativeToManaged))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.UnmanagedToManagedIn, typeof(NativeToManaged))]
public static class HoursMarshaller
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ManagedToNative
    {
        public static Hours ConvertToUnmanaged(TimeSpan managed)
        {
            return new Hours((int)managed.TotalHours);
        }
    }

    public static class NativeToManaged
    {
        public static TimeSpan ConvertToManaged(Hours unmanaged)
        {
            return unmanaged.AsTimeSpan();
        }
    }
#pragma warning restore CS1591
}