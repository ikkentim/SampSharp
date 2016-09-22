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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.GameMode.API.NativeObjects
{
    /// <summary>
    /// A generator for native object instances.
    /// </summary>
    public class NativeObjectILGenerator : NativeILGenerator
    {
        private readonly string[] _identifiers;
        private readonly Type _nativeObjectType;

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeObjectILGenerator"/> class.
        /// </summary>
        /// <param name="native">The native.</param>
        /// <param name="nativeObjectType">Type of the native object.</param>
        /// <param name="identifiers">The identifiers.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="returnType">Type of the return.</param>
        /// <exception cref="ArgumentNullException">Thrown if nativeObjectType or idnetifiers is null.</exception>
        public NativeObjectILGenerator(INative native, Type nativeObjectType, string[] identifiers,
            Type[] parameterTypes, Type returnType)
            : base(
                native,
                Enumerable.Repeat(typeof (int), identifiers?.Length ?? 0)
                    .Concat(parameterTypes ?? new Type[0])
                    .ToArray(), returnType)
        {
            if (nativeObjectType == null) throw new ArgumentNullException(nameof(nativeObjectType));
            if (identifiers == null) throw new ArgumentNullException(nameof(identifiers));
            _nativeObjectType = nativeObjectType;
            _identifiers = identifiers;
        }

        #region Overrides of NativeILGenerator

        /// <summary>
        /// Returns the native argument index for the specified method argument index.
        /// </summary>
        /// <param name="index">The method argument index.</param>
        /// <returns>The native argument index for the specified method argument index.</returns>
        protected override int NativeArgIndexToMethodArgIndex(int index)
        {
            var num = index - _identifiers.Length;

            if (num >= 0)
                num++;

            return num;
        }

        /// <summary>
        /// Generates the pass trough for input arguments.
        /// </summary>
        /// <param name="il">The il generator.</param>
        /// <param name="argsLocal">The arguments local.</param>
        protected override void GeneratePassTrough(ILGenerator il, LocalBuilder argsLocal)
        {
            // Generate a pass-trough for every parameter of the native.
            for (var index = 0; index < _identifiers.Length; index++)
            {
                // Load the args array onto the stack.
                il.Emit(OpCodes.Ldloc, argsLocal);

                // Push the parameter index onto the stack.
                il.Emit(OpCodes.Ldc_I4, index);

                // Load the identifier from the object.
                il.Emit(OpCodes.Ldarg_0);

                var id = _nativeObjectType.GetProperty(_identifiers[index],
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (id.PropertyType != typeof(int))
                    throw new Exception("Invalid identifier property return type for type " + _nativeObjectType);

                if (!id.CanRead)
                    throw new Exception("Invalid identifier property read accessibility for type " + _nativeObjectType);

                il.Emit(OpCodes.Callvirt, id.GetGetMethod(true));

                // If the parameter is a value type, box it.
                il.Emit(OpCodes.Box, typeof (int));

                // Replace the element at the current index within the parameters array with the argument at 
                // the current index.
                il.Emit(OpCodes.Stelem_Ref);
            }

            base.GeneratePassTrough(il, argsLocal);
        }

        #endregion
    }
}