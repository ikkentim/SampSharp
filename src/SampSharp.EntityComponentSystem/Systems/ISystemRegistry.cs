using System;
using System.Collections.Generic;

namespace SampSharp.EntityComponentSystem.Systems
{
    public interface ISystemRegistry
    {
        IEnumerable<Type> Systems { get; }
        IEnumerable<Type> ConfiguringSystems { get; }

        void Add(Type type);
    }
}