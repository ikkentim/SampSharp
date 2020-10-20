using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.Core.Natives.NativeObjects
{
    public abstract class NativeObjectProxyFactoryBase : INativeObjectProxyFactory
    {
        private readonly IGameModeClient _gameModeClient;
        private readonly Dictionary<Type, Type> _knownTypes = new Dictionary<Type, Type>();
        private readonly ModuleBuilder _moduleBuilder;
        private readonly object _lock = new object();

        protected NativeObjectProxyFactoryBase(IGameModeClient gameModeClient, string assemblyName)
        {
            _gameModeClient = gameModeClient;
            var asmName = new AssemblyName(assemblyName);
            var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
            _moduleBuilder = asmBuilder.DefineDynamicModule(asmName.Name + ".dll");
        }

        public object CreateInstance(Type type, params object[] arguments)
        {
            if(type == null)
                throw new ArgumentNullException(nameof(type));
            
            // Check already known types.
            if (_knownTypes.TryGetValue(type, out var outType))
                return CreateProxyInstance(outType, arguments);

            lock (_lock)
            {
                // Double check known types because we're in a lock by now.
                if (!_knownTypes.TryGetValue(type, out outType))
                    _knownTypes[type] = outType = GenerateProxyType(type);

                return CreateProxyInstance(outType, arguments);
            }
        }

        protected virtual object CreateProxyInstance(Type proxyType, object[] arguments)
        {
            return Activator.CreateInstance(proxyType, GetProxyConstructorArgs().Concat(arguments).ToArray());
        }
        
        protected virtual object[] GetProxyConstructorArgs()
        {
            return new object[] {_gameModeClient};
        }

        protected virtual FieldInfo[] GetProxyFields(TypeBuilder typeBuilder)
        {
            var gameModeClientField =//TODO: Unused field
                typeBuilder.DefineField("_gameModeClient", typeof(IGameModeClient), FieldAttributes.Private);

            return new[] {(FieldInfo) gameModeClientField};
        }

        private Type GenerateProxyType(Type type)
        {
            if (!(type.IsNested ? type.IsNestedPublic : type.IsPublic))
            {
                throw new ArgumentException(
                    $"Type {type} is not public. Native proxies can only be created for public types.", nameof(type));
            }

            // Define a type for the native object.
            var typeBuilder = _moduleBuilder.DefineType(type.Name + "ProxyClass",
                TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class, type);

            // Add default constructor.
            var proxyFields = GetProxyFields(typeBuilder);
            AddConstructor(type, typeBuilder, proxyFields);

            // Get the common identifiers for the native object.
            var objectIdentifiers = type.GetCustomAttribute<NativeObjectIdentifiersAttribute>()?.Identifiers ??
                                    new string[0];

            // Keep track of whether any method or property has been wrapped in a proxy method.
            var didWrapAnything = false;

            // Wrap properties
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic |
                                                        BindingFlags.Instance))
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

                if (WrapProperty(type, propertyAttribute, objectIdentifiers, typeBuilder, property, get, set, proxyFields))
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

                if (WrapMethod(type, attr, method, objectIdentifiers, typeBuilder, proxyFields))
                    didWrapAnything = true;
            }

            // Store the newly created type and return and instance of it.
            return didWrapAnything ? typeBuilder.CreateTypeInfo().AsType() : type;
        }

        private void AddConstructor(Type type, TypeBuilder typeBuilder, FieldInfo[] proxyFields)
        {
            foreach (var baseConstructor in type.GetConstructors())
            {
                //.method public hidebysig specialname rtspecialname 
                // instance void 
                var constructorParams = baseConstructor.GetParameters();
                var paramTypes = new[] {typeof(IGameModeClient)}
                    .Concat(constructorParams.Select(p => p.ParameterType))
                    .ToArray();
                var attributes = MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName |
                                 MethodAttributes.RTSpecialName;
                var constructor = typeBuilder.DefineConstructor(attributes, CallingConventions.Standard, paramTypes);

                var ilGenerator = constructor.GetILGenerator();

                // base.ctor(arg2, arg3, ..., argN)
                ilGenerator.Emit(OpCodes.Ldarg_0);
                for (var i = 0; i < constructorParams.Length; i++)
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1 + proxyFields.Length);
                ilGenerator.Emit(OpCodes.Call, baseConstructor);
                
                for (var i = 0; i < proxyFields.Length; i++)
                {
                    // _gameModeClient = gameModeClient;
                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.Emit(OpCodes.Stfld, proxyFields[i]);
                }

                // }
                ilGenerator.Emit(OpCodes.Ret);

            }
        }

        private bool WrapMethod(Type type, NativeMethodAttribute attr, MethodInfo method, string[] objectIdentifiers,
            TypeBuilder typeBuilder, FieldInfo[] proxyFields)
        {
            // Determine the details about the native.
            var name = attr.Function ?? method.Name;
            var idIndex = Math.Max(0, attr.IdentifiersIndex);

            var result = CreateMethodBuilder(name, type, attr.Lengths ?? Array.Empty<uint>(), typeBuilder, method,
                attr.IgnoreIdentifiers ? Array.Empty<string>() : objectIdentifiers, idIndex, proxyFields);
            return result != null;
        }

        private bool WrapProperty(Type type, NativePropertyAttribute propertyAttribute, string[] objectIdentifiers,
            TypeBuilder typeBuilder, PropertyInfo property, MethodInfo get, MethodInfo set, FieldInfo[] proxyFields)
        {
            var identifiers = propertyAttribute.IgnoreIdentifiers ? Array.Empty<string>() : objectIdentifiers;

            MethodBuilder getMethodBuilder = null;
            MethodBuilder setMethodBuilder = null;

            if (get != null)
            {
                var nativeName = propertyAttribute.GetFunction ?? $"Get{property.Name}";
                var argLengths = propertyAttribute.GetLengths ?? Array.Empty<uint>();
                getMethodBuilder = CreateMethodBuilder(nativeName, type, argLengths, typeBuilder, get, identifiers, 0, proxyFields);
            }

            if (set != null)
            {
                var nativeName = propertyAttribute.SetFunction ?? $"Set{property.Name}";
                var argLengths = propertyAttribute.SetLengths ?? Array.Empty<uint>();
                setMethodBuilder = CreateMethodBuilder(nativeName, type, argLengths, typeBuilder, set, identifiers, 0, proxyFields);
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

        protected MethodAttributes GetMethodOverrideAttributes(MethodInfo method)
        {
            return (method.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) |
                   MethodAttributes.ReuseSlot |
                   MethodAttributes.Virtual |
                   MethodAttributes.HideBySig;
        }

        protected Type[] GetMethodParameters(MethodInfo method)
        {
            return method.GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();
        }

        protected Type[] GetNativeParameters(Type[] methodParameters, int idIndex, int idCount)
        {
            return idCount > 0
                ? methodParameters.Take(idIndex)
                    .Concat(Enumerable.Repeat(typeof(int), idCount))
                    .Concat(methodParameters.Skip(idIndex))
                    .ToArray()
                : methodParameters;
        }

        protected abstract MethodBuilder CreateMethodBuilder(string nativeName, Type proxyType,
            uint[] nativeArgumentLengths,
            TypeBuilder typeBuilder, MethodInfo method, string[] identifierPropertyNames, int idIndex, FieldInfo[] proxyFields);
    }
}