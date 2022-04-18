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
/// When added to a static readonly <see cref="Guid" /> field, the attribute specifies the type name of entities with the
/// entity type Guid specified in this field. If no name is specified in the the attribute, the field name of is used as the type name.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class EntityTypeAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityTypeAttribute" /> class.
    /// </summary>
    public EntityTypeAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityTypeAttribute" /> class.
    /// </summary>
    /// <param name="name">The name of the entity type.</param>
    /// <param name="invalidHandle">The default handle is used to indicate an invalid instance of this entity.</param>
    public EntityTypeAttribute(string name, int invalidHandle = -1)
    {
        Name = name;
        InvalidHandle = invalidHandle;
    }

    /// <summary>
    /// Gets or sets the name of the entity type.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the default handle is used to indicate an invalid instance of this entity.
    /// </summary>
    public int InvalidHandle { get; set; } = -1;
}