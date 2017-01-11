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
using SampSharp.GameMode.SAMP.Commands.ParameterTypes;

namespace SampSharp.UnitTests.SAMP.Commands.Arguments
{
    [TestClass]
    public class WordTest : ArgumentTest<WordType>
    {
        [TestMethod]
        public void NoTextTest()
        {
            Test("", false);
        }

        [TestMethod]
        public void RegularTest()
        {
            Test("some text", "some", "text");
        }

        [TestMethod]
        public void OneWordTest()
        {
            Test("one-word", "one-word", "");
        }
    }
}