using System;
using SampSharp.Entities;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3400:Methods should not return constants", Justification = "Testing purposes")]
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
            throw new InvalidOperationException("RCON threw an error");
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