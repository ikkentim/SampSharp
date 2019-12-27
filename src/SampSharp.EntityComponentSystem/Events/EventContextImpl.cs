using System;

namespace SampSharp.EntityComponentSystem.Events
{
    internal class EventContextImpl : EventContext
    {
        private object[] _arguments;
        private string _name;
        private IServiceProvider _eventServices;

        public override string Name => _name;

        public override object[] Arguments => _arguments;

        public override IServiceProvider EventServices => _eventServices;

        public void SetName(string name) => _name = name;
        public void SetArguments(object[] arguments) => _arguments = arguments;
        public void SetEventServices(IServiceProvider eventServices) => _eventServices = eventServices;
    }
}