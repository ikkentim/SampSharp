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

using System.Linq;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents a player command-parameter.
    /// </summary>
    public class PlayerAttribute : WordAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerAttribute class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        public PlayerAttribute(string name)
            : base(name)
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
            if (!base.Check(ref command, out output))
                return false;

            int id;
            BasePlayer player = null;
            var word = (output as string).ToLower();

            /*
             * Check whether the word is not a number.
             * If it is, find the player with this id.
             */
            if (!int.TryParse(word, out id))
            {
                var players = BasePlayer.All.Where(p => p.Name.ToLower().Contains(word.ToLower()));
                if (players.Count() == 1)
                {
                    player = players.First();
                }
            }
            else
            {
                player = BasePlayer.Find(id);
            }

            if (player == null || !player.IsConnected)
            {
                output = null;
                return false;
            }

            output = player;
            return true;
        }
    }
}