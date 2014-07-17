// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SampSharp.GameMode.Natives
{
    public static partial class Native
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern int CallNativeArray(string name, string format, object[] args, int[] sizes);

        private static string FormatNativeList(out object[] args, out int[] lengths, RuntimeArgumentHandle handle)
        {
            var iterator = new ArgIterator(handle);
            var len = iterator.GetRemainingCount();
            var lengthList = new List<int>();
            args = new object[len];

            string format = "";
            for (int idx = 0; idx < len; idx++)
            {
                var arg = iterator.GetNextArg();
                var type = TypedReference.GetTargetType(arg);

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
        /// <returns>True on success, False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RegisterExtension(object extension);

        /// <summary>
        ///     Call a native.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <returns>The returned integer.</returns>
        public static int CallNative(string name)
        {
            return CallNativeArray(name, "", null, null);
        }

        /// <summary>
        ///     Call a native with the given arguments.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <returns>The returned integer.</returns>
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
        /// <param name="lengths"></param>
        /// <returns>The returned integer.</returns>
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
        /// <returns>The returned boolean.</returns>
        public static bool CallNativeAsBool(string name)
        {
            return CallNativeArray(name, "", null, null) > 0;
        }

        /// <summary>
        ///     Call a native with the given arguments.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <returns>The returned boolean.</returns>
        public static bool CallNativeAsBool(string name, __arglist)
        {
            object[] args;
            int[] sizes;
            string format = FormatNativeList(out args, out sizes, __arglist);

            return CallNativeArray(name, format, args, sizes) > 0;
        }

        /// <summary>
        ///     Call a native with the given arguments.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <param name="lengths"></param>
        /// <returns>The returned boolean.</returns>
        public static bool CallNativeAsBool(string name, int[] lengths, __arglist)
        {
            object[] args;
            int[] sizes;
            string format = FormatNativeList(out args, out sizes, __arglist);

            return CallNativeArray(name, format, args, lengths) > 0;
        }
    }
}