using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a marshaller entrypoint for marshalling <see cref="BanEntry" /> to its unmanaged counterpart.
/// </summary>
[CustomMarshaller(typeof(BanEntry), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToNative))]
[CustomMarshaller(typeof(BanEntry), MarshalMode.ManagedToUnmanagedOut, typeof(NativeToManaged))]
public static unsafe class BanEntryMarshaller
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ManagedToNative
    {
        public static int BufferSize { get; } = Marshal.SizeOf<Native>();

        public static BlittableStructRef<Native> ConvertToUnmanaged(BanEntry managed, Span<byte> callerAllocatedBuffer)
        {
            var native = ToNative(managed);

            
            var ptr = (nint)Unsafe.AsPointer(ref callerAllocatedBuffer.GetPinnableReference());
            Marshal.StructureToPtr(native, ptr, false);

            return new BlittableStructRef<Native>(ptr);
        }
        
        private static Native ToNative(BanEntry entry)
        {
            return new Native(new HybridString46(entry.Address), 
                TimePoint.FromDateTimeOffset(entry.Time), 
                new HybridString25(entry.Name),
                new HybridString32(entry.Reason));
        }
    }

    public static class NativeToManaged
    {
        public static BanEntry? ConvertToManaged(BlittableStructRef<Native> unmanaged)
        {
            if (!unmanaged.HasValue)
            {
                return null;
            }

            var native = unmanaged.GetValueOrDefault();

            return FromNative(native);
        }
        
        private static BanEntry FromNative(Native native)
        {
            return new BanEntry(native.AddressString.ToString(),
                native.Time.ToDateTimeOffset(),
                native.Name.ToString(),
                native.Reason.ToString());
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Native(HybridString46 addressString, TimePoint time, HybridString25 name, HybridString32 reason)
    {
        public readonly HybridString46 AddressString = addressString;
        public readonly TimePoint Time = time;
        public readonly HybridString25 Name = name; // MAX_PLAYER_NAME + 1
        public readonly HybridString32 Reason = reason;
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}