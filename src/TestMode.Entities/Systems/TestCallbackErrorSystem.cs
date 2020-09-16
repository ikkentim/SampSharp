using System;
using System.Collections.Generic;
using System.Text;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Entities;

namespace TestMode.Entities.Systems
{
    public class TestCallbackErrorSystem : ISystem
    {
        [Event]
        public void TestCallback(int a, int b)
        {
            Console.WriteLine($"TEST CB: {a} {b}");
        }
        
        [Event]
        public void OnGameModeInit(INativeProxy<TestNatives> m)
        {
            Console.WriteLine("About to call a remote func!");
            m.Instance.CallRemoteFunction("TestCallback", "ddd", 1, 2, 3);
        }

        public class TestNatives
        {
            [NativeMethod]
            public virtual void CallRemoteFunction(string name, string format, int a, int b, int c)
            {
                throw new NativeNotImplementedException();
            }
        }
    }

}
