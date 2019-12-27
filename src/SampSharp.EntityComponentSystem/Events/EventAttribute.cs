using System;

namespace SampSharp.EntityComponentSystem.Events
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventAttribute : Attribute
    {
        public string Name { get; set; }
    }
}