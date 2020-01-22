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
            var playerEntity = SampEntities.GetPlayerId((int) inArgs[0]);
            
            if (!entityManager.Exists(playerEntity))
                return null;

            var hitType = (int) context.Arguments[2];
            var hitId = (int) context.Arguments[3];

            EntityId hit;
            switch ((BulletHitType) hitType)
            {
                case BulletHitType.Vehicle:
                    hit = SampEntities.GetVehicleId(hitId);
                    break;
                case BulletHitType.Object:
                    hit = SampEntities.GetObjectId(hitId);
                    break;
                case BulletHitType.Player:
                    hit = SampEntities.GetPlayerId(hitId);
                    break;
                case BulletHitType.PlayerObject:
                    hit = SampEntities.GetPlayerObjectId(playerEntity, hitId);
                    break;
                default:
                    hit = default;
                    break;
            }

            // It could be the hit entity does not exist, for instance if it is a player object created by
            // the streamer plugin. We can't however dismiss this event or else the user will not be able
            // to handle the event in some other way.

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