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

internal class UnoccupiedVehicleUpdateMiddleware
{
    private readonly ArgumentsOverrideEventContext _context = new(5);
    private readonly EventDelegate _next;

    public UnoccupiedVehicleUpdateMiddleware(EventDelegate next)
    {
        _next = next;
    }

    public object Invoke(EventContext context, IEntityManager entityManager)
    {
        var inArgs = context.Arguments;
        var vehicleEntity = SampEntities.GetVehicleId((int) inArgs[0]);
        var playerEntity = SampEntities.GetPlayerId((int) inArgs[1]);
            
        if (!entityManager.Exists(playerEntity) || !entityManager.Exists(vehicleEntity))
            return null;

        _context.BaseContext = context;

        var args = _context.Arguments;
        args[0] = vehicleEntity;
        args[1] = playerEntity;
        args[2] = inArgs[2]; // passengerSeat
        args[3] = new Vector3((float)inArgs[3], (float)inArgs[4], (float)inArgs[5]); // newPosition
        args[4] = new Vector3((float)inArgs[6], (float)inArgs[7], (float)inArgs[8]); // velocity

        return _next(_context);
    }
}