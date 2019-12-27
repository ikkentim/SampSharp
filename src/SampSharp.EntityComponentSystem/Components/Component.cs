using System;
using System.Collections.Generic;
using System.Text;
using SampSharp.EntityComponentSystem.Entities;

namespace SampSharp.EntityComponentSystem.Components
{
    public abstract class Component
    {
        public Entity Entity { get; internal set; }
        
        public T GetComponent<T>() where T : Component => Entity.GetComponent<T>();

        public IEnumerable<T> GetComponents<T>() where T : Component => Entity.GetComponents<T>();

        public T GetComponentInChildren<T>() where T : Component => Entity.GetComponentInChildren<T>();

        public IEnumerable<T> GetComponentsInChildren<T>() where T : Component => Entity.GetComponentsInChildren<T>();

        public T GetComponentInParent<T>() where T : Component => Entity.GetComponentInParent<T>();

        public IEnumerable<T> GetComponentsInParent<T>() where T : Component => Entity.GetComponentsInParent<T>();


        protected virtual void OnInitializeComponent(){}
        protected virtual void OnDestroyComponent(){}

        internal void InitializeComponent()
        {
            OnInitializeComponent();
        }

        internal void DestroyComponent()
        {
            OnDestroyComponent();
        }
    }
}
