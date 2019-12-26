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
        private readonly IGameModeClient _gameModeClient;
        private readonly MethodInfo _methodInfo;
        private readonly ParameterType[] _parameterTypes;
        private readonly object[] _parameterValues;
        private readonly object[] _parametersContainer;
        private readonly object _target;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Callback" /> class.
        /// </summary>
        /// <param name="target">The target to invoke the method on.</param>
        /// <param name="methodInfo">The information about the method to invoke.</param>
        /// <param name="name">The name of the callback.</param>
        /// <param name="gameModeClient">The game mode client.</param>
        public Callback(object target, MethodInfo methodInfo, string name, IGameModeClient gameModeClient)
        {
            _gameModeClient = gameModeClient ?? throw new ArgumentNullException(nameof(gameModeClient));
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _methodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
            Name = name ?? throw new ArgumentNullException(nameof(name));

            var parameterInfos = methodInfo.GetParameters();
            _parameterValues = new object[parameterInfos.Length];
            _parameterTypes = new ParameterType[parameterInfos.Length];

            for (var i = 0; i < parameterInfos.Length; i++)
            {
                _parameterTypes[i] = GetParameterType(parameterInfos[i].ParameterType);
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Callback" /> class.
        /// </summary>
        /// <param name="target">The target to invoke the method on.</param>
        /// <param name="methodInfo">The information about the method to invoke.</param>
        /// <param name="name">The name of the callback.</param>
        /// <param name="parameterTypes">The types of the parameters.</param>
        /// <param name="gameModeClient">The game mode client.</param>
        public Callback(object target, MethodInfo methodInfo, string name, Type[] parameterTypes, IGameModeClient gameModeClient)
        {
            _gameModeClient = gameModeClient ?? throw new ArgumentNullException(nameof(gameModeClient));
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _methodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
            Name = name ?? throw new ArgumentNullException(nameof(name));

            var parameterInfos = methodInfo.GetParameters();
            _parameterValues = new object[parameterTypes.Length];
            _parameterTypes = new ParameterType[parameterTypes.Length];


            // Verify the parameters match the method info
            if (parameterInfos.Length == 1 && parameterInfos[0].ParameterType == typeof(object[]))
            {
                _parametersContainer = new object[1];
            }
            else
            {
                if (parameterTypes.Where((t, i) => t != parameterInfos[i].ParameterType).Any())
                {
                    throw new CallbackRegistrationException(
                        "The specified parameters does not match the parameters of the specified method.");
                }
            }

            for (var i = 0; i < parameterTypes.Length; i++)
            {
                _parameterTypes[i] = GetParameterType(parameterTypes[i]);
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
            for (var i = 0; i < _parameterTypes.Length; i++)
            {
                var parameterType = _parameterTypes[i];

                int length;
                int value;
                switch (parameterType)
                {
                    case ParameterType.Int:
                        value = ValueConverter.ToInt32(buffer, bufferIndex);
                        bufferIndex += 4;
                        _parameterValues[i] = value;
                        break;
                    case ParameterType.Single:
                        value = ValueConverter.ToInt32(buffer, bufferIndex);
                        bufferIndex += 4;
                        _parameterValues[i] = ValueConverter.ToSingle(value);
                        break;
                    case ParameterType.Bool:
                        value = ValueConverter.ToInt32(buffer, bufferIndex);
                        bufferIndex += 4;
                        _parameterValues[i] = ValueConverter.ToBoolean(value);
                        break;
                    case ParameterType.IntArray:
                        length = ValueConverter.ToInt32(buffer, bufferIndex);
                        bufferIndex += 4;
                        var intArray = new int[length];
                        _parameterValues[i] = intArray;

                        for (var j = 0; j < length; j++)
                        {
                            intArray[j] = ValueConverter.ToInt32(buffer, bufferIndex + j * 4);
                        }

                        bufferIndex += length * 4;

                        break;
                    case ParameterType.SingleArray:
                        length = ValueConverter.ToInt32(buffer, bufferIndex);
                        bufferIndex += 4;
                        var singleArray = new float[length];
                        _parameterValues[i] = singleArray;

                        for (var j = 0; j < length; j++)
                        {
                            singleArray[j] = ValueConverter.ToSingle(buffer, bufferIndex + j * 4);
                        }

                        bufferIndex += length * 4;

                        break;
                    case ParameterType.BoolArray:
                        length = ValueConverter.ToInt32(buffer, bufferIndex);
                        bufferIndex += 4;
                        var boolArray = new bool[length];
                        _parameterValues[i] = boolArray;

                        for (var j = 0; j < length; j++)
                        {
                            boolArray[j] = ValueConverter.ToBoolean(buffer, bufferIndex + j * 4);
                        }

                        bufferIndex += length * 4;

                        break;
                    case ParameterType.String:
                        var stringValue = ValueConverter.ToString(buffer, bufferIndex, _gameModeClient.Encoding);
                        bufferIndex += stringValue.Length + 1;
                        _parameterValues[i] = stringValue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            object[] parameters;
            if (_parametersContainer != null)
            {
                _parametersContainer[0] = _parameterValues;
                parameters = _parametersContainer;
            }
            else
            {
                parameters = _parameterValues;
            }

            var result = _methodInfo.Invoke(_target, parameters);

            switch (result)
            {
                case int intValue:
                    return intValue;
                case float singleValue:
                    return ValueConverter.ToInt32(singleValue);
                case bool boolValue:
                    return ValueConverter.ToInt32(boolValue);
                default:
                    return null;
            }
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

            return type == typeof(void) || type == typeof(object) || IsValidValueType(type);
        }

        /// <summary>
        ///     Returns a value indicating whether the specified callback parameter information matches the parameters of this callbacks.
        /// </summary>
        /// <param name="infos">The callback parameter information.</param>
        /// <returns><c>true</c> if the specified information matches this callback; otherwise, <c>false</c>.</returns>
        public bool MatchesParameters(CallbackParameterInfo[] infos)
        {
            if (infos == null) throw new ArgumentNullException(nameof(infos));

            if (infos.Length != _parameterTypes.Length)
                return false;

            return !infos.Where((t, i) => !TypeMatches(_parameterTypes[i], t.Type)).Any();
        }
        
        private static bool TypeMatches(ParameterType parameterType, CallbackParameterType callbackParameterType)
        {
            switch (callbackParameterType)
            {
                case CallbackParameterType.Array:
                    return parameterType == ParameterType.BoolArray || parameterType == ParameterType.IntArray ||
                           parameterType == ParameterType.SingleArray;
                case CallbackParameterType.Value:
                    return parameterType == ParameterType.Bool || parameterType == ParameterType.Int ||
                           parameterType == ParameterType.Single;
                case CallbackParameterType.String:
                    return parameterType == ParameterType.String;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ParameterType GetParameterType(Type type)
        {
            if (type == typeof(int))
                return ParameterType.Int;
            if (type == typeof(float))
                return ParameterType.Single;
            if (type == typeof(bool))
                return ParameterType.Bool;
            if (type == typeof(int[]))
                return ParameterType.Int;
            if (type == typeof(float[]))
                return ParameterType.SingleArray;
            if (type == typeof(bool[]))
                return ParameterType.BoolArray;
            if (type == typeof(string))
                return ParameterType.String;

            throw new CallbackRegistrationException($"Parameter type {type} is unsupported.");
        }

        private enum ParameterType
        {
            Int,
            Single,
            Bool,
            IntArray,
            SingleArray,
            BoolArray,
            String
        }

    }
}