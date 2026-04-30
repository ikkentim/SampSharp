using System.Diagnostics;
using System.Runtime.InteropServices;
using SampSharp.OpenMp.Core.RobinHood;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Full copy of the unmanaged implementation of the MarkedPoolIterator struct. We need to implement this ourselves
/// because the original implementation uses a template for the type of the pool entries and calls getID on the entries.
/// In the proxy wrapper code we assume all IPools to be of type IDProvider, which would cause mismatching vtable refs.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct MarkedPoolIterator<T> : IDisposable, IEquatable<MarkedPoolIterator<T>> where T : unmanaged, IIDProvider.IManagedInterface
{
    private readonly IPool<T> _pool;
    private int _lockedId;
    private readonly FlatPtrHashSet<T> _entries;
    private FlatPtrHashSetIterator _iter;

    internal MarkedPoolIterator(IPool<T> pool, FlatPtrHashSet<T> entries, FlatPtrHashSetIterator iter)
    {
        _pool = pool;
        _lockedId = -1;
        _entries = entries;
        _iter = iter;

        Lock();
    }

    public void Dispose()
    {
        Unlock();
    }

    private void Lock()
    {
        Debug.Assert(_lockedId == -1);
        if (_iter != _entries.End())
        {
            _lockedId = _iter.Get<T>().GetID();
            _pool.Lock(_lockedId);
        }
    }

    private void Unlock()
    {
        if (_lockedId != -1)
        {
            _pool.Unlock(_lockedId);
            _lockedId = -1;
        }
    }

    public readonly T Current => _iter.Get<T>();

    public readonly bool Equals(MarkedPoolIterator<T> other)
    {
        return _iter.Equals(other._iter);
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is MarkedPoolIterator<T> other && Equals(other);
    }

    public override readonly int GetHashCode()
    {
        return _iter.GetHashCode();
    }
    
    public void Advance()
    {
        _iter.Advance();
        Unlock();
        Lock();
    }

    public static bool operator ==(MarkedPoolIterator<T> a, MarkedPoolIterator<T> b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(MarkedPoolIterator<T> a, MarkedPoolIterator<T> b)
    {
        return !a.Equals(b);
    }
}