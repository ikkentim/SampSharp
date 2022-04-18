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

namespace SampSharp.Entities;

/// <summary>
/// Provides the functionality for a registry of system types.
/// </summary>
public interface ISystemRegistry
{
    /// <summary>
    /// Sets the systems available in this registry and locks further changes to the registry.
    /// </summary>
    /// <param name="types">The types of systems which should be available in the registry.</param>
    void SetAndLock(Type[] types);

    /// <summary>
    /// Gets all types of systems of the specified <paramref name="type" />.
    /// </summary>
    /// <param name="type">The type of the systems to get.</param>
    /// <returns>An array of the systems of the specified type.</returns>
    ISystem[] Get(Type type);

    /// <summary>
    /// Gets all types of systems of the specified <typeparamref name="TSystem" />.
    /// </summary>
    /// <typeparam name="TSystem">The type of the systems to get.</typeparam>
    /// <returns>An array of the systems of the specified type.</returns>
    TSystem[] Get<TSystem>() where TSystem : ISystem;
}