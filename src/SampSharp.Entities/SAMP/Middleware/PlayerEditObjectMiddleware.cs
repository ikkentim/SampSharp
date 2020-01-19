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
    internal class PlayerEditObjectMiddleware
    {
        private readonly ArgumentsOverrideEventContext _context = new ArgumentsOverrideEventContext(5);
        private readonly EventDelegate _next;

        public PlayerEditObjectMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context)
        {
            var inArgs = context.Arguments;
            EntityId playerEntity = (SampEntities.GetPlayerId((int) inArgs[0]));
            
            var isPlayerObject = (bool) inArgs[1];
            var objectId = (int) inArgs[2];

            var objectEntity = isPlayerObject
                ? SampEntities.GetPlayerObjectId(playerEntity, objectId)
                : SampEntities.GetObjectId(objectId);
            
            // Allow unknown objects to be passed through to the event.

            _context.BaseContext = context;

            var args = _context.Arguments;
            args[0] = playerEntity;
            args[1] = objectEntity;
            args[2] = inArgs[3]; // response
            args[3] = new Vector3((float)inArgs[4], (float)inArgs[5], (float)inArgs[6]); // position
            args[4] = new Vector3((float)inArgs[7], (float)inArgs[8], (float)inArgs[9]); // rotation

            return _next(_context);
        }
    }
}