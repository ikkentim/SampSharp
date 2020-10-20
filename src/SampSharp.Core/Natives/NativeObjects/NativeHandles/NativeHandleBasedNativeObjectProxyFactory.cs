using System;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.Core.Natives.NativeObjects.NativeHandles
{
    internal class NativeHandleBasedNativeObjectProxyFactory : NativeObjectProxyFactoryBase
    {
        private readonly INativeLoader _nativeLoader;

        public NativeHandleBasedNativeObjectProxyFactory(IGameModeClient gameModeClient, INativeLoader nativeLoader) : base(gameModeClient, "ProxyAssembly")
        {
            _nativeLoader = nativeLoader;
        }

        protected override MethodBuilder CreateMethodBuilder(string nativeName, Type proxyType, uint[] nativeArgumentLengths,
            TypeBuilder typeBuilder, MethodInfo method, string[] identifierPropertyNames, int idIndex, FieldInfo[] proxyFields)
        {
            // Define the method.
            var idCount = identifierPropertyNames.Length;

            var attributes = GetMethodOverrideAttributes(method);
            var parameterTypes = GetMethodParameters(method);
            var nativeParameterTypes = GetNativeParameters(parameterTypes, idIndex, idCount);

            // Find the native.
            var native = _nativeLoader.Load(nativeName, nativeArgumentLengths, nativeParameterTypes);

            if (native == null)
                return null;
            
            // Generate the method body.
            var methodBuilder = typeBuilder.DefineMethod(method.Name, attributes, method.ReturnType, parameterTypes);

            var gen = new NativeHandleBasedProxyGenerator(native, proxyType, identifierPropertyNames, 0, parameterTypes, method.ReturnType);
            gen.Generate(methodBuilder.GetILGenerator());

            return methodBuilder;
        }
    }
}