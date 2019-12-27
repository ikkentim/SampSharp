using SampSharp.EntityComponentSystem.Components;
using SampSharp.EntityComponentSystem.Events;

namespace TestMode.Ecs.Components
{
    public class TestComponent : Component
    {
        // Components will allow adding behaviour to entities by responding to events. These events will also allow dependency injection

        public string Hi => $"Hi, {Entity}";
        [Event]
        public void OnPlayerText(string text)
        {
            // TODO: Still working on getting this to be called
        }
    }
}