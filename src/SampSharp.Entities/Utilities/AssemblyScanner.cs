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
    /// Represents a utility for scanning for types and members with specific attributes in loaded assemblies.
    /// </summary>
    public sealed class AssemblyScanner
    {
        private List<Assembly> _assemblies = new List<Assembly>();
        private List<Type> _classAttributes = new List<Type>();
        private List<Type> _classImplements = new List<Type>();
        private bool _includeInstanceMembers = true;
        private bool _includeNonPublicMembers;
        private bool _includeStaticMembers;
        private List<Type> _memberAttributes = new List<Type>();
        private bool _includeAbstract;

        private BindingFlags MemberBindingFlags =>
            (_includeInstanceMembers ? BindingFlags.Instance : BindingFlags.Default) |
            (_includeStaticMembers ? BindingFlags.Static : BindingFlags.Default) |
            BindingFlags.Public |
            (_includeNonPublicMembers ? BindingFlags.NonPublic : BindingFlags.Default);

        /// <summary>
        /// Includes the specified <paramref name="assembly" /> in the scan.
        /// </summary>
        /// <param name="assembly">The assembly to include.</param>
        /// <returns>An updated scanner.</returns>
        public AssemblyScanner IncludeAssembly(Assembly assembly)
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
        public AssemblyScanner IncludeReferencedAssemblies(bool skipSystem = true)
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
        public AssemblyScanner IncludeAllAssemblies(bool skipSystem = true)
        {
            return IncludeEntryAssembly()
                .IncludeReferencedAssemblies(skipSystem);
        }

        /// <summary>
        /// Includes the entry assembly in the scan.
        /// </summary>
        /// <returns>An updated scanner.</returns>
        public AssemblyScanner IncludeEntryAssembly()
        {
            return IncludeAssembly(Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// Includes static members in the scan.
        /// </summary>
        /// <param name="exclusive">If set to <c>true</c>, only include static members in the scan.</param>
        /// <returns>An updated scanner.</returns>
        public AssemblyScanner IncludeStatic(bool exclusive)
        {
            var result = Clone();
            result._includeStaticMembers = true;
            result._includeInstanceMembers = !exclusive;
            return result;
        }

        /// <summary>
        /// Includes non-public members in the scan.
        /// </summary>
        /// <returns>An updated scanner.</returns>
        public AssemblyScanner IncludeNonPublicMembers()
        {
            var result = Clone();
            result._includeNonPublicMembers = true;
            return result;
        }
        
        /// <summary>
        /// Includes members of abstract classes in the scan.
        /// </summary>
        /// <returns>An updated scanner.</returns>
        public AssemblyScanner IncludeAbstract()
        {
            var result = Clone();
            result._includeAbstract = true;
            return result;
        }

        /// <summary>
        /// Includes only members of classes which implement <typeparamref name="T" /> in the scan.
        /// </summary>
        /// <typeparam name="T">The class or interface the results of the scan should implement.</typeparam>
        /// <returns>An updated scanner.</returns>
        public AssemblyScanner Implements<T>()
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
        public AssemblyScanner HasClassAttribute<T>() where T : Attribute
        {
            var result = Clone();
            result._classAttributes.Add(typeof(T));
            return result;
        }

        /// <summary>
        /// Includes only members which have an attribute <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of the attribute the member should have.</typeparam>
        /// <returns>An updated scanner.</returns>
        public AssemblyScanner HasAttribute<T>() where T : Attribute
        {
            var result = Clone();
            result._memberAttributes.Add(typeof(T));
            return result;
        }
        
        private bool ApplyTypeFilter(Type type)
        {
            return type.IsClass &&
                   (_includeAbstract || !type.IsAbstract) &&
                   _classImplements.All(i => i.IsAssignableFrom(type)) &&
                   _classAttributes.All(a => type.GetCustomAttribute(a) != null);
        }

        private bool ApplyMemberFilter(MemberInfo memberInfo)
        {
            return _memberAttributes.All(a => memberInfo.GetCustomAttribute(a) != null);
        }
        
        /// <summary>
        /// Runs the scan for methods and provides the attribute <typeparamref name="TAttribute" /> in the results.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The found methods with their attribute of type <typeparamref name="TAttribute" />.</returns>
        public IEnumerable<(Type type, TAttribute attribute)> ScanTypes<TAttribute>() where TAttribute : Attribute
        {
            return HasClassAttribute<TAttribute>()
                .ScanTypes()
                .Select(type => (type, attribute: type.GetCustomAttribute<TAttribute>()));
        }

        /// <summary>
        /// Runs the scan for methods.
        /// </summary>
        /// <returns>The found methods.</returns>
        public IEnumerable<Type> ScanTypes()
        {
            return _assemblies
                .SelectMany(a => a.GetTypes())
                .Where(ApplyTypeFilter)
                .ToArray();
        }

        /// <summary>
        /// Runs the scan for methods and provides the attribute <typeparamref name="TAttribute" /> in the results.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The found methods with their attribute of type <typeparamref name="TAttribute" />.</returns>
        public IEnumerable<(MethodInfo method, TAttribute attribute)> ScanMethods<TAttribute>() where TAttribute : Attribute
        {
            return HasAttribute<TAttribute>()
                .ScanMethods()
                .Select(method => (method, attribute: method.GetCustomAttribute<TAttribute>()));
        }

        /// <summary>
        /// Runs the scan for methods.
        /// </summary>
        /// <returns>The found methods.</returns>
        public IEnumerable<MethodInfo> ScanMethods()
        {
            return _assemblies
                .SelectMany(a => a.GetTypes())
                .Where(ApplyTypeFilter)
                .SelectMany(t => t.GetMethods(MemberBindingFlags))
                .Where(ApplyMemberFilter)
                .ToArray();
        }
        
        /// <summary>
        /// Runs the scan for fields and provides the attribute <typeparamref name="TAttribute" /> in the results.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The found fields with their attribute of type <typeparamref name="TAttribute" />.</returns>
        public IEnumerable<(FieldInfo field, TAttribute attribute)> ScanFields<TAttribute>() where TAttribute : Attribute
        {
            return HasAttribute<TAttribute>()
                .ScanFields()
                .Select(field => (field, attribute: field.GetCustomAttribute<TAttribute>()));
        }

        /// <summary>
        /// Runs the scan for fields.
        /// </summary>
        /// <returns>The found fields.</returns>
        public IEnumerable<FieldInfo> ScanFields()
        {
            return _assemblies
                .SelectMany(a => a.GetTypes())
                .Where(ApplyTypeFilter)
                .SelectMany(t => t.GetFields(MemberBindingFlags))
                .Where(ApplyMemberFilter)
                .ToArray();
        }

        private AssemblyScanner Clone()
        {
            return new AssemblyScanner
            {
                _assemblies = new List<Assembly>(_assemblies),
                _includeStaticMembers = _includeStaticMembers,
                _includeNonPublicMembers = _includeNonPublicMembers,
                _includeInstanceMembers = _includeInstanceMembers,
                _classImplements = new List<Type>(_classImplements),
                _classAttributes = new List<Type>(_classAttributes),
                _memberAttributes = new List<Type>(_memberAttributes),
                _includeAbstract = _includeAbstract
            };
        }
    }
}