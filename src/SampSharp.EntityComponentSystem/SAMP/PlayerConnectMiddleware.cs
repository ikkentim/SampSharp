using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Events;

namespace SampSharp.EntityComponentSystem.SAMP
{
    public class PlayerConnectMiddleware
    {
        private readonly EventDelegate _next;

        public PlayerConnectMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            // TODO: Parenting to connection
            context.Arguments[0] = entityManager.Create(null, SampEntities.GetPlayerId((int)context.Arguments[0]));

            return _next(context);
        }
    }
}