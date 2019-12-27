using System;

namespace SampSharp.EntityComponentSystem.Events
{
    public abstract class EventContext
    {
        public abstract string Name { get; }

        public abstract object[] Arguments { get; }

        public abstract IServiceProvider EventServices { get; }
    }
}