using System;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Entities;

namespace TestMode.Entities.Systems.IssueTests
{
    public class Issue363ReproServerCrash : ISystem
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
            // TODO: This can crash; need to investigate.
            m.Instance.CallRemoteFunction("TestCallback", "dd", 1, 2);
        }

        public class TestNatives
        {
            [NativeMethod(ReferenceIndices = new[] {2, 3})]
            public virtual void CallRemoteFunction(string name, string format, int a, int b)
            {
                throw new NativeNotImplementedException();
            }
        }
    }

}
