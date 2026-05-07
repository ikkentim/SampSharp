using System;
using System.Linq;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

/// <summary>
/// Tests for BooleanParser.
/// </summary>
public class BooleanParserTests
{
    private readonly BooleanParser _parser = new();
    private readonly Mock<IServiceProvider> _services = new();

    [Theory]
    [InlineData("true")]
    [InlineData("True")]
    [InlineData("TRUE")]
    [InlineData("1")]
    [InlineData("yes")]
    [InlineData("Yes")]
    [InlineData("on")]
    public void TryParse_TrueVariants_ParsesAsTrue(string input)
    {
        var span = StringSpan.For(input);
        var result = _parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(true);
    }

    [Theory]
    [InlineData("false")]
    [InlineData("False")]
    [InlineData("FALSE")]
    [InlineData("0")]
    [InlineData("no")]
    [InlineData("No")]
    [InlineData("off")]
    public void TryParse_FalseVariants_ParsesAsFalse(string input)
    {
        var span = StringSpan.For(input);
        var result = _parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(false);
    }

    [Fact]
    public void TryParse_TrueWithFollowingText_ParsesTrueOnly()
    {
        var span = StringSpan.For("true rest");
        var result = _parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(true);
        span.ToString().ShouldBe(" rest");
    }

    [Fact]
    public void TryParse_LeadingWhitespace_SkipsAndParses()
    {
        var span = StringSpan.For("   true");
        var result = _parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(true);
    }

    [Fact]
    public void TryParse_InvalidInput_ReturnsFalse()
    {
        var span = StringSpan.For("maybe");
        var result = _parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeFalse();
    }

    [Fact]
    public void TryParse_EmptyInput_ReturnsFalse()
    {
        var span = StringSpan.For("");
        var result = _parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeFalse();
    }

    [Fact]
    public void TryParse_Numeric_OnlyAccepts0And1()
    {
        var span1 = StringSpan.For("1");
        _parser.TryParse(_services.Object, ref span1, out var result1).ShouldBeTrue();
        result1.ShouldBe(true);

        var span0 = StringSpan.For("0");
        _parser.TryParse(_services.Object, ref span0, out var result0).ShouldBeTrue();
        result0.ShouldBe(false);

        var span2 = StringSpan.For("2");
        _parser.TryParse(_services.Object, ref span2, out _).ShouldBeFalse();
    }
}

/// <summary>
/// Tests for EnumParser.
/// </summary>
public class EnumParserTests
{
    private readonly Mock<IServiceProvider> _services = new();

    public enum TestEnum
    {
        Option1,
        Option2,
        LongOptionName
    }

    [Fact]
    public void TryParse_ExactEnumName_ParsesCorrectly()
    {
        var parser = new EnumParser(typeof(TestEnum));
        var span = StringSpan.For("Option1");
        
        var result = parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(TestEnum.Option1);
    }

    [Fact]
    public void TryParse_CaseInsensitive_ParsesCorrectly()
    {
        var parser = new EnumParser(typeof(TestEnum));
        var span = StringSpan.For("option1");
        
        var result = parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(TestEnum.Option1);
    }

    [Fact]
    public void TryParse_PartialMatch_ParsesCorrectly()
    {
        var parser = new EnumParser(typeof(TestEnum));
        var span = StringSpan.For("long");
        
        var result = parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(TestEnum.LongOptionName);
    }

    [Fact]
    public void TryParse_AmbiguousPartialMatch_ReturnsFalse()
    {
        var parser = new EnumParser(typeof(TestEnum));
        var span = StringSpan.For("Option");
        
        // "Option" matches both Option1 and Option2, should be ambiguous
        var result = parser.TryParse(_services.Object, ref span, out _);
        result.ShouldBeFalse();
    }

    [Fact]
    public void TryParse_InvalidValue_ReturnsFalse()
    {
        var parser = new EnumParser(typeof(TestEnum));
        var span = StringSpan.For("InvalidOption");
        
        var result = parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeFalse();
        parsed.ShouldBeNull();
    }

    [Fact]
    public void TryParse_EmptyInput_ReturnsFalse()
    {
        var parser = new EnumParser(typeof(TestEnum));
        var span = StringSpan.For("");
        
        var result = parser.TryParse(_services.Object, ref span, out _);
        
        result.ShouldBeFalse();
    }

    [Fact]
    public void TryParse_WithFollowingText_ParsesEnumOnly()
    {
        var parser = new EnumParser(typeof(TestEnum));
        var span = StringSpan.For("Option1 rest");
        
        var result = parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(TestEnum.Option1);
        span.ToString().ShouldBe(" rest");
    }

    [Fact]
    public void TryParse_LeadingWhitespace_SkipsAndParses()
    {
        var parser = new EnumParser(typeof(TestEnum));
        var span = StringSpan.For("   Option2");
        
        var result = parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(TestEnum.Option2);
    }

    [Fact]
    public void TryParse_NumericValue_ParsesIfValidOrdinal()
    {
        var parser = new EnumParser(typeof(TestEnum));
        var span = StringSpan.For("0");
        
        var result = parser.TryParse(_services.Object, ref span, out var parsed);
        
        result.ShouldBeTrue();
        parsed.ShouldBe(TestEnum.Option1);
    }
}
