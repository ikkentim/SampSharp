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

namespace SampSharp.Core.Natives.NativeObjects;

/// <summary>Contains logic for creating native object proxies.</summary>
public static class NativeObjectProxyFactory
{
    /// <summary>Assembly name of generated assembly for native object proxy types.</summary>
    public const string AssemblyName = "SampSharpProxyAssembly";

    /// <summary>Creates a proxy instance of the specified type <typeparamref name="T" />.</summary>
    /// <typeparam name="T">The type to create a proxy of.</typeparam>
    /// <param name="arguments">The arguments.</param>
    /// <returns>The proxy instance.</returns>
    public static T CreateInstance<T>(params object[] arguments) where T : class
    {
        return CreateInstance(typeof(T), arguments) as T;
    }

    /// <summary>Creates a proxy instance of the specified <paramref name="type" />.</summary>
    /// <param name="type">The type to create a proxy of.</param>
    /// <param name="arguments">The arguments.</param>
    /// <returns>The proxy instance.</returns>
    public static object CreateInstance(Type type, params object[] arguments)
    {
        return InternalStorage.RunningClient.NativeObjectProxyFactory.CreateInstance(type, arguments);
    }
}