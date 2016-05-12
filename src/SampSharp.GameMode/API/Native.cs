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
using System.Runtime.InteropServices;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.API
{
    /// <summary>
    ///     Represents a native function.
    /// </summary>
    public class Native : INative
    {
        private static INativeLoader _nativeLoader = new DefaultNativeLoader();
        private static readonly List<string> LoadedAssemblies = new List<string>();

        private readonly int _handle;
        private readonly Type[] _parameterTypes;

        internal Native(string name, int[] sizes, params Type[] parameterTypes)
        {
            _parameterTypes = parameterTypes;
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;

            if (parameterTypes == null || parameterTypes.Length == 0)
            {
                _handle = Interop.LoadNative(name, string.Empty, null);
                return;
            }

            // Compute the parameter format string.
            string format;
            var lengthIndices = ComputeFormatString(parameterTypes, out format);

            _handle = Interop.LoadNative(name, format,
                (sizes?.Length ?? 0) == 0 ? lengthIndices : sizes);
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
        public virtual string Name { get; }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a float.</returns>
        public virtual float InvokeFloat(params object[] arguments)
        {
            return InvokeHandleAsFloat(_handle, arguments, _parameterTypes);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a bool.</returns>
        public virtual bool InvokeBool(params object[] arguments)
        {
            return InvokeHandleAsBool(_handle, arguments, _parameterTypes);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native.</returns>
        public virtual int Invoke(params object[] arguments)
        {
            return InvokeHandle(_handle, arguments, _parameterTypes);
        }

        /// <summary>
        ///     Generates an invoker delegate for the function this instance represents.
        /// </summary>
        /// <param name="delegateType">Type of the delegate.</param>
        /// <returns>The generated invoker delegate.</returns>
        /// <exception cref="ArgumentException">type is not a delegate;type.</exception>
        /// <exception cref="Exception">Unsupported return type of delegate or Native invoker is missing.</exception>
        public virtual Delegate GenerateInvoker(Type delegateType)
        {
            var invokeMethod = delegateType.GetMethod("Invoke");

            if (invokeMethod == null)
                throw new ArgumentException("type is not a delegate", nameof(delegateType));

            if (_parameterTypes.Length != invokeMethod.GetParameters().Length ||
                _parameterTypes.Zip(invokeMethod.GetParameters(), (n, d) => n == d.ParameterType).Any(v => !v))
                throw new ArgumentException("Invalid parameter types", nameof(delegateType));

            var parameterCount = _parameterTypes.Length;
            var returnType = invokeMethod.ReturnType;

            // Pick the right invoke method based on the return type of the delegate.
            MethodInfo invokeMethodInfo;
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Static;
            if (returnType == typeof (int))
                invokeMethodInfo = typeof (Native).GetMethod("InvokeHandle", flags);
            else if (returnType == typeof (bool))
                invokeMethodInfo = typeof (Native).GetMethod("InvokeHandleAsBool", flags);
            else if (returnType == typeof (float))
                invokeMethodInfo = typeof (Native).GetMethod("InvokeHandleAsFloat", flags);
            else
                throw new Exception("Unsupported return type of delegate");

            if (invokeMethodInfo == null)
                throw new Exception("Native invoker is missing");

            // Create a dynamic method for the delegate implementation.
            var dynamicMethod = new DynamicMethod("DynamicCall", returnType, _parameterTypes, typeof (Native));
            var ilGenerator = dynamicMethod.GetILGenerator();


            // Create an instance of object[].
            ilGenerator.Emit(OpCodes.Ldc_I4_S, parameterCount);
            ilGenerator.Emit(OpCodes.Newarr, typeof (object));

            // Store the newly created array to the args local.
            var argsLocal = ilGenerator.DeclareLocal(typeof (object[]));
            ilGenerator.Emit(OpCodes.Stloc, argsLocal);

            // Create an instance of Type[].
            ilGenerator.Emit(OpCodes.Ldc_I4_S, parameterCount);
            ilGenerator.Emit(OpCodes.Newarr, typeof (Type));

            // Store the newly created array to the argTypes local.
            var argTypesLocal = ilGenerator.DeclareLocal(typeof (Type[]));
            ilGenerator.Emit(OpCodes.Stloc, argTypesLocal);

            // Generate a pass-trough for every parameter of the native.
            for (var index = 0; index < parameterCount; index++)
            {
                var isByRef = _parameterTypes[index].IsByRef;
                var type = isByRef
                    ? _parameterTypes[index].GetElementType()
                    : _parameterTypes[index];

                // Load the arg types array onto the stack.
                ilGenerator.Emit(OpCodes.Ldloc, argTypesLocal);

                // Push the parameter index onto the stack.
                ilGenerator.Emit(OpCodes.Ldc_I4, index);

                // Load the parameter type at the current parameter index onto the stack.
                if (isByRef)
                {
                    ilGenerator.Emit(OpCodes.Ldtoken, type);
                    ilGenerator.Emit(OpCodes.Call, typeof (Type).GetMethod("GetTypeFromHandle"));
                    ilGenerator.Emit(OpCodes.Call, typeof (Type).GetMethod("MakeByRefType"));
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Ldtoken, type);
                    ilGenerator.Emit(OpCodes.Call, typeof (Type).GetMethod("GetTypeFromHandle"));
                }

                // Replace the element at the current index within the parameters array with the type.
                ilGenerator.Emit(OpCodes.Stelem_Ref);

                // If this parameter is of an output type no pass-trough is required; skip it.
                if (isByRef)
                    continue;

                // Load the args array onto the stack.
                ilGenerator.Emit(OpCodes.Ldloc, argsLocal);

                // Push the parameter index onto the stack.
                ilGenerator.Emit(OpCodes.Ldc_I4, index);

                // Load the argument at the current parameter index onto the stack.
                ilGenerator.Emit(OpCodes.Ldarg, index);

                // If the parameter is a value type, box it.
                if (type.IsValueType)
                    ilGenerator.Emit(OpCodes.Box, type);

                // Replace the element at the current index within the parameters array with the argument at 
                // the current index.
                ilGenerator.Emit(OpCodes.Stelem_Ref);
            }

            // Push the handle of the native onto the stack.
            ilGenerator.Emit(OpCodes.Ldc_I4, _handle);

            // Load the args array onto the stack.
            ilGenerator.Emit(OpCodes.Ldloc, argsLocal);

            // Load the args array onto the stack.
            ilGenerator.Emit(OpCodes.Ldloc, argTypesLocal);

            // Invoke the native invocation method.
            if (invokeMethodInfo.IsFinal || !invokeMethodInfo.IsVirtual)
                ilGenerator.Emit(OpCodes.Call, invokeMethodInfo);
            else
                ilGenerator.Emit(OpCodes.Callvirt, invokeMethodInfo);

            // Generate a pass-back for every output parameter of the native.
            for (var index = 0; index < parameterCount; index++)
            {
                var isByRef = _parameterTypes[index].IsByRef;
                var type = isByRef
                    ? _parameterTypes[index].GetElementType()
                    : _parameterTypes[index];

                // If this parameter is not of an output or reference type no pass-back is required; skip it.
                if (!isByRef)
                    continue;

                // Load the argument at the current parameter index onto the stack.
                ilGenerator.Emit(OpCodes.Ldarg, index);

                // Load the args array onto the stack.
                ilGenerator.Emit(OpCodes.Ldloc, argsLocal);

                // Push the current parameter index onto the stack.
                ilGenerator.Emit(OpCodes.Ldc_I4, index);

                // Load the element at the specified index from the arguments local.
                ilGenerator.Emit(OpCodes.Ldelem_Ref);

                // Store the value in the reference argument at the current parameter index.
                if (type == typeof (int))
                {
                    ilGenerator.Emit(OpCodes.Unbox_Any, typeof (int));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (type == typeof (bool))
                {
                    ilGenerator.Emit(OpCodes.Unbox_Any, typeof (bool));
                    ilGenerator.Emit(OpCodes.Stind_I4);
                }
                else if (type == typeof (float))
                {
                    ilGenerator.Emit(OpCodes.Unbox_Any, typeof (float));
                    ilGenerator.Emit(OpCodes.Stind_R4);
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Stind_Ref);
                }
            }

            // Return to the caller.
            ilGenerator.Emit(OpCodes.Ret);

            return dynamicMethod.CreateDelegate(delegateType);
        }

        private static char GetTypeFormatChar(Type type)
        {
            if (type == typeof (int) || type == typeof (bool) || type == typeof (float))
                return 'd';
            if (type == typeof (int[]) || type == typeof (bool[]) || type == typeof (float[]))
                return 'a';
            if (type == typeof (string))
                return 's';

            throw new ApplicationException("Invalid native delegate argument type");
        }

        private static int[] ComputeFormatString(Type[] types, out string format)
        {
            var lengthIndices = new List<int>();
            format = string.Empty;

            for (var i = 0; i < types.Length; i++)
            {
                var c = types[i].IsByRef
                    ? char.ToUpper(GetTypeFormatChar(types[i].GetElementType()))
                    : GetTypeFormatChar(types[i]);

                if (c == 'S' || c == 'a' || c == 'A')
                    lengthIndices.Add(i + 1);

                format += c;
            }

            return lengthIndices.ToArray();
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
            if (type == null) throw new ArgumentNullException(nameof(type));
            LoadDelegates(type.Assembly);
        }

        /// <summary>
        ///     Loads the delegate fields annotated with a <see cref="NativeAttribute" /> attribute within the specified
        ///     <paramref name="assembly" /> with calls to the desired native function.
        /// </summary>
        /// <param name="assembly">The assembly of which to load the delegate fields.</param>
        public static void LoadDelegates(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            if (LoadedAssemblies.Contains(assembly.FullName))
            {
                FrameworkLog.WriteLine(FrameworkMessageLevel.Debug,
                    $"Native delegates in {assembly.FullName} have already been loaded. Skipping...");
                return;
            }

            FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, $"Loading native delegates in {assembly.FullName}...");

            LoadedAssemblies.Add(assembly.FullName);

            // Loop trough every type in the assembly and load all native delegate fields it contains.
            foreach (var type in assembly.GetTypes())
            {
                // Do not process interfaces or types with generic parameters.
                if (type.IsInterface || type.ContainsGenericParameters)
                    continue;

                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                    )
                {
                    // Skip non-static or non-delegate fields.
                    if (!field.IsStatic || !typeof (Delegate).IsAssignableFrom(field.FieldType))
                        continue;

                    var delegateType = field.FieldType;
                    var attribute = field.GetCustomAttribute<NativeAttribute>();

                    // Skip fields without Native attribute or with set values.
                    if (attribute == null || field.GetValue(null) != null)
                        continue;

                    // Load the native.
                    var parameterTypes = delegateType.GetMethod("Invoke")
                        .GetParameters()
                        .Select(p => p.ParameterType)
                        .ToArray();

                    var nativeFunction = Load(attribute.Name, attribute.Lengths, parameterTypes);
                    if (nativeFunction == null)
                    {
                        FrameworkLog.WriteLine(FrameworkMessageLevel.Warning, $"Could not load native '{attribute.Name}'");
                        continue;
                    }

                    field.SetValue(null, nativeFunction.GenerateInvoker(delegateType));
                }
            }
        }

        private static float InvokeHandleAsFloat(int handle, object[] args, Type[] types)
        {
            return ConvertIntToFloat(InvokeHandle(handle, args, types));
        }

        private static bool InvokeHandleAsBool(int handle, object[] args, Type[] types)
        {
            return ConvertIntToBool(InvokeHandle(handle, args, types));
        }

        private static int InvokeHandleCast(int handle, object[] args, Type[] types)
        {
            var baseArgs = new object[args.Length];

            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i].IsByRef ? types[i].GetElementType() : types[i];

                if (type == typeof (float))
                    baseArgs[i] = Cast<float, int>(ConvertFloatToInt, args[i]);
                else if (type == typeof (bool))
                    baseArgs[i] = Cast<bool, int>(ConvertBoolToInt, args[i]);
                else if (type == typeof (float[]))
                    baseArgs[i] = Cast<float[], int[]>(ConvertFloatArrayToIntArray, args[i]);
                else if (type == typeof (bool[]))
                    baseArgs[i] = Cast<bool[], int[]>(ConvertBoolArrayToIntArray, args[i]);
                else
                    baseArgs[i] = args[i];
            }

            var result = Interop.InvokeNative(handle, baseArgs);

            for (var i = 0; i < types.Length; i++)
            {
                if (!types[i].IsByRef)
                    continue;

                var type = types[i].GetElementType();

                if (type == typeof (float))
                    args[i] = Cast<int, float>(ConvertIntToFloat, baseArgs[i]);
                else if (type == typeof (bool))
                    args[i] = Cast<int, bool>(ConvertIntToBool, baseArgs[i]);
                else if (type == typeof (float[]))
                    args[i] = Cast<int[], float[]>(ConvertIntArrayToFloatArray, baseArgs[i]);
                else if (type == typeof (bool[]))
                    args[i] = Cast<int[], bool[]>(ConvertIntArrayToBoolArray, baseArgs[i]);
                else
                    args[i] = baseArgs[i];
            }

            return result;
        }

        private static object Cast<T1, T2>(Func<T1, T2> func, object input)
        {
            return input is T1 ? (object) func((T1) input) : null;
        }

        private static int InvokeHandle(int handle, object[] args, Type[] types)
        {
            if (Sync.IsRequired)
            {
                FrameworkLog.WriteLine(FrameworkMessageLevel.Debug,
                    $"Call to native handle 0x{handle.ToString("X")} is being synchronized.");
                return Sync.RunSync(() => InvokeHandleCast(handle, args, types));
            }

            return InvokeHandleCast(handle, args, types);
        }

        #region Overrides of Object

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return $"{Name}#0x{_handle.ToString("X")}";
        }

        #endregion

        private static float ConvertIntToFloat(int value)
        {
            return new ValueUnion {Integer = value}.Float;
        }

        private static int ConvertFloatToInt(float value)
        {
            return new ValueUnion {Float = value}.Integer;
        }

        private static float[] ConvertIntArrayToFloatArray(int[] value)
        {
            return value.Select(ConvertIntToFloat).ToArray();
        }

        private static int[] ConvertFloatArrayToIntArray(float[] value)
        {
            return value.Select(ConvertFloatToInt).ToArray();
        }

        private static int ConvertBoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        private static bool ConvertIntToBool(int value)
        {
            return value != 0;
        }

        private static bool[] ConvertIntArrayToBoolArray(int[] value)
        {
            return value.Select(ConvertIntToBool).ToArray();
        }

        private static int[] ConvertBoolArrayToIntArray(bool[] value)
        {
            return value.Select(ConvertBoolToInt).ToArray();
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct ValueUnion
        {
            [FieldOffset(0)] public int Integer;
            [FieldOffset(0)] public float Float;
        }
    }
}
