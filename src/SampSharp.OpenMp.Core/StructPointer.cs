using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// Provides methods to work with structs which represent pointers to open.mp interfaces.
/// </summary>
internal static unsafe class StructPointer
{
    /// <summary>
    /// Reinterprets the given pointer as an open.mp pointer struct of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The open.mp pointer struct type.</typeparam>
    /// <param name="pointer">The pointer to reinterpret.</param>
    /// <returns>The open.mp pointer type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T AsStruct<T>(nint pointer) where T : unmanaged, IUnmanagedInterface
    {
        Debug.Assert(sizeof(T) == sizeof(nint));
        return *(T*)&pointer;
    }

    /// <summary>
    /// Deference the given pointer and return the value as a struct of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The unmanaged type the pointer points to.</typeparam>
    /// <param name="pointer">The pointer to dereference.</param>
    /// <returns>The dereferenced value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Dereference<T>(nint pointer) where T : unmanaged
    {
        return *(T*)pointer;
    }
}