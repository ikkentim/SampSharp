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