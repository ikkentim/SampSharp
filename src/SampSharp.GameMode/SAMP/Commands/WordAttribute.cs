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

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents an word command-parameter.
    /// </summary>
    public class WordAttribute : ParameterAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the WordAttribute class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        public WordAttribute(string name) : base(name)
        {
        }

        /// <summary>
        ///     Check if the parameter is well-formatted and return the output.
        /// </summary>
        /// <param name="command">The command text.</param>
        /// <param name="output">The output of this parameter.</param>
        /// <returns>True if the parameter is well-formatted, False otherwise.</returns>
        public override bool Check(ref string command, out object output)
        {
            /*
             * A word should at least be one character long.
             */
            if (command.Length == 0)
            {
                output = null;
                return false;
            }

            int idx = command.IndexOf(' ');
            string word = idx == -1 ? command : command.Substring(0, idx);

            output = word;
            command = word == command ? string.Empty : command.Substring(word.Length);

            return true;
        }
    }
}