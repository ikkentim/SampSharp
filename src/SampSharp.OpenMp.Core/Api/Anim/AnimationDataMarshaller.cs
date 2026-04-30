using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a marshaller entrypoint for marshalling <see cref="AnimationData" /> to its unmanaged counterpart.
/// </summary>
[CustomMarshaller(typeof(AnimationData), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToNative))]
[CustomMarshaller(typeof(AnimationData), MarshalMode.ManagedToUnmanagedOut, typeof(NativeToManaged))]
public static unsafe class AnimationDataMarshaller
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ManagedToNative
    {
        public static int BufferSize { get; } = Marshal.SizeOf<Native>();
        
        public static BlittableStructRef<Native> ConvertToUnmanaged(AnimationData managed, Span<byte> callerAllocatedBuffer)
        {
            var native = ToNative(managed);

            
            var ptr = (nint)Unsafe.AsPointer(ref callerAllocatedBuffer.GetPinnableReference());
            Marshal.StructureToPtr(native, ptr, false);

            return new BlittableStructRef<Native>(ptr);
        }
        
        private static Native ToNative(AnimationData managed)
        {
            return new Native(managed.Delta, managed.Loop, managed.LockX, managed.LockY, managed.Freeze, managed.Time, new HybridString16(managed.Library),
                new HybridString24(managed.Name));
        }
    }
    
    public static class NativeToManaged
    {
        public static AnimationData? ConvertToManaged(BlittableStructRef<Native> unmanaged)
        {
            if (!unmanaged.HasValue)
            {
                return null;
            }

            var native = unmanaged.GetValueOrDefault();

            return FromNative(native);
        }
        
        private static AnimationData FromNative(Native native)
        {
            return new AnimationData(native.Delta, native.Loop, native.LockX, native.LockY, native.Freeze, native.Time, native.Lib.ToString(), native.Name.ToString());
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Native
    {
        [FieldOffset(0)]
        public float Delta;
        [FieldOffset(4)]
        public bool Loop;
        [FieldOffset(5)]
        public bool LockX;
        [FieldOffset(6)]
        public bool LockY;
        [FieldOffset(7)]
        public bool Freeze;
        [FieldOffset(8)]
        public uint Time;
        [FieldOffset(16)]
        public HybridString16 Lib;
        [FieldOffset(40)]
        public HybridString24 Name;

        public Native(float delta, bool loop, bool lockX, bool lockY, bool freeze, uint time, HybridString16 lib, HybridString24 name)
        {
            Delta = delta;
            Loop = loop;
            LockX = lockX;
            LockY = lockY;
            Freeze = freeze;
            Time = time;
            Lib = lib;
            Name = name;
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}