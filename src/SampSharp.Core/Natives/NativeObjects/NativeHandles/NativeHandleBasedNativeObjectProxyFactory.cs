using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.Core.Natives.NativeObjects
{
    internal class NativeHandleBasedNativeObjectProxyFactory : INativeObjectProxyFactory
    {
        private readonly INativeLoader _nativeLoader;
        private readonly Dictionary<Type, Type> _knownTypes = new Dictionary<Type, Type>();
        private readonly ModuleBuilder _moduleBuilder;
        private readonly object _lock = new object();

        public NativeHandleBasedNativeObjectProxyFactory(INativeLoader nativeLoader)
        {
            _nativeLoader = nativeLoader;
            var asmName = new AssemblyName("ProxyAssembly");
            var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
            _moduleBuilder = asmBuilder.DefineDynamicModule(asmName.Name + ".dll");
        }

        public object CreateInstance(Type type, params object[] arguments)
        {
            if(type == null)
                throw new ArgumentNullException(nameof(type));
            
            // Check already known types.
            if (_knownTypes.TryGetValue(type, out var outType))
                return Activator.CreateInstance(outType, arguments);

            lock (_lock)
            {
                // Double check known types because we're in a lock by now.
                if (_knownTypes.TryGetValue(type, out outType))
                    return Activator.CreateInstance(outType, arguments);
                
                if (!(type.IsNested ? type.IsNestedPublic : type.IsPublic))
                {
                    throw new ArgumentException($"Type {type} is not public. Native proxies can only be created for public types.", nameof(type));
                }

                // Define a type for the native object.
                var typeBuilder = _moduleBuilder.DefineType(type.Name + "ProxyClass",
                    TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class, type);

                // Get the common identifiers for the native object.
                var objectIdentifiers = type.GetCustomAttribute<NativeObjectIdentifiersAttribute>()?.Identifiers ?? new string[0];

                // Keep track of whether any method or property has been wrapped in a proxy method.
                var didWrapAnything = false;

                // Wrap properties
                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    var get = property.GetMethod;
                    var set = property.SetMethod;

                    // Make sure the property is virtual and not an indexed property.
                    if (!(get?.IsVirtual ?? set.IsVirtual) || property.GetIndexParameters().Length != 0)
                        continue;

                    // Find the attribute containing details about the property.
                    var propertyAttribute = property.GetCustomAttribute<NativePropertyAttribute>();

                    if (propertyAttribute == null)
                        continue;

                    if (WrapProperty(type, propertyAttribute, objectIdentifiers, typeBuilder, property, get, set))
                        didWrapAnything = true;
                }

                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    // Make sure the method is virtual, public and not protected.
                    if (!method.IsVirtual || !method.IsPublic && !method.IsFamily)
                        continue;

                    // Find the attribute containing details about the method.
                    var attr = method.GetCustomAttribute<NativeMethodAttribute>();

                    if (attr == null)
                        continue;

                    if (WrapMethod(type, attr, method, objectIdentifiers, typeBuilder))
                        didWrapAnything = true;
                }

                // Store the newly created type and return and instance of it.
                outType = didWrapAnything ? typeBuilder.CreateTypeInfo().AsType() : type;

                _knownTypes[type] = outType;
                return Activator.CreateInstance(outType, arguments);
            }
        }

        private bool WrapMethod(Type type, NativeMethodAttribute attr, MethodInfo method, string[] objectIdentifiers,
            TypeBuilder typeBuilder)
        {
            // Determine the details about the native.
            var name = attr.Function ?? method.Name;
            var idIndex = Math.Max(0, attr.IdentifiersIndex);

            var result = CreateMethodBuilder(name, type, attr.Lengths ?? Array.Empty<uint>(), typeBuilder, method,
                attr.IgnoreIdentifiers ? Array.Empty<string>() : objectIdentifiers, idIndex);
            return result != null;
        }

        private bool WrapProperty(Type type, NativePropertyAttribute propertyAttribute, string[] objectIdentifiers,
            TypeBuilder typeBuilder, PropertyInfo property, MethodInfo get, MethodInfo set)
        {
            var identifiers = propertyAttribute.IgnoreIdentifiers ? Array.Empty<string>() : objectIdentifiers;

            MethodBuilder getMethodBuilder = null;
            MethodBuilder setMethodBuilder = null;

            if (get != null)
            {
                var nativeName = propertyAttribute.GetFunction ?? $"Get{property.Name}";
                var argLengths = propertyAttribute.GetLengths ?? Array.Empty<uint>();
                getMethodBuilder = CreateMethodBuilder(nativeName, type, argLengths, typeBuilder, get, identifiers, 0);
            }

            if (set != null)
            {
                var nativeName = propertyAttribute.SetFunction ?? $"Set{property.Name}";
                var argLengths = propertyAttribute.SetLengths ?? Array.Empty<uint>();
                setMethodBuilder = CreateMethodBuilder(nativeName, type, argLengths, typeBuilder, set, identifiers, 0);
            }

            if (getMethodBuilder == null && setMethodBuilder == null)
                return false;
            
            // Define the wrapping property.
            var wrapProperty = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType,
                Type.EmptyTypes);

            wrapProperty.SetGetMethod(getMethodBuilder);
            wrapProperty.SetSetMethod(setMethodBuilder);
            
            return true;
        }
        
        private MethodBuilder CreateMethodBuilder(string nativeName, Type proxyType, uint[] nativeArgumentLengths,
            TypeBuilder typeBuilder, MethodInfo method, string[] identifierPropertyNames, int idIndex)
        {
            // Define the method.
            var attributes = (method.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) |
                             MethodAttributes.ReuseSlot |
                             MethodAttributes.Virtual |
                             MethodAttributes.HideBySig;
            
            var idCount = identifierPropertyNames.Length;

            var parameterTypes = method.GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();
            
            var nativeParameterTypes = idCount > 0
                ? parameterTypes.Take(idIndex)
                    .Concat(Enumerable.Repeat(typeof(int), idCount))
                    .Concat(parameterTypes.Skip(idIndex))
                    .ToArray()
                : parameterTypes;

            // Find the native.
            var native = _nativeLoader.Load(nativeName, nativeArgumentLengths, nativeParameterTypes);

            if (native == null)
                return null;
            
            // Generate the method body.
            var methodBuilder = typeBuilder.DefineMethod(method.Name, attributes, method.ReturnType, parameterTypes);

            var gen = new NativeHandleBasedNativeObjectIlGenerator(native, proxyType, identifierPropertyNames, 0, parameterTypes, method.ReturnType);
            gen.Generate(methodBuilder.GetILGenerator());

            return methodBuilder;
        }
    }
}