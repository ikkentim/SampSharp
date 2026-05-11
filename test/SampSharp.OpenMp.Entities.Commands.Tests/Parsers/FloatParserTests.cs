using System;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

public class FloatParserTests
{
    private readonly Mock<IServiceProvider> _services = new();
    private readonly FloatParser _floatParser = new();

    [Fact]
    public void FloatParser_ValidFloat_ParsesCorrectly()
    {
        var input = StringSpan.For("123.456");
        var result = _floatParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        ((float)parsed!).ShouldBe(123.456f, 0.001f);
    }

    [Fact]
    public void FloatParser_Integer_ParsesCorrectly()
    {
        var input = StringSpan.For("123");
        var result = _floatParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        ((float)parsed!).ShouldBe(123f);
    }

    [Fact]
    public void FloatParser_NegativeFloat_ParsesCorrectly()
    {
        var input = StringSpan.For("-123.456");
        var result = _floatParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        ((float)parsed!).ShouldBe(-123.456f, 0.001f);
    }

    [Fact]
    public void FloatParser_ScientificNotation_ParsesCorrectly()
    {
        var input = StringSpan.For("1.23e2");
        var result = _floatParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        ((float)parsed!).ShouldBe(1.23e2f, 0.1f);
    }

    [Fact]
    public void FloatParser_NonNumericText_ReturnsFalse()
    {
        var input = StringSpan.For("hello");
        var result = _floatParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeFalse();
    }
}
