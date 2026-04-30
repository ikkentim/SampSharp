using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core
{
    /// <summary>
    /// Represents a pointer to an unmanaged value.
    /// </summary>
    /// <typeparam name="T">the type of the unmanaged value.</typeparam>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct BlittableRef<T> where T : unmanaged
    {
        private readonly nint _ptr;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlittableRef{T}" /> struct.
        /// </summary>
        /// <param name="ptr">The pointer value.</param>
        public BlittableRef(nint ptr)
        {
            _ptr = ptr;
        }

        /// <summary>
        /// Gets a value indicating whether the pointer has a value (is not a null pointer).
        /// </summary>
        public bool HasValue => _ptr != 0;

        /// <summary>
        /// Gets a reference to the value this pointer points to.
        /// </summary>
        public unsafe ref T Value
        {
            get
            {
                if (!HasValue)
                {
                    throw new InvalidOperationException("Value is not set");
                }
                return ref Unsafe.AsRef<T>((void*)_ptr);
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
}