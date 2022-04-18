// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Entities;

namespace TestMode.Entities.Systems.IssueTests;

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