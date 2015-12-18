using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampSharp.GameMode.SAMP.Commands.Parameters;
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