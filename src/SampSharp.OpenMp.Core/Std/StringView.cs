using System.Runtime.InteropServices;
using System.Text;

namespace SampSharp.OpenMp.Core.Std;

/// <summary>
/// Represents a view of a string which is represented in memory like an <c>std::basic_string_view</c> from the C++
/// standard library.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct StringView : ISpanFormattable
{
    internal readonly byte* _reference;
    private readonly Size _size;

    /// <summary>
    /// Initializes a new instance of the <see cref="StringView" /> struct.
    /// </summary>
    /// <param name="data">A pointer to the underlying sequence.</param>
    /// <param name="size">The number of characters.</param>
    public StringView(byte* data, Size size)
    {
        _reference = data;
        _size = size;
    }

    /// <summary>
    /// Converts this view to a read-only span of bytes.
    /// </summary>
    /// <returns>The readonly span of bytes.</returns>
    public ReadOnlySpan<byte> AsSpan()
    {
        return new ReadOnlySpan<byte>(_reference, _size.Value.ToInt32());
    }

    /// <summary>
    /// Converts this view to a <see langword="string" /> using <see cref="StringViewMarshaller.Encoding"/>
    /// (UTF-8 by default, can be overridden at startup).
    /// </summary>
    /// <returns>The converted string.</returns>
    public override string? ToString()
    {
        return _reference == null ? null : StringViewMarshaller.Encoding.GetString(AsSpan());
    }

    /// <summary>
    /// Converts this view to a <see langword="string" /> using UTF-8 encoding.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns></returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString() ?? string.Empty;
    }


    /// <summary>Tries to format the value of this string view into the provided span of characters. The string view is
    /// converted using UTF-8 encoding.</summary>
    /// <param name="destination">The span in which to write this instance's value formatted as a span of
    /// characters.</param>
    /// <param name="charsWritten">When this method returns, contains the number of characters that were written in
    /// <paramref name="destination" />.</param>
    /// <param name="format">A span containing the characters that represent a standard or custom format string that
    /// defines the acceptable format for <paramref name="destination" />.</param>
    /// <param name="provider">An optional object that supplies culture-specific formatting information for <paramref
    /// name="destination" />.</param>
    /// <returns>
    /// <see langword="true" /> if the formatting was successful; otherwise, <see langword="false" />.</returns>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return StringViewMarshaller.Encoding.TryGetChars(AsSpan(), destination, out charsWritten);
    }

    /// <summary>
    /// Converts a <see cref="StringView" /> to a <see cref="string" /> using UTF-8 encoding.
    /// </summary>
    /// <param name="view"></param>
    public static implicit operator string?(StringView view)
    {
        return view.ToString();
    }
}