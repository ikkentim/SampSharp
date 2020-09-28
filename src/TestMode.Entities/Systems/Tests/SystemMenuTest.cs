using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemMenuTest : ISystem
    {
        private Menu _menu;

        [Event]
        public void OnGameModeInit(IWorldService worldService)
        {
            _menu = worldService.CreateMenu("Test menu", new Vector2(200, 300), 100);
            _menu.AddItem("Hello!!!");
            _menu.AddItem("Hello!!");
            _menu.AddItem("Hello!");
        }

        [PlayerCommand]
        public void MenuCommand(EntityId sender)
        {
            _menu.Show(sender);
        }
    }
}