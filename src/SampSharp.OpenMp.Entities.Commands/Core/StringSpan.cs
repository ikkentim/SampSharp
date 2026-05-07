using System.Diagnostics;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a span of characters within a string.
/// </summary>
[DebuggerDisplay("{DebugString}")]
public readonly struct StringSpan : IEquatable<string>, IEquatable<StringSpan>
{
    private readonly string? _string;

    /// <summary>
    /// Initializes a new instance of the <see cref="StringSpan"/> struct.
    /// </summary>
    /// <param name="string">The underlying string.</param>
    /// <param name="range">The range of characters within the string.</param>
    private StringSpan(string @string, Range range)
    {
        _string = @string;
        Range = range;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is StringSpan other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_string, Range);
    }

    /// <summary>
    /// Gets an empty <see cref="StringSpan"/>.
    /// </summary>
    public static readonly StringSpan Empty = new();

    /// <summary>
    /// Gets the range of characters within the string.
    /// </summary>
    private Range Range { get; }

    /// <summary>
    /// Gets the underlying string.
    /// </summary>
    public string String => _string ?? string.Empty;

    internal string DebugString => ToString();

    /// <summary>
    /// Gets the offset of the span from the start of the source text and the length of the span.
    /// </summary>
    /// <returns>The offset and length of the span.</returns>
    public (int offset, int length) GetOffsetAndLength()
    {
        return Range.GetOffsetAndLength(String.Length);
    }

    /// <summary>
    /// Creates a <see cref="StringSpan"/> that represents the entire string.
    /// </summary>
    /// <param name="string">The underlying string.</param>
    /// <returns>A <see cref="StringSpan"/> that represents the entire string.</returns>
    public static StringSpan For(string @string)
    {
        return new StringSpan(@string, Range.All);
    }

    /// <summary>
    /// Gets the length of the span.
    /// </summary>
    public int Length
    {
        get
        {
            var (_, inputLength) = GetOffsetAndLength();
            return inputLength;
        }
    }

    /// <summary>
    /// Gets the character at the specified index within the span.
    /// </summary>
    /// <param name="index">The index of the character.</param>
    /// <returns>The character at the specified index.</returns>
    public char this[int index] => AsSpan()[index];

    /// <summary>
    /// Creates a new <see cref="StringSpan"/> that represents a portion of the current span.
    /// </summary>
    /// <param name="length">The length of the new span.</param>
    /// <returns>A new <see cref="StringSpan"/> that represents a portion of the current span.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the length is negative or greater than the current span length.</exception>
    public StringSpan Take(int length)
    {
        if (length < 0 || length > Length)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        var newEnd = GetRangeFromStart(length);

        return new StringSpan(String, new Range(Range.Start, newEnd));
    }

    private Index GetRangeFromStart(int length)
    {
        var newEnd = Index.FromStart(Range.Start.Value + length);
        if (Range.Start.IsFromEnd)
        {
            newEnd = Index.FromEnd(Range.Start.Value - length);
        }

        return newEnd;
    }

    /// <summary>
    /// Creates a new <see cref="StringSpan"/> that skips the specified number of characters from the start.
    /// </summary>
    /// <param name="count">The number of characters to skip.</param>
    /// <returns>A new <see cref="StringSpan"/> that skips the specified number of characters from the start.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the count is negative or greater than the current span length.</exception>
    public StringSpan Skip(int count)
    {
        if (count < 0 || count > Length)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        if (count == 0)
        {
            return this;
        }

        var newStart = GetRangeFromStart(count);
        return new StringSpan(String, new Range(newStart, Range.End));
    }

    /// <summary>
    /// Creates a new <see cref="StringSpan"/> with leading whitespace removed.
    /// </summary>
    /// <returns>A new <see cref="StringSpan"/> with leading whitespace removed.</returns>
    public StringSpan TrimStart()
    {
        var span = AsSpan();
        var trimmedStart = 0;
        while (trimmedStart < span.Length && char.IsWhiteSpace(span[trimmedStart]))
        {
            trimmedStart++;
        }

        if (trimmedStart == 0)
        {
            return this;
        }

        if (trimmedStart >= Length)
        {
            return new StringSpan(String, new Range(Range.End, Range.End));
        }

        return Skip(trimmedStart);
    }

    /// <summary>
    /// Returns a read-only span of characters that represents the current span.
    /// </summary>
    /// <returns>A read-only span of characters that represents the current span.</returns>
    public ReadOnlySpan<char> AsSpan()
    {
        return String.AsSpan()[Range];
    }

    /// <summary>
    /// Implicitly converts a <see cref="StringSpan"/> to a read-only span of characters.
    /// </summary>
    /// <param name="span">The <see cref="StringSpan"/> to convert.</param>
    /// <returns>A read-only span of characters.</returns>
    public static implicit operator ReadOnlySpan<char>(StringSpan span)
    {
        return span.AsSpan();
    }

    /// <summary>
    /// Determines whether the current span is equal to the specified string.
    /// </summary>
    /// <param name="other">The string to compare.</param>
    /// <returns><c>true</c> if the current span is equal to the specified string; otherwise, <c>false</c>.</returns>
    public bool Equals(string? other)
    {
        return other != null && Equals((ReadOnlySpan<char>)other);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return AsSpan().ToString();
    }

    /// <summary>
    /// Determines whether the current span is equal to the specified read-only span of characters.
    /// </summary>
    /// <param name="other">The read-only span of characters to compare.</param>
    /// <returns><c>true</c> if the current span is equal to the specified read-only span of characters; otherwise, <c>false</c>.</returns>
    public bool Equals(ReadOnlySpan<char> other)
    {
        return AsSpan().SequenceEqual(other);
    }

    /// <summary>
    /// Determines whether the current span is equal to the specified <see cref="StringSpan"/>.
    /// </summary>
    /// <param name="other">The <see cref="StringSpan"/> to compare.</param>
    /// <returns><c>true</c> if the current span is equal to the specified <see cref="StringSpan"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(StringSpan other)
    {
        return Equals(other.AsSpan());
    }

    /// <summary>
    /// Determines whether two <see cref="StringSpan"/> instances are equal.
    /// </summary>
    /// <param name="span1">The first <see cref="StringSpan"/> to compare.</param>
    /// <param name="span2">The second <see cref="StringSpan"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="StringSpan"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(StringSpan span1, StringSpan span2)
    {
        return span1.Equals(span2);
    }

    /// <summary>
    /// Determines whether two <see cref="StringSpan"/> instances are not equal.
    /// </summary>
    /// <param name="span1">The first <see cref="StringSpan"/> to compare.</param>
    /// <param name="span2">The second <see cref="StringSpan"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="StringSpan"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(StringSpan span1, StringSpan span2)
    {
        return !span1.Equals(span2);
    }

    /// <summary>
    /// Determines whether a <see cref="StringSpan"/> instance is equal to a read-only span of characters.
    /// </summary>
    /// <param name="span1">The <see cref="StringSpan"/> to compare.</param>
    /// <param name="span2">The read-only span of characters to compare.</param>
    /// <returns><c>true</c> if the <see cref="StringSpan"/> instance is equal to the read-only span of characters; otherwise, <c>false</c>.</returns>
    public static bool operator ==(StringSpan span1, ReadOnlySpan<char> span2)
    {
        return span1.Equals(span2);
    }

    /// <summary>
    /// Determines whether a <see cref="StringSpan"/> instance is not equal to a read-only span of characters.
    /// </summary>
    /// <param name="span1">The <see cref="StringSpan"/> to compare.</param>
    /// <param name="span2">The read-only span of characters to compare.</param>
    /// <returns><c>true</c> if the <see cref="StringSpan"/> instance is not equal to the read-only span of characters; otherwise, <c>false</c>.</returns>
    public static bool operator !=(StringSpan span1, ReadOnlySpan<char> span2)
    {
        return !span1.Equals(span2);
    }

    /// <summary>
    /// Determines whether a read-only span of characters is equal to a <see cref="StringSpan"/> instance.
    /// </summary>
    /// <param name="span1">The read-only span of characters to compare.</param>
    /// <param name="span2">The <see cref="StringSpan"/> to compare.</param>
    /// <returns><c>true</c> if the read-only span of characters is equal to the <see cref="StringSpan"/> instance; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ReadOnlySpan<char> span1, StringSpan span2)
    {
        return span2.Equals(span1);
    }

    /// <summary>
    /// Determines whether a read-only span of characters is not equal to a <see cref="StringSpan"/> instance.
    /// </summary>
    /// <param name="span1">The read-only span of characters to compare.</param>
    /// <param name="span2">The <see cref="StringSpan"/> to compare.</param>
    /// <returns><c>true</c> if the read-only span of characters is not equal to the <see cref="StringSpan"/> instance; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ReadOnlySpan<char> span1, StringSpan span2)
    {
        return !span2.Equals(span1);
    }
}
