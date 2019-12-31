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
using SampSharp.Entities.SAMP.Components;
using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Middleware
{
    internal class PlayerConnectMiddleware
    {
        private readonly EventDelegate _next;

        public PlayerConnectMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            // TODO: Parenting to connection
            var entity = entityManager.Create(null, SampEntities.GetPlayerId((int) context.Arguments[0]));

            entity.AddComponent<NativePlayer>();
            entity.AddComponent<Player>();

            context.Arguments[0] = entity;

            return _next(context);
        }
    }
}