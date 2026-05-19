using System.Diagnostics.CodeAnalysis;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities;

/// <summary>
/// Provides a safe pointer to an open.mp component of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The unmanaged open.mp component type.</typeparam>
public sealed class SafeComponentHandle<T> : ISafeComponentHandle where T : unmanaged, IComponent.IManagedInterface
{
    private nint _componentHandle;
    private T _value;

    internal SafeComponentHandle(T value, nint componentHandle)
    {
        _componentHandle = componentHandle;
        Value = value;
    }

    /// <summary>
    /// Gets the current value stored in the container.
    /// </summary>
    public T Value
    {
        get
        {
            if (_value.HasValue)
            {
                return _value;
            }

            ThrowDisposed();
            return default;
        }
        private set => _value = value;
    }

    /// <summary>
    /// Gets a value indicating whether the current instance contains a valid value.
    /// </summary>
    public bool HasValue => _value.HasValue;

    nint ISafeComponentHandle.Handle => _componentHandle;

    void ISafeComponentHandle.Free()
    {
        Value = default;
        _componentHandle = 0;
    }

    [DoesNotReturn]
    private static void ThrowDisposed()
    {
        throw new ObjectDisposedException(nameof(T));
    }

    /// <summary>
    /// Defines an implicit conversion from <see cref="SafeComponentHandle{T}"/> to <typeparamref name="T"/>, allowing for seamless access to the underlying component value while ensuring safety against freed components.
    /// </summary>
    public static implicit operator T(SafeComponentHandle<T> safeHandle)
    {
        return safeHandle?.Value ?? default;
    }
}