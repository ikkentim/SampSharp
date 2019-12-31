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

using SampSharp.Entities.Events;
using SampSharp.Entities.SAMP.Middleware;

namespace SampSharp.Entities.SAMP.Systems
{
    internal class ActorSystem : IConfiguringSystem
    {
        public void Configure(IEcsBuilder builder)
        {
            builder.EnableEvent<int, int>("OnActorStreamIn");
            builder.EnableEvent<int, int>("OnActorStreamOut");

            builder.UseMiddleware<EntityMiddleware>("OnActorStreamIn", 0, SampEntities.ActorType, true);
            builder.UseMiddleware<EntityMiddleware>("OnActorStreamIn", 1, SampEntities.PlayerType, true);
            builder.UseMiddleware<EntityMiddleware>("OnActorStreamOut", 0, SampEntities.ActorType, true);
            builder.UseMiddleware<EntityMiddleware>("OnActorStreamOut", 1, SampEntities.PlayerType, true);
        }
    }
}