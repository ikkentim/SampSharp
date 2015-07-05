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
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.API
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public partial class Native
    {
        private readonly string _format;
        private readonly int _handle;
        private readonly string _name;

        #region Constructors of Native

        /// <summary>
        ///     Initializes a new instance of the <see cref="Native" /> class.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <param name="parameterTypes">The parameter types of the native.</param>
        public Native(string name, params Type[] parameterTypes)
            : this(name, null, parameterTypes)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Native" /> class.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <param name="sizes">The sizes of the parameters which require size information.</param>
        /// <param name="parameterTypes">The parameter types of the native.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public Native(string name, int[] sizes, params Type[] parameterTypes)
        {
            if (name == null) throw new ArgumentNullException("name");

            _name = name;
            _format = string.Empty;

            if (parameterTypes == null || parameterTypes.Length == 0)
            {
                _handle = Native.Load(name, string.Empty, null);
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
                else if (param == typeof(string))
                    _format += "s";
                else if (param == typeof(float))
                    _format += "f";
                else if (param == typeof(int[]))
                {
                    lengthList.Add(i + 1);
                    _format += "a";
                }
                else if (param == typeof(float[]))
                {
                    lengthList.Add(i + 1);
                    _format += "v";
                }
                else if (param == typeof(int).MakeByRefType())
                    _format += "D";
                else if (param == typeof(string).MakeByRefType())
                {
                    lengthList.Add(i + 1);
                    _format += "S";
                }
                else if (param == typeof(float).MakeByRefType())
                    _format += "F";
                else if (param == typeof(int[]).MakeByRefType())
                {
                    lengthList.Add(i + 1);
                    _format += "A";
                }
                else if (param == typeof(float[]).MakeByRefType())
                {
                    lengthList.Add(i + 1);
                    _format += "V";
                }
            }

            _handle = Native.Load(name, _format,
                sizes == null || sizes.Length == 0 ? (lengthList.Count > 0 ? lengthList.ToArray() : null) : sizes);
        }

        #endregion

        #region Properties of Native

        /// <summary>
        ///     Gets the name of the native function.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        ///     Gets the handle of the native function.
        /// </summary>
        public int Handle
        {
            get { return _handle; }
        }

        #endregion

        #region Native Invoke Methods

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The return value of the native.</returns>
        public int Invoke(params object[] args)
        {
            return InvokeHandle(_handle, args);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <returns>The return value of the native.</returns>
        public int Invoke(__arglist)
        {
            return Invoke(CreateRefArray(__arglist));
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The return value of the native as a float.</returns>
        public float InvokeFloat(params object[] args)
        {
            return InvokeHandleAsFloat(_handle, args);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <returns>The return value of the native as a float.</returns>
        public float InvokeFloat(__arglist)
        {
            return InvokeFloat(CreateRefArray(__arglist));
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The return value of the native as a bool.</returns>
        public bool InvokeBool(params object[] args)
        {
            return InvokeHandleAsBool(_handle, args);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <returns>The return value of the native as a bool.</returns>
        public bool InvokeBool(__arglist)
        {
            return InvokeBool(CreateRefArray(__arglist));
        }

        #endregion

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
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        public static bool Exists(string name)
        {
            return Interop.NativeExists(name);
        }


        private static int Load(string name, string format, int[] sizes)
        {
            return Interop.LoadNative(name, format, sizes);
        }

        #region Native Handle Invokers

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

        #endregion

        #region Native Autoloader Methods

        public static void Load(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            Load(type.Assembly);
        }

        public static void Load<T>()
        {
            Load(typeof(T));
        }

        public static void Load(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsInterface)
                    continue;

                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                    )
                {
                    if (!field.IsStatic || !typeof(Delegate).IsAssignableFrom(field.FieldType))
                        continue;

                    var @delegate = field.FieldType;
                    var attribute = field.GetCustomAttribute<NativeAttribute>();

                    if (attribute == null)
                        continue;

                    if (field.GetValue(null) == null)
                    {
                        var nativeFunction = new Native(attribute.Name, attribute.Sizes,
                            @delegate.GetMethod("Invoke").GetParameters().Select(p => p.ParameterType).ToArray());

                        field.SetValue(null, nativeFunction.GenerateInvoker(@delegate));
                    }
                }
            }
        }

        private Delegate GenerateInvoker(Type delegateType)
        {
            var invokeMethod = delegateType.GetMethod("Invoke");

            if (invokeMethod == null)
                throw new ArgumentException("type is not a delegate", "type");

            var parameters = invokeMethod.GetParameters();
            var parameterTypes = parameters.Select(p => p.ParameterType).ToArray();
            var returnType = invokeMethod.ReturnType;

            // Pick the right invoker.
            MethodInfo invokeMethodInfo;
            if (returnType == typeof(int))
                invokeMethodInfo = typeof(Native).GetMethod("InvokeHandle", BindingFlags.NonPublic | BindingFlags.Static);
            else if (returnType == typeof(bool))
                invokeMethodInfo = typeof(Native).GetMethod("InvokeHandleAsBool", BindingFlags.NonPublic | BindingFlags.Static);
            else if (returnType == typeof(float))
                invokeMethodInfo = typeof(Native).GetMethod("InvokeHandleAsFloat", BindingFlags.NonPublic | BindingFlags.Static);
            else
                throw new Exception("Unsupported return type of delegate");

            if (invokeMethodInfo == null)
                throw new Exception("Native invoker is missing");

            // Generate the handler method.
            var dynamicMethod = new DynamicMethod("DynamicCall", returnType, parameterTypes, typeof(Native));
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();

            // Create an array of objects and store it in a local
            LocalBuilder args = ilGenerator.DeclareLocal(typeof(object[]));
            ilGenerator.Emit(OpCodes.Ldc_I4_S, parameterTypes.Length);
            ilGenerator.Emit(OpCodes.Newarr, typeof(object));
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

        #endregion
    }
}