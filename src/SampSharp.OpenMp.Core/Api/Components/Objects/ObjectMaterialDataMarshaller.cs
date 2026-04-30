using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a marshaller entrypoint for marshalling <see cref="ObjectMaterialData" /> to its unmanaged counterpart.
/// </summary>
[CustomMarshaller(typeof(ObjectMaterialData), MarshalMode.ManagedToUnmanagedOut, typeof(NativeToManaged))]
public static class ObjectMaterialDataMarshaller
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class NativeToManaged
    {
        public static ObjectMaterialData? ConvertToManaged(BlittableStructRef<NativeObjMat> unmanaged)
        {
            if (!unmanaged.HasValue)
            {
                return null;
            }

            var native = unmanaged.GetValueOrDefault();

            return FromNative(native);
        }

        private static ObjectMaterialData FromNative(NativeObjMat native)
        {
            return new ObjectMaterialData(native.Model, native.MaterialSize, native.FontSize, native.Alignment, native.Bold, native.MaterialColour, native.BackgroundColour,
                native.TextOrTXD.ToString(), native.FontOrTexture.ToString(), native.Type, native.Used);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct NativeObjMat
    {
        [FieldOffset(0)] public readonly int Model;
        [FieldOffset(0)] public readonly byte MaterialSize;
        [FieldOffset(1)] public readonly byte FontSize;
        [FieldOffset(2)] public readonly byte Alignment;
        [FieldOffset(3)] public readonly BlittableBoolean Bold; // len = 1
        [FieldOffset(4)] public readonly Colour MaterialColour; // len = 4
        [FieldOffset(8)] public readonly Colour BackgroundColour; // len = 4
        [FieldOffset(16)] public readonly HybridString32 TextOrTXD; // len = 40
        [FieldOffset(56)] public readonly HybridString32 FontOrTexture; // len = 40
        [FieldOffset(96)] public readonly MaterialType Type; // len = 1
        [FieldOffset(97)] public readonly BlittableBoolean Used; // len = 1
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}