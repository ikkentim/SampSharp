using System;
using SampSharp.Core.Callbacks;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests.Callbacks;

public unsafe class CallbackParameterIntTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void GetValue_should_succeed(int value)
    {
        var sut = new CallbackParameterInt();

        var result = sut.GetValue(IntPtr.Zero, (IntPtr)(&value));

        result.ShouldBe(value);
    }
}