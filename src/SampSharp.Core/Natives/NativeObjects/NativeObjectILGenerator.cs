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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.Core.Natives.NativeObjects
{
    /// <summary>
    ///     A generator for native object instances. This class can be used to generate a method for invoking a specific native
    ///     with a number of predefined identifiers stored within the class the method is a member of.
    /// </summary>
    public class NativeObjectILGenerator : NativeILGenerator
    {
        private readonly string[] _identifiers;
        private readonly int _identifierIndex;
        private readonly Type _nativeObjectType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeObjectILGenerator" /> class.
        /// </summary>
        /// <param name="native">The native.</param>
        /// <param name="nativeObjectType">Type of the native object.</param>
        /// <param name="identifiers">The identifiers.</param>
        /// <param name="identifierIndex">The start index of the identifiers.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="returnType">Type of the return.</param>
        /// <exception cref="ArgumentNullException">Thrown if nativeObjectType or identifiers is null.</exception>
        public NativeObjectILGenerator(INative native, Type nativeObjectType, string[] identifiers, int identifierIndex,
            Type[] parameterTypes, Type returnType)
            : base(native, AddIdentifiersToParameterTypes(identifiers, identifierIndex, parameterTypes), returnType)
        {
            _nativeObjectType = nativeObjectType ?? throw new ArgumentNullException(nameof(nativeObjectType));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _identifierIndex = identifierIndex;
        }

        private static Type[] AddIdentifiersToParameterTypes(string[] identifiers ,int identifierIndex, Type[] parameterTypes)
        {
            if(identifierIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(identifierIndex));

            if (parameterTypes == null) parameterTypes = new Type[0];

            return parameterTypes.Take(identifierIndex)
                .Concat(Enumerable.Repeat(typeof(int), identifiers?.Length ?? 0))
                .Concat(parameterTypes.Skip(identifierIndex))
                .ToArray();
        }

        #region Overrides of NativeILGenerator

        /// <summary>
        ///     Returns the native argument index for the specified method argument index.
        /// </summary>
        /// <param name="index">The method argument index.</param>
        /// <returns>The native argument index for the specified method argument index.</returns>
        protected override int NativeArgIndexToMethodArgIndex(int index)
        {
            if (_identifiers.Length == 0 || index < _identifierIndex)
                return index + 1; // Add 1 to compensate for "this" argument

            if (index >= _identifierIndex && index < _identifierIndex + _identifiers.Length)
                return -1;

            return index - _identifiers.Length + 1; // Add 1 to compensate for "this" argument
        }

        /// <summary>
        ///     Emits opcodes to the specified IL Generator to store all input arguments in the arguments array local.
        /// </summary>
        /// <param name="il">The il generator.</param>
        /// <param name="argsLocal">The arguments local.</param>
        protected override void GenerateInvokeInputCode(ILGenerator il, LocalBuilder argsLocal)
        {
            // Load the identifiers into the args array
            for (var index = 0; index < _identifiers.Length; index++)
            {
                // Load the args array onto the stack.
                il.Emit(OpCodes.Ldloc, argsLocal);

                // Push the parameter index onto the stack.
                il.Emit(OpCodes.Ldc_I4, index + _identifierIndex);

                // Load the identifier from the object.
                il.Emit(OpCodes.Ldarg_0);

                var id = _nativeObjectType.GetTypeInfo()
                    .GetProperty(_identifiers[index],
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                
                if (id == null)
                    throw new Exception("Identifier property not found for " + _nativeObjectType);

                if (id.PropertyType != typeof(int))
                    throw new Exception("Invalid identifier property return type for type " + _nativeObjectType);

                if (!id.CanRead)
                    throw new Exception("Invalid identifier property read accessibility for type " + _nativeObjectType);

                il.Emit(OpCodes.Callvirt, id.GetGetMethod(true));

                // Box the identifier value.
                il.Emit(OpCodes.Box, typeof(int));

                // Replace the element at the current index within the parameters array with the argument at 
                // the current index.
                il.Emit(OpCodes.Stelem_Ref);
            }

            base.GenerateInvokeInputCode(il, argsLocal);
        }

        #endregion
    }
}