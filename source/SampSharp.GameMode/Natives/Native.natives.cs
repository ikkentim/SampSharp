using System;
using System.Runtime.CompilerServices;

namespace SampSharp.GameMode.Natives
{
    public static partial class Native
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern int CallNativeArray(string name, string format, object[] args);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern float CallNativeArrayFloat(string name, string format, object[] args);

        private static string FormatNativeList(out object[] args, RuntimeArgumentHandle handle)
        {
            var iterator = new ArgIterator(handle);
            var len = iterator.GetRemainingCount();
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
                        break;
                    case "System.Int32":
                        format += "d";
                        break;
                    case "System.Single":
                        format += "f";
                        break;
                    case "System.Single&":
                        format += "F";
                        break;
                    case "System.Boolean":
                        format += "b";
                        break;
                    default:
                        throw new NotSupportedException("parameter type " + type + " is not supported");
                }

                args[idx] = TypedReference.ToObject(arg);
            }

            return format;
        }

        /// <summary>
		/// Call a native with the given arguments.
		/// </summary>
		/// <param name="name">The name of the native to call.</param>
		/// <returns>The returned integer.</returns>
        public static int CallNative(string name, __arglist)
        {
            object[] args;
            string format = FormatNativeList(out args, __arglist);

            return CallNativeArray(name, format, args);
        }

        /// <summary>
        /// Call a native with the given arguments.
        /// </summary>
        /// <param name="name">The name of the native to call.</param>
        /// <returns>The returned float.</returns>
        public static float CallNativeFloat(string name, __arglist)
        {
            object[] args;
            string format = FormatNativeList(out args, __arglist);

            return CallNativeArrayFloat(name, format, args);
        }
    }
}
