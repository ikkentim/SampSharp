﻿// SampSharp
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
using System.Reflection;
using System.Text;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Natives;
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Core
{
    /// <summary>
    ///     Contains the methods of a SampSharp game mode client.
    /// </summary>
    public interface IGameModeClient
    {
        /// <summary>
        ///     Gets the default encoding to use when translating server messages.
        /// </summary>
        Encoding Encoding { get; }
        
        /// <summary>
        ///     Gets the native loader to be used to load natives.
        /// </summary>
        [Obsolete("Native handle based native invocation is deprecated and will be removed in a future version.")]
        INativeLoader NativeLoader { get; }
        
        /// <summary>
        /// Gets the factory for native method wrapper objects.
        /// </summary>
        INativeObjectProxyFactory NativeObjectProxyFactory { get; }

        /// <summary>
        /// Gets the provider which can be used for synchronizing a call to the main thread.
        /// </summary>
        ISynchronizationProvider SynchronizationProvider { get; }

        /// <summary>
        ///     Gets the path to the server directory.
        /// </summary>
        string ServerPath { get; }

        /// <summary>
        ///     Occurs when an exception is unhandled during the execution of a callback or tick.
        /// </summary>
        event EventHandler<UnhandledExceptionEventArgs> UnhandledException;
        
        /// <summary>
        ///     Registers a callback with the specified <paramref name="name" />. When the callback is called, the specified
        ///     <paramref name="methodInfo" /> will be invoked on the specified <paramref name="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        /// <param name="parameters">The parameters of the callback.</param>
        void RegisterCallback(string name, object target, MethodInfo methodInfo, CallbackParameterInfo[] parameters);
        
        /// <summary>
        ///     Registers a callback with the specified <paramref name="name" />. When the callback is called, the specified
        ///     <paramref name="methodInfo" /> will be invoked on the specified <paramref name="target" />.
        /// </summary>
        /// <param name="name">The name af the callback to register.</param>
        /// <param name="target">The target on which to invoke the method.</param>
        /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
        /// <param name="parameters">The parameters of the callback.</param>
        /// <param name="parameterTypes">The types of the parameters.</param>
        void RegisterCallback(string name, object target, MethodInfo methodInfo, CallbackParameterInfo[] parameters,
            Type[] parameterTypes);

        /// <summary>
        ///     Prints the specified text to the server console.
        /// </summary>
        /// <param name="text">The text to print to the server console.</param>
        void Print(string text);

        /// <summary>
        ///     Gets the handle of the native with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the native.</param>
        /// <returns>The handle of the native with the specified <paramref name="name" />.</returns>
        [Obsolete("Native handle based native invocation is deprecated and will be removed in a future version.")]
        int GetNativeHandle(string name);

        /// <summary>
        ///     Invokes a native using the specified <paramref name="data" /> buffer.
        /// </summary>
        /// <param name="data">The data buffer to be used.</param>
        /// <returns>The response from the native.</returns>
        [Obsolete("Native handle based native invocation is deprecated and will be removed in a future version.")]
        byte[] InvokeNative(IEnumerable<byte> data);

        /// <summary>
        ///     Shuts down the server after the current callback has been processed.
        /// </summary>
        [Obsolete("Multi-process mode is deprecated and will be removed in a future release.")]
        void ShutDown();
    }
}