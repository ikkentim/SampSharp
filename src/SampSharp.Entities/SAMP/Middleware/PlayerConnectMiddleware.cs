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
        var entity = SampEntities.GetPlayerId((int) context.Arguments[0]);

        entityManager.Create(entity);
        entityManager.AddComponent<NativePlayer>(entity);
        entityManager.AddComponent<Player>(entity);

        context.Arguments[0] = entity;

        return _next(context);
    }
}