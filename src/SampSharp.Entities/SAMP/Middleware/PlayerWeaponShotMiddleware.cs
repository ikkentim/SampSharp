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