using System;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemTextdrawTest : ISystem
    {
        private TextDraw _welcome;

        [Event]
        public void OnGameModeInit(IWorldService worldService)
        {
            _welcome = worldService.CreateTextDraw(new Vector2(20, 40), "Hello, world");
            _welcome.Alignment = TextDrawAlignment.Left;
            _welcome.Font = TextDrawFont.Diploma;
            _welcome.Proportional = true;
            Console.WriteLine("TD pos: " + _welcome.Position);
            Console.WriteLine(_welcome.Entity.ToString());
        }


        [Event]
        public void OnPlayerConnect(Player player)
        {
            _welcome.Show(player.Entity);
        }

        [PlayerCommand]
        public void HelloPlayerCommand(Player player, IWorldService worldService)
        {
            var welcome = worldService.CreatePlayerTextDraw(player, new Vector2(100, 80), "Hello, Player");
            welcome.Alignment = TextDrawAlignment.Left;
            welcome.Font = TextDrawFont.Diploma;
            welcome.Proportional = true;
            welcome.LetterSize = new Vector2(1, 1.2f);
            welcome.Show();
            player.SendClientMessage("Show see msg now...");
        }
    }
}