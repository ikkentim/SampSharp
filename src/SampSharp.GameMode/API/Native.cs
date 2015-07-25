// SampSharp
// Copyright 2015 Tim Potze
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.GameMode.API
{
    /// <summary>
    ///     Represents a native function.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public partial class Native : INative
    {
        private static INativeLoader _nativeLoader = new DefaultNativeLoader();
        private static readonly List<string> LoadedAssemblies = new List<string>();

        private readonly string _format;
        private readonly int _handle;
        private readonly string _name;

        internal Native(string name, int[] sizes, params Type[] parameterTypes)
        {
            if (name == null) throw new ArgumentNullException("name");

            _name = name;
            _format = string.Empty;

            if (parameterTypes == null || parameterTypes.Length == 0)
            {
                _handle = Interop.LoadNative(name, string.Empty, null);
                return;
            }

            var lengthList = new List<int>();
            for (var i = 0; i < parameterTypes.Length; i++)
            {
                var param = parameterTypes[i];
                if (param == typeof (int))
                    _format += "d";
                else if (param == typeof (bool))
                    _format += "b";
                else if (param == typeof (string))
                    _format += "s";
                else if (param == typeof (float))
                    _format += "f";
                else if (param == typeof (int[]))
                {
                    lengthList.Add(i + 1);
                    _format += "a";
                }
                else if (param == typeof (float[]))
                {
                    lengthList.Add(i + 1);
                    _format += "v";
                }
                else if (param == typeof (int).MakeByRefType())
                    _format += "D";
                else if (param == typeof(bool).MakeByRefType())
                    _format += "B";
                else if (param == typeof (string).MakeByRefType())
                {
                    lengthList.Add(i + 1);
                    _format += "S";
                }
                else if (param == typeof (float).MakeByRefType())
                    _format += "F";
                else if (param == typeof (int[]).MakeByRefType())
                {
                    lengthList.Add(i + 1);
                    _format += "A";
                }
                else if (param == typeof (float[]).MakeByRefType())
                {
                    lengthList.Add(i + 1);
                    _format += "V";
                }
                else
                {
                    throw new ApplicationException("Invalid native delegate argument type");
                }
            }

            _handle = Interop.LoadNative(name, _format,
                sizes == null || sizes.Length == 0 ? (lengthList.Count > 0 ? lengthList.ToArray() : null) : sizes);
        }

        /// <summary>
        ///     Gets or sets the native loader.
        /// </summary>
        public static INativeLoader NativeLoader
        {
            get { return _nativeLoader; }
            set { if (value != null) _nativeLoader = value; }
        }

        /// <summary>
        ///     Gets the name of the native function.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
        }

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native.</returns>
        public virtual int Invoke(params object[] arguments)
        {
            return InvokeHandle(_handle, arguments);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <returns>The return value of the native.</returns>
        public virtual int Invoke(__arglist)
        {
            return Invoke(CreateRefArray(__arglist));
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a float.</returns>
        public virtual float InvokeFloat(params object[] arguments)
        {
            return InvokeHandleAsFloat(_handle, arguments);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <returns>The return value of the native as a float.</returns>
        public virtual float InvokeFloat(__arglist)
        {
            return InvokeFloat(CreateRefArray(__arglist));
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a bool.</returns>
        public virtual bool InvokeBool(params object[] arguments)
        {
            return InvokeHandleAsBool(_handle, arguments);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <returns>The return value of the native as a bool.</returns>
        public virtual bool InvokeBool(__arglist)
        {
            return InvokeBool(CreateRefArray(__arglist));
        }

        /// <summary>
        ///     Generates an invoker delegate for the function this instance represents.
        /// </summary>
        /// <param name="delegateType">Type of the delegate.</param>
        /// <returns>The generated invoker delegate.</returns>
        /// <exception cref="System.ArgumentException">type is not a delegate;type.</exception>
        /// <exception cref="System.Exception">Unsupported return type of delegate or Native invoker is missing.</exception>
        public virtual Delegate GenerateInvoker(Type delegateType)
        {
            var invokeMethod = delegateType.GetMethod("Invoke");

            if (invokeMethod == null)
                throw new ArgumentException("type is not a delegate", "type");

            // TODO: Verify format.

            var parameters = invokeMethod.GetParameters();
            var parameterTypes = parameters.Select(p => p.ParameterType).ToArray();
            var returnType = invokeMethod.ReturnType;

            // Pick the right invoker.
            MethodInfo invokeMethodInfo;
            if (returnType == typeof (int))
                invokeMethodInfo = typeof (Native).GetMethod("InvokeHandle",
                    BindingFlags.NonPublic | BindingFlags.Static);
            else if (returnType == typeof (bool))
                invokeMethodInfo = typeof (Native).GetMethod("InvokeHandleAsBool",
                    BindingFlags.NonPublic | BindingFlags.Static);
            else if (returnType == typeof (float))
                invokeMethodInfo = typeof (Native).GetMethod("InvokeHandleAsFloat",
                    BindingFlags.NonPublic | BindingFlags.Static);
            else
                throw new Exception("Unsupported return type of delegate");

            if (invokeMethodInfo == null)
                throw new Exception("Native invoker is missing");

            // Generate the handler method.
            var dynamicMethod = new DynamicMethod("DynamicCall", returnType, parameterTypes, typeof (Native));
            var ilGenerator = dynamicMethod.GetILGenerator();

            // Create an array of objects and store it in a local
            var args = ilGenerator.DeclareLocal(typeof (object[]));
            ilGenerator.Emit(OpCodes.Ldc_I4_S, parameterTypes.Length);
            ilGenerator.Emit(OpCodes.Newarr, typeof (object));
            ilGenerator.Emit(OpCodes.Stloc, args);

            if (parameterTypes.Length > 0)
            {
                ilGenerator.Emit(OpCodes.Ldloc, args);

                for (var index = 0; index < parameterTypes.Length; index++)
                {
                    if (parameters[index].IsOut)
                        continue;
                    ilGenerator.Emit(OpCodes.Ldloc, args);
                    ilGenerator.Emit(OpCodes.Ldc_I4, index);
                    ilGenerator.Emit(OpCodes.Ldarg, index);
                    if (parameterTypes[index].IsValueType)
                        ilGenerator.Emit(OpCodes.Box, parameterTypes[index]);
                    ilGenerator.Emit(OpCodes.Stelem_Ref);
                }

                ilGenerator.Emit(OpCodes.Stloc, args);
            }

            ilGenerator.Emit(OpCodes.Ldc_I4, _handle);
            ilGenerator.Emit(OpCodes.Ldloc, args);

            if (invokeMethodInfo.IsFinal || !invokeMethodInfo.IsVirtual)
                ilGenerator.Emit(OpCodes.Call, invokeMethodInfo);
            else
                ilGenerator.Emit(OpCodes.Callvirt, invokeMethodInfo);

            for (var index = 0; index < parameterTypes.Length; index++)
            {
                if (!parameterTypes[index].IsByRef)
                    continue;

                if (parameters[index].IsOut || parameters[index].ParameterType.IsByRef)
                {
                    ilGenerator.Emit(OpCodes.Ldarg, index);
                    ilGenerator.Emit(OpCodes.Ldloc, args);
                    ilGenerator.Emit(OpCodes.Ldc_I4, index);
                    ilGenerator.Emit(OpCodes.Ldelem_Ref);
                    ilGenerator.Emit(OpCodes.Stind_Ref);
                }
            }

            ilGenerator.Emit(OpCodes.Ret);

            return dynamicMethod.CreateDelegate(delegateType);
        }

        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        public static bool Exists(string name)
        {
            return _nativeLoader.Exists(name);
        }

        /// <summary>
        ///     Loads a native with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The loaded native.</returns>
        public static INative Load(string name, params Type[] parameterTypes)
        {
            return Load(name, null, parameterTypes);
        }

        /// <summary>
        ///     Loads a native with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sizes">The references to the parameter which contains the size of array parameters.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The loaded native.</returns>
        public static INative Load(string name, int[] sizes, params Type[] parameterTypes)
        {
            return _nativeLoader.Load(name, sizes, parameterTypes);
        }

        private object[] CreateRefArray(RuntimeArgumentHandle handle)
        {
            var iterator = new ArgIterator(handle);
            var len = iterator.GetRemainingCount();

            if ((_format != null && _format.Length != len) || (_format == null && len != 0))
                throw new ArgumentException("Invalid arguments");

            var args = new object[len];
            for (var idx = 0; idx < len; idx++)
                args[idx] = TypedReference.ToObject(iterator.GetNextArg());

            return args;
        }

        /// <summary>
        ///     Loads the delegate fields annotated with a <see cref="NativeAttribute" /> attribute within the assembly of the
        ///     specified type <typeparamref name="T" /> with calls to the desired native function.
        /// </summary>
        /// <typeparam name="T">A type within the assembly of which to load the delegate fields.</typeparam>
        public static void LoadDelegates<T>()
        {
            LoadDelegates(typeof (T));
        }

        /// <summary>
        ///     Loads the delegate fields annotated with a <see cref="NativeAttribute" /> attribute within the assembly of the
        ///     specified <paramref name="type" /> with calls to the desired native function.
        /// </summary>
        /// <param name="type">A type within the assembly of which to load the delegate fields.</param>
        public static void LoadDelegates(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            LoadDelegates(type.Assembly);
        }

        /// <summary>
        ///     Loads the delegate fields annotated with a <see cref="NativeAttribute" /> attribute within the specified
        ///     <paramref name="assembly" /> with calls to the desired native function.
        /// </summary>
        /// <param name="assembly">The assembly of which to load the delegate fields.</param>
        public static void LoadDelegates(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            if (LoadedAssemblies.Contains(assembly.FullName))
            {
                FrameworkLog.WriteLine(FrameworkMessageLevel.Debug,
                    "Native delegates in {0} have already been loaded. Skipping...", assembly.FullName);

                return;
            }

            FrameworkLog.WriteLine(FrameworkMessageLevel.Debug,
                "Loading native delegates in {0}...", assembly.FullName);

            LoadedAssemblies.Add(assembly.FullName);

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsInterface)
                    continue;

                if (type.ContainsGenericParameters)
                    continue;

                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                    )
                {
                    if (!field.IsStatic || !typeof (Delegate).IsAssignableFrom(field.FieldType))
                        continue;

                    var @delegate = field.FieldType;
                    var attribute = field.GetCustomAttribute<NativeAttribute>();

                    if (attribute == null)
                        continue;

                    if (field.GetValue(null) == null)
                    {
                        var nativeFunction = Load(attribute.Name, attribute.Lengths,
                            @delegate.GetMethod("Invoke").GetParameters().Select(p => p.ParameterType).ToArray());

                        if (nativeFunction != null)
                            field.SetValue(null, nativeFunction.GenerateInvoker(@delegate));
                        else
                        {
                            FrameworkLog.WriteLine(FrameworkMessageLevel.Warning, "Could not load native '{0}'",
                                attribute.Name);
                        }
                    }
                }
            }
        }

        private static float InvokeHandleAsFloat(int handle, object[] args)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(InvokeHandle(handle, args)), 0);
        }

        private static int InvokeHandle(int handle, object[] args)
        {
            return Interop.InvokeNative(handle, args);
        }

        private static bool InvokeHandleAsBool(int handle, object[] args)
        {
            return InvokeHandle(handle, args) != 0;
        }

        #region Overrides of Object

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}#{1}", _name, _handle);
        }

        #endregion
    }
}