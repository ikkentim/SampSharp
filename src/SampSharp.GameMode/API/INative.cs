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
using System.Reflection;
using System.Reflection.Emit;

namespace SampSharp.GameMode.API
{
    /// <summary>
    ///     Contains the definition of a native.
    /// </summary>
    public interface INative
    {
        /// <summary>
        ///     Gets the handle of this native.
        /// </summary>
        int Handle { get; }

        /// <summary>
        ///     Gets the name of the native function.
        /// </summary>
        string Name { get; }


        /// <summary>
        ///     Gets the parameter types.
        /// </summary>
        Type[] ParameterTypes { get; }

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native.</returns>
        int Invoke(params object[] arguments);

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a float.</returns>
        float InvokeFloat(params object[] arguments);

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a bool.</returns>
        bool InvokeBool(params object[] arguments);
        
        /// <summary>
        ///     Generates an invoker delegate for the function this instance represents.
        /// </summary>
        /// <param name="delegateType">Type of the delegate.</param>
        /// <returns>The generated invoker delegate.</returns>
        Delegate GenerateInvoker(Type delegateType);
    }
}