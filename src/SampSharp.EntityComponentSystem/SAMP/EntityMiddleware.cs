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
        private readonly bool _isTarget;
        private readonly string _componentName;

        public EntityMiddleware(EventDelegate next, int index, Func<int, EntityId> idBuilder, bool isTarget, string componentName = null)
        {
            _next = next;
            _index = index;
            _idBuilder = idBuilder;
            _isTarget = isTarget;
            _componentName = componentName;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            context.Arguments[_index] = entityManager.Get(_idBuilder((int) context.Arguments[_index]));

            if (_isTarget)
                context.TargetArgumentIndex = _index;

            if (_componentName != null)
                context.ComponentTargetName = _componentName;

            return _next(context);
        }
    }
}