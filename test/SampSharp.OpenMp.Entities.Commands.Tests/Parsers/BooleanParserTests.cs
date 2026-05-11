using System;
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