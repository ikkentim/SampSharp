using System;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for the StringSpan zero-allocation string slicing utility.
/// </summary>
public class StringSpanTests
{
    [Fact]
    public void For_CreatesSpanForEntireString()
    {
        var span = StringSpan.For("hello world");
        span.ToString().ShouldBe("hello world");
        span.Length.ShouldBe(11);
    }

    [Fact]
    public void For_EmptyString_CreatesEmptySpan()
    {
        var span = StringSpan.For("");
        span.Length.ShouldBe(0);
        span.ToString().ShouldBe("");
    }

    [Fact]
    public void Empty_ReturnsEmptySpan()
    {
        var span = StringSpan.Empty;
        span.Length.ShouldBe(0);
        span.ToString().ShouldBe("");
    }

    [Fact]
    public void Take_CreatesSpanWithSpecifiedLength()
    {
        var span = StringSpan.For("hello world").Take(5);
        span.ToString().ShouldBe("hello");
        span.Length.ShouldBe(5);
    }

    [Fact]
    public void Take_FullLength_ReturnsSameSpan()
    {
        var original = StringSpan.For("hello");
        var span = original.Take(5);
        span.ToString().ShouldBe("hello");
    }

    [Fact]
    public void Take_Zero_ReturnsEmptySpan()
    {
        var span = StringSpan.For("hello").Take(0);
        span.Length.ShouldBe(0);
        span.ToString().ShouldBe("");
    }

    [Fact]
    public void Take_ExceedingLength_ThrowsArgumentOutOfRangeException()
    {
        var span = StringSpan.For("hello");
        var ex = Should.Throw<ArgumentOutOfRangeException>(() => span.Take(10));
        ex.ParamName.ShouldBe("length");
    }

    [Fact]
    public void Take_NegativeLength_ThrowsArgumentOutOfRangeException()
    {
        var span = StringSpan.For("hello");
        Should.Throw<ArgumentOutOfRangeException>(() => span.Take(-1));
    }

    [Fact]
    public void Skip_SkipsSpecifiedNumberOfCharacters()
    {
        var span = StringSpan.For("hello world").Skip(6);
        span.ToString().ShouldBe("world");
        span.Length.ShouldBe(5);
    }

    [Fact]
    public void Skip_Zero_ReturnsSameContent()
    {
        var span = StringSpan.For("hello").Skip(0);
        span.ToString().ShouldBe("hello");
    }

    [Fact]
    public void Skip_FullLength_ReturnsEmptySpan()
    {
        var span = StringSpan.For("hello").Skip(5);
        span.Length.ShouldBe(0);
        span.ToString().ShouldBe("");
    }

    [Fact]
    public void Skip_ExceedingLength_ThrowsArgumentOutOfRangeException()
    {
        var span = StringSpan.For("hello");
        Should.Throw<ArgumentOutOfRangeException>(() => span.Skip(10));
    }

    [Fact]
    public void Skip_NegativeCount_ThrowsArgumentOutOfRangeException()
    {
        var span = StringSpan.For("hello");
        Should.Throw<ArgumentOutOfRangeException>(() => span.Skip(-1));
    }

    [Fact]
    public void TrimStart_RemovesLeadingWhitespace()
    {
        var span = StringSpan.For("   hello world").TrimStart();
        span.ToString().ShouldBe("hello world");
    }

    [Fact]
    public void TrimStart_NoLeadingWhitespace_ReturnsSame()
    {
        var span = StringSpan.For("hello world").TrimStart();
        span.ToString().ShouldBe("hello world");
    }

    [Fact]
    public void TrimStart_AllWhitespace_ReturnsEmpty()
    {
        var span = StringSpan.For("   \t  ").TrimStart();
        span.Length.ShouldBe(0);
    }

    [Fact]
    public void TrimStart_VariousWhitespace_RemovesAll()
    {
        var span = StringSpan.For(" \t\n  hello").TrimStart();
        span.ToString().ShouldBe("hello");
    }

    [Fact]
    public void AsSpan_ReturnReadOnlySpan()
    {
        var span = StringSpan.For("hello");
        var readOnlySpan = span.AsSpan();
        readOnlySpan.ToString().ShouldBe("hello");
    }

    [Fact]
    public void ImplicitConversionToReadOnlySpan()
    {
        StringSpan span = StringSpan.For("hello");
        ReadOnlySpan<char> readOnlySpan = span;
        readOnlySpan.ToString().ShouldBe("hello");
    }

    [Fact]
    public void Indexer_ReturnsCharacterAtIndex()
    {
        var span = StringSpan.For("hello");
        span[0].ShouldBe('h');
        span[4].ShouldBe('o');
    }

    [Fact]
    public void Indexer_NegativeIndex_ThrowsIndexOutOfRangeException()
    {
        var span = StringSpan.For("hello");
        Should.Throw<IndexOutOfRangeException>(() => _ = span[-1]);
    }

    [Fact]
    public void Indexer_ExceedingIndex_ThrowsIndexOutOfRangeException()
    {
        var span = StringSpan.For("hello");
        Should.Throw<IndexOutOfRangeException>(() => _ = span[10]);
    }

    [Fact]
    public void GetOffsetAndLength_ReturnsCorrectOffsetAndLength()
    {
        var span = StringSpan.For("hello world").Skip(6);
        var (offset, length) = span.GetOffsetAndLength();
        offset.ShouldBe(6);
        length.ShouldBe(5);
    }

    [Fact]
    public void Equals_StringSpan_WithEqualSpan_ReturnsTrue()
    {
        var span1 = StringSpan.For("hello");
        var span2 = StringSpan.For("hello");
        span1.Equals(span2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_StringSpan_WithDifferentSpan_ReturnsFalse()
    {
        var span1 = StringSpan.For("hello");
        var span2 = StringSpan.For("world");
        span1.Equals(span2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_String_WithEqualString_ReturnsTrue()
    {
        var span = StringSpan.For("hello");
        span.Equals("hello").ShouldBeTrue();
    }

    [Fact]
    public void Equals_String_WithDifferentString_ReturnsFalse()
    {
        var span = StringSpan.For("hello");
        span.Equals("world").ShouldBeFalse();
    }

    [Fact]
    public void Equals_NullString_ReturnsFalse()
    {
        var span = StringSpan.For("hello");
        span.Equals((string?)null).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_SameSpan_ReturnsSameHashCode()
    {
        var span1 = StringSpan.For("hello");
        var span2 = StringSpan.For("hello");
        span1.GetHashCode().ShouldBe(span2.GetHashCode());
    }

    [Fact]
    public void ToString_ReturnsStringContent()
    {
        var span = StringSpan.For("hello world");
        span.ToString().ShouldBe("hello world");
    }

    [Fact]
    public void ChainedOperations_TakeAndSkip()
    {
        var span = StringSpan.For("hello world").Skip(6).Take(5);
        span.ToString().ShouldBe("world");
    }

    [Fact]
    public void ChainedOperations_SkipAndTrimStart()
    {
        var span = StringSpan.For("hello   world").Skip(5).TrimStart();
        span.ToString().ShouldBe("world");
    }
}
