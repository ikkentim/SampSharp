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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampSharp.GameMode.SAMP.Commands;

namespace SampSharp.UnitTests.SAMP.Commands
{
    [TestClass]
    public class CommandPathTest
    {
        [TestMethod]
        public void CommandPathMatchTest()
        {
            var path = new CommandPath("alpha", "bravo", "charly", "delta");

            Assert.IsTrue(path.Matches("alpha bravo charly delta"));
            Assert.IsFalse(path.Matches("alpha b charly delta"));
            Assert.IsFalse(path.Matches("alpha bravo charly"));
            Assert.IsFalse(path.Matches("alpha bravo"));
            Assert.IsFalse(path.Matches("alpha"));
        }

        [TestMethod]
        public void CommandPathLengthTest()
        {
            var path = new CommandPath("alpha", "bravo", "charly", "delta");

            Assert.AreEqual("alpha bravo charly delta".Length, path.Length);
        }
    }
}