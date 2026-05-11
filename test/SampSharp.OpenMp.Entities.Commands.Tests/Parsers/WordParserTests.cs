using System;
using Moq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Parsers;

/// <summary>
/// Tests for WordParser, which consumes the next whitespace-delimited word.
/// </summary>
public class WordParserTests
{
    private readonly WordParser _parser = new();
    private readonly Mock<IServiceProvider> _services = new();

    [Fact]
    public void TryParse_SingleWord_ParsesCorrectly()
    {
        var input = StringSpan.For("hello");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello");
        input.Length.ShouldBe(0);
    }

    [Fact]
    public void TryParse_MultipleWords_ParsesFirstWord()
    {
        var input = StringSpan.For("hello world");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello");
        input.ToString().ShouldBe(" world");
    }

    [Fact]
    public void TryParse_LeadingWhitespace_SkipsAndParsesWord()
    {
        var input = StringSpan.For("   hello world");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello");
    }

    [Fact]
    public void TryParse_TabDelimited_ParsesCorrectly()
    {
        // Note: WordParser treats tabs as regular characters, not as delimiters
        var input = StringSpan.For("hello\tworld");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        // Tabs are not treated as delimiters, so the entire string becomes one word
        parsed.ShouldBe("hello\tworld");
    }

    [Fact]
    public void TryParse_MixedWhitespace_ParsesCorrectly()
    {
        var input = StringSpan.For("hello \t world");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello");
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
    public void TryParse_SpecialCharacters_ParsesAsWord()
    {
        var input = StringSpan.For("hello-world test");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("hello-world");
    }

    [Fact]
    public void TryParse_Numbers_ParsesCorrectly()
    {
        var input = StringSpan.For("123 456");
        var result = _parser.TryParse(_services.Object, ref input, out var parsed);

        result.ShouldBeTrue();
        parsed.ShouldBe("123");
    }

    [Fact]
    public void TryParse_UpdatesInputSpan()
    {
        var input = StringSpan.For("hello world test");

        _parser.TryParse(_services.Object, ref input, out _);
        var remaining = input.ToString();

        remaining.ShouldBe(" world test");
    }

    [Fact]
    public void TryParse_ChainedCalls()
    {
        var input = StringSpan.For("one two three");

        _parser.TryParse(_services.Object, ref input, out var first);
        first.ShouldBe("one");

        _parser.TryParse(_services.Object, ref input, out var second);
        second.ShouldBe("two");

        _parser.TryParse(_services.Object, ref input, out var third);
        third.ShouldBe("three");
    }
}
