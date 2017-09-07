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
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.Core.Natives.NativeObjects
{
    /// <summary>
    ///     Represents a native method IL generator. This class can be used to generate a method for invoking a specific
    ///     native.
    /// </summary>
    public class NativeILGenerator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeILGenerator" /> class.
        /// </summary>
        /// <param name="native">The native.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="returnType">Type of the return value.</param>
        /// <exception cref="ArgumentNullException">Thrown if native, parameterTypes or returnType is null.</exception>
        public NativeILGenerator(INative native, Type[] parameterTypes, Type returnType)
        {
            ParameterTypes = parameterTypes ?? throw new ArgumentNullException(nameof(parameterTypes));
            Native = native ?? throw new ArgumentNullException(nameof(native));
            ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        }

        /// <summary>
        ///     Gets the parameter types.
        /// </summary>
        public Type[] ParameterTypes { get; }

        /// <summary>
        ///     Gets the native.
        /// </summary>
        protected INative Native { get; }

        /// <summary>
        ///     Gets the type of the return value.
        /// </summary>
        protected Type ReturnType { get; }

        /// <summary>
        ///     Gets the handle invoker method.
        /// </summary>
        /// <returns>The handle invoker method.</returns>
        /// <exception cref="Exception">Thrown if unsupported return type of method or native invoker is missing.</exception>
        protected virtual MethodInfo GetHandleInvokerMethod()
        {
            // Pick the right invoke method based on the return type of the delegate.
            MethodInfo result;
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Static;
            if (ReturnType == typeof(int))
                result = typeof(NativeHandleInvokers).GetTypeInfo().GetMethod(nameof(NativeHandleInvokers.InvokeHandle), flags);
            else if (ReturnType == typeof(bool))
                result = typeof(NativeHandleInvokers).GetTypeInfo().GetMethod(nameof(NativeHandleInvokers.InvokeHandleAsBool), flags);
            else if (ReturnType == typeof(float))
                result = typeof(NativeHandleInvokers).GetTypeInfo().GetMethod(nameof(NativeHandleInvokers.InvokeHandleAsFloat), flags);
            else if (ReturnType == typeof(void))
                result = typeof(NativeHandleInvokers).GetTypeInfo().GetMethod(nameof(NativeHandleInvokers.InvokeHandleAsVoid), flags);
            else
                throw new Exception("Unsupported return type of method");

            if (result == null)
                throw new Exception("Native invoker is missing");

            return result;
        }

        /// <summary>
        ///     Emits opcodes to the specified IL Generator to generate an array for the parameters. The array is stored in the
        ///     local stored in the returned lcoal builder.
        /// </summary>
        /// <param name="il">The il generator.</param>
        /// <returns>The local for the arguments array.</returns>
        protected virtual LocalBuilder GenerateArgsArray(ILGenerator il)
        {
            // Create an instance of object[].
            il.Emit(OpCodes.Ldc_I4_S, ParameterTypes.Length);
            il.Emit(OpCodes.Newarr, typeof(object));

            // Store the newly created array to the args local.
            var result = il.DeclareLocal(typeof(object[]));
            il.Emit(OpCodes.Stloc, result);

            return result;
        }

        /// <summary>
        ///     Returns the native argument index for the specified method argument index.
        /// </summary>
        /// <param name="index">The method argument index.</param>
        /// <returns>The native argument index for the specified method argument index.</returns>
        protected virtual int NativeArgIndexToMethodArgIndex(int index)
        {
            return index;
        }

        /// <summary>
        ///     Emits opcodes to the specified IL Generator to store all input arguments in the arguments array local.
        /// </summary>
        /// <param name="il">The il generator.</param>
        /// <param name="argsLocal">The arguments local.</param>
        protected virtual void GenerateInvokeInputCode(ILGenerator il, LocalBuilder argsLocal)
        {
            // Generate a pass-trough for every parameter of the native.
            for (var index = 0; index < ParameterTypes.Length; index++)
            {
                var isByRef = ParameterTypes[index].IsByRef;
                var type = isByRef
                    ? ParameterTypes[index].GetElementType()
                    : ParameterTypes[index];

                var argIndex = NativeArgIndexToMethodArgIndex(index);

                if (argIndex < 0)
                    continue;

                // If this parameter is of an output type no pass-trough is required; skip it.
                if (isByRef)
                    continue;

                // Load the args array onto the stack.
                il.Emit(OpCodes.Ldloc, argsLocal);

                // Push the parameter index onto the stack.
                il.Emit(OpCodes.Ldc_I4, index);

                // Load the argument at the current parameter index onto the stack.
                il.Emit(OpCodes.Ldarg, argIndex);

                // If the parameter is a value type, box it.
                if (type.GetTypeInfo().IsValueType)
                    il.Emit(OpCodes.Box, type);

                // Replace the element at the current index within the parameters array with the argument at 
                // the current index.
                il.Emit(OpCodes.Stelem_Ref);
            }
        }

        /// <summary>
        ///     Generates the handle invoker.
        /// </summary>
        /// <param name="il">The il generator.</param>
        /// <param name="argsLocal">The arguments local.</param>
        protected virtual void GenerateHandleInvokeCode(ILGenerator il, LocalBuilder argsLocal)
        {
            // Push the handle of the native onto the stack.
            il.Emit(OpCodes.Ldc_I4, Native.Handle);

            // Load the args array onto the stack.
            il.Emit(OpCodes.Ldloc, argsLocal);

            // Invoke the native invocation method.
            var invokeMethodInfo = GetHandleInvokerMethod();

            if (invokeMethodInfo.IsFinal || !invokeMethodInfo.IsVirtual)
                il.Emit(OpCodes.Call, invokeMethodInfo);
            else
                il.Emit(OpCodes.Callvirt, invokeMethodInfo);
        }

        /// <summary>
        ///     Emits opcodes to the specified IL Generator to set reference arguments to the value sotred in the argument array
        ///     local after the native has been invoked.
        /// </summary>
        /// <param name="il">The il generator.</param>
        /// <param name="argsLocal">The arguments local.</param>
        protected virtual void GenerateInvokeOutputCode(ILGenerator il, LocalBuilder argsLocal)
        {
            // Generate a pass-back for every output parameter of the native.
            for (var index = 0; index < ParameterTypes.Length; index++)
            {
                var isByRef = ParameterTypes[index].IsByRef;
                var type = isByRef
                    ? ParameterTypes[index].GetElementType()
                    : ParameterTypes[index];

                var argIndex = NativeArgIndexToMethodArgIndex(index);

                // If this parameter is not of an output or reference type no pass-back is required; skip it.
                if (!isByRef || argIndex < 0)
                    continue;

                // Load the argument at the current parameter index onto the stack.
                il.Emit(OpCodes.Ldarg, argIndex);

                // Load the args array onto the stack.
                il.Emit(OpCodes.Ldloc, argsLocal);

                // Push the current parameter index onto the stack.
                il.Emit(OpCodes.Ldc_I4, index);

                // Load the element at the specified index from the arguments local.
                il.Emit(OpCodes.Ldelem_Ref);

                // Store the value in the reference argument at the current parameter index.
                if (type == typeof(int))
                {
                    il.Emit(OpCodes.Unbox_Any, typeof(int));
                    il.Emit(OpCodes.Stind_I4);
                }
                else if (type == typeof(bool))
                {
                    il.Emit(OpCodes.Unbox_Any, typeof(bool));
                    il.Emit(OpCodes.Stind_I4);
                }
                else if (type == typeof(float))
                {
                    il.Emit(OpCodes.Unbox_Any, typeof(float));
                    il.Emit(OpCodes.Stind_R4);
                }
                else
                    il.Emit(OpCodes.Stind_Ref);
            }
        }

        /// <summary>
        ///     Emits opcodes to the specified IL Generator to return.
        /// </summary>
        /// <param name="il">The il generator.</param>
        protected virtual void GenerateReturn(ILGenerator il)
        {
            // Return to the caller.
            il.Emit(OpCodes.Ret);
        }

        /// <summary>
        ///     Generates the IL code with the speicifed il generator.
        /// </summary>
        /// <param name="il">The il generator.</param>
        public virtual void Generate(ILGenerator il)
        {
            var argsLocal = GenerateArgsArray(il);

            GenerateInvokeInputCode(il, argsLocal);
            GenerateHandleInvokeCode(il, argsLocal);
            GenerateInvokeOutputCode(il, argsLocal);
            GenerateReturn(il);
        }
    }
}