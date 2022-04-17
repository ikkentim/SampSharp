using System;
using System.Reflection;

namespace SampSharp.Core.Natives
{
    /// <summary>
    /// Provides information about a native which can be consumed by a proxy factory IL generator.
    /// </summary>
    internal class NativeIlGenContext
    {
        /// <summary>
        /// Gets or sets the name of the native to be called.
        /// </summary>
        public string NativeName { get; set; }

        /// <summary>
        /// Gets or sets the base method of the proxy type to by overridden.
        /// </summary>
        public MethodInfo BaseMethod { get; set; }

        /// <summary>
        /// Gets or sets the parameters of the native.
        /// </summary>
        public NativeIlGenParam[] Parameters { get; set; }

        /// <summary>
        /// Gets or sets the synchronization provider field.
        /// </summary>
        public FieldInfo SynchronizationProviderField { get; set; }

        /// <summary>
        /// Gets or sets the base method parameter types.
        /// </summary>
        public Type[] MethodParameterTypes { get; set; }

        /// <summary>
        /// Gets or sets the method attributes to be used to override the base method implementation.
        /// </summary>
        public MethodAttributes MethodOverrideAttributes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this native has variable arguments.
        /// </summary>
        public bool HasVarArgs { get; set; }
    }
}