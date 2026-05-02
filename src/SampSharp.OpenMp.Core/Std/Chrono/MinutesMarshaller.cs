using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a marshaller entrypoint for marshalling <see cref="TimeSpan" /> to a native <see cref="Minutes" /> structure.
/// </summary>
[CustomMarshaller(typeof(TimeSpan), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToNative))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.UnmanagedToManagedOut, typeof(ManagedToNative))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.ManagedToUnmanagedOut, typeof(NativeToManaged))]
[CustomMarshaller(typeof(TimeSpan), MarshalMode.UnmanagedToManagedIn, typeof(NativeToManaged))]
public static class MinutesMarshaller
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ManagedToNative
    {
        public static Minutes ConvertToUnmanaged(TimeSpan managed)
        {
            return new Minutes((int)managed.TotalMinutes);
        }
    }

    public static class NativeToManaged
    {
        public static TimeSpan ConvertToManaged(Minutes unmanaged)
        {
            return unmanaged.AsTimeSpan();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
