namespace SampSharp.Entities.SAMP.Middleware
{
    internal class PlayerEditObjectMiddleware
    {
        private readonly EventDelegate _next;

        public PlayerEditObjectMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            var playerEntity = entityManager.Get(SampEntities.GetPlayerId((int) context.Arguments[0]));
            
            if (playerEntity == null)
                return null;

            var isPlayerObject = (bool) context.Arguments[1];
            var objectId = (int) context.Arguments[2];

            var objectEntity = isPlayerObject
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