using SampSharp.OpenMp.Core.Std;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IReadOnlyPool{T}" /> interface.
/// </summary>
[OpenMpApi]
public readonly partial struct IReadOnlyPool<T> where T : unmanaged, IUnmanagedInterface
{
    /// <summary>
    /// Gets the element at the specified index from the pool.
    /// </summary>
    /// <param name="index">The index of the element to retrieve.</param>
    /// <returns>The element at the specified index.</returns>
    public T Get(int index)
    {
        var data = IReadOnlyPoolInterop.IReadOnlyPool_get(_handle, index);
        return StructPointer.AsStruct<T>(data);
    }

    /// <summary>
    /// Retrieves the bounds of the pool.
    /// </summary>
    /// <param name="bounds">The output parameter that will contain the bounds of the pool as a pair of sizes.</param>
    public void Bounds(out Pair<Size, Size> bounds)
    {
        IReadOnlyPoolInterop.IReadOnlyPool_bounds(_handle, out bounds);
    }
}
