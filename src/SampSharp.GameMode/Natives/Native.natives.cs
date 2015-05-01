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
using System.Runtime.CompilerServices;

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern int CallNativeArray(string name, string format, object[] args, int[] sizes);

        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool NativeExists(string name);

        private static string FormatNativeList(out object[] args, out int[] lengths, RuntimeArgumentHandle handle)
        {
            var iterator = new ArgIterator(handle);
            int len = iterator.GetRemainingCount();
            var lengthList = new List<int>();
            args = new object[len];

            string format = "";
            for (int idx = 0; idx < len; idx++)
            {
                TypedReference arg = iterator.GetNextArg();
                Type type = TypedReference.GetTargetType(arg);

                switch (type.ToString())
                {
                    case "System.String":
                        format += "s";
                        break;
                    case "System.String&":
                        format += "S";
                        lengthList.Add(idx + 1);
                        break;
                    case "System.Int32":
                        format += "d";
                        break;
                    case "System.Int32&":
                        format += "D";
                        break;
                    case "System.Int32[]":
                        format += "a";
                        lengthList.Add(idx + 1);
                        break;
                    case "System.Int32[]&":
                        format += "A";
                        lengthList.Add(idx + 1);
                        break;
                    case "System.Single":
                        format += "f";
                        break;
                    case "System.Single&":
                        format += "F";
                        break;
                    case "System.Single[]":
                        format += "v";
                        lengthList.Add(idx + 1);
                        break;
                    case "System.Single[]&":
                        format += "V";
                        lengthList.Add(idx + 1);
                        break;
                    case "System.Boolean":
                        format += "b";
                        break;
                    default:
                        throw new NotSupportedException("parameter type " + type + " is not supported");
                }

                args[idx] = TypedReference.ToObject(arg);
            }

            lengths = lengthList.Count > 0 ? lengthList.ToArray() : null;
            return format;
        }

        /// <summary>
        ///     Registers an extension to the plugin.
        /// </summary>
        /// <param name="extension">The extension to register.</param>
        /// <returns>
        ///     True on success, False otherwise.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RegisterExtension(object extension);

        /// <summary>
        ///     Call a native.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <returns>
        ///     The returned integer.
        /// </returns>
        public static int CallNative(string name)
        {
            return CallNativeArray(name, "", null, null);
        }

        /// <summary>
        ///     Call a native with the given arguments.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <returns>
        ///     The returned integer.
        /// </returns>
        public static int CallNative(string name, __arglist)
        {
            object[] args;
            int[] sizes;
            string format = FormatNativeList(out args, out sizes, __arglist);

            return CallNativeArray(name, format, args, sizes);
        }

        /// <summary>
        ///     Call a native with the given arguments.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <param name="lengths">The lengths of arrays and referenced strings.</param>
        /// <returns>
        ///     The returned integer.
        /// </returns>
        public static int CallNative(string name, int[] lengths, __arglist)
        {
            object[] args;
            int[] sizes;
            string format = FormatNativeList(out args, out sizes, __arglist);

            return CallNativeArray(name, format, args, lengths);
        }

        /// <summary>
        ///     Call a native.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <returns>
        ///     The returned boolean.
        /// </returns>
        public static bool CallNativeAsBool(string name)
        {
            return CallNativeArray(name, "", null, null) > 0;
        }

        /// <summary>
        ///     Call a native with the given arguments.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <returns>
        ///     The returned boolean.
        /// </returns>
        public static bool CallNativeAsBool(string name, __arglist)
        {
            object[] args;
            int[] sizes;
            string format = FormatNativeList(out args, out sizes, __arglist);

            return CallNativeArray(name, format, args, sizes) != 0;
        }

        /// <summary>
        ///     Call a native with the given arguments.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <param name="lengths">The lengths of arrays and referenced strings.</param>
        /// <returns>
        ///     The returned boolean.
        /// </returns>
        public static bool CallNativeAsBool(string name, int[] lengths, __arglist)
        {
            object[] args;
            int[] sizes;
            string format = FormatNativeList(out args, out sizes, __arglist);

            return CallNativeArray(name, format, args, lengths) != 0;
        }
    }
}