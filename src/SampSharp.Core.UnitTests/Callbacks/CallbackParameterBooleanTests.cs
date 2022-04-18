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
using SampSharp.Core.Callbacks;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests.Callbacks;

public unsafe class CallbackParameterBooleanTests
{
    [Theory]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(-1, true)]
    [InlineData(0, false)]
    public void GetValue_should_succeed(int value, bool expectedResult)
    {
        var sut = new CallbackParameterBoolean();

        var result = sut.GetValue(IntPtr.Zero, (IntPtr)(&value));

        result.ShouldBe(expectedResult);
    }
}