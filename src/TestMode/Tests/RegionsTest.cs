using System;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using SampSharp.GameMode.World.Shapes;

namespace TestMode.Tests
{
    public class RegionsTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            Region test = new Region(new Cube(5, 10, 5, 10, 0, 10));
            Region test2 = new Region(new Sphere(new Vector(30, 30, 1), 10));
            Region test3 = new Region(new Square(-10, 0, -10, 0));
            Region test4 = new Region(new Polygon(new[]
            {
                new Vector(-20),
                new Vector(-20, -30, 0),
                new Vector(-30), 
                new Vector(-30, -20, 0),
 
            
            }));

            test.Enter += (sender, args) => args.Player.SendClientMessage(Color.Red, "Entered test region");
            test.Leave += (sender, args) => args.Player.SendClientMessage(Color.Red, "Left test region");
            test2.Enter += (sender, args) => args.Player.SendClientMessage(Color.White, "Entered test2 region");
            test2.Leave += (sender, args) => args.Player.SendClientMessage(Color.White, "Left test2 region");
            test3.Enter += (sender, args) => args.Player.SendClientMessage(Color.BlueViolet, "Entered test3 region");
            test3.Leave += (sender, args) => args.Player.SendClientMessage(Color.BlueViolet, "Left test3 region");
            test4.Enter += (sender, args) => args.Player.SendClientMessage(Color.OrangeRed, "Entered test4 region");
            test4.Leave += (sender, args) => args.Player.SendClientMessage(Color.OrangeRed, "Left test4 region");

            Console.WriteLine("RegionsTest loaded");
        }

        [Command("coords")]
        public static bool CoordsCommand(Player player)
        {
            player.SendClientMessage(Color.White, "You are at " + player.Position);
            return true;
        }
    }
}
