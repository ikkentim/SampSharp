// SampSharp
// Copyright 2017 Tim Potze
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
using System.Linq;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.SAMP.Commands.Parameters;
using SampSharp.GameMode.SAMP.Commands.ParameterTypes;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class CommandsTest// : ITest
    {
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

        [Command("color")]
        public static void TestComand(BasePlayer sender,
            [Parameter(typeof (CustomType))] Color color)
        {
            sender.SendClientMessage(color, "YOU CHOSE THIS COLOR!!!");
        }
        
        [CommandGroup("alpha", "a")]
        private class A
        {
            [CommandGroup("bravo", "b")]
            private class B
            {
                [Command("whisper", Shortcut = "w")]
                public static void WhisperCommand(BasePlayer p, string message)
                {
                    Console.WriteLine("Whipser: {0}", message);
                    p.SendClientMessage("You whispered {0}", message);
                }
            }
        }

        private class CustomType : ICommandParameterType
        {
            #region Implementation of ICommandParameterType

            /// <summary>
            ///     Gets the value for the occurance of this parameter type at the start of the commandText. The processed text will be
            ///     removed from the commandText.
            /// </summary>
            /// <param name="commandText">The command text.</param>
            /// <param name="output">The output.</param>
            /// <returns>true if parsed successfully; false otherwise.</returns>
            public bool Parse(ref string commandText, out object output)
            {
                output = null;

                // Can't parse without intput.
                if (string.IsNullOrWhiteSpace(commandText))
                    return false;

                // Get the first word.
                var word = commandText.TrimStart().Split(' ').First();

                // Set the output (color) based on the input.
                switch (word.ToLower())
                {
                    case "red":
                        output = Color.Red;
                        break;
                    case "green":
                        output = Color.Green;
                        break;
                    case "blue":
                        output = Color.Blue;
                        break;
                }

                // Remove the word from the input and trim the start.
                if (output != null)
                {
                    commandText = commandText.Length == word.Length
                        ? string.Empty
                        : commandText.Substring(word.Length).TrimStart();
                    return true;
                }

                return false;
            }

            #endregion
        }
    }
}