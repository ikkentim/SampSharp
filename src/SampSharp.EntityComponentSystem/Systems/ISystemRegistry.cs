// SampSharp
// Copyright 2019 Tim Potze
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

namespace SampSharp.EntityComponentSystem.Systems
{
    /// <summary>
    /// Provides the functionality for a registry of system types.
    /// </summary>
    public interface ISystemRegistry
    {
        /// <summary>
        /// Gets all types of systems.
        /// </summary>
        IEnumerable<Type> Systems { get; }

        /// <summary>
        /// Gets all types of systems which require configuration.
        /// </summary>
        IEnumerable<Type> ConfiguringSystems { get; }

        /// <summary>
        /// Adds the specified type of system to this registry.
        /// </summary>
        /// <param name="type">The type of the system.</param>
        void Add(Type type);
    }
}