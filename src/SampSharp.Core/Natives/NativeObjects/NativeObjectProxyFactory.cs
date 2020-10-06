// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.Core.Natives.NativeObjects
{
    public interface INativeObjectProxyFactory
    {
        /// <summary>
        ///     Creates a proxy instance of the specified <paramref name="type" />.
        /// </summary>
        /// <param name="type">The type to create a proxy of.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The proxy instance.</returns>
        object CreateInstance(Type type, params object[] arguments);
    }

    internal class NativeHandleBasedNativeObjectProxyFactory : INativeObjectProxyFactory
    {
        private readonly IGameModeClient _gameModeClient;
        private readonly Dictionary<Type, Type> _knownTypes = new Dictionary<Type, Type>();
        private readonly ModuleBuilder _moduleBuilder;
        private readonly object _lock = new object();

        public NativeHandleBasedNativeObjectProxyFactory(IGameModeClient gameModeClient)
        {
            _gameModeClient = gameModeClient;
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
            var argTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
            var sizes = attr.Lengths;
            var idIndex = Math.Max(0, attr.IdentifiersIndex);

            // Find the native.
            var nativeArgTypes = !attr.IgnoreIdentifiers && objectIdentifiers.Length > 0
                ? argTypes.Take(idIndex)
                    .Concat(Enumerable.Repeat(typeof(int), objectIdentifiers.Length))
                    .Concat(argTypes.Skip(idIndex))
                    .ToArray()
                : argTypes;

            var native = _gameModeClient.NativeLoader.Load(name, sizes, nativeArgTypes);

            if (native == null)
                return false;

            // Define the method.
            var methodBuilder = typeBuilder.DefineMethod(method.Name,
                (method.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.ReuseSlot |
                MethodAttributes.Virtual | MethodAttributes.HideBySig, method.ReturnType, argTypes);

            // Generate the method.
            var il = methodBuilder.GetILGenerator();

            var gen = new NativeObjectIlGenerator(native, type,
                attr.IgnoreIdentifiers ? new string[0] : objectIdentifiers, idIndex, argTypes,
                method.ReturnType);
            gen.Generate(il);
            return true;
        }

        private bool WrapProperty(Type type, NativePropertyAttribute propertyAttribute, string[] objectIdentifiers,
            TypeBuilder typeBuilder, PropertyInfo property, MethodInfo get, MethodInfo set)
        {
            var identifiers = propertyAttribute.IgnoreIdentifiers ? new string[0] : objectIdentifiers;

            // Define the wrapping property.
            var wrapProperty = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType,
                Type.EmptyTypes);

            // Generate getters
            if (get != null)
            {
                // Use "Get{propertyName}" as the default name of the native to call if none is specified in the attached attribute.
                var name = propertyAttribute.GetFunction ?? "Get" + property.Name;

                // Define the method.
                var methodBuilder = typeBuilder.DefineMethod(get.Name,
                    (get.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.ReuseSlot |
                    MethodAttributes.Virtual | MethodAttributes.HideBySig, get.ReturnType, Type.EmptyTypes);

                // Find the native.
                var native = _gameModeClient.NativeLoader.Load(name, propertyAttribute.GetLengths,
                    Enumerable.Repeat(typeof(int), propertyAttribute.IgnoreIdentifiers ? 0 : objectIdentifiers.Length)
                        .ToArray());

                if (native == null)
                    return false;

                // Generate the method body.
                var gen = new NativeObjectIlGenerator(native, type, identifiers, 0, new Type[0], get.ReturnType);
                gen.Generate(methodBuilder.GetILGenerator());

                wrapProperty.SetGetMethod(methodBuilder);
            }

            // Generate setters
            if (set != null)
            {
                // Use "Set{propertyName}" as the default name of the native to call if none is specified in the attached attribute.
                var name = propertyAttribute.SetFunction ?? "Set" + property.Name;

                // Define the method.
                var methodBuilder = typeBuilder.DefineMethod(set.Name,
                    (set.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.ReuseSlot |
                    MethodAttributes.Virtual | MethodAttributes.HideBySig, set.ReturnType,
                    new[] {property.PropertyType});

                // Find the native.
                var native = _gameModeClient.NativeLoader.Load(name, propertyAttribute.SetLengths,
                    Enumerable.Repeat(typeof(int), propertyAttribute.IgnoreIdentifiers ? 0 : objectIdentifiers.Length)
                        .Concat(new[] {property.PropertyType})
                        .ToArray());

                if (native == null)
                    return false;

                // Generate the method body.
                var gen = new NativeObjectIlGenerator(native, type, identifiers, 0, new[] {property.PropertyType},
                    typeof(void));
                gen.Generate(methodBuilder.GetILGenerator());

                wrapProperty.SetSetMethod(methodBuilder);
            }

            return true;
        }
    }

    /// <summary>
    ///     Contains logic for creating native object proxies.
    /// </summary>
    public static class NativeObjectProxyFactory
    {
        /// <summary>
        ///     Creates a proxy instance of the specified type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type to create a proxy of.</typeparam>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The proxy instance.</returns>
        public static T CreateInstance<T>(params object[] arguments) where T : class
        {
            return CreateInstance(typeof(T), arguments) as T;
        }

        /// <summary>
        ///     Creates a proxy instance of the specified <paramref name="type" />.
        /// </summary>
        /// <param name="type">The type to create a proxy of.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The proxy instance.</returns>
        public static object CreateInstance(Type type, params object[] arguments)
        {
            return InternalStorage.RunningClient.NativeObjectProxyFactory.CreateInstance(type, arguments);
        }
    }
}