using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.Core.Natives.NativeObjects
{
    /// <summary>
    /// Represents a base implementation of a native object proxy factory.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "Documentation for generated IL code.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Finding members for code generation.")]
    public abstract class NativeObjectProxyFactoryBase : INativeObjectProxyFactory
    {
        private int _typeNumber;
        private readonly Dictionary<Type, Type> _knownTypes = new Dictionary<Type, Type>();
        private readonly ModuleBuilder _moduleBuilder;
        private readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeObjectProxyFactoryBase" /> class.
        /// </summary>
        /// <param name="assemblyName">Name of the dynamic assembly.</param>
        protected NativeObjectProxyFactoryBase(string assemblyName)
        {
            var asmName = new AssemblyName(assemblyName);
            var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
            _moduleBuilder = asmBuilder.DefineDynamicModule(asmName.Name + ".dll");
        }

        /// <inheritdoc />
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

        private object CreateProxyInstance(Type proxyType, object[] arguments)
        {
            return Activator.CreateInstance(proxyType, GetProxyConstructorArgs().Concat(arguments).ToArray());
        }

        /// <summary>
        /// Gets additional constructor arguments prepended to every constructor call of proxies.
        /// </summary>
        /// <returns>The additional constructor arguments.</returns>
        protected virtual object[] GetProxyConstructorArgs()
        {
            return Array.Empty<object>();
        }

        /// <summary>
        /// Defines additional fields in the proxy type.
        /// </summary>
        /// <param name="typeBuilder">The type builder for the proxy.</param>
        /// <returns>The defined proxy fields.</returns>
        protected virtual FieldInfo[] DefineProxyFields(TypeBuilder typeBuilder)
        {
            return Array.Empty<FieldInfo>();
        }

        private Type GenerateProxyType(Type type)
        {
            if (!(type.IsNested ? type.IsNestedPublic : type.IsPublic))
            {
                throw new ArgumentException(
                    $"Type '{type}' is not public. Native proxies can only be created for public types.", nameof(type));
            }

            // Define a type for the native object.
            
            var typeBuilder = _moduleBuilder.DefineType($"{type.Name}ProxyClass_{++_typeNumber}",
                TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class, type);

            // Add default constructor.
            var proxyFields = DefineProxyFields(typeBuilder);
            AddConstructor(type, typeBuilder, proxyFields);

            // Get the common identifiers for the native object.
            var objectIdentifiers = type.GetCustomAttribute<NativeObjectIdentifiersAttribute>()?.Identifiers ??
                                    Array.Empty<string>();

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
                if (!method.IsVirtual || (!method.IsPublic && !method.IsFamily))
                    continue;

                // Find the attribute containing details about the method.
                var attr = method.GetCustomAttribute<NativeMethodAttribute>();

                if (attr == null)
                    continue;

                if (WrapMethod(type, attr, method, objectIdentifiers, typeBuilder, proxyFields))
                    didWrapAnything = true;
            }

            // Store the newly created type and return and instance of it.
            return didWrapAnything || proxyFields.Length > 0 ? typeBuilder.CreateTypeInfo()!.AsType() : type;
        }

        private void AddConstructor(Type type, TypeBuilder typeBuilder, FieldInfo[] proxyFields)
        {
            foreach (var baseConstructor in type.GetConstructors())
            {
                //.method public hidebysig specialname rtspecialname 
                // instance void 
                var constructorParams = baseConstructor.GetParameters();
                var paramTypes = proxyFields.Select(f => f.FieldType)
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
                attr.IgnoreIdentifiers ? Array.Empty<string>() : objectIdentifiers, idIndex, proxyFields, attr.ReferenceIndices);
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
                getMethodBuilder = CreateMethodBuilder(nativeName, type, argLengths, typeBuilder, get, identifiers, 0, proxyFields, null);
            }

            if (set != null)
            {
                var nativeName = propertyAttribute.SetFunction ?? $"Set{property.Name}";
                var argLengths = propertyAttribute.SetLengths ?? Array.Empty<uint>();
                setMethodBuilder = CreateMethodBuilder(nativeName, type, argLengths, typeBuilder, set, identifiers, 0, proxyFields, null);
            }

            if (getMethodBuilder == null && setMethodBuilder == null)
                return false;
            
            // Define the wrapping property.
            var wrapProperty = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType,
                Type.EmptyTypes);

            if(getMethodBuilder != null)
                wrapProperty.SetGetMethod(getMethodBuilder);

            if(setMethodBuilder != null)
                wrapProperty.SetSetMethod(setMethodBuilder);
            
            return true;
        }

        private MethodBuilder CreateMethodBuilder(string nativeName, Type proxyType,
            uint[] nativeArgumentLengths,
            TypeBuilder typeBuilder, MethodInfo method, string[] identifierPropertyNames, int idIndex,
            FieldInfo[] proxyFields, int[] referenceIndices)
        {
            return CreateMethodBuilder(typeBuilder,
                CreateContext(nativeName, proxyType, nativeArgumentLengths, method, identifierPropertyNames, idIndex,
                    proxyFields, referenceIndices));
        }

        private NativeIlGenContext CreateContext(string nativeName, Type proxyType,
            uint[] nativeArgumentLengths, MethodInfo method, string[] identifierPropertyNames, int idIndex,
            FieldInfo[] proxyFields, int[] referenceIndices)
        {
            var methodParameters = method.GetParameters();
            identifierPropertyNames ??= Array.Empty<string>();
            
            // Seed parameters array with indices
            var parameters = new NativeIlGenParam[methodParameters.Length + identifierPropertyNames.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new NativeIlGenParam
                {
                    Index = i
                };
            }

            // Populate identifier parameters
            for (var i = 0; i < identifierPropertyNames.Length; i++)
            {
                parameters[idIndex + i].Property = proxyType.GetProperty(identifierPropertyNames[i],
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }

            // Populate method parameter based parameters
            var lengthIndex = 0;
            for (var i = 0; i < methodParameters.Length; i++)
            {
                var methodParameter = methodParameters[i];
                var paramIndex = i >= idIndex ? i + identifierPropertyNames.Length : i;
                var parameter = parameters[paramIndex];

                parameter.IsReferenceInput = referenceIndices != null && referenceIndices.Contains(i);
                parameter.Parameter = methodParameter;

                if (parameter.RequiresLength && nativeArgumentLengths.Length > 0)
                {
                    if(nativeArgumentLengths.Length == lengthIndex)
                        throw new InvalidOperationException("No length provided for native argument");

                    parameter.LengthParam = parameters[nativeArgumentLengths[lengthIndex++]];
                }
            }

            // Find length parameters for arrays and string refs
            if (nativeArgumentLengths.Length == 0)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].RequiresLength)
                    {
                        for (var j = i + 1; j < parameters.Length; j++)
                        {
                            if (parameters[j].Type == NativeParameterType.Int32 && !parameters[j].IsLengthParam)
                            {
                                parameters[j].IsLengthParam = true;
                                parameters[i].LengthParam = parameters[j];
                                break;
                            }
                        }

                        if (parameters[i].LengthParam == null)
                            throw new InvalidOperationException("No length provided for native argument");
                    }
                }
            }

            var methodParameterTypes = method.GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            var methodOverrideAttributes =
                (method.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) |
                MethodAttributes.ReuseSlot |
                MethodAttributes.Virtual |
                MethodAttributes.HideBySig;

            return new NativeIlGenContext
            {
                NativeName = nativeName,
                BaseMethod = method,
                ProxyGeneratedFields = proxyFields,
                Parameters = parameters,
                MethodParameterTypes = methodParameterTypes,
                MethodOverrideAttributes = methodOverrideAttributes,
                HasVarArgs = parameters.Any(x=> x.Type == NativeParameterType.VarArgs)
            };
        }

        /// <summary>
        /// Creates a method builder for building an implementation of a native calling method.
        /// </summary>
        /// <param name="typeBuilder">The type builder to create the method in.</param>
        /// <param name="context">The IL generation context.</param>
        /// <returns>Create created method builder.</returns>
        protected abstract MethodBuilder CreateMethodBuilder(TypeBuilder typeBuilder, NativeIlGenContext context);
    }
}