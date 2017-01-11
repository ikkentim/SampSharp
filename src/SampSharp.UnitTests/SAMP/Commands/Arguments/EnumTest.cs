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
    public class EnumTest : ArgumentTest<EnumType<EnumTest.TestEnum>>
    {
        public enum TestEnum
        {
            ValueA = 0,
            ValueB = 1,
            ValueC = 2
        }

        private bool _testForValue;

        #region Overrides of ArgumentTest<EnumType<TestEnum>>

        protected override void Prepare(EnumType<TestEnum> type)
        {
            type.TestForValue = _testForValue;

            base.Prepare(type);
        }

        #endregion

        [TestMethod]
        public void RegularTest()
        {
            _testForValue = false;
            Test("ValueA", TestEnum.ValueA, "");
        }

        [TestMethod]
        public void SubstringTest()
        {
            _testForValue = false;
            Test("A", TestEnum.ValueA, "");
        }

        [TestMethod]
        public void ColisionTest()
        {
            _testForValue = false;
            Test("Value", false);
        }

        [TestMethod]
        public void NothingTest()
        {
            _testForValue = false;
            Test("", false);
        }

        [TestMethod]
        public void ValueTest()
        {
            _testForValue = true;
            Test("2", TestEnum.ValueC, "");
        }

        [TestMethod]
        public void NoMatchTest()
        {
            _testForValue = false;
            Test("NoValue", false);
        }
    }
}