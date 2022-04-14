namespace SampSharp.Entities.SAMP
{
    internal class PlayerClickMapMiddleware
    {
        private readonly ArgumentsOverrideEventContext _context = new(2);
        private readonly EventDelegate _next;

        public PlayerClickMapMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            var playerEntity = SampEntities.GetPlayerId((int) context.Arguments[0]);
            
            if (!entityManager.Exists(playerEntity))
                return null;

            _context.BaseContext = context;
            _context.Arguments[0] = playerEntity;
            _context.Arguments[1] = new Vector3((float)context.Arguments[1], (float)context.Arguments[2], (float)context.Arguments[3]); // position

            return _next(_context);
        }
    }
}