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

using SampSharp.Entities.SAMP.Definitions;

namespace SampSharp.Entities.SAMP.Middleware
{
    internal class PlayerWeaponShotMiddleware
    {
        private readonly EventDelegate _next;

        public PlayerWeaponShotMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            var playerEntity = entityManager.Get(SampEntities.GetPlayerId((int) context.Arguments[0]));

            if (playerEntity == null)
                return null;

            var hitType = (int) context.Arguments[2];
            var hitId = (int) context.Arguments[3];

            Entity hit;
            switch ((BulletHitType) hitType)
            {
                case BulletHitType.Vehicle:
                    hit = entityManager.Get(SampEntities.GetVehicleId(hitId));
                    break;
                case BulletHitType.Object:
                    hit = entityManager.Get(SampEntities.GetObjectId(hitId));
                    break;
                case BulletHitType.Player:
                    hit = entityManager.Get(SampEntities.GetPlayerId(hitId));
                    break;
                case BulletHitType.PlayerObject:
                    hit = entityManager.Get(SampEntities.GetPlayerObjectId(playerEntity.Id, hitId));
                    break;
                default:
                    hit = null;
                    break;
            }

            context.Arguments[0] = playerEntity;
            context.Arguments[3] = hit;

            return _next(context);
        }
    }
}