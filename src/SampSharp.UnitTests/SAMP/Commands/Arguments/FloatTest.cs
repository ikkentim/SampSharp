using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampSharp.GameMode.SAMP.Commands.Parameters;

namespace SampSharp.UnitTests.SAMP.Commands.Arguments
{
    [TestClass]
    public class FloatTest : ArgumentTest<FloatCommandParameterType>
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