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
using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents the ECS system builder.
    /// </summary>
    /// <seealso cref="IEcsBuilder" />
    public class EcsBuilder : IEcsBuilder
    {
        private readonly IEventService _eventService;

        internal EcsBuilder(IServiceProvider services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            
            _eventService = services.GetRequiredService<IEventService>();
        }

        /// <inheritdoc />
        public IServiceProvider Services { get; }

        /// <inheritdoc />
        public IEcsBuilder EnableEvent(string name, Type[] parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            _eventService.EnableEvent(name, parameters);

            return this;
        }

        /// <inheritdoc />
        public IEcsBuilder UseMiddleware(string name, Func<EventDelegate, EventDelegate> middleware)
        {
            _eventService.UseMiddleware(name, middleware);

            return this;
        }
    }
}