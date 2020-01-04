// SampSharp
// Copyright 2020 Tim Potze
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

namespace SampSharp.Entities.Utilities
{
    /// <summary>
    /// Represents a utility for scanning for methods with specific attributes in loaded assemblies.
    /// </summary>
    /// <see cref="MethodScanner.Create" />
    public sealed class MethodScanner
    {
        private List<Assembly> _assemblies = new List<Assembly>();
        private List<Type> _classAttributes = new List<Type>();
        private List<Type> _classImplements = new List<Type>();
        private bool _includeInstanceMethod = true;
        private bool _includeNonPublicMethod;
        private bool _includeStaticMethod;
        private List<Type> _methodAttributes = new List<Type>();

        private MethodScanner()
        {
        }

        private BindingFlags MethodBindingFlags =>
            (_includeInstanceMethod ? BindingFlags.Instance : BindingFlags.Default) |
            (_includeStaticMethod ? BindingFlags.Static : BindingFlags.Default) |
            BindingFlags.Public |
            (_includeNonPublicMethod ? BindingFlags.NonPublic : BindingFlags.Default);

        /// <summary>
        /// Creates a new scanner.
        /// </summary>
        /// <returns>A newly created scanner.</returns>
        public static MethodScanner Create()
        {
            return new MethodScanner();
        }

        /// <summary>
        /// Includes the specified <paramref name="assembly" /> in the scan.
        /// </summary>
        /// <param name="assembly">The assembly to include.</param>
        /// <returns>An updated scanner.</returns>
        public MethodScanner IncludeAssembly(Assembly assembly)
        {
            if (assembly == null || _assemblies.Contains(assembly))
                return this;

            var result = Clone();
            result._assemblies.Add(assembly);
            return result;
        }

        /// <summary>
        /// Includes the referenced assemblies of the previously included assemblies in the scan.
        /// </summary>
        /// <param name="skipSystem">
        /// If set to <c>true</c>, system assemblies (System.*, Microsoft.*, netstandard) are skipped in
        /// the scan.
        /// </param>
        /// <returns>An updated scanner.</returns>
        public MethodScanner IncludeReferencedAssemblies(bool skipSystem = true)
        {
            var assemblies = new List<Assembly>();

            void AddToScan(Assembly asm)
            {
                if (assemblies.Contains(asm))
                    return;

                assemblies.Add(asm);

                foreach (var assemblyRef in asm.GetReferencedAssemblies())
                {
                    if (skipSystem &&
                        (assemblyRef.Name.StartsWith("System") ||
                         assemblyRef.Name.StartsWith("Microsoft") ||
                         assemblyRef.Name.StartsWith("netstandard")))
                        continue;

                    AddToScan(Assembly.Load(assemblyRef));
                }
            }

            foreach (var a in _assemblies)
                AddToScan(a);

            var result = Clone();
            result._assemblies = assemblies;
            return result;
        }

        /// <summary>
        /// Includes the entry assembly and all referenced assemblies in the scan..
        /// </summary>
        /// <param name="skipSystem">
        /// if set to <c>true</c>, system assemblies (System.*, Microsoft.*, netstandard) are skipped in
        /// the scan.
        /// </param>
        /// <returns>An updated scanner.</returns>
        public MethodScanner IncludeAllAssemblies(bool skipSystem = true)
        {
            return IncludeEntryAssembly()
                .IncludeReferencedAssemblies(skipSystem);
        }

        /// <summary>
        /// Includes the entry assembly in the scan.
        /// </summary>
        /// <returns>An updated scanner.</returns>
        public MethodScanner IncludeEntryAssembly()
        {
            return IncludeAssembly(Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// Includes static methods in the scan.
        /// </summary>
        /// <param name="exclusive">If set to <c>true</c>, only include static methods in the scan.</param>
        /// <returns>An updated scanner.</returns>
        public MethodScanner IncludeStaticMethods(bool exclusive)
        {
            var result = Clone();
            result._includeStaticMethod = true;
            result._includeInstanceMethod = !exclusive;
            return result;
        }

        /// <summary>
        /// Includes non-public methods in the scan.
        /// </summary>
        /// <returns>An updated scanner.</returns>
        public MethodScanner IncludeNonPublicMethods()
        {
            var result = Clone();
            result._includeNonPublicMethod = true;
            return result;
        }

        /// <summary>
        /// Includes only members of classes which implement <typeparamref name="T" /> in the scan.
        /// </summary>
        /// <typeparam name="T">The class or interface the results of the scan should implement.</typeparam>
        /// <returns>An updated scanner.</returns>
        public MethodScanner Implements<T>()
        {
            var result = Clone();
            result._classImplements.Add(typeof(T));
            return result;
        }

        /// <summary>
        /// Includes only members of classes which have an attribute <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of the attribute the class should have.</typeparam>
        /// <returns>An updated scanner.</returns>
        public MethodScanner HasClassAttribute<T>() where T : Attribute
        {
            var result = Clone();
            result._classAttributes.Add(typeof(T));
            return result;
        }

        /// <summary>
        /// Includes only methods which have an attribute <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of the attribute the method should have.</typeparam>
        /// <returns>An updated scanner.</returns>
        public MethodScanner HasAttribute<T>() where T : Attribute
        {
            var result = Clone();
            result._methodAttributes.Add(typeof(T));
            return result;
        }

        private bool ApplyTypeFilter(Type type)
        {
            return type.IsClass &&
                   !type.IsAbstract &&
                   _classImplements.All(i => i.IsAssignableFrom(type)) &&
                   _classAttributes.All(a => type.GetCustomAttribute(a) != null);
        }

        private bool ApplyMethodFilter(MethodInfo methodInfo)
        {
            return _methodAttributes.All(a => methodInfo.GetCustomAttribute(a) != null);
        }

        /// <summary>
        /// Runs the scan and provides the attribute <typeparamref name="TAttribute" /> in the results.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The found methods with their attribute of type <typeparamref name="TAttribute" />.</returns>
        public IEnumerable<(MethodInfo method, TAttribute attribute)> Scan<TAttribute>() where TAttribute : Attribute
        {
            return HasAttribute<TAttribute>()
                .Scan()
                .Select(method => (method, attribute: method.GetCustomAttribute<TAttribute>()));
        }

        /// <summary>
        /// Runs the scan.
        /// </summary>
        /// <returns>The found methods.</returns>
        public IEnumerable<MethodInfo> Scan()
        {
            var methods = _assemblies
                .SelectMany(a => a.GetTypes())
                .Where(ApplyTypeFilter)
                .SelectMany(t => t.GetMethods(MethodBindingFlags))
                .Where(ApplyMethodFilter)
                .ToArray();

            return methods;
        }

        private MethodScanner Clone()
        {
            return new MethodScanner
            {
                _assemblies = new List<Assembly>(_assemblies),
                _includeStaticMethod = _includeStaticMethod,
                _includeNonPublicMethod = _includeNonPublicMethod,
                _includeInstanceMethod = _includeInstanceMethod,
                _classImplements = new List<Type>(_classImplements),
                _classAttributes = new List<Type>(_classAttributes),
                _methodAttributes = new List<Type>(_methodAttributes)
            };
        }
    }
}