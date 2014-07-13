using System.Runtime.InteropServices;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class MenuTest : ITest
    {
        public void Start(GameMode gameMode)
        {

        }

        [Command("menu")]
        public static bool MenuCommand(Player player)
        {
            Menu m = new Menu("Test menu", 0, 0);

            m.Columns.Add(new MenuColumn(100));

            m.Rows.Add(new MenuRow("Active"));
            m.Rows.Add(new MenuRow("Disabled", true));
            m.Rows.Add(new MenuRow("Active2"));

            m.Show(player);

            m.Exit += (o, eventArgs) =>
            {
                eventArgs.Player.SendClientMessage(Color.Red, "MENU CLOSED");
                m.Dispose();
            };

            m.Response += (o, eventArgs) =>
            {
                eventArgs.Player.SendClientMessage(Color.Green, "SELECTED ROW " + eventArgs.Row);
                m.Dispose();
            };

            return true;
        }

    }
}
