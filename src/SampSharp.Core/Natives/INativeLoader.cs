using System;
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Core.Natives
{
    /// <summary>
    ///     Contains the functionality of a native function loader.
    /// </summary>
    [Obsolete("Native handle based native invocation is deprecated and will be removed in a future version.")]
    public interface INativeLoader
    {
        /// <summary>
        /// Gets the factory for creating native object proxies.
        /// </summary>
        INativeObjectProxyFactory ProxyFactory { get; }
        
        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        bool Exists(string name);
    }
}