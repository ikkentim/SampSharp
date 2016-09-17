// SampSharp
// Copyright 2016 Tim Potze
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

namespace SampSharp.GameMode.API
{
    /// <summary>
    /// Contains logic for creating natove object proxies.
    /// </summary>
    public static class NativeObjectProxyFactory
    {
        private static readonly Dictionary<Type, Type> KnownTypes = new Dictionary<Type, Type>();
        private static readonly ModuleBuilder ModuleBuilder;

        /// <summary>
        /// Initializes the <see cref="NativeObjectProxyFactory"/> class.
        /// </summary>
        static NativeObjectProxyFactory()
        {
            var asmName = new AssemblyName("ProxyAssembly");
            var asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder = asmBuilder.DefineDynamicModule(asmName.Name, asmName.Name + ".dll");
        }

        /// <summary>
        /// Creates a proxy instance of the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to create a proxy of.</typeparam>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The proxy instance.</returns>
        public static T CreateInstance<T>(params object[] arguments) where T : class
        {
            return CreateInstance(typeof (T), arguments) as T;
        }

        /// <summary>
        /// Creates a proxy instance of  the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to create a proxy of.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The proxy isntance</returns>
        public static object CreateInstance(Type type, params object[] arguments)
        {
            Type outType;

            if (KnownTypes.TryGetValue(type, out outType))
                return Activator.CreateInstance(outType, arguments);

            var typeBuilder = ModuleBuilder.DefineType(type.Name + "ProxyClass",
                TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class, type);

            var objAttr = type.GetCustomAttribute<NativeObjectIdentifiersAttribute>();

            var didWrapAnything = false;

            foreach (
                var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                )
            {
                var get = property.GetMethod;
                var set = property.SetMethod;

                if (!(get?.IsVirtual ?? set.IsVirtual) || property.GetIndexParameters().Length != 0)
                    continue;

                var attr = property.GetCustomAttribute<NativePropertyAttribute>();

                if (attr == null)
                    continue;

                var newProp = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType,
                    Type.EmptyTypes);

                var identifiers = attr.IgnoreIdentifiers ? new string[0] : objAttr?.Identifiers ?? new string[0];
                if (get != null)
                {
                    var name = attr.GetFunction ?? "Get" + property.Name;
                    var sizes = attr.GetLengths;
                    var methodBuilder = typeBuilder.DefineMethod(get.Name,
                        (get.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.ReuseSlot |
                        MethodAttributes.Virtual | MethodAttributes.HideBySig, get.ReturnType, Type.EmptyTypes);

                    var il = methodBuilder.GetILGenerator();
                    var native = Native.Load(name, sizes,
                        Enumerable.Repeat(typeof (int), attr.IgnoreIdentifiers ? 0 : objAttr?.Identifiers?.Length ?? 0)
                            .ToArray());

                    var gen = new NativeObjectILGenerator(native, type, identifiers, new Type[0], get.ReturnType);
                    gen.Generate(il);

                    newProp.SetGetMethod(methodBuilder);
                }


                if (set != null)
                {
                    var name = attr.SetFunction ?? "Set" + property.Name;
                    var sizes = attr.SetLengths;
                    var methodBuilder = typeBuilder.DefineMethod(set.Name,
                        (set.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.ReuseSlot |
                        MethodAttributes.Virtual | MethodAttributes.HideBySig, set.ReturnType,
                        new[] {property.PropertyType});

                    var il = methodBuilder.GetILGenerator();
                    var native = Native.Load(name, sizes,
                        Enumerable.Repeat(typeof (int), attr.IgnoreIdentifiers ? 0 : objAttr?.Identifiers?.Length ?? 0)
                            .Concat(new[] {property.PropertyType})
                            .ToArray());

                    var gen = new NativeObjectILGenerator(native, type, identifiers, new[] {property.PropertyType},
                        typeof (void));
                    gen.Generate(il);

                    newProp.SetSetMethod(methodBuilder);
                }

                didWrapAnything = true;
            }

            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                )
            {
                if (!method.IsVirtual || (!method.IsPublic && !method.IsFamily))
                    continue;

                var attr = method.GetCustomAttribute<NativeMethodAttribute>();

                if (attr == null)
                    continue;

                var name = attr.Function ?? method.Name;
                var argTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
                var sizes = attr.Lengths;

                var methodBuilder = typeBuilder.DefineMethod(method.Name,
                    (method.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.ReuseSlot |
                    MethodAttributes.Virtual | MethodAttributes.HideBySig, method.ReturnType, argTypes);

                var il = methodBuilder.GetILGenerator();
                var native = Native.Load(name, sizes,
                    Enumerable.Repeat(typeof (int), attr.IgnoreIdentifiers ? 0 : objAttr?.Identifiers?.Length ?? 0)
                        .Concat(argTypes)
                        .ToArray());

                if (native == null)
                {
                    continue;
                }

                var gen = new NativeObjectILGenerator(native, type,
                    attr.IgnoreIdentifiers ? new string[0] : objAttr?.Identifiers ?? new string[0], argTypes,
                    method.ReturnType);
                gen.Generate(il);

                didWrapAnything = true;
            }

            outType = didWrapAnything ? typeBuilder.CreateType() : type;

            KnownTypes[type] = outType;
            return Activator.CreateInstance(outType, arguments);
        }
    }
}