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
    public class FloatTest : ArgumentTest<FloatType>
    {
        [TestMethod]
        public void TextTest()
        {
            Test("no number", false);
        }

        [TestMethod]
        public void RegularTest()
        {
            Test("123456", 123456f, "");
        }

        [TestMethod]
        public void RegularWithRemainingNumbersTest()
        {
            Test("123456 789", 123456f, "789");
        }

        [TestMethod]
        public void CommaDecimalSeparatorsWithRemainingNumbersTest()
        {
            Test("123,456 789", 123.456f, "789");
        }

        [TestMethod]
        public void DotDecimalSeparatorsWithRemainingNumbersTest()
        {
            Test("123.456 789", 123.456f, "789");
        }

        [TestMethod]
        public void CommaThousandsSeparatorsTest()
        {
            Test("123,456,789", 123456789f, "");
        }

        [TestMethod]
        public void DotThousandsSeparatorsTest()
        {
            Test("123.456.789", 123456789f, "");
        }

        [TestMethod]
        public void DotThousandsAndCommaDecimalSeparatorTest()
        {
            Test("123.456,789", 123456.789f, "");
        }

        [TestMethod]
        public void CommaThousandsAndDotDecimalSeparatorWithRemainderTest()
        {
            Test("123,456.789  k", 123456.789f, "k");
        }
    }
}