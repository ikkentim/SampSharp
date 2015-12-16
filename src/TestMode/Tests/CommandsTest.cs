// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class CommandsTest : ITest
    {
        [CommandGroup("alpha", "a")]
        class A
        {
            [CommandGroup("bravo", "b")]
            class B
            {
                [Command("whisper", Shortcut = "w")]
                public static void WhisperCommand(BasePlayer p, string message)
                {
                    Console.WriteLine("Whipser: {0}", message);
                    p.SendClientMessage("You whispered {0}", message);
                }
            }
        }

        [Command("test me", Shortcut = "testme", UsageMessage = "Usage: /testme [number]")]
        public static void TestCommand(BasePlayer player, int value)
        {
            Console.WriteLine($"Test with int {value}");
            player.SendClientMessage($"You tested with int {value}");
        }

        [Command("test me", UsageMessage = "Usage: /test me [decimal]")]
        public static void TestCommand(BasePlayer player, float value)
        {
            Console.WriteLine($"Test with float {value}");
            player.SendClientMessage($"You tested with float {value}");
        }

        [Command("test me", UsageMessage = "Usage: /test me [player]")]
        public static void TestCommand(BasePlayer player, BasePlayer value)
        {
            Console.WriteLine($"Test with BasePlayer {value}");
            player.SendClientMessage($"You tested with BasePlayer {value}");
        }

        [Command("test me", UsageMessage = "Usage: /test me [model]")]
        public static void TestCommand(BasePlayer player, VehicleModelType value)
        {
            Console.WriteLine($"Test with VehicleModelType {value}");
            player.SendClientMessage($"You tested with VehicleModelType {value}");
        }

        [Command("awkward")]
        public static void AwkwardCommand(BasePlayer sender, int num1, int num2, string word1, string word2,
            float float1, string text = null)
        {
            sender.SendClientMessage("That was awkward.");
        }

        #region Implementation of ITest

        public void Start(GameMode gameMode)
        {
            var m = gameMode.Services.GetService<ICommandsManager>();
            Console.WriteLine($"Commands: {m.Commands.Count}");
            foreach (var command in m.Commands)
            {
                Console.WriteLine($"  {command}");
            }
        }

        #endregion
    }
}