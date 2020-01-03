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
using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Systems
{
    internal class WorldSystem : IConfiguringSystem
    {
        /// <summary>
        /// The type of a world entity.
        /// </summary>
        public static readonly Guid WorldType = new Guid("DD999ED7-9935-4F66-9CDC-E77484AF6BB8");

        /// <summary>
        /// The entity identifier used by the world entity.
        /// </summary>
        public static readonly EntityId WorldId = new EntityId(WorldType, 0);

        public void Configure(IEcsBuilder builder)
        {
            builder.Services.GetService<IEntityManager>()
                .Create(null, WorldId)
                .AddComponent<NativeWorld>();
        }
    }
}