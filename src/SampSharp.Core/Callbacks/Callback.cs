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
using SampSharp.Core.Communication;

namespace SampSharp.Core.Callbacks
{
    /// <summary>
    ///     Represents a public call (callback).
    /// </summary>
    internal class Callback
    {
        private readonly MethodInfo _methodInfo;
        private readonly ParameterInfo[] _parameterInfos;
        private readonly CallbackParameterInfo[] _parameters;
        private readonly object[] _parameterValues;

        private readonly object _target;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Callback" /> class.
        /// </summary>
        /// <param name="target">The target to invoke the method on.</param>
        /// <param name="methodInfo">The information about the method to invoke.</param>
        /// <param name="name">The name of the callback.</param>
        /// <param name="parameters">The parameters of the callback.</param>
        public Callback(object target, MethodInfo methodInfo, string name, CallbackParameterInfo[] parameters)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _methodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _parameterInfos = methodInfo.GetParameters();
            _parameterValues = new object[parameters.Length];

            // Verify the parameters match the method info
            if (parameters.Length != _parameterInfos.Length)
                throw new CallbackRegistrationException("The specified parameters does not match the parameters of the specified method.");

            for (var i = 0; i < parameters.Length; i++)
            {
                var par = parameters[i];
                var info = _parameterInfos[i];

                switch (par.Type)
                {
                    case CallbackParameterType.Value:
                        if (!new[] { typeof(int), typeof(float), typeof(bool) }.Contains(info.ParameterType))
                        {
                            throw new CallbackRegistrationException(
                                "The specified parameters does not match the parameters of the specified method.");
                        }
                        break;
                    case CallbackParameterType.Array:
                        if (!new[] { typeof(int[]), typeof(float[]), typeof(bool[]) }.Contains(info.ParameterType))
                        {
                            throw new CallbackRegistrationException(
                                "The specified parameters does not match the parameters of the specified method.");
                        }
                        break;
                    case CallbackParameterType.String:
                        if (typeof(string) != info.ParameterType)
                        {
                            throw new CallbackRegistrationException(
                                "The specified parameters does not match the parameters of the specified method.");
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        /// <summary>
        ///     Gets the name of the callback.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Invokes the callback with the specified arguments buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>The value returned by the callback.</returns>
        public int? Invoke(byte[] buffer, int startIndex)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));

            var bufferIndex = startIndex;
            for (var i = 0; i < _parameters.Length; i++)
            {
                var par = _parameters[i];
                var info = _parameterInfos[i];

                switch (par.Type)
                {
                    case CallbackParameterType.Value:
                    {
                        var value = ValueConverter.ToInt32(buffer, bufferIndex);
                        bufferIndex += 4;
                        if (info.ParameterType == typeof(int))
                            _parameterValues[i] = value;
                        else if (info.ParameterType == typeof(float))
                            _parameterValues[i] = ValueConverter.ToSingle(value);
                        else if (info.ParameterType == typeof(bool))
                            _parameterValues[i] = ValueConverter.ToBoolean(value);
                        break;
                    }
                    case CallbackParameterType.Array:
                    {
                        var length = ValueConverter.ToInt32(buffer, bufferIndex);
                        bufferIndex += 4;
                        var array = Array.CreateInstance(info.ParameterType.GetElementType(), length);
                        _parameterValues[i] = array;

                        if (info.ParameterType == typeof(int))
                        {
                            for (var j = 0; j < length; j++)
                            {
                                array.SetValue(ValueConverter.ToInt32(buffer, bufferIndex + j * 4), j);
                            }
                        }
                        else if (info.ParameterType == typeof(float))
                        {
                            for (var j = 0; j < length; j++)
                            {
                                array.SetValue(ValueConverter.ToSingle(buffer, bufferIndex + j * 4), j);
                            }
                        }
                        else if (info.ParameterType == typeof(bool))
                        {
                            for (var j = 0; j < length; j++)
                            {
                                array.SetValue(ValueConverter.ToBoolean(buffer, bufferIndex + j * 4), j);
                            }
                        }


                        bufferIndex += length * 4;
                        break;
                    }
                    case CallbackParameterType.String:
                    {
                        var value = ValueConverter.ToString(buffer, bufferIndex);
                        bufferIndex += value.Length + 1;
                        _parameterValues[i] = value;

                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var result = _methodInfo.Invoke(_target, _parameterValues);

            if (result is int)
                return (int) result;
            if (result is float)
                return ValueConverter.ToInt32((float) result);
            if (result is bool)
                return ValueConverter.ToInt32((bool) result);

            return null;
        }

        /// <summary>
        ///     Determines whether the specified type is a valid value type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is a valid value type; otherwise, <c>false</c>.</returns>
        public static bool IsValidValueType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type == typeof(int) || type == typeof(float) || type == typeof(bool);
        }

        /// <summary>
        ///     Determines whether the specified type is a valid array type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is a valid array type; otherwise, <c>false</c>.</returns>
        public static bool IsValidArrayType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsArray && !type.IsByRef && type.HasElementType && IsValidValueType(type.GetElementType());
        }

        /// <summary>
        ///     Determines whether the specified type is a valid string type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is a valid string type; otherwise, <c>false</c>.</returns>
        public static bool IsValidStringType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type == typeof(string);
        }

        /// <summary>
        ///     Determines whether the specified type is a valid return type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is a valid return type; otherwise, <c>false</c>.</returns>
        public static bool IsValidReturnType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type == typeof(void) || IsValidValueType(type);
        }
    }
}