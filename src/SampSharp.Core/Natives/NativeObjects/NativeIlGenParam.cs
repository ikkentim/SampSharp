using System;
using System.Reflection;

namespace SampSharp.Core.Natives.NativeObjects
{
    /// <summary>
    /// Provides information about a native parameter which can be consumed by a proxy factory IL generator.
    /// </summary>
    public class NativeIlGenParam
    {
        private ParameterInfo _parameter;
        private PropertyInfo _property;

        /// <summary>
        /// Gets or sets the index of this native parameter.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the method parameter of this native parameter.
        /// </summary>
        public ParameterInfo Parameter
        {
            get => _parameter;
            set
            {
                if (value != null)
                {
                    _property = null;

                    if (value.ParameterType.IsArray && value.GetCustomAttribute<ParamArrayAttribute>() != null)
                    {
                        Type = NativeParameterType.VarArgs;
                    }
                    else
                    {
                        Type = NativeParameterInfo.ForType(value.ParameterType).Type;
                    }
                }
                _parameter = value;
            }
        }

        /// <summary>
        /// Gets or sets the index property of this native parameter.
        /// </summary>
        public PropertyInfo Property
        {
            get => _property;
            set
            {
                if (value != null)
                {
                    _parameter = null;
                    Type = NativeParameterInfo.ForType(value.PropertyType).Type;
                }
                _property = value;
            }
        }

        /// <summary>
        /// Gets or sets the length parameter of this native parameter.
        /// </summary>
        public NativeIlGenParam LengthParam { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this native parameter is a length parameter.
        /// </summary>
        public bool IsLengthParam { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this native parameter is an input value by reference.
        /// </summary>
        public bool IsReferenceInput { get; set; }

        /// <summary>
        /// Gets the name of this native parameter.
        /// </summary>
        public string Name => Parameter?.Name ?? Property?.Name;
        /// <summary>
        /// Gets the type of the method parameter or index property of this native paramter.
        /// </summary>
        public Type InputType => Parameter?.ParameterType ?? Property.PropertyType;

        /// <summary>
        /// Gets the type of this native parameter.
        /// </summary>
        public NativeParameterType Type { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this native parameter requires a length.
        /// </summary>
        public bool RequiresLength => Type.HasFlag(NativeParameterType.Array) || Type == NativeParameterType.StringReference;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name}[{Index}:{Type}{(LengthParam == null ? string.Empty : $", len={LengthParam.Name}")}]";
        }
    }
}