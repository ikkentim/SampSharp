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
    /// <summary>
    ///     Contains logic for creating natove object proxies.
    /// </summary>
    public static class NativeObjectProxyFactory
    {
        private static readonly Dictionary<Type, Type> KnownTypes = new Dictionary<Type, Type>();
        private static readonly ModuleBuilder ModuleBuilder;

        /// <summary>
        ///     Initializes the <see cref="NativeObjectProxyFactory" /> class.
        /// </summary>
        static NativeObjectProxyFactory()
        {
            var asmName = new AssemblyName("ProxyAssembly");
            var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
            ModuleBuilder = asmBuilder.DefineDynamicModule(asmName.Name + ".dll");
        }

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
        /// <returns>The proxy isntance</returns>
        public static object CreateInstance(Type type, params object[] arguments)
        {
            // Check already known types.
            if (KnownTypes.TryGetValue(type, out var outType))
                return Activator.CreateInstance(outType, arguments);

            // Define a type for the native object.
            var typeBuilder = ModuleBuilder.DefineType(type.Name + "ProxyClass",
                TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class, type);

            // Get the common identifiers for the native object.
            var objectIdentifiers = type.GetTypeInfo().GetCustomAttribute<NativeObjectIdentifiersAttribute>()?.Identifiers ?? new string[0];

            // Keep track of whether any method or property has been wrapped in a proxy method.
            var didWrapAnything = false;

            // Wrap properties
            foreach (var property in type.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
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
                    var native = InternalStorage.RunningClient.NativeLoader.Load(name, propertyAttribute.GetLengths,
                        Enumerable.Repeat(typeof(int), propertyAttribute.IgnoreIdentifiers ? 0 : objectIdentifiers.Length)
                            .ToArray());

                    if (native == null)
                        continue;

                    // Generate the method body.
                    var gen = new NativeObjectILGenerator(native, type, identifiers, new Type[0], get.ReturnType);
                    gen.Generate(methodBuilder.GetILGenerator());

                    wrapProperty.SetGetMethod(methodBuilder);
                }

                // Getnerate setters
                if (set != null)
                {
                    // Use "Set{propertyName}" as the default name of the native to call if none is specified in the attached attribute.
                    var name = propertyAttribute.SetFunction ?? "Set" + property.Name;

                    // Define the method.
                    var methodBuilder = typeBuilder.DefineMethod(set.Name,
                        (set.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.ReuseSlot |
                        MethodAttributes.Virtual | MethodAttributes.HideBySig, set.ReturnType,
                        new[] { property.PropertyType });

                    // Find the native.
                    var native = InternalStorage.RunningClient.NativeLoader.Load(name, propertyAttribute.SetLengths,
                        Enumerable.Repeat(typeof(int), propertyAttribute.IgnoreIdentifiers ? 0 : objectIdentifiers.Length)
                            .Concat(new[] { property.PropertyType })
                            .ToArray());

                    if (native == null)
                        continue;

                    // Generate the method body.
                    var gen = new NativeObjectILGenerator(native, type, identifiers, new[] { property.PropertyType }, typeof(void));
                    gen.Generate(methodBuilder.GetILGenerator());

                    wrapProperty.SetSetMethod(methodBuilder);
                }

                didWrapAnything = true;
            }

            foreach (var method in type.GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                // Make sure the method is virtual, public and not protected.
                if (!method.IsVirtual || !method.IsPublic && !method.IsFamily)
                    continue;

                // Find the attribute containing details about the method.
                var attr = method.GetCustomAttribute<NativeMethodAttribute>();

                if (attr == null)
                    continue;

                // Determine the details about the native.
                var name = attr.Function ?? method.Name;
                var argTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
                var sizes = attr.Lengths;

                // Find the native.
                var native = InternalStorage.RunningClient.NativeLoader.Load(name, sizes,
                    Enumerable.Repeat(typeof(int), attr.IgnoreIdentifiers ? 0 : objectIdentifiers.Length)
                        .Concat(argTypes)
                        .ToArray());

                if (native == null)
                    continue;

                // Define the method.
                var methodBuilder = typeBuilder.DefineMethod(method.Name,
                    (method.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.ReuseSlot |
                    MethodAttributes.Virtual | MethodAttributes.HideBySig, method.ReturnType, argTypes);

                // Generate the method.
                var il = methodBuilder.GetILGenerator();

                var gen = new NativeObjectILGenerator(native, type,
                    attr.IgnoreIdentifiers ? new string[0] : objectIdentifiers, argTypes,
                    method.ReturnType);
                gen.Generate(il);

                didWrapAnything = true;
            }

            // Store the newly created type and return and instance of it.
            outType = didWrapAnything ? typeBuilder.CreateTypeInfo().AsType() : type;

            KnownTypes[type] = outType;
            return Activator.CreateInstance(outType, arguments);
        }
    }
}