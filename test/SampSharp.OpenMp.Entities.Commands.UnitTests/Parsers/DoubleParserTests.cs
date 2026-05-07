using System;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

public class DoubleParserTests
{
    private readonly Mock<IServiceProvider> _services = new();
    private readonly DoubleParser _doubleParser = new();

    [Fact]
    public void DoubleParser_ValidDouble_ParsesCorrectly()
    {
        var input = StringSpan.For("123.456");
        var result = _doubleParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        ((double)parsed!).ShouldBe(123.456d, 0.001d);
    }

    [Fact]
    public void DoubleParser_Integer_ParsesCorrectly()
    {
        var input = StringSpan.For("123");
        var result = _doubleParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        ((double)parsed!).ShouldBe(123d);
    }

    [Fact]
    public void DoubleParser_VeryLargePrecision_ParsesCorrectly()
    {
        var input = StringSpan.For("1.23456789012345");
        var result = _doubleParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        ((double)parsed!).ShouldBe(1.23456789012345d, 0.0000000001d);
    }

    [Fact]
    public void DoubleParser_NonNumericText_ReturnsFalse()
    {
        var input = StringSpan.For("hello");
        var result = _doubleParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeFalse();
    }
}
