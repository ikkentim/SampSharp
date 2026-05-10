using System;
using System.Linq;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

/// <summary>
/// Tests for StringParser, which consumes all remaining input text.
/// </summary>
public class StringParserTests
{
    private readonly StringParser _parser = new();
    private readonly Mock<IServiceProvider> _services = new();

    [Fact]
    public void TryParse_SimpleString_ConsumesAll()
    {
        var input = StringSpan.For("hello");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello");
        input.Length.ShouldBe(0);
    }

    [Fact]
    public void TryParse_MultipleWords_ConsumesAll()
    {
        var input = StringSpan.For("hello world test");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello world test");
        input.Length.ShouldBe(0);
    }

    [Fact]
    public void TryParse_LeadingWhitespace_SkipsAndConsumesAll()
    {
        var input = StringSpan.For("   hello world");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello world");
        input.Length.ShouldBe(0);
    }

    [Fact]
    public void TryParse_EmptyInput_ReturnsFalse()
    {
        var input = StringSpan.For("");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeFalse();
        parsed.ShouldBeNull();
    }

    [Fact]
    public void TryParse_OnlyWhitespace_ReturnsFalse()
    {
        var input = StringSpan.For("   \t  ");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeFalse();
    }

    [Fact]
    public void TryParse_SpecialCharacters_PreservedInOutput()
    {
        var input = StringSpan.For("hello@world!#test");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello@world!#test");
    }

    [Fact]
    public void TryParse_Numbers_ParsedAsString()
    {
        var input = StringSpan.For("123 456 789");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("123 456 789");
    }

    [Fact]
    public void TryParse_SingleCharacter_ParsesCorrectly()
    {
        var input = StringSpan.For("a");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("a");
    }

    [Fact]
    public void TryParse_VeryLongString_ParsesCorrectly()
    {
        var longText = string.Concat(Enumerable.Range(0, 1000).Select(i => $"word{i} "));
        var input = StringSpan.For(longText);
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe(longText);
    }

    [Fact]
    public void TryParse_WithTrailingWhitespace_Consumed()
    {
        var input = StringSpan.For("hello world   ");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello world   ");
    }

    [Fact]
    public void TryParse_PreservesInternalWhitespace()
    {
        var input = StringSpan.For("  a   b  c  ");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        // After TrimStart, leading spaces removed, then all remaining consumed
        parsed.ShouldBe("a   b  c  ");
    }
}
