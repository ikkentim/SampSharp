namespace SampSharp.Entities.SAMP
{
    internal class PlayerTextDrawMiddleware
    {
        private readonly EventDelegate _next;

        public PlayerTextDrawMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            var playerEntity = entityManager.Get(SampEntities.GetPlayerId((int) context.Arguments[0]));
            var textDrawEntity = entityManager.Get(SampEntities.GetPlayerTextDrawId((int) context.Arguments[0], (int) context.Arguments[1]));

            if (playerEntity == null || textDrawEntity == null)
                return null;

            context.Arguments[0] = playerEntity;
            context.Arguments[1] = textDrawEntity;

            return _next(context);
        }
    }
}