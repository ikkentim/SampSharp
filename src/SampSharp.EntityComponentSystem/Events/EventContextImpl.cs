using System;
using SampSharp.EntityComponentSystem.Entities;

namespace SampSharp.EntityComponentSystem.Events
{
    internal class EventContextImpl : EventContext
    {
        private string _name;
        private IServiceProvider _eventServices;

        public override string Name => _name;

        public override object[] Arguments { get; set; }
        public override int TargetArgumentIndex { get; set; } = -1;
        public override string ComponentTargetName { get; set; }
        public override object ArgumentsSubstitute { get; set; }


        public override IServiceProvider EventServices => _eventServices;

        public void SetName(string name) => _name = name;
        public void SetEventServices(IServiceProvider eventServices) => _eventServices = eventServices;
    }
}