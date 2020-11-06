using System;
using SampSharp.Core.Communication;
using SampSharp.Core.Natives.NativeObjects.FastNatives;
using Interop = TestMode.Entities.Systems.IssueTests.Issue365FastNatives.Interop;
namespace TestMode.Entities.Systems.IssueTests
{
    /// <remarks>
    /// prototype to inspect IL code for developing code generator
    /// </remarks>
    public class Issue365FastNativesIlCode
    {
        private void Move(float x, out float y)
        {
            y = x;
        }
        private void Move(bool x, out bool y)
        {
            y = x;
        }
        private void Move(int x, out int y)
        {
            y = x;
        }

        public unsafe int GetPlayerNameB(int playerid, out string name, int strlen)
        {
            var data = stackalloc int[5];

            if(strlen <= 0)
                throw new ArgumentOutOfRangeException(nameof(strlen));

            var nameBuf = strlen < 128 ? stackalloc byte[strlen] : new Span<byte>(new byte[strlen]);

            fixed (byte* nameBufPin = nameBuf)
            {
                data[0] = NativeUtils.IntPointerToInt(data + 3);
                data[1] = (int) (IntPtr) nameBufPin;
                data[2] = NativeUtils.IntPointerToInt(data + 4);

                data[3] = playerid;
                data[4] = strlen;


                var result = Interop.FastNativeInvoke(new IntPtr(9999), "dSd", data);

                name = NativeUtils.GetString(nameBuf);

                return result;
            }
        }
        public unsafe int GetPlayerNameC(int playerid, out string name, int strlen)
        {
            var data = stackalloc int[5];

            if(strlen <= 0)
                throw new ArgumentOutOfRangeException(nameof(strlen));

            var nameBuf = strlen < 128 ? stackalloc byte[strlen] : new Span<byte>(new byte[strlen]);

            fixed (byte* nameBufPin = &nameBuf.GetPinnableReference())
            {
                data[0] = NativeUtils.IntPointerToInt(data + 3);
                data[1] = (int) (IntPtr) nameBufPin;
                data[2] = NativeUtils.IntPointerToInt(data + 4);

                data[3] = playerid;
                data[4] = strlen;


                var result = Interop.FastNativeInvoke(new IntPtr(9999), "dSd", data);

                name = NativeUtils.GetString(nameBuf);

                return result;
            }
        }

        private unsafe int GetVehiclePosStackBased(int id, out float x, out float y, out float z)
        {
            var data = stackalloc int[8];

            data[0] = NativeUtils.IntPointerToInt(data + 4);
            data[1] = NativeUtils.IntPointerToInt(data + 5);
            data[2] = NativeUtils.IntPointerToInt(data + 6);
            data[3] = NativeUtils.IntPointerToInt(data + 7);
            data[4] = id;

            var result = Interop.FastNativeInvoke(new IntPtr(9999), "dRRR", data);

            x = ValueConverter.ToSingle(data[5]);
            y = ValueConverter.ToSingle(data[6]);
            z = ValueConverter.ToSingle(data[7]);

            return result;
        }

        private unsafe void IsPlayerConnectedSpanBased(int id)
        {
            Span<int> data = stackalloc int[2];

            fixed (int* ptr = &data.GetPinnableReference())
            {
                data[0] = NativeUtils.IntPointerToInt(ptr + 1);
                data[1] = id;

                Interop.FastNativeInvoke(new IntPtr(9999), "d", ptr);
            }
        }

        private unsafe int IsPlayerConnectedStackBased(int id)
        {
            var data = stackalloc int[2];

            data[0] = NativeUtils.IntPointerToInt(data + 1);
            data[1] = id;

            return Interop.FastNativeInvoke(new IntPtr(9999), "d", data);
        }

        private unsafe void GetPlayerHealthSpanBased(int id, out float health)
        {
            Span<int> data = stackalloc int[4];

            fixed (int* ptr = &data.GetPinnableReference())
            {
                data[0] = NativeUtils.IntPointerToInt(ptr + 2);
                data[1] = NativeUtils.IntPointerToInt(ptr + 3);
                data[2] = id;

                Interop.FastNativeInvoke(new IntPtr(9999), "dR", ptr);

                health = ValueConverter.ToSingle(data[3]);
            }
        }

        private unsafe void GetPlayerHealthStackBased(int id, out float health)
        {
            var data = stackalloc int[4];

            data[0] = NativeUtils.IntPointerToInt(data + 2);
            data[1] = NativeUtils.IntPointerToInt(data + 3);
            data[2] = id;

            Interop.FastNativeInvoke(new IntPtr(9999), "dR", data);

            health = ValueConverter.ToSingle(data[3]);
        }

        private unsafe void SetGameModeTextCallStackBase(string textString)
        {
            var data = stackalloc int[1];

            var len = NativeUtils.GetByteCount(textString);
            var textBytes = stackalloc byte[len];
            NativeUtils.GetBytes(textString, textBytes, len);


            data[0] = NativeUtils.BytePointerToInt(textBytes);

            Interop.FastNativeInvoke(new IntPtr(9999), "s", data);
        }
    }
}