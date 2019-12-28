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
using SampSharp.EntityComponentSystem.Events;

namespace SampSharp.EntityComponentSystem
{
    /// <summary>
    /// Provides functionality for configuring the SampSharp EntityComponentSystem.
    /// </summary>
    public interface IEcsBuilder
    {
        /// <summary>
        /// Gets the service provider.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// Adds a middleware to the handler of the event with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="middleware">The middleware to add to the event.</param>
        /// <returns>The builder.</returns>
        IEcsBuilder Use(string name, Func<EventDelegate, EventDelegate> middleware);

        /// <summary>
        /// Adds the system with the specified <paramref name="systemType" />.
        /// </summary>
        /// <param name="systemType">Type of the system.</param>
        /// <returns>The builder.</returns>
        IEcsBuilder UseSystem(Type systemType);
    }
}