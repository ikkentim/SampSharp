using System;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.Events;

namespace SampSharp.EntityComponentSystem.SAMP
{
    public class EntityMiddleware
    {
        private readonly EventDelegate _next;
        private readonly int _index;
        private readonly Func<int, EntityId> _idBuilder;

        public EntityMiddleware(EventDelegate next, int index, Func<int, EntityId> idBuilder)
        {
            _next = next;
            _index = index;
            _idBuilder = idBuilder;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            context.Arguments[_index] = entityManager.Get(_idBuilder((int) context.Arguments[_index]));

            return _next(context);
        }
    }
}