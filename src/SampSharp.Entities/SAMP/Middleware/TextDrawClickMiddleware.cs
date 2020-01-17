namespace SampSharp.Entities.SAMP
{
    internal class TextDrawClickMiddleware
    {
        private readonly EventDelegate _next;

        public TextDrawClickMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager, IEventService eventService)
        {
            var playerEntity = entityManager.Get(SampEntities.GetPlayerId((int) context.Arguments[0]));
            var textDrawEntity = entityManager.Get(SampEntities.GetTextDrawId((int) context.Arguments[1]));

            if (playerEntity == null)
                return null;

            // Forward to OnPlayerCancelTextDraw and cancel continuation OnPlayerClickTextDraw event.
            if (textDrawEntity == null)
            {
                return eventService.Invoke("OnPlayerCancelTextDraw", playerEntity);
            }

            context.Arguments[0] = playerEntity;
            context.Arguments[1] = textDrawEntity;

            return _next(context);
        }
    }
}