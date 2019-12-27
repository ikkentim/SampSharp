using System;
using System.Collections.Generic;

namespace SampSharp.EntityComponentSystem.Systems
{
    public class SystemRegistry : ISystemRegistry
    {
        private readonly List<Type> _systems = new List<Type>();
        private readonly List<Type> _configuringSystems = new List<Type>();

        public void Add(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if(!typeof(ISystem).IsAssignableFrom(type))
                throw new ArgumentException("Type must implement ISystem", nameof(type));

            _systems.Add(type);

            if (typeof(IConfiguringSystem).IsAssignableFrom(type))
                _configuringSystems.Add(type);
        }

        public IEnumerable<Type> ConfiguringSystems => _configuringSystems.AsReadOnly();

        public IEnumerable<Type> Systems => _systems.AsReadOnly();
    }
}