using SampSharp.Entities.SAMP.Definitions;

namespace SampSharp.Entities.SAMP.Middleware
{
    internal class PlayerSelectObjectMiddleware
    {
        private readonly EventDelegate _next;

        public PlayerSelectObjectMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            var playerEntity = entityManager.Get(SampEntities.GetPlayerId((int) context.Arguments[0]));
            
            if (playerEntity == null)
                return null;

            var objectType = (int) context.Arguments[1];
            var objectId = (int) context.Arguments[2];

            var objectEntity = objectType == (int) ObjectType.PlayerObject
                ? entityManager.Get(SampEntities.GetPlayerObjectId(playerEntity.Id, objectId))
                : entityManager.Get(SampEntities.GetObjectId(objectId));

            if (objectEntity == null)
                return null;

            context.Arguments[0] = playerEntity;
            context.Arguments[2] = objectEntity;

            return _next(context);
        }
    }
}