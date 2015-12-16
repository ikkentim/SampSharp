using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampSharp.GameMode.SAMP.Commands.Arguments;

namespace SampSharp.UnitTests.SAMP.Commands.Arguments
{
    [TestClass]
    public class IntegerTest : ArgumentTest<IntegerCommandParameterType>
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
