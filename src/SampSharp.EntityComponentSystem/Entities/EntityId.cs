using System;

namespace SampSharp.EntityComponentSystem.Entities
{
    public struct EntityId
    {
        public EntityId(Guid type, int handle)
        {
            Type = type;
            Handle = handle;
        }

        public Guid Type { get; }
        public int Handle { get; }

        public override string ToString()
        {
            return $"(Type = {Type}, Handle = {Handle})";
        }
    }
}