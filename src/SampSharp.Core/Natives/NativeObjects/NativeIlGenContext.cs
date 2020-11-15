using System;
using System.Reflection;

namespace SampSharp.Core.Natives.NativeObjects
{
    public class NativeIlGenContext
    {
        public string NativeName { get; set; }
        public MethodInfo BaseMethod { get; set; }
        public NativeIlGenParam[] Parameters { get; set; }
        public FieldInfo[] ProxyGeneratedFields { get; set; }
        public Type[] MethodParameterTypes { get; set; }
        public MethodAttributes MethodOverrideAttributes { get; set; }
    }
}