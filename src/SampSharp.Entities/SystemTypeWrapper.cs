using System;

namespace SampSharp.Entities
{
    internal class SystemTypeWrapper
    {
        public SystemTypeWrapper(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}