using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampSharp.GameMode.SAMP.Commands.Parameters;

namespace SampSharp.UnitTests.SAMP.Commands.Arguments
{
    [TestClass]
    public class EnumTest : ArgumentTest<EnumCommandParameterType<EnumTest.TestEnum>> 
    {
        public enum TestEnum
        {
            ValueA = 0,
            ValueB = 1,
            ValueC = 2
        }

        private bool _testForValue;

        #region Overrides of ArgumentTest<EnumCommandParameterType<TestEnum>>

        protected override void Prepare(EnumCommandParameterType<TestEnum> commandParameterType)
        {
            commandParameterType.TestForValue = _testForValue;

            base.Prepare(commandParameterType);
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