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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SampSharp.GameMode.Natives
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NativeAttribute : Attribute
    {
        public NativeAttribute(string name) : this(name, null)
        {
        }

        public NativeAttribute(string name, params int[] sizes)
        {
            if (name == null) throw new ArgumentNullException("name");
            Name = name;
            Sizes = sizes;
        }

        public string Name { get; private set; }
        public int[] Sizes { get; private set; }
    }

    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int LoadNative(string name, string format, int[] sizes);

        [MethodImpl((MethodImplOptions.InternalCall))]
        public static extern int InvokeNative(int handle, object[] args);

        [MethodImpl((MethodImplOptions.InternalCall))]
        public static extern float InvokeNativeFloat(int handle, object[] args);

        public static bool InvokeNativeBool(int handle, object[] args)
        {
            return InvokeNative(handle, args) != 0;
        }

        /// <summary>
        ///     Utility method. Checks whether current thread is main thread.
        /// </summary>
        /// <returns>
        ///     True if current thread is main thread; False otherwise.
        /// </returns>
        /// <remarks>
        ///     This method can be used for debugging purposes. In general,
        ///     comparing <see cref="Thread.CurrentThread" /> works just as well.
        /// </remarks>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsMainThread();

        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool NativeExists(string name);

        /// <summary>
        ///     Registers an extension to the plugin.
        /// </summary>
        /// <param name="extension">The extension to register.</param>
        /// <returns>
        ///     True on success, False otherwise.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RegisterExtension(object extension);

        public static void LoadNatives(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            LoadNatives(type.Assembly);
        }

        public static void LoadNatives<T>()
        {
            LoadNatives(typeof(T));
        }

        public static void LoadNatives(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsInterface)
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
                        var nativeFunction = new NativeFunction(attribute.Name, attribute.Sizes,
                            @delegate.GetMethod("Invoke").GetParameters().Select(p => p.ParameterType).ToArray());

                        field.SetValue(null, CreateNativeCallDelegate(nativeFunction.Handle, @delegate));
                    }
                }
            }
        }

        private static Delegate CreateNativeCallDelegate(int handle, Type type)
        {
            var invokeMethod = type.GetMethod("Invoke");

            if(invokeMethod == null)
                throw new ArgumentException("type is not a delegate", "type");

            var parameters = invokeMethod.GetParameters();
            var parameterTypes = parameters.Select(p => p.ParameterType).ToArray();
            var returnType = invokeMethod.ReturnType;
            
            // Pick the right invoker.
            MethodInfo invokeMethodInfo;
            if (returnType == typeof (int))
                invokeMethodInfo = typeof(Native).GetMethod("InvokeNative");
            else if (returnType == typeof(bool))
                invokeMethodInfo = typeof(Native).GetMethod("InvokeNativeBool");
            else if (returnType == typeof (float))
                invokeMethodInfo = typeof(Native).GetMethod("InvokeNativeFloat");
            else
                throw new Exception("Unsupported return type of delegate");

            if (invokeMethodInfo == null)
                throw new Exception("Native invoker is missing");
            
            // Generate the handler method.
            var dynamicMethod = new DynamicMethod("DynamicCall", returnType, parameterTypes,
                invokeMethod.DeclaringType ?? typeof (Native));
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

            ilGenerator.Emit(OpCodes.Ldc_I4, handle);
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

            return dynamicMethod.CreateDelegate(type);
        }

    }
}