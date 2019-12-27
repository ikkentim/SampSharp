using System;
using SampSharp.EntityComponentSystem.Entities;

namespace SampSharp.EntityComponentSystem.Events
{
    public abstract class EventContext
    {
        public abstract string Name { get; }

        public abstract object[] Arguments { get; set; }
        
        public abstract int TargetArgumentIndex { get; set; }

        public abstract string ComponentTargetName { get; set; }

        public abstract object ArgumentsSubstitute { get; set; }

        public abstract IServiceProvider EventServices { get; }
    }
}