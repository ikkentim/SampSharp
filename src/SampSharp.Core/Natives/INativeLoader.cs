using System;

namespace SampSharp.Core.Natives
{
    /// <summary>
    ///     Contains the functionality of a native function loader.
    /// </summary>
    public interface INativeLoader
    {
        /// <summary>
        ///     Loads a native with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sizes">The references to the parameter which contains the size of array parameters.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The loaded native.</returns>
        INative Load(string name, uint[] sizes, Type[] parameterTypes);

        /// <summary>
        ///     Loads a native with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The loaded native.</returns>
        INative Load(string name, NativeParameterInfo[] parameters);

        /// <summary>
        ///     Gets the native with the specified handle.
        /// </summary>
        /// <param name="handle">The handle of the native.</param>
        /// <returns>The native.</returns>
        INative Get(int handle);

        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        bool Exists(string name);
    }
}