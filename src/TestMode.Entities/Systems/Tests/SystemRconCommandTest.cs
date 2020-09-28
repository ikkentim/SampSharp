using System;
using SampSharp.Entities;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemRconCommandTest : ISystem
    {
        [RconCommand]
        public bool RetCommand()
        {
            return true;
        }

        [RconCommand]
        public bool RetFalseCommand()
        {
            return false;
        }

        [RconCommand]
        public bool ErrCommand()
        {
            throw new Exception("RCON threw an error");
        }

        [RconCommand]
        public void ArgsCommand(int a, int b, int c)
        {
            Console.WriteLine($"{a} {b} {c}");
        }

        [Event]
        public bool OnRconCommand(string cmd)
        {
            Console.WriteLine("RCON");

            return false;
        }
    }
}