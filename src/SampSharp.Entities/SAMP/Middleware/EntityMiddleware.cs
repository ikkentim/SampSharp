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

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a middleware which replaces an integer entity id with an entity in the arguments of an event.
/// </summary>
public class EntityMiddleware
{
    private readonly Guid _entityType;
    private readonly int _index;
    private readonly bool _isRequired;
    private readonly EventDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityMiddleware" /> class.
    /// </summary>
    /// <param name="next">The next middleware handler.</param>
    /// <param name="index">The index of the parameter which contains the entity identifier.</param>
    /// <param name="entityType">The type of the <see cref="EntityId" />.</param>
    /// <param name="isRequired">
    /// If set to <c>true</c>, the event will be canceled if no entity could be found with the entity
    /// identifier.
    /// </param>
    public EntityMiddleware(EventDelegate next, int index, Guid entityType, bool isRequired = true)
    {
        _next = next;
        _index = index;
        _entityType = entityType;
        _isRequired = isRequired;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    public object Invoke(EventContext context, IEntityManager entityManager)
    {
        var entity = new EntityId(_entityType, (int) context.Arguments[_index]);

        if (_isRequired && !entityManager.Exists(entity))
            return null;

        context.Arguments[_index] = entity;

        return _next(context);
    }
}