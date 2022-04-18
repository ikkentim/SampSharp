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
using System.Runtime.Serialization;

namespace SampSharp.Entities;

/// <summary>
/// The exception that is thrown when an entity could not be found.
/// </summary>
[Serializable]
public class EntityNotFoundException : ArgumentException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException" /> class.
    /// </summary>
    /// <param name="paramName">Name of the parameter.</param>
    public EntityNotFoundException(string paramName) : base("The specified entity could not be found.", paramName)
    {
    }
        
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException" /> class.
    /// </summary>
    /// <param name="info">The object that holds the serialized object data.</param>
    /// <param name="context">The contextual information about the source or destination.</param>
    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}