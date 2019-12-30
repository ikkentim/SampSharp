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
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Events;

namespace SampSharp.EntityComponentSystem.SAMP.Middleware
{
    /// <summary>
    /// Represents a middleware which replaces an entity id with an entity in the arguments of an event.
    /// </summary>
    public class EntityMiddleware
    {
        private readonly string _componentName;
        private readonly Func<int, EntityId> _idBuilder;
        private readonly int _index;
        private readonly bool _isRequired;
        private readonly bool _isTarget;
        private readonly EventDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next middleware handler.</param>
        /// <param name="index">The index of the parameter which contains the entity identifier.</param>
        /// <param name="idBuilder">
        /// A function which returns an <see cref="EntityId" /> based on the integer value provided in the
        /// parameter of the event.
        /// </param>
        /// <param name="isTarget">If set to <c>true</c>, the parameter is marked as the identifier target entity.</param>
        /// <param name="isRequired">
        /// If set to <c>true</c>, the event will be canceled if no entity could be found with the entity
        /// identifier provided by <paramref name="idBuilder" />.
        /// </param>
        /// <param name="componentName">The event name substitute used when invoking the event on a component.</param>
        public EntityMiddleware(EventDelegate next, int index, Func<int, EntityId> idBuilder, bool isTarget = false,
            bool isRequired = true, string componentName = null)
        {
            _next = next;
            _index = index;
            _idBuilder = idBuilder;
            _isTarget = isTarget;
            _isRequired = isRequired;
            _componentName = componentName;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            var entity = entityManager.Get(_idBuilder((int) context.Arguments[_index]));

            if (entity == null && _isRequired)
                return null;

            context.Arguments[_index] = entity;

            if (_isTarget)
                context.TargetArgumentIndex = _index;

            if (_componentName != null)
                context.ComponentTargetName = _componentName;

            return _next(context);
        }
    }
}