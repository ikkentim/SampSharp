﻿namespace SampSharp.Entities.SAMP
{
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
}