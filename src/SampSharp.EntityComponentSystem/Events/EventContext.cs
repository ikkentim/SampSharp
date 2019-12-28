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

namespace SampSharp.EntityComponentSystem.Events
{
    /// <summary>
    /// Contains context information about a fired event.
    /// </summary>
    public abstract class EventContext
    {
        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the arguments of the event.
        /// </summary>
        public abstract object[] Arguments { get; }

        /// <summary>
        /// Gets or sets the index of the argument which indicates the target (subject) entity of the event. The event will be
        /// invoked on components of the target entity.
        /// </summary>
        public abstract int TargetArgumentIndex { get; set; }

        /// <summary>
        /// Gets or sets the event name substitute used when invoking the event on a component.
        /// </summary>
        public abstract string ComponentTargetName { get; set; }

        /// <summary>
        /// Gets or sets an object which can be used by event listeners as a single argument instead of all the arguments.
        /// </summary>
        public abstract object ArgumentsSubstitute { get; set; }

        /// <summary>
        /// Gets the service provider which can be used for providing services for events.
        /// </summary>
        public abstract IServiceProvider EventServices { get; }
    }
}