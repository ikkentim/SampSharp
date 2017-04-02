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

using System.Reflection;
using System.Threading.Tasks;

namespace SampSharp.Core
{
    /// <summary>
    ///     Contains the methods of a SampSharp game mode client.
    /// </summary>
    public interface IGameModeClient
    {
        /// <summary>
        ///     Registers a callback with the specified <see cref="name" />. When the callback is called, the specified
        ///     <see cref="methodInfo" /> will be invoked on the specified <see cref="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        /// <param name="parameters">The parameters of the callback.</param>
        /// <returns></returns>
        Task RegisterCallback(string name, object target, MethodInfo methodInfo, params CallbackParameterInfo[] parameters);

        /// <summary>
        ///     Registers a callback with the specified <see cref="name" />. When the callback is called, the specified
        ///     <see cref="methodInfo" /> will be invoked on the specified <see cref="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        /// <returns></returns>
        Task RegisterCallback(string name, object target, MethodInfo methodInfo);
    }
}