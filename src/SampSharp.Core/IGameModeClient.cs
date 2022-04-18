// SampSharp
// Copyright 2022 Tim Potze
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
using System.Reflection;
using System.Text;
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Core;

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
    ///     <paramref name="methodInfo" /> will be invoked on the specified <paramref name="target" />. If the method contains only
    ///     one object[] parameter, all parameters are provided to the method in this array.
    /// </summary>
    /// <param name="name">The name af the callback to register.</param>
    /// <param name="target">The target on which to invoke the method.</param>
    /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
    /// <param name="parameterTypes">The types of the parameters the callback handler expects.</param>
    /// <param name="lengthIndices">The indices at which the lengths are provides for parameters which require lengths.</param>
    void RegisterCallback(string name, object target, MethodInfo methodInfo, Type[] parameterTypes,
        uint?[] lengthIndices = null);
        
    /// <summary>
    ///     Registers a callback with the specified <paramref name="name" />. When the callback is called, the specified
    ///     <paramref name="methodInfo" /> will be invoked on the specified <paramref name="target" />.
    /// </summary>
    /// <param name="name">The name af the callback to register.</param>
    /// <param name="target">The target on which to invoke the method.</param>
    /// <param name="methodInfo">The method information of the method to invoke when the callback is called.</param>
    void RegisterCallback(string name, object target, MethodInfo methodInfo);

    /// <summary>
    ///     Prints the specified text to the server console.
    /// </summary>
    /// <param name="text">The text to print to the server console.</param>
    void Print(string text);
}