using System;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

/// <summary>
/// Tests for numeric parsers: IntParser, FloatParser, DoubleParser.
/// </summary>
public class NumericParserTests
{
    private readonly Mock<IServiceProvider> _services = new();

    #region IntParser Tests

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
        // Parser leaves the space; it's up to the caller to trim if needed
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

    #endregion

    #region FloatParser Tests

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

    #endregion

    #region DoubleParser Tests

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

    #endregion
}
