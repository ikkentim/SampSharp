using System.Collections;
using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.RobinHood;

/// <summary>
/// Represent a pointer to an <c>robin_hood::unordered_flat_set</c> of <see cref="StringView" /> values.  <c>robin_hood::unordered_flat_set</c> is part of the <c>robin_hood</c> C++
/// library.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct FlatHashSetStringView : IReadOnlyCollection<string?>
{
    private readonly nint _data;

    /// <summary>
    /// Gets the number of elements in this set.
    /// </summary>
    public int Count => RobinHoodInterop.FlatHashSetStringView_size(_data).Value.ToInt32();

    /// <inheritdoc />
    public IEnumerator<string?> GetEnumerator()
    {
        var iter = RobinHoodInterop.FlatHashSetStringView_begin(_data);

        while (iter != RobinHoodInterop.FlatHashSetStringView_end(_data))
        {
            yield return iter.Get<StringView>().ToString();
            iter = RobinHoodInterop.FlatHashSetStringView_inc(iter);
        }
    }

    /// <summary>
    /// Adds a value to this set.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public void Emplace(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        scoped StringViewMarshaller.ManagedToNative valueMarshaller = new();

        try
        {
            Span<byte> buffer = stackalloc byte[StringViewMarshaller.ManagedToNative.BufferSize];
            valueMarshaller.FromManaged(value, buffer);
            var valueMarshalled = valueMarshaller.ToUnmanaged();

            RobinHoodInterop.FlatHashSetStringView_emplace(_data, valueMarshalled);
        }
        finally
        {
            valueMarshaller.Free();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}