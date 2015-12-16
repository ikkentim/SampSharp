using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampSharp.GameMode.SAMP.Commands.Parameters;

namespace SampSharp.UnitTests.SAMP.Commands.Arguments
{
    public abstract class ArgumentTest<T> where T : ICommandParameterType
    {
        protected void Test(string commandText, bool expectedResult, object expectedOutput, string expectedRemainder)
        {
            var parameter = Activator.CreateInstance<T>();

            Prepare(parameter);

            object output;
            var result = parameter.GetValue(ref commandText, out output);

            Assert.AreEqual(expectedResult, result, "Unexpected result");
            Assert.AreEqual(expectedOutput, output, "Unexpected output");
            Assert.AreEqual(expectedRemainder, commandText, "Unexpected command text remainder");
        }

        protected virtual void Prepare(T commandParameterType)
        {
            
        }

        protected void Test(string commandText, object expectedOutput, string expectedRemainder)
        {
            Test(commandText, true, expectedOutput, expectedRemainder);
        }

        protected void Test(string commandText, bool expectedResult)
        {
            Test(commandText, false, null, commandText);
        }
    }
}