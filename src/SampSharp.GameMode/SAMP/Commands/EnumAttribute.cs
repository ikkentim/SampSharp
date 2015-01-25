// SampSharp
// Copyright 2015 Tim Potze
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
using System.Collections.Generic;
using System.Linq;
using SampSharp.GameMode.Helpers;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents an enum command-parameter.
    /// </summary>
    public class EnumAttribute : WordAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the EnumAttribute class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        public EnumAttribute(string name, Type type) : base(name)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException("type is not an enumerator");
            }

            Type = type;
            TestForValue = true;// default to true
        }

        /// <summary>
        ///     Gets the enum type in which to look for values.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        ///     Gets or sets whether input should be matches against the enum values.
        ///     When False, the input will only be matches against the names.
        /// </summary>
        public bool TestForValue { get; set; }

        /// <summary>
        ///     Check if the parameter is well-formatted and return the output.
        /// </summary>
        /// <param name="command">The command text.</param>
        /// <param name="output">The output of this parameter.</param>
        /// <returns>True if the parameter is well-formatted, False otherwise.</returns>
        public override bool Check(ref string command, out object output)
        {
            if (!base.Check(ref command, out output))
                return false;

            string word = (output as string).ToLower();

            /*
             * Find an enum value that contains the given word and select its index.
             */
            string[] names = Type.GetEnumNames();
            Array values = Type.GetEnumValues();
            IEnumerable<string> results =
                names.Where(
                    (e, i) => e.ToLower().Contains(word) || (TestForValue && values.GetValue(i).ToString() == word));

            if (results.Count() > 1)
            {
                results = results.Where(e => e.ToLower() == word);
            }
            if (results.Count() == 1)
            {
                output = values.GetValue(Type.GetEnumNames().IndexOf(results.First()));
                return true;
            }

            output = null;
            return false;
        }
    }
}