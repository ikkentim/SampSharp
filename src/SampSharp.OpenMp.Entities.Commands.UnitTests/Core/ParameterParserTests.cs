using System;
using Moq;
using Shouldly;
using Xunit;

namespace SampSharp.Entities.SAMP.Commands.Tests.Core;

public class ParameterParserTests
{
    private readonly Mock<IServiceProvider> _mockServices = new();

    public ParameterParserTests()
    {
        _mockServices = new Mock<IServiceProvider>();
    }

    [Fact]
    public void IntParser_ValidNumber_ParsesSuccessfully()
    {
        var parser = new IntParser();
        var input = "42";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe(42);
    }

    [Fact]
    public void BooleanParser_TrueValue_ParsesSuccessfully()
    {
        var parser = new BooleanParser();
        var input = "true";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe(true);
    }

    [Fact]
    public void BooleanParser_FalseValue_ParsesSuccessfully()
    {
        var parser = new BooleanParser();
        var input = "false";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe(false);
    }

    [Fact]
    public void BooleanParser_InvalidValue_ReturnsFalse()
    {
        var parser = new BooleanParser();
        var input = "invalid";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeFalse();
    }

    [Fact]
    public void StringParser_AnyText_ParsesSuccessfully()
    {
        var parser = new StringParser();
        var input = "hello world";
        var result = parser.TryParse(_mockServices.Object, ref input, out var value);
        result.ShouldBeTrue();
        value.ShouldBe("hello world");
    }
}