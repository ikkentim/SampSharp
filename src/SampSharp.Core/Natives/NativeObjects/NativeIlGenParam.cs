using System;
using System.Reflection;

namespace SampSharp.Core.Natives.NativeObjects
{
    public class NativeIlGenParam
    {
        public int Index { get; set; }
        public ParameterInfo Parameter { get; set; }
        public PropertyInfo Property { get; set; }
        public NativeIlGenParam LengthParam { get; set; }
        public bool IsLengthParam { get; set; }
        public string Name => Parameter?.Name ?? Property?.Name;
        public Type InputType => Parameter?.ParameterType ?? Property.PropertyType;
        public NativeParameterType Type => NativeParameterInfo.ForType(Property?.PropertyType ?? Parameter.ParameterType).Type;
        public bool RequiresLength => Type.HasFlag(NativeParameterType.Array) || Type == NativeParameterType.StringReference;

        public override string ToString()
        {
            return $"{Name}[{Index}:{Type}{(LengthParam == null ? string.Empty : $", len={LengthParam.Name}")}]";
        }
    }
}