using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SampSharp.Core;
using SampSharp.Core.Communication;
using SampSharp.Core.Hosting;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Core.Natives.NativeObjects.FastNatives;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.Entities.Systems.IssueTests
{
    public class Issue365FastNatives : ISystem
    {
        private readonly IGameModeClient _client;
        private int _testVehicleId;
        private IntPtr _nativeGetVehicleParamsEx;
        private TestingFastNative _fastProxy;

        public Issue365FastNatives(IGameModeClient client)
        {
            _client = client;
        }

        [Event]
        public void OnGameModeInit(IWorldService worldService, IEntityManager entityManager, INativeObjectProxyFactory proxyFactory, ITimerService timerService)
        {
            // Only test FastNative performance if FastNative is not activated
            if (proxyFactory is FastNativeBasedNativeObjectProxyFactory)
                return;

            //timerService.Start(_ => BenchmarkRunTimer(), TimeSpan.FromSeconds(2));
            timerService.Start(_ => BenchmarkRunTimerProxy(), TimeSpan.FromSeconds(2));

            _nativeGetVehicleParamsEx = Interop.FastNativeFind("GetVehicleParamsEx");
            _testVehicleId = worldService.CreateVehicle(VehicleModelType.BMX, Vector3.One, 0, 0, 0).Entity.Handle;

            var fastFactory = new FastNativeBasedNativeObjectProxyFactory(_client);
            var handleProxy = NativeObjectProxyFactory.CreateInstance<TestingFastNative>();
            var fastProxy = (TestingFastNative)fastFactory.CreateInstance(typeof(TestingFastNative));
            _fastProxy = fastProxy;

            // Call IsPlayerConnected
            timerService.Start(_ =>
            {
                Console.WriteLine("RequiresInvoke: " + ((ISynchronizationProvider)_client).InvokeRequired);
                Console.WriteLine("IsPlayerConnected fast: " + fastProxy.IsPlayerConnected(0));
                Console.WriteLine("IsPlayerConnected handle: " + handleProxy.IsPlayerConnected(0));

                Task.Run(() =>
                {
                    Console.WriteLine("TASK.RequiresInvoke: " + ((ISynchronizationProvider)_client).InvokeRequired);
                    Console.WriteLine("TASK.IsPlayerConnected fast: " + fastProxy.IsPlayerConnected(0));
                    Console.WriteLine("TASK.IsPlayerConnected handle: " + handleProxy.IsPlayerConnected(0));
                });
            }, TimeSpan.FromSeconds(1));


            // Call CreateVehicle native
            var testPosition = new Vector3(65.13f, 123.123f, 555.555f);
            var fastVehicleId = fastProxy.CreateVehicle((int) VehicleModelType.Landstalker, testPosition.X,
                testPosition.Y, testPosition.Z, 15, -1, -1, -1, 0);

            // Create Vehicle entity to verify vehicle position was set properly through the native
            var entity = SampEntities.GetVehicleId(fastVehicleId);
            entityManager.Create(entity);

            entityManager.AddComponent<NativeVehicle>(entity);
            var fastVehicleComp = entityManager.AddComponent<Vehicle>(entity);

            Console.WriteLine($"Created vehicle {fastVehicleId} position {fastVehicleComp.Position}; matches? {(fastVehicleComp.Position == testPosition)}");

            // Call GetVehiclePos
            var ret = fastProxy.GetVehiclePos(fastVehicleId, out var x, out var y, out var z);
            var getPos = new Vector3(x, y, z);
            Console.WriteLine($"get pos ({ret}): {getPos} matches? {(testPosition == getPos)}");
            
            // Test immediate call to SetGameModeText
            // var nativeSetGameModeText = Interop.FastNativeFind("SetGameModeText");
            // SetGameModeTextCall(nativeSetGameModeText, "TestValue");

            // Test via proxy
            fastProxy.SetGameModeText("TestValueViaProxy");
        }
        
        public void BenchmarkRunTimer()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 400000; i++)
            {
                InitialPerformanceTestNativeCall(_nativeGetVehicleParamsEx, _testVehicleId);
            }
            sw.Stop();
            Console.WriteLine("TestMultiple={0}", sw.Elapsed.TotalMilliseconds);
        }

        public void BenchmarkRunTimerProxy()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 400000; i++)
            {
                _fastProxy.GetVehicleParamsEx(_testVehicleId, out var _, out var _, out var _, out var _, out var _,
                    out var _, out var _);
            }
            sw.Stop();
            Console.WriteLine("TestMultiple={0}", sw.Elapsed.TotalMilliseconds);
        }

        private ReadOnlySpan<char> GetStringEncodingAndLength(string inp, out Encoding enc, out int len)
        {
            enc = _client.Encoding ?? Encoding.ASCII;
            var result = inp.AsSpan();
            len = enc.GetByteCount(result) + 1;
            return result;
        }
        
        private unsafe int GetVehiclePosStackBased(int id, out float x, out float y, out float z)
        {
            // prototype to inspect IL code for developing code generator
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
            // prototype to inspect IL code for developing code generator
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
            // prototype to inspect IL code for developing code generator
            var data = stackalloc int[2];

            data[0] = NativeUtils.IntPointerToInt(data + 1);
            data[1] = id;

            return Interop.FastNativeInvoke(new IntPtr(9999), "d", data);
        }

        private unsafe void GetPlayerHealthSpanBased(int id, out float health)
        {
            // prototype to inspect IL code for developing code generator
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
            // prototype to inspect IL code for developing code generator
            var data = stackalloc int[4];

            data[0] = NativeUtils.IntPointerToInt(data + 2);
            data[1] = NativeUtils.IntPointerToInt(data + 3);
            data[2] = id;

            Interop.FastNativeInvoke(new IntPtr(9999), "dR", data);
            
            health = ValueConverter.ToSingle(data[3]);
        }
        
        private unsafe void SetGameModeTextCall(IntPtr nativeSetGameModeText, string textString)
        {
            // concept test with string parameters
            Span<int> data = stackalloc int[16];

            var textSpan = GetStringEncodingAndLength(textString, out var enc, out var len);
            Span<byte> textBytes = stackalloc byte[len];
            textBytes[len - 1] = 0;
            enc.GetBytes(textSpan, textBytes);

            fixed (int* dataPtr = &data.GetPinnableReference())
            fixed (byte* textPtr = &textBytes.GetPinnableReference())
            {
                data[0] = NativeUtils.BytePointerToInt(textPtr);
                Interop.FastNativeInvoke(nativeSetGameModeText, "s", dataPtr);
            }
        }

        private unsafe void SetGameModeTextCallStackBase(string textString)
        {
            // prototype to inspect IL code for developing code generator
            var data = stackalloc int[1];

            var len = NativeUtils.GetByteCount(textString);
            var textBytes = stackalloc byte[len];
            NativeUtils.GetBytes(textString, textBytes, len);


            data[0] = NativeUtils.BytePointerToInt(textBytes);

            Interop.FastNativeInvoke(new IntPtr(9999), "s", data);
        }

        private unsafe void InitialPerformanceTestNativeCall(IntPtr native, int id)
        {
            // most optimal call with span based data
            Span<int> data = stackalloc int[16];
            // 0-7: pointers to cells   [8]
            // 8: id cell               [1]
            // 9-15: out cells          [7]

            // Fixing not needed because data is allocated on stack, but cannot get a pointer without calling GetPinnableRefererence?
            fixed (int* ptData = &data.GetPinnableReference())
            {
                for (var j = 0; j < 8; j++)// set points for all 8 args
                {
                    data[j] = (int) (IntPtr) (ptData + 8 + j);
                }

                data[8] = id;

                Interop.FastNativeInvoke(native, "dRRRRRRR", ptData);
            }
        }

        public class BaseNativeClass
        {
            // public string Prop1 { get; }
            // public string Prop2 { get; }
            //
            // public BaseNativeClass(string prop1, string prop2)
            // {
            //     Prop1 = prop1;
            //     Prop2 = prop2;
            // }

            [NativeMethod]
            public virtual int IsPlayerConnected(int id)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int CreateVehicle(int type, float x, float y, float z, float r, int color1, int color2, int respawnDelay, int hasSiren)
            {
                throw new NativeNotImplementedException();
            }
            
            [NativeMethod]
            public virtual int GetVehiclePos(int vehicleId, out float x, out float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int SetGameModeText(string text)
            {
                throw new NativeNotImplementedException();
            }
            
            [NativeMethod]
            public virtual int GetVehicleParamsEx(int vehicleid, out int a1, out int a2, out int a3, out int a4, out int a5, out int a6, out int a7)
            {
                throw new NativeNotImplementedException();
            }
        }
        public class TestingFastNative : BaseNativeClass
        {
            // private IGameModeClient _gameModeClient;
            // private ISynchronizationProvider _synchronizationProvider;
            // public TestingFastNative(IGameModeClient gameModeClient, string prop1, string prop2) : base(prop1, prop2)
            // {
            //     _gameModeClient = gameModeClient;
            // }

            // public unsafe override int IsPlayerConnected(int id)
            // {
            //     int* data = stackalloc int[2];
            //     data[0] = NativeUtils.IntPointerToInt(data + 1);
            //     data[1] = id;
            //
            //     if (_synchronizationProvider.InvokeRequired)
            //     {
            //         return NativeUtils.SynchronizeInvoke(_synchronizationProvider, new IntPtr(999), "d", data);
            //     }
            //
            //     return Interop.FastNativeInvoke(new IntPtr(999), "d", data);
            // }
        }
    }
}
