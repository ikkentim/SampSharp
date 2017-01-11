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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands.ParameterTypes
{
    /// <summary>
    ///     Represents a player command parameter.
    /// </summary>
    public class PlayerType : ICommandParameterType
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

            // find a player with a matching id.
            int id;
            if (int.TryParse(word, NumberStyles.Integer, CultureInfo.InvariantCulture, out id))
            {
                var player = BasePlayer.Find(id);
                if (player != null)
                {
                    output = player;
                    commandText = commandText.Substring(word.Length).TrimStart(' ');
                    return true;
                }
            }

            var lowerWord = word.ToLower();

            // find all candiates containing the input word, case insensitive.
            var candidates = BasePlayer.All.Where(p => p.Name.ToLower().Contains(lowerWord))
                .ToList();

            // in case of ambiguities find all candiates containing the input word, case sensitive.
            if (candidates.Count > 1)
                candidates = candidates.Where(p => p.Name.Contains(word)).ToList();

            // in case of ambiguities find all candiates matching exactly the input word, case insensitive.
            if (candidates.Count > 1)
                candidates = candidates.Where(p => p.Name.ToLower() == lowerWord).ToList();

            // in case of ambiguities find all candiates matching exactly the input word, case sensitive.
            if (candidates.Count > 1)
                candidates = candidates.Where(p => p.Name == word).ToList();

            if (candidates.Count == 1)
            {
                output = candidates.First();

                commandText = word.Length == commandText.Length
                    ? string.Empty
                    : commandText.Substring(word.Length).TrimStart(' ');
                return true;
            }

            return false;
        }

        #endregion
    }
}