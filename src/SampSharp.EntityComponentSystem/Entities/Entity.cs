using System;
using System.Collections.Generic;
using System.Linq;
using SampSharp.EntityComponentSystem.Components;

namespace SampSharp.EntityComponentSystem.Entities
{
    public sealed class Entity
    {
        private readonly List<Component> _components = new List<Component>();
        private readonly List<Entity> _children = new List<Entity>();

        public Entity(Entity parent, EntityId id)
        {
            Parent = parent;
            Id = id;

            parent?._children.Add(this);
        }

        public EntityId Id { get; }

        public Entity Parent { get; private set; }

        public IEnumerable<Entity> Children => _children.AsReadOnly();

        public void AddComponent<T>() where T : Component, new()
        {
            AddComponent(typeof(T));
        }
        
        public void AddComponent(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if(!typeof(Component).IsAssignableFrom(type) || !type.IsClass || type.IsAbstract)
                throw new ArgumentException("The type must be a subtype of Component", nameof(type));

            var component = (Component)Activator.CreateInstance(type);
            component.Entity = this;

            _components.Add(component);

            component.InitializeComponent();
        }

        public T GetComponent<T>() where T : Component
        {
            return _components.OfType<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            return _components.OfType<T>();
        }

        public T GetComponentInChildren<T>() where T : Component
        {
            foreach (var c in _children)
            {
                var component = c.GetComponent<T>() ?? c.GetComponentInChildren<T>();

                if (component != null)
                    return component;
            }

            return null;
        }

        public IEnumerable<T> GetComponentsInChildren<T>() where T : Component
        {
            if (_children.Count == 0)
                return EmptyArray<T>.Instance;

            var result = new List<T>();
            foreach (var c in _children)
                c.GetComponentsInCurrentAndChildren(result);
            return result;
        }
        
        private void GetComponentsInCurrentAndChildren<T>(List<T> result) where T : Component
        {
            result.AddRange(_components.OfType<T>());
            
            foreach (var c in _children)
                c.GetComponentsInCurrentAndChildren(result);
        }

        public T GetComponentInParent<T>() where T : Component
        {
            return Parent?.GetComponent<T>() ?? Parent?.GetComponentInParent<T>();
        }
        
        public IEnumerable<T> GetComponentsInParent<T>() where T : Component
        {
            if (Parent == null)
                return EmptyArray<T>.Instance;

            var result = new List<T>();
            Parent?.GetComponentsInCurrentAndParent(result);
            return result;
        }

        private void GetComponentsInCurrentAndParent<T>(List<T> result) where T : Component
        {
            result.AddRange(_components.OfType<T>());

            Parent?.GetComponentsInCurrentAndParent(result);
        }

        internal void Destroy()
        {
            Parent?._children.Remove(this);
            Parent = null;

            foreach (var component in _components)
            {
                component.DestroyComponent();
            }

            _components.Clear();
        }

        public override string ToString()
        {
            return $"(Id: {Id})";
        }

        private static class EmptyArray<T>
        {
            public static readonly T[] Instance = new T[0];
        }
    }
}