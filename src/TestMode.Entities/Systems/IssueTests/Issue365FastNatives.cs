using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SampSharp.Core;
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
        
        private static void TestInOutInt(TestNatives test)
        {
            if(test.InOutInt(471) != 471)
                throw new InvalidOperationException("arg mismatch");
        }
        
        private static void TestInIdOutInt(TestNatives test)
        {
            test.Identifier = 5515;
            if(test.InIdOutRef(out var a) != 1 || a != 5515)
                throw new InvalidOperationException("arg mismatch");
        }

        private static void TestInRefOutInt(TestNatives test)
        {
            if(1 != test.InRefOutInt(789, out var b) || b != 789)
                throw new InvalidOperationException("arg mismatch");
        }
        private static void TestInOutString(TestNatives test)
        {
            if(1 != test.InOutString("foo bar", out var b, 32) || b != "foo bar")
                throw new InvalidOperationException("arg mismatch");
        }
        private static void TestInOutIntArray(TestNatives test)
        {
            var arr = new[] {5, 9, 1};
            
            if (1 != test.InOutIntArray(arr, out var b, 3) || !arr.SequenceEqual(b))
                throw new InvalidOperationException("arg mismatch");
        }
        private static void TestInOutBoolArray(TestNatives test)
        {
            var arr = new[] {true, false, true};
            
            if (1 != test.InOutBoolArray(arr, out var b, 3) || !arr.SequenceEqual(b))
                throw new InvalidOperationException("arg mismatch");
        }
        private static void TestInOutFloatArray(TestNatives test)
        {
            var arr = new[] {5.4544f, 881.121f, 7789.1123f};
            
            if (1 != test.InOutFloatArray(arr, out var b, 3) || !arr.SequenceEqual(b))
                throw new InvalidOperationException("arg mismatch");
        }

        private void RunTests(INativeObjectProxyFactory factory)
        {
            var testNatives = (TestNatives)factory.CreateInstance(typeof(TestNatives));

            var tests = new Expression<Action<TestNatives>>[]
            {
                x => TestInOutInt(x),
                x => TestInRefOutInt(x),
                x => TestInOutString(x),
                x => TestInOutIntArray(x),
                x => TestInOutBoolArray(x),
                x => TestInOutFloatArray(x),
                x => TestInIdOutInt(x),
            };

            foreach (var test in tests)
            {
                var result = "ok";
                try
                {
                    test.Compile()(testNatives);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                Console.WriteLine($"Test {((MethodCallExpression)(test.Body)).Method.Name}... {result}");
            }
        }

        private void ThreadingTest(TestingFastNative fastProxy, TestingFastNative handleProxy)
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
        }

        [Event]
        public void OnGameModeInit(IWorldService worldService, IEntityManager entityManager,
            INativeObjectProxyFactory proxyFactory, ITimerService timerService)
        {
            // Only test FastNative performance if FastNative is not activated
            if (proxyFactory is FastNativeBasedNativeObjectProxyFactory)
                return;
            
            // Benchmarking
            var fastFactory = new FastNativeBasedNativeObjectProxyFactory(_client);
            var fastProxy = (TestingFastNative) fastFactory.CreateInstance(typeof(TestingFastNative));
            _fastProxy = fastProxy;
            _nativeGetVehicleParamsEx = Interop.FastNativeFind("GetVehicleParamsEx");
            _testVehicleId = worldService.CreateVehicle(VehicleModelType.BMX, Vector3.One, 0, 0, 0).Entity.Handle;
            // disabled: timerService.Start(_ => BenchmarkRunTimer(), TimeSpan.FromSeconds(2))
            // disabled: timerService.Start(_ => BenchmarkRunTimerProxy(), TimeSpan.FromSeconds(2))
            
            // Test native features
            Console.WriteLine("TEST WITH HANDLE FACTORY:");
            RunTests(proxyFactory);
            Console.WriteLine("TEST WITH FAST FACTORY:");
            RunTests(fastFactory);

            // Threading test
            // disabled: timerService.Start(_ => ThreadingTest(fastProxy, handleProxy), TimeSpan.FromSeconds(15))

            // Multiple calls test
           InvokeVehicleNatives(entityManager, fastProxy);
        }
        
        private static void InvokeVehicleNatives(IEntityManager entityManager, TestingFastNative fastProxy)
        {
            // Call CreateVehicle native
            var testPosition = new Vector3(65.13f, 123.123f, 555.555f);
            var fastVehicleId = fastProxy.CreateVehicle((int) VehicleModelType.Landstalker, testPosition.X,
                testPosition.Y, testPosition.Z, 15, -1, -1, -1, 0);

            // Create Vehicle entity to verify vehicle position was set properly through the native
            var entity = SampEntities.GetVehicleId(fastVehicleId);
            entityManager.Create(entity);

            entityManager.AddComponent<NativeVehicle>(entity);
            var fastVehicleComp = entityManager.AddComponent<Vehicle>(entity);

            Console.WriteLine(
                $"Created vehicle {fastVehicleId} position {fastVehicleComp.Position}; matches? {(fastVehicleComp.Position == testPosition)}");

            // Call GetVehiclePos
            var ret = fastProxy.GetVehiclePos(fastVehicleId, out var x, out var y, out var z);
            var getPos = new Vector3(x, y, z);
            Console.WriteLine($"get pos ({ret}): {getPos} matches? {(testPosition == getPos)}");
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
                for (var j = 0; j < 8; j++) // set points for all 8 args
                {
                    data[j] = (int) (IntPtr) (ptData + 8 + j);
                }

                data[8] = id;

                Interop.FastNativeInvoke(native, "dRRRRRRR", ptData);
            }
        }

        public class TestingFastNative
        {
            [NativeMethod]
            public virtual int IsPlayerConnected(int id)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int CreateVehicle(int type, float x, float y, float z, float r, int color1, int color2,
                int respawnDelay, int hasSiren)
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
            public virtual int GetVehicleParamsEx(int vehicleid, out int a1, out int a2, out int a3, out int a4,
                out int a5, out int a6, out int a7)
            {
                throw new NativeNotImplementedException();
            }
        }

        [NativeObjectIdentifiers(nameof(Identifier))]
        public class TestNatives
        {
            public int Identifier { get; set; }

            [NativeMethod(true, Function = "sampsharptest_inout")]
            public virtual int InOutInt(int a)
            {
                throw new NativeNotImplementedException();
            }
            
            [NativeMethod(true, Function = "sampsharptest_inrefout")]
            public virtual int InRefOutInt(int a, out int b)
            {
                throw new NativeNotImplementedException();
            }
            
            [NativeMethod(Function = "sampsharptest_inrefout")]
            public virtual int InIdOutRef(out int b)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod(true, 2, Function = "sampsharptest_inoutstr")]
            public virtual int InOutString(string a, out string b, int blen)
            {
                throw new NativeNotImplementedException();
            }
            
            [NativeMethod(true, 2, 2, Function = "sampsharptest_inoutarr")]
            public virtual int InOutIntArray(int[] a, out int[] b, int ablen)
            {
                throw new NativeNotImplementedException();
            }
            [NativeMethod(true, 2, 2, Function = "sampsharptest_inoutarr")]
            public virtual int InOutBoolArray(bool[] a, out bool[] b, int ablen)
            {
                throw new NativeNotImplementedException();
            }
            [NativeMethod(true, 2, 2, Function = "sampsharptest_inoutarr")]
            public virtual int InOutFloatArray(float[] a, out float[] b, int ablen)
            {
                throw new NativeNotImplementedException();
            }
        }
    }

}
