// SampSharp
// Copyright 2020 Tim Potze
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

namespace SampSharp.Entities
{
    /// <summary>
    /// Provides functionality for handling events.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Enables handling of the callback with the specified <paramref name="name" /> as an event.
        /// </summary>
        /// <param name="name">The name of the callback.</param>
        /// <param name="parameters">The types of the parameters of the callback.</param>
        void EnableEvent(string name, params Type[] parameters);

        /// <summary>
        /// Adds a middleware to the handler of the event with the specified <paramref name="name" />.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="middleware">The middleware to add to the event.</param>
        void UseMiddleware(string name, Func<EventDelegate, EventDelegate> middleware);

        /// <summary>
        /// Invokes the event with the specified <paramref name="name"/> and <paramref name="arguments"/>.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="arguments">The arguments of the event.</param>
        /// <returns>The result of the event.</returns>
        object Invoke(string name, params object[] arguments);
    }
}