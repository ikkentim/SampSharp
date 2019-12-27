using System;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Events;

namespace SampSharp.EntityComponentSystem.SAMP
{
    public static class SampEntities
    {
        public static Guid PlayerType = new Guid("C96C8A5A-80D6-40EF-9308-4AF28CBE9657");
        public static Guid VehicleType = new Guid("877A5625-9F2A-4C92-BC83-1C6C220A9D05");

        public static EntityId GetPlayerId(int playerId)
        {
            return new EntityId(PlayerType, playerId);
        }
    }
}