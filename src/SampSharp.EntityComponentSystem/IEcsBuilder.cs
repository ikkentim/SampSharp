using System;
using SampSharp.EntityComponentSystem.Events;

namespace SampSharp.EntityComponentSystem
{
    public interface IEcsBuilder
    {
        IServiceProvider Services { get; }

        IEcsBuilder Use(string name, Func<EventDelegate, EventDelegate> middleware);

        IEcsBuilder UseSystem(Type systemType);
    }
}