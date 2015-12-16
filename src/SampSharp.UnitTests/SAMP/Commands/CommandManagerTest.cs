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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace SampSharp.UnitTests.SAMP.Commands
{
    [TestClass]
    public class CommandManagerTest
    {
        public static void A(BasePlayer player, string arg1)
        {
        }

        [TestMethod]
        public void FindCommandTest()
        {
            Action<string, string, string, bool> action = (methodName, commandPath, runCommand, expectSuccess) =>
            {
                var manager = new CommandsManager(new TestGameMode());
                var expectedMethod = GetType().GetMethod(methodName);
                manager.Register(new[] {new CommandPath(commandPath)}, null, false, null, expectedMethod, null);

                var actualCommand = (manager.GetCommandForText(new BasePlayer(), runCommand) as DefaultCommand)?.Method;

                if (expectSuccess)
                    Assert.AreEqual(expectedMethod, actualCommand);
                else
                    Assert.AreNotEqual(expectedMethod, actualCommand);
            };

            action("A", "cmd a", "/cmd a", true);
            action("A", "cmd a", "/cmd a a", true);
            action("A", "cmd b", "/cmd a", false);
            action("A", "cmd c a", "/cmd c", false);
        }
    }
}