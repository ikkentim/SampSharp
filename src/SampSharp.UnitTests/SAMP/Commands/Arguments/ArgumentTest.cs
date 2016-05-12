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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampSharp.GameMode.SAMP.Commands.ParameterTypes;

namespace SampSharp.UnitTests.SAMP.Commands.Arguments
{
    public abstract class ArgumentTest<T> where T : ICommandParameterType
    {
        protected void Test(string commandText, bool expectedResult, object expectedOutput, string expectedRemainder)
        {
            var parameter = Activator.CreateInstance<T>();

            Prepare(parameter);

            object output;
            var result = parameter.Parse(ref commandText, out output);

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