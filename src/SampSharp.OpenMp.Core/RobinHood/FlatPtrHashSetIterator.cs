using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.RobinHood;

[StructLayout(LayoutKind.Sequential)]
internal struct FlatPtrHashSetIterator : IEquatable<FlatPtrHashSetIterator>
{
    private nint _value; // NodePtr mKeyVals{nullptr};
    private nint _info; // uint8_t const* mInfo{nullptr};
    
    public readonly T Get<T>() where T : unmanaged
    {
        return StructPointer.Dereference<T>(_value);
    }

    public readonly bool Equals(FlatPtrHashSetIterator other)
    {
        return _value == other._value;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is FlatPtrHashSetIterator other && Equals(other);
    }

    public override readonly int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public void Advance()
    {
        var u = RobinHoodInterop.FlatPtrHashSet_inc(this);
        _value = u._value;
        _info = u._info;
    }

    public static bool operator ==(FlatPtrHashSetIterator a, FlatPtrHashSetIterator b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(FlatPtrHashSetIterator a, FlatPtrHashSetIterator b)
    {
        return !a.Equals(b);
    }
}