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

namespace SampSharp.Entities.SAMP
{
    internal class PlayerWeaponShotMiddleware
    {
        private readonly ArgumentsOverrideEventContext _context = new ArgumentsOverrideEventContext(4);
        private readonly EventDelegate _next;

        public PlayerWeaponShotMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            var inArgs = context.Arguments;
            var playerEntity = entityManager.Get(SampEntities.GetPlayerId((int) inArgs[0]));

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
            
            _context.BaseContext = context;

            var args = _context.Arguments;
            args[0] = playerEntity;
            args[1] = inArgs[1]; // weapon
            args[2] = hit;
            args[3] = new Vector3((float)inArgs[4], (float)inArgs[5], (float)inArgs[6]); // position

            return _next(_context);
        }
    }
}