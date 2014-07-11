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
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SampSharp.GameMode.Natives
{
    public static partial class Native
    {
        /// <summary>
        ///     Prints a message to the serverlog.
        /// </summary>
        /// <param name="msg">The message to print to the serverlog.</param>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Print(string msg);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Test(object[] args);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CallNativeArray(string name, string format, object[] args);


        public static int CallNative(string name, __arglist)
        {
            var iterator = new ArgIterator(__arglist);
            var len = iterator.GetRemainingCount();
            var args = new object[len];

            string format = "";
            for (int idx=0;idx<len;idx++)
            {
                var arg = iterator.GetNextArg();
                var type = TypedReference.GetTargetType(arg);

                //Console.WriteLine("{0}", type);
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

            return CallNativeArray(name, format, args);
        }
    }
}