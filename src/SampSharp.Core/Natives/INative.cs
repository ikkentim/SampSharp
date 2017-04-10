namespace SampSharp.Core.Natives
{
    /// <summary>
    ///     Contains the definition of a native.
    /// </summary>
    public interface INative
    {
        /// <summary>
        ///     Gets the handle of this native.
        /// </summary>
        int Handle { get; }

        /// <summary>
        ///     Gets the name of the native function.
        /// </summary>
        string Name { get; }


        /// <summary>
        ///     Gets the parameters.
        /// </summary>
        NativeParameterInfo[] Parameters { get; }

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native.</returns>
        int Invoke(params object[] arguments);

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a float.</returns>
        float InvokeFloat(params object[] arguments);

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a bool.</returns>
        bool InvokeBool(params object[] arguments);
    }
}