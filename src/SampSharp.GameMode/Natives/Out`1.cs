using System;

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Represents an ouptut argument of the specified type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The type this output argument represents.</typeparam>
    public struct Out<T>
    {
        /// <summary>
        ///     Gets the type this instance represents.
        /// </summary>
        public Type Type
        {
            get { return typeof (T); }
        }
    }
}