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

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Represents a native function.
    /// </summary>
    public struct NativeFunction
    {
        private readonly string _format;
        private readonly int _handle;
        private readonly string _name;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeFunction" /> struct.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <param name="parameterTypes">The parameter types of the native.</param>
        public NativeFunction(string name, params Type[] parameterTypes)
            : this(name, null, parameterTypes)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeFunction" /> struct.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <param name="sizes">The sizes of the parameters which require size information.</param>
        /// <param name="parameterTypes">The parameter types of the native.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public NativeFunction(string name, int[] sizes, params Type[] parameterTypes)
        {
            if (name == null) throw new ArgumentNullException("name");

            _name = name;
            _format = string.Empty;

            if (parameterTypes == null || parameterTypes.Length == 0)
            {
                _handle = Native.LoadNative(name, string.Empty, null);
                return;
            }

            var lengthList = new List<int>();
            for (var i = 0; i < parameterTypes.Length; i++)
            {
                var param = parameterTypes[i];
                if (param == typeof (int))
                    _format += "d";
                else if (param == typeof (bool))
                    _format += "b";
                else if (param == typeof(string))
                    _format += "s";
                else if (param == typeof(float))
                    _format += "f";
                else if (param == typeof(int[]))
                {
                    lengthList.Add(i + 1);
                    _format += "a";
                }
                else if (param == typeof(float[]))
                {
                    lengthList.Add(i + 1);
                    _format += "v";
                }
                else if (param == typeof(Out<int>))
                    _format += "D";
                else if (param == typeof(Out<string>))
                {
                    lengthList.Add(i + 1);
                    _format += "S";
                }
                else if (param == typeof(Out<float>))
                    _format += "F";
                else if (param == typeof(Out<int[]>))
                {
                    lengthList.Add(i + 1);
                    _format += "A";
                }
                else if (param == typeof(Out<float[]>))
                {
                    lengthList.Add(i + 1);
                    _format += "V";
                }
            }

            _handle = Native.LoadNative(name, _format, sizes ?? (lengthList.Count > 0 ? lengthList.ToArray() : null));
        }

        /// <summary>
        ///     Gets the name of the native function.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        private object[] CreateRefArray(RuntimeArgumentHandle handle)
        {
            var iterator = new ArgIterator(handle);
            var len = iterator.GetRemainingCount();

            if ((_format != null && _format.Length != len) || (_format == null && len != 0))
                throw new ArgumentException("Invalid arguments");

            var args = new object[len];
            for (var idx = 0; idx < len; idx++)
                args[idx] = TypedReference.ToObject(iterator.GetNextArg());

            return args;
        }

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The return value of the native.</returns>
        public int Invoke(params object[] args)
        {
            return Native.InvokeNative(_handle, args);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <returns>The return value of the native.</returns>
        public int Invoke(__arglist)
        {
            return Invoke(CreateRefArray(__arglist));
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The return value of the native as a float.</returns>
        public int InvokeFloat(params object[] args)
        {
            return Native.InvokeNative(_handle, args);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <returns>The return value of the native as a float.</returns>
        public float InvokeFloat(__arglist)
        {
            return InvokeFloat(CreateRefArray(__arglist));
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The return value of the native as a bool.</returns>
        public bool InvokeBool(params object[] args)
        {
            return Invoke(args) != 0;
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <returns>The return value of the native as a bool.</returns>
        public bool InvokeBool(__arglist)
        {
            return InvokeBool(CreateRefArray(__arglist));
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}#{1}", _name, _handle);
        }
    }
}