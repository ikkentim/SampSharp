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
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests;

public class HostedGameModeClientTests
{
    [Fact]
    public unsafe void PublicCall_should_invoke_registered_callback()
    {
        // arrange
        var handler = () => 321;

        var args = new[] { 0 };
        var result = 0;

        var sut = new HostedGameModeClient(Mock.Of<IGameModeProvider>(), Encoding.ASCII);
        sut.InitializeForTesting();

        // act
        sut.RegisterCallback("OnTesting", handler.Target, handler.Method);

        fixed (int* argsPtr = args)
        {
            sut.PublicCall(IntPtr.Zero, "OnTesting", (IntPtr)argsPtr, (IntPtr)(&result));
        }

        // assert
        result.ShouldBe(321);
    }

    [Fact]
    public void RegisterCallback_should_throw_when_game_mode_not_running()
    {
        // arrange
        var handler = () => { };

        var sut = new HostedGameModeClient(Mock.Of<IGameModeProvider>(), Encoding.ASCII);

        // act
        Should.Throw<GameModeNotRunningException>(() => sut.RegisterCallback("OnTesting", handler.Target, handler.Method));
    }
}