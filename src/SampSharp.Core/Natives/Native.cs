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
using System.Collections.Generic;
using System.Linq;
using SampSharp.Core.Communication;

namespace SampSharp.Core.Natives
{
    /// <summary>
    ///     Represents a SA-MP native function.
    /// </summary>
    public class Native : INative
    {
        private readonly IGameModeClient _gameModeClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Native" /> class.
        /// </summary>
        /// <param name="gameModeClient">The game mode client.</param>
        /// <param name="name">The name.</param>
        /// <param name="handle">The handle.</param>
        /// <param name="parameters">The parameters.</param>
        public Native(IGameModeClient gameModeClient, string name, int handle, params NativeParameterInfo[] parameters)
        {
            if (handle < 0) throw new ArgumentOutOfRangeException(nameof(handle));

            _gameModeClient = gameModeClient ?? throw new ArgumentNullException(nameof(gameModeClient));
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Handle = handle;

            if (parameters.Any(info => info.RequiresLength && info.LengthIndex >= parameters.Length))
                throw new ArgumentOutOfRangeException(nameof(parameters), "Invalid parameter length index.");
        }

        #region Implementation of INative

        /// <summary>
        ///     Gets the handle of this native.
        /// </summary>
        public int Handle { get; }

        /// <summary>
        ///     Gets the name of the native function.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the parameter types.
        /// </summary>
        public NativeParameterInfo[] Parameters { get; }

        /// <summary>
        ///     Invokes the native with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native.</returns>
        public int Invoke(params object[] arguments)
        {
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));

            if (Parameters.Length != arguments.Length)
                throw new ArgumentOutOfRangeException(nameof(arguments), "Invalid argument count");

            IEnumerable<byte> data = ValueConverter.GetBytes(Handle);

            for (var i = 0; i < Parameters.Length; i++)
            {
                data = data.Concat(new[] { (byte) Parameters[i].ArgumentType });

                var length = 0;

                if (Parameters[i].RequiresLength)
                {
                    var lengthObj = arguments[Parameters[i].LengthIndex];
                    if (arguments[Parameters[i].LengthIndex] is int len)
                        length = len;
                    else
                    {
                        throw new ArgumentException("Argument is expected to be of type int, but is " + (lengthObj?.GetType().Name ?? "null"),
                            nameof(arguments));
                    }
                }

                data = data.Concat(Parameters[i].GetBytes(arguments[i], length));
            }

            var response = _gameModeClient.InvokeNative(data);

            if (response.Length < 4)
                return 0;

            var respPos = 4;

            for (var i = 0; i < Parameters.Length; i++)
            {
                var value = Parameters[i].GetReferenceArgument(response, ref respPos);
                if (value != null)
                    arguments[i] = value;
            }

            return ValueConverter.ToInt32(response, 0);
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a float.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a float.</returns>
        public float InvokeFloat(params object[] arguments)
        {
            return ValueConverter.ToSingle(Invoke(arguments));
        }

        /// <summary>
        ///     Invokes the native with the specified arguments and returns the return value as a bool.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The return value of the native as a bool.</returns>
        public bool InvokeBool(params object[] arguments)
        {
            return ValueConverter.ToBoolean(Invoke(arguments));
        }

        #endregion
    }
}