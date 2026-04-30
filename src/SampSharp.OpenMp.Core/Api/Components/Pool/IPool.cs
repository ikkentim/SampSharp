using System.Collections;
using SampSharp.OpenMp.Core.RobinHood;
using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPool{T}" /> interface.
/// </summary>
[OpenMpApi]
public readonly partial struct IPool<T> : IEnumerable<T> where T : unmanaged, IIDProvider.IManagedInterface
{
    /// <summary>
    /// Gets the element at the specified index.
    /// </summary>
    /// <param name="index">The index of the element to retrieve.</param>
    /// <returns>The element at the specified index.</returns>
    public T Get(int index)
    {
        return ((IReadOnlyPool<T>)this).Get(index);
    }

    /// <summary>
    /// Gets the bounds of the pool.
    /// </summary>
    /// <returns>A tuple containing the lower and upper bounds of the pool.</returns>
    public (Size, Size) Bounds()
    {
        ((IReadOnlyPool<T>)this).Bounds(out var bounds);
        return bounds;
    }

    /// <summary>
    /// Releases the element at the specified index.
    /// </summary>
    /// <param name="index">The index of the element to release.</param>
    public void Release(int index)
    {
        IPoolInterop.IPool_release(_handle, index);
    }

    /// <summary>
    /// Locks the element at the specified index.
    /// </summary>
    /// <param name="index">The index of the element to lock.</param>
    public void Lock(int index)
    {
        IPoolInterop.IPool_lock(_handle, index);
    }

    /// <summary>
    /// Unlocks the element at the specified index.
    /// </summary>
    /// <param name="index">The index of the element to unlock.</param>
    /// <returns><see langword="true" /> if the element was successfully unlocked; otherwise, <see langword="false" />.</returns>
    public bool Unlock(int index)
    {
        return IPoolInterop.IPool_unlock(_handle, index);
    }

    /// <summary>
    /// Gets the number of elements in the pool.
    /// </summary>
    /// <returns>The number of elements in the pool.</returns>
    public Size Count()
    {
        return IPoolInterop.IPool_count(_handle);
    }

    /// <summary>
    /// Gets the event dispatcher for the pool.
    /// </summary>
    /// <returns>An <see cref="IEventDispatcher{T}" /> for handling pool events.</returns>
    public IEventDispatcher<IPoolEventHandler<T>> GetPoolEventDispatcher()
    {
        var data = IPoolInterop.IPool_getPoolEventDispatcher(_handle);
        return new IEventDispatcher<IPoolEventHandler<T>>(data);
    }

    private FlatPtrHashSet<T> Entries()
    {
        return new FlatPtrHashSet<T>(IPoolInterop.IPool_entries(_handle));
    }

    private MarkedPoolIterator<T> Begin()
    {
        var entries = Entries();
        return new MarkedPoolIterator<T>(this, entries, entries.Begin());
    }

    private MarkedPoolIterator<T> End()
    {
        var entries = Entries();
        return new MarkedPoolIterator<T>(this, entries, entries.End());
    }

    /// <summary>
    /// Gets an enumerator that iterates through the pool.
    /// </summary>
    /// <returns>An enumerator for the pool.</returns>
    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
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
    /// Converts the pool to a read-only pool.
    /// </summary>
    /// <param name="value">The pool to convert.</param>
    public static explicit operator IReadOnlyPool<T>(IPool<T> value)
    {
        return new IReadOnlyPool<T>(value._handle);
    }

    /// <summary>
    /// Represents an enumerator for the <see cref="IPool{T}" />.
    /// </summary>
    public struct Enumerator : IEnumerator<T>
    {
        private readonly IPool<T> _pool;
        private MarkedPoolIterator<T>? _iterator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enumerator" /> struct.
        /// </summary>
        /// <param name="pool">The pool to enumerate.</param>
        internal Enumerator(IPool<T> pool)
        {
            _pool = pool;
            _iterator = null;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the pool.
        /// </summary>
        /// <returns><see langword="true" /> if the enumerator was successfully advanced; otherwise, <see langword="false" />.</returns>
        public bool MoveNext()
        {
            if (!_iterator.HasValue)
            {
                _iterator = _pool.Begin();
                return _iterator != _pool.End();
            }

            var iter = _iterator.Value;
            iter.Advance();

            if (iter == _pool.End())
            {
                return false;
            }

            _iterator = iter;
            return true;
        }

        /// <summary>
        /// Resets the enumerator to its initial position.
        /// </summary>
        /// <exception cref="InvalidOperationException">Always thrown as resetting is not supported.</exception>
        public void Reset()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets the element in the pool at the current position of the enumerator.
        /// </summary>
        public readonly T Current => _iterator?.Current ?? throw new InvalidOperationException();

        readonly object IEnumerator.Current => Current;

        /// <summary>
        /// Releases all resources used by the enumerator.
        /// </summary>
        public void Dispose()
        {
            _iterator?.Dispose();
            _iterator = null;
        }
    }
}
