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

internal class PlayerEditAttachedObjectMiddleware
{
    private readonly ArgumentsOverrideEventContext _context = new(8);
    private readonly EventDelegate _next;

    public PlayerEditAttachedObjectMiddleware(EventDelegate next)
    {
        _next = next;
    }

    public object Invoke(EventContext context, IEntityManager entityManager)
    {
        var inArgs = context.Arguments;
        var playerEntity = SampEntities.GetPlayerId((int) inArgs[0]);
            
        if (!entityManager.Exists(playerEntity))
            return null;

        _context.BaseContext = context;

        var args = _context.Arguments;

        args[0] = playerEntity;
        args[1] = inArgs[1]; // response
        args[2] = inArgs[2]; // index
        args[3] = inArgs[3]; // modelId
        args[4] = inArgs[4]; // bone
        args[5] = new Vector3((float)inArgs[5], (float)inArgs[6], (float)inArgs[7]); // offset
        args[6] = new Vector3((float)inArgs[8], (float)inArgs[9], (float)inArgs[10]); // rotation
        args[7] = new Vector3((float)inArgs[11], (float)inArgs[12], (float)inArgs[13]); // scale

        return _next(_context);
    }
}