using System;
using SampSharp.Entities;
using SampSharp.Entities.Events;
using TestMode.Entities.Services;

namespace TestMode.Entities.Components
{
    public class TestComponent : Component
    {
        public string WelcomingMessage => $"WelcomingMessage, {Entity}";
    }
}