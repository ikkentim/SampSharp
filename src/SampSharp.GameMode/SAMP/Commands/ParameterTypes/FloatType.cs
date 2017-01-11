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
using System.Globalization;
using System.Linq;

namespace SampSharp.GameMode.SAMP.Commands.ParameterTypes
{
    /// <summary>
    ///     Represents a float command parameter.
    /// </summary>
    public class FloatType : ICommandParameterType
    {
        #region Implementation of ICommandParameterType

        /// <summary>
        ///     Gets the value for the occurance of this parameter type at the start of the commandText. The processed text will be
        ///     removed from the commandText.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="output">The output.</param>
        /// <returns>
        ///     true if parsed successfully; false otherwise.
        /// </returns>
        public bool Parse(ref string commandText, out object output)
        {
            var text = commandText.TrimStart();
            output = null;

            if (string.IsNullOrEmpty(text))
                return false;

            var word = text.Split(' ').First();

            // Unify input culture.
            var preProcessedWord = word;
            var commaCount = preProcessedWord.Count(c => c == ',');
            var dotCount = preProcessedWord.Count(c => c == '.');

            if (commaCount > 0 && dotCount == 0)
            {
                preProcessedWord = commaCount == 1
                    ? preProcessedWord.Replace(',', '.') // comma is decimal separator.
                    : preProcessedWord.Replace(",", ""); // comma is thousands separator.
            }
            else if (dotCount > 0 && commaCount == 0)
            {
                preProcessedWord = dotCount == 1
                    ? preProcessedWord // dot is decimal separator.
                    : preProcessedWord.Replace(".", ""); // dot is thousands separator.
            }
            else if (commaCount > 0 && dotCount > 0)
            {
                var firstComma = preProcessedWord.IndexOf(',');
                var firstDot = preProcessedWord.IndexOf('.');

                preProcessedWord = firstComma < firstDot
                    ? preProcessedWord.Replace(",", "") // comma is thousands separator, dot is decimal separator.
                    : preProcessedWord.Replace(".", "").Replace(',', '.');
                // dot is thousands separator, comma is decimal separator.
            }

            // Parse the number
            float number;
            if (float.TryParse(preProcessedWord, NumberStyles.Float, CultureInfo.InvariantCulture, out number))
            {
                commandText = commandText.Substring(word.Length).TrimStart(' ');
                output = number;
                return true;
            }

            return false;
        }

        #endregion
    }
}