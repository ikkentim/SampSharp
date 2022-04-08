using System;
using SampSharp.Core.Callbacks;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests.Callbacks;

public unsafe class CallbackParameterSingleTests
{
    [Theory]
    [InlineData(1f)]
    [InlineData(1.23f)]
    [InlineData(0f)]
    [InlineData(float.MaxValue)]
    [InlineData(float.MinValue)]
    public void GetValue_should_succeed(float value)
    {
        var sut = new CallbackParameterSingle();

        var result = sut.GetValue(IntPtr.Zero, (IntPtr)(&value));

        result.ShouldBe(value);
    }
}