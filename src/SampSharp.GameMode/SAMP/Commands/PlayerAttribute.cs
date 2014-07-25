// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System.Linq;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents a player command-parameter.
    /// </summary>
    public class PlayerAttribute : WordAttribute
    {
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
            Player player = null;
            var word = (output as string).ToLower();
            
            /*
             * Check whether the word is not a number.
             * If it is, find the player with this id.
             */
            if (!int.TryParse(word, out id))
            {
                var players = Player.All.Where(p => p.Name.ToLower().Contains(word.ToLower()));
                if (players.Count() == 1)
                {
                    player = players.First();
                }
            }
            else
            {
                player = Player.Find(id);
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