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
            var playerEntity = SampEntities.GetPlayerId((int) context.Arguments[0]);
            var textDrawEntity = SampEntities.GetPlayerTextDrawId(playerEntity, (int) context.Arguments[1]);
            
            if (!entityManager.Exists(playerEntity))
                return null;

            // Allow unknown text draws to be passed through to the event.

            context.Arguments[0] = playerEntity;
            context.Arguments[1] = textDrawEntity;

            return _next(context);
        }
    }
}