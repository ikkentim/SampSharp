// SampSharp
// Copyright 2016 Tim Potze
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
    public class IntegerTest : ArgumentTest<IntegerType>
    {
        [TestMethod]
        public void TextTest()
        {
            Test("no number", false);
        }

        [TestMethod]
        public void RegularTest()
        {
            Test("123456", 123456, "");
        }

        [TestMethod]
        public void RegularWithRemainingNumbersTest()
        {
            Test("123456 789", 123456, "789");
        }

        [TestMethod]
        public void CommaThousandsSeparatorsWithRemainingNumbersTest()
        {
            Test("123,456 789", false);
        }

        [TestMethod]
        public void CommaThousandsSeparatorsTest()
        {
            Test("123,456,789", false);
        }

        [TestMethod]
        public void DotThousandsSeparatorsTest()
        {
            Test("123.456.789", false);
        }

        [TestMethod]
        public void MixedThousandsSeparatorsTest()
        {
            Test("123.456,789", false);
        }
    }
}