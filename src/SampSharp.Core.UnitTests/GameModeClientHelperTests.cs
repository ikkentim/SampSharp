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
using System.Text;
using Moq;
using SampSharp.Core.Callbacks;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests;

public class GameModeClientHelperTests
{
    [Fact]
    public unsafe void RegisterCallbacksInObject_should_register_all_callbacks()
    {
        // arrange
        var args = new[] { 0 };
        var result1 = 0;
        var result2 = 0;
            
        var sut = new HostedGameModeClient(Mock.Of<IGameModeProvider>(), Encoding.ASCII);
        sut.InitializeForTesting();

        // act
        sut.RegisterCallbacksInObject(this);
            
        fixed (int* argsPtr = args)
        {
            sut.PublicCall(IntPtr.Zero, "OnTesting1", (IntPtr)argsPtr, (IntPtr)(&result1));
            sut.PublicCall(IntPtr.Zero, "OnTesting2", (IntPtr)argsPtr, (IntPtr)(&result2));
        }

        // assert
        result1.ShouldBe(321);
        result2.ShouldBe(123);
    }
        
    [Callback]
    public int OnTesting1() => 321;
        
    [Callback]
    public int OnTesting2() => 123;
}