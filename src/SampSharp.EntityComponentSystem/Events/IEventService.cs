using System;

namespace SampSharp.EntityComponentSystem.Events
{
    public interface IEventService
    {
        void Load(string name, params Type[] parameters);
        void Use(string name, Func<EventDelegate, EventDelegate> middleware);
    }
}