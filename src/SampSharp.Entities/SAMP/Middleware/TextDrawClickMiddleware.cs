namespace SampSharp.Entities.SAMP
{
    internal class TextDrawClickMiddleware
    {
        private readonly EventDelegate _next;

        public TextDrawClickMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEventService eventService, IEntityManager entityManager)
        {
            var playerEntity = SampEntities.GetPlayerId((int) context.Arguments[0]);
            var textDrawEntity = SampEntities.GetTextDrawId((int) context.Arguments[1]);
            
            if (!entityManager.Exists(playerEntity))
                return null;
            
            // Allow unknown text draws to be passed through to the event.

            // Forward to OnPlayerCancelTextDraw and cancel continuation OnPlayerClickTextDraw event.
            if (textDrawEntity == NativeTextDraw.InvalidId)
            {
                return eventService.Invoke("OnPlayerCancelTextDraw", playerEntity);
            }

            context.Arguments[0] = playerEntity;
            context.Arguments[1] = textDrawEntity;

            return _next(context);
        }
    }
}