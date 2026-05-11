using System;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

public class IntParserTests
{
    private readonly Mock<IServiceProvider> _services = new();
    private readonly IntParser _intParser = new();

    [Fact]
    public void IntParser_ValidInteger_ParsesCorrectly()
    {
        var input = StringSpan.For("123");
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe(123);
        input.Length.ShouldBe(0);
    }

    [Fact]
    public void IntParser_NegativeInteger_ParsesCorrectly()
    {
        var input = StringSpan.For("-456");
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe(-456);
    }

    [Fact]
    public void IntParser_Zero_ParsesCorrectly()
    {
        var input = StringSpan.For("0");
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe(0);
    }

    [Fact]
    public void IntParser_IntegerWithLeadingWhitespace_ParsesCorrectly()
    {
        var input = StringSpan.For("   123");
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe(123);
    }

    [Fact]
    public void IntParser_IntegerFollowedByText_ParsesInteger()
    {
        var input = StringSpan.For("123 rest");
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe(123);
        input.ToString().ShouldBe(" rest");
    }

    [Fact]
    public void IntParser_FloatingPoint_ReturnsFalse()
    {
        var input = StringSpan.For("123.456");
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeFalse();
        parsed.ShouldBeNull();
    }

    [Fact]
    public void IntParser_NonNumericText_ReturnsFalse()
    {
        var input = StringSpan.For("hello");
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeFalse();
    }

    [Fact]
    public void IntParser_EmptyInput_ReturnsFalse()
    {
        var input = StringSpan.For("");
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeFalse();
    }

    [Fact]
    public void IntParser_MaxValue_ParsesCorrectly()
    {
        var input = StringSpan.For(int.MaxValue.ToString());
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe(int.MaxValue);
    }

    [Fact]
    public void IntParser_MinValue_ParsesCorrectly()
    {
        var input = StringSpan.For(int.MinValue.ToString());
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe(int.MinValue);
    }

    [Fact]
    public void IntParser_OverflowValue_ReturnsFalse()
    {
        var input = StringSpan.For("99999999999999999999");
        var result = _intParser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeFalse();
    }
}
