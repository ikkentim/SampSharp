using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;

namespace TestMode.Tests
{
    public class MenuTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            gameMode.PlayerConnected += (sender, args) =>
            {
                Menu m = new Menu("Test menu", 0, 0, new[] {new MenuColumn(100)},
                    new[] {new MenuRow("Active"), new MenuRow("Disabled", true), new MenuRow("Active2"),});

                m.Show(args.Player);

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
            };
        }

    }
}
