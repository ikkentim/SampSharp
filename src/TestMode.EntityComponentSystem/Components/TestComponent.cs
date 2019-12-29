using System;
using SampSharp.EntityComponentSystem.Components;
using SampSharp.EntityComponentSystem.Events;
using TestMode.EntityComponentSystem.Services;

namespace TestMode.EntityComponentSystem.Components
{
    public class TestComponent : Component
    {
        public string WelcomingMessage => $"WelcomingMessage, {Entity}";

        [Event]
        public bool OnText(string text, IFunnyService funny)
        {
            Console.WriteLine($"Player {funny.MakePlayerNameFunny(Entity)} said " + text);

            return true;
        }
    }
}