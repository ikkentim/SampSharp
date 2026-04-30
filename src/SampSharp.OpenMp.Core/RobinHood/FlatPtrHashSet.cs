using System.Collections;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.RobinHood;

/// <summary>
/// Represent a pointer to an <c>robin_hood::unordered_flat_set</c> of open.mp interface pointers of type <typeparamref name="T" />. <c>robin_hood::unordered_flat_set</c> is part of the <c>robin_hood</c> C++ library.
/// </summary>
/// <typeparam name="T"></typeparam>
[StructLayout(LayoutKind.Sequential)]
public readonly struct FlatPtrHashSet<T> : IReadOnlyCollection<T> where T : unmanaged, IUnmanagedInterface
{
    private readonly nint _data;

    internal FlatPtrHashSet(IntPtr data)
    {
        _data = data;
    }

    /// <summary>
    /// Gets the number of elements in this set.
    /// </summary>
    public int Count => RobinHoodInterop.FlatPtrHashSet_size(_data).ToInt32();

    /// <summary>
    /// Returns an enumerator that iterates through the set.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    internal FlatPtrHashSetIterator Begin()
    {
        return RobinHoodInterop.FlatPtrHashSet_begin(_data);
    }

    internal FlatPtrHashSetIterator End()
    {
        return RobinHoodInterop.FlatPtrHashSet_end(_data);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Represents an enumerator for a <see cref="FlatPtrHashSet{T}" />.
    /// </summary>
    public struct Enumerator : IEnumerator<T>
    {
        private readonly FlatPtrHashSet<T> _set;
        private FlatPtrHashSetIterator? _iterator;

        internal Enumerator(FlatPtrHashSet<T> set)
        {
            _set = set;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            var end = _set.End();
            if (!_iterator.HasValue)
            {
                _iterator = _set.Begin();
                return _iterator != end;
            }

            if (_iterator == end)
            {
                return false;
            }

            var iter = _iterator.Value;
            iter.Advance();
            _iterator = iter;
            return _iterator != end;
        }
        
        /// <inheritdoc />
        public void Reset()
        {
            throw new NotSupportedException();
        }
        
        /// <inheritdoc />
        public readonly T Current => _iterator?.Get<T>() ?? throw new InvalidOperationException();

        readonly object IEnumerator.Current => Current;
        
        /// <inheritdoc />
        public void Dispose()
        {
            _iterator = null;
        }
    }
}