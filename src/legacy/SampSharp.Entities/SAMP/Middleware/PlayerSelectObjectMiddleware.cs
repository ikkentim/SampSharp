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

namespace SampSharp.Entities.SAMP;

internal class PlayerSelectObjectMiddleware
{
    private readonly ArgumentsOverrideEventContext _context = new(3);
    private readonly EventDelegate _next;

    public PlayerSelectObjectMiddleware(EventDelegate next)
    {
        _next = next;
    }

    public object Invoke(EventContext context, IEntityManager entityManager)
    {
        var playerEntity = SampEntities.GetPlayerId((int)context.Arguments[0]);

        var objectType = (int)context.Arguments[1];
        var objectId = (int)context.Arguments[2];

        var objectEntity = objectType == (int)ObjectType.PlayerObject
            ? SampEntities.GetPlayerObjectId(playerEntity, objectId)
            : SampEntities.GetObjectId(objectId);

        if (!entityManager.Exists(playerEntity))
            return null;

        // Allow unknown objects to be passed through to the event.

        _context.BaseContext = context;

        _context.Arguments[0] = playerEntity;
        _context.Arguments[1] = objectEntity;
        _context.Arguments[2] = context.Arguments[3]; // modelId
        _context.Arguments[3] = new Vector3((float)context.Arguments[4], (float)context.Arguments[5], (float)context.Arguments[6]); // position

        return _next(_context);
    }
}