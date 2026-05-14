using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Std;

/// <summary>
/// Represents a span which is represented in memory like an <c>std::span_t</c> from the C++ standard library.
/// </summary>
/// <typeparam name="T"></typeparam>
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct SpanLite<T> where T : unmanaged
{
    private readonly T* _data;
    private readonly Size _size;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpanLite{T}" /> struct.
    /// </summary>
    /// <param name="data">A pointer to the underlying sequence.</param>
    /// <param name="size">The number of elements.</param>
    public SpanLite(T* data, Size size)
    {
        _data = data;
        _size = size;
    }

    /// <summary>
    /// Converts the span to a <see cref="Span{T}" />.
    /// </summary>
    /// <returns>The converted span.</returns>
    public Span<T> AsSpan()
    {
        return new Span<T>(_data, _size.Value.ToInt32());
    }
}