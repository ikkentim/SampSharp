using System;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using TestMode.Entities.Components;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemPlayerCommandTest : ISystem
    {
        [PlayerCommand]
        public void UnusedCommand(UnusedComponent sender)
        {
            Console.WriteLine("How did he manage to call this?! " + sender);
        }

        [PlayerCommand]
        public void TestCommand(Player sender, int a, int b, int c)
        {
            sender.SendClientMessage($"Hello, world! {a} {b} {c}");
        }

        [PlayerCommand]
        public void Test2Command(Player sender, int a, int b, int c, string d)
        {
            sender.SendClientMessage($"Hello, world! {a} {b} {c} {d}");
        }

        [PlayerCommand]
        public void Test3Command(Player sender, int a, int b, int c, string d = "a sensible default")
        {
            sender.SendClientMessage($"Hello, world! {a} {b} {c} {d}");
        }
    }
}