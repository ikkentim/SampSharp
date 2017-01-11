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
using System;
using System.Linq;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    /// Represents a single path to a command.
    /// </summary>
    public struct CommandPath
    {
        /// <summary>
        ///     Gets the group.
        /// </summary>
        public string Group { get; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the full name.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        ///     Gets the length.
        /// </summary>
        public int Length => FullName.Length;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandPath" /> struct.
        /// </summary>
        /// <param name="words">The words.</param>
        public CommandPath(params string[] words)
        {
            if (words == null) throw new ArgumentNullException(nameof(words));

            words = words.SelectMany(w => w.Split(' ')).Where(w => !string.IsNullOrEmpty(w)).ToArray();

            if (words.Length == 0)
                throw new ArgumentException("must contain at least one non-empty word", nameof(words));
            Group = string.Join(" ", words.Take(words.Length - 1));
            Name = words.Last();
            FullName = string.IsNullOrEmpty(Group) ? Name : $"{Group} {Name}";
        }

        /// <summary>
        ///     Matches the specified command text.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="ignoreCase">A value indicating whether to ignore the case of the command.</param>
        /// <returns>true if matches; false otherwise.</returns>
        public bool Matches(string commandText, bool ignoreCase = true)
        {
            // The command text is shorter than the full name.
            if (commandText.Length < Length)
                return false;

            // The command text is longer than the full name.
            if (commandText.Length > Length)
            {
                // The next character in the command text must be a space.
                if (commandText[Length] != ' ')
                    return false;
            }

            // The substring of the command text matches the full name.
            return ignoreCase
                ? commandText.Substring(0, Length).ToLower() == FullName.ToLower()
                : commandText.Substring(0, Length) == FullName;
        }

        #region Overrides of ValueType

        /// <summary>
        ///     Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return FullName;
        }

        #endregion
    }
}