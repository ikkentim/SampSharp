using System;
using Moq;
using SampSharp.Entities.SAMP.Commands;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

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