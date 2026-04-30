using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// Represents a pointer to a structure.
/// </summary>
/// <typeparam name="T"></typeparam>
[StructLayout(LayoutKind.Sequential)]
public readonly struct BlittableStructRef<T> where T : struct
{
    private readonly nint _ptr;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlittableStructRef{T}" /> struct.
    /// </summary>
    /// <param name="ptr">The pointer value.</param>
    public BlittableStructRef(nint ptr)
    {
        _ptr = ptr;
    }
    
    /// <summary>
    /// Gets a value indicating whether the pointer has a value (is not a null pointer).
    /// </summary>
    public bool HasValue => _ptr != 0;

    /// <summary>
    /// Gets the value this pointer points to. Uses <see cref=" Marshal.PtrToStructure{T}(nint)" /> marshalling for marshalling the structure.
    /// </summary>
    public T Value
    {
        get
        {
            if (!HasValue)
            {
                throw new InvalidOperationException("Value is not set");
            }

            return Marshal.PtrToStructure<T>(_ptr);
        }
    }
    
    /// <summary>
    /// Gets the value this pointer points to or the default value of <typeparamref name="T" /> if the pointer is null.
    /// </summary>
    /// <returns>The value this pointer points to or the default value of <typeparamref name="T" /> if the pointer is null.</returns>
    public T GetValueOrDefault()
    {
        return HasValue ? Value : default;
    }
    
    /// <summary>
    /// Gets the value this pointer points to or the specified <paramref name="defaultValue" /> if the pointer is null.
    /// </summary>
    /// <param name="defaultValue">The default value to return if the pointer is null.</param>
    /// <returns>The value this pointer points to or the default value if the pointer is null.</returns>
    public T GetValueorDefault(T defaultValue)
    {
        return HasValue ? Value : defaultValue;
    }
}