using System;
using System.Reflection;

namespace SampSharp.Core.Natives.NativeObjects
{
    /// <summary>
    /// Provides information about a native which can be consumed by a proxy factory IL generator.
    /// </summary>
    public class NativeIlGenContext
    {
        private MethodInfo _baseMethod;

        /// <summary>
        /// Gets or sets the name of the native to be called.
        /// </summary>
        public string NativeName { get; set; }

        /// <summary>
        /// Gets or sets the base method of the proxy type to by overridden.
        /// </summary>
        public MethodInfo BaseMethod
        {
            get => _baseMethod;
            set => _baseMethod = value;
        }

        /// <summary>
        /// Gets or sets the parameters of the native.
        /// </summary>
        public NativeIlGenParam[] Parameters { get; set; }
        /// <summary>
        /// Gets or sets the fields generated for the proxy.
        /// </summary>
        public FieldInfo[] ProxyGeneratedFields { get; set; }

        /// <summary>
        /// Gets or sets the base method parameter types.
        /// </summary>
        public Type[] MethodParameterTypes { get; set; }

        /// <summary>
        /// Gets or sets the method attributes to be used to override the base method implementation.
        /// </summary>
        public MethodAttributes MethodOverrideAttributes { get; set; }
    }
}