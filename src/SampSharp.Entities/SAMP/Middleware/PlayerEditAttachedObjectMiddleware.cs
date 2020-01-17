namespace SampSharp.Entities.SAMP
{
    internal class PlayerEditAttachedObjectMiddleware
    {
        private readonly ArgumentsOverrideEventContext _context = new ArgumentsOverrideEventContext(8);
        private readonly EventDelegate _next;

        public PlayerEditAttachedObjectMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            var inArgs = context.Arguments;
            var playerEntity = entityManager.Get(SampEntities.GetPlayerId((int) inArgs[0]));

            if (playerEntity == null)
                return null;

            _context.BaseContext = context;

            var args = _context.Arguments;

            args[0] = playerEntity;
            args[1] = inArgs[1]; // response
            args[2] = inArgs[2]; // index
            args[3] = inArgs[3]; // modelId
            args[4] = inArgs[4]; // bone
            args[5] = new Vector3((float)inArgs[5], (float)inArgs[6], (float)inArgs[7]); // offset
            args[6] = new Vector3((float)inArgs[8], (float)inArgs[9], (float)inArgs[10]); // rotation
            args[7] = new Vector3((float)inArgs[11], (float)inArgs[12], (float)inArgs[13]); // scale

            return _next(_context);
        }
    }
}